#include <SPI.h>

#define ADC_THRESHOLD 5

#define LED 10

#define SCK 7
#define MOSI 5
#define MISO 6
#define SS 2

int adc;
int temperature;
unsigned long timePrevious = 0;
unsigned long timeNext = 0;
unsigned int updateMilliseconds = 500;

double gyroAngle = 0;

void setup() {
	Serial.begin(57600);
	
	pinMode(LED, OUTPUT);
	digitalWrite(LED, HIGH);
	
	/*
	SPI.begin();
	
	SPCR = // Configure SPI mode:
		(1<<SPE) |  // to enable SPI
		(1<<MSTR) | // to set Master SPI mode
		(1<<CPOL) | // SCK is high when idle
		(1<<CPHA) | // data is sampled on the trailing edge of the SCK
		(1<<SPR0);  // It sets SCK freq. in 8 times less than a system clock
	*/
	
	/*SPSR = (1<<SPI2X); // Set SPI double frequency
	SPCR = // Configure SPI mode:
		// (1<<SPIE) | // should be activated to enable interruption from the SPI
		(1<<SPE) | // to enable SPI
		(1<<MSTR) | // to set Master SPI mode
		(1<<CPOL) | // SCK is high when idle
		(1<<CPHA) | // data is sampled on the trailing edge of the SCK
		// (1<<SPR1) | // In this example SPI0=1, SPR1=0 (commented) and SPI2X=1
		(1<<SPR0); // It sets SCK freq. in 8 times less than a system clock
		// DORD=0: the MSB is transmitted first
	*/
	
	SPCR = (1<<MSTR) | (1<<SPIE);
	
	pinMode(MOSI, OUTPUT);
	pinMode(SCK, OUTPUT);	
	pinMode(SS, OUTPUT);
	pinMode(4, OUTPUT);
	//SPI.begin();
	
	digitalWrite(SS, HIGH);
	
	Serial.print("SS PIN: ");
	Serial.print(SS);
	Serial.println("Initialized!");
	
	getAdc();
}

int calibratedAdc = 0;
unsigned long adcSum = 0;
unsigned long count = 0;
bool needsCalibration = true;

void loop() {
	unsigned long timeNow = millis();
	if (needsCalibration) {
		if (timeNow < 250) { }
		else if (timeNow >= 250 && timeNow < 2000) {
			adcSum += getAdc();
			count++;
		}
		else {
			if (count != 0)
				calibratedAdc = round((float)adcSum / count);
			else
				Serial.println("No results measured.");

			needsCalibration = false;
			Serial.print("Calibrated adc: ");
			Serial.println(calibratedAdc);
		}
		
		delay(10);
		return;
	}

///*
	adc = getAdc();
	double angularRate = adcToAngularRate(adc);
	
	if (abs(adc - calibratedAdc) > ADC_THRESHOLD) {
		gyroAngle += angularRate * (timeNow - timePrevious) / 1000.0;
	}

	if(timeNow > timeNext) {
		Serial.println();
	
		timeNext = timeNow + updateMilliseconds;

		Serial.print("ADC ");
		Serial.print(adc);
		
		Serial.print(",\tAR ");
		Serial.print(angularRate);

		Serial.print(",\tAN ");
		Serial.print(gyroAngle);
		Serial.print("\t");
		
		//Serial.print(", \tTP ");
		//Serial.print(adcToTemperature(temperature), DEC);
	}
	
	if (abs(adc - calibratedAdc) > ADC_THRESHOLD) {
		Serial.print(", ");
		Serial.print(adc - calibratedAdc);
	}
	
	timePrevious = timeNow;
	delay(10);
//*/
}

//byte spiTransfer(byte tx) { return SPI.transfer(tx); }

byte spiTransfer(byte tx) {
	byte rxb = 0;

	for (int i = 0; i < 8; i++) {
		digitalWrite(SCK, LOW); // clock down		
		digitalWrite(MOSI, (tx & 0x80) ? HIGH : LOW); //write data MSB first

		tx = (tx << 1); //shift data right
		digitalWrite(SCK, HIGH); // clock up
		rxb = (rxb << 1); //shift receive left

		if (digitalRead(MISO))
			rxb |= 1; //read response MSB first
	}

	return rxb;
}

// get angular rate adc in millivolts
unsigned int getAdc()
{
	digitalWrite(SS, LOW);
	spiTransfer(0b10010100);  // ADCC for angular rate channel
	digitalWrite(SS, HIGH);
	
	delayMicroseconds(250);
	
	digitalWrite(SS, LOW);
	spiTransfer(0b10000000);  // send SPI ADCR instruction	
	byte dataH = spiTransfer(0x00);  // get the sensor response high byte
	byte dataL = spiTransfer(0x00);  // get the sensor response low byte
	digitalWrite(SS,HIGH);

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
double adcToAngularRate(unsigned int adcValue)
{
	int vOutAngularRate = (adcValue * 25/12)+400;  // in mV (millivolts)

	// from the data sheet, N2 version is 6,67	
	// E2 is 13,33 and R2 is 26,67 mV/deg
	// change accordingly.
	return (vOutAngularRate - 2500)/6.67; 
}