#include <TimedAction.h>
#include <MLX90316.h>
#include "pins.h"
#include <PID_Beta6.h>

const int calcInterval = 26;

MLX90316 left_sensor = MLX90316();
TimedAction angleAction = TimedAction(2, checkAngle);
TimedAction avgAction = TimedAction(calcInterval, calculateSpeed);
TimedAction pidAction = TimedAction(calcInterval * 4, pid);

float maxRPM = 500; // ~8 rotations per second * 60
float leftMeasuredSpeed;

double input;
double output;
double setpoint;
double prevOutput;
unsigned long serialTime;

//PID leftPid(&input, &output, &setpoint, 0.408, 0.612, 0.204);
PID leftPid(&input, &output, &setpoint, 1.0, 1.5, 0.0);

void setup() {
	pinMode(LEFT_DIR, OUTPUT);
	pinMode(LEFT_PWM, OUTPUT);

	left_sensor.attach(SS_LEFT, SCK, MISO);

	//analogWrite(LEFT_PWM, 255);

	pinMode(MODE, OUTPUT);
	digitalWrite(MODE, HIGH);
	
	input = map(abs(leftMeasuredSpeed), 0, 500, 0, 255); // RPM to PWM
	setpoint = 200; // PWM
	leftPid.SetMode(AUTO);
	//leftPid.SetOutputLimits(0, 255);

	Serial.begin(9600);
}


void loop() {
	angleAction.check();
	avgAction.check();
	pidAction.check();

	/*if (prevOutput != output) {
		Serial.print("input: ");
		Serial.print(input);
		Serial.print("output: ");
		Serial.print(output);
		Serial.print(", setpoint: ");
		Serial.println(setpoint);
	}*/

	//long value = map(output, 0, 500, 0, 255);
	//digitalWrite(LEFT_DIR, output >= 0 ? LOW : HIGH);

	if (millis() > serialTime) {
		SerialReceive();
		SerialSend();
		serialTime += 500;
	}

	//prevOutput = output;
}

void pid() {
	input = map(abs(leftMeasuredSpeed), 0, 500, 0, 255); // RPM to PWM
	leftPid.Compute();

	analogWrite(LEFT_PWM, abs(output));
}

long prev = 0;
long diff = 0;
void checkAngle() {
	int angle = left_sensor.readAngle();
	if (angle < 0) return;

	int delta = angle - prev;
	if (delta < -1800) {
		diff += 3600 + delta;
	}
	else if (delta > 1800) {
		diff += delta - 3600;
	}
	else {
		diff += delta;
	}

	prev = angle;

	/*
	Serial.print(angle);
	Serial.print(", ");
	Serial.println(diff);
	*/
}

unsigned long previousTime = 0;

// RPM 0 - 500
// PWM 60 - 255
void calculateSpeed() {
	unsigned long currentTime = millis();
	unsigned int timeDiff = currentTime - previousTime;

	if (timeDiff <= 0) return;
	
	float rotations = diff;
	leftMeasuredSpeed = (rotations / timeDiff) * 16.666667; // 60000 ms / 3600.0 degrees
	
	//Serial.println(leftMeasuredSpeed);
	previousTime = currentTime;
	diff = 0;
}



/********************************************
 * Serial Communication functions / helpers
 ********************************************/


union {                // This Data structure lets
  byte asBytes[24];    // us take the byte array
  float asFloat[6];    // sent from processing and
}                      // easily convert it to a
foo;                   // float array



// getting float values from processing into the arduino
// was no small task.  the way this program does it is
// as follows:
//  * a float takes up 4 bytes.  in processing, convert
//    the array of floats we want to send, into an array
//    of bytes.
//  * send the bytes to the arduino
//  * use a data structure known as a union to convert
//    the array of bytes back into an array of floats

//  the bytes coming from the arduino follow the following
//  format:
//  0: 0=Manual, 1=Auto, else = ? error ?
//  1-4: float setpoint
//  5-8: float input
//  9-12: float output  
//  13-16: float P_Param
//  17-20: float I_Param
//  21-24: float D_Param
void SerialReceive()
{

  // read the bytes sent from Processing
  int index=0;
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
    setpoint=double(foo.asFloat[0]);
    //input=double(foo.asFloat[1]);       // * the user has the ability to send the 
                                          //   value of "Input"  in most cases (as 
                                          //   in this one) this is not needed.
    if(Auto_Man==0)                       // * only change the output if we are in 
    {                                     //   manual mode.  otherwise we'll get an
      output=double(foo.asFloat[2]);      //   output blip, then the controller will 
    }                                     //   overwrite.
    
    double p, i, d;                       // * read in and set the controller tunings
    p = double(foo.asFloat[3]);           //
    i = double(foo.asFloat[4]);           //
    d = double(foo.asFloat[5]);           //
    leftPid.SetTunings(p, i, d);            //
    
    if(Auto_Man==0) leftPid.SetMode(MANUAL);// * set the controller mode
    else leftPid.SetMode(AUTO);             //
  }
  Serial.flush();                         // * clear any random data from the serial buffer
}



// unlike our tiny microprocessor, the processing ap
// has no problem converting strings into floats, so
// we can just send strings.  much easier than getting
// floats from processing to here no?
void SerialSend()
{
  Serial.print("PID ");
  Serial.print(setpoint);   
  Serial.print(" ");
  Serial.print(input);   
  Serial.print(" ");
  Serial.print(output);   
  Serial.print(" ");
  Serial.print(leftPid.GetP_Param());   
  Serial.print(" ");
  Serial.print(leftPid.GetI_Param());   
  Serial.print(" ");
  Serial.print(leftPid.GetD_Param());   
  Serial.print(" ");
  if(leftPid.GetMode()==AUTO) Serial.println("Automatic");
  else Serial.println("Manual");  
}
