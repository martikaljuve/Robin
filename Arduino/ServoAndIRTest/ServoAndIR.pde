#include <Servo.h>
#include <TimedAction.h>
#include <WProgram.h>
#include "pins.h"


Servo servo;
int currentAngle = 0;
int turnDirection = 4;
int turnTime = 5;
TimedAction servoAction = TimedAction(turnTime, turnServo);
TimedAction outputAction = TimedAction(700, outputIR);

void servoSetup(){
    servo.attach(SERVO_PIN);
}

void turnServo(){
    servo.write(currentAngle+turnDirection);
    calculateServo();
}

void calculateServo(){
   int newAngle = currentAngle += turnDirection;
   if(newAngle >= 0 && newAngle < 180){
     currentAngle = newAngle;
   }else{
     //Serial.println("Servo arvutuse viga!");
   }
   calculateSweepServo();
}

void calculateSweepServo(){
   if(currentAngle >= 178 || currentAngle <= 0){
      turnDirection *= -1;
      increaseSpeed();
   }
}

void increaseSpeed(){
    if(turnTime > 1){
      turnTime--;
    }
}

void servoAndIRsetup(){
    Serial.begin(57600);
    irSetup();
    servoSetup();
}

void servoAndIRloop(){
    irLoop();
    servoAction.check();
    //outputAction.check();
}

void outputServo(){
   Serial.print("Servo nurk: ");
   Serial.println(currentAngle);
}

void outputIR(){
   Serial.print("Left: ");
   Serial.print(getLeftResult());
   Serial.print("\tRight: ");
   Serial.print(getRightResult());   
   Serial.println();
}

int getServoAngle(){ //Returns the probable servo angle, servo might not be in that position yet
  return currentAngle;
}

