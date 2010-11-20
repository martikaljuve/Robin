#include <Wire.h>

#include "ArduinoPins.h"
#include "CommandData.h"

CommandData command;
union CommandUnion cmdUnion;

void wireReceiverSetup() {
	setupTemp();

	Wire.begin(1);
	Wire.onReceive(dataReceived);
}

void wireReceiverLoop() {

}

void setupTemp() {
	pinMode(LED, OUTPUT);
}

void parseCommand(struct CommandData &cmd) {
	switch(cmd.command) {
		case 'S':
			wheels.stop();
			break;
		case 'M':
			wheels.move(cmd.first, cmd.second);
			break;
		case 'T':
			wheels.turn(cmd.first);
			break;
		case 'G':
			wheels.moveAndTurn(cmd.first, cmd.second, cmd.third);

			break;
		case 'D':
			dribbler.setSpeedWithDirection(255);
			break;
		case 'd':
			dribbler.stop();
			break;
		default:
			break;
	}
}

void dataReceived(int numBytes) {
	for(int i = 0; i < min(7, numBytes); i++) {
		byte tmp = Wire.available() ? Wire.receive() : 0;
		cmdUnion.bytes[i] = tmp;
	}
	
	parseCommand(cmdUnion.command);
}