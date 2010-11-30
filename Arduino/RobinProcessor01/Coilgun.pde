
bool coilgunShouldCharge = true;
bool coilgunCharging;
long coilgunChargingEnd;
const int CHARGE_TIME = 3000;

void coilgunSetup() {
	pinMode(DONE_PIN, INPUT);
	pinMode(KICK_PIN, OUTPUT);
	pinMode(CHARGE_PIN, OUTPUT);

	if (digitalRead(DONE_PIN) == LOW)
		coilgunShouldCharge = false;
}

void coilgunLoop() {
	if (coilgunCharging && coilgunChargingEnd < millis()) {
		digitalWrite(CHARGE_PIN, LOW);
		coilgunCharging = false;
	}

	if (coilgunShouldCharge) {
		digitalWrite(CHARGE_PIN, HIGH);
		coilgunCharging = true;
		coilgunChargingEnd = millis() + CHARGE_TIME;
		coilgunShouldCharge = false;
	}
}

void fireCoilgun(byte power) {
	power = constrain(power, 0, 100);
	digitalWrite(KICK_PIN, HIGH);
	delayMicroseconds(map(power, 0, 100, 0, 5000));
	digitalWrite(KICK_PIN, LOW);

	coilgunShouldCharge = true;
}
