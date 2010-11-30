//#define PID_DEBUG
//#define KIN_DEBUG

TimedAction pidAction = TimedAction(50, pidCompute);
#ifdef KIN_DEBUG
TimedAction debugAction = TimedAction(500, pidDebug);
#endif

long timePrevious = 0;
long desiredLeft, desiredRight, desiredBack;
long leftSpeed, rightSpeed, backSpeed;

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

	long left = magnetLeft.getCurrentDelta();
	long right = magnetRight.getCurrentDelta();
	long back = magnetBack.getCurrentDelta();

	magnetLeft.resetCurrentDelta();
	magnetRight.resetCurrentDelta();
	magnetBack.resetCurrentDelta();

	wheels.updateGlobalPosition(left, right, back);
	wheels.getDesiredWheelPositions(desiredLeft, desiredRight, desiredBack);

	if (abs(desiredLeft) > 500 || abs(desiredRight) > 500 || abs(desiredBack) > 500) {
		int maxSpeed = max(abs(desiredLeft), max(abs(desiredRight), abs(desiredBack)));

		leftSpeed = desiredLeft * 500 / maxSpeed;
		rightSpeed = desiredRight * 500 / maxSpeed;
		backSpeed = desiredBack * 500 / maxSpeed;
	}
	else {
		leftSpeed = desiredLeft;
		rightSpeed = desiredRight;
		backSpeed = desiredBack;
	}

	pidLeft.setSetpoint(leftSpeed);
	pidRight.setSetpoint(rightSpeed);
	pidBack.setSetpoint(backSpeed);

	pidLeft.compute(dt / 1000.0);
	pidRight.compute(dt / 1000.0);
	pidBack.compute(dt / 1000.0);

	motorLeft.setSpeedWithDirection(pidLeft.output);
	motorRight.setSpeedWithDirection(pidRight.output);
	motorBack.setSpeedWithDirection(pidBack.output);
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

	Serial.print("final: ");
	Serial.print(wheels.worldFinalX / 10.0);
	Serial.print(", ");
	Serial.print(wheels.worldFinalY / 10.0);
	Serial.print(", ");
	Serial.print(wheels.worldFinalTheta / 10.0);

	Serial.print("\tworld: ");
	Serial.print(wheels.worldCurrentX / 10.0);
	Serial.print(", ");
	Serial.print(wheels.worldCurrentY / 10.0);
	Serial.print(", ");
	Serial.print(wheels.worldCurrentTheta / 10.0);

	Serial.print("\twheels: ");
	Serial.print(magnetLeft.getPositionTotal() / 10.0);
	Serial.print(", ");
	Serial.print(magnetRight.getPositionTotal() / 10.0);
	Serial.print(", ");
	Serial.print(magnetBack.getPositionTotal() / 10.0);

	Serial.print("\tdiff: ");
	Serial.print(desiredLeft / 10.0);
	Serial.print(", ");
	Serial.print(desiredRight / 10.0);
	Serial.print(", ");
	Serial.print(desiredBack / 10.0);

	Serial.print("\tspeed: ");
	Serial.print(leftSpeed);
	Serial.print(", ");
	Serial.print(rightSpeed);
	Serial.print(", ");
	Serial.print(backSpeed);

	Serial.print(",\tgyro: ");
	Serial.println(gyro.getCurrentAngle());
}
#endif
