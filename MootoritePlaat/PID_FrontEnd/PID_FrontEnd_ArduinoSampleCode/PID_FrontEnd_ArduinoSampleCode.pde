/******************************************************************
 * PID Simple Example (Augmented with Processing.org Communication)
 * Reading analog input 0 to control analog PWM output 3
 ******************************************************************/

#include <PID_Beta6.h>

//Define Variables we'll be connecting to
double Setpoint, Input, Output;
int inputPin=0, outputPin=3;

//Specify the links and initial tuning parameters
PID myPID(&Input, &Output, &Setpoint,2,5,1);


unsigned long serialTime; //this will help us know when to talk with processing

void setup()
{
  //initialize the serial link with processing
  Serial.begin(9600);
  
  //initialize the variables we're linked to
  Input = analogRead(inputPin);
  Setpoint = 100;

  //turn the PID on
  myPID.SetMode(AUTO);
}

void loop()
{
  //pid-related code
  Input = analogRead(inputPin);
  myPID.Compute();
  analogWrite(outputPin,Output);
  

  
  //send-receive with processing if it's time
  if(millis()>serialTime)
  {
    SerialReceive();
    SerialSend();
    serialTime+=500;
  }
  
  
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
    Setpoint=double(foo.asFloat[0]);
    //Input=double(foo.asFloat[1]);       // * the user has the ability to send the 
                                          //   value of "Input"  in most cases (as 
                                          //   in this one) this is not needed.
    if(Auto_Man==0)                       // * only change the output if we are in 
    {                                     //   manual mode.  otherwise we'll get an
      Output=double(foo.asFloat[2]);      //   output blip, then the controller will 
    }                                     //   overwrite.
    
    double p, i, d;                       // * read in and set the controller tunings
    p = double(foo.asFloat[3]);           //
    i = double(foo.asFloat[4]);           //
    d = double(foo.asFloat[5]);           //
    myPID.SetTunings(p, i, d);            //
    
    if(Auto_Man==0) myPID.SetMode(MANUAL);// * set the controller mode
    else myPID.SetMode(AUTO);             //
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
  Serial.print(Setpoint);   
  Serial.print(" ");
  Serial.print(Input);   
  Serial.print(" ");
  Serial.print(Output);   
  Serial.print(" ");
  Serial.print(myPID.GetP_Param());   
  Serial.print(" ");
  Serial.print(myPID.GetI_Param());   
  Serial.print(" ");
  Serial.print(myPID.GetD_Param());   
  Serial.print(" ");
  if(myPID.GetMode()==AUTO) Serial.println("Automatic");
  else Serial.println("Manual");  
}

