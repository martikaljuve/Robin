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
void Motor::setSpeedWithDirection(int newSpeed) {
	// HACK: Let's not allow negative speed at the moment.
	if (newSpeed < 0) {
		newSpeed = 0;
		//Serial.println("Negative speed not allowed at the moment.");
	}

	speed = newSpeed;

	if (speed < -255) speed = -255;
	if (speed > 255) speed = 255;

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