using System;

namespace Robin.Arduino
{
	public class ArduinoCommander
	{
		private readonly ArduinoSerial _arduino;

		public ArduinoCommander(ArduinoSerial arduino)
		{
			_arduino = arduino;
		}

		public void FireCoilgun()
		{
			_arduino.Command(ArduinoPrefix.CoilgunFire);
		}

		public void FireCoilgunHalf()
		{
			_arduino.Command(ArduinoPrefix.CoilgunFireHalf);
		}

		public void Move(float direction, int speed)
		{
			_arduino.Command(ArduinoPrefix.Move, direction, speed);
		}

		public void MoveDistance(float direction, int speed, float distance)
		{
			_arduino.Command(ArduinoPrefix.MoveDistance, direction, speed, distance);
		}

		public void Turn(float degrees, int speed)
		{
			_arduino.Command(ArduinoPrefix.Turn, degrees, speed);
		}

		public void Stop()
		{
			_arduino.Command(ArduinoPrefix.Stop);
		}
	}
}