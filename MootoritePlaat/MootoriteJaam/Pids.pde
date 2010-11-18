TimedAction pidAction = TimedAction(200, pidCompute);

long timePrevious = 0;

void pids_setup() {
	//pidLeft.setOutputLimits(-255, 255);
	//pidRight.setOutputLimits(-255, 255);
	//pidBack.setOutputLimits(-255, 255);
}

void pids_loop() {
	pidAction.check();
}

void pidCompute() {
	long now = millis();
	long dt = now - timePrevious;
	timePrevious = now;

	pidLeft.setInput(magnetLeft.average);
	pidRight.setInput(magnetRight.average);
	pidBack.setInput(magnetBack.average);

	pidLeft.compute(dt / 1000.0);
	pidRight.compute(dt / 1000.0);
	pidBack.compute(dt / 1000.0);
	
	//if (pidLeft.setpoint != 0)
	motorLeft.setSpeedWithDirection(pidLeft.output);
	//else
	//	motorLeft.stop();

	//if (pidRight.setpoint != 0)
	motorRight.setSpeedWithDirection(pidRight.output);
	//else
	//	motorRight.stop();

	//if (pidBack.setpoint != 0)
	motorBack.setSpeedWithDirection(pidBack.output);
	//else
	//	motorBack.stop();
}