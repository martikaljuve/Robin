/*
Author: Raimond Tunnel
17. october 2010
This code speeds motors up to max speed, then slows down to 0, changes direction and repeats the process.
*/

#include "pins.h"

#define MAX_PWM 255 //Maximum PWM we can send to driver
#define MIN_PWM 50 //Minimum PWM to turn the motor (Change this, when the motors are attached to robot!!)
#define STEP_PWM 8 //PWM step (value we increase/decrease by)

//Arrays for motor pwm, dir pins and dir values
int motors_pwm[] = {motor_left_pwm, motor_right_pwm, motor_back_pwm, motor_up_pwm};
int motors_dir[] = {motor_left_dir, motor_right_dir, motor_back_dir, motor_up_dir};
int motors_dir_values[] = {HIGH, HIGH, HIGH, HIGH};

void setup() {                
  pinMode(led, OUTPUT);
  for(int i = 0; i < 4; i++){ //All motor dir pins to output
    pinMode(motors_dir[i], OUTPUT);
    digitalWrite(motors_dir[i], motors_dir_values[i]);
  }
  digitalWrite(motor_left_dir, LOW);
  
  Serial.begin(19200); //For serial output
}

void loop() {
  int d = STEP_PWM;
  boolean change_dir;

  //Cycle in the segment [MIN_PWM, MAX_PWM], Infinite loop!
  for(int i = 1; ; i+=d){
   
    if(i >= MAX_PWM){ //If we go over the MAX_PWM, start to slow down
      i = MAX_PWM;
      d *= -1; //Invert acceleration
    }
    if(i <= MIN_PWM){ //If we go under the MIN_PWM, start to speed up and change direction
      i = MIN_PWM;
      d *= -1; //Invert acceleration
      change_dir = true;
    }
    
    //Cycle over all motors
    for(int m = 0; m<4 ; m++){
      
      analogWrite(motors_pwm[m], i); //Change motor pwm

      //If we nee to change direction      
      if(change_dir){
          analogWrite(motors_pwm[m], 0); //Change motor pwm to 0
          Serial.println("Change dir!!");
          motors_dir_values[m] = (motors_dir_values[m]==HIGH)? LOW : HIGH; //Change it's value
          digitalWrite(motors_dir[m], motors_dir_values[m]); //Send new value to digital pin
      }
     
     
       //Output stuff
      Serial.print("Motor: ");
      Serial.print(m);
      Serial.print("dir: ");
      Serial.print(motors_dir_values[m]);      
      Serial.print(" speed: ");
      Serial.println(i);
      
      
    }
   
    change_dir = false; //Reset the change_dir value
    delay(100);
  }
  
  
  digitalWrite(10, HIGH);
  delay(500);              // wait for a second
  digitalWrite(10, LOW);
  delay(500);              // wait for a second
}
