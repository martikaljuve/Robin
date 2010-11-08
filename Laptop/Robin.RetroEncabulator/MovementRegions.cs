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

			var topLeft = new Rectangle(0, 0, edgeWidth-1, topHeight-1);
			var topCenterLeft = new Rectangle(edgeWidth, 0, centerEdgeWidth-1, topHeight-1);
			var topCenter = new Rectangle(edgeWidth + centerEdgeWidth, 0, centerWidth-1, topHeight-1);
			var topCenterRight = new Rectangle(size.Width - (edgeWidth + centerEdgeWidth), 0, centerEdgeWidth-1, topHeight-1);
			var topRight = new Rectangle(size.Width - edgeWidth, 0, edgeWidth, topHeight-1);

			var bottomLeft = new Rectangle(0, 0, edgeWidth - 1, bottomHeight);
			var bottomCenterLeft = new Rectangle(edgeWidth, 0, centerEdgeWidth - 1, bottomHeight);
			var bottomCenter = new Rectangle(edgeWidth + centerEdgeWidth, 0, centerWidth - 1, bottomHeight);
			var bottomCenterRight = new Rectangle(size.Width - (edgeWidth + centerEdgeWidth), 0, centerEdgeWidth - 1, bottomHeight);
			var bottomRight = new Rectangle(size.Width - edgeWidth, 0, edgeWidth, bottomHeight);

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
	}
}