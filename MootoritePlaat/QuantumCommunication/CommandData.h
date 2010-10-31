struct CommandData {
	char command;
	int first;
	byte second;
	byte third;
};

union CommandUnion {
	byte bytes[5];
	struct CommandData command;
};