using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Reflection;
using System.Windows.Forms;
using Robin.Arduino;
using Robin.ControlPanel.Properties;
using Robin.Core;
using Robin.VideoProcessor;
using System.Linq;

namespace Robin.ControlPanel
{
	public partial class MainForm : Form
	{
		private readonly Stopwatch timer = new Stopwatch();
		private readonly BackgroundWorker mainLogicWorker = new BackgroundWorker();
		private readonly ArduinoSerial arduinoSerial = new ArduinoSerial();
		private readonly ArduinoCommander arduinoCommander;
		private VideoForm videoForm;

		private IRobotController selectedController;
		private MainVideoProcessor videoProcessor;

		[ImportMany]
		public IEnumerable<Lazy<IRobotController>> RobotControllers { get; set; }
		
		public MainForm()
		{
			InitializeComponent();

			arduinoCommander = new ArduinoCommander(arduinoSerial);

			InitializeUiControls();
			InitializeSerialPortControls();
			InitializeMainLogicControls();
			InitializeVisionControls();

			timer.Start();

			//using (var file = File.CreateText("stateMachine.txt"))
			//    file.WriteLine(RobotController.ToDebugString());
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
			videoProcessor = new MainVideoProcessor(Settings.Default.CamIndex);
			videoProcessor.FrameProcessed += VideoProcessorOnFrameProcessed;
			Application.ApplicationExit +=
				(o1, args1) =>
				{
					mainLogicWorker.CancelAsync();
					videoProcessor.Stop();
					foreach (var controller in RobotControllers)
						controller.Value.Dispose();
				};

			KeyPress += OnKeyPress;

			uxShowVideo.Click += (sender, args) =>
			{
				if (videoForm != null) {
					videoForm.Dispose();
					return;
				}

				videoForm = new VideoForm();
				videoForm.KeyPress += OnKeyPress;
				videoForm.Show(this);
			};
		}

		private void InitializeMainLogicControls()
		{
			var dirCatalog = new DirectoryCatalog("Controllers");
			var assemblyCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
			var aggregateCatalog = new AggregateCatalog(dirCatalog, assemblyCatalog);
			var container = new CompositionContainer(aggregateCatalog);
			container.ComposeParts(this);

			uxControllers.DataSource = RobotControllers;
			uxControllers.DisplayMember = "Name";
			uxControllers.SelectedIndexChanged += (sender, args) => SetSelectedController((IRobotController)uxControllers.SelectedItem);

			if (!string.IsNullOrWhiteSpace(Settings.Default.SelectedController))
			{
				foreach (var controller in RobotControllers)
				{
					if (controller.Value.Name != Settings.Default.SelectedController)
						continue;

					SetSelectedController(controller.Value);
					uxControllers.SelectedItem = controller;
					break;
				}
			}
			else
			{
				SetSelectedController(RobotControllers.Select(x => x.Value).First());
				uxControllers.SelectedItem = selectedController;
			}

			mainLogicWorker.WorkerSupportsCancellation = true;
			mainLogicWorker.DoWork += MainLogicWorkerOnDoWork;
			mainLogicWorker.ProgressChanged += MainLogicWorkerOnProgressChanged;
			mainLogicWorker.WorkerReportsProgress = true;
			mainLogicWorker.RunWorkerAsync();
		}

		private void SetSelectedController(IRobotController controller)
		{
			selectedController = controller;
			selectedController.Commander = arduinoCommander;
			selectedController.Parent = Handle;
		}

		private void InitializeSerialPortControls()
		{
			int index = 0;
			arduinoSerial.DataReceived +=
				(o, args) =>
				{
					var data = ArduinoDataUtil.GetSensorDataFromString(args.Data);
					Action appendAction = () =>
					{
						if (!args.Data.StartsWith("D"))
							uxSerialData.AppendText(index++ + ": " + args.Data);
						else
							uxSerialData.AppendText(index++ + ": " + data + Environment.NewLine);

						selectedController.SensorData = data;
					};
					uxSerialData.Invoke(appendAction);
				};

			arduinoSerial.DataSent +=
				(o, args) =>
				{
					Action appendAction = () => uxSerialSendData.AppendText(args.Data + Environment.NewLine);
					uxSerialSendData.Invoke(appendAction);
				};

			var connectCommand = new ConnectCommand(arduinoSerial);
			uxPortConnect.DataBindings.Add("Text", connectCommand, "DisplayName");
			uxPortConnect.DataBindings.Add("Enabled", connectCommand, "Enabled");
			uxPortConnect.Click += (sender, args) => connectCommand.Execute();

			var portsBinding = new Binding("Enabled", arduinoSerial, "IsOpen");
			portsBinding.Format += (sender, args) => {
				if (args.Value != null)
					args.Value = !(bool)args.Value;
			};
			uxPorts.DataBindings.Add(portsBinding);

			uxIrChannelPanel.DataBindings.Add("Enabled", arduinoSerial, "IsOpen");

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
			selectedController.VisionData = results.ToVisionData();

			var frame = frameEventArgs.Frame;

			if (videoForm == null || !videoForm.Visible)
				return;

			DrawDebugInfo(frame, results);
			videoForm.Frame = frame;
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
				if (timer.IsRunning)
				{
					timerCount++;
					var timeNow = timer.ElapsedMilliseconds;
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

				selectedController.Update();
				mainLogicWorker.ReportProgress(0, fps);
			}
		}

		private void MainLogicWorkerOnProgressChanged(object sender, ProgressChangedEventArgs progressChangedEventArgs)
		{
			var fps = (float)progressChangedEventArgs.UserState;
			uxLogicFps.Text = string.Format("Main Logic: {0:0.00}fps", fps);
			uxVisionFps.Text = string.Format("Vision: {0:0.00}fps", videoProcessor.FramesPerSecond);
		}
	}
}
