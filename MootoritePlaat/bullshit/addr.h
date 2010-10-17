#define LED PD2		//LED

#define M0_DIR PA2	//DIR_L
#define M0_PWM PB3	//PWM_L
 
#define M1_DIR PA3	//DIR_R
#define M1_PWM PB4	//PWM_R
 
#define M2_DIR PC5	//DIR_B
#define M2_PWM PD7	//PWM_B
 
#define M3_DIR PC4	//DIR_U
#define M3_PWM PD6	//PWM_U


//#define SENSOR_PIN PA0
#define SENSOR_L_PIN PA0 //First
#define SENSOR_R_PIN PA1 //Second
#define SENSOR_U_PIN PC4 //Third
#define SENSOR_B_PIN PC5 //Fourth

/*
#define SENSOR_PORT PORTA
#define SENSOR_DDR DDRA
*/
#define SENSOR_L_PORT PORTA
#define SENSOR_R_PORT PORTA
#define SENSOR_U_PORT PORTC
#define SENSOR_B_PORT PORTC

#define SENSOR_L_DDR DDRA
#define SENSOR_R_DDR DDRA
#define SENSOR_U_DDR DDRC
#define SENSOR_B_DDR DDRC
