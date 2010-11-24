#pragma once

#include <MLX90316.h>

class MagnetSensor {
	int angleInitial;
	int anglePrevious;
	long positionTotal;
	long positionPrevious;
	MLX90316 sensor;
	
public:

	MagnetSensor();
	MagnetSensor(int slaveSelect, int sck, int miso);
	
	void update();

	long getPositionTotal();
	int getCurrentDelta();
	void resetCurrentDelta();

private:
	void calculateNewPosition(int angle);
};

