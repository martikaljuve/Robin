#define F_CPU 20000000 // ATMega324P is a 20 MHz CPU
 
#include <stdlib.h>
#include <avr/io.h>
#include <util/delay.h>

#define MIN_SPEED 64 // Väikseim ühe mootori kiirus, millega robot kohalt võtab
#define MAX_SPEED 255-64 //Maksimaalne võimalik ühe mootori kiirus, 191
#define STOP_SPEED 2 // v2him kiirus mis mootorile sobilik. 0x00 tekitab halba myra
#define LED PD2

#define MODE PC7

 
#define MOTOR_0_DIR PA2  //LEFT
#define MOTOR_0_PWM PB3  //LEFT
 
#define MOTOR_1_DIR PA3  //RIGHT
#define MOTOR_1_PWM PB4  //RIGHT
 
#define MOTOR_2_DIR PC5  //BACK
#define MOTOR_2_PWM PD7  //BACK

#define MOTOR_3_DIR PC4  //UP
#define MOTOR_3_PWM PD6  //UP

#define output_bwd(port,pin) port &= ~(1<<pin)    // pordi baidis pööratakse 'pin'-is bitt 0-ks
#define output_fwd(port,pin) port |= (1<<pin)    // pordi baidis pööratakse 'pin'-is bitt 1-ks, i.e. - pin PA2=2 -> 0x00000[1]00
#define output_toggle(port, pin) port ^= (1 << pin) // pordi baidis pööratakse 'pin'-is bitt vastupidiseks, i.e.  value = !value
 
#define set_input(portdir,pin) portdir &= ~(1<<pin)    //Määrab pinni inputiks, seab pinni 0-ks DDR-de peal
#define set_output(portdir,pin) portdir |= (1<<pin) //Määrab pinni outputiks, seab pinni 1-ks DDR-de peal

#define set_speed(motor, speed) *(uint8_t*)motors[motor] = MIN_SPEED + speed

uint8_t motors[4] = { &OCR0A, &OCR0B, &OCR2A , &OCR2B};  //PWM compare väärtused

/*
Funktsioon, mis tõstab või langetab ühe mootori kiirust sujuvalt. 
Sõltub parameetritest smoothness ja _delay_ms ajast
Saavutab soovitud kiiruse smoothness väärtuse täpsuseni
*/
void set_speed_smooth(motor, speed){
	smoothness = 8; //Change this
	_delay_ms(1); //Change this

	if(motors[motor] < speed-smoothness/2){
		if(motors[motor] += motors[motor]/smoothness > MAX_SPEED){
			motors[motor] = MAX_SPEED;
		}else{
			motors[motor] += motors[motor]/smoothness;
		}
		set_speed_smooth(motor, speed);
	}else if(motors[motor] > speed+smoothness/2){
		motors[motor] -= motors[motor]/smoothness;
		set_speed_smooth(motor, speed);
	}
}


void set_output_pins(){

	set_output(DDRD, LED);
	set_output(DDRA, MOTOR_0_DIR); //PA2  LEFT MOOTOR
    set_output(DDRB, MOTOR_0_PWM); //PB3  LEFT MOOTOR
    set_output(DDRA, MOTOR_1_DIR); //PA3  RIGHT
    set_output(DDRB, MOTOR_1_PWM); //PB4  RIGHT
    set_output(DDRC, MOTOR_2_DIR); //PC5  BACK
    set_output(DDRD, MOTOR_2_PWM); //PD7  BACK
    set_output(DDRC, MOTOR_3_DIR); //PC4  TOP
    set_output(DDRD, MOTOR_3_PWM); //PD6  TOP

}

void start_pwm(){

	TCCR0A = 0b10100001;  //Taimeri 0 register A  | lk 104-106, modes. a3 Fast PWM, a1 phase correct
    TCCR0B = 0b00000001;  //Taimeri 0 register B  | 0,0,1 - PWM is active with a pre-scaler value of 1
 	
	TCCR2A = 0b10100001;  
    TCCR2B = 0b00000001;
 
}

void turn(int angle){
	turn_speed = 100; //Change this

	if(angle > 0){  //Päripäeva
		output_fwd(PORTA, MOTOR_0_DIR);
		output_fwd(PORTA, MOTOR_1_DIR);
		output_fwd(PORTC, MOTOR_2_DIR);
	}else{ //Vastupäeva
		output_bwd(PORTA, MOTOR_0_DIR);
		output_bwd(PORTA, MOTOR_1_DIR);
		output_bwd(PORTC, MOTOR_2_DIR);
	}


	set_speed_smooth(1, turn_speed);
	set_speed_smooth(2, turn_speed);
	set_speed_smooth(3, turn_speed);

	for(i=0;i<angle;i++){
		_delay_ms(30); //Change this value according to the real angle turned
	}

	set_speed_smooth(1, STOP_SPEED);
	set_speed_smooth(2, STOP_SPEED);
	set_speed_smooth(3, STOP_SPEED);
}



int main(void){



	output_fwd(PORTA, MOTOR_0_DIR);
	output_fwd(PORTA, MOTOR_1_DIR);
	output_fwd(PORTC, MOTOR_2_DIR);
	output_fwd(PORTC, MOTOR_3_DIR);

	start_pwm();

	while(1){
		_delay_ms(100);

		//for(i=0;i<40;i+=j){
			set_speed(0, 64);
			set_speed(1, 64);
			set_speed(2, 64);
			set_speed(3, 64);
		//}

		output_toggle(PORTD, LED);
	}






}
