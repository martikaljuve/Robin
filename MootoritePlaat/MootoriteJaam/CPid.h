#pragma once

#include <PID_Beta6.h>

class Pid {
public:
	PID pid;
	double input;
	double output;
	double setpoint;
	
	Pid();
	Pid(double p, double i, double d);
	
	void setInput(double newInput);	
	void setInputRpm(double newRpm);
	void setSetpoint(double newSetpoint);
	double compute();
};