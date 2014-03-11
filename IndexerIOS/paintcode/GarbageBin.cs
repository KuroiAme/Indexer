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

			//// Group
			{
				//// Oval 2 Drawing
				var oval2Path = UIBezierPath.FromOval(new RectangleF(1.5f, 0.5f, 41, 8));
				color.SetFill();
				oval2Path.Fill();
				UIColor.Black.SetStroke();
				oval2Path.LineWidth = 1;
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
				bezierPath.LineWidth = 1;
				bezierPath.Stroke();


				//// Bezier 2 Drawing
				UIBezierPath bezier2Path = new UIBezierPath();
				bezier2Path.MoveTo(new PointF(43.42f, 3.82f));
				bezier2Path.AddLineTo(new PointF(43.42f, 39.18f));
				UIColor.Black.SetStroke();
				bezier2Path.LineWidth = 1;
				bezier2Path.Stroke();


				//// Bezier 8 Drawing
				UIBezierPath bezier8Path = new UIBezierPath();
				bezier8Path.MoveTo(new PointF(0.58f, 39.18f));
				bezier8Path.AddLineTo(new PointF(1.34f, 39.18f));
				bezier8Path.AddCurveToPoint(new PointF(6.99f, 41.53f), new PointF(1.91f, 40), new PointF(3.79f, 40.81f));
				bezier8Path.AddCurveToPoint(new PointF(37.01f, 41.53f), new PointF(15.27f, 43.39f), new PointF(28.73f, 43.39f));
				bezier8Path.AddCurveToPoint(new PointF(42.84f, 38.64f), new PointF(40.9f, 40.66f), new PointF(42.84f, 39.65f));
				bezier8Path.AddLineTo(new PointF(44, 38.64f));
				bezier8Path.AddCurveToPoint(new PointF(37.28f, 42.57f), new PointF(44, 40.1f), new PointF(41.76f, 41.56f));
				bezier8Path.AddCurveToPoint(new PointF(6.72f, 42.57f), new PointF(28.83f, 44.48f), new PointF(15.17f, 44.48f));
				bezier8Path.AddCurveToPoint(new PointF(0.58f, 37.37f), new PointF(0.92f, 41.27f), new PointF(-1.12f, 39.21f));
				bezier8Path.AddLineTo(new PointF(0.58f, 39.18f));
				bezier8Path.ClosePath();
				UIColor.Black.SetFill();
				bezier8Path.Fill();
			}



		}

		static void paintCodeNonRetina ()
		{
			//// Color Declarations
			UIColor color = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.000f);

			//// Oval 2 Drawing
			var oval2Path = UIBezierPath.FromOval(new RectangleF(2.5f, 0.5f, 17, 4));
			color.SetFill();
			oval2Path.Fill();
			UIColor.Black.SetStroke();
			oval2Path.LineWidth = 1;
			oval2Path.Stroke();


			//// Bezier 7 Drawing
			UIBezierPath bezier7Path = new UIBezierPath();
			bezier7Path.MoveTo(new PointF(14.54f, 3.99f));
			bezier7Path.AddCurveToPoint(new PointF(10.68f, 2.81f), new PointF(14.54f, 3.99f), new PointF(12.45f, 2.81f));
			bezier7Path.AddCurveToPoint(new PointF(6.82f, 3.99f), new PointF(8.91f, 2.81f), new PointF(6.82f, 3.99f));
			bezier7Path.AddLineTo(new PointF(6.82f, 2.81f));
			bezier7Path.AddCurveToPoint(new PointF(10.68f, 1.03f), new PointF(6.82f, 2.81f), new PointF(8.75f, 1.21f));
			bezier7Path.AddCurveToPoint(new PointF(14.54f, 2.81f), new PointF(12.61f, 0.84f), new PointF(14.54f, 2.81f));
			bezier7Path.AddLineTo(new PointF(14.54f, 3.99f));
			bezier7Path.ClosePath();
			color.SetFill();
			bezier7Path.Fill();
			UIColor.Black.SetStroke();
			bezier7Path.LineWidth = 1;
			bezier7Path.Stroke();


			//// Bezier 4 Drawing
			UIBezierPath bezier4Path = new UIBezierPath();
			bezier4Path.MoveTo(new PointF(10.76f, 4.98f));
			bezier4Path.AddLineTo(new PointF(10.76f, 21.74f));
			UIColor.Black.SetStroke();
			bezier4Path.LineWidth = 1;
			bezier4Path.Stroke();


			//// Bezier 5 Drawing
			UIBezierPath bezier5Path = new UIBezierPath();
			bezier5Path.MoveTo(new PointF(5.55f, 4.45f));
			bezier5Path.AddLineTo(new PointF(5.55f, 21.21f));
			UIColor.Black.SetStroke();
			bezier5Path.LineWidth = 1;
			bezier5Path.Stroke();


			//// Bezier 6 Drawing
			UIBezierPath bezier6Path = new UIBezierPath();
			bezier6Path.MoveTo(new PointF(15.97f, 4.45f));
			bezier6Path.AddLineTo(new PointF(15.97f, 21.21f));
			UIColor.Black.SetStroke();
			bezier6Path.LineWidth = 1;
			bezier6Path.Stroke();


			//// Oval Drawing
			var ovalPath = UIBezierPath.FromOval(new RectangleF(17.5f, 0.5f, 0, 0));
			UIColor.White.SetFill();
			ovalPath.Fill();
			UIColor.Black.SetStroke();
			ovalPath.LineWidth = 1;
			ovalPath.Stroke();


			//// Bezier Drawing
			UIBezierPath bezierPath = new UIBezierPath();
			bezierPath.MoveTo(new PointF(2.24f, 2.36f));
			bezierPath.AddLineTo(new PointF(2.5f, 20.5f));
			UIColor.White.SetFill();
			bezierPath.Fill();
			UIColor.Black.SetStroke();
			bezierPath.LineWidth = 1;
			bezierPath.Stroke();


			//// Bezier 2 Drawing
			UIBezierPath bezier2Path = new UIBezierPath();
			bezier2Path.MoveTo(new PointF(19.76f, 2.36f));
			bezier2Path.AddLineTo(new PointF(19.76f, 19.64f));
			UIColor.Black.SetStroke();
			bezier2Path.LineWidth = 1;
			bezier2Path.Stroke();


			//// Bezier 8 Drawing
			UIBezierPath bezier8Path = new UIBezierPath();
			bezier8Path.MoveTo(new PointF(2.24f, 19.64f));
			bezier8Path.AddLineTo(new PointF(2.55f, 19.64f));
			bezier8Path.AddCurveToPoint(new PointF(4.86f, 20.79f), new PointF(2.78f, 20.05f), new PointF(3.55f, 20.44f));
			bezier8Path.AddCurveToPoint(new PointF(17.14f, 20.79f), new PointF(8.24f, 21.7f), new PointF(13.76f, 21.7f));
			bezier8Path.AddCurveToPoint(new PointF(19.53f, 19.38f), new PointF(18.73f, 20.37f), new PointF(19.53f, 19.87f));
			bezier8Path.AddLineTo(new PointF(20, 19.38f));
			bezier8Path.AddCurveToPoint(new PointF(17.25f, 21.3f), new PointF(20, 20.1f), new PointF(19.08f, 20.81f));
			bezier8Path.AddCurveToPoint(new PointF(4.75f, 21.3f), new PointF(13.79f, 22.23f), new PointF(8.21f, 22.23f));
			bezier8Path.AddCurveToPoint(new PointF(2.24f, 18.76f), new PointF(2.38f, 20.66f), new PointF(1.54f, 19.66f));
			bezier8Path.AddLineTo(new PointF(2.24f, 19.64f));
			bezier8Path.ClosePath();
			UIColor.Black.SetFill();
			bezier8Path.Fill();




		}
	}
}

