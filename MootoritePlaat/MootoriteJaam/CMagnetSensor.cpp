#include <WProgram.h>
#include "CMagnetSensor.h"

MagnetSensor::MagnetSensor(int slaveSelect, int sck, int miso) {
	pinMode(slaveSelect, OUTPUT);
	digitalWrite(slaveSelect, LOW);

	sensor = MLX90316();
	sensor.attach(slaveSelect, sck, miso);
}

void MagnetSensor::check() {
	int result = sensor.readAngle();
	
	add(result);
}

float MagnetSensor::calculateSpeed(long currentTime) {
	unsigned int timeDiff = currentTime - timePrev;
	
	if (timeDiff <= 0) return speed;
	if (angleDiff <= 0) return speed;
	
	speed = (angleDiff / (float)timeDiff) * 16.666667; // 60000ms / 360.0 (3600) degrees
	
	timePrev = currentTime;
	angleDiff = 0;

	return speed;
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
