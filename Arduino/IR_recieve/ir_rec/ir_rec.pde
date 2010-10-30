#define DEBUG
#include <IRremote.h>
#include <TimedAction.h>

int RECV_PIN_L = 2;
int RECV_PIN_R = 3;
IRrecv irrecv_left(RECV_PIN_L);
IRrecv irrecv_right(RECV_PIN_R);
decode_results results_left;
decode_results results_right;


void setup()
{
  Serial.begin(9600);
  irrecv_left.enableIRIn(); // Start the receiver
  irrecv_right.enableIRIn();

}



// Dumps out the decode_results structure.
// Call this after IRrecv::decode()
// void * to work around compiler issue
//void dump(void *v) {
//  decode_results *results = (decode_results *)v
void dump(decode_results *results) {
  int count = results->rawlen;
  if (results->decode_type == UNKNOWN) {
    Serial.println("Could not decode message");
  } 
  else {
    if (results->decode_type == RC5) {
      Serial.print("Decoded RC5: ");
    }
    Serial.print(results->value, HEX);
    Serial.print(" (");
    Serial.print(results->bits, DEC);
    Serial.println(" bits)");
  }
  Serial.print("Raw (");
  Serial.print(count, DEC);
  Serial.print("): ");

  for (int i = 0; i < count; i++) {
    if ((i % 2) == 1) {
      Serial.print(results->rawbuf[i]*USECPERTICK, DEC);
    } 
    else {
      Serial.print(-(int)results->rawbuf[i]*USECPERTICK, DEC);
    }
    Serial.print(" ");
  }
  Serial.println("");
}


void loop() {
  
  bool left = irrecv_left.decode(&results_left);
  if (left) {
    results_left.value &= 0b011111111111;
    Serial.print("LEFT");
    Serial.print("val: ");
    Serial.print(results_left.value, BIN);
   // Serial.print("bin: ");
    //Serial.print(results_left.bits, BIN);
    //dump(&results_left);
    irrecv_left.resume(); // Receive the next value
    irrecv_right.resume();
  }


  bool right = irrecv_right.decode(&results_right);
  //bool right = false;
  if (right) {
    results_right.value &= 0b011111111111;
    Serial.print("RIGHT");
    Serial.print("val: ");
    Serial.print(results_right.value, BIN);
    //Serial.print("bin: ");
    //Serial.print(results_right.bits, BIN);
    //dump(&results_right);
    irrecv_right.resume(); // Receive the next value
     irrecv_left.resume();
     
  }
  
  if(left || right){
    Serial.println();
  }
 
}
