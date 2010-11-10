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

		public void FireCoilgun(byte power)
		{
			_arduino.Command(ArduinoPrefix.CoilgunFire, power);
		}

		public void Move(int direction, int speed)
		{
			_arduino.Command(ArduinoPrefix.Move, direction, speed);
		}

		public void Turn(int speed)
		{
			_arduino.Command(ArduinoPrefix.Turn, speed);
		}
		
		public void Stop()
		{
			_arduino.Command(ArduinoPrefix.Stop);
		}

		public void SetDribbler(bool enabled)
		{
			_arduino.Command(ArduinoPrefix.SetDribbler, enabled);
		}

		public void MoveAndTurn(int direction, int moveSpeed, int turnSpeed)
		{
			_arduino.Command(ArduinoPrefix.MoveAndTurn, direction, moveSpeed, turnSpeed);
		}

		public void SetIrChannel(byte channel)
		{
			_arduino.Command(ArduinoPrefix.SetIrChannel, channel);
		}

		public void SetState(byte state)
		{
			_arduino.Command(ArduinoPrefix.SetState, state);
		}

		public ArduinoSerial ArduinoSerial
		{
			get { return _arduino; }
		}
	}
}