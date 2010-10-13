using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using Robin.ControlPanel.Properties;

namespace Robin.ControlPanel
{
	public partial class MainForm : Form
	{
		private readonly BackgroundWorker _logicWorker;
		private readonly BackgroundWorker _sensorWorker;
		public static ArduinoSensorData SensorData { get; set; }

		[Import]
		public IProcessor Processor { get; set; }

		public MainForm()
		{
			InitializeComponent();

			InitializeUserControls();

			_sensorWorker = new BackgroundWorker();
			_sensorWorker.DoWork += SensorWorkerOnDoWork;
			_sensorWorker.RunWorkerAsync();

			_logicWorker = new BackgroundWorker();
			_logicWorker.DoWork += LogicWorkerOnDoWork;
			_logicWorker.RunWorkerAsync();


		}

		private void InitializeUserControls()
		{
			var connectCommand = new ConnectCommand(new ArduinoSerial());
			uxPortConnect.DataBindings.Add("Text", connectCommand, "DisplayName");
			uxPortConnect.DataBindings.Add("Enabled", connectCommand, "Enabled");
			uxPortConnect.Click += (sender, args) => connectCommand.Execute();

			var ports = SerialPort.GetPortNames();
			Array.Sort(ports);

			uxPorts.Items.Add("");
			foreach (var portName in ports)
				uxPorts.Items.Add(portName);

			uxPorts.Items.Add(Settings.Default.ArduinoSerialPort);
			uxPorts.SelectedText = Settings.Default.ArduinoSerialPort;

			uxPorts.SelectedIndexChanged +=
				(o, eventArgs) =>
				{
					connectCommand.Enabled = (uxPorts.SelectedIndex != 0);
					Settings.Default.ArduinoSerialPort = uxPorts.SelectedText;
					Settings.Default.Save();
				};

			uxPortConnect.Click += UxPortConnectOnClick;
		}

		private void UxPortConnectOnClick(object sender, EventArgs eventArgs)
		{
			var serial = new ArduinoSerial(uxPorts.SelectedText);
			serial.Open();
		}

		private void SensorWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
		{
			
		}

		private void LogicWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
		{
			Processor.Update(SensorData);
		}

		private static void LogicWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			
		}
	}

	public abstract class Command : INotifyPropertyChanged
	{
		private bool _enabled;
		private string _displayName;

		public event PropertyChangedEventHandler PropertyChanged;

		public string Key { get; protected set; }
		public string DisplayName {
			get { return _displayName; }
			protected set {
				if (_displayName == value) return;
				_displayName = value;
				OnPropertyChanged(new PropertyChangedEventArgs("DisplayName"));
			}
		}

		public bool Enabled {
			get { return _enabled; }
			set {
				if (_enabled == value) return;
				_enabled = value;

				OnPropertyChanged(new PropertyChangedEventArgs("Enabled"));
			}
		}

		public abstract void Execute();

		protected void OnPropertyChanged(PropertyChangedEventArgs e) {
			PropertyChangedEventHandler handler = PropertyChanged;

			if (handler != null)
				handler(this, e);
		}
	}

	public class ActionCommand<T> : Command {
		private readonly Func<T> _function;

		public ActionCommand(Func<T> func)
		{
			_function = func;
		}

		public override void Execute() { _function(); }
	}

	public class ConnectCommand : Command
	{
		private readonly ArduinoSerial _serial;

		public ConnectCommand(ArduinoSerial serial)
		{
			_serial = serial;
			DisplayName = "Connect";
		}

		public override void Execute()
		{
			if (_serial.IsOpen) {
				_serial.Close();
				DisplayName = "Connect";
			}
			else
			{
				_serial.Open();
				DisplayName = "Disconnect";
			}
		}
	}
}
