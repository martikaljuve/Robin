using System;
using System.Text;

namespace Robin.Arduino
{
	public class ArduinoSerialDataEventArgs : EventArgs
	{
		public ArduinoSerialDataEventArgs(byte[] data)
		{
			Bytes = data;
			Data = Encoding.ASCII.GetString(data);
		}

		public ArduinoSerialDataEventArgs(string data)
		{
			Data = data;
			Bytes = Encoding.ASCII.GetBytes(data);
		}

		public byte[] Bytes { get; private set; }
		public string Data { get; private set; }
	}
}