#include "pins.h"
#include <Stdint.h>
#include <MLX90316.h>

MLX90316 leftMagnet = MLX90316();

//#include <SPI.h>

void setup() {
	pinMode(SS_GYRO, OUTPUT);
	pinMode(SS_LEFT, OUTPUT);
	//pinMode(SS_RIGHT, OUTPUT);
	//pinMode(SS_BACK, OUTPUT);
	
	pinMode(SS_GYRO, HIGH);

	Serial.begin(57600);
	leftMagnet.attach(SS_LEFT, SCK, MISO);

	//SPI.setDataMode(1);
	//SPI.begin();
}

void loop() {
	int result = leftMagnet.readAngle();
	
	Serial.print(result);
	Serial.print(",\t");
	Serial.println(result, BIN);
}