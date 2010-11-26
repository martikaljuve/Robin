#pragma once

#include "ArduinoPins.h"
#include "CMagnetSensor.h"
#include "CMotor.h"
#include "CPid.h"
#include "CWheels.h"

#define CCW 1
#define CW 0

Pid pidLeft = Pid(0.03, 0.0, 0.0);
Pid pidRight = Pid(0.03, 0.0, 0.0);
Pid pidBack = Pid(0.03, 0.0, 0.0);

Motor motorLeft = Motor(MOTOR_LEFT_PWM, MOTOR_LEFT_DIR);
Motor motorRight = Motor(MOTOR_RIGHT_PWM, MOTOR_RIGHT_DIR);
Motor motorBack = Motor(MOTOR_BACK_PWM, MOTOR_BACK_DIR);
Motor dribbler = Motor(MOTOR_TOP_PWM, MOTOR_TOP_DIR);

MagnetSensor magnetLeft = MagnetSensor(MAGNET_SS_LEFT, SCK, MISO);
MagnetSensor magnetRight = MagnetSensor(MAGNET_SS_RIGHT, SCK, MISO);
MagnetSensor magnetBack = MagnetSensor(MAGNET_SS_BACK, SCK, MISO);

Wheels wheels;
