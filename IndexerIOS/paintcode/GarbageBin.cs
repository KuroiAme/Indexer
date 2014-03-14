using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace IndexerIOS
{
	public static class GarbageBin
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
			UIColor color = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.000f);

			//// Oval 2 Drawing
			var oval2Path = UIBezierPath.FromOval(new RectangleF(1.5f, 0.5f, 41, 8));
			color.SetFill();
			oval2Path.Fill();
			UIColor.Black.SetStroke();
			oval2Path.LineWidth = 2;
			oval2Path.Stroke();


			//// Bezier 7 Drawing
			UIBezierPath bezier7Path = new UIBezierPath();
			bezier7Path.MoveTo(new PointF(30.64f, 7.16f));
			bezier7Path.AddCurveToPoint(new PointF(21.21f, 4.74f), new PointF(30.64f, 7.16f), new PointF(25.54f, 4.74f));
			bezier7Path.AddCurveToPoint(new PointF(11.79f, 7.16f), new PointF(16.89f, 4.74f), new PointF(11.79f, 7.16f));
			bezier7Path.AddLineTo(new PointF(11.79f, 4.74f));
			bezier7Path.AddCurveToPoint(new PointF(21.21f, 1.1f), new PointF(11.79f, 4.74f), new PointF(16.5f, 1.48f));
			bezier7Path.AddCurveToPoint(new PointF(30.64f, 4.74f), new PointF(25.93f, 0.72f), new PointF(30.64f, 4.74f));
			bezier7Path.AddLineTo(new PointF(30.64f, 7.16f));
			bezier7Path.ClosePath();
			color.SetFill();
			bezier7Path.Fill();
			UIColor.Black.SetStroke();
			bezier7Path.LineWidth = 1;
			bezier7Path.Stroke();


			//// Bezier 4 Drawing
			UIBezierPath bezier4Path = new UIBezierPath();
			bezier4Path.MoveTo(new PointF(21.42f, 9.18f));
			bezier4Path.AddLineTo(new PointF(21.42f, 43.46f));
			UIColor.Black.SetStroke();
			bezier4Path.LineWidth = 1;
			bezier4Path.Stroke();


			//// Bezier 5 Drawing
			UIBezierPath bezier5Path = new UIBezierPath();
			bezier5Path.MoveTo(new PointF(8.68f, 8.11f));
			bezier5Path.AddLineTo(new PointF(8.68f, 42.39f));
			UIColor.Black.SetStroke();
			bezier5Path.LineWidth = 1;
			bezier5Path.Stroke();


			//// Bezier 6 Drawing
			UIBezierPath bezier6Path = new UIBezierPath();
			bezier6Path.MoveTo(new PointF(34.16f, 8.11f));
			bezier6Path.AddLineTo(new PointF(34.16f, 42.39f));
			UIColor.Black.SetStroke();
			bezier6Path.LineWidth = 1;
			bezier6Path.Stroke();


			//// Oval Drawing
			var ovalPath = UIBezierPath.FromOval(new RectangleF(38.5f, 0.5f, 0, 0));
			UIColor.White.SetFill();
			ovalPath.Fill();
			UIColor.Black.SetStroke();
			ovalPath.LineWidth = 1;
			ovalPath.Stroke();


			//// Bezier Drawing
			UIBezierPath bezierPath = new UIBezierPath();
			bezierPath.MoveTo(new PointF(0.58f, 3.82f));
			bezierPath.AddLineTo(new PointF(1.22f, 40.93f));
			UIColor.White.SetFill();
			bezierPath.Fill();
			UIColor.Black.SetStroke();
			bezierPath.LineWidth = 2;
			bezierPath.Stroke();


			//// Bezier 2 Drawing
			UIBezierPath bezier2Path = new UIBezierPath();
			bezier2Path.MoveTo(new PointF(43.42f, 3.82f));
			bezier2Path.AddLineTo(new PointF(43.42f, 39.18f));
			UIColor.Black.SetStroke();
			bezier2Path.LineWidth = 2;
			bezier2Path.Stroke();


			//// Bezier 3 Drawing
			UIBezierPath bezier3Path = new UIBezierPath();
			bezier3Path.MoveTo(new PointF(1.5f, 40.5f));
			bezier3Path.AddCurveToPoint(new PointF(21.5f, 43.5f), new PointF(1.5f, 40.5f), new PointF(10.69f, 43.12f));
			bezier3Path.AddCurveToPoint(new PointF(43.5f, 38.5f), new PointF(32.31f, 43.88f), new PointF(43.5f, 38.5f));
			UIColor.Black.SetStroke();
			bezier3Path.LineWidth = 2;
			bezier3Path.Stroke();




		}

		static void paintCodeNonRetina ()
		{
			//// Color Declarations
			UIColor color = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.000f);

			//// Group
			{
				//// Oval 2 Drawing
				var oval2Path = UIBezierPath.FromOval(new RectangleF(0.5f, 0.5f, 21, 4));
				color.SetFill();
				oval2Path.Fill();
				UIColor.Black.SetStroke();
				oval2Path.LineWidth = 2;
				oval2Path.Stroke();


				//// Bezier 7 Drawing
				UIBezierPath bezier7Path = new UIBezierPath();
				bezier7Path.MoveTo(new PointF(15.32f, 3.58f));
				bezier7Path.AddCurveToPoint(new PointF(10.61f, 2.37f), new PointF(15.32f, 3.58f), new PointF(12.77f, 2.37f));
				bezier7Path.AddCurveToPoint(new PointF(5.89f, 3.58f), new PointF(8.45f, 2.37f), new PointF(5.89f, 3.58f));
				bezier7Path.AddLineTo(new PointF(5.89f, 2.37f));
				bezier7Path.AddCurveToPoint(new PointF(10.61f, 0.55f), new PointF(5.89f, 2.37f), new PointF(8.25f, 0.74f));
				bezier7Path.AddCurveToPoint(new PointF(15.32f, 2.37f), new PointF(12.96f, 0.36f), new PointF(15.32f, 2.37f));
				bezier7Path.AddLineTo(new PointF(15.32f, 3.58f));
				bezier7Path.ClosePath();
				color.SetFill();
				bezier7Path.Fill();
				UIColor.Black.SetStroke();
				bezier7Path.LineWidth = 1;
				bezier7Path.Stroke();


				//// Bezier 4 Drawing
				UIBezierPath bezier4Path = new UIBezierPath();
				bezier4Path.MoveTo(new PointF(10.71f, 4.59f));
				bezier4Path.AddLineTo(new PointF(10.71f, 21.73f));
				UIColor.Black.SetStroke();
				bezier4Path.LineWidth = 1;
				bezier4Path.Stroke();


				//// Bezier 5 Drawing
				UIBezierPath bezier5Path = new UIBezierPath();
				bezier5Path.MoveTo(new PointF(4.34f, 4.05f));
				bezier5Path.AddLineTo(new PointF(4.34f, 21.2f));
				UIColor.Black.SetStroke();
				bezier5Path.LineWidth = 1;
				bezier5Path.Stroke();


				//// Bezier 6 Drawing
				UIBezierPath bezier6Path = new UIBezierPath();
				bezier6Path.MoveTo(new PointF(17.08f, 4.05f));
				bezier6Path.AddLineTo(new PointF(17.08f, 21.2f));
				UIColor.Black.SetStroke();
				bezier6Path.LineWidth = 1;
				bezier6Path.Stroke();


				//// Oval Drawing
				var ovalPath = UIBezierPath.FromOval(new RectangleF(19.5f, 0.5f, 0, 0));
				UIColor.White.SetFill();
				ovalPath.Fill();
				UIColor.Black.SetStroke();
				ovalPath.LineWidth = 1;
				ovalPath.Stroke();


				//// Bezier Drawing
				UIBezierPath bezierPath = new UIBezierPath();
				bezierPath.MoveTo(new PointF(0.29f, 1.91f));
				bezierPath.AddLineTo(new PointF(0.61f, 20.47f));
				UIColor.White.SetFill();
				bezierPath.Fill();
				UIColor.Black.SetStroke();
				bezierPath.LineWidth = 2;
				bezierPath.Stroke();


				//// Bezier 2 Drawing
				UIBezierPath bezier2Path = new UIBezierPath();
				bezier2Path.MoveTo(new PointF(21.71f, 1.91f));
				bezier2Path.AddLineTo(new PointF(21.71f, 19.59f));
				UIColor.Black.SetStroke();
				bezier2Path.LineWidth = 2;
				bezier2Path.Stroke();


				//// Bezier 3 Drawing
				UIBezierPath bezier3Path = new UIBezierPath();
				bezier3Path.MoveTo(new PointF(0.75f, 20.25f));
				bezier3Path.AddCurveToPoint(new PointF(10.75f, 21.75f), new PointF(0.75f, 20.25f), new PointF(5.34f, 21.56f));
				bezier3Path.AddCurveToPoint(new PointF(21.75f, 19.25f), new PointF(16.16f, 21.94f), new PointF(21.75f, 19.25f));
				UIColor.Black.SetStroke();
				bezier3Path.LineWidth = 2;
				bezier3Path.Stroke();
			}



		}
	}
}

