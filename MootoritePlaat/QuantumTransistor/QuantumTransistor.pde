void setup() {
	pinMode(10, OUTPUT);
	pinMode(2, OUTPUT);
}

void loop() {
	digitalWrite(10, HIGH);
	digitalWrite(2, HIGH);

	delay(5000);

	digitalWrite(10, LOW);
	digitalWrite(2, LOW);

	delay(5000);
}