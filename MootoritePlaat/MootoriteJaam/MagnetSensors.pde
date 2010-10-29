void magnet_sensors_setup();
void magnet_sensors_loop();
void checkAngles();

TimedAction angleAction = TimedAction(2, checkAngles);

const int speedCalcInterval = 5;
int checkCount = 0;

void magnet_sensors_setup() {

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
