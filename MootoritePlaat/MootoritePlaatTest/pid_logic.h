#pragma once
#include <TimedAction.h>
#include <MLX90316.h>
#include "pins.h"
#include <PID_Beta6.h>
#include "motor_logic.h"
#include "mag_sens.h"

void pid_setup(); //Pid setup
void pid_loop(); //Pid loop, that calibrates pwm each turn a bit

//These methods are for PID FrontEnd graph
void SerialReceive();
void SerialSend();
void pidCalc();
