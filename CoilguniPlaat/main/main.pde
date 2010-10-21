#define kick2 1 // PD1
#define plunger 2 // PD2
#define done 3 // PD3
#define charge2 4 // PD4

/*
#define ss 10 // PB2
#define mosi 11 // PB3
#define miso 12 // PB4
#define sck 13 // PB5
*/

#define KICK_TIME_MS 10

void setup() {
  pinMode(plunger, INPUT);
  pinMode(done, INPUT);
  
  // interrupt 0 - PD2 / plunger
  // interrupt 1 - PD3 / done
  attachInterrupt(0, fireCoilgun, RISING); // fireCoilgun() is called when input changes from LOW to HIGH
  chargeInterruptOn();
  
  pinMode(kick2, OUTPUT);
  pinMode(charge2, OUTPUT);
  
  digitalWrite(kick2, LOW);
  digitalWrite(charge2, HIGH);
}

void loop() {
  
}

void fireCoilgun() {
  chargeInterruptOff();
  digitalWrite(kick2, HIGH);
  delay(KICK_TIME_MS);
  digitalWrite(kick2, LOW);
  chargeInterruptOn();
}

void chargeDone() {
  int val = digitalRead(done);
  
  delay(100);
  digitalWrite(charge2, val);
}

void chargeInterruptOn() {
  attachInterrupt(1, chargeDone, CHANGE); // chargeDone() is called when input changes LOW->HIGH or HIGH->LOW
}

void chargeInterruptOff() {
  detachInterrupt(1);
}
