#pragma once
#include <TimedAction.h>
#include <MLX90316.h>
#include "pins.h"

static int const maximum_nr_of_sensors = 4;
static int const active_nr_of_sensors = 3;

static int const nr_of_saved_results = 10; //Number of results we save, until we calculate a median
static int result_values[maximum_nr_of_sensors][nr_of_saved_results]; //Arrays for saving the results
static int real_result[maximum_nr_of_sensors]; //Last result
static long real_result_diff[maximum_nr_of_sensors]; //Last difference

//static int real_speed[maximum_nr_of_sensors]; //Current measured and calculated motor speeds



void mag_sens_setup();

void mag_sens_loop();

void checkAngles();

void calculateSpeed(int sensor_nr);

int find_median(int m[], int n);

int getRealSpeed(int sensor_nr);

 void printArray(int m[]);
