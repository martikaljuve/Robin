#include <WProgram.h>

#include "CPid.h"

// Unlike our tiny microprocessor, the processing app has no problem converting strings into floats, so
// we can just send strings. Much easier than getting floats from processing to here no?
void SerialSend(Pid pid)
{
	/*Serial.print("PID ");
	Serial.print(pid.setpoint);
	Serial.print(" ");
	Serial.print(pid.input);   
	Serial.print(" ");
	Serial.print(pid.output);   
	Serial.print(" ");
	Serial.print(pid.kp);   
	Serial.print(" ");
	Serial.print(pid.ki);
	Serial.print(" ");
	Serial.print(pid.kd);
	Serial.print(" ");

	Serial.println("Automatic");*/
}

