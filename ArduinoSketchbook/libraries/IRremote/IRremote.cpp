/*
 * IRremote
 * Version 0.11 August, 2009
 * Copyright 2009 Ken Shirriff
 * For details, see http://arcfn.com/2009/08/multi-protocol-infrared-remote-library.html
 *
 * Interrupt code based on NECIRrcv by Joe Knapp
 * http://www.arduino.cc/cgi-bin/yabb2/YaBB.pl?num=1210243556
 * Also influenced by http://zovirl.com/2008/11/12/building-a-universal-remote-with-an-arduino/
 */

#include "IRremote.h"

// Provides ISR
#include <avr/interrupt.h>

int IRrecv::receiver_index;
volatile irparams_t* IRrecv::ir_receivers[2];


//volatile irparams_t irparams;

// These versions of MATCH, MATCH_MARK, and MATCH_SPACE are only for debugging.
// To use them, set DEBUG in IRremoteInt.h
// Normally macros are used for efficiency
#ifdef DEBUG

int MATCH(int measured, int desired) {
  Serial.print("Testing: ");
  Serial.print(TICKS_LOW(desired), DEC);
  Serial.print(" <= ");
  Serial.print(measured, DEC);
  Serial.print(" <= ");
  Serial.println(TICKS_HIGH(desired), DEC);
  return measured >= TICKS_LOW(desired) && measured <= TICKS_HIGH(desired);
}

int MATCH_MARK(int measured_ticks, int desired_us) {
  Serial.print("Testing mark ");
  Serial.print(measured_ticks * USECPERTICK, DEC);
  Serial.print(" vs ");
  Serial.print(desired_us, DEC);
  Serial.print(": ");
  Serial.print(TICKS_LOW(desired_us + MARK_EXCESS), DEC);
  Serial.print(" <= ");
  Serial.print(measured_ticks, DEC);
  Serial.print(" <= ");
  Serial.println(TICKS_HIGH(desired_us + MARK_EXCESS), DEC);
  return measured_ticks >= TICKS_LOW(desired_us + MARK_EXCESS) && measured_ticks <= TICKS_HIGH(desired_us + MARK_EXCESS);
}

int MATCH_SPACE(int measured_ticks, int desired_us) {
  Serial.print("Testing space ");
  Serial.print(measured_ticks * USECPERTICK, DEC);
  Serial.print(" vs ");
  Serial.print(desired_us, DEC);
  Serial.print(": ");
  Serial.print(TICKS_LOW(desired_us - MARK_EXCESS), DEC);
  Serial.print(" <= ");
  Serial.print(measured_ticks, DEC);
  Serial.print(" <= ");
  Serial.println(TICKS_HIGH(desired_us - MARK_EXCESS), DEC);
  return measured_ticks >= TICKS_LOW(desired_us - MARK_EXCESS) && measured_ticks <= TICKS_HIGH(desired_us - MARK_EXCESS);
}
#endif

IRrecv::IRrecv(int recvpin)
{
	irparams.recvpin = recvpin;
	irparams.blinkflag = 0;
	ir_receivers[receiver_index++] = &irparams;
}

// initialization
void IRrecv::enableIRIn() {
  // setup pulse clock timer interrupt
  TCCR2A = 0;  // normal mode

  //Prescale /8 (16M/8 = 0.5 microseconds per tick)
  // Therefore, the timer interval can range from 0.5 to 128 microseconds
  // depending on the reset value (255 to 0)
  cbi(TCCR2B,CS22);
  sbi(TCCR2B,CS21);
  cbi(TCCR2B,CS20);

  //Timer2 Overflow Interrupt Enable
  sbi(TIMSK2,TOIE2);

  RESET_TIMER2;

  sei();  // enable interrupts

  // initialize state machine variables
  irparams.rcvstate = STATE_IDLE;
  irparams.rawlen = 0;

  // set pin modes
   //Serial.println(id+" started!");
  pinMode(irparams.recvpin, INPUT);
}

// enable/disable blinking of pin 13 on IR processing
void IRrecv::blink13(int blinkflag)
{
  irparams.blinkflag = blinkflag;
  if (blinkflag)
    pinMode(BLINKLED, OUTPUT);
}


