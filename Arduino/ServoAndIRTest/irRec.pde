#include "pins.h"
#include <IRremote.h>
#include <TimedAction.h>
#include <WProgram.h>

static IRrecv irRecvLeft(RECV_PIN_L);
static IRrecv irRecvRight(RECV_PIN_R);
static decode_results resultsLeft;
static decode_results resultsRight;

int leftResult;
int rightResult;

int leftCount = 0;
int rightCount = 0;
int const maxCount = 2; //How many results to take the max value from

bool debug = true;

void irSetup(){
  irRecvLeft.enableIRIn(); // Start the receiver
  irRecvRight.enableIRIn();
}

void irResume(){
    irRecvLeft.resume(); // Receive the next value
    irRecvRight.resume();
}


void irLoop() {
  
  bool left = irRecvLeft.decode(&resultsLeft);
  bool right = irRecvRight.decode(&resultsRight);
  
  if(left){
    resultsLeft.value &= 0b011111111111;
    leftCount++;

    //Serial.println(left_count);
    
    if(resultsLeft.value >= leftResult){    
      leftResult = resultsLeft.value;  
    }

    if(leftCount >= maxCount){
      leftCount = 0; 
      leftResult = 0;
    }
    
    if(debug){
      Serial.print("LEFT: ");
      Serial.print(resultsLeft.value, BIN);
      
    }
    irResume();
  }

  if(right){
    resultsRight.value &= 0b011111111111;
    rightCount++;


    if(resultsRight.value >= rightResult){    
      rightResult = resultsRight.value;  
    }
    
    if(rightCount >= maxCount){
      rightCount = 0; 
      rightResult = 0;
    }
    
        
    if(debug){
      Serial.print("\t");
      Serial.print("RIGHT: ");
      Serial.print(resultsRight.value, BIN);
    }
    irResume();
  }
  
  if(left || right){
    if(debug)
      Serial.println();
  }
 
}

int getLeftResult(){
  return leftResult;
}

int getRightResult(){
  return rightResult;
}
