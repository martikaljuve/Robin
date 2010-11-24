#pragma once

#include "CMagnetSensor.h"
#include "CMotor.h"
#include "CPid.h"

#define RADIUS 1

// Rataste andmed:
// 12cm - Ratta keskkoha kaugus roboti keskpunktist
// 12.5cm - Ratta välimiste rullikute keskkoha kaugus roboti keskpunktist
// 2.54cm - Ratta raadius (1 toll)
// ~4.73 - Ratta poolt tehtud ringide arv roboti 360-kraadise pöörde puhul (12cm / 2.54cm)
// ~1700 - Ratta poolt läbitud kraadid roboti 360-kraadise pöörde puhul
// ~16cm - Ratta poolt läbitud distants ühe täispöörde jooksul
// 22.5 - koefitsient magnetanduri näitude ja millimeetrite vaheliseks teisendamiseks
//          * nt. 270 kraadi puhul: 2700 / 22.5 = 120(mm)
//          * nt. 10cm puhul: 100mm * 22.5 = 2250
// 1.745 - koefitsient magnetanduri näitude (0..3600) ja radiaanide lookup tabeli (0..6283) vaheliseks teisendamiseks
//          * nt. 270 kraadi puhul: 2700 * 1.745 = 4711
//          * nt. 4.0 radiaani puhul: 4000 / 1.745 = 2292

class Wheels {
	int speedLeft;
	int speedRight;
	int speedBack;
	MagnetSensor& magnetLeft;
	MagnetSensor& magnetRight;
	MagnetSensor& magnetBack;

	//static const int MAX_RPM = 400;
	//static const int MAX_DISTANCE = 3600;
	static const double THETA_RATIO = 35000 / 6283;

public:
	Wheels(MagnetSensor& leftMagnet, MagnetSensor& rightMagnet, MagnetSensor& backMagnet);

	int worldCurrentX;
	int worldCurrentY;
	int worldCurrentTheta;

	int worldFinalX;
	int worldFinalY;
	int worldFinalTheta;

	//void move(int direction, int speed);
	//void turn(int speed);
	//void moveAndTurn(int direction, int moveSpeed, int turnSpeed);
	
	void moveDistance(int localDirection, int distance);
	void turnDistance(int localRotation);
	void moveAndTurnDistance(int localDirection, int distance, int localRotation);

	void stop();

	void update(unsigned long deltaInMilliseconds, long &desiredLeft, long &desiredRight, long &desiredBack);
	
	static void forwardKinematics(int left, int right, int back, int &x, int &y, int &theta);
	static void inverseKinematics(int x, int y, int theta, int &left, int &right, int &back);

private:
	//void moveAndTurnCalculate(int direction, int moveSpeed, int turnSpeed, int &left, int &right, int &back);
	//void resetDesiredPositions();
	//void setDesiredSpeeds(int leftRpm, int rightRpm, int backRpm);
};

