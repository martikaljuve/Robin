#include "CMotor.h"
#include "CWheels.h"

Motor motorLeft = Motor(MOTOR_LEFT_PWM, MOTOR_LEFT_DIR);
Motor motorRight = Motor(MOTOR_RIGHT_PWM, MOTOR_RIGHT_DIR);
Motor motorBack = Motor(MOTOR_BACK_PWM, MOTOR_BACK_DIR);
Motor dribbler = Motor(MOTOR_TOP_PWM, MOTOR_TOP_DIR);
Wheels wheels = Wheels(motorLeft, motorRight, motorBack);

void motors_setup() {
	pinMode(MOTOR_MODE, OUTPUT);
	digitalWrite(MOTOR_MODE, HIGH);
	
	dribbler.setSpeed(255);
}

void motors_loop() {
	
}
