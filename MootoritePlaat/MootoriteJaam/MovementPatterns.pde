//For a rectangle test
int fullCorners[] = {0, 90, 180, 270};
int j = 0;
void runLoop(){
	if(j%2 == 1){
		wheels.stop();
		//Serial.println("STOP");
	}else{
		wheels.moveAndTurn(fullCorners[j], 200, 0);
		//Serial.println("Turn");
	}
	j++;
}

void frag1ball(){
	delay(10);
	dribbler.stop();
	delay(10);
	dribbler.setSpeedWithDirection(255); //We start turning the tribbler in the opposite direction

	delay(2000); 
	wheels.moveAndTurn(350, 255, 0);
	delay(2500);

	wheels.stop(); //Done

	dribbler.stop();
}

void frag2ball(){
	delay(2000);  //Wait for 2 seconds until start, in that time motor_up is working

	wheels.moveAndTurn(285, 255, 0); //Try to move left. Because of the inbalances, 285 was used instead
	delay(1130); //Wait some time
	//setSpeedWithDirection(0,0,-170); //Because out moving to 285 (~270) degrees was fucked up, we need to balance things out
	//delay(240); //For 250 ms
	wheels.stop(); //Stop everything, robot is in front of the gate
	delay(200); //Wait a bit

	dribbler.stop();
	delay(10); 
	dribbler.setSpeedWithDirection(255);

	wheels.moveAndTurn(0, 255, 0); //We move in a straight line towards the enemy gate. This works suprisingly well.

	delay(2000);

	wheels.stop();
	dribbler.stop();
}

void test_motor_speeds(){
	const int STEP_PWM = 10;
	const int MAX_PWM = 255;
	const int MIN_PWM = 0;

	int d = STEP_PWM;
	boolean change_dir;

	//Cycle in the segment [MIN_PWM, MAX_PWM], Infinite loop!
	for(int i = 1; ; i+=d){
		if(i >= MAX_PWM){ //If we go over the MAX_PWM, start to slow down
			i = MAX_PWM;
			d *= -1; //Invert acceleration
		}
		if(i <= MIN_PWM){ //If we go under the MIN_PWM, start to speed up and change direction
			i = MIN_PWM;
			d *= -1; //Invert acceleration
			change_dir = true;
		}

		motorLeft.setSpeedWithDirection(i);
		motorRight.setSpeedWithDirection(i);
		motorBack.setSpeedWithDirection(i);    

		//If we nee to change direction      
		if(change_dir){
			motorLeft.stop();
			motorRight.stop();
			motorBack.stop(); 
		}

		change_dir = false; //Reset the change_dir value
		delay(100);
	}

	digitalWrite(10, HIGH);
	delay(500);
	digitalWrite(10, LOW);
	delay(500);
}

void figureRectangle() {
	wheels.stop();
	delay(1000);
	wheels.moveAndTurn(0, 255, 0);
	delay(1000);
  
	wheels.stop();
	delay(1000);
	wheels.moveAndTurn(90, 255, 0);  
	delay(1000);
  
	wheels.stop();
	delay(1000);
	wheels.moveAndTurn(180, 255, 0);  
	delay(1000);

	wheels.stop();
	delay(1000);
	wheels.moveAndTurn(270, 255, 0);  
	delay(1000);

	wheels.stop();
	delay(1000);
	wheels.moveAndTurn(0, 0, 100);
	delay(1000);
}

void figureCircle() {
	for (int i = 0; i < 100; i++) {
		wheels.stop();
		delay(1000);
		wheels.moveAndTurn(0, 255, 10);
		delay(1000);
	}
}