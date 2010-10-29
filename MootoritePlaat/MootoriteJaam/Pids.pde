#include "CPid.h"

TimedAction pidAction = TimedAction(100, pidCompute);

Pid pidLeft;
Pid pidRight;
Pid pidBack;

void pids_setup() {
	pidLeft = Pid(3, 5, 0);
	pidRight = Pid(3, 5, 0);
	pidBack = Pid(3, 5, 0);
}

void pids_loop() {
	pidAction.check();
}

void pidCompute() {
	pidLeft.setInput(magnetLeft.speed);
	pidRight.setInput(magnetRight.speed);
	pidBack.setInput(magnetBack.speed);
	
	pidLeft.compute();
	pidRight.compute();
	pidBack.compute();
	
	motorLeft.setSpeed(pidLeft.output);
	motorRight.setSpeed(pidRight.output);
	motorBack.setSpeed(pidBack.output);
}
