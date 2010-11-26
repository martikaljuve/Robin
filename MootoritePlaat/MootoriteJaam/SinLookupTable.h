#ifndef _SINLUT_H
#define SINLUT_H

#define LOOKUP_PERIOD 6283			// full period in milliradians
#define LOOKUP_QUARTER_PERIOD  ((LOOKUP_PERIOD + 2)/4) 
#define LOOKUP_HALF_PERIOD  ((LOOKUP_PERIOD + 1)/2)
#define LOOKUP_SCALE 10			// results are scaled up by 2^10 = 1024

class SinLookupTable {
public:
	static int getSin(int angleInMilliRadians); // angle in milliradians
	static int getCos(int angleInMilliRadians); // angle in milliradians
	static int getSinFromTenthDegrees(int angle);
	static int getCosFromTenthDegrees(int angle);
};

#endif