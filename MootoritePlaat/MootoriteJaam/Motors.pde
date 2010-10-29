void motors_setup() {
	pinMode(MOTOR_MODE, OUTPUT);
	digitalWrite(MOTOR_MODE, HIGH);
	
	motorLeft.setMinMaxPwm(20, 255);
	motorLeft.setMinMaxPwm(20, 255);
	motorLeft.setMinMaxPwm(20, 255);

	dribbler.setSpeed(255);
}

void motors_loop() {

}
