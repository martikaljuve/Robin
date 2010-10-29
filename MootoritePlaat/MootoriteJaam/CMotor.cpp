#include "WProgram.h"
#include "CMotor.h"

Motor::Motor() { }

Motor::Motor(int speedPin, int directionPin) {
	pwmPin = speedPin;
	dirPin = directionPin;
	
	pinMode(pwmPin, OUTPUT);
	pinMode(dirPin, OUTPUT);
}

// speed - -255..255
void Motor::setSpeed(int newSpeed) {
	speed = newSpeed;
	digitalWrite(dirPin, speed >= 0 ? HIGH : LOW);
	analogWrite(pwmPin, abs(speed));
}

void Motor::stop() {
	analogWrite(pwmPin, 0);
}
