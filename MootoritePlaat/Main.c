#include "Main.h"

void blink(int a){
	for(int i = 0; i < a; i++){
		set_output(PORTD, LED);
		_delay_ms(250);
		set_input(PORTD, LED);
		_delay_ms(250);
	}
}


 
int main()
{
	initialize();
 
	int dir = FWD;
 
	set_dir(0, dir);
	set_dir(1, !dir);
	set_dir(2, dir);
	set_dir(3, !dir);

	InitSPI();
	set_input(PORTD, LED);
 
	while(1)
	{
		// stop all
		checkSPI();

		//blink(rx_idx+1);

		_delay_ms(200);

		/*set_all(0);

		set_dir(0, dir);
		set_dir(1, !dir);
		set_dir(2, dir);
		set_dir(3, !dir);
 
		set_all(255);
 
		_delay_ms(500);*/
		//_delay_ms(1500);
		/*if(dir)
			set_output(PORTD, LED);
		else
			set_input(PORTD, LED);
		*/
 
		dir = !dir;


		
	}
 
 
	return 0;
}
