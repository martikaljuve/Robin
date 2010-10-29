#include <TimedAction.h>
#include <MLX90316.h>
#include <PID_Beta6.h>

#include "ArduinoPins.h"

void setup() {
	ledOn();

	motors_setup();
	magnet_sensors_setup();
	pids_setup();
}

void loop() {
	motors_loop();
	magnet_sensors_loop();
	pids_loop();
}

// Light the LED
void ledOn() {
	pinMode(LED, OUTPUT);
	digitalWrite(LED, HIGH);
}
