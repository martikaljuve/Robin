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
			if (port == null || !port.IsOpen)
				return;

			port.Close();
			OnPropertyChanged(new PropertyChangedEventArgs("IsOpen"));
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
			if (!port.IsOpen)
				return;

			var currentCommand = command + string.Join("", parameters);

			if (currentCommand == previousCommand /*&& arduinoPrefix.NonRepeatableCommands.Contains(command)*/) {
				previousCommand = currentCommand;
				return;
			}

			previousCommand = currentCommand;
			
			var cmdBytes = Encoding.ASCII.GetBytes(command);

			var byteList = new List<byte>();
			byteList.AddRange(cmdBytes);

			foreach (var parameter in parameters) {
				if (parameter is bool) {
					var bytes = BitConverter.GetBytes((bool)parameter);
					byteList.AddRange(bytes);
				}
				else if (parameter is byte)
					byteList.Add((byte)parameter);
				else {
					var bytes = BitConverter.GetBytes((short)parameter);
					if (!BitConverter.IsLittleEndian)
						bytes = bytes.Reverse().ToArray();
					byteList.AddRange(bytes);
				}
			}

			// Fill with 0's
			while (byteList.Count < 7)
				byteList.Add(0);

			//byteList.Add((byte)'\n');

			if (!WriteLine(byteList))
				return;

			OnDataSent(new ArduinoSerialDataEventArgs(command + ": " + string.Join(", ", parameters)));
		}

		private bool WriteLine(IEnumerable<byte> bytes)
		{
			try
			{
				var byteArray = bytes.ToArray();
				port.Write(byteArray, 0, byteArray.Length);
				port.WriteLine(string.Empty);
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