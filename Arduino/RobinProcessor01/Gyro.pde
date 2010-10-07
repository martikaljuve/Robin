#include <SPI.h>

const int SS = 10;

int rate;
int temperature;

void gyroSetup() {
	SPI.begin();
	digitalWrite(SS, HIGH);
}

void gyroLoop() {
	rate = getAngularRate();
	temperature = getTemperature();
	Serial.print("AR ");
	Serial.println(adcToAngularRate(rate), DEC);
	Serial.print("AT ");
	Serial.println(adcToTemperature(temperature), DEC);
}

// get temperature adc in millivolts
unsigned int getTemperature()
{	
	byte dataH;
	byte dataL;

	digitalWrite(SS, LOW);
	SPI.transfer(0b10011100); // ADCC for temperature channel
	digitalWrite(SS, HIGH);
	delay(250);
	digitalWrite(SS, LOW);
	SPI.transfer(0b10000000);  // ADCR (ADC reading) Instruction
	dataH = SPI.transfer(0x00);  // get the sensor response high byte
	dataL = SPI.transfer(0x00);  // get the sensor response low byte
	digitalWrite(SS, HIGH);

	// The sensor response is two bytes length but the answer length
	// is 11 bits only and saved using the lower 4 bits from the high 
	// (first) byte and the upper 7 bits from the lower (second) byte
	// (dataH & 0b00001111) gets the lower 4 bits value from the 
	// high byte.
	// (dataL>>1) gets the upper 7 bits value from the lower byte.
	// The temperature is the resulting word of the high and low bytes
	unsigned int result = word((dataH & 0b00001111), (dataL>>1));
	return result;
}

// get angular rate adc in millivolts
unsigned int getAngularRate()
{
	byte dataH;
	byte dataL;

	digitalWrite(SS, LOW);
	SPI.transfer(0b10010100);  // ADCC for angular rate channel
	digitalWrite(SS,HIGH);
	delayMicroseconds(250);
	digitalWrite(SS,LOW);
	SPI.transfer(0b10000000);  // send SPI ADCR instruction
	dataH = SPI.transfer(0x00);  // get the sensor response high byte
	dataL = SPI.transfer(0x00);  // get the sensor response low byte
	digitalWrite(SS,HIGH);

	// The sensor response is two bytes length but the answer length
	// is 11 bits only and saved using the lower 4 bits from the high 
	// (first) byte and the upper 7 bits from the lower (second) byte
	// (dataH & 0b00001111) gets the lower 4 bits value from the 
	// high byte.
	// (dataL>>1) gets the upper 7 bits value from the lower byte.
	// The angular rate is the resulting word of the high and low bytes
	unsigned int result = word((dataH & 0b00001111), (dataL>>1));
	return result;
}

// converts the adc reading to angles per second
int adcToAngularRate(unsigned int adcValue)
{
	int vOutAngularRate = (adcValue * 25/12)+400;  // in mV (millivolts)

	// from the data sheet, N2 version is 6,67	
	// E2 is 13,33 and R2 is 26,67 mV/deg
	// change accordingly.
	return (vOutAngularRate - 2500)/26.67; 
}

// converts the adc reading to centigrades
int adcToTemperature(unsigned int adcValue)
{
	int vOutTemperature = (adcValue * 25/16)+300;  // in mV (millivolts)
	return 25 + ((vOutTemperature - 2500)/10);  // from the data sheet factor is 10mV/K
}