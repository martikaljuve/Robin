using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Robin.VideoProcessor;

namespace Robin.RetroEncabulator
{
	public static class MovementRegions
	{
		private const double EdgeRatio = 0.3;
		private const double CenterEdgeRatio = 0.1;
		private const double TopHeightRatio = 0.6;
		private static Dictionary<MovementRegion, Rectangle> regions;

		static MovementRegions()
		{
			var size = RobinVideoConstants.Size;
			var edgeWidth = (int)(size.Width*EdgeRatio);
			var centerEdgeWidth = (int)(size.Width*CenterEdgeRatio);
			var centerWidth = size.Width - (2*edgeWidth + 2*centerEdgeWidth);

			var topHeight = (int)(size.Height*TopHeightRatio);
			var bottomHeight = size.Height - topHeight;

			var topLeft = new Rectangle(0, 0, edgeWidth, topHeight);
			var topCenterLeft = new Rectangle(edgeWidth, 0, centerEdgeWidth, topHeight);
			var topCenter = new Rectangle(edgeWidth + centerEdgeWidth, 0, centerWidth, topHeight);
			var topCenterRight = new Rectangle(size.Width - (edgeWidth + centerEdgeWidth), 0, centerEdgeWidth, topHeight);
			var topRight = new Rectangle(size.Width - edgeWidth, 0, edgeWidth, topHeight);

			var bottomLeft = new Rectangle(0, topHeight, edgeWidth, bottomHeight);
			var bottomCenterLeft = new Rectangle(edgeWidth, topHeight, centerEdgeWidth, bottomHeight);
			var bottomCenter = new Rectangle(edgeWidth + centerEdgeWidth, topHeight, centerWidth, bottomHeight);
			var bottomCenterRight = new Rectangle(size.Width - (edgeWidth + centerEdgeWidth), topHeight, centerEdgeWidth, bottomHeight);
			var bottomRight = new Rectangle(size.Width - edgeWidth, topHeight, edgeWidth, bottomHeight);

			regions =
				new Dictionary<MovementRegion, Rectangle>
					{
						{ MovementRegion.TopLeft, topLeft },
						{ MovementRegion.TopCenterLeft, topCenterLeft },
						{ MovementRegion.TopCenter, topCenter },
						{ MovementRegion.TopCenterRight, topCenterRight },
						{ MovementRegion.TopRight, topRight },

						{ MovementRegion.BottomLeft, bottomLeft },
						{ MovementRegion.BottomCenterLeft, bottomCenterLeft },
						{ MovementRegion.BottomCenter, bottomCenter },
						{ MovementRegion.BottomCenterRight, bottomCenterRight },
						{ MovementRegion.BottomRight, bottomRight },
					};
		}

		public static MovementRegion GetRegionFromPoint(Point p)
		{
			return regions.FirstOrDefault(x => x.Value.Contains(p)).Key;
		}

		public static Dictionary<MovementRegion, Rectangle> Regions
		{
			get { return regions; }
		}
	}
}