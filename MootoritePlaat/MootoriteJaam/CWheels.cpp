#include <WProgram.h>
#include "CMagnetSensor.h"
#include "CWheels.h"
#include "CPid.h"
#include "SinLookupTable.h"

void Wheels::moveDistance(int localDirection, int distance) {
	moveAndTurnDistance(localDirection, distance, 0);
}

void Wheels::turnDistance(int localRotation) {
	moveAndTurnDistance(0, 0, localRotation);
}

// decidegrees, millimeters, decidegrees
void Wheels::moveAndTurnDistance(int localDirectionInDeciDegrees, int distance, int localRotation) {
	int localDirectionInRadians = M_PI * localDirectionInDeciDegrees / 1.8; // 180 * 10 / 1000
	
	long sinRotation = SinLookupTable::getSinFromTenthDegrees(worldCurrentTheta);
	long cosRotation = SinLookupTable::getCosFromTenthDegrees(worldCurrentTheta);

	long localX = SinLookupTable::getSin(localDirectionInRadians);
	long localY = SinLookupTable::getCos(localDirectionInRadians);

	long worldX = (localX * cosRotation) + (localY * sinRotation);
	long worldY = (-localX * sinRotation) + (localY * cosRotation);

	worldX >>= LOOKUP_SCALE;
	worldY >>= LOOKUP_SCALE;

	worldFinalX = worldCurrentX + (worldX * distance / 1024.0);
	worldFinalY = worldCurrentY + (worldY * distance / 1024.0);
	worldFinalTheta = worldCurrentTheta + localRotation;
}

void Wheels::stop() {
	worldFinalX = worldCurrentX;
	worldFinalY = worldCurrentY;
	worldFinalTheta = worldCurrentTheta;
}

void Wheels::resetGlobalPosition() {
	worldCurrentX = 0;
	worldCurrentY = 0;
	worldCurrentTheta = 0;
}

void Wheels::updateGlobalPosition(long leftWheel, long rightWheel, long backWheel) {
	long localX, localY, localTheta;
	forwardKinematics(leftWheel, rightWheel, backWheel, localX, localY, localTheta);

	worldCurrentTheta += localTheta;

	int cosRotation = SinLookupTable::getCos(worldCurrentTheta);
	int sinRotation = SinLookupTable::getSin(worldCurrentTheta);

	int worldX = (-localX * sinRotation) + (localY * cosRotation);
	int worldY = (localX * cosRotation) + (localY * sinRotation);

	worldX >>= LOOKUP_SCALE;
	worldY >>= LOOKUP_SCALE;
		
	worldCurrentX += worldX;
	worldCurrentY += worldY;
}

void Wheels::getDesiredWheelPositions(long &desiredLeft, long &desiredRight, long &desiredBack) {
	int diffX = worldCurrentX - worldFinalX;
	int diffY = worldCurrentY - worldFinalY;
	int diffTheta = worldCurrentTheta - worldFinalTheta;

	inverseKinematics(diffX, diffY, diffTheta, desiredLeft, desiredRight, desiredBack);
}

void Wheels::forwardKinematics(long left, long right, long back, long &x, long &y, long &theta) {
	// x = ((sqrt(3) * vRight) / 3) - ((sqrt(3) * vLeft) / 3)
	// y = (vRight / 3) + (vLeft / 3) - ((2 * vBack) / 3)
	// theta = (vRight / 3) + (vLeft / 3) + (vBack / 3)

	const long sqrt3 = 1732; // sqrt(3) = 1.73205081 

	x = ((sqrt3 * left) - (sqrt3 * right)) / 3000; // * 1000, because sqrt3 is * 1000
	y = (right + left - (2 * back)) / 3;
	theta = (left + right + back) / -3;

	x /= DECIDEGREES_TO_MILLIMETERS_DIVISOR;
	y /= DECIDEGREES_TO_MILLIMETERS_DIVISOR;
	theta *= WHEEL_TO_ROBOT_ROTATION_MULTIPLIER;
}

void Wheels::inverseKinematics(long x, long y, long theta, long &left, long &right, long &back) {
	// vLeft = -vx * sin(150deg) + vy * cos(150deg) + Rw
	// vRight = -vx * sin(30deg) + vy * cos(30deg) + Rw
	// vBack = -vx * sin(270deg) + vy * cos(270deg) + Rw

	x *= DECIDEGREES_TO_MILLIMETERS_DIVISOR; // 22.5
	y *= DECIDEGREES_TO_MILLIMETERS_DIVISOR; // 22.5
	theta /= WHEEL_TO_ROBOT_ROTATION_MULTIPLIER; // 0.21175225

	int temp1 = -0.5 * x;
	int temp2 = (0.866025404 * y);

	right = temp1 + temp2 + theta;
	left = (temp1 - temp2) + theta;
	back = x + theta;
}

// Coordinate system:
//    ^ +Y
//    |
// <--+--> +X
//    |        |
//    V      <- +Theta
