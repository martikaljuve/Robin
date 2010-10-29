#pragma once

#include <MLX90316.h>

class MagnetSensor {
	long anglePrev;
	long angleDiff;
	unsigned long timePrev;
	MLX90316 sensor;

	static const int NUM_READINGS = 12;
	int readings[NUM_READINGS];
	int readingIndex;
	int readingTotal;
public:
	float speed;
	int average;

	MagnetSensor();
	MagnetSensor(int slaveSelect, int sck, int miso);
	
	void takeMeasurement();
	float calculateSpeed(long currentTime);
private:
	void averageSpeed();
	void add(int angle);
};
