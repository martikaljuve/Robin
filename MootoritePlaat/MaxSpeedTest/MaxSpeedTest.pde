#include <TimedAction.h>
#include <MLX90316.h>
#include "pins.h"
#include "AngleUtil.h"

MLX90316 leftMagnet = MLX90316();
MLX90316 rightMagnet = MLX90316();
MLX90316 backMagnet = MLX90316();

TimedAction addAction = TimedAction(2, checkAngles);
TimedAction calcAction = TimedAction(10000, calculateSpeeds);
TimedAction debugAction = TimedAction(10000, debug);

void setup() {
	pinMode(LEFT_DIR, OUTPUT);
	pinMode(RIGHT_DIR, OUTPUT);
	pinMode(BACK_DIR, OUTPUT);
	
	pinMode(LEFT_PWM, OUTPUT);
	pinMode(RIGHT_PWM, OUTPUT);
	pinMode(BACK_PWM, OUTPUT);
	
	leftMagnet.attach(SS_LEFT, SCK, MISO);
	rightMagnet.attach(SS_RIGHT, SCK, MISO);
	backMagnet.attach(SS_BACK, SCK, MISO);
	
	pinMode(MODE, OUTPUT);
	digitalWrite(MODE, HIGH);
	
	analogWrite(LEFT_PWM, 255);
	analogWrite(RIGHT_PWM, 255);
	analogWrite(BACK_PWM, 255);
	
	Serial.begin(57600);
}

void loop() {
	addAction.check();
	calcAction.check();
	debugAction.check();
}

AngleUtil leftAngle = AngleUtil();
AngleUtil rightAngle = AngleUtil();
AngleUtil backAngle = AngleUtil();

void checkAngles() {
	int angle = leftMagnet.readAngle();
	leftAngle.add(angle);
	
	angle = rightMagnet.readAngle();
	rightAngle.add(angle);
	
	angle = backMagnet.readAngle();
	backAngle.add(angle);
}

void calculateSpeeds() {
	leftAngle.calculateSpeed(millis());
	rightAngle.calculateSpeed(millis());
	backAngle.calculateSpeed(millis());
}

void debug() {
	Serial.print(leftAngle.speed);
	Serial.print(",\t");	
	Serial.print(rightAngle.speed);
	Serial.print(",\t");
	Serial.println(backAngle.speed);
}