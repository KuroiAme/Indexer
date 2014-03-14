using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace IndexerIOS
{
	public static class ScissorsIcon
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
			UIColor color = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
			UIColor color2 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.000f);

			//// Bezier Drawing
			UIBezierPath bezierPath = new UIBezierPath();
			bezierPath.MoveTo(new PointF(33.51f, 2.63f));
			bezierPath.AddLineTo(new PointF(13.55f, 26.55f));
			bezierPath.AddCurveToPoint(new PointF(24.53f, 7.67f), new PointF(13.55f, 26.55f), new PointF(19.54f, 13.65f));
			bezierPath.AddCurveToPoint(new PointF(33.51f, 2.63f), new PointF(29.52f, 1.69f), new PointF(33.51f, 2.63f));
			color.SetFill();
			bezierPath.Fill();
			UIColor.Black.SetStroke();
			bezierPath.LineWidth = 1;
			bezierPath.Stroke();


			//// Bezier 2 Drawing
			UIBezierPath bezier2Path = new UIBezierPath();
			bezier2Path.MoveTo(new PointF(14.55f, 27.81f));
			bezier2Path.AddCurveToPoint(new PointF(40.5f, 11.44f), new PointF(14.55f, 27.81f), new PointF(40.56f, 11.03f));
			bezier2Path.AddCurveToPoint(new PointF(37.51f, 16.48f), new PointF(40.45f, 11.85f), new PointF(42.73f, 11.33f));
			bezier2Path.AddCurveToPoint(new PointF(14.55f, 27.81f), new PointF(32.28f, 21.62f), new PointF(14.55f, 27.81f));
			bezier2Path.ClosePath();
			color.SetFill();
			bezier2Path.Fill();
			UIColor.Black.SetStroke();
			bezier2Path.LineWidth = 1;
			bezier2Path.Stroke();


			//// Oval 2 Drawing
			var oval2Path = UIBezierPath.FromOval(new RectangleF(9.5f, 27.5f, 9, 12));
			color2.SetFill();
			oval2Path.Fill();
			UIColor.Black.SetStroke();
			oval2Path.LineWidth = 2;
			oval2Path.Stroke();


			//// Oval Drawing
			var ovalPath = UIBezierPath.FromOval(new RectangleF(3.5f, 19.5f, 10, 12));
			color2.SetFill();
			ovalPath.Fill();
			UIColor.Black.SetStroke();
			ovalPath.LineWidth = 2;
			ovalPath.Stroke();



		}

		static void paintCodeNonRetina ()
		{
			//// Color Declarations
			UIColor color = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
			UIColor color2 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.000f);

			//// Group
			{
				//// Bezier Drawing
				UIBezierPath bezierPath = new UIBezierPath();
				bezierPath.MoveTo(new PointF(15.05f, 2.27f));
				bezierPath.AddLineTo(new PointF(7.17f, 12.34f));
				bezierPath.AddCurveToPoint(new PointF(11.5f, 4.39f), new PointF(7.17f, 12.34f), new PointF(9.53f, 6.9f));
				bezierPath.AddCurveToPoint(new PointF(15.05f, 2.27f), new PointF(13.47f, 1.87f), new PointF(15.05f, 2.27f));
				color.SetFill();
				bezierPath.Fill();
				UIColor.Black.SetStroke();
				bezierPath.LineWidth = 1;
				bezierPath.Stroke();


				//// Bezier 2 Drawing
				UIBezierPath bezier2Path = new UIBezierPath();
				bezier2Path.MoveTo(new PointF(7.56f, 12.87f));
				bezier2Path.AddCurveToPoint(new PointF(17.8f, 5.98f), new PointF(7.56f, 12.87f), new PointF(17.82f, 5.8f));
				bezier2Path.AddCurveToPoint(new PointF(16.62f, 8.1f), new PointF(17.78f, 6.15f), new PointF(18.68f, 5.93f));
				bezier2Path.AddCurveToPoint(new PointF(7.56f, 12.87f), new PointF(14.56f, 10.26f), new PointF(7.56f, 12.87f));
				bezier2Path.ClosePath();
				color.SetFill();
				bezier2Path.Fill();
				UIColor.Black.SetStroke();
				bezier2Path.LineWidth = 1;
				bezier2Path.Stroke();


				//// Oval 2 Drawing
				var oval2Path = UIBezierPath.FromOval(new RectangleF(5.5f, 12.5f, 4, 5));
				color2.SetFill();
				oval2Path.Fill();
				UIColor.Black.SetStroke();
				oval2Path.LineWidth = 2;
				oval2Path.Stroke();


				//// Oval Drawing
				var ovalPath = UIBezierPath.FromOval(new RectangleF(3.5f, 9.5f, 4, 5));
				color2.SetFill();
				ovalPath.Fill();
				UIColor.Black.SetStroke();
				ovalPath.LineWidth = 2;
				ovalPath.Stroke();
			}



		}
	}
}

