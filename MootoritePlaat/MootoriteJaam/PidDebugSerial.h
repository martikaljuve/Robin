#include <WProgram.h>

#include "CPid.h"

/********************************************
 * Serial Communication functions / helpers
 ********************************************/

union {                // This Data structure lets
  byte asBytes[24];    // us take the byte array
  float asFloat[6];    // sent from processing and
}                      // easily convert it to a
foo;                   // float array

// Getting float values from processing into the arduino was no small task. The way this program does it is
// as follows:
//  * a float takes up 4 bytes. In processing, convert the array of floats we want to send, into an array
//    of bytes.
//  * send the bytes to the arduino
//  * use a data structure known as a union to convert the array of bytes back into an array of floats

//  The bytes coming from the arduino follow the following format:
//  0: 0=Manual, 1=Auto, else = ? error ?
//  1-4: float setpoint
//  5-8: float input
//  9-12: float output  
//  13-16: float P_Param
//  17-20: float I_Param
//  21-24: float D_Param

void SerialReceive(Pid pid)
{
	// read the bytes sent from Processing
	int index = 0;
	byte Auto_Man = -1;
	while(Serial.available()&&index<25)
	{
		if(index==0) Auto_Man = Serial.read();
		else foo.asBytes[index-1] = Serial.read();
		index++;
	}

	// if the information we got was in the correct format, 
	// read it into the system
	if(index==25  && (Auto_Man==0 || Auto_Man==1))
	{
		pid.setpoint = double(foo.asFloat[0]);

		// the user has the ability to send the value of "Input"  in most cases (as in this one) this is not needed.
		// pid.input = double(foo.asFloat[1]);

		// only change the output if we are in manual mode. Otherwise we'll get an output blip, then the controller
		// will overwrite.
		if(Auto_Man==0)
			pid.output = double(foo.asFloat[2]);
	
		// read in and set the controller tunings
		double p = double(foo.asFloat[3]);
		double i = double(foo.asFloat[4]);
		double d = double(foo.asFloat[5]);
		pid.pid.SetTunings(p, i, d);
	
		// set the controller mode
		if(Auto_Man==0)
			pid.pid.SetMode(MANUAL);
		else
			pid.pid.SetMode(AUTO);
	}

	// clear any random data from the serial buffer
	Serial.flush();
}

// Unlike our tiny microprocessor, the processing app has no problem converting strings into floats, so
// we can just send strings. Much easier than getting floats from processing to here no?
void SerialSend(Pid pid)
{
	Serial.print("PID ");
	Serial.print(pid.setpoint);
	Serial.print(" ");
	Serial.print(pid.input);   
	Serial.print(" ");
	Serial.print(pid.output);   
	Serial.print(" ");
	Serial.print(pid.pid.GetP_Param());   
	Serial.print(" ");
	Serial.print(pid.pid.GetI_Param());   
	Serial.print(" ");
	Serial.print(pid.pid.GetD_Param());   
	Serial.print(" ");

	if(pid.pid.GetMode()==AUTO)
		Serial.println("Automatic");
	else
		Serial.println("Manual");
}