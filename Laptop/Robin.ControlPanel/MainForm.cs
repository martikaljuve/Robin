using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using Emgu.CV;
using Emgu.CV.Structure;
using Robin.Arduino;
using Robin.ControlPanel.Properties;
using Robin.RetroEncabulator;
using Robin.VideoProcessor;
using System.Linq;

namespace Robin.ControlPanel
{
	public partial class MainForm : Form
	{
		private readonly Stopwatch _timer = new Stopwatch();
		private readonly BackgroundWorker _logicWorker = new BackgroundWorker();
		private readonly ArduinoSerial _arduinoSerial = new ArduinoSerial();

		private VideoFeed _feed;

		private bool dragging = false;
		private Point startDrag;
		private Rectangle dragRectangle;

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

			//_feed = new VideoFeed(VideoFeed.Sample2);
			_feed = new VideoFeed();
			_feed.FrameProcessed +=FeedOnFrameProcessed;

			uxFrame.MouseDown += UxFrameOnMouseDown;
			uxFrame.MouseUp += UxFrameOnMouseUp;
			uxFrame.MouseMove += UxFrameOnMouseMove;
			KeyPress += OnKeyPress;

			Application.ApplicationExit += (o1, args1) => _feed.Stop();
		}

		private void FeedOnFrameProcessed(object sender, FrameEventArgs frameEventArgs)
		{
			var frame = frameEventArgs.Frame;

			if (dragging && (dragRectangle.Width != 0 && dragRectangle.Height != 0))
				frame.Draw(dragRectangle, new Bgr(Color.Red), 2);

			uxFrame.Image = frame;
		}

		private void OnKeyPress(object sender, KeyPressEventArgs keyPressEventArgs)
		{
			if (_feed != null)
				_feed.ProcessKeyCommand(keyPressEventArgs.KeyChar);
			VisionExperiments.ProcessKey(keyPressEventArgs.KeyChar);
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

			var end = uxFrame.PointToClient(MousePosition);

			dragRectangle = Rectangle.FromLTRB(
				Math.Min(startDrag.X, end.X),
				Math.Min(startDrag.Y, end.Y),
				Math.Max(startDrag.X, end.X),
				Math.Max(startDrag.Y, end.Y));
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
