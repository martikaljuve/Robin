long nextSend;
const int sendInterval = 2000;

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

	Serial.write((byte)'D');
	Serial.write(first);
	Serial.write(getBytesFromInt(getGyroDirection()), 2);
	Serial.write(getBytesFromInt(getServoDirection()), 2);
	Serial.write('\n');
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

int getGyroDirection() {
	return (int)'A'; // gyroDirection;
}

int getServoDirection() {
	return (int)'B'; // servoDirection;
}