void IRrecv::checkIR(){
	 uint8_t irdata = (uint8_t)digitalRead(irparams.recvpin);
  

	  irparams.timer++; // One more 50us tick
	  if (irparams.rawlen >= RAWBUF) {
		// Buffer overflow
		irparams.rcvstate = STATE_STOP;
	  }
	  switch(irparams.rcvstate) {
	  case STATE_IDLE: // In the middle of a gap
		if (irdata == MARK) {
		  if (irparams.timer < GAP_TICKS) {
			// Not big enough to be a gap.
			irparams.timer = 0;
		  } 
		  else {
			// gap just ended, record duration and start recording transmission
			irparams.rawlen = 0;
			irparams.rawbuf[irparams.rawlen++] = irparams.timer;
			irparams.timer = 0;
			irparams.rcvstate = STATE_MARK;
		  }
		}
		break;
	  case STATE_MARK: // timing MARK
		if (irdata == SPACE) {   // MARK ended, record time
		  irparams.rawbuf[irparams.rawlen++] = irparams.timer;
		  irparams.timer = 0;
		  irparams.rcvstate = STATE_SPACE;
		}
		break;
	  case STATE_SPACE: // timing SPACE
		if (irdata == MARK) { // SPACE just ended, record it
		  irparams.rawbuf[irparams.rawlen++] = irparams.timer;
		  irparams.timer = 0;
		  irparams.rcvstate = STATE_MARK;
		} 
		else { // SPACE
		  if (irparams.timer > GAP_TICKS) {
			// big SPACE, indicates gap between codes
			// Mark current code as ready for processing
			// Switch to STOP
			// Don't reset timer; keep counting space width
			irparams.rcvstate = STATE_STOP;
		  } 
		}
		break;
	  case STATE_STOP: // waiting, measuring gap
		if (irdata == MARK) { // reset gap timer
		  irparams.timer = 0;
		}
		break;
	  }


}

// TIMER2 interrupt code to collect raw data.
// Widths of alternating SPACE, MARK are recorded in rawbuf.
// Recorded in ticks of 50 microseconds.
// rawlen counts the number of entries recorded so far.
// First entry is the SPACE between transmissions.
// As soon as a SPACE gets long, ready is set, state switches to IDLE, timing of SPACE continues.
// As soon as first MARK arrives, gap width is recorded, ready is cleared, and new logging starts

ISR(TIMER2_OVF_vect)
{
  RESET_TIMER2;

  for(int i = 0; i < IRrecv::receiver_index; i++){
	  volatile irparams_t* irparams = IRrecv::ir_receivers[i];
	   //Serial.print(i+": ");
	   //Serial.println(irparams.recvpin[i]);
	  uint8_t irdata = (uint8_t)digitalRead(irparams->recvpin);
  

	  irparams->timer++; // One more 50us tick
	  if (irparams->rawlen >= RAWBUF) {
		// Buffer overflow
		irparams->rcvstate = STATE_STOP;
	  }
	  switch(irparams->rcvstate) {
	  case STATE_IDLE: // In the middle of a gap
		if (irdata == MARK) {
		  if (irparams->timer < GAP_TICKS) {
			// Not big enough to be a gap.
			irparams->timer = 0;
		  } 
		  else {
			// gap just ended, record duration and start recording transmission
			irparams->rawlen = 0;
			irparams->rawbuf[irparams->rawlen++] = irparams->timer;
			irparams->timer = 0;
			irparams->rcvstate = STATE_MARK;
		  }
		}
		break;
	  case STATE_MARK: // timing MARK
		if (irdata == SPACE) {   // MARK ended, record time
		  irparams->rawbuf[irparams->rawlen++] = irparams->timer;
		  irparams->timer = 0;
		  irparams->rcvstate = STATE_SPACE;
		}
		break;
	  case STATE_SPACE: // timing SPACE
		if (irdata == MARK) { // SPACE just ended, record it
		  irparams->rawbuf[irparams->rawlen++] = irparams->timer;
		  irparams->timer = 0;
		  irparams->rcvstate = STATE_MARK;
		} 
		else { // SPACE
		  if (irparams->timer > GAP_TICKS) {
			// big SPACE, indicates gap between codes
			// Mark current code as ready for processing
			// Switch to STOP
			// Don't reset timer; keep counting space width
			irparams->rcvstate = STATE_STOP;
		  } 
		}
		break;
	  case STATE_STOP: // waiting, measuring gap
		if (irdata == MARK) { // reset gap timer
		  irparams->timer = 0;
		}
		break;
	  }

	  if (irparams->blinkflag) {
		if (irdata == MARK) {
		  PORTB |= B00100000;  // turn pin 13 LED on
		} 
		else {
		  PORTB &= B11011111;  // turn pin 13 LED off
		}
	  }
 }
}

