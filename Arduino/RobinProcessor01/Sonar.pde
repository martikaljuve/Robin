#include <Wire.h>

#define srfAddress 0x70 // Address of the SRF08
#define cmdByte 0x00	// Command byte
#define lightByte 0x01	// Byte to read light sensor
#define rangeByte 0x02	// Byte for start of ranging data

byte highByte = 0x00;	// Stores high byte from ranging
byte lowByte = 0x00;	// Stored low byte from ranging

void sonarSetup() {
	
}

void sonarLoop() {

	int rangeData = getRange();
	Serial.print("S1 ");
	Serial.println(rangeData, DEC);

	int lightData = getLight();
	Serial.println(lightData, DEC);
}

int getRange(){
	int range = 0; 
	
	Wire.beginTransmission(srfAddress);	// Start communicating with SRF08
	Wire.send(cmdByte);					// Send Command Byte
	Wire.send(0x51);					// Send 0x51 to start a ranging
	Wire.endTransmission();
	
	delay(100);							// Wait for ranging to be complete
	
	Wire.beginTransmission(srfAddress);	// start communicating with SRFmodule
	Wire.send(rangeByte);				// Call the register for start of ranging data
	Wire.endTransmission();
	
	Wire.requestFrom(srfAddress, 2);	// Request 2 bytes from SRF module
	while(Wire.available() < 2);		// Wait for data to arrive
	highByte = Wire.receive();			// Get high byte
	lowByte = Wire.receive();			// Get low byte

	range = (highByte << 8) + lowByte;	// Put them together
	
	return(range);						// Returns Range
}

int getLight(){
	Wire.beginTransmission(srfAddress);
	Wire.send(lightByte);				// Call register to get light reading
	Wire.endTransmission();
	
	Wire.requestFrom(srfAddress, 1);	// Request 1 byte
	while(Wire.available() < 0);		// While byte available
	int lightRead = Wire.receive();		// Get light reading
		
	return(lightRead);					// Returns lightRead
}

int getSoft(){							// Function to get software revision
	Wire.beginTransmission(srfAddress);	// Begin communication with the SRF module
	Wire.send(cmdByte);					// Sends the command bit, when this bit is read it returns the software revision
	Wire.endTransmission();
	
	Wire.requestFrom(srfAddress, 1);	// Request 1 byte
	while(Wire.available() < 0);		// While byte available
	int software = Wire.receive();		// Get byte
		
	return(software);
}