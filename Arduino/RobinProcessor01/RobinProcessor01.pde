#include "CMotorBoard.h"

void setup(){
	Serial.begin(57600);
	//Serial.begin(9600);

	//stateMachineSetup();
	//gyroSetup();
	serialReceiverSetup();
	serialSenderSetup();
	wireSenderSetup();
}

void loop(){

	//stateMachineLoop();
	//gyroLoop();
	serialReceiverLoop();
	serialSenderLoop();
	wireSenderLoop();

	//delay(100);
}