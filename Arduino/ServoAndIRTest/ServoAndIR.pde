#include <Servo.h>
#include <TimedAction.h>
#include <WProgram.h>
#include "pins.h"


Servo servo;
int currentAngle = 0;
int turnDirection = 4;
int turnDirectionAbs = abs(turnDirection);
int turnTime = 5;
TimedAction servoAction = TimedAction(turnTime, turnServo);
TimedAction outputAction = TimedAction(700, outputInfo);

void servoSetup(){
    servo.attach(SERVO_PIN);
}

void turnServo(){
    calculateTurnDirection();
    calculateServoAngle();
    servo.write(currentAngle);
}

void calculateTurnDirection(){
  if(isLeft() && !isRight()){
      turnDirection = turnDirectionAbs;
  }else if(!isLeft() && isRight()){
      turnDirection = -turnDirectionAbs;
  }else if(isLeft() && isRight()){
      turnDirection = 0;
  }
}

void calculateServoAngle(){
   int newAngle = currentAngle + turnDirection;
   if(newAngle >= 0 && newAngle < 180){
     currentAngle = newAngle;
   }else{
     newAngle = currentAngle;
     //Serial.println("Servo arvutuse viga!");
   }
  
   //calculateSweepServo();
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
    outputAction.check();
}

void outputInfo(){
   outputServo();
   outputIR();
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

