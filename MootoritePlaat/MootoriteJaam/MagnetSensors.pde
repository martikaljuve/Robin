TimedAction angleAction = TimedAction(5, checkAngles);

bool magnetsReset = false;

void magnet_sensors_setup() { }

void magnet_sensors_loop(){
	angleAction.check();
}

void checkAngles() {
	magnetLeft.update();
	magnetRight.update();
	magnetBack.update();
}
