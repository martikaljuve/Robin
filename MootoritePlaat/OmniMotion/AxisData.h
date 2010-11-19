#pragma once

struct AxisData {
	/*unsigned int numerator;		// millimeter/meter
	unsigned int denominator;	// ticks/meter
	int previous;
	int current;
	bool isMoving;*/
	
	// Trajectory input parameters
	/*long endPosition;
	long velocity;
	unsigned int acceleration;
	unsigned int trajectory;*/
	
	// Trajectory generator output
	/*int generatedVelocity;
	int generatedPosition;*/
	
	// PID variables
	/*int desiredPosition;
	int errorIntegral;
	int errorPrevious;*/
	bool isHolding;

	/*AxisData(unsigned int num, unsigned int den) {
		numerator = num;
		denominator = den;
	}*/
};

enum Axis { X, Y, ROTATION };