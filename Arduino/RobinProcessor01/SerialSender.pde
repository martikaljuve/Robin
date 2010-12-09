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
	first |= (getTripSensorStatus() << 0);
	first |= (getLeftIrStatus() << 1);
	first |= (getRightIrStatus() << 2);
	first |= (getPower() << 3);

	byte second = getIrChannel();

	sensorData.command = (byte)'D';
	sensorData.first = first;
	sensorData.second = second;
	sensorData.x = getGlobalX();
	sensorData.y = getGlobalY();
	sensorData.direction = getGlobalDirection();
	sensorData.servoDirection = getServoDirection();
	sensorData.carriageReturn = '\r';
	sensorData.lineFeed = '\n';

	sensorUnion.data = sensorData;
	sensorUnion.data.checksum = calculateChecksum(sensorUnion);

	Serial.write(sensorUnion.bytes, 14);
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

int getGlobalX() {
	return globalX; // X-coordinate
}

int getGlobalY() {
	return globalY; // Y-coordinate
}

int getGlobalDirection() {
	return globalDirection; // gyro direction
}

int getServoDirection() {
	return getServoAngle(); // servoDirection;
}

//int swap(int data) {
	return (int)((data & 255) << 8) + (data >> 8);
//}

byte calculateChecksum(SensorUnion sensorUnion) {
	byte checksum = 0;

	for (int i = 0; i < 11; i++)
		checksum ^= sensorUnion.bytes[i];

	return checksum;
}
