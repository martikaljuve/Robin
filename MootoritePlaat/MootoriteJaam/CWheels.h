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
	int speedLeft;
	int speedRight;
	int speedBack;
	static const int PID_DELAY = 1000;
public:
	int pidDelayEnd;

	Wheels(Motor& left, Motor& right, Motor& back, Pid& leftPid, Pid& rightPid, Pid& backPid);
	void moveAndTurn(int direction, int moveSpeed, int turnSpeed);
	void moveAndTurnPid(int direction, int moveSpeed, int turnSpeed);
	void stop();
	void setSpeeds(int left, int right, int back);

private:
	float degreesToRadians(int degrees);
	void moveAndTurn(int direction, int moveSpeed, int turnSpeed, int &left, int &right, int &back);
};
