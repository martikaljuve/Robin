long nextSend;
const int sendInterval = 500;

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
	first |= (getPower() << 3);

	byte second = getIrChannel();

	Serial.write((byte)'D');
	Serial.write(first);
	Serial.write(second);
	SerialUtil.writeInt(getGyroDirection());
	SerialUtil.writeInt(getServoDirection());
	Serial.println();
}

bool getTripSensorStatus() {
	return digitalRead(TRIP_SENSOR) == HIGH; // tripSensor;
}

bool getLeftIrStatus() {
	return isLeftIr(); // leftIrInView;
}

bool getRightIrStatus() {
	return isRightIr(); // rightIrInView;
}

int getGyroDirection() {
	return (int)'A'; // gyroDirection;
}

int getServoDirection() {
	//return -60;
	return getServoAngle(); // servoDirection;
}