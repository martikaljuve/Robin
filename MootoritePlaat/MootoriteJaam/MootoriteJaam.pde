#include <TimedAction.h>
#include <MLX90316.h>

#include "ArduinoPins.h"
#include "Definitions.h"

void setup() {
	ledOn();

	//Serial.begin(57600);

	// SETUP
	motors_setup();
	magnet_sensors_setup();
	pids_setup();
	wireReceiverSetup();
	// END SETUP

	//pid_debug_setup();
	
	// HACK: Testing is easier with a delay.
	//delay(4000);

	//wheels.moveAndTurn(0, 255, 0);
	//pidBack.setSetpoint(200);
	//pidLeft.setSetpoint(200);
}

unsigned long elapsedTime;

void loop() {
	// LOOP
	motors_loop();
	magnet_sensors_loop();
	pids_loop();
	wireReceiverLoop();
	// END LOOP

	//pid_debug_loop();
}

// Light the LED
void ledOn() {
	pinMode(LED, OUTPUT);
	digitalWrite(LED, HIGH);
}
