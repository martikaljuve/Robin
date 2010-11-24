#include "SinLookupTable.h"
#include "SinLookupTableInl.h"

int SinLut(int angle) {
	if (angle>DSL_PERIOD)
		angle %= DSL_PERIOD;
	else if (angle<0) {
		if (angle<-DSL_PERIOD)
			angle %= DSL_PERIOD;
		angle += DSL_PERIOD;
	}

	return (int)LutSin[angle];
}

int CosLut(int angle) {
	angle += DSL_QUARTER_PERIOD;
	if (angle>DSL_PERIOD)
		angle %= DSL_PERIOD;
	else if (angle<0) {
		if (angle<-DSL_PERIOD)
			angle %= DSL_PERIOD;
		angle += DSL_PERIOD;
	}

	return (int)LutSin[angle];
}

