using System;

namespace Robin.Arduino
{
	public class ArduinoCommander
	{
		private readonly ArduinoSerial arduino;

		public ArduinoCommander(ArduinoSerial arduino)
		{
			this.arduino = arduino;
		}

		public void FireCoilgun(byte power)
		{
			arduino.Command(ArduinoPrefix.CoilgunFire, power);
		}

		public void Move(short direction, short speed)
		{
			arduino.Command(ArduinoPrefix.Move, direction, speed);
		}

		public void Turn(short speed)
		{
			arduino.Command(ArduinoPrefix.Turn, speed);
		}
		
		public void Stop()
		{
			arduino.Command(ArduinoPrefix.Stop);
		}

		public void SetDribbler(bool enabled)
		{
			arduino.Command(ArduinoPrefix.SetDribbler, enabled);
		}

		public void MoveAndTurn(short direction, short moveSpeed, short turnSpeed)
		{
			arduino.Command(ArduinoPrefix.MoveAndTurn, direction, moveSpeed, turnSpeed);
		}

		public void SetIrChannel(byte channel)
		{
			arduino.Command(ArduinoPrefix.SetIrChannel, channel);
		}

		public void SetState(byte state)
		{
			arduino.Command(ArduinoPrefix.SetState, state);
		}

		public ArduinoSerial ArduinoSerial
		{
			get { return arduino; }
		}
	}
}