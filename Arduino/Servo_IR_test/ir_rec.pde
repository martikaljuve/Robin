#include "pins.h"
#include <IRremote.h>
#include <TimedAction.h>
#include <WProgram.h>

IRrecv irrecv_left(RECV_PIN_L);
IRrecv irrecv_right(RECV_PIN_R);
decode_results results_left;
decode_results results_right;

int left_result;
int right_result;

int left_count = 0;
int right_count = 0;
int const max_count = 2;

bool debug = true;

void ir_setup(){
  irrecv_left.enableIRIn(); // Start the receiver
  irrecv_right.enableIRIn();
}

void ir_resume(){
    irrecv_left.resume(); // Receive the next value
    irrecv_right.resume();
}


void ir_loop() {
  
  bool left = irrecv_left.decode(&results_left);
  bool right = irrecv_right.decode(&results_right);
  
  if(left){
    results_left.value &= 0b011111111111;
    left_count++;

    //Serial.println(left_count);
    
    if(results_left.value >= left_result){    
      left_result = results_left.value;  
    }

    if(left_count >= max_count){
      left_count = 0; 
      left_result = 0;
    }
    
    if(debug){
      Serial.print("LEFT: ");
      Serial.print(results_left.value, BIN);
      
    }
    ir_resume();
  }

  if(right){
    results_right.value &= 0b011111111111;
    right_count++;


    if(results_right.value >= right_result){    
      right_result = results_right.value;  
    }
    
    if(right_count >= max_count){
      right_count = 0; 
      right_result = 0;
    }
    
        
    if(debug){
      Serial.print("\t");
      Serial.print("RIGHT: ");
      Serial.print(results_right.value, BIN);
    }
    ir_resume();
  }
  
  if(left || right){
    if(debug)
      Serial.println();
  }
 
}

int getLeftResult(){
  return left_result;
}

int getRightResult(){
  return right_result;
}
