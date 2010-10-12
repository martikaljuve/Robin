#pragma once
#include <stdlib.h>
#include <avr/io.h>
#include "../utils/binary.h"
 
/*
	@author Jorma Rebane, Universitas Tartuensis
	@date 08.10.2010
	@notes: 
	--- ROBOTEX 2010 ---
	Motors.h for ATmega324P for simplified control over
	motors and PWM pulse width
 */
 
typedef volatile uint8_t* REG_PTR;
 
#define MIN_SPEED 96 // v2ikseim pwm, millega �ks ratas suudab kogu robotit liigutada
#define STOP_SPEED 2 // v2him kiirus mis mootorile sobilik. 0x00 tekitab halba myra
 
#define MotorCount 4
#define M0_DIR PA2	//DIR_L
#define M0_PWM PB3	//PWM_L
 
#define M1_DIR PA3	//DIR_R
#define M1_PWM PB4	//PWM_R
 
#define M2_DIR PC5	//DIR_B
#define M2_PWM PD7	//PWM_B
 
#define M3_DIR PC4	//DIR_U
#define M3_PWM PD6	//PWM_U
 
#define M0_FWD 1
#define M1_FWD 1
#define M2_FWD 0
#define M3_FWD 1
 
#define FWD 1 
#define BWD 0
 
 
/* The Motor struct containing all necessary data for grouping Motor related data into a class like structure */
typedef struct __Motor {
	uint8_t target_pwm; // the target pwm the motor wants to reach
	REG_PTR	pwm; // the value where to flip the power
	uint8_t	dir_pin;  // the pin to flip, in order to change motor dir
	REG_PTR dir_port; // port where to flip the dir_pin
	uint8_t	current_direction; // current direction of the motor (FORWARD 1 or 0 BACKWARD)
	uint8_t fwd_dir; // The direction which is 'forward' on this motor
} Motor;
 
 
/* Creates a new motor struct with the given pwm REG_PTR, dir_port REG_PTR, 
 the dir_pin number in the dir_port and forwardDirection 0/1 - which direction is forward */
Motor* newMotor(REG_PTR pwm, uint8_t dir_pin, REG_PTR dir_port, uint8_t forwardDirection)
{
	Motor* m = (Motor*)malloc(sizeof(Motor));
	m->pwm = pwm;
	m->dir_pin = dir_pin;
	m->dir_port = dir_port;
	m->fwd_dir = forwardDirection;
	return m;
}
 
 
 
/* The motors array containing the pointers to motor structs */
Motor* motors[MotorCount];
 
 
 
 
/* Sets output to low 0. */
#define output_lo(port, pin) (*(port) &= ~(1<<(pin))) /* flip the port pin to 0 */
 
/* Sets output to high 1. */
#define output_hi(port, pin) (*(port) |= (1<<(pin))) /* flip the port pin to 1 */
 
 
 
 
/* Sets the direction of a motor struct pointer. */
#define set_motor_dir(motor_ptr, dir) motor_ptr->current_direction = dir; \
	(motor_ptr->fwd_dir ? dir : !dir) \
	? output_hi( motor_ptr->dir_port,  motor_ptr->dir_pin ) \
	: output_lo( motor_ptr->dir_port,  motor_ptr->dir_pin )
/* Toggles the direction of a motor struct pointer. */
#define toggle_motor_dir(motor_ptr) motor_ptr->current_direction = !motor_ptr->current_direction; \
	(*(motor_ptr->dir_port) ^= (1 << (motor_ptr->dir_pin))) // pordi baidis p��ratakse 'pin'-is bitt vastupidiseks, i.e.  value = !value
 
 
 
#define get_motor_pwm(motor) *(motor->pwm)
#define set_motor_target_pwm(motor, value) motor->target_pwm = value
 
 
/* Sets the PWM pulse width of a motor struct pointer. MAX Value 255, MIN Value 0. */
#define set_motor_pwm(motor, value) get_motor_pwm(motor) = (value)
/* Increments the PWM value of a motor struct pointer by a given value. */
#define incr_motor_pwm(motor, value) get_motor_pwm(motor) += (value)
/* Decrements the PWM value of a motor struct pointer by a given value. */
#define decr_motor_pwm(motor, value) get_motor_pwm(motor) -= (value)
 
 
/* Sets the direction of a motor with the given index 
   (0..MotorCount-1) in motors array. */
#define set_dir(motor_id, dir) set_motor_dir(motors[motor_id], dir)
/* Toggles the direction of a motor with the given index 
   (0..MotorCount-1) in motors array. */
#define toggle_dir(motor_id) toggle_motor_dir(motors[motor_id])
 
 
 
#define get_pwm(motor_id) get_motor_pwm(motors[motor_id])
#define get_pwmRegPtr(motor_id) motors[motor_id]->pwm
#define set_target_pwm(motor_id, value) set_motor_target_pwm(motors[motor_id], value)
 
 
/* Sets the PWM pulse width of a motor with the given index 
   (0..MotorCount-1) in motors array. MAX Value 255, MIN Value 0. */
