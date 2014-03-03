using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace IndexerIOS
{
	public class Letter
	{
		public Letter ()
		{
		}

		public static UIImage MakeLetter ()
		{
			bool retina = (UIScreen.MainScreen.Scale > 1.0);
			if (retina) {
				UIGraphics.BeginImageContext (new System.Drawing.SizeF (96, 64));

				//START PAINTCODE RETINA
				//// Color Declarations
				UIColor color = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.000f);

				//// Group
				{
					//// Rectangle Drawing
					var rectanglePath = UIBezierPath.FromRect(new RectangleF(4.5f, 4.5f, 86, 53));
					color.SetFill();
					rectanglePath.Fill();
					UIColor.Blue.SetStroke();
					rectanglePath.LineWidth = 2;
					rectanglePath.Stroke();


					//// Bezier Drawing
					UIBezierPath bezierPath = new UIBezierPath();
					bezierPath.MoveTo(new PointF(4.17f, 6.65f));
					bezierPath.AddLineTo(new PointF(49.91f, 33.43f));
					bezierPath.AddLineTo(new PointF(90.83f, 9.09f));
					color.SetFill();
					bezierPath.Fill();
					UIColor.Blue.SetStroke();
					bezierPath.LineWidth = 2;
					bezierPath.Stroke();
				}
				//END PAINTCODE RETINA


			} else {
				UIGraphics.BeginImageContext (new System.Drawing.SizeF (48, 32));

				//start paintcode NON RETINA
				// Color Declarations
				UIColor color = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.000f);

				// Group
				{
					//// Rectangle Drawing
					var rectanglePath = UIBezierPath.FromRect(new RectangleF(3.5f, 3.5f, 40, 25));
					color.SetFill();
					rectanglePath.Fill();
					UIColor.Blue.SetStroke();
					rectanglePath.LineWidth = 2;
					rectanglePath.Stroke();


					//// Bezier Drawing
					UIBezierPath bezierPath = new UIBezierPath();
					bezierPath.MoveTo(new PointF(3.54f, 4.7f));
					bezierPath.AddLineTo(new PointF(24.61f, 17.13f));
					bezierPath.AddLineTo(new PointF(43.46f, 5.83f));
					color.SetFill();
					bezierPath.Fill();
					UIColor.Blue.SetStroke();
					bezierPath.LineWidth = 2;
					bezierPath.Stroke();
				}

				//END PAINTCODE NON RETINA
			}
			var converted = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return converted;

		}
	}
}

