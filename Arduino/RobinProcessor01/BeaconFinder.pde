#include <Servo.h>
#include <TimedAction.h>
#include <EEPROM.h>

#define SERVO_DEBUG

Servo servo;
bool moveLeft = false;
int currentAngle = 0;

int leftCount = 0;
int rightCount = 0;

const int halfRotation = 90;

TimedAction servoAction = TimedAction(200, turnServo);
//TimedAction servoAction = TimedAction(200, turnServo);
TimedAction beaconAction = TimedAction(40, beaconFinderAction);

#ifdef SERVO_DEBUG
TimedAction outputAction = TimedAction(700, outputInfo);
#endif

void beaconFinderSetup(){
	beaconIrSetup();
	servo.attach(SERVO_PIN);
}

void beaconFinderAction(){
 beaconIrLoop();
 turnServo();
 
}
void beaconFinderLoop(){
	beaconIrLoop();
	servoAction.check();

	//beaconIrLoop();
	//servoAction.check();
       beaconAction.check();
	#ifdef SERVO_DEBUG
	outputAction.check();
	#endif
}

void turnServo(){
	calculateServoAngle();
	servo.write(currentAngle);
}

void calculateServoAngle(){
	if(isLeftIr() && isRightIr()){
		leftCount = 0;
		rightCount = 0;
	}
	else if (isLeftIr()) {
		leftCount++;
		//leftCount++;
		rightCount = 0;
		moveLeft = true;
	}
	else if (isRightIr()) {
		leftCount = 0;
		rightCount++;
		//rightCount++;
		moveLeft = false;
	}
	else {
		leftCount++;
		rightCount++;
	}

	if (leftCount == 0 && rightCount == 0)
		return;

	int maxCount = max(leftCount, rightCount);
	int speed = constrain(maxCount, 1, 15);
	//int speed = constrain(maxCount, 1, 15);
	//int speed = 2;
	currentAngle += speed * (moveLeft ? -1 : 1);

	if (currentAngle < 0) {
		currentAngle = 0;
		moveLeft = !moveLeft;
		leftCount = 0;
		rightCount = 0;
	}
	else if (currentAngle > 180) {
		currentAngle = 180;
		moveLeft = !moveLeft;
		leftCount = 0;
		rightCount = 0;
	}
}

int getServoAngle(){ //Returns the probable servo angle, servo might not be in that position yet
	return currentAngle - halfRotation;
}

#ifdef SERVO_DEBUG
void outputInfo(){
	outputServo();
	outputIR();
}

void outputServo(){
	Serial.print("Servo nurk: ");
	Serial.print(currentAngle);
}

void outputIR(){
	Serial.print("\tLeft: ");
	Serial.print(getLeftResult());
	Serial.print("\tRight: ");
	Serial.print(getRightResult());   
	Serial.println();
}
#endif

