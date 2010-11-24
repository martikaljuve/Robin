#ifndef _SINLUT_H
#define SINLUT_H

#define DSL_PERIOD 6283			// full period in milliradians
#define DSL_QUARTER_PERIOD  ((DSL_PERIOD + 2)/4) 
#define DSL_SCALE 10			// results are scaled up by 2^10 = 1024

int SinLut(int angle); // angle in milliradians
int CosLut(int angle); // angle in milliradians

#endif

