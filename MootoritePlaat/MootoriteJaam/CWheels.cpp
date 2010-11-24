#include <WProgram.h>
#include "CMagnetSensor.h"
#include "CWheels.h"
#include "CPid.h"
#include "CWheelSpeedTable.h"

Wheels::Wheels(MagnetSensor& left, MagnetSensor& right, MagnetSensor& back)
	:
	magnetLeft(left),
	magnetRight(right),
	magnetBack(back) { }

void Wheels::move(int direction, int speed) {
	moveAndTurn(direction, speed, 0);
}

void Wheels::turn(int speed) {
	moveAndTurn(0, 0, speed);
}

void Wheels::moveAndTurn(int direction, int moveSpeed, int turnSpeed) {
	int left, right, back;
	
	moveAndTurnCalculate(direction, moveSpeed, turnSpeed, left, right, back);

	resetDesiredPositions();
	setDesiredSpeeds(left, right, back);
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

	/*
	Serial.print("New speeds: ");
	Serial.print(left);
	Serial.print(", ");
	Serial.print(right);
	Serial.print(", ");
	Serial.println(back);
	*/
}

void Wheels::resetDesiredPositions() {
	desiredPositionLeft = magnetLeft.position;
	desiredPositionRight = magnetRight.position;
	desiredPositionBack = magnetBack.position;
}

void Wheels::setDesiredSpeeds(int leftRpm, int rightRpm, int backRpm) {
	const long tenthDegreesPerRotation = 1;
	const long centisecondsInMinute = 1;
	//const long tenthDegreesPerRotation = 3600;
	//const long centisecondsInMinute = 6000;

	speedLeft = leftRpm * tenthDegreesPerRotation / centisecondsInMinute;
	speedRight = rightRpm * tenthDegreesPerRotation / centisecondsInMinute;
	speedBack = backRpm * tenthDegreesPerRotation / centisecondsInMinute;
}

void Wheels::update(unsigned long deltaInMilliseconds) {
	desiredPositionLeft += speedLeft * deltaInMilliseconds;
	desiredPositionRight += speedRight * deltaInMilliseconds;
	desiredPositionBack += speedBack * deltaInMilliseconds;
}

void Wheels::stop() {
	speedLeft = 0;
	speedRight = 0;
	speedBack = 0;
}