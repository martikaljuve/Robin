#include "CMotorBoard.h"

void setup(){
	Serial.begin(57600);
	//Serial.begin(9600);

	//stateMachineSetup();
	//gyroSetup();
	//sonarSetup();
	//sharpIrSetup();
	serialReceiverSetup();
	serialSenderSetup();
	wireSenderSetup();
}

void loop(){

	//stateMachineLoop();
	//gyroLoop();
	//sonarLoop();
	//sharpIrLoop();
	serialReceiverLoop();
	serialSenderLoop();
	wireSenderLoop();

	//delay(100);
}