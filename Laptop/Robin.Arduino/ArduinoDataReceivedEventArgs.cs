using System;

namespace Robin.Arduino
{
	public class ArduinoDataReceivedEventArgs : EventArgs
	{
		public ArduinoDataReceivedEventArgs(string data)
		{
			Data = data;
		}

		public string Data { get; private set; }
	}
}