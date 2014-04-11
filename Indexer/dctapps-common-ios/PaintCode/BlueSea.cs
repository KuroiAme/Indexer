using System;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Drawing;

namespace no.dctapps.commons
{
	public class BlueSea
	{
		public BlueSea ()
		{
		}

		public static UIImage MakeBlueSea (){
			UIGraphics.BeginImageContext(new System.Drawing.SizeF(640,1136));
			//BEGIN PAINTCODE

			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			UIColor color = UIColor.FromRGBA(0.114f, 0.705f, 1.000f, 1.000f);
			UIColor gradientColor = UIColor.FromRGBA(0.088f, 0.606f, 0.676f, 1.000f);
			UIColor gradientColor2 = UIColor.FromRGBA(0.018f, 0.509f, 0.675f, 1.000f);

			//// Gradient Declarations
			var gradientColors = new CGColor [] {color.CGColor, UIColor.FromRGBA(0.066f, 0.607f, 0.837f, 1.000f).CGColor, gradientColor2.CGColor, gradientColor.CGColor};
			var gradientLocations = new float [] {0, 0.21f, 0.46f, 1};
			var gradient = new CGGradient(colorSpace, gradientColors, gradientLocations);

			//// Rectangle Drawing
			var rectanglePath = UIBezierPath.FromRect(new RectangleF(-46.5f, -35.5f, 728, 1192));
			context.SaveState();
			rectanglePath.AddClip();
			context.DrawLinearGradient(gradient, new PointF(317.5f, -35.5f), new PointF(317.5f, 1156.5f), 0);
			context.RestoreState();
			UIColor.Black.SetStroke();
			rectanglePath.LineWidth = 1;
			rectanglePath.Stroke();

			//END PAINTCODE

			var converted = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return converted;

		}
	}
}

