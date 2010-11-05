struct CommandData {
	char command;
	int first;
	byte second;
	int third;
};

union CommandUnion {
	byte bytes[6];
	struct CommandData command;
};