
void setup(){
	#ifdef SERVO_DEBUG
	Serial.begin(57600);
	#endif
	beaconFinderSetup();
}

void loop(){
	beaconFinderLoop();
}
