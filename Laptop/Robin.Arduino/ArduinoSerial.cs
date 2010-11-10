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
		private readonly SerialPort port = new SerialPort();
		private string portName;
		private readonly int baudRate;
		private string previousCommand;

		public event EventHandler<ArduinoSerialDataEventArgs> DataReceived;
		public event EventHandler<ArduinoSerialDataEventArgs> DataSent;

		public ArduinoSerial(string portName = null, int baudRate = BaudRate)
		{
			this.portName = portName;
			this.baudRate = baudRate;
			port.DataReceived += (sender, args) => OnDataReceived(new ArduinoSerialDataEventArgs(port.ReadExisting()));
		}

		public bool IsOpen
		{
			get { return port.IsOpen; }
		}

		public void Open()
		{
			if (TryOpenPort(portName))
				return;

			var names = SerialPort.GetPortNames();
			if (names.Any(TryOpenPort))
				return;
		}

		public void Open(string portName)
		{
			this.portName = portName;
			Open();
		}

		public void Close()
		{
			if (port != null && port.IsOpen) { 
				port.Close();
				OnPropertyChanged(new PropertyChangedEventArgs("IsOpen"));
			}
		}

		private bool TryOpenPort(string name)
		{
			try
			{
				port.PortName = name;
				port.BaudRate = baudRate;
				port.NewLine = "\n";
				port.DtrEnable = true;
				port.ReadTimeout = 2000;
				port.WriteTimeout = 2000;
				port.Open();
				OnPropertyChanged(new PropertyChangedEventArgs("IsOpen"));
				return port.IsOpen;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public void Command(string command, params object[] parameters)
		{
			var currentCommand = command + string.Join("", parameters);

			if (currentCommand == previousCommand && ArduinoPrefix.NonRepeatableCommands.Contains(command)) {
				previousCommand = currentCommand;
				return;
			}

			previousCommand = currentCommand;

			var cmdBytes = Encoding.ASCII.GetBytes(command);

			var byteList = new List<byte>();
			byteList.AddRange(cmdBytes);

			foreach (var parameter in parameters)
			{
				if (parameter.GetType() == typeof(short))
					byteList.AddRange(BitConverter.GetBytes((short)parameter));
				else if (parameter.GetType() == typeof(bool))
					byteList.AddRange(BitConverter.GetBytes((bool)parameter));
				else if (parameter.GetType() == typeof(byte))
					byteList.Add((byte)parameter);
			}

			byteList.Add((byte)'.');

			if (!Write(byteList.ToArray(), 0, byteList.Count))
				return;

			OnDataSent(new ArduinoSerialDataEventArgs(command + ": " + string.Join(", ", parameters)));
		}

		private bool Write(byte[] buffer, int offset, int count)
		{
			if (!port.IsOpen)
				return false;

			try
			{
				port.Write(buffer, offset, count);
				port.WriteLine("");
				return true;
			}
			catch (TimeoutException)
			{
				return false;
			}
		}

		protected void OnDataReceived(ArduinoSerialDataEventArgs eventArgs)
		{
			var temp = DataReceived;
			if (temp != null)
				temp(this, eventArgs);
		}

		protected void OnDataSent(ArduinoSerialDataEventArgs eventArgs)
		{
			var temp = DataSent;
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