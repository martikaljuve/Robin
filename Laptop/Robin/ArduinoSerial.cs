using System;
using System.IO.Ports;
using System.Linq;

namespace Robin
{
	public class ArduinoSerial
	{
		private const int BaudRate = 56600;
		private SerialPort _port;
		private string _portName;
		private readonly int _baudRate;

		//public event EventHandler<ArduinoDataReceivedEventArgs> DataReceived;

		public ArduinoSerial(string portName = null, int baudRate = BaudRate)
		{
			_portName = portName;
			_baudRate = baudRate;
		}

		public void Open()
		{
			if (TryOpenPort(_portName, out _port))
				return;

			var names = SerialPort.GetPortNames();
			if (names.Any(name => TryOpenPort(name, out _port)))
				return;
		}

		public void Open(string portName)
		{
			_portName = portName;
			Open();
		}

		public void Close()
		{
			if (_port != null && _port.IsOpen)
				_port.Close();
		}

		private bool TryOpenPort(string name, out SerialPort port)
		{
			try
			{
				port = new SerialPort(name, _baudRate);
				port.NewLine = "\n";
				port.DtrEnable = true;
				port.ReadTimeout = 2000;
				port.WriteTimeout = 2000;
				//port.DataReceived += (sender, args) => OnDataReceived(new ArduinoDataReceivedEventArgs(_port.ReadExisting()));
				port.Open();
				return port.IsOpen;
			}
			catch (Exception)
			{
				port = null;
				return false;
			}
		}

		public string Query(ArduinoQueries command, params  object[] parameters)
		{
			var data = string.Join(" ", new[] { command.ToString() }.Concat(parameters));
			return Query(data);
		}

		public string Query(string data)
		{
			Write(data);
			return Read();
		}

		public void Command(ArduinoCommands command, params object[] parameters)
		{
			var data = string.Join(" ", new[] { command.ToString() }.Concat(parameters));
			Command(data);
		}

		public void Command(string data)
		{
			Write(data);
		}

		private bool Write(string data)
		{
			try
			{
				_port.WriteLine(data);
				return true;
			}
			catch (TimeoutException)
			{
				return false;
			}
		}

		private string Read()
		{
			try
			{
				return _port.ReadLine();
			}
			catch (TimeoutException)
			{
				return null;
			}
		}

		//public void OnDataReceived(ArduinoDataReceivedEventArgs eventArgs)
		//{
		//    var temp = DataReceived;
		//    if (temp != null)
		//        temp(this, eventArgs);
		//}

		public static ArduinoSerial Open(string portName, int baudRate)
		{
			var arduino = new ArduinoSerial(portName, baudRate);
			arduino.Open();
			return arduino;
		}
	}
}