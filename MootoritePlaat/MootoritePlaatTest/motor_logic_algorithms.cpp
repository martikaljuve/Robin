#include "motor_logic_algorithms.h"

void frag2ball(){
  
  delay(2000);  //Wait for 2 seconds until start, in that time motor_up is working

  moveAndTurn(285, 255, 0); //Try to move left. Because of the inbalances, 285 was used instead
  delay(1130); //Wait some time
  setSpeed(0,0,-170); //Because out moving to 285 (~270) degrees was fucked up, we need to balance things out
  delay(240); //For 250 ms
  stop(); //Stop everything, robot is in front of the gate
  delay(200); //Wait a bit
  
  analogWrite(MOTOR_UP_PWM, 0); //Tribbler stop
  delay(10); 
  digitalWrite(MOTOR_UP_DIR, HIGH); //We start turning the tribbler in the opposite direction
  analogWrite(MOTOR_UP_PWM, 255); 
  moveAndTurn(0, 255, 0); //We move in a straight line towards the enemy gate. This works suprisingly well.
  delay(2000); //Do that for about 2 secons
  stop(); //Done
  
  analogWrite(MOTOR_UP_PWM, 0); //Stop tribbles
  
}

void test_motor_speeds(){
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
    for(int m = 0; m<3 ; m++){
      
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

void figureRectangle() {
  stop();
  delay(1000);
  moveAndTurn(0, 255, 0);
  delay(1000);
  
  stop();
  delay(1000);
  moveAndTurn(90, 255, 0);  
  delay(1000);
  
  stop();
  delay(1000);
  moveAndTurn(180, 255, 0);  
  delay(1000);

  stop();
  delay(1000);
  moveAndTurn(270, 255, 0);  
  delay(1000);

  stop();
  delay(1000);
  moveAndTurn(0, 0, 100);
  delay(1000);
}

void figureCircle() {
  for (int i = 0; i < 100; i++) {
    stop();
    delay(1000);
    moveAndTurn(0, 255, 10);
    delay(1000);
  }
}
