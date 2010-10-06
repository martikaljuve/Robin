using System;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Rihma.FindGolfBalls
{
	[Export(typeof (IImageProcessor))]
	public class GolfBallFinder : IImageProcessor
	{
		private const double TwoPi = 2*Math.PI;
		private readonly Gray _binaryMaximumValue = new Gray(255);
		private readonly Gray _binaryThreshold = new Gray(100);

		#region IImageProcessor Members

		public string Name
		{
			get { return "Golf Ball Finder"; }
		}

		public void Process(Image<Bgr, byte> sourceImage, ref Image<Bgr, byte> destinationImage)
		{
			using (var storage = new MemStorage())
			{
				Image<Gray, byte> binary = sourceImage.Convert<Gray, byte>().ThresholdBinary(_binaryThreshold, _binaryMaximumValue);
				Contour<Point> contours = binary.FindContours(
					CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_NONE,
					RETR_TYPE.CV_RETR_EXTERNAL,
					storage);

				for (; contours != null; contours = contours.HNext)
				{
					MCvMoments moments = contours.GetMoments();

					//var eccentricity = GetEccentricity(moments);

					double roundness = (contours.Perimeter*contours.Perimeter)/(TwoPi*contours.Area);
					//if (roundness > 20) continue;

					PointF[] points = contours.Select(x => (PointF) x).ToArray();
					CircleF circle = PointCollection.MinEnclosingCircle(points);

					if (roundness < 3)
						destinationImage.Draw(circle, new Bgr(Color.Green), 2);
					else
						destinationImage.Draw(contours, new Bgr(Color.Red), 1);

					//var cross = new Cross2DF(new PointF((float)moments.GravityCenter.x, (float)moments.GravityCenter.y), 10, 10);
					var cross = new Cross2DF(circle.Center, 10, 10);
					//gray.Draw(cross, new Gray(100), 2);
					destinationImage.Draw(cross, new Bgr(Color.Brown), 2);

					//img.Draw(contours.BoundingRectangle, new Bgr(Color.Blue), 1);
					destinationImage.Draw(string.Format("{0:0.0}", roundness), ref EmguHelper.SmallFont,
					                      contours.BoundingRectangle.Location,
					                      new Bgr(Color.WhiteSmoke));

					//gray.Draw(contours.BoundingRectangle, new Gray(70), 3);
					//gray.DrawPolyline(contours.ToArray(), false, new Gray(35), 2);
					//img.DrawPolyline(contours.ToArray(), false, new Bgr(Color.Gray), 2);
				}
			}
		}

		#endregion
	}

//	public class ChessboardFinder
//	{
//		private Matrix<float> _homographyMatrix;
//		public Image<Bgr, byte> FindChessboard(Image<Bgr, byte> img, Image<Gray, byte> gray)
//		{
//			var matrix = FindHomographyMatrix(img, gray);
//
//			if (matrix != null)
//				_homographyMatrix = matrix;
//
//			if (_homographyMatrix == null)
//				return img;
//
//			Image<Bgr, Byte> rotated = img.WarpPerspective(
//				_homographyMatrix,
//				INTER.CV_INTER_LINEAR,
//				WARP.CV_WARP_FILL_OUTLIERS, new Bgr(Color.Black));
//
//			return rotated;
//		}
//
//		private Matrix<float> FindHomographyMatrix(Image<Bgr, byte> img, Image<Gray, byte> gray)
//		{
//			var patternSize = new Size(5, 8);
//			PointF[] corners;
//
//			var patternFound = CameraCalibration.FindChessboardCorners(
//				gray,
//				patternSize,
//				CALIB_CB_TYPE.DEFAULT,
//				out corners);
//
//			gray.FindCornerSubPix(new PointF[][] { corners }, new Size(10, 10), new Size(-1, -1), new MCvTermCriteria(0.05));
//
	//var cornerCount = 0;
	//CvInvoke.cvDrawChessboardCorners(img.Ptr, patternSize, corners, cornerCount, patternFound ? 1 : 0);
	//CameraCalibration.DrawChessboardCorners(gray, patternSize, corners, patternFound);
	//gray.Save("chess2" + DateTime.Now.Ticks + ".jpg");
	//img.Save("chess1" + DateTime.Now.Ticks + (patternFound ? "Yes" : "No") + ".jpg");
//
//			if (!patternFound) return null;
//
//			var objPts = new PointF[4];
//			var imgPts = new PointF[4];
//
//			int width = 5;
//			int height = 8;
//			int wx = 450;
//			int hy = 450;
//			int lx = 400;
//			int ly = 400;
//
//			objPts[0] = new PointF(lx, ly);
//			objPts[1] = new PointF(wx, ly);
//			objPts[2] = new PointF(lx, hy);
//			objPts[3] = new PointF(wx, hy);
//
//			imgPts[0] = corners[0];
//			imgPts[1] = corners[width - 1];
//			imgPts[2] = corners[(height - 1) * width];
//			imgPts[3] = corners[(height - 1) * width + width - 1];
//
//			float[,] src =
//			{
//				{objPts[0].X, objPts[0].Y},
//				{objPts[1].X, objPts[1].Y},
//				{objPts[2].X, objPts[2].Y},
//				{objPts[3].X, objPts[3].Y}
//			};
//
//			float[,] dest =
//			{
//				{imgPts[0].X, imgPts[0].Y},
//				{imgPts[1].X, imgPts[1].Y},
//				{imgPts[2].X, imgPts[2].Y},
//				{imgPts[3].X, imgPts[3].Y}
//			};
//
//			Matrix<float> srcpm = new Matrix<float>(src);
//			Matrix<float> dstpm = new Matrix<float>(dest);
//
//			Matrix<float> homographyMatrix = CameraCalibration.FindHomography(
//				dstpm, //points on the observed image
//				srcpm, //points on the object image
//				HOMOGRAPHY_METHOD.RANSAC,
//				3
//			).Convert<float>();
//
//			return homographyMatrix;
//		}
//	}
}