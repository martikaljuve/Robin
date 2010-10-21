/*
Author: Raimond Tunnel
17. october 2010
This code speeds motors up to max speed, then slows down to 0, changes direction and repeats the process.
*/
/*
#include "lib/TimedAction/TimedAction.h"
#include "lib/MLX90316/MLX90316.h"
*/
#include <TimedAction.h>
#include <MLX90316.h>
#include "pins.h"
#include "ir_rec.h"
#include "motor_logic.h"
#include "motor_logic_algorithms.h"


void setup() {
  pinMode(LED, OUTPUT);  
  digitalWrite(LED, HIGH);
  Serial.begin(57600); //For serial output
  motor_logic_setup();
  mag_sens_setup();
  
  //frag2ball();
  frag1ball();
  
}

void loop() {
  mag_sens_loop();
  

  //int m[] = {6, 8, 1, 3, 1, 4, 5, 1, 7, 2};
  //Serial.println(find_median(m, 10));
  //delay(500);
}







