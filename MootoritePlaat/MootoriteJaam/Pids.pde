TimedAction pidAction = TimedAction(20, pidCompute);

void pids_setup() {
	
}

void pids_loop() {
	pidAction.check();
}

void pidCompute() {
	bool pidDelayOff = millis() > wheels.pidDelayEnd;

	if (pidDelayOff) {
		pidLeft.setInputRpm(magnetLeft.average);
		pidRight.setInputRpm(magnetRight.average);
		pidBack.setInputRpm(magnetBack.average);
	}
	else {
		pidLeft.setInput(pidLeft.output);
		pidRight.setInput(pidRight.output);
		pidBack.setInput(pidBack.output);
	}

	pidLeft.compute();
	pidRight.compute();
	pidBack.compute();
	
	if (pidDelayOff) {
		motorLeft.setPwm(pidLeft.output);
		motorRight.setPwm(pidRight.output);
		motorBack.setPwm(pidBack.output);
	}
}
