#include <Wire.h>

#include "ArduinoPins.h"
#include "CommandData.h"

union CommandUnion cmdUnion2;

void wireSenderSetup() {
	Wire.onRequest(dataRequested);
}

void wireSenderLoop() {

}

void dataRequested() {
	cmdUnion2.command.command = 'P';
	cmdUnion2.command.first = wheels.worldCurrentX;
	cmdUnion2.command.second = wheels.worldCurrentY;
	cmdUnion2.command.third = gyro.getCurrentAngle();

	Wire.send(cmdUnion2.bytes, 7);
}

