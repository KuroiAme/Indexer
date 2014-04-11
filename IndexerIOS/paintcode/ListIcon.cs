using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace no.dctapps.commons
{
	public static class ListIcon
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

			//// Bezier Drawing
			UIBezierPath bezierPath = new UIBezierPath();
			bezierPath.MoveTo(new PointF(11.5f, 9.5f));
			bezierPath.AddCurveToPoint(new PointF(35.5f, 9.5f), new PointF(35.5f, 9.5f), new PointF(35.5f, 9.5f));
			UIColor.Black.SetStroke();
			bezierPath.LineWidth = 1;
			bezierPath.Stroke();


			//// Bezier 2 Drawing
			UIBezierPath bezier2Path = new UIBezierPath();
			bezier2Path.MoveTo(new PointF(11.5f, 15.5f));
			bezier2Path.AddCurveToPoint(new PointF(35.5f, 15.5f), new PointF(35.5f, 15.5f), new PointF(35.5f, 15.5f));
			UIColor.Black.SetStroke();
			bezier2Path.LineWidth = 1;
			bezier2Path.Stroke();


			//// Bezier 3 Drawing
			UIBezierPath bezier3Path = new UIBezierPath();
			bezier3Path.MoveTo(new PointF(11.5f, 21.5f));
			bezier3Path.AddCurveToPoint(new PointF(35.5f, 21.5f), new PointF(35.5f, 21.5f), new PointF(35.5f, 21.5f));
			UIColor.Black.SetStroke();
			bezier3Path.LineWidth = 1;
			bezier3Path.Stroke();


			//// Bezier 4 Drawing
			UIBezierPath bezier4Path = new UIBezierPath();
			bezier4Path.MoveTo(new PointF(11.5f, 27.5f));
			bezier4Path.AddCurveToPoint(new PointF(35.5f, 27.5f), new PointF(35.5f, 27.5f), new PointF(35.5f, 27.5f));
			UIColor.Black.SetStroke();
			bezier4Path.LineWidth = 1;
			bezier4Path.Stroke();


			//// Bezier 5 Drawing
			UIBezierPath bezier5Path = new UIBezierPath();
			bezier5Path.MoveTo(new PointF(11.5f, 32.5f));
			bezier5Path.AddCurveToPoint(new PointF(35.5f, 32.5f), new PointF(12.48f, 32.5f), new PointF(35.5f, 32.5f));
			UIColor.Black.SetStroke();
			bezier5Path.LineWidth = 1;
			bezier5Path.Stroke();


			//// Bezier 6 Drawing
			UIBezierPath bezier6Path = new UIBezierPath();
			bezier6Path.MoveTo(new PointF(11.5f, 38.5f));
			bezier6Path.AddCurveToPoint(new PointF(35.5f, 38.5f), new PointF(35.5f, 38.5f), new PointF(35.5f, 38.5f));
			UIColor.Black.SetStroke();
			bezier6Path.LineWidth = 1;
			bezier6Path.Stroke();


			//// Oval Drawing
			var ovalPath = UIBezierPath.FromOval(new RectangleF(7.5f, 8.5f, 2, 2));
			UIColor.Black.SetStroke();
			ovalPath.LineWidth = 1;
			ovalPath.Stroke();


			//// Oval 2 Drawing
			var oval2Path = UIBezierPath.FromOval(new RectangleF(7.5f, 14.5f, 2, 2));
			UIColor.Black.SetStroke();
			oval2Path.LineWidth = 1;
			oval2Path.Stroke();


			//// Oval 3 Drawing
			var oval3Path = UIBezierPath.FromOval(new RectangleF(7.5f, 20.5f, 2, 2));
			UIColor.Black.SetStroke();
			oval3Path.LineWidth = 1;
			oval3Path.Stroke();


			//// Oval 4 Drawing
			var oval4Path = UIBezierPath.FromOval(new RectangleF(7.5f, 25.5f, 2, 2));
			UIColor.Black.SetStroke();
			oval4Path.LineWidth = 1;
			oval4Path.Stroke();


			//// Oval 5 Drawing
			var oval5Path = UIBezierPath.FromOval(new RectangleF(7.5f, 31.5f, 2, 2));
			UIColor.Black.SetStroke();
			oval5Path.LineWidth = 1;
			oval5Path.Stroke();


			//// Oval 6 Drawing
			var oval6Path = UIBezierPath.FromOval(new RectangleF(7.5f, 37.5f, 2, 2));
			UIColor.Black.SetStroke();
			oval6Path.LineWidth = 1;
			oval6Path.Stroke();



		}

		static void paintCodeNonRetina ()
		{

			//// Group
			{
				//// Bezier Drawing
				UIBezierPath bezierPath = new UIBezierPath();
				bezierPath.MoveTo(new PointF(3.41f, 1.03f));
				bezierPath.AddCurveToPoint(new PointF(21.62f, 1.03f), new PointF(21.62f, 1.03f), new PointF(21.62f, 1.03f));
				UIColor.Black.SetStroke();
				bezierPath.LineWidth = 1;
				bezierPath.Stroke();


				//// Bezier 2 Drawing
				UIBezierPath bezier2Path = new UIBezierPath();
				bezier2Path.MoveTo(new PointF(3.41f, 5.16f));
				bezier2Path.AddCurveToPoint(new PointF(21.62f, 5.16f), new PointF(21.62f, 5.16f), new PointF(21.62f, 5.16f));
				UIColor.Black.SetStroke();
				bezier2Path.LineWidth = 1;
				bezier2Path.Stroke();


				//// Bezier 3 Drawing
				UIBezierPath bezier3Path = new UIBezierPath();
				bezier3Path.MoveTo(new PointF(3.41f, 9.28f));
				bezier3Path.AddCurveToPoint(new PointF(21.62f, 9.28f), new PointF(21.62f, 9.28f), new PointF(21.62f, 9.28f));
				UIColor.Black.SetStroke();
				bezier3Path.LineWidth = 1;
				bezier3Path.Stroke();


				//// Bezier 4 Drawing
				UIBezierPath bezier4Path = new UIBezierPath();
				bezier4Path.MoveTo(new PointF(3.41f, 13.41f));
				bezier4Path.AddCurveToPoint(new PointF(21.62f, 13.41f), new PointF(21.62f, 13.41f), new PointF(21.62f, 13.41f));
				UIColor.Black.SetStroke();
				bezier4Path.LineWidth = 1;
				bezier4Path.Stroke();


				//// Bezier 5 Drawing
				UIBezierPath bezier5Path = new UIBezierPath();
				bezier5Path.MoveTo(new PointF(3.41f, 16.84f));
				bezier5Path.AddCurveToPoint(new PointF(21.62f, 16.84f), new PointF(4.16f, 16.84f), new PointF(21.62f, 16.84f));
				UIColor.Black.SetStroke();
				bezier5Path.LineWidth = 1;
				bezier5Path.Stroke();


				//// Bezier 6 Drawing
				UIBezierPath bezier6Path = new UIBezierPath();
				bezier6Path.MoveTo(new PointF(3.41f, 20.97f));
				bezier6Path.AddCurveToPoint(new PointF(21.62f, 20.97f), new PointF(21.62f, 20.97f), new PointF(21.62f, 20.97f));
				UIColor.Black.SetStroke();
				bezier6Path.LineWidth = 1;
				bezier6Path.Stroke();


				//// Oval Drawing
				var ovalPath = UIBezierPath.FromOval(new RectangleF(0.5f, 0.5f, 1, 1));
				UIColor.Black.SetStroke();
				ovalPath.LineWidth = 1;
				ovalPath.Stroke();


				//// Oval 2 Drawing
				var oval2Path = UIBezierPath.FromOval(new RectangleF(0.5f, 4.5f, 1, 1));
				UIColor.Black.SetStroke();
				oval2Path.LineWidth = 1;
				oval2Path.Stroke();


				//// Oval 3 Drawing
				var oval3Path = UIBezierPath.FromOval(new RectangleF(0.5f, 8.5f, 1, 1));
				UIColor.Black.SetStroke();
				oval3Path.LineWidth = 1;
				oval3Path.Stroke();


				//// Oval 4 Drawing
				var oval4Path = UIBezierPath.FromOval(new RectangleF(0.5f, 12.5f, 1, 1));
				UIColor.Black.SetStroke();
				oval4Path.LineWidth = 1;
				oval4Path.Stroke();


				//// Oval 5 Drawing
				var oval5Path = UIBezierPath.FromOval(new RectangleF(0.5f, 16.5f, 1, 1));
				UIColor.Black.SetStroke();
				oval5Path.LineWidth = 1;
				oval5Path.Stroke();


				//// Oval 6 Drawing
				var oval6Path = UIBezierPath.FromOval(new RectangleF(0.5f, 20.5f, 1, 1));
				UIColor.Black.SetStroke();
				oval6Path.LineWidth = 1;
				oval6Path.Stroke();
			}



		}
	}
}