#define set_pwm(motor_id, value) set_motor_pwm(motors[motor_id], value)
/* Increments the PWM of a motor with the given index 
(0..MotorCount-1) in the motors array by the given value */
#define incr_pwm(motor_id, value) incr_motor_pwm(motors[motor_id], value)
/* Decrements the PWM of a motor with the given index 
(0..MotorCount-1) in the motors array by the given value */
#define decr_pwm(motor_id, value) decr_motor_pwm(motors[motor_id], value)
 
#define DECAY_MODE_FAST 0
#define DECAY_MODE_SLOW 1
 
#define PWM_MODE_FAST 1
#define PWM_MODE_PHASE_CORRECT 2
 
#define PRESCALING_OFF 1
#define PRESCALING_8 2
#define PRESCALING_64 3
#define PRESCALING_256 4
#define PRESCALING_1024 5
 
uint8_t __pwm_mode;
uint8_t __prescaling;
uint8_t getPwmMode()	{ return __pwm_mode; }
uint8_t getPrescaling() { return __prescaling; }
 
void pwm_mode(uint8_t pwm_mode, uint8_t prescaler)
{
	/*@note:
		@pdf # - check the # page in ATmega datasheet for more details
 
		The PWM is generated by two timers: timer_0 and timer_2
		The timer settings in the Timer Counter Control Registers (TCCR)-s
		for specific PWM modes are described below:
	*/
 
	// validate pwm and prescaler
	if(1 <= pwm_mode && pwm_mode <= 2) 
		__pwm_mode = pwm_mode;
	if(1 <= prescaler && prescaler <= 5)
		__prescaling = prescaler;
 
	switch(__pwm_mode) 
	{
	case PWM_MODE_FAST:
		// FAST PWM MODE
		/* @pdf 104
			set 10 10 00 11
			10 - clear OC0A on compare match
			10 - clear OC0B on compare match
			00 - not used
			11 - Fast PWM mode
		*/
		TCCR0A = B8(10100011); // = 0xa3;
		/* @pdf 107
			set 00 00 0 ###
			00 - these bits must be set zero for PWM mode
			00 - not used
			0 - 0 for any simple PWM (upcounts to 0xFF)
			### - 001 = No prescaling, 010 = 8, 011 = 64, 100 = 256, 101 = 1024
		*/
		TCCR0B = __prescaling;
 
		// similar settings as above, but:
		// clear OC2A on match, clear OC2B on match
		TCCR2A = B8(10100011);
		TCCR2B = __prescaling;
		break;
	case PWM_MODE_PHASE_CORRECT:
		// PHASE CORRECT PWM MODE
		/* @pdf 105
			set 10 10 00 01
			10 - Clear OC0A on Compare Match when up-counting. Set OC0A on Compare Match when down-counting.
			10 - Clear OC0B on Compare Match when up-counting. Set OC0B on Compare Match when down-counting.
			00 - not used
			01 - PWM, Phase Correct
		*/
		TCCR0A = B8(10100001);
		/* @pdf 107
			set 00 00 0 001
			00 - these bits must be set zero for PWM mode
			00 - not used
			0 - 0 for any simple PWM (upcounts to 0xFF)
			001 - No prescaling
		*/
		TCCR0B = __prescaling;
 
		// similar settings as above, but for OC2A and OC2B
		TCCR2A = B8(10100001);
		TCCR2B = __prescaling;
		break;
	}
 
}
 
/* Sets the decay mode of the motors.
   Possible options:
	DECAY_MODE_FAST
	DECAY_MODE_SLOW
*/
void set_decay_mode(uint8_t DECAY_MODE_)
{
	/*@note PORTC, pin PC7 (7) sets the
	 decay mode
	 */
	switch(DECAY_MODE_) {
	case DECAY_MODE_FAST: // 0
		output_lo(&PORTC, PC7);
		break;
	case DECAY_MODE_SLOW: // 1
		output_hi(&PORTC, PC7);
		break;
	}
}
 
/* Initializes the motors */
void init_motors(uint8_t PWM_MODE_, uint8_t PRESCALING_)
{
	pwm_mode(PWM_MODE_, PRESCALING_);
 
	motors[0] =	newMotor(&OCR0A, M0_DIR, &PORTA, M0_FWD);
	motors[1] =	newMotor(&OCR0B, M1_DIR, &PORTA, M1_FWD);
	motors[2] =	newMotor(&OCR2A, M2_DIR, &PORTC, M2_FWD);
	motors[3] =	newMotor(&OCR2B, M3_DIR, &PORTC, M3_FWD);
 
	// Set port pins to 'output' mode (1)
	output_hi(&DDRA, M0_DIR); //PA2
    output_hi(&DDRB, M0_PWM); //PB3 -> OC0A
    output_hi(&DDRA, M1_DIR); //PA3
    output_hi(&DDRB, M1_PWM); //PB4 -> OC0B
    output_hi(&DDRC, M2_DIR); //PC5 
    output_hi(&DDRD, M2_PWM); //PD7 -> OC2A
	output_hi(&DDRC, M3_DIR); //PC4 
	output_hi(&DDRD, M3_PWM); //PD6 -> OC2B
}