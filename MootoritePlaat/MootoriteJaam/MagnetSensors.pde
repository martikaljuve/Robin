//#define MAGNET_DEBUG

TimedAction angleAction = TimedAction(5, checkAngles);

#ifdef MAGNET_DEBUG
TimedAction debugAction = TimedAction(500, debugAngles);
#endif

void magnet_sensors_setup() {

}

void magnet_sensors_loop(){
	angleAction.check();

#ifdef MAGNET_DEBUG
	debugAction.check();
#endif
}

void checkAngles() {
	magnetLeft.update();
	magnetRight.update();
	magnetBack.update();
}

#ifdef MAGNET_DEBUG
void debugAngles() {
	Serial.print(magnetLeft.position);
	Serial.print(", ");
	Serial.print(magnetRight.position);
	Serial.print(", ");
	Serial.println(magnetBack.position);
}
#endif