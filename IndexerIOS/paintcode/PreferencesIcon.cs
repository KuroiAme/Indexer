using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace no.dctapps.commons
{
	public static class PreferencesIcon
	{
		public static UIImage MakeImage (){
			bool retina = (UIScreen.MainScreen.Scale > 1.0);
			if (retina) {
				UIGraphics.BeginImageContext (new SizeF (44, 44));
				paintCodeRetina();
			}else{
				UIGraphics.BeginImageContext (new SizeF (22, 22));
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

			//// Group
			{
				//// Bezier Drawing
				UIBezierPath bezierPath = new UIBezierPath();
				bezierPath.MoveTo(new PointF(13.72f, 13.72f));
				bezierPath.AddCurveToPoint(new PointF(13.72f, 29.28f), new PointF(9.43f, 18.02f), new PointF(9.43f, 24.98f));
				bezierPath.AddCurveToPoint(new PointF(29.28f, 29.28f), new PointF(18.02f, 33.57f), new PointF(24.98f, 33.57f));
				bezierPath.AddCurveToPoint(new PointF(29.28f, 13.72f), new PointF(33.57f, 24.98f), new PointF(33.57f, 18.02f));
				bezierPath.AddCurveToPoint(new PointF(13.72f, 13.72f), new PointF(24.98f, 9.43f), new PointF(18.02f, 9.43f));
				bezierPath.ClosePath();
				bezierPath.MoveTo(new PointF(32.11f, 10.19f));
				bezierPath.AddCurveToPoint(new PointF(32.11f, 32.81f), new PointF(37.96f, 16.43f), new PointF(37.96f, 26.57f));
				bezierPath.AddCurveToPoint(new PointF(10.89f, 32.81f), new PointF(26.25f, 39.06f), new PointF(16.75f, 39.06f));
				bezierPath.AddCurveToPoint(new PointF(10.89f, 10.19f), new PointF(5.04f, 26.57f), new PointF(5.04f, 16.43f));
				bezierPath.AddCurveToPoint(new PointF(32.11f, 10.19f), new PointF(16.75f, 3.94f), new PointF(26.25f, 3.94f));
				bezierPath.ClosePath();
				color.SetFill();
				bezierPath.Fill();
				UIColor.Black.SetStroke();
				bezierPath.LineWidth = 1;
				bezierPath.Stroke();


				//// Rounded Rectangle Drawing
				var roundedRectanglePath = UIBezierPath.FromRoundedRect(new RectangleF(16.5f, 1.5f, 11, 8), 4);
				color.SetFill();
				roundedRectanglePath.Fill();
				color2.SetStroke();
				roundedRectanglePath.LineWidth = 1;
				roundedRectanglePath.Stroke();


				//// Rounded Rectangle 2 Drawing
				var roundedRectangle2Path = UIBezierPath.FromRoundedRect(new RectangleF(31.5f, 17.5f, 11, 8), 4);
				color.SetFill();
				roundedRectangle2Path.Fill();
				color2.SetStroke();
				roundedRectangle2Path.LineWidth = 1;
				roundedRectangle2Path.Stroke();


				//// Rounded Rectangle 3 Drawing
				var roundedRectangle3Path = UIBezierPath.FromRoundedRect(new RectangleF(0.5f, 17.5f, 11, 8), 4);
				color.SetFill();
				roundedRectangle3Path.Fill();
				color2.SetStroke();
				roundedRectangle3Path.LineWidth = 1;
				roundedRectangle3Path.Stroke();


				//// Rounded Rectangle 4 Drawing
				var roundedRectangle4Path = UIBezierPath.FromRoundedRect(new RectangleF(16.5f, 35.5f, 11, 8), 4);
				color.SetFill();
				roundedRectangle4Path.Fill();
				color2.SetStroke();
				roundedRectangle4Path.LineWidth = 1;
				roundedRectangle4Path.Stroke();


				//// Rounded Rectangle 5 Drawing
				var roundedRectangle5Path = UIBezierPath.FromRoundedRect(new RectangleF(29.5f, 29.5f, 11, 8), 4);
				color.SetFill();
				roundedRectangle5Path.Fill();
				color2.SetStroke();
				roundedRectangle5Path.LineWidth = 1;
				roundedRectangle5Path.Stroke();


				//// Rounded Rectangle 6 Drawing
				var roundedRectangle6Path = UIBezierPath.FromRoundedRect(new RectangleF(3.5f, 29.5f, 11, 8), 4);
				color.SetFill();
				roundedRectangle6Path.Fill();
				color2.SetStroke();
				roundedRectangle6Path.LineWidth = 1;
				roundedRectangle6Path.Stroke();


				//// Rounded Rectangle 7 Drawing
				var roundedRectangle7Path = UIBezierPath.FromRoundedRect(new RectangleF(3.5f, 5.5f, 11, 8), 4);
				color.SetFill();
				roundedRectangle7Path.Fill();
				color2.SetStroke();
				roundedRectangle7Path.LineWidth = 1;
				roundedRectangle7Path.Stroke();


				//// Rounded Rectangle 8 Drawing
				var roundedRectangle8Path = UIBezierPath.FromRoundedRect(new RectangleF(29.5f, 5.5f, 11, 8), 4);
				color.SetFill();
				roundedRectangle8Path.Fill();
				color2.SetStroke();
				roundedRectangle8Path.LineWidth = 1;
				roundedRectangle8Path.Stroke();
			}


			//// Group 2
			{
				//// Bezier 2 Drawing
				UIBezierPath bezier2Path = new UIBezierPath();
				bezier2Path.MoveTo(new PointF(18.06f, 18.03f));
				bezier2Path.AddCurveToPoint(new PointF(18.06f, 24.18f), new PointF(16.17f, 19.73f), new PointF(16.17f, 22.48f));
				bezier2Path.AddCurveToPoint(new PointF(24.94f, 24.18f), new PointF(19.96f, 25.88f), new PointF(23.04f, 25.88f));
				bezier2Path.AddCurveToPoint(new PointF(24.94f, 18.03f), new PointF(26.83f, 22.48f), new PointF(26.83f, 19.73f));
				bezier2Path.AddCurveToPoint(new PointF(18.06f, 18.03f), new PointF(23.04f, 16.33f), new PointF(19.96f, 16.33f));
				bezier2Path.ClosePath();
				bezier2Path.MoveTo(new PointF(26.19f, 16.63f));
				bezier2Path.AddCurveToPoint(new PointF(26.19f, 25.58f), new PointF(28.77f, 19.1f), new PointF(28.77f, 23.11f));
				bezier2Path.AddCurveToPoint(new PointF(16.81f, 25.58f), new PointF(23.6f, 28.05f), new PointF(19.4f, 28.05f));
				bezier2Path.AddCurveToPoint(new PointF(16.81f, 16.63f), new PointF(14.23f, 23.11f), new PointF(14.23f, 19.1f));
				bezier2Path.AddCurveToPoint(new PointF(26.19f, 16.63f), new PointF(19.4f, 14.16f), new PointF(23.6f, 14.16f));
				bezier2Path.ClosePath();
				color.SetFill();
				bezier2Path.Fill();
				UIColor.Black.SetStroke();
				bezier2Path.LineWidth = 1;
				bezier2Path.Stroke();


				//// Rounded Rectangle 9 Drawing
				var roundedRectangle9Path = UIBezierPath.FromRoundedRect(new RectangleF(20.5f, 13.5f, 3, 3), 1.5f);
				color.SetFill();
				roundedRectangle9Path.Fill();
				color2.SetStroke();
				roundedRectangle9Path.LineWidth = 1;
				roundedRectangle9Path.Stroke();


				//// Rounded Rectangle 10 Drawing
				var roundedRectangle10Path = UIBezierPath.FromRoundedRect(new RectangleF(25.5f, 19.5f, 5, 3), 1.5f);
				color.SetFill();
				roundedRectangle10Path.Fill();
				color2.SetStroke();
				roundedRectangle10Path.LineWidth = 1;
				roundedRectangle10Path.Stroke();


				//// Rounded Rectangle 11 Drawing
				var roundedRectangle11Path = UIBezierPath.FromRoundedRect(new RectangleF(12.5f, 19.5f, 5, 3), 1.5f);
				color.SetFill();
				roundedRectangle11Path.Fill();
				color2.SetStroke();
				roundedRectangle11Path.LineWidth = 1;
				roundedRectangle11Path.Stroke();


				//// Rounded Rectangle 12 Drawing
				var roundedRectangle12Path = UIBezierPath.FromRoundedRect(new RectangleF(20.5f, 26.5f, 3, 3), 1.5f);
				color.SetFill();
				roundedRectangle12Path.Fill();
				color2.SetStroke();
				roundedRectangle12Path.LineWidth = 1;
				roundedRectangle12Path.Stroke();


				//// Rounded Rectangle 13 Drawing
				var roundedRectangle13Path = UIBezierPath.FromRoundedRect(new RectangleF(24.5f, 24.5f, 5, 2), 1);
				color.SetFill();
				roundedRectangle13Path.Fill();
				color2.SetStroke();
				roundedRectangle13Path.LineWidth = 1;
				roundedRectangle13Path.Stroke();


				//// Rounded Rectangle 14 Drawing
				var roundedRectangle14Path = UIBezierPath.FromRoundedRect(new RectangleF(13.5f, 24.5f, 6, 2), 1);
				color.SetFill();
				roundedRectangle14Path.Fill();
				color2.SetStroke();
				roundedRectangle14Path.LineWidth = 1;
				roundedRectangle14Path.Stroke();


				//// Rounded Rectangle 15 Drawing
				var roundedRectangle15Path = UIBezierPath.FromRoundedRect(new RectangleF(13.5f, 15.5f, 6, 2), 1);
				color.SetFill();
				roundedRectangle15Path.Fill();
				color2.SetStroke();
				roundedRectangle15Path.LineWidth = 1;
				roundedRectangle15Path.Stroke();


				//// Rounded Rectangle 16 Drawing
				var roundedRectangle16Path = UIBezierPath.FromRoundedRect(new RectangleF(24.5f, 15.5f, 5, 2), 1);
				color.SetFill();
				roundedRectangle16Path.Fill();
				color2.SetStroke();
				roundedRectangle16Path.LineWidth = 1;
				roundedRectangle16Path.Stroke();
			}




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
				bezierPath.MoveTo(new PointF(7.02f, 6.51f));
				bezierPath.AddCurveToPoint(new PointF(7.02f, 14.47f), new PointF(4.82f, 8.71f), new PointF(4.82f, 12.27f));
				bezierPath.AddCurveToPoint(new PointF(14.98f, 14.47f), new PointF(9.22f, 16.67f), new PointF(12.78f, 16.67f));
				bezierPath.AddCurveToPoint(new PointF(14.98f, 6.51f), new PointF(17.18f, 12.27f), new PointF(17.18f, 8.71f));
				bezierPath.AddCurveToPoint(new PointF(7.02f, 6.51f), new PointF(12.78f, 4.31f), new PointF(9.22f, 4.31f));
				bezierPath.ClosePath();
				bezierPath.MoveTo(new PointF(16.43f, 4.7f));
				bezierPath.AddCurveToPoint(new PointF(16.43f, 16.28f), new PointF(19.42f, 7.9f), new PointF(19.42f, 13.08f));
				bezierPath.AddCurveToPoint(new PointF(5.57f, 16.28f), new PointF(13.43f, 19.47f), new PointF(8.57f, 19.47f));
				bezierPath.AddCurveToPoint(new PointF(5.57f, 4.7f), new PointF(2.58f, 13.08f), new PointF(2.58f, 7.9f));
				bezierPath.AddCurveToPoint(new PointF(16.43f, 4.7f), new PointF(8.57f, 1.5f), new PointF(13.43f, 1.5f));
				bezierPath.ClosePath();
				color.SetFill();
				bezierPath.Fill();
				UIColor.Black.SetStroke();
				bezierPath.LineWidth = 1;
				bezierPath.Stroke();


				//// Rounded Rectangle Drawing
				var roundedRectanglePath = UIBezierPath.FromRoundedRect(new RectangleF(8.5f, 0.5f, 6, 4), 2);
				color.SetFill();
				roundedRectanglePath.Fill();
				color2.SetStroke();
				roundedRectanglePath.LineWidth = 1;
				roundedRectanglePath.Stroke();


				//// Rounded Rectangle 2 Drawing
				var roundedRectangle2Path = UIBezierPath.FromRoundedRect(new RectangleF(16.5f, 8.5f, 5, 4), 2);
				color.SetFill();
				roundedRectangle2Path.Fill();
				color2.SetStroke();
				roundedRectangle2Path.LineWidth = 1;
				roundedRectangle2Path.Stroke();


				//// Rounded Rectangle 3 Drawing
				var roundedRectangle3Path = UIBezierPath.FromRoundedRect(new RectangleF(0.5f, 8.5f, 5, 4), 2);
				color.SetFill();
				roundedRectangle3Path.Fill();
				color2.SetStroke();
				roundedRectangle3Path.LineWidth = 1;
				roundedRectangle3Path.Stroke();


				//// Rounded Rectangle 4 Drawing
				var roundedRectangle4Path = UIBezierPath.FromRoundedRect(new RectangleF(8.5f, 17.5f, 6, 4), 2);
				color.SetFill();
				roundedRectangle4Path.Fill();
				color2.SetStroke();
				roundedRectangle4Path.LineWidth = 1;
				roundedRectangle4Path.Stroke();


				//// Rounded Rectangle 5 Drawing
				var roundedRectangle5Path = UIBezierPath.FromRoundedRect(new RectangleF(15.5f, 14.5f, 5, 4), 2);
				color.SetFill();
				roundedRectangle5Path.Fill();
				color2.SetStroke();
				roundedRectangle5Path.LineWidth = 1;
				roundedRectangle5Path.Stroke();


				//// Rounded Rectangle 6 Drawing
				var roundedRectangle6Path = UIBezierPath.FromRoundedRect(new RectangleF(1.5f, 14.5f, 6, 4), 2);
				color.SetFill();
				roundedRectangle6Path.Fill();
				color2.SetStroke();
				roundedRectangle6Path.LineWidth = 1;
				roundedRectangle6Path.Stroke();


				//// Rounded Rectangle 7 Drawing
				var roundedRectangle7Path = UIBezierPath.FromRoundedRect(new RectangleF(1.5f, 2.5f, 6, 4), 2);
				color.SetFill();
				roundedRectangle7Path.Fill();
				color2.SetStroke();
				roundedRectangle7Path.LineWidth = 1;
				roundedRectangle7Path.Stroke();


				//// Rounded Rectangle 8 Drawing
				var roundedRectangle8Path = UIBezierPath.FromRoundedRect(new RectangleF(15.5f, 2.5f, 5, 4), 2);
				color.SetFill();
				roundedRectangle8Path.Fill();
				color2.SetStroke();
				roundedRectangle8Path.LineWidth = 1;
				roundedRectangle8Path.Stroke();
			}


			//// Group 2
			{
				//// Bezier 2 Drawing
				UIBezierPath bezier2Path = new UIBezierPath();
				bezier2Path.MoveTo(new PointF(9.19f, 8.66f));
				bezier2Path.AddCurveToPoint(new PointF(9.19f, 11.92f), new PointF(8.19f, 9.56f), new PointF(8.19f, 11.02f));
				bezier2Path.AddCurveToPoint(new PointF(12.81f, 11.92f), new PointF(10.19f, 12.82f), new PointF(11.81f, 12.82f));
				bezier2Path.AddCurveToPoint(new PointF(12.81f, 8.66f), new PointF(13.81f, 11.02f), new PointF(13.81f, 9.56f));
				bezier2Path.AddCurveToPoint(new PointF(9.19f, 8.66f), new PointF(11.81f, 7.76f), new PointF(10.19f, 7.76f));
				bezier2Path.ClosePath();
				bezier2Path.MoveTo(new PointF(13.47f, 7.92f));
				bezier2Path.AddCurveToPoint(new PointF(13.47f, 12.66f), new PointF(14.83f, 9.23f), new PointF(14.83f, 11.35f));
				bezier2Path.AddCurveToPoint(new PointF(8.53f, 12.66f), new PointF(12.1f, 13.97f), new PointF(9.9f, 13.97f));
				bezier2Path.AddCurveToPoint(new PointF(8.53f, 7.92f), new PointF(7.17f, 11.35f), new PointF(7.17f, 9.23f));
				bezier2Path.AddCurveToPoint(new PointF(13.47f, 7.92f), new PointF(9.9f, 6.61f), new PointF(12.1f, 6.61f));
				bezier2Path.ClosePath();
				color.SetFill();
				bezier2Path.Fill();
				UIColor.Black.SetStroke();
				bezier2Path.LineWidth = 1;
				bezier2Path.Stroke();


				//// Rounded Rectangle 9 Drawing
				var roundedRectangle9Path = UIBezierPath.FromRoundedRect(new RectangleF(11.5f, 6.5f, 0, 2), 0);
				color.SetFill();
				roundedRectangle9Path.Fill();
				color2.SetStroke();
				roundedRectangle9Path.LineWidth = 1;
				roundedRectangle9Path.Stroke();


				//// Rounded Rectangle 10 Drawing
				var roundedRectangle10Path = UIBezierPath.FromRoundedRect(new RectangleF(12.5f, 9.5f, 3, 1), 0.5f);
				color.SetFill();
				roundedRectangle10Path.Fill();
				color2.SetStroke();
				roundedRectangle10Path.LineWidth = 1;
				roundedRectangle10Path.Stroke();


				//// Rounded Rectangle 11 Drawing
				var roundedRectangle11Path = UIBezierPath.FromRoundedRect(new RectangleF(6.5f, 9.5f, 3, 1), 0.5f);
				color.SetFill();
				roundedRectangle11Path.Fill();
				color2.SetStroke();
				roundedRectangle11Path.LineWidth = 1;
				roundedRectangle11Path.Stroke();


				//// Rounded Rectangle 12 Drawing
				var roundedRectangle12Path = UIBezierPath.FromRoundedRect(new RectangleF(11.5f, 12.5f, 0, 2), 0);
				color.SetFill();
				roundedRectangle12Path.Fill();
				color2.SetStroke();
				roundedRectangle12Path.LineWidth = 1;
				roundedRectangle12Path.Stroke();


				//// Rounded Rectangle 13 Drawing
				var roundedRectangle13Path = UIBezierPath.FromRoundedRect(new RectangleF(11.5f, 11.5f, 4, 1), 0.5f);
				color.SetFill();
				roundedRectangle13Path.Fill();
				color2.SetStroke();
				roundedRectangle13Path.LineWidth = 1;
				roundedRectangle13Path.Stroke();


				//// Rounded Rectangle 14 Drawing
				var roundedRectangle14Path = UIBezierPath.FromRoundedRect(new RectangleF(6.5f, 11.5f, 5, 1), 0.5f);
				color.SetFill();
				roundedRectangle14Path.Fill();
				color2.SetStroke();
				roundedRectangle14Path.LineWidth = 1;
				roundedRectangle14Path.Stroke();


				//// Rounded Rectangle 15 Drawing
				var roundedRectangle15Path = UIBezierPath.FromRoundedRect(new RectangleF(6.5f, 7.5f, 5, 1), 0.5f);
				color.SetFill();
				roundedRectangle15Path.Fill();
				color2.SetStroke();
				roundedRectangle15Path.LineWidth = 1;
				roundedRectangle15Path.Stroke();


				//// Rounded Rectangle 16 Drawing
				var roundedRectangle16Path = UIBezierPath.FromRoundedRect(new RectangleF(11.5f, 7.5f, 4, 1), 0.5f);
				color.SetFill();
				roundedRectangle16Path.Fill();
				color2.SetStroke();
				roundedRectangle16Path.LineWidth = 1;
				roundedRectangle16Path.Stroke();
			}




		}
	}
}

