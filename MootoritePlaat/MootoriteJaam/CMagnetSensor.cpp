#include <WProgram.h>
#include "CMagnetSensor.h"

MagnetSensor::MagnetSensor() { }

MagnetSensor::MagnetSensor(int slaveSelect, int sck, int miso) {
	pinMode(slaveSelect, OUTPUT);
	digitalWrite(slaveSelect, LOW);

	sensor = MLX90316();
	sensor.attach(slaveSelect, sck, miso);
}

void MagnetSensor::update() {
	int result = sensor.readAngle();
	calculateNewPosition(result);
}

void MagnetSensor::calculateNewPosition(int angle) {
	if (angle < 0) // Angle should be between 0..3600, otherwise an error occurred
		return;

	int delta = angle - anglePrevious;

	int angleDiff;
	if (delta < -1800)
		angleDiff += 3600 + delta;
	else if (delta > 1800)
		angleDiff += delta - 3600;
	else
		angleDiff += delta;

	anglePrevious = angle;
	position += angleDiff;
}
