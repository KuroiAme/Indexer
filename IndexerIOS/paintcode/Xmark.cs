using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace IndexerIOS
{
	public class Xmark
	{
		public static UIImage MakeImage (){
			bool retina = (UIScreen.MainScreen.Scale > 1.0);
			if (retina) {
				UIGraphics.BeginImageContext (new System.Drawing.SizeF (44, 44));
				paintCodeRetina();
			}else{
				UIGraphics.BeginImageContext (new System.Drawing.SizeF (22, 22));
				paintCodeNonRetina();
			}

			var converted = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return converted;

		}

		static void paintCodeRetina ()
		{
			//// Color Declarations
			UIColor color = UIColor.FromRGBA(0.000f, 0.219f, 0.657f, 1.000f);

			//// Bezier Drawing
			UIBezierPath bezierPath = new UIBezierPath();
			bezierPath.MoveTo(new PointF(1.5f, 1.5f));
			bezierPath.AddCurveToPoint(new PointF(40.5f, 41.5f), new PointF(40.5f, 41.5f), new PointF(40.5f, 41.5f));
			color.SetStroke();
			bezierPath.LineWidth = 4;
			bezierPath.Stroke();


			//// Bezier 2 Drawing
			UIBezierPath bezier2Path = new UIBezierPath();
			bezier2Path.MoveTo(new PointF(40.5f, 1.5f));
			bezier2Path.AddLineTo(new PointF(1.5f, 42.5f));
			color.SetStroke();
			bezier2Path.LineWidth = 4;
			bezier2Path.Stroke();

		}



		static void paintCodeNonRetina ()
		{
			//// Color Declarations
			UIColor color = UIColor.FromRGBA(0.000f, 0.219f, 0.657f, 1.000f);

			//// Group
			{
				//// Bezier Drawing
				UIBezierPath bezierPath = new UIBezierPath();
				bezierPath.MoveTo(new PointF(1.25f, 1.24f));
				bezierPath.AddCurveToPoint(new PointF(20.75f, 20.29f), new PointF(20.75f, 20.29f), new PointF(20.75f, 20.29f));
				color.SetStroke();
				bezierPath.LineWidth = 4;
				bezierPath.Stroke();


				//// Bezier 2 Drawing
				UIBezierPath bezier2Path = new UIBezierPath();
				bezier2Path.MoveTo(new PointF(20.75f, 1.24f));
				bezier2Path.AddLineTo(new PointF(1.25f, 20.76f));
				color.SetStroke();
				bezier2Path.LineWidth = 4;
				bezier2Path.Stroke();
			}

		}
	}
}

