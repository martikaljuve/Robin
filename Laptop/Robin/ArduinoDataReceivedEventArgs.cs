using System;

namespace Robin
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