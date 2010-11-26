#include <WProgram.h>
#include "SinLookupTable.h"
#include "SinLookupTableInl.h"

int SinLookupTable::getSin(int angleInMilliRadians) {
	int position = angleInMilliRadians;

	position %= LOOKUP_PERIOD;
	if (position < 0)
		position += LOOKUP_PERIOD;

	bool isNegative = false;
	if (position > LOOKUP_HALF_PERIOD) {
		isNegative = true;
		position -= LOOKUP_HALF_PERIOD;
	}

	if (position > LOOKUP_QUARTER_PERIOD && position <= LOOKUP_HALF_PERIOD)
		position = LOOKUP_QUARTER_PERIOD - (position - LOOKUP_QUARTER_PERIOD);

	const prog_int16_t* pointer = &sinLookupTable[position];
	int sin = (int16_t)pgm_read_word(pointer);
	
	return isNegative ? -sin : sin;
}

int SinLookupTable::getCos(int angleInMilliRadians) {
	return getSin(angleInMilliRadians + LOOKUP_QUARTER_PERIOD);
}

int SinLookupTable::getSinFromTenthDegrees(int angle) {
	return getSin(angle * M_PI / 1.8);
}

int SinLookupTable::getCosFromTenthDegrees(int angle) {
	return getCos(angle * M_PI / 1.8);
}