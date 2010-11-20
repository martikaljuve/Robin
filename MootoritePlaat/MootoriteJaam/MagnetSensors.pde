TimedAction angleAction = TimedAction(30, checkAngles);

void magnet_sensors_setup() {

}

void magnet_sensors_loop(){
	angleAction.check();
}

void checkAngles() {
	magnetLeft.takeMeasurement();
	magnetRight.takeMeasurement();
	magnetBack.takeMeasurement();

	long time = millis();
	magnetLeft.calculateSpeed(time);
	magnetRight.calculateSpeed(time);
	magnetBack.calculateSpeed(time);
}