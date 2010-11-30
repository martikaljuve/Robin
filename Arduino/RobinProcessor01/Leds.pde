bool getBit(byte data, byte n) {
	const byte mask = 1 << n-1;
	return (data & mask);
}

void ledsSetup() {
	pinMode(RED_PIN, OUTPUT);
	pinMode(GREEN_PIN, OUTPUT);
	pinMode(BLUE_PIN, OUTPUT);
}

void setLedRed(bool enabled) {
	digitalWrite(RED_PIN, enabled);
}

void setLedGreen(bool enabled) {
	digitalWrite(GREEN_PIN, enabled);
}

void setLedBlue(bool enabled) {
	digitalWrite(BLUE_PIN, enabled);
}
