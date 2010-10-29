#include "WProgram.h"
#include "CMotor.h"

Motor::Motor() {
	minPwm = 0;
	maxPwm = 255;
}

Motor::Motor(byte speedPin, byte directionPin) {
	pwmPin = speedPin;
	dirPin = directionPin;
	
	pinMode(pwmPin, OUTPUT);
	pinMode(dirPin, OUTPUT);
}

// speed - -255..255
void Motor::setSpeed(int newSpeed) {
	speed = newSpeed;

	setPwm(abs(speed));
	setDirection(speed >= 0);
}

void Motor::setPwm(byte newPwm) {
	pwm = newPwm;

	// Map PWM to specific min/max values
	if (minPwm == 0 && maxPwm == 255)
		analogWrite(pwmPin, pwm);
	else
		analogWrite(pwmPin, map(pwm, 0, 255, minPwm, maxPwm));
}

void Motor::setDirection(bool forward) {
	direction = forward;
	digitalWrite(dirPin, direction);
}

void Motor::stop() {
	analogWrite(pwmPin, 0);
}

void Motor::setMinMaxPwm(byte min, byte max) {
	minPwm = min;
	maxPwm = max;
}