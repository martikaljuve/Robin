#include <WProgram.h>
#include "CWheels.h"

Wheels::Wheels() { }

Wheels::Wheels(Motor left, Motor right, Motor back) {
	motorLeft = left;
	motorRight = right;
	motorBack = back;
}

void Wheels::moveAndTurn(int direction, int moveSpeed, int turnSpeed) {
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

	//setSpeed(round(speed_left * moveSpeed), round(speed_right * moveSpeed), round(speed_back * moveSpeed));
	setSpeeds(speed_left * moveSpeed, speed_right * moveSpeed, speed_back * moveSpeed);
}

void Wheels::stop() {
  setSpeeds(0, 0, 0);
}

void Wheels::setSpeeds(int left, int right, int back) {
#ifdef DEBUG
	Serial.print("L");
	Serial.print(left);
	Serial.print(", R");
	Serial.print(right);
	Serial.print(", B");
	Serial.print(back);
	Serial.println();
#endif

	motorLeft.setSpeed(left);
	motorRight.setSpeed(right);
	motorBack.setSpeed(back);
}

float Wheels::degreesToRadians(int degrees) {
  return M_PI * degrees / 180;
}
