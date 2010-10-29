#include <TimedAction.h>
#include <MLX90316.h>
#include <PID_Beta6.h>

#include "ArduinoPins.h"
#include "Definitions.h"

void setup() {
	ledOn();

	Serial.begin(9600);

	// SETUP
	motors_setup();
	magnet_sensors_setup();
	pids_setup();
	pid_debug_setup();
	// END SETUP

	//delay(4000);
	
	//wheels.moveAndTurnPid(270, 150, 0);

	//pidRight.setSetpoint(100);
	//pidLeft.setSetpoint(100);
}

unsigned long elapsedTime;

void loop() {
	// LOOP
	motors_loop();
	magnet_sensors_loop();
	pids_loop();
	pid_debug_loop();
	// END LOOP
}

// Light the LED
void ledOn() {
	pinMode(LED, OUTPUT);
	digitalWrite(LED, HIGH);
}
