#include "motor_logic.h"


void motor_logic_setup(){
  pinMode(MOTOR_MODE, OUTPUT);
  digitalWrite(MOTOR_MODE, HIGH);
  for(int i = 0; i < 4; i++){ //All motor dir pins to output
    pinMode(motors_dir[i], OUTPUT);
    digitalWrite(motors_dir[i], motors_dir_values[i]);
  }
  //digitalWrite(MOTOR_LEFT_DIR, LOW);
  digitalWrite(MOTOR_UP_DIR, LOW); //Set TOP motor direction
  analogWrite(MOTOR_UP_PWM, MAX_PWM); //Init TOP motor with max PWM
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

  digitalWrite(MOTOR_LEFT_DIR, left >= 0 ? LOW : HIGH);
  digitalWrite(MOTOR_RIGHT_DIR, right >= 0 ? LOW : HIGH);
  digitalWrite(MOTOR_BACK_DIR, back >= 0 ? LOW : HIGH);
  
  analogWrite(MOTOR_LEFT_PWM, abs(left));
  analogWrite(MOTOR_RIGHT_PWM, abs(right));
  analogWrite(MOTOR_BACK_PWM, abs(back));
}

float degreesToRadians(int degrees) {
  return M_PI * degrees / 180;
}
