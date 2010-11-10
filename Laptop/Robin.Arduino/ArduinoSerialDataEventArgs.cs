using System;

namespace Robin.Arduino
{
	public class ArduinoSerialDataEventArgs : EventArgs
	{
		public ArduinoSerialDataEventArgs(string data)
		{
			Data = data;
		}

		public string Data { get; private set; }
	}
}