#include "CMotor.h"

#define RADIUS 1

class Wheels {
	Motor motorLeft;
	Motor motorRight;
	Motor motorBack;
public:
	Wheels(Motor left, Motor right, Motor back);
	void moveAndTurn(int direction, int moveSpeed, int turnSpeed);
	void stop();
	void setSpeeds(int left, int right, int back);

private:
	float degreesToRadians(int degrees);
};
