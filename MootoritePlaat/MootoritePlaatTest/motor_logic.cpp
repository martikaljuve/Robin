#pragma once
#include "motor_logic.h"


//Arrays for motor pwm, dir pins and dir values
int motors_pwm[] = {MOTOR_LEFT_PWM, MOTOR_RIGHT_PWM, MOTOR_BACK_PWM, MOTOR_UP_PWM};
int motors_pwm_values[] = {0, 0, 0, 0};
int motors_dir[] = {MOTOR_LEFT_DIR, MOTOR_RIGHT_DIR, MOTOR_BACK_DIR, MOTOR_UP_DIR};
int motors_dir_values[] = {HIGH, HIGH, HIGH, HIGH};


void motor_logic_setup(){
  pinMode(MOTOR_MODE, OUTPUT);
  digitalWrite(MOTOR_MODE, HIGH);
  for(int i = 0; i < 4; i++){ //All motor dir pins to output
    pinMode(motors_dir[i], OUTPUT);
    digitalWrite(motors_dir[i], motors_dir_values[i]);
  }
  //digitalWrite(MOTOR_LEFT_DIR, LOW);
  digitalWrite(MOTOR_UP_DIR, LOW);
  setOneSpeed(3, 255);
  /*digitalWrite(MOTOR_UP_DIR, LOW); //Set TOP motor direction
  analogWrite(MOTOR_UP_PWM, MAX_PWM); //Init TOP motor with max PWM
  */
}

/*
Method that sets the nibbler PWM to 0
*/
void stopDribbler(){
	setOneSpeed(3, 0);
}

/*
Method that sets the nibbler PWM to 255
*/
void startDribbler(){
	setOneSpeed(3, 255);
}

/*
We should somehow gracefully set the nibbler PWM to -255 here (that means pwm=255, dir=-1)
*/
void reverseDribbler(){
	//Todo
}

/*
Move and burn!
*/
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


  setOneSpeed(0, left);
  setOneSpeed(1, right);
  setOneSpeed(2, back);
  
}

int getOneSpeed(int motor_nr){
	int pwm = motors_pwm_values[motor_nr];
	/*Serial.print("Motor nr ");
	Serial.print(motor_nr);
	Serial.print(" pwm: ");
	Serial.println(pwm);*/
	if(motors_dir_values[motor_nr] == HIGH){
		return pwm;
	}else{
		return -1*pwm;
	}
}

/*
Function that sets the speed of one motor.
Always use this function to set the speed of a motor (except for PID).
*/
void setOneSpeed(int motor_nr, int speed){
	if(abs(speed) <= 255){
		if(speed >= 0){
			//digitalWrite(motors_dir[motor_nr], LOW);
			motors_dir_values[motor_nr] = HIGH;
		}else{
			//digitalWrite(motors_dir[motor_nr], HIGH);
			motors_dir_values[motor_nr] = LOW;
		}
		//analogWrite(motors_pwm[motor_nr], abs(speed));
		motors_pwm_values[motor_nr] = abs(speed);
	}
}

/*
Function that sets PWM directly for the given motor.
PID uses this function as not to mess up the setpoint = motors_pwm_values
*/
int last_pwm[] = {0,0,0,0};
void setOnePWM(int motor_nr, int pwm){
    if(motors_dir_values[motor_nr] == HIGH){
      analogWrite(motors_pwm[motor_nr], pwm);
      digitalWrite(motors_dir[motor_nr], HIGH);
    }else{
      analogWrite(motors_pwm[motor_nr], pwm);
      digitalWrite(motors_dir[motor_nr], LOW);
    }
}

float degreesToRadians(int degrees) {
  return M_PI * degrees / 180;
}
