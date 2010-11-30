#include "CGyroscope.h"

//#define GYRO_DEBUG

TimedAction gyroAction = TimedAction(5, gyroCheck);

unsigned long previousGyroTime;

void gyro_setup() {
	pinMode(SS_GYRO, OUTPUT);
}

void gyro_loop() {
	gyroAction.check();
}

void gyroCheck() {
	long current = millis();
	gyro.update(current - previousGyroTime);
	previousGyroTime = current;
}
