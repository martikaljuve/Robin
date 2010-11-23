//#define PID_DEBUG

TimedAction pidAction = TimedAction(10, pidCompute);

long timePrevious = 0;

void pids_setup() {
	//pidLeft.setOutputLimits(-255, 255);
	//pidRight.setOutputLimits(-255, 255);
	//pidBack.setOutputLimits(-255, 255);

	wheels.desiredPositionLeft = magnetLeft.position;
	wheels.desiredPositionRight = magnetRight.position;
	wheels.desiredPositionBack = magnetBack.position;
	wheels.stop();
}

void pids_loop() {
	pidAction.check();
}

void pidCompute() {
	long now = millis();
	long dt = now - timePrevious;
	timePrevious = now;

	wheels.update(dt);

	pidLeft.setInput(magnetLeft.position);
	pidRight.setInput(magnetRight.position);
	pidBack.setInput(magnetBack.position);

	pidLeft.setSetpoint(wheels.desiredPositionLeft);
	pidRight.setSetpoint(wheels.desiredPositionRight);
	pidBack.setSetpoint(wheels.desiredPositionBack);

	pidLeft.compute(dt / 1000.0);
	pidRight.compute(dt / 1000.0);
	pidBack.compute(dt / 1000.0);

	motorLeft.setSpeedWithDirection(pidLeft.output);
	motorRight.setSpeedWithDirection(pidRight.output);
	motorBack.setSpeedWithDirection(pidBack.output);

#ifdef PID_DEBUG
	Serial.print("right input: ");
	Serial.print(magnetRight.position);
	Serial.print(", setpoint: ");
	Serial.print(wheels.desiredPositionRight);
	Serial.print(", output: ");
	Serial.println(pidRight.output);
#endif
}