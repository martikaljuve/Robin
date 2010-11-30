#include <Wire.h>

#include "ArduinoPins.h"
#include "CommandData.h"

union CommandUnion cmdUnion;

void wireReceiverSetup() {
	Wire.begin(1);
	Wire.onReceive(dataReceived);
}

void wireReceiverLoop() {

}

void parseCommand(struct CommandData &cmd) {
	switch(cmd.command) {
		case 'S':
			wheels.stop();
			break;
		case 'M':
			wheels.moveDistance(cmd.first, cmd.second);
			break;
		case 'T':
			wheels.turnDistance(cmd.first);
			break;
		case 'G':
			wheels.moveAndTurnDistance(cmd.first, cmd.second, cmd.third);
			break;
		case 'D':
			dribbler.setSpeedWithDirection(cmd.first);
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

