#include <WProgram.h>
#include <PID_Beta6.h>
#include "CPid.h"

Pid::Pid() { }

Pid::Pid(double p, double i, double d) {
	pid = PID(&input, &output, &setpoint, p, i, d);
	pid.SetMode(AUTO);
}

void Pid::setInput(double newInput) {
	input = newInput;
}

void Pid::setInputRpm(double newRpm) {
	input = map(abs(newRpm), 0, 500, 0, 255);
}

void Pid::setSetpoint(double newSetpoint) {
	setpoint = newSetpoint;
}

double Pid::compute() {
	pid.Compute();
	return output;
}