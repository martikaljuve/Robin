#include "CMotor.h"
#include "CWheels.h"

Motor motorLeft;
Motor motorRight;
Motor motorBack;
Motor dribbler;
Wheels wheels;

void motors_setup() {
	pinMode(MOTOR_MODE, OUTPUT);
	digitalWrite(MOTOR_MODE, HIGH);
	
	motorLeft = Motor(MOTOR_LEFT_PWM, MOTOR_LEFT_DIR);
	motorRight = Motor(MOTOR_RIGHT_PWM, MOTOR_RIGHT_DIR);
	motorBack = Motor(MOTOR_BACK_PWM, MOTOR_BACK_DIR);
	dribbler = Motor(MOTOR_TOP_PWM, MOTOR_TOP_DIR);
	wheels = Wheels(motorLeft, motorRight, motorBack);

	dribbler.setSpeed(255);
}

void motors_loop() {
	
}
