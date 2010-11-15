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
	WheelSpeedTable::fromDirection(direction, left, right, back);
	
	left = (int)(left * moveSpeed / 255.0);
	right = (int)(right * moveSpeed / 255.0);
	back = (int)(back * moveSpeed / 255.0);

	left += turnSpeed;
	right += turnSpeed;
	back += turnSpeed;

	// Constrain values to -255..255
	if (abs(left) > 255 || abs(right) > 255 || abs(back) > 255) {
		int max = max(abs(left), max(abs(right), abs(back)));
		left = map(left, -max, max, -255, 255);
		right = map(right, -max, max, -255, 255);
		back = map(back, -max, max, -255, 255);
	}
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