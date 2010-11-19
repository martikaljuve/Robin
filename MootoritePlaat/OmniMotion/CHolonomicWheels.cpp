#include "CHolonomicWheels.h"

HolonomicWheels::HolonomicWheels(Motor& left, Motor& right, Motor& back, MagnetSensor& leftMagnet, MagnetSensor& rightMagnet, MagnetSensor& backMagnet)
	:
	motorLeft(left), motorRight(right), motorBack(back),
	magnetLeft(leftMagnet), magnetRight(rightMagnet), magnetBack(backMagnet)
	{ }

void HolonomicWheels::Update() {
	//Acceleration();
	InverseKinematics();
	PidControl();
}

void HolonomicWheels::Hold(enum Axis axis, bool hold) {
	AxisData axisData = GetAxisData(axis);
	axisData.isHolding = true;
}

void HolonomicWheels::ForwardKinematics() {
	const long sqrt3 = 1732; // sqrt(3) = 1.73205081

	int x = ((sqrt3 * magnetLeft.speed) - (sqrt3 * magnetRight.speed)) / 3000; // (3 * 1000)
	int y = (magnetLeft.speed + magnetRight.speed - (2 * magnetBack.speed)) / 3;
	int rotation = (magnetLeft.speed + magnetRight.speed + magnetBack.speed) / 3;
}

void HolonomicWheels::InverseKinematics() {
	int velX, velY, rotation;

	long temp1 = (-500 * velX) / 1000;
	long temp2 = (866 * velY) / 1000;
	
	long speedLeft = (temp1 - temp2) + rotation;
	long speedRight = (temp1 + temp2) + rotation;
	long speedBack = velX + rotation;
}

void HolonomicWheels::Acceleration() {
	
}

void HolonomicWheels::SetPidGains(unsigned int p, unsigned int i, unsigned int d) {
	pGain = p;
	iGain = i;
	dGain = d;
}

void HolonomicWheels::PidControl() {
	int position = magnetLeft.speed;
	int error = leftSetpoint - position;
	int pwm = pGain * error + dGain * (error - errorPrevious);

	if (iGain) {
		pwm += iGain * errorIntegral;
		errorIntegral += error;
	}

	errorPrevious = error;

	motorLeft.setSpeed(pwm);
}

AxisData& HolonomicWheels::GetAxisData(enum Axis axis) {
	switch(axis) {
	default:
	case X:
		return axisX;
	case Y:
		return axisY;
	case ROTATION:
		return axisRotation;
	}
}