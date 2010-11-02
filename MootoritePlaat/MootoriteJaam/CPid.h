#pragma once

class Pid {
	double errorPrevious;
	double integral;
	int inputMin;
	int inputMax;
	int outputMin;
	int outputMax;
public:
	int input;
	int output;
	int setpoint;

	double kp;
	double ki;
	double kd;

	Pid(double p, double i, double d);
	
	void setInputLimits(int min, int max);
	void setOutputLimits(int min, int max);
	void setInput(int newInput);
	void setSetpoint(int newSetpoint);
	void compute(double dt);
};