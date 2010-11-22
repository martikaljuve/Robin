#include "CMovementPlanner.h"

void MovementPlanner::setMovement(int leftRpm, int rightRpm, int backRpm) {
	speedLeft = leftRpm * rpmToTenthDegreesPerMillisecond;
	speedRight = rightRpm * rpmToTenthDegreesPerMillisecond;
	speedBack = backRpm * rpmToTenthDegreesPerMillisecond;
}

void MovementPlanner::update(unsigned long deltaInMilliseconds) {
	positionLeft += speedLeft * deltaInMilliseconds;
	positionRight += speedRight * deltaInMilliseconds;
	positionBack += speedBack * deltaInMilliseconds;
}