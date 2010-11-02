TimedAction angleAction = TimedAction(2, checkAngles);
TimedAction speedAction = TimedAction(50, calculateSpeed);

void magnet_sensors_setup() {

}

void magnet_sensors_loop(){
	angleAction.check();
	speedAction.check();
}

void checkAngles() {
	magnetLeft.takeMeasurement();
	magnetRight.takeMeasurement();
	magnetBack.takeMeasurement();
}

void calculateSpeed() {
	long time = millis();
	magnetLeft.calculateSpeed(time);
	magnetRight.calculateSpeed(time);
	magnetBack.calculateSpeed(time);
}