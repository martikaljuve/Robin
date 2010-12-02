#include <TimedAction.h>

TimedAction powerAction = TimedAction(100, powerCheck);

bool powerPrevious;

void powerSetup() {
	pinMode(POWER_PIN, INPUT);
}

void powerLoop() {
	powerAction.check();
}

void powerCheck() {
	POWER = digitalRead(POWER_PIN);

	if (powerPrevious != POWER && POWER == false)
		MotorBoard::sendCommand('S');
	powerPrevious = POWER;
}

bool getPower() {
	return (POWER == HIGH);
}
