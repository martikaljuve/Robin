#include <IRremote.h>
#include <TimedAction.h>

static IRrecv irRecvLeft(RECV_PIN_L);
static IRrecv irRecvRight(RECV_PIN_R);
static decode_results resultsLeft;
static decode_results resultsRight;
static const short MAX_TIMER = 20;

TimedAction irCheckAction = TimedAction(50, irCheck);

int leftResult;
int rightResult;
short leftTimer;
short rightTimer;
int const maxCount = 2; //How many results to take the max value from
byte channelToSearch;

void beaconIrSetup(){
  irRecvLeft.enableIRIn(); // Start the receiver
  irRecvRight.enableIRIn();

  channelToSearch = readChannelFromEeprom();
  //channelToSearch = 1;
}

void beaconIrLoop() {
  irCheckAction.check();
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

  if (isLeftInView())
	  leftTimer = MAX_TIMER;
  else if (leftTimer > 0)
	  leftTimer--;

  if (isRightInView())
	  rightTimer = MAX_TIMER;
  else if (rightTimer > 0)
	  rightTimer--;

  #ifdef SERVO_DEBUG
  Serial.print(leftResult);
  Serial.print("\t");
  Serial.print(rightResult);
  Serial.println();
  #endif
}

int getLeftResult(){
  return leftResult;
}

int getRightResult(){
  return rightResult;
}

bool isLeftInView(){
  return getLeftResult() == channelToSearch;
}

bool isRightInView(){
  return getRightResult() == channelToSearch;
}

bool isLeftIr() {
	return leftTimer > 0;
}

bool isRightIr() {
	return rightTimer > 0;
}

void writeChannelToEeprom(byte newChannel){
  EEPROM.write(0, newChannel);
}

byte readChannelFromEeprom(){
 EEPROM.read(0);
}

void setIrChannel(byte channel) {
	channelToSearch = channel;
	writeChannelToEeprom(channel);
}