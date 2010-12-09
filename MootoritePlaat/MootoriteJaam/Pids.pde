//#define PID_DEBUG
//#define KIN_DEBUG

TimedAction positionUpdateAction = TimedAction(50, positionUpdate);
TimedAction pidAction = TimedAction(100, pidCompute);
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
	positionUpdateAction.check();
#ifdef KIN_DEBUG
	debugAction.check();
#endif
}

void positionUpdate(){

	long left = magnetLeft.getCurrentDelta();
	long right = magnetRight.getCurrentDelta();
	long back = magnetBack.getCurrentDelta();

	magnetLeft.resetCurrentDelta();
	magnetRight.resetCurrentDelta();
	magnetBack.resetCurrentDelta();

	wheels.updateGlobalPosition(left, right, back, gyro.getCurrentAngle());
}

void pidCompute() {
	long now = millis();
	long dt = now - timePrevious;
	timePrevious = now;

	wheels.getDesiredWheelPositions(desiredLeft, desiredRight, desiredBack);

	if (abs(desiredLeft) > 5000 || abs(desiredRight) > 5000 || abs(desiredBack) > 5000) {
		int maxSpeed = max(abs(desiredLeft), max(abs(desiredRight), abs(desiredBack)));

		leftSpeed = desiredLeft * 5000 / maxSpeed;
		rightSpeed = desiredRight * 5000 / maxSpeed;
		backSpeed = desiredBack * 5000 / maxSpeed;
	}
	else {
		leftSpeed = desiredLeft;
		rightSpeed = desiredRight;
		backSpeed = desiredBack;
	}

	pidLeft.setSetpoint(map(leftSpeed, -5000, 5000, -255, 255));
	pidRight.setSetpoint(map(rightSpeed, -5000, 5000, -255, 255));
	pidBack.setSetpoint(map(backSpeed, -5000, 5000, -255, 255));

	pidLeft.compute(dt / 1000.0);
	pidRight.compute(dt / 1000.0);
	pidBack.compute(dt / 1000.0);

	motorLeft.setSpeedWithDirection(pidLeft.output);
	motorRight.setSpeedWithDirection(pidRight.output);
	motorBack.setSpeedWithDirection(pidBack.output);
}

#ifdef KIN_DEBUG
double previousX, previousY;
int previousTheta;

void pidDebug() {
	if (previousX == wheels.worldCurrentX &&
		previousY == wheels.worldCurrentY &&
		previousTheta == wheels.globalCurrentTheta)
		return;

	previousX = wheels.worldCurrentX;
	previousY = wheels.worldCurrentY;
	previousTheta = wheels.globalCurrentTheta;

/*
	Serial.print("final: ");
	Serial.print(wheels.worldFinalX / 10.0);
	Serial.print(", ");
	Serial.print(wheels.worldFinalY / 10.0);
	Serial.print(", ");
	Serial.print(wheels.worldFinalTheta / 10.0);*/

	Serial.print("world: ");
	Serial.print(wheels.worldCurrentX / 10.0);
	Serial.print(", ");
	Serial.print(wheels.worldCurrentY / 10.0);
	Serial.print(", ");
	Serial.print(wheels.globalCurrentTheta / 10.0);

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

	Serial.print("\tpid out (L,R,B): ");
	Serial.print(pidLeft.output);
	Serial.print(", ");
	Serial.print(pidRight.output);
	Serial.print(", ");
	Serial.print(pidBack.output);

	Serial.print(",\tgyro: ");
	Serial.println(gyro.getCurrentAngle());
}
#endif
