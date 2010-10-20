#pragma once
#include <TimedAction.h>
#include <MLX90316.h>
#include "pins.h"

static int mag_sens_ss[] = {SS_MS_LEFT, SS_MS_RIGHT, SS_MS_BACK, SS_MS_TOP}; //Magnet sensors

void mag_sens_setup();

void mag_sens_loop();

void checkAngles();

void debugPrint();

int find_median(int m[], int n);
