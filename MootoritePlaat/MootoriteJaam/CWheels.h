#pragma once

#include "CMotor.h"
#include "CPid.h"

#define RADIUS 1

// Rataste andmed:
// 12cm - Ratta keskkoha kaugus roboti keskpunktist
// 12.5cm - Ratta välimiste rullikute keskkoha kaugus roboti keskpunktist
// 2.54cm - Ratta raadius (1 toll)
// ~4.73 - Ratta poolt tehtud ringide arv roboti 360-kraadise pöörde puhul (12cm / 2.54cm)
// ~1700 - Ratta poolt läbitud kraadid roboti 360-kraadise pöörde puhul
// 17001 - Ratta poolt läbitud detsikraadid roboti 360-kraadise pöörde puhul
// ~16cm - Ratta poolt läbitud distants ühe täispöörde jooksul
// 22.5 - koefitsient magnetanduri näitude ja millimeetrite vaheliseks teisendamiseks
//          * nt. 270 kraadi puhul: 2700 / 22.5 = 120(mm)
//          * nt. 10cm puhul: 100mm * 22.5 = 2250
// 1.745 - koefitsient magnetanduri näitude (0..3600) ja radiaanide lookup tabeli (0..6283) vaheliseks teisendamiseks
//          * nt. 270 kraadi puhul: 2700 * 1.745 = 4711
//          * nt. 4.0 radiaani puhul: 4000 / 1.745 = 2292

#define DECIDEGREES_TO_MILLIMETERS_DIVISOR 22.5
#define WHEEL_TO_ROBOT_ROTATION_MULTIPLIER 0.21175225

class Wheels {
	int speedLeft;
	int speedRight;
	int speedBack;
	//static const int MAX_RPM = 400;
	//static const int MAX_DISTANCE = 3600;

public:
	int worldCurrentX; // millimeters
	int worldCurrentY; // millimeters
	int worldCurrentTheta; // decidegrees

	int worldFinalX; // millimeters
	int worldFinalY; // millimeters
	int worldFinalTheta; // decidegrees

	void moveDistance(int localDirection, int distance); // direction in "decidegrees" (0..3600), distance in millimeters (0..3500)
	void turnDistance(int localRotation); // decidegrees, 1 turn = 3600
	void moveAndTurnDistance(int localDirection, int distance, int localRotation); // decidegrees, millimeters, decidegrees

	void stop();

	void resetGlobalPosition();
	void updateGlobalPosition(int leftWheel, int rightWheel, int backWheel);
	void getDesiredWheelPositions(int &desiredLeft, int &desiredRight, int &desiredBack); // desired: decidegrees
	
	static void forwardKinematics(int left, int right, int back, int &x, int &y, int &theta);
	static void inverseKinematics(int x, int y, int theta, int &left, int &right, int &back);
};

