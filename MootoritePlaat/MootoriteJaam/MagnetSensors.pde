#include "CMagnetSensor.h"

MagnetSensor magnetLeft;
MagnetSensor magnetRight;
MagnetSensor magnetBack;

void magnet_sensors_setup();
void magnet_sensors_loop();
void checkAngles();

TimedAction angleAction = TimedAction(2, checkAngles);

const int speedCalcInterval = 10;
int checkCount = 0;

void magnet_sensors_setup() {
	magnetLeft = MagnetSensor(MAGNET_SS_LEFT, SCK, MISO);
	magnetRight = MagnetSensor(MAGNET_SS_RIGHT, SCK, MISO);
	magnetBack = MagnetSensor(MAGNET_SS_BACK, SCK, MISO);
}

void magnet_sensors_loop(){
	angleAction.check();
}

void checkAngles(){
	magnetLeft.takeMeasurement();
	magnetRight.takeMeasurement();
	magnetBack.takeMeasurement();
	
	checkCount++;
	if (checkCount == speedCalcInterval) {
		checkCount = 0;
		
		long time = millis();
		magnetLeft.calculateSpeed(time);
		magnetRight.calculateSpeed(time);
		magnetBack.calculateSpeed(time);
	}
}
