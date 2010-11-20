#include <TimedAction.h>

TimedAction powerAction = TimedAction(100, powerCheck);

void powerSetup() {
	pinMode(POWER_PIN, INPUT);
}

void powerLoop() {
	powerAction.check();
}

void powerCheck() {
	POWER = digitalRead(POWER_PIN);
}

bool getPower() {
	return (POWER == HIGH);
}