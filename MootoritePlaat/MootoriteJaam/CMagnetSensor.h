#pragma once

#include <MLX90316.h>

class MagnetSensor {
	long knownAngle;
	int angleInitial;
	int anglePrevious;

	long positionTotal;
	long positionPrevious;
	MLX90316 sensor;
	
public:
	int currentAngle;

	MagnetSensor(int slaveSelect, int sck, int miso);
	
	void update();

	void reset();
	long getPositionTotal();
	int getCurrentDelta();
	void resetCurrentDelta();

private:
	void calculateNewPosition(int angle);
};

