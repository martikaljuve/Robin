void motors_setup() {
	pinMode(MOTOR_MODE, OUTPUT);
	digitalWrite(MOTOR_MODE, HIGH);
	
	//motorLeft.setMinMaxPwm(30, 255);
	//motorRight.setMinMaxPwm(30, 255);
	//motorBack.setMinMaxPwm(30, 255);

	//dribbler.setMinMaxPwm(50, 255);
	//dribbler.setSpeedWithDirection(255);
}

void motors_loop() {

}

