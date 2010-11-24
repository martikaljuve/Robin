#pragma once

#include "CMagnetSensor.h"
#include "CMotor.h"
#include "CPid.h"

#define RADIUS 1

class Wheels {
	int speedLeft;
	int speedRight;
	int speedBack;
	MagnetSensor& magnetLeft;
	MagnetSensor& magnetRight;
	MagnetSensor& magnetBack;

	static const int MAX_RPM = 400;
	static const int MAX_DISTANCE = 3600;

public:
	Wheels(MagnetSensor& leftMagnet, MagnetSensor& rightMagnet, MagnetSensor& backMagnet);

	long desiredPositionLeft;
	long desiredPositionRight;
	long desiredPositionBack;

	void move(int direction, int speed);
	void turn(int speed);
	void moveAndTurn(int direction, int moveSpeed, int turnSpeed);
	void stop();

	void update(unsigned long deltaInMilliseconds);
	
	void forwardKinematics();

private:
	void moveAndTurnCalculate(int direction, int moveSpeed, int turnSpeed, int &left, int &right, int &back);
	void resetDesiredPositions();
	void setDesiredSpeeds(int leftRpm, int rightRpm, int backRpm);
};