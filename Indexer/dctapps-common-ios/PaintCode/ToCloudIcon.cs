using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace no.dctapps.commons
{
	public static class ToCloudIcon
	{
		public static UIImage MakeImage (){
			bool retina = (UIScreen.MainScreen.Scale > 1.0);
			if (retina) {
				UIGraphics.BeginImageContext (new System.Drawing.SizeF (96, 64));
				paintCodeRetina();
			}else{
				UIGraphics.BeginImageContext (new System.Drawing.SizeF (48, 32));
				paintCodeNonRetina();
			}

			var converted = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return converted;

		}

		static void paintCodeRetina ()
		{
			//// Color Declarations
			UIColor color2 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);

			//// Bezier Drawing
			UIBezierPath bezierPath = new UIBezierPath();
			bezierPath.MoveTo(new PointF(22.72f, 5.43f));
			bezierPath.AddCurveToPoint(new PointF(23.37f, 6.19f), new PointF(22.95f, 5.67f), new PointF(23.17f, 5.93f));
			bezierPath.AddCurveToPoint(new PointF(27.47f, 4.67f), new PointF(24.63f, 5.42f), new PointF(26.03f, 4.91f));
			bezierPath.AddCurveToPoint(new PointF(37.28f, 7.43f), new PointF(30.92f, 4.09f), new PointF(34.61f, 5.01f));
			bezierPath.AddCurveToPoint(new PointF(37.28f, 21.57f), new PointF(41.57f, 11.33f), new PointF(41.57f, 17.67f));
			bezierPath.AddCurveToPoint(new PointF(27.49f, 24.33f), new PointF(34.62f, 23.99f), new PointF(30.93f, 24.91f));
			bezierPath.AddCurveToPoint(new PointF(24.72f, 29.3f), new PointF(27.39f, 26.14f), new PointF(26.47f, 27.92f));
			bezierPath.AddCurveToPoint(new PointF(11.28f, 29.3f), new PointF(21.01f, 32.23f), new PointF(14.99f, 32.23f));
			bezierPath.AddCurveToPoint(new PointF(9.86f, 20.13f), new PointF(8.13f, 26.82f), new PointF(7.66f, 23.01f));
			bezierPath.AddCurveToPoint(new PointF(9.28f, 19.57f), new PointF(9.66f, 19.95f), new PointF(9.47f, 19.77f));
			bezierPath.AddCurveToPoint(new PointF(9.28f, 5.43f), new PointF(5.57f, 15.67f), new PointF(5.57f, 9.33f));
			bezierPath.AddCurveToPoint(new PointF(22.72f, 5.43f), new PointF(12.99f, 1.52f), new PointF(19.01f, 1.52f));
			bezierPath.ClosePath();
			UIColor.White.SetFill();
			bezierPath.Fill();
			UIColor.Black.SetStroke();
			bezierPath.LineWidth = 1;
			bezierPath.Stroke();


			//// Bezier 2 Drawing
			UIBezierPath bezier2Path = new UIBezierPath();
			bezier2Path.MoveTo(new PointF(3.5f, 40.5f));
			bezier2Path.AddCurveToPoint(new PointF(9.5f, 33.5f), new PointF(9.5f, 33.5f), new PointF(9.5f, 33.5f));
			color2.SetFill();
			bezier2Path.Fill();
			UIColor.Black.SetStroke();
			bezier2Path.LineWidth = 2.5f;
			bezier2Path.Stroke();


			//// Bezier 3 Drawing
			UIBezierPath bezier3Path = new UIBezierPath();
			bezier3Path.MoveTo(new PointF(6.5f, 32.5f));
			bezier3Path.AddLineTo(new PointF(11.5f, 37.5f));
			bezier3Path.AddLineTo(new PointF(10.5f, 32.5f));
			bezier3Path.AddLineTo(new PointF(6.5f, 32.5f));
			bezier3Path.ClosePath();
			color2.SetFill();
			bezier3Path.Fill();
			UIColor.Black.SetStroke();
			bezier3Path.LineWidth = 1;
			bezier3Path.Stroke();



		}

		static void paintCodeNonRetina ()
		{
			//// Color Declarations
			UIColor color2 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);

			//// Bezier Drawing
			UIBezierPath bezierPath = new UIBezierPath();
			bezierPath.MoveTo(new PointF(22.72f, 5.43f));
			bezierPath.AddCurveToPoint(new PointF(23.37f, 6.19f), new PointF(22.95f, 5.67f), new PointF(23.17f, 5.93f));
			bezierPath.AddCurveToPoint(new PointF(27.47f, 4.67f), new PointF(24.63f, 5.42f), new PointF(26.03f, 4.91f));
			bezierPath.AddCurveToPoint(new PointF(37.28f, 7.43f), new PointF(30.92f, 4.09f), new PointF(34.61f, 5.01f));
			bezierPath.AddCurveToPoint(new PointF(37.28f, 21.57f), new PointF(41.57f, 11.33f), new PointF(41.57f, 17.67f));
			bezierPath.AddCurveToPoint(new PointF(27.49f, 24.33f), new PointF(34.62f, 23.99f), new PointF(30.93f, 24.91f));
			bezierPath.AddCurveToPoint(new PointF(24.72f, 29.3f), new PointF(27.39f, 26.14f), new PointF(26.47f, 27.92f));
			bezierPath.AddCurveToPoint(new PointF(11.28f, 29.3f), new PointF(21.01f, 32.23f), new PointF(14.99f, 32.23f));
			bezierPath.AddCurveToPoint(new PointF(9.86f, 20.13f), new PointF(8.13f, 26.82f), new PointF(7.66f, 23.01f));
			bezierPath.AddCurveToPoint(new PointF(9.28f, 19.57f), new PointF(9.66f, 19.95f), new PointF(9.47f, 19.77f));
			bezierPath.AddCurveToPoint(new PointF(9.28f, 5.43f), new PointF(5.57f, 15.67f), new PointF(5.57f, 9.33f));
			bezierPath.AddCurveToPoint(new PointF(22.72f, 5.43f), new PointF(12.99f, 1.52f), new PointF(19.01f, 1.52f));
			bezierPath.ClosePath();
			UIColor.White.SetFill();
			bezierPath.Fill();
			UIColor.Black.SetStroke();
			bezierPath.LineWidth = 1;
			bezierPath.Stroke();


			//// Bezier 2 Drawing
			UIBezierPath bezier2Path = new UIBezierPath();
			bezier2Path.MoveTo(new PointF(3.5f, 40.5f));
			bezier2Path.AddCurveToPoint(new PointF(9.5f, 33.5f), new PointF(9.5f, 33.5f), new PointF(9.5f, 33.5f));
			color2.SetFill();
			bezier2Path.Fill();
			UIColor.Black.SetStroke();
			bezier2Path.LineWidth = 2.5f;
			bezier2Path.Stroke();


			//// Bezier 3 Drawing
			UIBezierPath bezier3Path = new UIBezierPath();
			bezier3Path.MoveTo(new PointF(6.5f, 32.5f));
			bezier3Path.AddLineTo(new PointF(11.5f, 37.5f));
			bezier3Path.AddLineTo(new PointF(10.5f, 32.5f));
			bezier3Path.AddLineTo(new PointF(6.5f, 32.5f));
			bezier3Path.ClosePath();
			color2.SetFill();
			bezier3Path.Fill();
			UIColor.Black.SetStroke();
			bezier3Path.LineWidth = 1;
			bezier3Path.Stroke();



		}
	}
}

