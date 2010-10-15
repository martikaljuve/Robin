#include "MootoritePlaat.h"
 
void initialize(){

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





 
 
 



