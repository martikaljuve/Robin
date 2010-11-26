#include <TimedAction.h>
#include <MLX90316.h>

#include "ArduinoPins.h"
#include "Definitions.h"

#define DEBUG
long milliseconds;
bool shouldInitialize = true;

void setup() {
	pinMode(SS_GYRO, OUTPUT); // Magnet sensors won't work unless this slave select pin is set as OUTPUT.
	ledOn();

#ifdef DEBUG
	Serial.begin(57600);
#endif

	// SETUP
	motors_setup();
	magnet_sensors_setup();
	pids_setup();
	wireReceiverSetup();
	// END SETUP

	//pid_debug_setup();
	
	// HACK: Testing is easier with a delay.
	//delay(4000);

	//wheels.moveAndTurnDistance(2700, 500, 0);
	//pidBack.setSetpoint(200);
	//pidLeft.setSetpoint(200);
}

unsigned long elapsedTime;

void loop() {
	milliseconds = millis();

	// LOOP
	motors_loop();
	magnet_sensors_loop();
	pids_loop();
	wireReceiverLoop();
	// END LOOP

	if (shouldInitialize && milliseconds > 200) {
		shouldInitialize = false;
		magnetLeft.reset();
		magnetRight.reset();
		magnetBack.reset();
		wheels.resetGlobalPosition();
	}

	//pid_debug_loop();
}

// Light the LED
void ledOn() {
	pinMode(LED, OUTPUT);
	digitalWrite(LED, HIGH);
}

