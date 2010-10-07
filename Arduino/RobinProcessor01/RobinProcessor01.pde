void setup(){
	Serial.begin(9600);

	gyroSetup();
	//sonarSetup();
	//sharpIrSetup();

	delay(100);
}

void loop(){

	gyroLoop();
	//sonarLoop();
	//sharpIrLoop();
		
	//delay(100);
}
