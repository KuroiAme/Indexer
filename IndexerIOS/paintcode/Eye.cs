using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace IndexerIOS
{
	public class Eye
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
			//// General Declarations
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			UIColor color3 = UIColor.FromRGBA(0.000f, 0.200f, 0.067f, 1.000f);
			UIColor color5 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
			UIColor color6 = UIColor.FromRGBA(0.200f, 0.067f, 0.000f, 1.000f);
			UIColor color7 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.000f);
			UIColor color8 = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 1.000f);

			//// Shadow Declarations
			var shadow = color6.CGColor;
			var shadowOffset = new SizeF(0.1f, 1.1f);
			var shadowBlurRadius = 11.5f;

			//// Bezier 17 Drawing
			UIBezierPath bezier17Path = new UIBezierPath();
			bezier17Path.MoveTo(new PointF(31.5f, 57.5f));
			bezier17Path.AddCurveToPoint(new PointF(34.47f, 57.42f), new PointF(32.56f, 57.47f), new PointF(33.55f, 57.44f));
			bezier17Path.AddCurveToPoint(new PointF(55.5f, 56.5f), new PointF(46.81f, 57.12f), new PointF(46.72f, 57.4f));
			bezier17Path.AddCurveToPoint(new PointF(79.5f, 53.5f), new PointF(64.93f, 55.53f), new PointF(79.5f, 53.5f));
			color6.SetStroke();
			bezier17Path.LineWidth = 3;
			bezier17Path.Stroke();


			//// Eyelash
			{
				//// Bezier Drawing
				UIBezierPath bezierPath = new UIBezierPath();
				bezierPath.MoveTo(new PointF(8.5f, 32.5f));
				bezierPath.AddCurveToPoint(new PointF(37.44f, 16.5f), new PointF(14.12f, 32.02f), new PointF(25.32f, 20.81f));
				bezierPath.AddCurveToPoint(new PointF(69.13f, 16.5f), new PointF(52.35f, 11.19f), new PointF(67.2f, 16.63f));
				bezierPath.AddCurveToPoint(new PointF(95.77f, 32.5f), new PointF(72.68f, 16.25f), new PointF(95.77f, 32.5f));
				color6.SetStroke();
				bezierPath.LineWidth = 3;
				bezierPath.Stroke();


				//// Bezier 2 Drawing
				UIBezierPath bezier2Path = new UIBezierPath();
				bezier2Path.MoveTo(new PointF(0.23f, 16.5f));
				bezier2Path.AddCurveToPoint(new PointF(6.71f, 24.6f), new PointF(0.23f, 16.5f), new PointF(4.58f, 22.88f));
				bezier2Path.AddCurveToPoint(new PointF(14.01f, 30.5f), new PointF(14.01f, 30.5f), new PointF(14.01f, 30.5f));
				color6.SetStroke();
				bezier2Path.LineWidth = 3;
				bezier2Path.Stroke();


				//// Bezier 3 Drawing
				UIBezierPath bezier3Path = new UIBezierPath();
				bezier3Path.MoveTo(new PointF(3.9f, 11.5f));
				bezier3Path.AddCurveToPoint(new PointF(10.38f, 19.6f), new PointF(3.9f, 11.5f), new PointF(8.25f, 17.88f));
				bezier3Path.AddCurveToPoint(new PointF(18.6f, 26.5f), new PointF(17.68f, 25.5f), new PointF(18.6f, 26.5f));
				color6.SetStroke();
				bezier3Path.LineWidth = 3;
				bezier3Path.Stroke();


				//// Bezier 4 Drawing
				UIBezierPath bezier4Path = new UIBezierPath();
				bezier4Path.MoveTo(new PointF(10.79f, 9.5f));
				bezier4Path.AddCurveToPoint(new PointF(17.27f, 17.6f), new PointF(10.79f, 9.5f), new PointF(15.14f, 15.88f));
				bezier4Path.AddCurveToPoint(new PointF(24.57f, 23.5f), new PointF(24.57f, 23.5f), new PointF(24.57f, 23.5f));
				color6.SetStroke();
				bezier4Path.LineWidth = 3;
				bezier4Path.Stroke();


				//// Bezier 5 Drawing
				UIBezierPath bezier5Path = new UIBezierPath();
				bezier5Path.MoveTo(new PointF(17.68f, 4.5f));
				bezier5Path.AddCurveToPoint(new PointF(24.16f, 12.6f), new PointF(17.68f, 4.5f), new PointF(22.03f, 10.88f));
				bezier5Path.AddCurveToPoint(new PointF(31.46f, 18.5f), new PointF(31.46f, 18.5f), new PointF(31.46f, 18.5f));
				color6.SetStroke();
				bezier5Path.LineWidth = 3;
				bezier5Path.Stroke();


				//// Bezier 6 Drawing
				UIBezierPath bezier6Path = new UIBezierPath();
				bezier6Path.MoveTo(new PointF(13.55f, 8.5f));
				bezier6Path.AddCurveToPoint(new PointF(27.79f, 20.5f), new PointF(27.24f, 20.5f), new PointF(27.79f, 20.5f));
				color6.SetStroke();
				bezier6Path.LineWidth = 3;
				bezier6Path.Stroke();


				//// Bezier 7 Drawing
				UIBezierPath bezier7Path = new UIBezierPath();
				bezier7Path.MoveTo(new PointF(34.68f, 17.5f));
				bezier7Path.AddCurveToPoint(new PointF(29.17f, 12.5f), new PointF(32.71f, 14.7f), new PointF(30.47f, 14.41f));
				bezier7Path.AddCurveToPoint(new PointF(24.11f, 1.5f), new PointF(23.48f, 4.18f), new PointF(24.11f, 1.5f));
				color6.SetStroke();
				bezier7Path.LineWidth = 3;
				bezier7Path.Stroke();


				//// Bezier 8 Drawing
				UIBezierPath bezier8Path = new UIBezierPath();
				bezier8Path.MoveTo(new PointF(27.79f, 2.5f));
				bezier8Path.AddCurveToPoint(new PointF(34.27f, 10.6f), new PointF(27.79f, 2.5f), new PointF(32.14f, 8.88f));
				bezier8Path.AddCurveToPoint(new PointF(41.57f, 16.5f), new PointF(41.57f, 16.5f), new PointF(41.57f, 16.5f));
				color6.SetStroke();
				bezier8Path.LineWidth = 3;
				bezier8Path.Stroke();


				//// Bezier 9 Drawing
				UIBezierPath bezier9Path = new UIBezierPath();
				bezier9Path.MoveTo(new PointF(31.46f, 0.5f));
				bezier9Path.AddCurveToPoint(new PointF(37.94f, 8.6f), new PointF(31.46f, 0.5f), new PointF(35.81f, 6.88f));
				bezier9Path.AddCurveToPoint(new PointF(45.24f, 14.5f), new PointF(45.24f, 14.5f), new PointF(45.24f, 14.5f));
				color6.SetStroke();
				bezier9Path.LineWidth = 3;
				bezier9Path.Stroke();


				//// Bezier 10 Drawing
				UIBezierPath bezier10Path = new UIBezierPath();
				bezier10Path.MoveTo(new PointF(38.35f, 0.5f));
				bezier10Path.AddCurveToPoint(new PointF(44.83f, 8.6f), new PointF(38.35f, 0.5f), new PointF(42.7f, 6.88f));
				bezier10Path.AddCurveToPoint(new PointF(52.13f, 14.5f), new PointF(52.13f, 14.5f), new PointF(52.13f, 14.5f));
				color6.SetStroke();
				bezier10Path.LineWidth = 3;
				bezier10Path.Stroke();


				//// Bezier 11 Drawing
				UIBezierPath bezier11Path = new UIBezierPath();
				bezier11Path.MoveTo(new PointF(45.24f, 0.5f));
				bezier11Path.AddCurveToPoint(new PointF(51.72f, 8.6f), new PointF(45.24f, 0.5f), new PointF(49.59f, 6.88f));
				bezier11Path.AddCurveToPoint(new PointF(59.02f, 14.5f), new PointF(59.02f, 14.5f), new PointF(59.02f, 14.5f));
				color6.SetStroke();
				bezier11Path.LineWidth = 3;
				bezier11Path.Stroke();


				//// Bezier 12 Drawing
				UIBezierPath bezier12Path = new UIBezierPath();
				bezier12Path.MoveTo(new PointF(52.13f, 2.5f));
				bezier12Path.AddCurveToPoint(new PointF(58.61f, 10.6f), new PointF(52.13f, 2.5f), new PointF(56.48f, 8.88f));
				bezier12Path.AddCurveToPoint(new PointF(65.91f, 16.5f), new PointF(65.91f, 16.5f), new PointF(65.91f, 16.5f));
				color6.SetStroke();
				bezier12Path.LineWidth = 3;
				bezier12Path.Stroke();


				//// Bezier 13 Drawing
				UIBezierPath bezier13Path = new UIBezierPath();
				bezier13Path.MoveTo(new PointF(59.02f, 3.5f));
				bezier13Path.AddCurveToPoint(new PointF(65.5f, 11.6f), new PointF(59.02f, 3.5f), new PointF(63.37f, 9.88f));
				bezier13Path.AddCurveToPoint(new PointF(72.8f, 17.5f), new PointF(72.8f, 17.5f), new PointF(72.8f, 17.5f));
				color6.SetStroke();
				bezier13Path.LineWidth = 3;
				bezier13Path.Stroke();


				//// Bezier 14 Drawing
				UIBezierPath bezier14Path = new UIBezierPath();
				bezier14Path.MoveTo(new PointF(66.5f, 3.5f));
				bezier14Path.AddLineTo(new PointF(82.45f, 22.5f));
				color6.SetStroke();
				bezier14Path.LineWidth = 3;
				bezier14Path.Stroke();


				//// Bezier 15 Drawing
				UIBezierPath bezier15Path = new UIBezierPath();
				bezier15Path.MoveTo(new PointF(82.5f, 7.5f));
				bezier15Path.AddCurveToPoint(new PointF(95.77f, 31.5f), new PointF(89.8f, 13.4f), new PointF(95.77f, 31.5f));
				color6.SetStroke();
				bezier15Path.LineWidth = 3;
				bezier15Path.Stroke();


				//// Bezier 16 Drawing
				UIBezierPath bezier16Path = new UIBezierPath();
				bezier16Path.MoveTo(new PointF(75.56f, 6.5f));
				bezier16Path.AddCurveToPoint(new PointF(88.42f, 26.5f), new PointF(82.86f, 12.4f), new PointF(88.42f, 26.5f));
				color6.SetStroke();
				bezier16Path.LineWidth = 3;
				bezier16Path.Stroke();
			}


			//// Oval Drawing
			var ovalPath = UIBezierPath.FromOval(new RectangleF(29.5f, 22.5f, 48, 34));
			color3.SetFill();
			ovalPath.Fill();

			////// Oval Inner Shadow
			var ovalBorderRect = ovalPath.Bounds;
			ovalBorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
			ovalBorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
			ovalBorderRect = RectangleF.Union(ovalBorderRect, ovalPath.Bounds);
			ovalBorderRect.Inflate(1, 1);

			var ovalNegativePath = UIBezierPath.FromRect(ovalBorderRect);
			ovalNegativePath.AppendPath(ovalPath);
			ovalNegativePath.UsesEvenOddFillRule = true;

			context.SaveState();
			{
				var xOffset = shadowOffset.Width + (float)Math.Round(ovalBorderRect.Width);
				var yOffset = shadowOffset.Height;
				context.SetShadowWithColor(
					new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
					shadowBlurRadius,
					shadow);

				ovalPath.AddClip();
				var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(ovalBorderRect.Width), 0);
				ovalNegativePath.ApplyTransform(transform);
				UIColor.Gray.SetFill();
				ovalNegativePath.Fill();
			}
			context.RestoreState();

			color7.SetStroke();
			ovalPath.LineWidth = 1;
			ovalPath.Stroke();


			//// Oval 3 Drawing
			var oval3Path = UIBezierPath.FromOval(new RectangleF(44.5f, 32.5f, 18, 13));
			color5.SetFill();
			oval3Path.Fill();
			color5.SetStroke();
			oval3Path.LineWidth = 1;
			oval3Path.Stroke();


			//// Oval 2 Drawing
			var oval2Path = UIBezierPath.FromOval(new RectangleF(60.5f, 27.5f, 12, 7));
			color8.SetFill();
			oval2Path.Fill();
			color7.SetStroke();
			oval2Path.LineWidth = 1;
			oval2Path.Stroke();


			//// Oval 4 Drawing
			var oval4Path = UIBezierPath.FromOval(new RectangleF(31.5f, 34.5f, 9, 11));
			color8.SetFill();
			oval4Path.Fill();
			color7.SetStroke();
			oval4Path.LineWidth = 1;
			oval4Path.Stroke();


			//// Oval 5 Drawing
			var oval5Path = UIBezierPath.FromOval(new RectangleF(43.5f, 45.5f, 9, 8));
			color8.SetFill();
			oval5Path.Fill();
			color7.SetStroke();
			oval5Path.LineWidth = 1;
			oval5Path.Stroke();


			//// Bezier 18 Drawing
			UIBezierPath bezier18Path = new UIBezierPath();
			bezier18Path.MoveTo(new PointF(47.5f, 23.5f));
			bezier18Path.AddCurveToPoint(new PointF(65.5f, 26.5f), new PointF(57.41f, 21.14f), new PointF(70.33f, 23.82f));
			bezier18Path.AddCurveToPoint(new PointF(60.3f, 30.41f), new PointF(62.62f, 28.1f), new PointF(61.08f, 29.39f));
			bezier18Path.AddCurveToPoint(new PointF(60.5f, 33.5f), new PointF(58.65f, 32.57f), new PointF(60.5f, 33.5f));
			bezier18Path.AddCurveToPoint(new PointF(52.5f, 31.5f), new PointF(60.5f, 33.5f), new PointF(56.21f, 31.49f));
			bezier18Path.AddCurveToPoint(new PointF(47.5f, 33.5f), new PointF(48.79f, 31.51f), new PointF(47.5f, 33.5f));
			bezier18Path.AddLineTo(new PointF(44.5f, 36.5f));
			bezier18Path.AddLineTo(new PointF(33.5f, 33.5f));
			bezier18Path.AddCurveToPoint(new PointF(47.5f, 23.5f), new PointF(33.5f, 33.5f), new PointF(38.95f, 25.54f));
			bezier18Path.ClosePath();
			color3.SetFill();
			bezier18Path.Fill();

			////// Bezier 18 Inner Shadow
			var bezier18BorderRect = bezier18Path.Bounds;
			bezier18BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
			bezier18BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
			bezier18BorderRect = RectangleF.Union(bezier18BorderRect, bezier18Path.Bounds);
			bezier18BorderRect.Inflate(1, 1);

			var bezier18NegativePath = UIBezierPath.FromRect(bezier18BorderRect);
			bezier18NegativePath.AppendPath(bezier18Path);
			bezier18NegativePath.UsesEvenOddFillRule = true;

			context.SaveState();
			{
				var xOffset = shadowOffset.Width + (float)Math.Round(bezier18BorderRect.Width);
				var yOffset = shadowOffset.Height;
				context.SetShadowWithColor(
					new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
					shadowBlurRadius,
					shadow);

				bezier18Path.AddClip();
				var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(bezier18BorderRect.Width), 0);
				bezier18NegativePath.ApplyTransform(transform);
				UIColor.Gray.SetFill();
				bezier18NegativePath.Fill();
			}
			context.RestoreState();

			color7.SetStroke();
			bezier18Path.LineWidth = 1;
			bezier18Path.Stroke();



		}

		static void paintCodeNonRetina ()
		{
			//// General Declarations
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			UIColor color3 = UIColor.FromRGBA(0.000f, 0.200f, 0.067f, 1.000f);
			UIColor color5 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
			UIColor color6 = UIColor.FromRGBA(0.200f, 0.067f, 0.000f, 1.000f);
			UIColor color7 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.000f);
			UIColor color8 = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 1.000f);

			//// Shadow Declarations
			var shadow = color6.CGColor;
			var shadowOffset = new SizeF(0.1f, 1.1f);
			var shadowBlurRadius = 11.5f;

			//// Group
			{
				//// Bezier 17 Drawing
				UIBezierPath bezier17Path = new UIBezierPath();
				bezier17Path.MoveTo(new PointF(15.75f, 31.72f));
				bezier17Path.AddCurveToPoint(new PointF(17.23f, 31.68f), new PointF(16.28f, 31.71f), new PointF(16.78f, 31.69f));
				bezier17Path.AddCurveToPoint(new PointF(27.75f, 31.17f), new PointF(23.41f, 31.51f), new PointF(23.36f, 31.67f));
				bezier17Path.AddCurveToPoint(new PointF(39.75f, 29.52f), new PointF(32.47f, 30.64f), new PointF(39.75f, 29.52f));
				color6.SetStroke();
				bezier17Path.LineWidth = 3;
				bezier17Path.Stroke();


				//// Eyelash
				{
					//// Bezier Drawing
					UIBezierPath bezierPath = new UIBezierPath();
					bezierPath.MoveTo(new PointF(4.25f, 17.73f));
					bezierPath.AddCurveToPoint(new PointF(18.72f, 9), new PointF(7.06f, 17.46f), new PointF(12.66f, 11.35f));
					bezierPath.AddCurveToPoint(new PointF(34.56f, 9), new PointF(26.18f, 6.1f), new PointF(33.6f, 9.07f));
					bezierPath.AddCurveToPoint(new PointF(47.89f, 17.73f), new PointF(36.34f, 8.86f), new PointF(47.89f, 17.73f));
					color6.SetStroke();
					bezierPath.LineWidth = 3;
					bezierPath.Stroke();


					//// Bezier 2 Drawing
					UIBezierPath bezier2Path = new UIBezierPath();
					bezier2Path.MoveTo(new PointF(0.11f, 9));
					bezier2Path.AddCurveToPoint(new PointF(3.35f, 13.42f), new PointF(0.11f, 9), new PointF(2.29f, 12.48f));
					bezier2Path.AddCurveToPoint(new PointF(7, 16.64f), new PointF(7, 16.64f), new PointF(7, 16.64f));
					color6.SetStroke();
					bezier2Path.LineWidth = 3;
					bezier2Path.Stroke();


					//// Bezier 3 Drawing
					UIBezierPath bezier3Path = new UIBezierPath();
					bezier3Path.MoveTo(new PointF(1.95f, 6.27f));
					bezier3Path.AddCurveToPoint(new PointF(5.19f, 10.69f), new PointF(1.95f, 6.27f), new PointF(4.13f, 9.75f));
					bezier3Path.AddCurveToPoint(new PointF(9.3f, 14.45f), new PointF(8.84f, 13.91f), new PointF(9.3f, 14.45f));
					color6.SetStroke();
					bezier3Path.LineWidth = 3;
					bezier3Path.Stroke();


					//// Bezier 4 Drawing
					UIBezierPath bezier4Path = new UIBezierPath();
					bezier4Path.MoveTo(new PointF(5.4f, 5.18f));
					bezier4Path.AddCurveToPoint(new PointF(8.64f, 9.6f), new PointF(5.4f, 5.18f), new PointF(7.57f, 8.66f));
					bezier4Path.AddCurveToPoint(new PointF(12.29f, 12.82f), new PointF(12.29f, 12.82f), new PointF(12.29f, 12.82f));
					color6.SetStroke();
					bezier4Path.LineWidth = 3;
					bezier4Path.Stroke();


					//// Bezier 5 Drawing
					UIBezierPath bezier5Path = new UIBezierPath();
					bezier5Path.MoveTo(new PointF(8.84f, 2.45f));
					bezier5Path.AddCurveToPoint(new PointF(12.08f, 6.87f), new PointF(8.84f, 2.45f), new PointF(11.02f, 5.93f));
					bezier5Path.AddCurveToPoint(new PointF(15.73f, 10.09f), new PointF(15.73f, 10.09f), new PointF(15.73f, 10.09f));
					color6.SetStroke();
					bezier5Path.LineWidth = 3;
					bezier5Path.Stroke();


					//// Bezier 6 Drawing
					UIBezierPath bezier6Path = new UIBezierPath();
					bezier6Path.MoveTo(new PointF(6.78f, 4.64f));
					bezier6Path.AddCurveToPoint(new PointF(13.89f, 11.18f), new PointF(13.62f, 11.18f), new PointF(13.89f, 11.18f));
					color6.SetStroke();
					bezier6Path.LineWidth = 3;
					bezier6Path.Stroke();


					//// Bezier 7 Drawing
					UIBezierPath bezier7Path = new UIBezierPath();
					bezier7Path.MoveTo(new PointF(17.34f, 9.55f));
					bezier7Path.AddCurveToPoint(new PointF(14.58f, 6.82f), new PointF(16.35f, 8.02f), new PointF(15.24f, 7.86f));
					bezier7Path.AddCurveToPoint(new PointF(12.06f, 0.82f), new PointF(11.74f, 2.28f), new PointF(12.06f, 0.82f));
					color6.SetStroke();
					bezier7Path.LineWidth = 3;
					bezier7Path.Stroke();


					//// Bezier 8 Drawing
					UIBezierPath bezier8Path = new UIBezierPath();
					bezier8Path.MoveTo(new PointF(13.89f, 1.36f));
					bezier8Path.AddCurveToPoint(new PointF(17.13f, 5.78f), new PointF(13.89f, 1.36f), new PointF(16.07f, 4.84f));
					bezier8Path.AddCurveToPoint(new PointF(20.78f, 9), new PointF(20.78f, 9), new PointF(20.78f, 9));
					color6.SetStroke();
					bezier8Path.LineWidth = 3;
					bezier8Path.Stroke();


					//// Bezier 9 Drawing
					UIBezierPath bezier9Path = new UIBezierPath();
					bezier9Path.MoveTo(new PointF(15.73f, 0.27f));
					bezier9Path.AddCurveToPoint(new PointF(18.97f, 4.69f), new PointF(15.73f, 0.27f), new PointF(17.91f, 3.75f));
					bezier9Path.AddCurveToPoint(new PointF(22.62f, 7.91f), new PointF(22.62f, 7.91f), new PointF(22.62f, 7.91f));
					color6.SetStroke();
					bezier9Path.LineWidth = 3;
					bezier9Path.Stroke();


					//// Bezier 10 Drawing
					UIBezierPath bezier10Path = new UIBezierPath();
					bezier10Path.MoveTo(new PointF(19.18f, 0.27f));
					bezier10Path.AddCurveToPoint(new PointF(22.42f, 4.69f), new PointF(19.18f, 0.27f), new PointF(21.35f, 3.75f));
					bezier10Path.AddCurveToPoint(new PointF(26.07f, 7.91f), new PointF(26.07f, 7.91f), new PointF(26.07f, 7.91f));
					color6.SetStroke();
					bezier10Path.LineWidth = 3;
					bezier10Path.Stroke();


					//// Bezier 11 Drawing
					UIBezierPath bezier11Path = new UIBezierPath();
					bezier11Path.MoveTo(new PointF(22.62f, 0.27f));
					bezier11Path.AddCurveToPoint(new PointF(25.86f, 4.69f), new PointF(22.62f, 0.27f), new PointF(24.8f, 3.75f));
					bezier11Path.AddCurveToPoint(new PointF(29.51f, 7.91f), new PointF(29.51f, 7.91f), new PointF(29.51f, 7.91f));
					color6.SetStroke();
					bezier11Path.LineWidth = 3;
					bezier11Path.Stroke();


					//// Bezier 12 Drawing
					UIBezierPath bezier12Path = new UIBezierPath();
					bezier12Path.MoveTo(new PointF(26.07f, 1.36f));
					bezier12Path.AddCurveToPoint(new PointF(29.31f, 5.78f), new PointF(26.07f, 1.36f), new PointF(28.24f, 4.84f));
					bezier12Path.AddCurveToPoint(new PointF(32.96f, 9), new PointF(32.96f, 9), new PointF(32.96f, 9));
					color6.SetStroke();
					bezier12Path.LineWidth = 3;
					bezier12Path.Stroke();


					//// Bezier 13 Drawing
					UIBezierPath bezier13Path = new UIBezierPath();
					bezier13Path.MoveTo(new PointF(29.51f, 1.91f));
					bezier13Path.AddCurveToPoint(new PointF(32.75f, 6.33f), new PointF(29.51f, 1.91f), new PointF(31.69f, 5.39f));
					bezier13Path.AddCurveToPoint(new PointF(36.4f, 9.55f), new PointF(36.4f, 9.55f), new PointF(36.4f, 9.55f));
					color6.SetStroke();
					bezier13Path.LineWidth = 3;
					bezier13Path.Stroke();


					//// Bezier 14 Drawing
					UIBezierPath bezier14Path = new UIBezierPath();
					bezier14Path.MoveTo(new PointF(33.25f, 1.91f));
					bezier14Path.AddLineTo(new PointF(41.22f, 12.27f));
					color6.SetStroke();
					bezier14Path.LineWidth = 3;
					bezier14Path.Stroke();


					//// Bezier 15 Drawing
					UIBezierPath bezier15Path = new UIBezierPath();
					bezier15Path.MoveTo(new PointF(41.25f, 4.09f));
					bezier15Path.AddCurveToPoint(new PointF(47.89f, 17.18f), new PointF(44.9f, 7.31f), new PointF(47.89f, 17.18f));
					color6.SetStroke();
					bezier15Path.LineWidth = 3;
					bezier15Path.Stroke();


					//// Bezier 16 Drawing
					UIBezierPath bezier16Path = new UIBezierPath();
					bezier16Path.MoveTo(new PointF(37.78f, 3.55f));
					bezier16Path.AddCurveToPoint(new PointF(44.21f, 14.45f), new PointF(41.43f, 6.76f), new PointF(44.21f, 14.45f));
					color6.SetStroke();
					bezier16Path.LineWidth = 3;
					bezier16Path.Stroke();
				}


				//// Oval Drawing
				var ovalPath = UIBezierPath.FromOval(new RectangleF(14.5f, 12.5f, 24, 19));
				color3.SetFill();
				ovalPath.Fill();

				////// Oval Inner Shadow
				var ovalBorderRect = ovalPath.Bounds;
				ovalBorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
				ovalBorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
				ovalBorderRect = RectangleF.Union(ovalBorderRect, ovalPath.Bounds);
				ovalBorderRect.Inflate(1, 1);

				var ovalNegativePath = UIBezierPath.FromRect(ovalBorderRect);
				ovalNegativePath.AppendPath(ovalPath);
				ovalNegativePath.UsesEvenOddFillRule = true;

				context.SaveState();
				{
					var xOffset = shadowOffset.Width + (float)Math.Round(ovalBorderRect.Width);
					var yOffset = shadowOffset.Height;
					context.SetShadowWithColor(
						new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadowBlurRadius,
						shadow);

					ovalPath.AddClip();
					var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(ovalBorderRect.Width), 0);
					ovalNegativePath.ApplyTransform(transform);
					UIColor.Gray.SetFill();
					ovalNegativePath.Fill();
				}
				context.RestoreState();

				color7.SetStroke();
				ovalPath.LineWidth = 1;
				ovalPath.Stroke();


				//// Oval 3 Drawing
				var oval3Path = UIBezierPath.FromOval(new RectangleF(22.5f, 17.5f, 9, 8));
				color5.SetFill();
				oval3Path.Fill();
				color5.SetStroke();
				oval3Path.LineWidth = 1;
				oval3Path.Stroke();


				//// Oval 2 Drawing
				var oval2Path = UIBezierPath.FromOval(new RectangleF(30.5f, 15.5f, 6, 4));
				color8.SetFill();
				oval2Path.Fill();
				color7.SetStroke();
				oval2Path.LineWidth = 1;
				oval2Path.Stroke();


				//// Oval 4 Drawing
				var oval4Path = UIBezierPath.FromOval(new RectangleF(15.5f, 19.5f, 5, 6));
				color8.SetFill();
				oval4Path.Fill();
				color7.SetStroke();
				oval4Path.LineWidth = 1;
				oval4Path.Stroke();


				//// Oval 5 Drawing
				var oval5Path = UIBezierPath.FromOval(new RectangleF(21.5f, 25.5f, 5, 4));
				color8.SetFill();
				oval5Path.Fill();
				color7.SetStroke();
				oval5Path.LineWidth = 1;
				oval5Path.Stroke();


				//// Bezier 18 Drawing
				UIBezierPath bezier18Path = new UIBezierPath();
				bezier18Path.MoveTo(new PointF(23.75f, 12.97f));
				bezier18Path.AddCurveToPoint(new PointF(32.75f, 14.62f), new PointF(28.7f, 11.66f), new PointF(35.17f, 13.14f));
				bezier18Path.AddCurveToPoint(new PointF(30.15f, 16.78f), new PointF(31.31f, 15.5f), new PointF(30.54f, 16.21f));
				bezier18Path.AddCurveToPoint(new PointF(30.25f, 18.48f), new PointF(29.32f, 17.97f), new PointF(30.25f, 18.48f));
				bezier18Path.AddCurveToPoint(new PointF(26.25f, 17.38f), new PointF(30.25f, 18.48f), new PointF(28.1f, 17.38f));
				bezier18Path.AddCurveToPoint(new PointF(23.75f, 18.48f), new PointF(24.4f, 17.38f), new PointF(23.75f, 18.48f));
				bezier18Path.AddLineTo(new PointF(22.25f, 20.14f));
				bezier18Path.AddLineTo(new PointF(16.75f, 18.48f));
				bezier18Path.AddCurveToPoint(new PointF(23.75f, 12.97f), new PointF(16.75f, 18.48f), new PointF(19.48f, 14.09f));
				bezier18Path.ClosePath();
				color3.SetFill();
				bezier18Path.Fill();

				////// Bezier 18 Inner Shadow
				var bezier18BorderRect = bezier18Path.Bounds;
				bezier18BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
				bezier18BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
				bezier18BorderRect = RectangleF.Union(bezier18BorderRect, bezier18Path.Bounds);
				bezier18BorderRect.Inflate(1, 1);

				var bezier18NegativePath = UIBezierPath.FromRect(bezier18BorderRect);
				bezier18NegativePath.AppendPath(bezier18Path);
				bezier18NegativePath.UsesEvenOddFillRule = true;

				context.SaveState();
				{
					var xOffset = shadowOffset.Width + (float)Math.Round(bezier18BorderRect.Width);
					var yOffset = shadowOffset.Height;
					context.SetShadowWithColor(
						new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadowBlurRadius,
						shadow);

					bezier18Path.AddClip();
					var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(bezier18BorderRect.Width), 0);
					bezier18NegativePath.ApplyTransform(transform);
					UIColor.Gray.SetFill();
					bezier18NegativePath.Fill();
				}
				context.RestoreState();

				color7.SetStroke();
				bezier18Path.LineWidth = 1;
				bezier18Path.Stroke();
			}



		}
	}
}

