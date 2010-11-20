#include "CMotorBoard.h"

#define TRIP_SENSOR 7
static bool POWER = false;

void setup(){
	Serial.begin(57600);
	//Serial.begin(9600);

	pinMode(TRIP_SENSOR, INPUT);

	//stateMachineSetup();
	//gyroSetup();
	serialReceiverSetup();
	serialSenderSetup();
	wireSenderSetup();
	coilgunSetup();
	beaconFinderSetup();
}

void loop(){

	//stateMachineLoop();
	//gyroLoop();
	POWER ? serialReceiverLoop() : Serial.flush();
	serialSenderLoop();
	wireSenderLoop();
	coilgunLoop();
	beaconFinderLoop();

	//delay(100);
}