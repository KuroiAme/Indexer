using System;
using System.Drawing;

namespace IndexerIOS
{
	public class Spiral
	{
		public Spiral ()
		{
		}



//		PointF drawSpiralPoint (double scale, double revolutions, int centreX, int centreY, object st, int width, int heigh)
//		{
//			throw new NotImplementedException ();
//		}

		public static RectangleF GetModuloRect (RectangleF currentRect, RectangleF outer, int ic)
		{
//			const double scale = 0.01;
//			//const double delta = 1;
//			const int degreesPerIteration = 30;
//			double revolutions = iterationCount * degreesPerIteration;
//			int centreX = (int)center.X;
//			int centreY = (int)center.Y;
//
//			int width = (int)frameSize.Width;
//			int heigh = (int)frameSize.Height;
//
//			return drawSpiralPoint (scale, revolutions, centreX, centreY, st, width, heigh);


			PointF center = IndexerUtils.findcenter (outer);




//			int direction = ic % 4;
//
//			if (direction == 0) { // NEGATIVE NEGATIVE
//					currentRect = new RectangleF (center.X - ic, center.Y - ic, currentRect.Width, currentRect.Height);
//				if(outer.IntersectsWith(currentRect)){
//					return currentRect;
//				}
//			}
//
//			if (direction == 1) { // POSITIVE POSITIVE
//					currentRect = new RectangleF (center.X + ic, center.X + ic, currentRect.Width, currentRect.Height);
//				if(outer.IntersectsWith(currentRect)){
//					return currentRect;
//				}
//			}
//
//			if (direction == 2) { // POSITIVE NEGATIVE
//					currentRect = new RectangleF (center.X + ic, center.X - ic, currentRect.Width, currentRect.Height);
//				if(outer.IntersectsWith(currentRect)){
//					return currentRect;
//				}
//			}
//
//			if (direction == 3) { // NEGATIVE POSTIVE
//					currentRect = new RectangleF (center.X - ic, center.X + ic, currentRect.Width, currentRect.Height);
//				if(outer.IntersectsWith(currentRect)){
//					return currentRect;
//				}
//			}

//			if (direction == 0) { // NEGATIVE NEGATIVE
				currentRect = new RectangleF (center.X - ic, center.Y - ic, currentRect.Width, currentRect.Height);
				if(outer.IntersectsWith(currentRect)){
					return currentRect;
				}
//			}

//			if (direction == 1) { // POSITIVE POSITIVE
				currentRect = new RectangleF (center.X + ic, center.X + ic, currentRect.Width, currentRect.Height);
				if(outer.IntersectsWith(currentRect)){
					return currentRect;
				}
//			}

//			if (direction == 2) { // POSITIVE NEGATIVE
				currentRect = new RectangleF (center.X + ic, center.X - ic, currentRect.Width, currentRect.Height);
				if(outer.IntersectsWith(currentRect)){
					return currentRect;
				}
//			}

//			if (direction == 3) { // NEGATIVE POSTIVE
				currentRect = new RectangleF (center.X - ic, center.X + ic, currentRect.Width, currentRect.Height);
				if(outer.IntersectsWith(currentRect)){
					return currentRect;
				}
//			}






			return GetModuloRect (currentRect, outer, ++ic);



		}
			
//		public PointF drawSpiralPoint (double scale,double revolutions, int centreX, int centreY, SpiralType spiralType, int width, int height)
//		{
//			PointF point;
//			double theta = 0;
//			double radius = 0;
//
//			theta = revolutions * 360;
//
//			if (spiralType == SpiralType.Linear) {
//				radius = theta * scale;
//			} else if (spiralType == SpiralType.Quadratic) {
//				radius = theta * theta * scale;
//			} else if (spiralType == SpiralType.Cubic) {
//				radius = theta * theta * theta * scale;
//			} else if (spiralType == SpiralType.Exponential) {
//				radius = (Math.Pow (theta / 180 * Math.PI, Math.E)) * scale;
//			}
//					
//			double X = (radius * Math.Cos (theta / 180 * Math.PI)) + centreX;
//			double Y = (radius * Math.Sin (theta / 180 * Math.PI)) + centreY;
//			point = new PointF ((float)X, (float)Y);
//			Console.WriteLine (point);
//			return point;
//
//		}

		//
		//
		// centerX-- X origin of the spiral.
		// centerY-- Y origin of the spiral.
		// radius--- Distance from origin to outer arm.
		// sides---- Number of points or sides along the spiral's arm.
		// coils---- Number of coils or full rotations. (Positive numbers spin clockwise, negative numbers spin counter-clockwise)
		// rotation- Overall rotation of the spiral. ('0'=no rotation, '1'=360 degrees, '180/360'=180 degrees)
		//
//		void SetBlockDisposition(float centerX, float centerY, float radius, float sides, float coils, float rotation)
//
//		// value of theta corresponding to end of last coil
//			const double thetaMax = coils * 2 * Math.PI;
//
//		// How far to step away from center for each side.
//			const  double awayStep = radius / thetaMax;
//
//		// distance between points to plot
//			const double chord = 10;
//
//		DoSome ( centerX, centerY );
//
//		// For every side, step around and away from center.
//		// start at the angle corresponding to a distance of chord
//		// away from centre.
//		for ( double theta = chord / awayStep; theta <= thetaMax; ) {
//			//
//			// How far away from center
//			double away = awayStep * theta;
//			//
//			// How far around the center.
//			double around = theta + rotation;
//			//
//			// Convert 'around' and 'away' to X and Y.
//			double x = centerX + Math.cos ( around ) * away;
//			double y = centerY + Math.sin ( around ) * away;
//			//
//			// Now that you know it, do it.
//			DoSome ( x, y );
//
//			// to a first approximation, the points are on a circle
//			// so the angle between them is chord/radius
//			theta += chord / away;
//		}
//		}
//
//		public enum SpiralType
//		{
//			Linear,
//			Quadratic,
//			Cubic,
//			Exponential
//		}
	}
}