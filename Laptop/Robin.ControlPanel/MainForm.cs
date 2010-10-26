using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO.Ports;
using System.Windows.Forms;
using Robin.Arduino;
using Robin.ControlPanel.Properties;
using Robin.RetroEncabulator;
using Robin.VideoProcessor;

namespace Robin.ControlPanel
{
	public partial class MainForm : Form
	{
		private readonly Stopwatch _timer = new Stopwatch();
		private readonly BackgroundWorker _logicWorker = new BackgroundWorker();
		private readonly ArduinoSerial _arduinoSerial = new ArduinoSerial();
		private VideoFeed _feed;

		public static ArduinoSensorData SensorData { get; set; }

		[Import]
		public IProcessor Processor { get; set; }

		static MainForm()
		{
			SensorData = new ArduinoSensorData();
		}

		public MainForm()
		{
			InitializeComponent();

			Processor = new Processor();
			Processor.Commander = new ArduinoCommander(_arduinoSerial);

			InitializeUserControls();
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

			_timer.Start();

			_logicWorker.DoWork += LogicWorkerOnDoWork;
			_logicWorker.ProgressChanged += LogicWorkerOnProgressChanged;
			_logicWorker.WorkerReportsProgress = true;
			_logicWorker.RunWorkerAsync();

			_feed = new VideoFeed();
			Application.Idle += ApplicationOnIdle;
		}

		private void ApplicationOnIdle(object sender, EventArgs eventArgs)
		{
			var frame = _feed.CaptureFrame();
			uxFrame.Image = frame;
		}

		private void UxPortConnectOnClick(object sender, EventArgs eventArgs)
		{
			_arduinoSerial.Open(uxPorts.SelectedText);
			_arduinoSerial.DataReceived += (o, args) => SensorData.UpdateFromSerialData(args.Data);
		}
		
		private void LogicWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
		{
			float fps = 0;
			long timeElapsed = 0;
			long timeLast = 0;
			long timerCount = 0;

			while (true)
			{
				if (_timer.IsRunning)
				{
					timerCount++;
					var timeNow = _timer.ElapsedMilliseconds;
					var timeDelta = timeNow - timeLast;
					timeElapsed += timeDelta;
					timeLast = timeNow;

					if (timeElapsed >= 1000)
					{
						fps = timerCount / (timeElapsed / 1000f);
						timerCount = 0;
						timeElapsed = 0;
					}
				}

				Processor.Update(SensorData);
				_logicWorker.ReportProgress(0, fps);
			}
		}

		private void LogicWorkerOnProgressChanged(object sender, ProgressChangedEventArgs progressChangedEventArgs)
		{
			var fps = (float)progressChangedEventArgs.UserState;
			uxFps.Text = fps.ToString();
		}

		private void uxFilenameBrowse_Click(object sender, EventArgs e)
		{

		}

		private void uxWebcam_Click(object sender, EventArgs e)
		{

		}
	}
}
