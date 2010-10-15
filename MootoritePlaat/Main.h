#pragma once
#ifndef __AVR_ATmega324P__
#define __AVR_ATmega324P__ // this must be defined
#endif

#define F_CPU 20000000 // ATMega324P is a 20 MHz CPU
 
#include <stdlib.h>
#include <avr/io.h>
#include <util/delay.h>
 
#include <avr/interrupt.h>
#include "addr.h"
#include "bitwise.h"
#include "motors.h"


void blink(int a);

