#include <Wire.h>

#include "CommandData.h"

CommandData cmdData;
union CommandUnion cmdUnion;

void communication_setup() {
	Wire.begin();
}

void communication_loop() {
	loopTemp();
}

void loopTemp() {
	sendCommand('D');
	delay(1000);
	sendCommand('d');
	delay(1000);
}

void sendCommand(struct CommandData &cmd, int byteCount) {
	Wire.beginTransmission(1);
	cmdUnion.command = cmd;
	for (int i = 0; i < byteCount; i++) {
		Wire.send(cmdUnion.bytes[i]);
	}
	Wire.endTransmission();
}

void sendCommand(char command) {
	sendCommand(command, 0, 0, 0, 1);
}

void sendCommand(char command, int first) {
	sendCommand(command, first, 0, 0, 3);
}

void sendCommand(char command, int first, byte second) {
	sendCommand(command, first, second, 0, 4);
}

void sendCommand(char command, int first, byte second, byte third) {
	sendCommand(command, first, second, third, 5);
}

void sendCommand(char command, int first, byte second, byte third, int byteCount) {
	cmdData.command = command;
	cmdData.first = first;
	cmdData.second = second;
	cmdData.third = third;
	sendCommand(cmdData, byteCount);
}