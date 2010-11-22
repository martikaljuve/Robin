#pragma once

#include "CMotor.h"
#include "CPid.h"

#define RADIUS 1

class Wheels {
	int speedLeft;
	int speedRight;
	int speedBack;
	static const int MAX_RPM = 400;

public:
	long desiredPositionLeft;
	long desiredPositionRight;
	long desiredPositionBack;

	void move(int direction, int speed);
	void turn(int speed);
	void moveAndTurn(int direction, int moveSpeed, int turnSpeed);
	void stop();

	void setDesiredSpeeds(int leftRpm, int rightRpm, int backRpm);
	void update(unsigned long deltaInMilliseconds);

private:
	void moveAndTurnCalculate(int direction, int moveSpeed, int turnSpeed, int &left, int &right, int &back);
};