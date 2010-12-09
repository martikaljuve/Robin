using System;
using System.Drawing;
using System.Linq;
using System.Timers;
using AForge.Video;
using Emgu.CV;
using Emgu.CV.Structure;
using Robin.Core;

namespace Robin.VideoProcessor
{
	public class MainVideoProcessor
	{
		private readonly VisionResults results;
		private readonly Camshift camshift;
		private readonly LogicState logicState = new LogicState();

		private bool foundBall;
		private int framesPerSecond;
		private bool ballHistogramCalculated;

		public event EventHandler<FrameEventArgs> FrameProcessed;

		public MainVideoProcessor(int camIndex = 0)
		{
			results = new VisionResults();
			camshift = new Camshift();

			Feed = new VideoFeed2(0);
			//Feed = VideoFeed.FromCamIndex(camIndex);
			//Feed = new VideoFeed(VideoFeed.Sample7);

			//var timer = new Timer(1000);
			//timer.Elapsed += (sender, args) => framesPerSecond = Feed.FramesReceived;
			//timer.Start();
		}

		private void VideoSourceOnNewFrame(object sender, NewFrameEventArgs eventArgs)
		{
			ProcessFrame(new Image<Bgr, byte>(eventArgs.Frame));
		}

		private void ProcessFrame(Image<Bgr, byte> frame)
		{
			var result = VisionExperiments.FindCirclesAndLinesWithHough(frame);
			var rect = new Rectangle(Point.Empty, result.Size);

			var circles = VisionExperiments.Circles;
			if (!ballHistogramCalculated && circles.Any())
			{
				//foundBall = true;
				ballHistogramCalculated = true;

				/*using (var frame2 = new Image<Gray, byte>(640, 480, new Gray(255)))
				{
					frame2.ROI = new Rectangle(0, 0, 10, 10);
					camshift.CalculateHistogram(frame2);
					frame2.ROI = Rectangle.Empty;
				}*/

				var circle = circles.First();
				var frameGray = VisionExperiments.FrameGray;
				frameGray.ROI = circle.GetRectangle();
				camshift.CalculateHistogram(frameGray);
				frameGray.ROI = Rectangle.Empty;
			}

			if (!foundBall && circles.Any())
			{
				foreach (var circle in circles)
				{
					var circleRect = circle.GetRectangle();
					if (!rect.Contains(circleRect))
						continue;
					camshift.SetTrackWindow(circleRect);
					foundBall = true;
					break;
				}
			}

			if (foundBall)
			{
				camshift.Track(VisionExperiments.FrameGray);

				var radius = RobinVideoConstants.RadiusFunc(camshift.TrackWindow.X, camshift.TrackWindow.Y);

				// If camshift track window is too large, find a better candidate
				if (camshift.TrackWindow.Width > radius * 4 || camshift.TrackWindow.Height > radius * 4)
					foundBall = false;

				if (!rect.Contains(camshift.TrackWindow))
					foundBall = false;
				else
				{
					camshift.Mask.ROI = camshift.TrackWindow;
					var avg = camshift.Mask.GetAverage();
					camshift.Mask.ROI = Rectangle.Empty;

					if (avg.Intensity < 10)
						foundBall = false;
				}
			}

			if (LogicState != null && LogicState.FindingGoal)
			{
				if (LogicState.GoalRed != null)
				{
					if (LogicState.GoalRed == true)
						results.GoalRectangles = VisionExperiments.FindRedGoalRectangles();
					else
						results.GoalRectangles = VisionExperiments.FindBlueGoalRectangles();
				}
			}

			results.Circles = VisionExperiments.Circles;
			results.TrackingBall = foundBall;
			results.TrackWindow = camshift.TrackWindow;
			results.TrackCenter = camshift.TrackCenter;
			results.Lines = VisionExperiments.Lines;

			OnFrameProcessed(new FrameEventArgs(frame.Bitmap));
		}

		private void OnFrameProcessed(FrameEventArgs eventargs)
		{
			var handler = FrameProcessed;
			if (handler != null)
				handler(this, eventargs);
		}

		public void Stop()
		{
			if (Feed != null)
				Feed.Stop();
		}

		public void Restart()
		{
			if (Feed != null)
				Feed.Restart();
		}

		public LogicState LogicState
		{
			get { return logicState; }
		}

		public VisionResults Results
		{
			get { return results; }
		}

		public int FramesPerSecond
		{
			get { return framesPerSecond; }
		}

		
		/*private VideoFeed feed;
		public VideoFeed Feed
		{
			get { return feed; }
			set
			{
				if (feed != null)
					feed.Stop();

				feed = value;

				if (feed == null)
					return;

				feed.NewFrame += VideoSourceOnNewFrame;
				feed.Start();
			}
		}
		*/ 

		public VideoFeed2 Feed { get; set; }

		public Bitmap Update()
		{
			var frame = Feed.Query();
			if (frame == null)
				return null;

			ProcessFrame(frame);
			return frame.Bitmap;
		}
	}
}