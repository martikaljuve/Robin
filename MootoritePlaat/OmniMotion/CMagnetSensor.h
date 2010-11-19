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
	long speed;
	long average;

	MagnetSensor();
	MagnetSensor(int slaveSelect, int sck, int miso);
	
	void takeMeasurement();
	long calculateSpeed(long currentTime);
private:
	void averageSpeed();
	void add(int angle);
};
