//#include <EEPROM.h>
#include <IRremote.h>
#include <TimedAction.h>


#include "pins.h"

TimedAction outputAction = TimedAction(200, outputCheck);

void setup(){
  Serial.begin(57600);
  beaconIrSetup();
}

void loop(){
  beaconIrLoop();
  outputAction.check();
}

void outputCheck(){
  Serial.print(getLeftResult());
  Serial.print("\t");
  Serial.print(getRightResult());  
  Serial.print("\t");  
  Serial.print(getLeftTimer());
  Serial.print("\t");
  Serial.print(getRightTimer());    
  Serial.println();
  
}
