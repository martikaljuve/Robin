#include <PID_Beta6.h>

class Pid {
	PID pid;
public:
	double input;
	double output;
	double setpoint;
	
	Pid(double p, double i, double d);
	
	void setInput(double newInput);	
	void setSetpoint(double newSetpoint);
	double compute();
};