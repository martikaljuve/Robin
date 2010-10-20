#pragma once
#include "WProgram.h"
#include "pins.h"

#define MAX_PWM 255 //Maximum PWM we can send to driver
#define MIN_PWM 50 //Minimum PWM to turn the motor (Change this, when the motors are attached to robot!!)
#define STEP_PWM 8 //PWM step (value we increase/decrease by)
#define RADIUS 1

//Arrays for motor pwm, dir pins and dir values
static int motors_pwm[] = {MOTOR_LEFT_PWM, MOTOR_RIGHT_PWM, MOTOR_BACK_PWM, MOTOR_UP_PWM};
static int motors_dir[] = {MOTOR_LEFT_DIR, MOTOR_RIGHT_DIR, MOTOR_BACK_DIR, MOTOR_UP_DIR};
static int motors_dir_values[] = {HIGH, HIGH, HIGH, HIGH};

void motor_logic_setup();

void moveAndTurn(int direction, int moveSpeed, int turnSpeed);

void stop();

void setSpeed(int left, int right, int back);

float degreesToRadians(int degrees);
