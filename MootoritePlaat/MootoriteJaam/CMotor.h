#ifndef MOTOR_H
#define MOTOR_H

class Motor {
	int pwmPin;
	int dirPin;
public:
	int speed;

	Motor();
	Motor(int speedPin, int directionPin);
	void setSpeed(int newSpeed);
	void stop();
};

#endif
