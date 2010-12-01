struct CommandData {
	char command;
	int first;
	int second;
	int third;
};

union CommandUnion {
	byte bytes[7];
	struct CommandData command;
};

struct SensorData {
	char command;
	byte first;
	byte second;
	int x;
	int y;
	int direction;
	int servoDirection;
	byte checksum;
	char carriageReturn;
	char lineFeed;
} sensorData;

union SensorUnion {
	struct SensorData data;
	byte bytes[14];
} sensorUnion;
