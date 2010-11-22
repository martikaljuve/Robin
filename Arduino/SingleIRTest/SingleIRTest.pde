//#include <EEPROM.h>
#include <IRremote.h>
#include <TimedAction.h>


#include "pins.h"


void setup(){
  Serial.begin(57600);
  beaconIrSetup();
}

void loop(){
  beaconIrLoop();
}
