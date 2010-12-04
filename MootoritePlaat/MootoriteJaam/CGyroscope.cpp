#include "CGyroscope.h"

Gyroscope::Gyroscope(int slaveSelect, int sck, int mosi, int miso) {
	pinSS = slaveSelect;
	pinSCK = sck;
	pinMOSI = mosi;
	pinMISO = miso;

	pinMode(pinMISO, INPUT);
	pinMode(pinMOSI, OUTPUT);
	pinMode(pinSCK, OUTPUT);
	pinMode(pinSS, OUTPUT);

	digitalWrite(pinSS, HIGH);
}

void Gyroscope::calibrate(int count) {
	calibrationIndex = 0;
	calibrationCount = count;
	isCalibrating = true;
	calibrationAdcMin = 2048;
}

void Gyroscope::update(unsigned long deltaInMilliseconds) {
	if (!enabled)
		return;

	unsigned int adcCode;
	if (!tryReadAdc(adcCode))
		return;

	if (isCalibrating) {
		calibrationAdcSum += adcCode;
		calibrationIndex++;
		calibrationAdcMin = min(calibrationAdcMin, adcCode);
		calibrationAdcMax = max(calibrationAdcMax, adcCode);

		if (calibrationIndex >= calibrationCount) {
			isCalibrating = false;
			calibrationAdcAvg = calibrationAdcSum / calibrationIndex;
			calibrationRateAvg = adcToAngularRate(calibrationAdcAvg);
			/*Serial.print("Count: ");
			Serial.print(calibrationIndex);
			Serial.print(", Minimum: ");
			Serial.print(calibrationAdcMin);
			Serial.print(", Maximum: ");
			Serial.print(calibrationAdcMax);
			Serial.print(", Calibration sum: ");
			Serial.print(calibrationAdcSum);
			Serial.print(", Average ADC: ");
			Serial.print(calibrationAdcAvg);
			Serial.print(", Average Rate: ");
			Serial.println(calibrationRateAvg);*/
		}

		return;
	}

	double angularRate = adcToAngularRate(adcCode) - calibrationRateAvg;

	if (adcCode < calibrationAdcMin - 5 || adcCode > calibrationAdcMax + 5) {
		currentAngle -= (angularRate * deltaInMilliseconds) / 1000.0;
	}
}

double Gyroscope::getCurrentAngle() {
	return currentAngle;
}

void Gyroscope::resetAngle(int angle) {
	currentAngle = angle;
}

void Gyroscope::enable() {
	enabled = true;
	
	digitalWrite(pinSS, LOW); // LOW - enable device	
	spiTransfer(ADCC_RATE); // put ADC to active mode if it wasn't
	digitalWrite(pinSS, HIGH);
}

bool Gyroscope::tryReadAdc(unsigned int &result) {
	digitalWrite(pinSS, LOW);
	spiTransfer(ADCC_RATE); // send control command (conversion start)	
	digitalWrite(pinSS, HIGH);

	delayMicroseconds(250);

	digitalWrite(pinSS, LOW);
	spiTransfer(ADCR); // send reading command
	byte dataHigh = spiTransfer(0x00);
	byte dataLow = spiTransfer(0x00);
	digitalWrite(pinSS, HIGH);

	if ((dataHigh & 0x80) == 0x80) { // 0x80 == 0b10000000, 15th bit
		//Serial.print("!");
		return false; // operation refused
	}
	if ((dataHigh & 0x20) != 0x20) { // 0x20 == 0b00100000, 13th bit (EOC)
		//Serial.print("?");
		return false; // conversion in progress
	}

	result = (word(0, dataHigh & 0b00001111) << 7) + (dataLow >> 1);

	return true;
}

double Gyroscope::adcToAngularRate(unsigned int adcValue) {
	double vOutAngularRate = (adcValue * 25/12) + 400;
	return (vOutAngularRate - 2500) / 6.67;
}

byte Gyroscope::spiTransfer(byte tx) {
	byte rxb = 0;

	for (int i = 0; i < 8; i++) {
		digitalWrite(pinSCK, LOW); // clock down		
		digitalWrite(pinMOSI, (tx & 0x80) ? HIGH : LOW); //write data MSB first

		tx = (tx << 1); //shift data right
		digitalWrite(pinSCK, HIGH); // clock up
		rxb = (rxb << 1); //shift receive left

		if (digitalRead(pinMISO))
			rxb |= 1; //read response MSB first
	}

	return rxb;
}

/**
	From the MLX90609 datasheet:

	ADEN bit - power management, 0: sleep, 1: allowed
	BUSY bit - 1: after reset, 0: after all initialization procedures. while 1, only refusal answers will be sent
	CHAN bit - 0 - angular rate channel, 1: temperature sensor channel
	EOC bit - end of conversion bit, 0: conversion in progress and cannot be restarted, 1: AD-conversion completed and can be restarted
	OPC bit - unknown operation code received

	Instructions:
	1 0 0 0 1 0 0 0 (0x88) - Status reading instruction (STATR)
	1 0 0 1 [CHAN] [ADEN] 0 0 (0x8?) - ADC control instruction (ADCC)
	1 0 0 0 0 0 0 0 (hex: 0x80) - ADC reading instruction (ADCR)

	Answers:
	1 [OPC] [EOC] . . [BUS] . . . . . . . . . . - Refusal
	0 . [EOC] . . . . . . . . . [CHN] [ADN] . . - Status reading
	0 . [EOC] . . . . . . . . . [CHN] [ADN] . . - ADC control
	0 . [EOC] . [A10] [A9] [A8] [A7] [A6] [A5] [A4] [A3] [A2] [A1] [A0] 0 - ADC reading

	Recommended sequence (step 1 and 4 can be skipped?):
	1. put ADC to active mode: send ADCC instruction - 0b10010100, read next byte, check if Most Significant Bit is 0
	2. send ADCC instruction - 0b10010100, read next byte, check if MSB is 0
	3. send ADCR instruction - 0b10000000, check MSB (15th bit) and 13th bit of result, if 13th bit is 1, the result is valid.
	4. put ADC to sleep mode: send ADCC - 0b10010000, read next byte, check if MSB is 0

	V_outAngularRate = 25/12 * ADCcode + 400
	V_outTemperature = 25/16 * ADCcode + 300
	ADCcode - 11-bit result of the conversion
*/
