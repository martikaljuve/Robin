#include <WProgram.h>
#include "CMagnetSensor.h"
#include "CWheels.h"
#include "CPid.h"
#include "CWheelSpeedTable.h"
#include "SinLookupTable.h"

Wheels::Wheels(MagnetSensor& left, MagnetSensor& right, MagnetSensor& back)
	:
	magnetLeft(left),
	magnetRight(right),
	magnetBack(back) { }

void Wheels::moveDistance(int localDirection, int distance) {
	moveAndTurnDistance(localDirection, distance, 0);
}

void Wheels::turnDistance(int localRotation) {
	moveAndTurnDistance(0, 0, localRotation);
}

void Wheels::moveAndTurnDistance(int localDirectionInRadians, int distance, int localRotation) {
	int cosRotation = CosLut(worldCurrentTheta);
	int sinRotation = SinLut(worldCurrentTheta);

	int localX = SinLut(localDirectionInRadians);
	int localY = CosLut(localDirectionInRadians);

	int worldX = (localX * cosRotation) + (localY * sinRotation);
	int worldY = (-localX * sinRotation) + (localY * cosRotation);

	worldX >>= DSL_SCALE;
	worldY >>= DSL_SCALE;

	worldFinalX = worldX * distance;
	worldFinalY = worldY * distance;
	worldFinalTheta = worldCurrentTheta + localRotation;
}

void Wheels::stop() {
	worldFinalX = worldCurrentX;
	worldFinalY = worldCurrentY;
	worldFinalTheta = worldCurrentTheta;
}

void Wheels::update(unsigned long deltaInMilliseconds, long &desiredLeft, long &desiredRight, long &desiredBack) {
	int leftPosition = magnetLeft.getCurrentDelta();
	int rightPosition = magnetRight.getCurrentDelta();
	int backPosition = magnetBack.getCurrentDelta();

	magnetLeft.resetCurrentDelta();
	magnetRight.resetCurrentDelta();
	magnetBack.resetCurrentDelta();

	int localX, localY, localTheta;
	forwardKinematics(leftPosition, rightPosition, backPosition, localX, localY, localTheta);

	worldCurrentTheta += localTheta;

	int cosRotation = CosLut(worldCurrentTheta);
	int sinRotation = SinLut(worldCurrentTheta);

	int worldX = (localX * cosRotation) + (localY * sinRotation);
	int worldY = (-localX * sinRotation) + (localY * cosRotation);

	worldX >>= DSL_SCALE;
	worldY >>= DSL_SCALE;
		
	worldCurrentX += worldX;
	worldCurrentY += worldY;

	int diffX = worldFinalX - worldCurrentX;
	int diffY = worldFinalY - worldCurrentY;
	int diffTheta = worldFinalTheta - worldCurrentTheta;

	int diffLeft, diffRight, diffBack;
	inverseKinematics(diffX, diffY, diffTheta, diffLeft, diffRight, diffBack);
	
	desiredLeft = magnetLeft.getPositionTotal() + diffLeft;
	desiredRight = magnetRight.getPositionTotal() + diffRight;
	desiredBack = magnetBack.getPositionTotal() + diffBack;

	/*
	desiredPositionLeft += speedLeft * deltaInMilliseconds;
	desiredPositionRight += speedRight * deltaInMilliseconds;
	desiredPositionBack += speedBack * deltaInMilliseconds;*/
}

// Coordinate system:
//    ^ +Y
//    |
// <--+--> +X
//    |        |
//    V      <- +Theta

void Wheels::forwardKinematics(int left, int right, int back, int &x, int &y, int &theta) {
	// x = ((sqrt(3) * vRight) / 3) - ((sqrt(3) * vLeft) / 3)
	// y = (vRight / 3) + (vLeft / 3) - ((2 * vBack) / 3)
	// theta = (vRight / 3) + (vLeft / 3) + (vBack / 3)

	const long sqrt3 = 1732; // sqrt(3) = 1.73205081 

	x = ((sqrt3 * left) - (sqrt3 * right)) / 3000; // * 1000, because sqrt3 is * 1000
	y = (left + right - (2 * back)) / 3;
	theta = (left + right + back) / 3;
}

void Wheels::inverseKinematics(int x, int y, int theta, int &left, int &right, int &back) {
	// vLeft = -vx * sin(150deg) + vy * cos(150deg) + Rw
	// vRight = -vx * sin(30deg) + vy * cos(30deg) + Rw
	// vBack = -vx * sin(270deg) + vy * cos(270deg) + Rw

	int temp1 = -0.5 * x;
	int temp2 = (0.866025404 * y) + theta;

	left = temp1 - temp2;
	right = temp1 + temp2;
	back = x + theta;
}

/*
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

void Wheels::resetDesiredPositions() {
	desiredPositionLeft = magnetLeft.getPosition();
	desiredPositionRight = magnetRight.getPosition();
	desiredPositionBack = magnetBack.getPosition();
}

void Wheels::stop() {
	speedLeft = 0;
	speedRight = 0;
	speedBack = 0;
}

*/

