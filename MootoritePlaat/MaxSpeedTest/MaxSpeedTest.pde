#include "pins.h"

void setup() {
	pinMode(LEFT_DIR, OUTPUT);
	pinMode(RIGHT_DIR, OUTPUT);
	pinMode(BACK_DIR, OUTPUT);
	
	pinMode(LEFT_PWM, OUTPUT);
	pinMode(RIGHT_PWM, OUTPUT);
	pinMode(BACK_PWM, OUTPUT);
	
	pinMode(MODE, OUTPUT);
	digitalWrite(MODE, HIGH);
	
	digitalWrite(LEFT_DIR, HIGH);

	analogWrite(LEFT_PWM, 255);
	analogWrite(RIGHT_PWM, 255);
	analogWrite(BACK_PWM, 0);
	
}

void loop() {
	delay(10000);
	analogWrite(LEFT_PWM, 125);
	analogWrite(RIGHT_PWM, 125);
	analogWrite(BACK_PWM, 0);
}
