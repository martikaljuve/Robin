void setup(){
	Serial.begin(9600);

	stateMachineSetup();
	gyroSetup();
	sonarSetup();
	sharpIrSetup();

	delay(100);
}

void loop(){

	stateMachineLoop();
	gyroLoop();
	sonarLoop();
	sharpIrLoop();
		
	//delay(100);
}
