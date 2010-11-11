#include "pins.h"
#include <IRremote.h>
#include <TimedAction.h>
#include <WProgram.h>

static IRrecv irRecvLeft(RECV_PIN_L);
static IRrecv irRecvRight(RECV_PIN_R);
static decode_results resultsLeft;
static decode_results resultsRight;
static byte channelToSearch;

TimedAction irCheckAction = TimedAction(50, irCheck);

int leftResult;
int rightResult;

int leftCount = 0;
int rightCount = 0;
int const maxCount = 2; //How many results to take the max value from

bool debug = false;

void irSetup(){
  irRecvLeft.enableIRIn(); // Start the receiver
  irRecvRight.enableIRIn();
}

void irResume(){
    irRecvLeft.resume(); // Receive the next value
    irRecvRight.resume();
}

void irCheck(){
  bool left = irRecvLeft.decode(&resultsLeft);
  bool right = irRecvRight.decode(&resultsRight);
  if(left){
    resultsLeft.value &= 0b000000000111;
    leftResult = resultsLeft.value;  
  }else{
    leftResult = 0;
  }
  if(right){
    resultsRight.value &= 0b000000000111;
    rightResult = resultsRight.value;      
  }else{
     rightResult = 0;
  }  
  irResume();
  if(debug){
    Serial.print(leftResult);
    Serial.print("\t");
    Serial.print(rightResult);
    Serial.println();
  }
}

void irLoop() {
  irCheckAction.check();
}

int getLeftResult(){
  return leftResult;
}

int getRightResult(){
  return rightResult;
}

bool isLeft(){
  return (getLeftResult() == channelToSearch);
}
bool isRight(){
  return (getRightResult() == channelToSearch);
}

void setChannelToSearch(byte newChannel){
  channelToSearch = newChannel;
}
