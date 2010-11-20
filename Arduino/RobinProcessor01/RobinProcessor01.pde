#include "ArduinoPins.h"
#include "CMotorBoard.h"

static bool POWER = false;

void setup(){
	Serial.begin(57600);
	//Serial.begin(9600);

	pinMode(TRIP_SENSOR, INPUT);

	//stateMachineSetup();
	//gyroSetup();
	powerSetup();
	serialReceiverSetup();
	serialSenderSetup();
	wireSenderSetup();
	coilgunSetup();
	beaconFinderSetup();
}

void loop(){

	//stateMachineLoop();
	//gyroLoop();
	powerLoop();
	POWER ? serialReceiverLoop() : Serial.flush();
	serialSenderLoop();
	wireSenderLoop();
	coilgunLoop();
	beaconFinderLoop();

	//delay(100);
}