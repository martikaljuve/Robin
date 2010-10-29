#pragma once
#include "WProgram.h"
#include "pins.h"
#include "mag_sens.h"

#define MAX_PWM 255 //Maximum PWM we can send to driver
#define MIN_PWM 50 //Minimum PWM to turn the motor (Change this, when the motors are attached to robot!!)
#define STEP_PWM 8 //PWM step (value we increase/decrease by)
#define RADIUS 1

void motor_logic_setup();

void moveAndTurn(int direction, int moveSpeed, int turnSpeed);

void stop();

void setSpeed(int left, int right, int back);

void setOneSpeed(int motor_nr, int speed);

void setOnePWM(int motor_nr, int pwm);

int getOneSpeed(int motor_nr);

float degreesToRadians(int degrees);

void stopDribbler();
void startDribbler();
