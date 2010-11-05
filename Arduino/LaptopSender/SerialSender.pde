long nextSend;
const int sendInterval = 100;

void serialSenderSetup() {
	
}

void serialSenderLoop() {
	if (millis() > nextSend) {
		sendMessage();

		nextSend += sendInterval;
	}
}

void sendMessage() {
	byte first;
	first = first | getTripSensorStatus();
	first = first | (getLeftIrStatus() << 1);
	first = first | (getRightIrStatus() << 2);

	Serial.write(first);
	Serial.write();
}

bool getTripSensorStatus() {
	return false; // tripSensor;
}

bool getLeftIrStatus() {
	return false; // leftIrInView;
}

bool getRightIrStatus() {
	return false; // rightIrInView;
}