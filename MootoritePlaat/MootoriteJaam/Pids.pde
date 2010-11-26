//#define PID_DEBUG
#define KIN_DEBUG

TimedAction pidAction = TimedAction(50, pidCompute);
#ifdef KIN_DEBUG
TimedAction debugAction = TimedAction(500, pidDebug);
#endif

long timePrevious = 0;
int desiredLeft, desiredRight, desiredBack;

void pids_setup() {
	//pidLeft.setOutputLimits(-255, 255);
	//pidRight.setOutputLimits(-255, 255);
	//pidBack.setOutputLimits(-255, 255);

	pidLeft.setInput(0);
	pidRight.setInput(0);
	pidBack.setInput(0);
}

void pids_loop() {
	pidAction.check();
#ifdef KIN_DEBUG
	debugAction.check();
#endif
}

void pidCompute() {
	long now = millis();
	long dt = now - timePrevious;
	timePrevious = now;

	int left = magnetLeft.getCurrentDelta();
	int right = magnetRight.getCurrentDelta();
	int back = magnetBack.getCurrentDelta();

	magnetLeft.resetCurrentDelta();
	magnetRight.resetCurrentDelta();
	magnetBack.resetCurrentDelta();

	wheels.updateGlobalPosition(left, right, back);
	wheels.getDesiredWheelPositions(desiredLeft, desiredRight, desiredBack);

	/*
	pidLeft.setSetpoint(desiredLeft);
	pidRight.setSetpoint(desiredRight);
	pidBack.setSetpoint(desiredBack);

	pidLeft.compute(dt / 1000.0);
	pidRight.compute(dt / 1000.0);
	pidBack.compute(dt / 1000.0);

	motorLeft.setSpeedWithDirection(pidLeft.output);
	motorRight.setSpeedWithDirection(pidRight.output);
	motorBack.setSpeedWithDirection(pidBack.output);
	*/
#ifdef PID_DEBUG
	Serial.print("right input: ");
	Serial.print(magnetRight.position);
	Serial.print(", setpoint: ");
	Serial.print(wheels.desiredPositionRight);
	Serial.print(", output: ");
	Serial.println(pidRight.output);
#endif
}

#ifdef KIN_DEBUG
int previousX, previousY, previousTheta;

void pidDebug() {
	if (previousX == wheels.worldCurrentX &&
		previousY == wheels.worldCurrentY &&
		previousTheta == wheels.worldCurrentTheta)
		return;

	previousX = wheels.worldCurrentX;
	previousY = wheels.worldCurrentY;
	previousTheta = wheels.worldCurrentTheta;

	Serial.print("wheels: ");
	Serial.print(magnetLeft.getPositionTotal() / 10.0);
	Serial.print(", ");
	Serial.print(magnetRight.getPositionTotal() / 10.0);
	Serial.print(", ");
	Serial.print(magnetBack.getPositionTotal() / 10.0);

	Serial.print("\tworld: ");
	Serial.print(wheels.worldCurrentX / 10.0);
	Serial.print(", ");
	Serial.print(wheels.worldCurrentY / 10.0);
	Serial.print(", ");
	Serial.print(wheels.worldCurrentTheta / 10.0);

	Serial.print("\tdiff: ");
	Serial.print(desiredLeft);
	Serial.print(", ");
	Serial.print(desiredRight);
	Serial.print(", ");
	Serial.println(desiredBack);
}
#endif
