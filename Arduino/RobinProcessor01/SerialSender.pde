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
	first |= (getTripSensorStatus() << 0);
	first |= (getLeftIrStatus() << 1);
	first |= (getRightIrStatus() << 2);

	Serial.write((byte)'D');
	Serial.write(first);
	SerialUtil.writeInt(getGyroDirection());
	SerialUtil.writeInt(getServoDirection());
	Serial.println();
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