void IRrecv::resume() {
  irparams.rcvstate = STATE_IDLE;
  irparams.rawlen = 0;
}



// Decodes the received IR message
// Returns 0 if no data ready, 1 if data ready.
// Results of decoding are stored in results
int IRrecv::decode(decode_results *results) {
  results->rawbuf = irparams.rawbuf;
  results->rawlen = irparams.rawlen;
  if (irparams.rcvstate != STATE_STOP) {
    return ERR;
  }
#ifdef DEBUG
  Serial.println("Attempting RC5 decode");
#endif  
  if (decodeRC5(results)) {
    return DECODED;
  }
  if (results->rawlen >= 6) {
    // Only return raw buffer if at least 6 bits
    results->decode_type = UNKNOWN;
    results->bits = 0;
    results->value = 0;
    return DECODED;
  }
  // Throw away and start over
  resume();
  return ERR;
}

// Gets one undecoded level at a time from the raw buffer.
// The RC5/6 decoding is easier if the data is broken into time intervals.
// E.g. if the buffer has MARK for 2 time intervals and SPACE for 1,
// successive calls to getRClevel will return MARK, MARK, SPACE.
// offset and used are updated to keep track of the current position.
// t1 is the time interval for a single bit in microseconds.
// Returns -1 for error (measured time interval is not a multiple of t1).
int IRrecv::getRClevel(decode_results *results, int *offset, int *used, int t1) {
  if (*offset >= results->rawlen) {
    // After end of recorded buffer, assume SPACE.
    return SPACE;
  }
  int width = results->rawbuf[*offset];
  int val = ((*offset) % 2) ? MARK : SPACE;
  int correction = (val == MARK) ? MARK_EXCESS : - MARK_EXCESS;

  int avail;
  if (MATCH(width, t1 + correction)) {
    avail = 1;
  } 
  else if (MATCH(width, 2*t1 + correction)) {
    avail = 2;
  } 
  else if (MATCH(width, 3*t1 + correction)) {
    avail = 3;
  } 
  else {
    return -1;
  }

  (*used)++;
  if (*used >= avail) {
    *used = 0;
    (*offset)++;
  }
#ifdef DEBUG
  if (val == MARK) {
    Serial.println("MARK");
  } 
  else {
    Serial.println("SPACE");
  }
#endif
  return val;   
}

long IRrecv::decodeRC5(decode_results *results) {
  if (irparams.rawlen < MIN_RC5_SAMPLES + 2) {
    return ERR;
  }
  int offset = 1; // Skip gap space
  long data = 0;
  int used = 0;
  // Get start bits
  if (getRClevel(results, &offset, &used, RC5_T1) != MARK) return ERR;
  if (getRClevel(results, &offset, &used, RC5_T1) != SPACE) return ERR;
  if (getRClevel(results, &offset, &used, RC5_T1) != MARK) return ERR;
  int nbits;
  for (nbits = 0; offset < irparams.rawlen; nbits++) {
    int levelA = getRClevel(results, &offset, &used, RC5_T1); 
    int levelB = getRClevel(results, &offset, &used, RC5_T1);
    if (levelA == SPACE && levelB == MARK) {
      // 1 bit
      data = (data << 1) | 1;
    } 
    else if (levelA == MARK && levelB == SPACE) {
      // zero bit
      data <<= 1;
    } 
    else {
      return ERR;
    } 
  }

  // Success
  results->bits = nbits;
  results->value = data;
  results->decode_type = RC5;
  return DECODED;
}
