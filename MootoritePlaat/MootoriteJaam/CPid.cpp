#include <WProgram.h>
#include "CPid.h"

Pid::Pid(double p, double i, double d) {
	inputMin = -500;
	inputMax = 500;

	outputMin = -255;
	outputMax = 255;

	kp = p;
	ki = i;
	kd = d;
}

void Pid::setInput(int newInput) {
	input = newInput;
}

void Pid::setSetpoint(int newSetpoint) {
	setpoint = newSetpoint;
}

void Pid::setInputLimits(int min, int max) {
	inputMin = min;
	inputMax = max;
}

void Pid::setOutputLimits(int min, int max) {
	outputMin = min;
	outputMax = max;
}

void Pid::compute(double dt) {
	int mappedSetpoint = map(setpoint, inputMin, inputMax, outputMin, outputMax);
	int mappedInput = map(input, inputMin, inputMax, outputMin, outputMax);

	int error = mappedSetpoint - mappedInput;
	integral = integral + (error * dt);
	double derivative = (error - errorPrevious) / dt;
	output += (kp * error) + (ki * integral) + (kd * derivative);
	errorPrevious = error;

	output = constrain(output, outputMin, outputMax);
}