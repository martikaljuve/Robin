
bool coilgunShouldCharge = true;
bool coilgunCharging;
long coilgunChargingStart;
long coilgunChargingEnd;
const int CHARGE_START_DELAY = 200;
const int CHARGE_TIME = 1500;

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

	if (coilgunShouldCharge) {
  		coilgunChargingStart = millis() + CHARGE_START_DELAY;
  		coilgunShouldCharge = false;
  	}
  
   	if (!coilgunCharging && millis() > coilgunChargingStart) {
		digitalWrite(CHARGE_PIN, HIGH);
		coilgunCharging = true;
		coilgunChargingEnd = millis() + CHARGE_TIME;
	}
}

void fireCoilgun(byte power) {
	power = constrain(power, 0, 100);
	digitalWrite(KICK_PIN, HIGH);
	delayMicroseconds(map(power, 0, 100, 0, 5000));
	digitalWrite(KICK_PIN, LOW);

	coilgunShouldCharge = true;
}
