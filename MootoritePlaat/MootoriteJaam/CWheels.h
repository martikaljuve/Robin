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
	static const int MAX_RPM = 400;
public:

	Wheels(Motor& left, Motor& right, Motor& back, Pid& leftPid, Pid& rightPid, Pid& backPid);
	void move(int direction, int speed);
	void turn(int speed);
	void moveAndTurn(int direction, int moveSpeed, int turnSpeed);
	void moveAndTurnWithoutPid(int direction, int moveSpeed, int turnSpeed);
	void stop();
	void setSpeedsWithoutPid(int left, int right, int back);

	void ForwardKinematics();

private:
	void moveAndTurnCalculate(int direction, int moveSpeed, int turnSpeed, int &left, int &right, int &back);
};