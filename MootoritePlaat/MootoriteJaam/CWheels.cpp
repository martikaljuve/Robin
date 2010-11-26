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
void Wheels::moveAndTurnDistance(int localDirectionInTenthDegrees, int distance, int localRotation) {
	int localDirectionInRadians = M_PI * localDirectionInTenthDegrees / 1.8; // 180 * 10 / 1000
	
	int sinRotation = SinLookupTable::getSinFromTenthDegrees(worldCurrentTheta);
	int cosRotation = SinLookupTable::getCosFromTenthDegrees(worldCurrentTheta);

	int localX = SinLookupTable::getSin(localDirectionInRadians);
	int localY = SinLookupTable::getCos(localDirectionInRadians);

	long worldX = (localX * cosRotation) + (localY * sinRotation);
	long worldY = (localX * -sinRotation) + (localY * cosRotation);

	worldX >>= LOOKUP_SCALE;
	worldY >>= LOOKUP_SCALE;

	worldFinalX = worldCurrentX + (worldX * distance);
	worldFinalY = worldCurrentY + (worldY * distance);
	worldFinalTheta = worldCurrentTheta + localRotation;

#ifdef DEBUG
	Serial.print("world final: ");
	Serial.print(wheels.worldFinalX);
	Serial.print(", ");
	Serial.print(wheels.worldFinalY);
	Serial.print(", ");
	Serial.println(wheels.worldFinalTheta);
#endif
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
	worldFinalX = 0;
	worldFinalY = 0;
	worldFinalTheta = 0;
}

void Wheels::updateGlobalPosition(int leftWheel, int rightWheel, int backWheel) {
	int localX, localY, localTheta;
	forwardKinematics(leftWheel, rightWheel, backWheel, localX, localY, localTheta);

	worldCurrentTheta += localTheta;

	int cosRotation = SinLookupTable::getCos(worldCurrentTheta);
	int sinRotation = SinLookupTable::getSin(worldCurrentTheta);

	int worldX = (localX * cosRotation) + (localY * sinRotation);
	int worldY = (-localX * sinRotation) + (localY * cosRotation);

	worldX >>= LOOKUP_SCALE;
	worldY >>= LOOKUP_SCALE;
		
	worldCurrentX += worldX;
	worldCurrentY += worldY;
}

void Wheels::getDesiredWheelPositions(int &desiredLeft, int &desiredRight, int &desiredBack) {
	int diffX = worldFinalX - worldCurrentX;
	int diffY = worldFinalY - worldCurrentY;
	int diffTheta = worldFinalTheta - worldCurrentTheta;

	inverseKinematics(diffX, diffY, diffTheta, desiredLeft, desiredRight, desiredBack);
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

	x /= DECIDEGREES_TO_MILLIMETERS_DIVISOR;
	y /= DECIDEGREES_TO_MILLIMETERS_DIVISOR;
	theta *= WHEEL_TO_ROBOT_ROTATION_MULTIPLIER;
}

void Wheels::inverseKinematics(int x, int y, int theta, int &left, int &right, int &back) {
	// vLeft = -vx * sin(150deg) + vy * cos(150deg) + Rw
	// vRight = -vx * sin(30deg) + vy * cos(30deg) + Rw
	// vBack = -vx * sin(270deg) + vy * cos(270deg) + Rw

	x *= DECIDEGREES_TO_MILLIMETERS_DIVISOR;
	y *= DECIDEGREES_TO_MILLIMETERS_DIVISOR;
	theta /= WHEEL_TO_ROBOT_ROTATION_MULTIPLIER;

	int temp1 = -0.5 * x;
	int temp2 = (0.866025404 * y) + theta;

	left = temp1 - temp2;
	right = temp1 + temp2;
	back = x + theta;
}
