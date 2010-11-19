#pragma once
#include <WProgram.h>

class Motor {
	byte pwmPin;
	byte dirPin;
	byte minPwm;
	byte maxPwm;
	int position;
	bool done;
public:
	int speed;
	byte pwm;
	bool direction;	
	
	Motor(byte speedPin, byte directionPin);
	void setSpeed(int newSpeed);
	void stop();

private:
	void setPwm(byte pwm);
	void setDirection(bool clockwiseFromMotor);
};