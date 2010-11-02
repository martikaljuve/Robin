#include <WProgram.h>
#include "CWheels.h"
#include "CPid.h"

Wheels::Wheels(Motor& left, Motor& right, Motor& back, Pid& leftPid, Pid& rightPid, Pid& backPid)
	:
	motorLeft(left),
	motorRight(right),
	motorBack(back),
	pidLeft(leftPid),
	pidRight(rightPid),
	pidBack(backPid) {
}

void Wheels::moveAndTurn(int direction, int moveSpeed, int turnSpeed) {
	int left, right, back;
	
	moveAndTurnCalculate(direction, moveSpeed, turnSpeed, left, right, back);

	pidLeft.setSetpoint(left);
	pidRight.setSetpoint(right);
	pidBack.setSetpoint(back);
}

void Wheels::moveAndTurnWithoutPid(int direction, int moveSpeed, int turnSpeed) {
	int left, right, back;
	
	moveAndTurnCalculate(direction, moveSpeed, turnSpeed, left, right, back);

	setSpeedsWithoutPid(left, right, back);
}

void Wheels::moveAndTurnCalculate(int direction, int moveSpeed, int turnSpeed, int &left, int &right, int &back) {
	float radians = degreesToRadians(direction);
	float vel_x = sin(radians);
	float vel_y = cos(radians);

	int Rw = RADIUS * turnSpeed;

	// int velR = -0.5*vx + 0.866*vy + Rw;
	// int velL = -0.5*vx - 0.866*vy + Rw;
	// int velB = vx + Rw;

	float tmp1 = (-0.5 * vel_x);
	float tmp2 = 0.866 * vel_y;
	float speed_left = tmp1 - tmp2;
	float speed_right = tmp1 + tmp2 ;
	float speed_back = vel_x;

	if (Rw != 0) {
		float rotation = turnSpeed / 255.0;
		speed_left = speed_left + rotation;
		speed_right = speed_right + rotation;
		speed_back = speed_back + rotation;

		if (speed_left > 255 || speed_right > 255 || speed_back > 255) {
			float ratio = 1;
			if (speed_left >= speed_right && speed_left >= speed_back) {
				ratio = 255.0 / speed_left;
				speed_left = 255;
				speed_right = speed_right / ratio;
				speed_back = speed_back / ratio;
			}
			else if (speed_right >= speed_left && speed_right >= speed_back) {
				ratio = 255.0 / speed_right;
				speed_left = speed_left / ratio;
				speed_back = speed_back / ratio;
			}
			else {
				ratio = 255.0 / speed_back;
				speed_left = speed_left / ratio;
				speed_right = speed_right / ratio;
			}
		}
	}

	left = speed_left * moveSpeed; //round(speed_left * moveSpeed);
	right = speed_right * moveSpeed; //round(speed_right * moveSpeed);
	back = speed_back * moveSpeed; //round(speed_back * moveSpeed));
}

void Wheels::stop() {
	pidLeft.setSetpoint(0);
	pidRight.setSetpoint(0);
	pidBack.setSetpoint(0);

	setSpeedsWithoutPid(0, 0, 0);
}

void Wheels::setSpeedsWithoutPid(int left, int right, int back) {
	motorLeft.setSpeedWithDirection(left);
	motorRight.setSpeedWithDirection(right);
	motorBack.setSpeedWithDirection(back);
}

float Wheels::degreesToRadians(int degrees) {
  return M_PI * degrees / 180;
}
