#include <IRremote.h>
#include <TimedAction.h>

static IRrecv irRecvLeft(RECV_PIN_L);
static IRrecv irRecvRight(RECV_PIN_R);
static decode_results resultsLeft;
static decode_results resultsRight;
static const short MAX_TIMER = 7;
static const short MAX_TIMER = 200;
static const short SIGNAL_TIMER_PERIOD = 113; //Kui 113 ms jooksul pole signaali

TimedAction irCheckAction = TimedAction(200, irCheck);
//TimedAction irCheckAction = TimedAction(200, irCheck);

int leftResult;
int rightResult;
short leftTimer;
short rightTimer;
long leftSignalTimer;
long rightSignalTimer;

byte channelToSearch;

void beaconIrSetup(){
	irRecvLeft.enableIRIn(); // Start the receiver
	irRecvRight.enableIRIn();

	//writeChannelToEeprom(51);
	channelToSearch = readChannelFromEeprom();
	if (channelToSearch < 1 || channelToSearch > 4)
		channelToSearch = 1;
}

void beaconIrLoop() {
	irCheckAction.check();
       irCheck();
	//irCheckAction.check();
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
               if(leftSignalTimer <= millis()){
		      leftResult = 0;
                     leftSignalTimer = millis()+SIGNAL_TIMER_PERIOD;
               }
	}
	if(right){
		resultsRight.value &= 0b000000000111;
		rightResult = resultsRight.value;      
	}else{
		rightResult = 0;
               if(rightSignalTimer <= millis()){
		      rightResult = 0;
                     rightSignalTimer = millis()+SIGNAL_TIMER_PERIOD;
               }
		//rightResult = 0;
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
	Serial.print("channel: ");
	Serial.print((int)channelToSearch);
	Serial.print("\t");
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

int readChannelFromEeprom(){
	return (int)EEPROM.read(0);
}

void setIrChannel(byte channel) {
	channelToSearch = channel;
	writeChannelToEeprom(channel);
}

byte getIrChannel() {
	return channelToSearch;
}

