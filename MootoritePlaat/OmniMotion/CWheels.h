#pragma once
#include "CMotor.h"

class HolonomicWheels {
	Motor& motorLeft;
	Motor& motorRight;
	Motor& motorBack;
	AxisData axisX;
	AxisData axisY;
	AxisData axisRotate;
public:
	HolonomicWheels(Motor& left, Motor& right, Motor& back);
	
	void Update();
	void MoveX(int endPosition, int velocity, unsigned int acceleration);
	void MoveY(int endPosition, int velocity, unsigned int acceleration);
	void Rotate(int endPosition, int velocity, unsigned int acceleration);

private:
	void InverseKinematics();
	void ForwardKinematics();
	void Acceleration();
};

struct AxisData {
	unsigned int numerator;		// millimeter/meter
	unsigned int denominator;	// ticks/meter
	int previous;
	int current;
	bool done;
	
	// Trajectory input parameters
	long endPosition;
	long velocity;
	unsigned int acceleration;
	unsigned int trajectory;
	
	// Trajectory generator output
	int generatedVelocity;
	int generatedPosition;
	
	// PID variables
	int desiredPosition;
	int errorIntegral;
	int errorPrevious;
	bool hold;
}

HolonomicWheels::HolonomicWheels(Motor& left, Motor& right, Motor& back) {
	motorLeft = left;
	motorRight = right;
	motorBack = back;
	
	axisX = {1000, 55000, 0, 0, true};
	axisY = {1000, 55000, 0, 0, true};
	axisRot = {6283, 35000, 0, 0, true};
}

void HolonomicWheels::Update() {
	Acceleration();
	InverseKinematics();
}

void HolonomicWheels::Accelerate() {
	if (axisX.done)
}