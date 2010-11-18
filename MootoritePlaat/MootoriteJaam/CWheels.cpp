#include <WProgram.h>
#include "CWheels.h"
#include "CPid.h"
#include "CWheelSpeedTable.h"

Wheels::Wheels(Motor& left, Motor& right, Motor& back, Pid& leftPid, Pid& rightPid, Pid& backPid)
	:
	motorLeft(left),
	motorRight(right),
	motorBack(back),
	pidLeft(leftPid),
	pidRight(rightPid),
	pidBack(backPid) {
}

void Wheels::move(int direction, int speed) {
	moveAndTurn(direction, speed, 0);
}

void Wheels::turn(int speed) {
	pidLeft.setSetpoint(speed);
	pidRight.setSetpoint(speed);
	pidBack.setSetpoint(speed);
}

void Wheels::moveAndTurn(int direction, int moveSpeed, int turnSpeed) {
	int left, right, back;
	
	moveAndTurnCalculate(direction, moveSpeed, turnSpeed, left, right, back);

	pidLeft.setSetpoint(left);
	pidRight.setSetpoint(right);
	pidBack.setSetpoint(back);
}

void Wheels::moveAndTurnWithoutPid(int direction, int moveSpeed, int turnSpeed) {
	int left, right, back;
	
	moveAndTurnCalculate(direction, moveSpeed, turnSpeed, left, right, back);

	setSpeedsWithoutPid(left, right, back);
}

void Wheels::moveAndTurnCalculate(int direction, int moveSpeed, int turnSpeed, int &left, int &right, int &back) {
	int left1, right1, back1;
	WheelSpeedTable::fromDirection(direction, left1, right1, back1);

	left1 = ((long)left1 * moveSpeed) / 255;
	right1 = ((long)right1 * moveSpeed) / 255;
	back1 = ((long)back1 * moveSpeed) / 255;

	left1 += turnSpeed;
	right1 += turnSpeed;
	back1 += turnSpeed;

	// Constrain values to -max_rpm..max_rpm
	if (abs(left1) > MAX_RPM || abs(right1) > MAX_RPM || abs(back1) > MAX_RPM) {
		int max = max(abs(left1), max(abs(right1), abs(back1)));
		left1 = map(left1, -max, max, -MAX_RPM, MAX_RPM);
		right1 = map(right1, -max, max, -MAX_RPM, MAX_RPM);
		back1 = map(back1, -max, max, -MAX_RPM, MAX_RPM);
	}

	left = left1;
	right = right1;
	back = back1;
}

void Wheels::stop() {
	pidLeft.setSetpoint(0);
	pidRight.setSetpoint(0);
	pidBack.setSetpoint(0);

	setSpeedsWithoutPid(0, 0, 0);
}

void Wheels::setSpeedsWithoutPid(int left, int right, int back) {
	motorLeft.setSpeedWithDirection(left);
	motorRight.setSpeedWithDirection(right);
	motorBack.setSpeedWithDirection(back);
}