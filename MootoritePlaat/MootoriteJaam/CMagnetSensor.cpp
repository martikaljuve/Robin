#include <WProgram.h>
#include "CMagnetSensor.h"

MagnetSensor::MagnetSensor() { }

MagnetSensor::MagnetSensor(int slaveSelect, int sck, int miso) {
	pinMode(slaveSelect, OUTPUT);
	digitalWrite(slaveSelect, LOW);

	sensor = MLX90316();
	sensor.attach(slaveSelect, sck, miso);

	for (int i = 0; i < NUM_READINGS; i++)
		readings[i] = 0;
}

void MagnetSensor::takeMeasurement() {
	int result = sensor.readAngle();
	
	add(result);
}

float MagnetSensor::calculateSpeed(long currentTime) {
	unsigned int timeDiff = currentTime - timePrev;
	
	if (timeDiff > 0) {
		speed = (-angleDiff / (float)timeDiff) * 16.666667; // 60000ms / 360.0 (3600) degrees
	
		timePrev = currentTime;
		angleDiff = 0;

		averageSpeed();
	}

	return speed;
}

void MagnetSensor::averageSpeed() {
	readingTotal = readingTotal - readings[readingIndex];
	readings[readingIndex] = speed;
	readingTotal = readingTotal + readings[readingIndex];
	readingIndex++;

	if (readingIndex >= NUM_READINGS)
		readingIndex = 0;

	average = readingTotal / NUM_READINGS;
}

void MagnetSensor::add(int angle) {
	if (angle < 0) return;
	
	int delta = angle - anglePrev;
	if (delta < -1800) {
		angleDiff += 3600 + delta;
	}
	else if (delta > 1800) {
		angleDiff += delta - 3600;
	}
	else {
		angleDiff += delta;
	}

	anglePrev = angle;
}
