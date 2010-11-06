using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace Robin.Arduino
{
	public class ArduinoSerial : INotifyPropertyChanged
	{
		private const int BaudRate = 57600;
		private readonly SerialPort _port = new SerialPort();
		private string _portName;
		private readonly int _baudRate;

		public event EventHandler<ArduinoDataReceivedEventArgs> DataReceived;

		public ArduinoSerial(string portName = null, int baudRate = BaudRate)
		{
			_portName = portName;
			_baudRate = baudRate;
			_port.DataReceived += (sender, args) => OnDataReceived(new ArduinoDataReceivedEventArgs(_port.ReadExisting()));
		}

		public bool IsOpen
		{
			get { return _port.IsOpen; }
		}

		public void Open()
		{
			if (TryOpenPort(_portName))
				return;

			var names = SerialPort.GetPortNames();
			if (names.Any(TryOpenPort))
				return;
		}

		public void Open(string portName)
		{
			_portName = portName;
			Open();
		}

		public void Close()
		{
			if (_port != null && _port.IsOpen) { 
				_port.Close();
				OnPropertyChanged(new PropertyChangedEventArgs("IsOpen"));
			}
		}

		private bool TryOpenPort(string name)
		{
			try
			{
				_port.PortName = name;
				_port.BaudRate = _baudRate;
				_port.NewLine = "\n";
				_port.DtrEnable = true;
				_port.ReadTimeout = 2000;
				_port.WriteTimeout = 2000;
				_port.Open();
				OnPropertyChanged(new PropertyChangedEventArgs("IsOpen"));
				return _port.IsOpen;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public void Command(string command, params object[] parameters)
		{
			var cmdBytes = Encoding.ASCII.GetBytes(command);

			var byteList = new List<byte>();
			byteList.AddRange(cmdBytes);

			foreach (var parameter in parameters)
			{
				if (parameter.GetType() == typeof(int))
					byteList.AddRange(BitConverter.GetBytes((int)parameter));
				else if (parameter.GetType() == typeof(bool))
					byteList.AddRange(BitConverter.GetBytes((bool)parameter));
				else if (parameter.GetType() == typeof(byte))
					byteList.Add((byte)parameter);
			}

			_port.Write(byteList.ToArray(), 0, byteList.Count);
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

		protected void OnDataReceived(ArduinoDataReceivedEventArgs eventArgs)
		{
			var temp = DataReceived;
			if (temp != null)
				temp(this, eventArgs);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, e);
		}
	}
}