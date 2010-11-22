TimedAction angleAction = TimedAction(30, checkAngles);

void magnet_sensors_setup() {

}

void magnet_sensors_loop(){
	angleAction.check();
}

void checkAngles() {
	magnetLeft.update();
	magnetRight.update();
	magnetBack.update();
}