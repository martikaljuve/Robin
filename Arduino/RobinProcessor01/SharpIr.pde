#define sharpIrPin 0	// Sharp IR sensor

void SharpIrSetup() {
	
}

void SharpIrLoop() {
	int distance = analogRead(sharpIrPin);
	Serial.print("I1 ");
	Serial.println(distance, DEC);
}