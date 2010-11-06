using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
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
		private readonly BackgroundWorker _mainLogicWorker = new BackgroundWorker();
		private readonly ArduinoSerial _arduinoSerial = new ArduinoSerial();

		private VideoFeed _feed;

		private bool dragging;
		private Point startDrag;
		private Rectangle dragRectangle;

		private static ArduinoSensorData SensorData { get; set; }
		private static VisionData VisionData { get; set; }

		[Import]
		public IProcessor MainLogicProcessor { get; set; }

		static MainForm()
		{
			SensorData = new ArduinoSensorData();
		}

		public MainForm()
		{
			InitializeComponent();

			InitializeSerialPortControls();
			InitializeMainLogicControls();
			InitializeVisionControls();

			_timer.Start();

			//using (var file = File.CreateText("stateMachine.txt"))
			//    file.WriteLine(MainLogicProcessor.ToDebugString());
		}
		
		private void InitializeVisionControls()
		{
			_feed = new VideoFeed(VideoFeed.Sample2);
			//_feed = new VideoFeed();
			_feed.FrameProcessed += FeedOnFrameProcessed;
			Application.ApplicationExit += (o1, args1) => _feed.Stop();
			
			uxPlayer.MouseDown += UxFrameOnMouseDown;
			uxPlayer.MouseUp += UxFrameOnMouseUp;
			uxPlayer.MouseMove += UxFrameOnMouseMove;
			KeyPress += OnKeyPress;
		}

		private void InitializeMainLogicControls()
		{
			MainLogicProcessor = new MainLogicProcessor();
			MainLogicProcessor.Commander = new ArduinoCommander(_arduinoSerial);

			_mainLogicWorker.DoWork += MainLogicWorkerOnDoWork;
			_mainLogicWorker.ProgressChanged += MainLogicWorkerOnProgressChanged;
			_mainLogicWorker.WorkerReportsProgress = true;
			_mainLogicWorker.RunWorkerAsync();
		}

		private void InitializeSerialPortControls()
		{
			_arduinoSerial.DataReceived +=
				(o, args) =>
				{
					var data = ArduinoSensorData.FromSerialData(args.Data);
					Action appendAction = () => uxSerialData.AppendText(data + Environment.NewLine);
					uxSerialData.Invoke(appendAction);
					SensorData.UpdateFromSerialData(args.Data);
				};

			var connectCommand = new ConnectCommand(_arduinoSerial);
			uxPortConnect.DataBindings.Add("Text", connectCommand, "DisplayName");
			uxPortConnect.DataBindings.Add("Enabled", connectCommand, "Enabled");
			uxPortConnect.Click += (sender, args) => connectCommand.Execute();

			uxIrChannelPanel.DataBindings.Add("Enabled", _arduinoSerial, "IsOpen");

			var ports = SerialPort.GetPortNames();
			Array.Sort(ports);

			uxPorts.Items.Add("");
			foreach (var portName in ports)
				uxPorts.Items.Add(portName);

			uxPorts.SelectedIndexChanged +=
				(o, eventArgs) =>
					{
						connectCommand.Enabled = (uxPorts.SelectedIndex != 0);
						connectCommand.PortName = uxPorts.SelectedItem.ToString();
						Settings.Default.ArduinoSerialPort = uxPorts.SelectedItem.ToString();
						Settings.Default.Save();
					};

			if (uxPorts.Items.Contains(Settings.Default.ArduinoSerialPort))
				uxPorts.SelectedItem = Settings.Default.ArduinoSerialPort;
		}

		private void FeedOnFrameProcessed(object sender, FrameEventArgs frameEventArgs)
		{
			var frame = frameEventArgs.Frame;

			//if (dragging && (dragRectangle.Width != 0 && dragRectangle.Height != 0))
			//	frame.Draw(dragRectangle, new Bgr(Color.Red), 2);

			uxPlayer.Image = frame;
		}

		private void OnKeyPress(object sender, KeyPressEventArgs keyPressEventArgs)
		{
			if (_feed != null)
				_feed.ProcessKeyCommand(keyPressEventArgs.KeyChar);
			VisionExperiments.ProcessKey(keyPressEventArgs.KeyChar);

			if (keyPressEventArgs.KeyChar == 'p')
				_feed.Restart();
		}

		private void UxFrameOnMouseDown(object sender, MouseEventArgs args)
		{
			if (args.Button != MouseButtons.Left)
			{
				dragging = false;
				return;
			}

			dragging = true;
			startDrag = args.Location;
		}

		private void UxFrameOnMouseUp(object sender, MouseEventArgs args)
		{
			if (args.Button != MouseButtons.Left) return;
			if (!dragging) return;
			
			if (dragRectangle.Width != 0 && dragRectangle.Height != 0)
				_feed.SetRegionOfInterest(dragRectangle);

			dragging = false;
		}

		private void UxFrameOnMouseMove(object sender, MouseEventArgs args)
		{
			if (!dragging) return;

			var end = uxPlayer.PointToClient(MousePosition);

			dragRectangle = Rectangle.FromLTRB(
				Math.Min(startDrag.X, end.X),
				Math.Min(startDrag.Y, end.Y),
				Math.Max(startDrag.X, end.X),
				Math.Max(startDrag.Y, end.Y));
		}
		
		private void MainLogicWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
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

				MainLogicProcessor.ArduinoData = SensorData;
				MainLogicProcessor.VisionData = VisionData;
				MainLogicProcessor.Update();
				_mainLogicWorker.ReportProgress(0, fps);
			}
		}

		private void MainLogicWorkerOnProgressChanged(object sender, ProgressChangedEventArgs progressChangedEventArgs)
		{
			var fps = (float)progressChangedEventArgs.UserState;
			uxFps.Text = fps.ToString();
		}
	}
}
