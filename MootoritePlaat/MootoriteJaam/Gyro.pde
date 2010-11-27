#include "CGyroscope.h"

#define GYRO_DEBUG

TimedAction gyroAction = TimedAction(5, gyroCheck);

#ifdef GYRO_DEBUG
TimedAction gyroDebugAction = TimedAction(500, gyroDebug);
#endif

unsigned long previousGyroTime;

void gyro_setup() {
	pinMode(SS_GYRO, OUTPUT); // Magnet sensors won't work unless this slave select pin is set as OUTPUT.
}

void gyro_loop() {
	gyroAction.check();

#ifdef GYRO_DEBUG
	gyroDebugAction.check();
#endif
}

void gyroCheck() {
	int current = millis();
	previousGyroTime = current;

	gyro.update(previousGyroTime - current);
}

#ifdef GYRO_DEBUG
void gyroDebug() {
	Serial.print("Gyro angle: ");
	Serial.println(gyro.getCurrentAngle());
}
#endif