#define sharpIrPin 0	// Sharp IR sensor

void sharpIrSetup() {
	
}

void sharpIrLoop() {
	int distance = analogRead(sharpIrPin);
	Serial.print("I1 ");
	Serial.println(distance, DEC);
}