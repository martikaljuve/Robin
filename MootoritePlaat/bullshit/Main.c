#include "Main.h"
#include "Serial.h"
#include "bootldr.h"

void blink(int a){
	for(int i = 0; i < a; i++){
		set_output(PORTD, LED);
		_delay_ms(250);
		set_input(PORTD, LED);
		_delay_ms(250);
	}
}

#define PROG_START 0x0000

//send data to comport
void WriteCom(unsigned char dat)
{

  UDRREG(COMPORTNo) = dat;
  while(!(UCSRAREG(COMPORTNo) & (1<<TXCBIT(COMPORTNo))));
  UCSRAREG(COMPORTNo) |= (1 << TXCBIT(COMPORTNo));

}


 
int main()
{

	asm volatile("cli": : );

	#if WDG_En
	  //enable watchdog
	  wdt_enable(WDTO_1S);
	#endif

	set_output(DDRD, LED);
	//set_high(PORTD, LED);
	//initialize();

	ComInit();
  	TimerInit();

		//set_output(SENSOR_R_DDR, SENSOR_R_PIN);
		//set_high(SENSOR_R_PORT, SENSOR_R_PIN);
		
		InitSPI();
		while(1){

			#if WDG_En
		    	//clear watchdog
		    	wdt_reset();
			#endif

			if(TIFRREG & (1<<OCF1A))    //T1 overflow
    		{
      			TIFRREG |= (1 << OCF1A);



				unsigned char dat = 'E';
				WriteCom(dat);
				_delay_ms(750);
			
				//checkSPI();
			}

		}
 
	/*
	int dir = FWD;
 
	set_dir(0, dir);
	set_dir(1, !dir);
	set_dir(2, dir);
	set_dir(3, !dir);

	InitSPI();
	set_input(PORTD, LED);
	*/
 

		// stop all
		//checkSPI();


		//blink(rx_idx+1);

		//_delay_ms(200);

		/*set_all(0);

		set_dir(0, dir);
		set_dir(1, !dir);
		set_dir(2, dir);
		set_dir(3, !dir);
 
		set_all(255);
 
		_delay_ms(500);*/
		//_delay_ms(1500);
		/*if(dir)
			set_output(PORTD, LED);
		else
			set_input(PORTD, LED);
		*/
 
		//dir = !dir;


 
 
	return 0;
}
