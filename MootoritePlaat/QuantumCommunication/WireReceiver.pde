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
		case 'D':
			digitalWrite(LED, HIGH);
			break;
		case 'd':
			digitalWrite(LED, LOW);
			break;
	}
}

void dataReceived(int numBytes) {
	for(int i = 0; i < min(5, numBytes); i++) {
		byte tmp = Wire.available() ? Wire.receive() : 0;
		cmdUnion.bytes[i] = tmp;
	}
	
	parseCommand(cmdUnion.command);
}