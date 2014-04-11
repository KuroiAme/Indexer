using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace no.dctapps.commons
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
				UIGraphics.BeginImageContext (new System.Drawing.SizeF (44, 44));

				//START PAINTCODE RETINA
				//// Color Declarations
				UIColor color = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.000f);
				UIColor color3 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);

				//// Group
				{
					//// Rectangle Drawing
					var rectanglePath = UIBezierPath.FromRect(new RectangleF(3.5f, 6.5f, 36, 29));
					color.SetFill();
					rectanglePath.Fill();
					color3.SetStroke();
					rectanglePath.LineWidth = 2;
					rectanglePath.Stroke();


					//// Bezier Drawing
					UIBezierPath bezierPath = new UIBezierPath();
					bezierPath.MoveTo(new PointF(4, 7));
					bezierPath.AddLineTo(new PointF(22.52f, 22.37f));
					bezierPath.AddLineTo(new PointF(40, 7));
					color.SetFill();
					bezierPath.Fill();
					color3.SetStroke();
					bezierPath.LineWidth = 2;
					bezierPath.Stroke();
				}



				//END PAINTCODE RETINA


			} else {
				UIGraphics.BeginImageContext (new System.Drawing.SizeF (22, 22));

				//start paintcode NON RETINA
				//// Color Declarations
				UIColor color = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.000f);
				UIColor color3 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);

				//// Group
				{
					//// Rectangle Drawing
					var rectanglePath = UIBezierPath.FromRect(new RectangleF(2.5f, 2.5f, 17, 17));
					color.SetFill();
					rectanglePath.Fill();
					color3.SetStroke();
					rectanglePath.LineWidth = 2;
					rectanglePath.Stroke();


					//// Bezier Drawing
					UIBezierPath bezierPath = new UIBezierPath();
					bezierPath.MoveTo(new PointF(2.49f, 2.6f));
					bezierPath.AddLineTo(new PointF(11.5f, 11.82f));
					bezierPath.AddLineTo(new PointF(20, 2.6f));
					color.SetFill();
					bezierPath.Fill();
					color3.SetStroke();
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

