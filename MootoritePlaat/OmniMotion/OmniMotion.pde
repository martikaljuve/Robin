#include "VSPDE.h"
#include <MLX90316.h>

#include "ArduinoPins.h"
#include "AxisData.h"
#include "CMagnetSensor.h"
#include "CHolonomicWheels.h"

Motor motorLeft = Motor(MOTOR_LEFT_PWM, MOTOR_LEFT_DIR);
Motor motorRight = Motor(MOTOR_RIGHT_PWM, MOTOR_RIGHT_DIR);
Motor motorBack = Motor(MOTOR_BACK_PWM, MOTOR_BACK_DIR);

MagnetSensor magnetLeft = MagnetSensor(MAGNET_SS_LEFT, SCK, MISO);
MagnetSensor magnetRight = MagnetSensor(MAGNET_SS_RIGHT, SCK, MISO);
MagnetSensor magnetBack = MagnetSensor(MAGNET_SS_BACK, SCK, MISO);

HolonomicWheels wheels = HolonomicWheels(motorLeft, motorRight, motorBack, magnetLeft, magnetRight, magnetBack);

void setup() {
	wheels.SetPidGains(100, 0, 0);
	wheels.Hold(X, true);
}

void loop() {
	wheels.Update();
	delay(5);
}