#include "SPI.h"

/*
Function that initializes the SPI. Sets correspoding data-directions, SPCR register and
sensor pin.
*/
void InitSPI(void){

	//OUT
	set_output(DDRB, PB7); //Set SCK
	set_output(DDRB, PB5); //Set MOSI
	set_output(DDRB, PB4); //Set SS

	//IN
	set_input(DDRB, PB6); //Set MISO


	/* SPCR register. Atmega324P datasheet page 168

	Bit7: SPIE: SPI Interrupt Enable
		Causes SPI interrupt to be executed if SPIF bit in the 
		SPSR Register is set and if the Global Interrupt Enable bit in SREG is set.

	Bit6: SPE: SPI Enable
		Enables SPI, must be set to enable any SPI operations.

	Bit5: DORD: Data Order
		1 - LSB of the data word is transmitted first
		0 - MSB of the data word is transmitted first

	Bit4: MSTR: Master/Slave Select
		1 - Master
		0 - Slave
		If SS is input and is driven low while MSTR is set, MSTR will be cleared
		and SPIF in SPSR will become set.

	Bit3: CPOL: Clock Polarity
		1 - SCK is high when idle
		0 - SCK is low when idle

	Bit2: CPHA: Clock Phase
		0 - Leading edge of SCK is sample. Trailing edge is setup.
		1 - Leading edge of SCK is setup. Trailing edge is sample.
	
	Bit1:0: Clock rate select
		If SPI2X is 0:
		  0:0 - f_OBC/4; 0:1 - f_OBC/16; 1:0 - f_OBC/64; 1:1 - f_OBC/128
		If SPI2X is 1:
		  0:0 - f_OBC/2; 0:1 - f_OBC/8; 1:0 - f_OBC/32; 1:1 - f_OBC/64

	*/
	SPCR = 0b11010110;


	//Sensor output
	set_output(SENSOR_DDR, SENSOR_PIN);
	//Sensor high
	set_output(SENSOR_PORT, SENSOR_PIN);


}



/*
Some functions that we may or may not need in the future...
*/

void WriteByteSPI(unsigned char byte){
		
SPDR = byte;					//Load byte to Data register
while(!(SPSR & (1<<SPIF))); 	// Wait for transmission complete 

}

char ReadByteSPI(char addr){
	SPDR = addr;					//Load byte to Data register
	while(!(SPSR & (1<<SPIF))); 	// Wait for transmission complete 
	addr=SPDR;
	return addr;
}




/*
Main code starts here.
Code inspired by: 
http://www.anyma.ch/blogs/research/2010/04/07/very-nice-sensor-mlx90316-rotary-position-sensor/

Also might prove useful:
http://www.edaboard.com/thread150628.html
*/


//We define some variables. 

uint16_t            angle; //We keep the angle value
uint8_t             rx_idx; //Buffer index
uint8_t             rx_buffer[10]; //Read buffer

 
// ==============================================================================
// Talk to MLX90316
// ------------------------------------------------------------------------------
uint8_t spi_transmit_byte(uint8_t byte) {
    SPDR = byte;
    while (!(SPSR & (1<<SPIF))) {}
    return SPDR;
}

/*
This function should be called that many times, as possible.
It selects a sensor and tries to read data from it. Also analyzes the data and 
writes it to angle variable defined before.
*/
 
void checkSPI(void) {

 
 	//If we start the buffer-cycle... index is 0
    if (rx_idx == 0) {

		set_input(SENSOR_PORT, SENSOR_PIN); // select angle sensor
        //PORTA &= ~(1 << 7);     
        _delay_us(6);
        rx_buffer[rx_idx] = spi_transmit_byte(0xAA);
		

        rx_idx++;
    } else {
		
	 
        if (rx_idx < 8) {
            rx_buffer[rx_idx] = spi_transmit_byte(0xFF);
            rx_idx++;
			//set_input(PORTD, LED);       
			
        } else {


			set_output(SENSOR_PORT, SENSOR_PIN); // deselect angle sensor
            //PORTA |= (1 << 7);     
            _delay_ms(2);
            rx_idx = 0;
			
 
            if (rx_buffer[3] & 1) {     // LSB == 0 means error from MLX90316
				//set_input(PORTD, LED);
                uint16_t temp;
                temp = (rx_buffer[2] << 8) + rx_buffer[3];

                if (temp < 0xffff) {    // angle cannot be 0xffff
                    angle = temp >> 2;  // bits 0..1 of data doesn't mean anything
					//set_output(PORTD, LED);

					//We try to make the LED show us the half-circle...
					if(angle > 0 && angle < 180){
						set_output(PORTD, LED);
					}else{
						set_input(PORTD, LED);
					}
                }else{
					 set_output(PORTD, LED);
					 //FOR SOME REASON WE END UP HERE!!!!
				}
            }else{
				
			}
        }
    }
}


/*
Some more noise...
*/



// ==============================================================================
// - main
// ------------------------------------------------------------------------------
/*int main(void)
{
    // ------------------------- Initialize Hardware
 
    //PORTB: Serial Communication
 
    DDRB = ~(1 << 6);       // 1011 1111 - All outputs except MISO 
    SPCR = (1<<SPE) | (1<<MSTR) | (1<<SPR1)|(0<<SPR0) | (0<<CPOL) | (1<<CPHA);      //  enable SPI in Master Mode, clk = fcpu/16 
    PORTB = (1 << 4);       // deselect angle sensor
    PORTB |= (1 << 6);
 
    // ------------------------- Main Loop
    while(1) {
        checkSPI();
    }
    return 0;
}*/
 
