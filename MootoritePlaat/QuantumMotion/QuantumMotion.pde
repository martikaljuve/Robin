#include <math.h>
#include "pins.h"

#define DEBUG
#define RADIUS 1
//#define PI 3.14159265

void setup() {
  pinMode(LED, OUTPUT);
  
  pinMode(MODE, OUTPUT);
  
  pinMode(LEFT_PWM, OUTPUT);
  pinMode(RIGHT_PWM, OUTPUT);
  pinMode(BACK_PWM, OUTPUT);
  
  pinMode(LEFT_DIR, OUTPUT);
  pinMode(RIGHT_DIR, OUTPUT);
  pinMode(BACK_DIR, OUTPUT);
  
  digitalWrite(MODE, HIGH);
  
  Serial.begin(57600);
  
  analogWrite(FRONT_PWM, 255);
  moveAndTurn(180, 100, 0);
}

void loop() {
  //setSpeed(50, 100, 150);
  //figureCircle();
  figureRectangle();
}

void figureRectangle() {
  stop();
  delay(1000);
  moveAndTurn(0, 255, 0);
  delay(1000);
  
  stop();
  delay(1000);
  moveAndTurn(90, 255, 0);  
  delay(1000);
  
  stop();
  delay(1000);
  moveAndTurn(180, 255, 0);  
  delay(1000);

  stop();
  delay(1000);
  moveAndTurn(270, 255, 0);  
  delay(1000);

  stop();
  delay(1000);
  moveAndTurn(0, 0, 100);
  delay(1000);
}

void figureCircle() {
  for (int i = 0; i < 100; i++) {
    stop();
    delay(1000);
    moveAndTurn(0, 255, 10);
    delay(1000);
  }
}

void moveAndTurn(int direction, int moveSpeed, int turnSpeed) {
  float radians = degreesToRadians(direction);
  float vel_x = sin(radians);
  float vel_y = cos(radians);
  
  int Rw = RADIUS * turnSpeed;
  
  // int velR = -0.5*vx + 0.866*vy + Rw;
  // int velL = -0.5*vx - 0.866*vy + Rw;
  // int velB = vx + Rw;
  
  float tmp1 = (-0.5 * vel_x);
  float tmp2 = 0.866 * vel_y;
  float speed_left = tmp1 - tmp2;
  float speed_right = tmp1 + tmp2 ;
  float speed_back = vel_x;
  
  if (Rw != 0) {
    float rotation = turnSpeed / 255.0;
    speed_left = speed_left + rotation;
    speed_right = speed_right + rotation;
    speed_back = speed_back + rotation;
    
    if (speed_left > 255 || speed_right > 255 || speed_back > 255) {
      float ratio = 1;
      if (speed_left >= speed_right && speed_left >= speed_back) {
        ratio = 255.0 / speed_left;
        speed_left = 255;
        speed_right = speed_right / ratio;
        speed_back = speed_back / ratio;
      }
      else if (speed_right >= speed_left && speed_right >= speed_back) {
        ratio = 255.0 / speed_right;
        speed_left = speed_left / ratio;
        speed_back = speed_back / ratio;
      }
      else {
        ratio = 255.0 / speed_back;
        speed_left = speed_left / ratio;
        speed_right = speed_right / ratio;
      }
    }
  }

  //setSpeed(round(speed_left * moveSpeed), round(speed_right * moveSpeed), round(speed_back * moveSpeed));
  setSpeed(speed_left * moveSpeed, speed_right * moveSpeed, speed_back * moveSpeed);
}

void stop() {
  setSpeed(0, 0, 0);
}

void setSpeed(int left, int right, int back) {
#ifdef DEBUG
  Serial.print("L");
  Serial.print(left);
  Serial.print(", R");
  Serial.print(right);
  Serial.print(", B");
  Serial.print(back);
  Serial.println();
#endif

  digitalWrite(LEFT_DIR, left >= 0 ? LOW : HIGH);
  digitalWrite(RIGHT_DIR, right >= 0 ? LOW : HIGH);
  digitalWrite(BACK_DIR, back >= 0 ? LOW : HIGH);
  
  analogWrite(LEFT_PWM, abs(left));
  analogWrite(RIGHT_PWM, abs(right));
  analogWrite(BACK_PWM, abs(back));
}

float degreesToRadians(int degrees) {
  return M_PI * degrees / 180;
}
