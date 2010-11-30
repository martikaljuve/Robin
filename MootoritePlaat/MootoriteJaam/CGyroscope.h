#pragma once

#include <WProgram.h>

#define ADCC_RATE 0b10010100 // conversion command for angular rate
#define ADCC_TEMP 0b10011100 // conversion command for temperature
#define ADCR 0b10000000 // result reading command

class Gyroscope {
	bool isCalibrating;
	bool enabled;
	double currentAngle;
	int pinSS;
	int pinSCK;
	int pinMOSI;
	int pinMISO;
	int calibrationCount;
	unsigned long calibrationAdcSum;
	unsigned int calibrationIndex;
	unsigned int calibrationAdcMin;
	unsigned int calibrationAdcMax;
	double calibrationAdcAvg;
	double calibrationRateAvg;

public:
	Gyroscope(int slaveSelect, int sck, int mosi, int miso);

	void enable();
	void update(unsigned long deltaInMilliseconds);
	double getCurrentAngle();
	void resetAngle(int angle);
	void calibrate(int count);

private:
	bool tryReadAdc(unsigned int &result);
	double adcToAngularRate(unsigned int adcValue);
	byte spiTransfer(byte tx);
};
