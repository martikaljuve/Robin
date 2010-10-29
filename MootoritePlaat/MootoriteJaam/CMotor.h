#pragma once

class Motor {
	byte pwmPin;
	byte dirPin;
	byte minPwm;
	byte maxPwm;
public:
	int speed;
	byte pwm;
	bool direction;

	Motor();
	Motor(byte speedPin, byte directionPin);
	void setSpeed(int newSpeed);
	void setPwm(byte newPwm);
	void setDirection(bool forward);
	void stop();
	void setMinMaxPwm(byte min, byte max);
};
