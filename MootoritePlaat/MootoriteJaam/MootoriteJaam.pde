#include <TimedAction.h>
#include <MLX90316.h>

#include "ArduinoPins.h"
#include "Definitions.h"

#define DEBUG
bool shouldInitialize = true;

void setup() {
	ledOn();

#ifdef DEBUG
	Serial.begin(57600);
#endif

	// SETUP
	motors_setup();
	magnet_sensors_setup();
	gyro_setup();
	pids_setup();
	wireReceiverSetup();
	wireSenderSetup();
	// END SETUP

	//pid_debug_setup();
	
	// HACK: Testing is easier with a delay.
	//delay(4000);

	//wheels.moveAndTurnDistance(3150, 400, 0);
	//pidBack.setSetpoint(200);
	//pidLeft.setSetpoint(200);
}

void loop() {
	if (!shouldInitialize) {
		// LOOP
		motors_loop();
		magnet_sensors_loop();
		gyro_loop();
		pids_loop();
		wireReceiverLoop();
		wireSenderLoop();
		// END LOOP
	}
	else if (shouldInitialize && millis() > 300) {
		shouldInitialize = false;
		magnetLeft.reset();
		magnetRight.reset();
		magnetBack.reset();
		gyro.enable();
		gyro.calibrate(1024);
		gyro.resetAngle(0);
		wheels.resetGlobalPosition(0);
	}

	//pid_debug_loop();
}

// Light the LED
void ledOn() {
	pinMode(LED, OUTPUT);
	digitalWrite(LED, HIGH);
}

