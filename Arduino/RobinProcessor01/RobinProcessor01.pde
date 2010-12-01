#include "ArduinoPins.h"
#include "CMotorBoard.h"

static bool POWER = false;
int globalX, globalY, globalDirection;

void setup(){
	Serial.begin(57600);
	//Serial.begin(9600);

	pinMode(TRIP_SENSOR, INPUT);

	//stateMachineSetup();
	powerSetup();
	serialReceiverSetup();
	serialSenderSetup();
	wireSenderSetup();
	wireReceiverSetup();
	coilgunSetup();
	beaconFinderSetup();
	ledsSetup();
}

void loop(){

	//stateMachineLoop();
	powerLoop();
	serialReceiverLoop();
	serialSenderLoop();
	wireSenderLoop();
	wireReceiverLoop();
	coilgunLoop();
	beaconFinderLoop();

	//delay(100);
}
