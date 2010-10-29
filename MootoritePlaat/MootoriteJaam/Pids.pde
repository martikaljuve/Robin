#include "CPid.h"

TimedAction pidAction = TimedAction(100, pidCompute);

Pid pidLeft = Pid(3, 5, 0);
Pid pidRight = Pid(3, 5, 0);
Pid pidBack = Pid(3, 5, 0);

void pids_setup() {
	
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
