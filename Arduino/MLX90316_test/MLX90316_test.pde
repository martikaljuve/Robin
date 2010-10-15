#include <TimedAction.h>
#include <MLX90316.h>

int pinSS = 10; //5
int pinSCK = 13; //3
int pinMOSI = 11; //4
int result;
int i = 0;
int sampleCount = 0;
float avg = 0;

TimedAction angleAction = TimedAction(5, checkAngle);
TimedAction printAction = TimedAction(100, debugPrint);
MLX90316 magnetSensor1  = MLX90316(); 

void setup(){
	Serial.begin(115200);
	magnetSensor1.attach(pinSS, pinSCK, pinMOSI);
}

void loop() {
	angleAction.check();
	printAction.check();
}

void checkAngle() {
	int resultNew = magnetSensor1.readAngle();
	if (resultNew < -5 || resultNew > -1) {
		result = resultNew;
		avg = (avg * sampleCount + result) / (sampleCount+1);
		sampleCount++;
	}
}

void debugPrint() {
	Serial.print(result/10.0);

	if (++i > 10) {
		Serial.print(" => AVERAGE: ");
		Serial.print(avg/10.0);
		Serial.print(" (sample count: ");
		Serial.print(sampleCount);
		Serial.println(")");
		i = 0;
		sampleCount = 0;
		avg = 0;
	}
	else
		Serial.print("\t");
}