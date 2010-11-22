#pragma once

class Pid {
	long errorPrevious;
	double integral;
	int outputMin;
	int outputMax;
public:
	long input;
	long setpoint;
	int output;

	double kp;
	double ki;
	double kd;

	Pid(double p, double i, double d);
	
	void setOutputLimits(int min, int max);
	void setInput(long newInput);
	void setSetpoint(long newSetpoint);
	void compute(double dt);
};