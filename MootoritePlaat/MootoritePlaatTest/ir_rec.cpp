#include "ir_rec.h"

int const maximum_nr_of_sensors = 4;
int active_nr_of_sensors = 3;

int result;
int i = 0;


int sampleCount[maximum_nr_of_sensors]; //Count the number of positive results from sensors
float avg[maximum_nr_of_sensors]; //Average of the results from sensors

TimedAction angleAction = TimedAction(2, checkAngles); //Timed action for checking angles (requesting results)
TimedAction printAction = TimedAction(1200, debugPrint); //Timed action for outputting data and clearing difference

MLX90316 magnetSensor[maximum_nr_of_sensors]; //Maximum of 4 MLX90316 sensors

int const nr_of_saved_results = 10; //Number of results we save, until we calculate a median
int result_values[maximum_nr_of_sensors][nr_of_saved_results]; //Arrays for saving the results
static int real_result[maximum_nr_of_sensors]; //Last result
static long real_result_diff[maximum_nr_of_sensors]; //Last difference

/*
Function that sets up an active_nr_of_sensors sensors and adds them to magnetSensor array. Should be called before any operations with magnet sensors.
*/
void mag_sens_setup(){
    for(int i = 0; i < active_nr_of_sensors; i++){
        magnetSensor[i] = MLX90316(); 
	magnetSensor[i].attach(mag_sens_ss[i], SCK, MISO);
    }
}


/*
Magnet sensor loop. Checks if angleAction of printAction is needed to be called according to timedAction.
Should be called constantly.
*/
void mag_sens_loop(){
	angleAction.check();
	printAction.check();
}


int result_counter[maximum_nr_of_sensors]; //We count how many results for each sensor we have gotten positive

/*
Function that calls readAngle on each sensor, saves result and after nr_of_saved_results results, calls median finding on the last results.
After that finds a real_result and subtracts if from the last real_result found. This gives us difference between consequetive results.
Saves results in real_result and real_result_diff arrays for each magnet sensor.
*/
void checkAngles(){
    for(int i = 0; i < active_nr_of_sensors; i++){ //Cycle through all sensors
	int resultNew = magnetSensor[i].readAngle(); //Read i-th sensor's angle


	if (resultNew >= 0) { //If angle is fiesable
                
                //This code acts somewhat weirdz
                result_values[i][result_counter[i]] = resultNew; //Save result to array nr_of_saved_results results for each i-th sensor
                
                if(result_counter[i]==nr_of_saved_results-1){ //If the i-th sensor has gotten nr_of_saved_results-1 positive readings
                
                    int new_real_result = find_median(result_values[i], nr_of_saved_results); //Find the median of those readings
                    int diff = new_real_result - real_result[i];
                    if(diff < -1800){
                      real_result_diff[i] += 3600+diff; 
                    }else if(diff > 1800){
                       real_result_diff[i] += diff-3600; 
                    }else{
                      real_result_diff[i] += diff; //Find the difference of that median compared to the last one and sum it
                    }
                    real_result[i] = new_real_result; //Write a new result
                    
                }
               result_counter[i]++;

               /* 
		avg[i] = (avg[i] * sampleCount[i] + resultNew) / (sampleCount[i]+1);
		sampleCount[i]++; */
      
	}else{
            //Serial.print("Error :");

        }
         //
        if(result_counter[i]==nr_of_saved_results){ //If we have exeeded the number of saved results for sensor i
          result_counter[i] = 0; //We clear that value to start counting again
        }
    }

    
}

int find_median(int m[], int n){
  for(int posn = 0; posn < n; posn++){ //Cycle through the array
    int smaller = 0; //smaller elements count
    int larger = 0; //larger elements count
    for(int i = 0; i < n; i++){ //Another cycle over the array
      if(m[i] < m[posn]){ //If element in inner loop is less then element in outer loop
        smaller++; //Increase smaller count
      }else if(m[i] > m[posn]){ //If element in inner loop is more then element in outer loop
        larger++; //Increase larger count
      }
    }
    if(smaller <= n/2 && larger <= n/2){ //If smaller and larger counts were both about equal
      return m[posn];
    }    
  } 
}


void debugPrint() {
	//Serial.print(result/10.0);

        for(int i = 0; i < active_nr_of_sensors; i++){
           Serial.print("\t Motor ");
           Serial.print(i);
           Serial.print(": ");
           Serial.print(real_result[i]); 
           Serial.print(" diff: ");
           Serial.print(real_result_diff[i]);
           real_result_diff[i] = 0;
           

           //Serial.print(result_values[i][0]);            
           //Serial.print([i][0]);            

          
           
           
        }
        Serial.println();

        /*
	if (++i > 10) {
		Serial.print(" => AVERAGE: ");
		Serial.print(avg/10.0);
		Serial.print(" (sample count: ");
		Serial.print(sampleCount);
		Serial.println(")");
		i = 0;
		sampleCount = 0;
		avg = 0;
	}
	else
		Serial.print("\t");
        */
}
