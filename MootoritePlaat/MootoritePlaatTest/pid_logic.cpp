#include "pid_logic.h"



float maxRPM = 500; // ~8 rotations per second * 60

double input[maximum_nr_of_sensors]; //Inputs for each sensor/wheel (what we get from sensors)
double output[maximum_nr_of_sensors]; //Outputs for each sensor/wheel (what we send to wheels)
double setpoint[maximum_nr_of_sensors]; //Setpoints for each sensor/wheel (what we want the wheels to be)
double prevOutput[maximum_nr_of_sensors]; 

unsigned long serialTime; //This is for Pid FrontEnd graph

PID pidInstances[maximum_nr_of_sensors]; //We keep each PID instance here

TimedAction pidAction = TimedAction(100, pidCalc);

void pid_setup() {

	//We cycle through each sensor/pid instance 
	for(int i = 0; i < active_nr_of_sensors; i++){

		//This initializes the pidInstance, constants are added here
		pidInstances[i] = PID(&input[i], &output[i], &setpoint[i], 3, 5, 0); //0.03, 0.17, 0.04 looks okei, second:  0.75, 0.67, 0.1
		
		//Pid only works with positive values, something to keep the dir is needed here
                /*
                int input_value = map(getRealSpeed(i), -300, 300, 0, 500);
                int setpoint_value = map(getRealSpeed(i), -255, 255, 0, 500);
		input[i] = input_value; //We get the input from getRealSpeed in the mag_sens file and convert RPM to PWM with /2
		setpoint[i] = setpoint_value; // we get the pwm from the motor_logic file
		*/
		//Some more pidInstance parameters
		pidInstances[i].SetMode(AUTO); 
		pidInstances[i].SetOutputLimits(0, 255); //PID is set to work in our PWM range [-255, 255]
	}

}



void pid_loop() {
  pidAction.check();
}

void pidCalc(){
  
	//Again we cycle through the sensors/wheels/pidInstances
	for(int i = 0; i < active_nr_of_sensors; i++){

                int real_speed = abs(getRealSpeed(i));
                if(real_speed > 255){
                  real_speed = 255;
                }
                int input_value = real_speed;
                int setpoint_value = abs(getOneSpeed(i));
		input[i] = input_value; //We get the input from getRealSpeed in the mag_sens file and convert RPM to PWM with /2
		setpoint[i] = setpoint_value; // we get the pwm from the motor_logic file
		pidInstances[i].Compute(); //Compute the new RPM

                int output_value = output[i];
                setOnePWM(i, output_value); //Set the new PWM directly (normally we should use setOneSpeed)
                
		//Some serials for breakfast
		if (prevOutput[i] != output[i] || !(millis()%1000)) {
			Serial.print("Speed: ");
			Serial.print(getRealSpeed(i));
			Serial.print(", motor: ");
			Serial.print(i);
			Serial.print(":  input: ");
			Serial.print(input_value);
			Serial.print(", output: ");
			Serial.print(output[i]);
			Serial.print(", output pwm: ");
			Serial.print(output_value);
			//Serial.print("output value: ");
			//Serial.print(value);
			Serial.print(", setpoint: ");
			Serial.println(setpoint_value);


			prevOutput[i] = output[i];
		}

		//This is needed for Pid FrontEnd graph
		if (millis() > serialTime && i == 2) {
			SerialReceive();
			SerialSend();
			serialTime += 500;
		}

	}
}



//Pid FrontEnd graph stuff starts here


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
    setpoint[0]=double(foo.asFloat[0]);
    //input=double(foo.asFloat[1]);       // * the user has the ability to send the 
                                          //   value of "Input"  in most cases (as 
                                          //   in this one) this is not needed.
    if(Auto_Man==0)                       // * only change the output if we are in 
    {                                     //   manual mode.  otherwise we'll get an
      output[0]=double(foo.asFloat[2]);      //   output blip, then the controller will 
    }                                     //   overwrite.
    
    double p, i, d;                       // * read in and set the controller tunings
    p = double(foo.asFloat[3]);           //
    i = double(foo.asFloat[4]);           //
    d = double(foo.asFloat[5]);           //
    pidInstances[0].SetTunings(p, i, d);            //
    
    if(Auto_Man==0) pidInstances[0].SetMode(MANUAL);// * set the controller mode
    else pidInstances[0].SetMode(AUTO);             //
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
  Serial.print(setpoint[2]);   
  Serial.print(" ");
  Serial.print(input[2]);   
  Serial.print(" ");
  Serial.print(output[2]);   
  Serial.print(" ");
  Serial.print(pidInstances[2].GetP_Param());   
  Serial.print(" ");
  Serial.print(pidInstances[2].GetI_Param());   
  Serial.print(" ");
  Serial.print(pidInstances[2].GetD_Param());   
  Serial.print(" ");
  if(pidInstances[2].GetMode()==AUTO) Serial.println("Automatic");
  else Serial.println("Manual");  
}
