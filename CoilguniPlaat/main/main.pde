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

void setup() {
	pinMode(plunger, OUTPUT);
	digitalWrite(plunger, HIGH);
}

void loop() {
	
}