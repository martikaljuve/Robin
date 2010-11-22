#pragma once

#include <MLX90316.h>

class MagnetSensor {
	int anglePrevious;
	MLX90316 sensor;
	
public:
	long position;
	int delta;

	MagnetSensor();
	MagnetSensor(int slaveSelect, int sck, int miso);
	
	void update();

private:
	void calculateNewPosition(int angle);
};
