#ifndef __AVR_ATmega324P__
#define __AVR_ATmega324P__ // this must be defined
#endif

#define F_CPU 20000000 // ATMega324P is a 20 MHz CPU
 
#include <stdlib.h>
#include <avr/io.h>
#include <util/delay.h>
 
#include <avr/interrupt.h>
#include "motors.h"
 
 
 
#define LED PD2		//LED
 
 
#define set_input(portdir,pin) (portdir &= ~(1<<(pin)))    // pordi pin pööratakse 0-ks - ehk input
#define set_output(portdir,pin) (portdir |= (1<<(pin))) // pordi pin pööratakse 1-ks - ehk output
 
void initialize()
{
	uint8_t mode = PWM_MODE_PHASE_CORRECT;
 
	init_motors(mode, PRESCALING_OFF);
 
	set_output(DDRD, LED);
}
 
 
/* @author Jorma 
 * Sets all motors to target PWM 
 */
void set_all(uint8_t targetPwm) 
{
	uint8_t i, notEqual;
	uint8_t target_below_min = (targetPwm < MIN_SPEED);
 
	if(target_below_min) 
		targetPwm = 0;
 
	REG_PTR pwmPtr;
	do
	{
		notEqual = 0;
		for(i=0; i < MotorCount; i++)
		{
			pwmPtr = motors[i]->pwm;
 
			if(*pwmPtr < MIN_SPEED)
				*pwmPtr = (target_below_min) ? 0 : MIN_SPEED;
 
			if(*pwmPtr > targetPwm)
				*pwmPtr -= 1;
			else if(*pwmPtr < targetPwm)
				*pwmPtr += 1;
 
			if(*pwmPtr != targetPwm)
				++notEqual; // increase non equal counter
 
		}
 
		_delay_ms(10);
 
	} while(notEqual);
 
}
 
 
int main()
{
	initialize();
 
	int dir = FWD;
 
	set_dir(0, dir);
	set_dir(1, !dir);
	set_dir(2, dir);
	set_dir(3, !dir);
 
	while(1)
	{
		// stop all
		set_all(0);
 
		set_dir(0, dir);
		set_dir(1, !dir);
		set_dir(2, dir);
		set_dir(3, !dir);
 
		set_all(255);
 
		_delay_ms(500);
		if(dir)
			set_output(PORTD, LED);
		else
			set_input(PORTD, LED);
 
		dir = !dir;
	}
 
 
	return 0;
}
