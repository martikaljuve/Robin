#pragma once

#include "CMotor.h"
#include "CPid.h"

#define RADIUS 1

class Wheels {
	Motor& motorLeft;
	Motor& motorRight;
	Motor& motorBack;
	Pid& pidLeft;
	Pid& pidRight;
	Pid& pidBack;
public:

	Wheels(Motor& left, Motor& right, Motor& back, Pid& leftPid, Pid& rightPid, Pid& backPid);
	void moveAndTurn(int direction, int moveSpeed, int turnSpeed);
	void moveAndTurnWithoutPid(int direction, int moveSpeed, int turnSpeed);
	void stop();
	void setSpeedsWithoutPid(int left, int right, int back);

private:
	float degreesToRadians(int degrees);
	void moveAndTurnCalculate(int direction, int moveSpeed, int turnSpeed, int &left, int &right, int &back);
};
