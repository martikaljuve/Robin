#pragma once

#include "CMotor.h"
#include "CMagnetSensor.h"
#include "AxisData.h"

class HolonomicWheels {
	Motor motorLeft;
	Motor motorRight;
	Motor motorBack;
	MagnetSensor magnetLeft;
	MagnetSensor magnetRight;
	MagnetSensor magnetBack;
	AxisData axisX;
	AxisData axisY;
	AxisData axisRotation;
	int leftSetpoint;
	int errorPrevious, errorIntegral;
	int pGain, iGain, dGain;
public:
	HolonomicWheels(Motor& left, Motor& right, Motor& back, MagnetSensor& leftMagnet, MagnetSensor& rightMagnet, MagnetSensor& backMagnet);

	void Update();
	void Move(enum Axis axis, int endPosition, int velocity, unsigned int acceleration);
	void Stop(enum Axis axis);
	void Hold(enum Axis axis, bool hold);
	
	void SetPidGains(unsigned int p, unsigned int i, unsigned int d);
	void PidControl();

private:
	void InverseKinematics();
	void ForwardKinematics();
	void Acceleration();

	AxisData& GetAxisData(enum Axis axis);
};