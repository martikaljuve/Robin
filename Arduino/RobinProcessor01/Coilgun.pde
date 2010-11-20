bool coilgunFired = true;
bool coilgunCharging;
long coilgunChargingEnd;
const int CHARGE_TIME = 3000;

void coilgunSetup() {
	pinMode(DONE_PIN, INPUT);
	pinMode(KICK_PIN, OUTPUT);
	pinMode(CHARGE_PIN, OUTPUT);
}

void coilgunLoop() {
	if (coilgunCharging && coilgunChargingEnd < millis()) {
		digitalWrite(CHARGE_PIN, LOW);
		coilgunCharging = false;
	}

	if (coilgunFired) {
		digitalWrite(CHARGE_PIN, HIGH);
		coilgunCharging = true;
		coilgunChargingEnd = millis() + CHARGE_TIME;
		coilgunFired = false;
	}
}

void fireCoilgun(byte power) {
	power = constrain(power, 0, 100);
	digitalWrite(KICK_PIN, HIGH);
	delay(map(power, 0, 100, 0, 10));
	digitalWrite(KICK_PIN, LOW);

	coilgunFired = true;
}