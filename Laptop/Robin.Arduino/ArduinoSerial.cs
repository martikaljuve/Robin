﻿using System;
using System.IO.Ports;
using System.Linq;

namespace Robin.Arduino
{
	public class ArduinoSerial
	{
		private const int BaudRate = 56600;
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
			if (_port != null && _port.IsOpen)
				_port.Close();
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
				return _port.IsOpen;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public string Query(string command, params  object[] parameters)
		{
			var data = string.Join(" ", new[] { command }.Concat(parameters));
			return Query(data);
		}

		public string Query(string data)
		{
			Write(data);
			return Read();
		}

		public void Command(string command, params object[] parameters)
		{
			var data = string.Join(" ", new[] { command }.Concat(parameters));
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

		public void OnDataReceived(ArduinoDataReceivedEventArgs eventArgs)
		{
			var temp = DataReceived;
			if (temp != null)
				temp(this, eventArgs);
		}

		public static ArduinoSerial Open(string portName, int baudRate)
		{
			var arduino = new ArduinoSerial(portName, baudRate);
			arduino.Open();
			return arduino;
		}
	}
}