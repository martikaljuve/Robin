#include "PidDebugSerial.h"

unsigned long serialTime;

void pid_debug_setup() {
	Serial.begin(9600);
}

void pid_debug_loop() {
	if (millis() > serialTime) {
		SerialReceive(pidRight);
		SerialSend(pidRight);
		//SerialReceive(pidLeft);
		//SerialSend(pidLeft);
		serialTime += 500;
	}
}