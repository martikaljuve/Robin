#include <MLX90316.h>

class MagnetSensor {
	long anglePrev;
	long angleDiff;
	unsigned long timePrev;
	MLX90316 sensor;
public:
	float speed;

	MagnetSensor(int slaveSelect, int sck, int miso);
	
	void check();
	float calculateSpeed(long currentTime);
	
private:
	void add(int angle);
};
