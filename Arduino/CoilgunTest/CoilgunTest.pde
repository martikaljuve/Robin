#define KICK 6
#define TRIP_SENSOR 7
#define DONE 13
#define CHARGE 12

void setup() {
	pinMode(DONE, INPUT);
	pinMode(TRIP_SENSOR, INPUT);
	pinMode(KICK, OUTPUT);
	pinMode(CHARGE, OUTPUT);
	
	delay(3000);
	digitalWrite(CHARGE, HIGH);
	delay(3000);
}

void loop() {
	int tripped = digitalRead(TRIP_SENSOR);
	if (tripped == HIGH) {
		digitalWrite(KICK, HIGH);
		delay(10);
		digitalWrite(KICK, LOW);
	}
}