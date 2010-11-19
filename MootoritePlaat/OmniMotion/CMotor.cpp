#include "WProgram.h"
#include "CMotor.h"

Motor::Motor(byte speedPin, byte directionPin) {
	minPwm = 0;
	maxPwm = 255;

	pwmPin = speedPin;
	dirPin = directionPin;
	
	pinMode(pwmPin, OUTPUT);
	pinMode(dirPin, OUTPUT);
}

// speed - -255..255
void Motor::setSpeed(int newSpeed) {
	speed = constrain(newSpeed, -255, 255);

	setPwm(abs(speed));
	setDirection(speed >= 0);
}

void Motor::setPwm(byte newPwm) {
	pwm = newPwm;
	
	if (minPwm != 0 || maxPwm != 0)
		pwm = map(pwm, 0, 255, minPwm, maxPwm);

	analogWrite(pwmPin, pwm);
}

void Motor::setDirection(bool clockwiseFromMotor) {
	direction = clockwiseFromMotor;
	digitalWrite(dirPin, direction);
}

void Motor::stop() {
	analogWrite(pwmPin, 0);
}