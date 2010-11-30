#pragma once

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

