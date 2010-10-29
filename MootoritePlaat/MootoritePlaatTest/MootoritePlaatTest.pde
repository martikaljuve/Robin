/*
Author: Raimond Tunnel
17. october 2010
This code speeds motors up to max speed, then slows down to 0, changes direction and repeats the process.
*/
/*
#include "lib/TimedAction/TimedAction.h"
#include "lib/MLX90316/MLX90316.h"
*/
#include <TimedAction.h>
#include <MLX90316.h>
#include <PID_Beta6.h>

#include "pins.h"

#include "motor_logic.h"
#include "mag_sens.h"
#include "pid_logic.h"

#include "motor_logic_algorithms.h"

//An action for some extra output
TimedAction printAction = TimedAction(1750, printSpeeds);

//Test of some motor algoritms using timed actions
TimedAction runAction = TimedAction(3500, runLoop);


void setup() {

  pinMode(LED, OUTPUT);   //Set the led
  digitalWrite(LED, HIGH);
  Serial.begin(9600); //For serial output

  delay(5000); //Wait 5 sec in the beginning

  motor_logic_setup(); //Setup motors
  mag_sens_setup(); //Setup magnets
  pid_setup(); //Setup PID
  Serial.println("Setup done!");


  //setOneSpeed(1, 120);
  
  //setSpeed(100,100,100);
  //setSpeed(70, -70, 120);
  
  setSpeed(0, 0, 220);
  	
  /*analogWrite(MOTOR_LEFT_PWM, 255);
    analogWrite(MOTOR_RIGHT_PWM, 255);
    analogWrite(MOTOR_BACK_PWM, 255);
    */
  
  //delay(2000);
  //moveAndTurn(0, 150, 0);
  
  //delay(1500);
  //stop();
  //setSpeed(150, 150, 0);
  
  //frag2ball(); //Go get them ballz

  //frag1ball();
  //setSpeed(100, 100, 100);
  //setSpeed(150, 0, 0);
  //stopDribbler();
  
}


void loop() {
	mag_sens_loop(); //Loop the mag sens
	pid_loop(); //Loop the pids

	//runAction.check(); //Uncomment for the dancing queen
  
    //For extra output we can call for printAction
	printAction.check();


}

/*
Function that prints the last measured speeds of 3 wheels
*/
void printSpeeds(){
	for(int i = 0; i < 3; i++){
		Serial.print("Motor ");
		Serial.print(i);
		Serial.print(" measured speed: ");
		Serial.print(getRealSpeed(i));
                Serial.print("->");
                Serial.print(getOneSpeed(i));
		Serial.print("\t");
	}
	Serial.println();

}




