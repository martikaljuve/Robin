#include <Servo.h>
#include <TimedAction.h>
#include <EEPROM.h>
#include <WProgram.h>
#include "pins.h"


Servo servo;
int currentAngle = 0;
int startTurnDirection = 4;
int turnDirection = startTurnDirection;
int turnDirectionAbs = abs(turnDirection);

int startTurnTime = 5;
int turnTime = startTurnTime;
int maxTurnTime = 20;

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
  if(isLeftInView() && !isRightInView()){
      turnDirection = (turnDirection == 0)?startTurnDirection:turnDirectionAbs;
  }else if(!isLeftInView() && isRightInView()){
      turnDirection = (turnDirection == 0)?-startTurnDirection:-turnDirectionAbs;
  }else if(isLeftInView() && isRightInView()){
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

//REDO
void increaseSpeed(){
    if(turnTime > 2){
      turnTime--;
    }
    servoAction.setInterval(turnTime);
}
//REDO
void decreaseSpeed(){
    if(turnTime < maxTurnTime){
      turnTime++;
    }
    servoAction.setInterval(turnTime);
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

