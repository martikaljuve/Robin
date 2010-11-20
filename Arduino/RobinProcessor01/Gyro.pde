#include <SPI.h>

int rate;
int temperature;
unsigned long timeLast = 0;
unsigned long timeNext = 0;
const int SampleFirstSkipCount = 100;
const int SampleCount = 500;
const int SampleTotalCount = SampleCount + SampleFirstSkipCount;
int updateMilliseconds = 500;
int sampleIterator = 0;
long samplingMin = 0;
long samplingMax = 0;
float samplingAvg = 0;
int samplingAvgDeg = 0;

float gyroAngle = 0;
float samplingRate = 0.01;

void gyroSetup() {
	SPCR = // Configure SPI mode:
		(1<<SPE) |  // to enable SPI
		(1<<MSTR) | // to set Master SPI mode
		(1<<CPOL) | // SCK is high when idle
		(1<<CPHA) | // data is sampled on the trailing edge of the SCK
		(1<<SPR0);  // It sets SCK freq. in 8 times less than a system clock

	SPI.begin();

	digitalWrite(SS_PIN, HIGH);
	timeLast = millis();
	timeNext = timeLast + updateMilliseconds;
}

void gyroLoop() {
	rate = getAngularRate();
	temperature = getTemperature();
	unsigned long timeNow = millis();
	unsigned long timeDelta = timeNow - timeLast;

	if (sampleIterator <= SampleTotalCount) {
		if (sampleIterator < SampleFirstSkipCount) {
			sampleIterator++;
		}
		else if (sampleIterator == SampleFirstSkipCount) {
			sampleIterator++;
			samplingAvg = rate;
			samplingMin = rate;
			samplingMax = rate;
		}
		else if (sampleIterator < SampleTotalCount) {
			if (rate > samplingMax) samplingMax = rate;
			if (rate < samplingMin) samplingMin = rate;
			samplingAvg = ((samplingAvg * sampleIterator) + rate) / (sampleIterator+1);
			sampleIterator++;
		}
		else if (sampleIterator == SampleTotalCount) {
			sampleIterator++;
			samplingAvgDeg = adcToAngularRate(samplingAvg);
			Serial.print("MIN: ");
			Serial.print(adcToAngularRate(samplingMin));
			Serial.print(", MAX: ");
			Serial.print(adcToAngularRate(samplingMax));
			Serial.print(", AVG: ");
			Serial.println(samplingAvgDeg);
		}
	}
	else {
		unsigned int angularRate = adcToAngularRate(rate);
		gyroAngle = ((angularRate - samplingAvgDeg) * samplingRate) + gyroAngle;

		if(timeNow > timeNext) {
			timeNext = timeNow + updateMilliseconds;

			Serial.print("AR ");
			Serial.print(angularRate, DEC);

			Serial.print(", \tAN ");
			Serial.print(gyroAngle);

			Serial.print(", \tTP ");
			Serial.print(adcToTemperature(temperature), DEC);

			Serial.print(", \tMS ");
			Serial.println(timeDelta, DEC);
		}
	}

	timeLast = timeNow;
}

// get temperature adc in millivolts
unsigned int getTemperature()
{	
	byte dataH;
	byte dataL;

	digitalWrite(SS_PIN, LOW);
	SPI.transfer(0b10011100); // ADCC for temperature channel
	digitalWrite(SS_PIN, HIGH);
	delayMicroseconds(250);
	digitalWrite(SS_PIN, LOW);
	SPI.transfer(0b10000000);  // ADCR (ADC reading) Instruction
	dataH = SPI.transfer(0x00);  // get the sensor response high byte
	dataL = SPI.transfer(0x00);  // get the sensor response low byte
	digitalWrite(SS_PIN, HIGH);

	// The sensor response is two bytes length but the answer length
	// is 11 bits only and saved using the lower 4 bits from the high 
	// (first) byte and the upper 7 bits from the lower (second) byte
	// (dataH & 0b00001111) gets the lower 4 bits value from the 
	// high byte.
	// (dataL>>1) gets the upper 7 bits value from the lower byte.
	// The temperature is the resulting word of the high and low bytes
	
	unsigned int result = ((dataH & 0b00001111)<<7) + (dataL>>1);
	return result;
}

// get angular rate adc in millivolts
unsigned int getAngularRate()
{
	byte dataH;
	byte dataL;

	digitalWrite(SS_PIN, LOW);
	SPI.transfer(0b10010100);  // ADCC for angular rate channel
	digitalWrite(SS_PIN,HIGH);
	delayMicroseconds(250);
	digitalWrite(SS_PIN,LOW);
	SPI.transfer(0b10000000);  // send SPI ADCR instruction
	dataH = SPI.transfer(0x00);  // get the sensor response high byte
	dataL = SPI.transfer(0x00);  // get the sensor response low byte
	digitalWrite(SS_PIN,HIGH);

	// The sensor response is two bytes length but the answer length
	// is 11 bits only and saved using the lower 4 bits from the high 
	// (first) byte and the upper 7 bits from the lower (second) byte
	// (dataH & 0b00001111) gets the lower 4 bits value from the 
	// high byte.
	// (dataL>>1) gets the upper 7 bits value from the lower byte.
	// The angular rate is the resulting word of the high and low bytes
	unsigned int result = (word(0, dataH & 0b00001111)<<7) + (dataL>>1);
	return result;
}

// converts the adc reading to angles per second
int adcToAngularRate(unsigned int adcValue)
{
	int vOutAngularRate = (adcValue * 25/12)+400;  // in mV (millivolts)

	// from the data sheet, N2 version is 6,67	
	// E2 is 13,33 and R2 is 26,67 mV/deg
	// change accordingly.
	return (vOutAngularRate - 2500)/6.67; 
}

// converts the adc reading to centigrades
int adcToTemperature(unsigned int adcValue)
{
	int vOutTemperature = (adcValue * 25/16)+300;  // in mV (millivolts)
	return 25 + ((vOutTemperature - 2500)/10);  // from the data sheet factor is 10mV/K
}