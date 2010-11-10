using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
		private readonly BackgroundWorker _mainLogicWorker = new BackgroundWorker();
		private readonly ArduinoSerial _arduinoSerial = new ArduinoSerial();

		private MainVideoProcessor videoProcessor;
		public MainLogicProcessor MainLogicProcessor { get; set; }
		
		public MainForm()
		{
			InitializeComponent();

			InitializeUiControls();
			InitializeSerialPortControls();
			InitializeMainLogicControls();
			InitializeVisionControls();

			_timer.Start();

			//using (var file = File.CreateText("stateMachine.txt"))
			//    file.WriteLine(MainLogicProcessor.ToDebugString());
		}

		static MainForm()
		{
			regionArrowPen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
		}

		private void InitializeUiControls()
		{
			uxContentPanel.MouseClick += (sender, args) => uxContentPanel.Focus();
			uxContentPanel.MouseEnter += (sender, args) => uxContentPanel.Focus();
		}
		
		private void InitializeVisionControls()
		{
			videoProcessor = new MainVideoProcessor();
			videoProcessor.FrameProcessed += VideoProcessorOnFrameProcessed;
			Application.ApplicationExit += (o1, args1) => videoProcessor.Stop();
			
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
					var data = SensorData.FromSerialData(args.Data);
					Action appendAction = () => uxSerialData.AppendText(data + Environment.NewLine);
					uxSerialData.Invoke(appendAction);
					MainLogicProcessor.SensorData.UpdateFromSerialData(args.Data);
				};

			_arduinoSerial.DataSent +=
				(o, args) =>
				{
					Action appendAction = () => uxSerialSendData.AppendText(args.Data + Environment.NewLine);
					uxSerialSendData.Invoke(appendAction);
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

		private static bool showCircles;
		private static bool showLines;

		private static readonly Pen ellipsePen = new Pen(Color.Fuchsia, 2);
		private static readonly Pen linePen = new Pen(Color.Gold, 2);
		private static readonly Pen camshiftPen = new Pen(Color.Red, 2);
		private static readonly Pen regionPen = new Pen(Color.Coral, 2);
		private static readonly Pen regionArrowPen = new Pen(Color.LightCoral, 8);

		private void VideoProcessorOnFrameProcessed(object sender, FrameEventArgs frameEventArgs)
		{
			var results = videoProcessor.Results;
			MainLogicProcessor.VisionData.UpdateFromVisionResults(results);

			var frame = frameEventArgs.Frame;

			if (!uxPlayer.Visible)
				return;

			DrawDebugInfo(frame, results);
			uxPlayer.Image = frame;
		}

		private void DrawDebugInfo(Bitmap frame, VisionResults results)
		{
			using (var g = Graphics.FromImage(frame))
			{
				/*{
					var thresholdString = string.Format("Threshold: {0}", HoughTransform.CannyThreshold);
					var linkingString = string.Format("Linking: {0}", HoughTransform.CannyThresholdLinking);

					g.FillRectangle(Brushes.White, 5, 5, 100, 50);
					g.DrawString(thresholdString, SystemFonts.DefaultFont, Brushes.Crimson, new PointF(10, 10));
					g.DrawString(linkingString, SystemFonts.DefaultFont, Brushes.Crimson, new PointF(10, 30));
				}*/

				if (showCircles)
					foreach (var circle in results.Circles)
					{
						g.DrawEllipse(ellipsePen, circle.X - circle.Radius, circle.Y - circle.Radius, circle.Radius * 2, circle.Radius * 2);
						g.DrawString(circle.Intensity.ToString(), SystemFonts.DefaultFont, Brushes.Orange, circle.X, circle.Y);
					}

				if (showLines)
					foreach (var line in results.Lines)
						g.DrawLine(linePen, line.P1, line.P2);

				if (results.TrackingBall)
				{
					g.DrawRectangle(camshiftPen, results.TrackWindow);
					g.DrawLine(camshiftPen, results.TrackCenter, Point.Add(results.TrackCenter, new Size(1, 1)));
				}

				foreach (var rectangle in MovementRegions.Regions)
				{
					g.DrawString(rectangle.Key.ToString(), SystemFonts.DefaultFont, Brushes.Khaki, rectangle.Value);
					g.DrawRectangle(regionPen, rectangle.Value);
				}
			}
		}

		private void OnKeyPress(object sender, KeyPressEventArgs keyPressEventArgs)
		{
			var key = keyPressEventArgs.KeyChar;
			if (key == 'p')
				videoProcessor.Restart();
			if (key == 'c')
				showCircles = !showCircles;
			if (key == 'l')
				showLines = !showLines;
			if (key == 'd')
				uxPlayer.Visible = !uxPlayer.Visible;

			VisionExperiments.ProcessKey(key);
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

				MainLogicProcessor.Update();
				_mainLogicWorker.ReportProgress(0, fps);
			}
		}

		private void MainLogicWorkerOnProgressChanged(object sender, ProgressChangedEventArgs progressChangedEventArgs)
		{
			var fps = (float)progressChangedEventArgs.UserState;
			uxLogicFps.Text = string.Format("Main Logic: {0}fps", fps);
			uxVisionFps.Text = string.Format("Vision: {0}fps", videoProcessor.FramesPerSecond);
		}
	}
}
