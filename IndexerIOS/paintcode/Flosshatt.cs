using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace GarageIndex
{
	public class Flosshatt
	{
		public Flosshatt ()
		{
		}

		public static UIImage MakeFlosshatt (){
			bool retina = (UIScreen.MainScreen.Scale > 1.0);
			if (retina) {
				UIGraphics.BeginImageContext (new System.Drawing.SizeF (96, 64));

				///BEGIN PAINTCODE RETINA

				//// General Declarations
				var context = UIGraphics.GetCurrentContext();

				//// Color Declarations
				UIColor color = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 1.000f);
				UIColor color2 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.000f);
				UIColor shadowColor2 = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.454f);
				UIColor color4 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);

				//// Shadow Declarations
				var shadow = shadowColor2.ColorWithAlpha(0.5f).CGColor;
				var shadowOffset = new SizeF(0.1f, 1.1f);
				var shadowBlurRadius = 5;

				//// Polygon Drawing
				UIBezierPath polygonPath = new UIBezierPath();
				polygonPath.MoveTo(new PointF(4.5f, 5.5f));
				polygonPath.AddLineTo(new PointF(4.5f, 5.5f));
				polygonPath.AddLineTo(new PointF(4.5f, 5.5f));
				polygonPath.AddLineTo(new PointF(4.5f, 5.5f));
				polygonPath.AddLineTo(new PointF(4.5f, 5.5f));
				polygonPath.ClosePath();
				UIColor.White.SetFill();
				polygonPath.Fill();
				UIColor.Black.SetStroke();
				polygonPath.LineWidth = 1;
				polygonPath.Stroke();


				//// Group
				{
					//// Star Drawing
					UIBezierPath starPath = new UIBezierPath();
					starPath.MoveTo(new PointF(14.5f, 6.5f));
					starPath.AddLineTo(new PointF(14.5f, 6.5f));
					starPath.AddLineTo(new PointF(14.5f, 6.5f));
					starPath.AddLineTo(new PointF(14.5f, 6.5f));
					starPath.AddLineTo(new PointF(14.5f, 6.5f));
					starPath.AddLineTo(new PointF(14.5f, 6.5f));
					starPath.AddLineTo(new PointF(14.5f, 6.5f));
					starPath.AddLineTo(new PointF(14.5f, 6.5f));
					starPath.AddLineTo(new PointF(14.5f, 6.5f));
					starPath.AddLineTo(new PointF(14.5f, 6.5f));
					starPath.ClosePath();
					UIColor.White.SetFill();
					starPath.Fill();
					UIColor.Black.SetStroke();
					starPath.LineWidth = 1;
					starPath.Stroke();


					//// Rectangle Drawing
					var rectanglePath = UIBezierPath.FromRect(new RectangleF(18.5f, 14.5f, 0, 0));
					color2.SetFill();
					rectanglePath.Fill();
					color.SetStroke();
					rectanglePath.LineWidth = 1;
					rectanglePath.Stroke();


					//// Rectangle 2 Drawing
					UIBezierPath rectangle2Path = new UIBezierPath();
					rectangle2Path.MoveTo(new PointF(19.44f, 50.58f));
					rectangle2Path.AddLineTo(new PointF(78.92f, 50.58f));
					rectangle2Path.AddLineTo(new PointF(78.92f, 7.23f));
					rectangle2Path.AddLineTo(new PointF(65.2f, 13.42f));
					rectangle2Path.AddLineTo(new PointF(35.46f, 13.42f));
					rectangle2Path.AddLineTo(new PointF(28.96f, 10.91f));
					rectangle2Path.AddLineTo(new PointF(19.44f, 7.23f));
					rectangle2Path.AddLineTo(new PointF(19.44f, 50.58f));
					rectangle2Path.ClosePath();
					color4.SetFill();
					rectangle2Path.Fill();

					////// Rectangle 2 Inner Shadow
					var rectangle2BorderRect = rectangle2Path.Bounds;
					rectangle2BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
					rectangle2BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
					rectangle2BorderRect = RectangleF.Union(rectangle2BorderRect, rectangle2Path.Bounds);
					rectangle2BorderRect.Inflate(1, 1);

					var rectangle2NegativePath = UIBezierPath.FromRect(rectangle2BorderRect);
					rectangle2NegativePath.AppendPath(rectangle2Path);
					rectangle2NegativePath.UsesEvenOddFillRule = true;

					context.SaveState();
					{
						var xOffset = shadowOffset.Width + (float)Math.Round(rectangle2BorderRect.Width);
						var yOffset = shadowOffset.Height;
						context.SetShadowWithColor(
							new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
							shadowBlurRadius,
							shadow);

						rectangle2Path.AddClip();
						var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(rectangle2BorderRect.Width), 0);
						rectangle2NegativePath.ApplyTransform(transform);
						UIColor.Gray.SetFill();
						rectangle2NegativePath.Fill();
					}
					context.RestoreState();

					color4.SetStroke();
					rectangle2Path.LineWidth = 1;
					rectangle2Path.Stroke();


					//// Oval Drawing
					UIBezierPath ovalPath = new UIBezierPath();
					ovalPath.MoveTo(new PointF(80.61f, 58.94f));
					ovalPath.AddCurveToPoint(new PointF(80.61f, 46.35f), new PointF(98.21f, 55.16f), new PointF(98.21f, 50.14f));
					ovalPath.AddCurveToPoint(new PointF(13.2f, 46.35f), new PointF(62.02f, 42.36f), new PointF(31.79f, 42.36f));
					ovalPath.AddCurveToPoint(new PointF(13.2f, 58.94f), new PointF(-4.4f, 50.14f), new PointF(-4.4f, 55.16f));
					ovalPath.AddCurveToPoint(new PointF(80.61f, 58.94f), new PointF(31.79f, 62.93f), new PointF(62.02f, 62.93f));
					ovalPath.ClosePath();
					ovalPath.MoveTo(new PointF(81.12f, 60.95f));
					ovalPath.AddCurveToPoint(new PointF(12.7f, 60.95f), new PointF(62.2f, 65.02f), new PointF(31.62f, 65.02f));
					ovalPath.AddCurveToPoint(new PointF(20.1f, 45.42f), new PointF(-7.21f, 56.67f), new PointF(0.2f, 49.7f));
					ovalPath.AddCurveToPoint(new PointF(22.34f, 51.61f), new PointF(24.18f, 44.54f), new PointF(17.56f, 57.44f));
					ovalPath.AddCurveToPoint(new PointF(35.74f, 51.61f), new PointF(25.32f, 47.98f), new PointF(17.36f, 50.89f));
					ovalPath.AddCurveToPoint(new PointF(69.24f, 51.61f), new PointF(50.06f, 52.17f), new PointF(58.45f, 50.67f));
					ovalPath.AddCurveToPoint(new PointF(78.18f, 43.35f), new PointF(76.7f, 52.27f), new PointF(76.94f, 43.09f));
					ovalPath.AddCurveToPoint(new PointF(81.12f, 60.95f), new PointF(98.08f, 47.63f), new PointF(101.02f, 56.67f));
					ovalPath.ClosePath();
					color4.SetFill();
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

					color4.SetStroke();
					ovalPath.LineWidth = 1;
					ovalPath.Stroke();


					//// Oval 2 Drawing
					var oval2Path = UIBezierPath.FromOval(new RectangleF(18.5f, 0.5f, 60, 14));
					color2.SetFill();
					oval2Path.Fill();

					////// Oval 2 Inner Shadow
					var oval2BorderRect = oval2Path.Bounds;
					oval2BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
					oval2BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
					oval2BorderRect = RectangleF.Union(oval2BorderRect, oval2Path.Bounds);
					oval2BorderRect.Inflate(1, 1);

					var oval2NegativePath = UIBezierPath.FromRect(oval2BorderRect);
					oval2NegativePath.AppendPath(oval2Path);
					oval2NegativePath.UsesEvenOddFillRule = true;

					context.SaveState();
					{
						var xOffset = shadowOffset.Width + (float)Math.Round(oval2BorderRect.Width);
						var yOffset = shadowOffset.Height;
						context.SetShadowWithColor(
							new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
							shadowBlurRadius,
							shadow);

						oval2Path.AddClip();
						var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(oval2BorderRect.Width), 0);
						oval2NegativePath.ApplyTransform(transform);
						UIColor.Gray.SetFill();
						oval2NegativePath.Fill();
					}
					context.RestoreState();

					color4.SetStroke();
					oval2Path.LineWidth = 1;
					oval2Path.Stroke();
				}




				// END PAINTCODE RETINA

			} else {
				UIGraphics.BeginImageContext (new System.Drawing.SizeF (48, 32));

				//start paintcode
				//// General Declarations
				var context = UIGraphics.GetCurrentContext ();

				//// Color Declarations
				UIColor color = UIColor.FromRGBA (1.000f, 1.000f, 1.000f, 1.000f);
				UIColor color2 = UIColor.FromRGBA (0.000f, 0.000f, 0.000f, 0.000f);
				UIColor shadowColor2 = UIColor.FromRGBA (1.000f, 1.000f, 1.000f, 0.454f);
				UIColor color4 = UIColor.FromRGBA (0.000f, 0.000f, 0.000f, 1.000f);

				//// Shadow Declarations
				var shadow = shadowColor2.ColorWithAlpha (0.5f).CGColor;
				var shadowOffset = new SizeF (0.1f, 1.1f);
				var shadowBlurRadius = 5;

				//// Star Drawing
				UIBezierPath starPath = new UIBezierPath ();
				starPath.MoveTo (new PointF (9.5f, 4.5f));
				starPath.AddLineTo (new PointF (9.5f, 4.5f));
				starPath.AddLineTo (new PointF (9.5f, 4.5f));
				starPath.AddLineTo (new PointF (9.5f, 4.5f));
				starPath.AddLineTo (new PointF (9.5f, 4.5f));
				starPath.AddLineTo (new PointF (9.5f, 4.5f));
				starPath.AddLineTo (new PointF (9.5f, 4.5f));
				starPath.AddLineTo (new PointF (9.5f, 4.5f));
				starPath.AddLineTo (new PointF (9.5f, 4.5f));
				starPath.AddLineTo (new PointF (9.5f, 4.5f));
				starPath.ClosePath ();
				UIColor.White.SetFill ();
				starPath.Fill ();
				UIColor.Black.SetStroke ();
				starPath.LineWidth = 1;
				starPath.Stroke ();


				//// Polygon Drawing
				UIBezierPath polygonPath = new UIBezierPath ();
				polygonPath.MoveTo (new PointF (4.5f, 5.5f));
				polygonPath.AddLineTo (new PointF (4.5f, 5.5f));
				polygonPath.AddLineTo (new PointF (4.5f, 5.5f));
				polygonPath.AddLineTo (new PointF (4.5f, 5.5f));
				polygonPath.AddLineTo (new PointF (4.5f, 5.5f));
				polygonPath.ClosePath ();
				UIColor.White.SetFill ();
				polygonPath.Fill ();
				UIColor.Black.SetStroke ();
				polygonPath.LineWidth = 1;
				polygonPath.Stroke ();


				//// Rectangle Drawing
				var rectanglePath = UIBezierPath.FromRect (new RectangleF (11.5f, 7.5f, 0, 0));
				color2.SetFill ();
				rectanglePath.Fill ();
				color.SetStroke ();
				rectanglePath.LineWidth = 1;
				rectanglePath.Stroke ();


				//// Rectangle 2 Drawing
				UIBezierPath rectangle2Path = new UIBezierPath ();
				rectangle2Path.MoveTo (new PointF (11.5f, 25.5f));
				rectangle2Path.AddLineTo (new PointF (37.5f, 25.5f));
				rectangle2Path.AddLineTo (new PointF (37.5f, 4.5f));
				rectangle2Path.AddLineTo (new PointF (31.5f, 7.5f));
				rectangle2Path.AddLineTo (new PointF (18.5f, 7.5f));
				rectangle2Path.AddLineTo (new PointF (15.66f, 6.28f));
				rectangle2Path.AddLineTo (new PointF (11.5f, 4.5f));
				rectangle2Path.AddLineTo (new PointF (11.5f, 25.5f));
				rectangle2Path.ClosePath ();
				color4.SetFill ();
				rectangle2Path.Fill ();

				////// Rectangle 2 Inner Shadow
				var rectangle2BorderRect = rectangle2Path.Bounds;
				rectangle2BorderRect.Inflate (shadowBlurRadius, shadowBlurRadius);
				rectangle2BorderRect.Offset (-shadowOffset.Width, -shadowOffset.Height);
				rectangle2BorderRect = RectangleF.Union (rectangle2BorderRect, rectangle2Path.Bounds);
				rectangle2BorderRect.Inflate (1, 1);

				var rectangle2NegativePath = UIBezierPath.FromRect (rectangle2BorderRect);
				rectangle2NegativePath.AppendPath (rectangle2Path);
				rectangle2NegativePath.UsesEvenOddFillRule = true;

				context.SaveState ();
				{
					var xOffset = shadowOffset.Width + (float)Math.Round (rectangle2BorderRect.Width);
					var yOffset = shadowOffset.Height;
					context.SetShadowWithColor (
						new SizeF (xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadowBlurRadius,
						shadow);

					rectangle2Path.AddClip ();
					var transform = CGAffineTransform.MakeTranslation (-(float)Math.Round (rectangle2BorderRect.Width), 0);
					rectangle2NegativePath.ApplyTransform (transform);
					UIColor.Gray.SetFill ();
					rectangle2NegativePath.Fill ();
				}
				context.RestoreState ();

				color4.SetStroke ();
				rectangle2Path.LineWidth = 1;
				rectangle2Path.Stroke ();


				//// Oval Drawing
				UIBezierPath ovalPath = new UIBezierPath ();
				ovalPath.MoveTo (new PointF (39.09f, 28.04f));
				ovalPath.AddCurveToPoint (new PointF (39.09f, 23.16f), new PointF (46.97f, 26.57f), new PointF (46.97f, 24.63f));
				ovalPath.AddCurveToPoint (new PointF (8.91f, 23.16f), new PointF (30.77f, 21.61f), new PointF (17.23f, 21.61f));
				ovalPath.AddCurveToPoint (new PointF (8.91f, 28.04f), new PointF (1.03f, 24.63f), new PointF (1.03f, 26.57f));
				ovalPath.AddCurveToPoint (new PointF (39.09f, 28.04f), new PointF (17.23f, 29.59f), new PointF (30.77f, 29.59f));
				ovalPath.ClosePath ();
				ovalPath.MoveTo (new PointF (39.32f, 28.82f));
				ovalPath.AddCurveToPoint (new PointF (8.68f, 28.82f), new PointF (30.85f, 30.39f), new PointF (17.15f, 30.39f));
				ovalPath.AddCurveToPoint (new PointF (12, 22.8f), new PointF (-0.23f, 27.16f), new PointF (3.09f, 24.46f));
				ovalPath.AddCurveToPoint (new PointF (13, 25.2f), new PointF (13.82f, 22.46f), new PointF (10.86f, 27.46f));
				ovalPath.AddCurveToPoint (new PointF (19, 25.2f), new PointF (14.33f, 23.79f), new PointF (10.77f, 24.92f));
				ovalPath.AddCurveToPoint (new PointF (34, 25.2f), new PointF (25.41f, 25.42f), new PointF (29.17f, 24.83f));
				ovalPath.AddCurveToPoint (new PointF (38, 22), new PointF (37.34f, 25.45f), new PointF (37.45f, 21.9f));
				ovalPath.AddCurveToPoint (new PointF (39.32f, 28.82f), new PointF (46.91f, 23.66f), new PointF (48.23f, 27.16f));
				ovalPath.ClosePath ();
				color4.SetFill ();
				ovalPath.Fill ();

				////// Oval Inner Shadow
				var ovalBorderRect = ovalPath.Bounds;
				ovalBorderRect.Inflate (shadowBlurRadius, shadowBlurRadius);
				ovalBorderRect.Offset (-shadowOffset.Width, -shadowOffset.Height);
				ovalBorderRect = RectangleF.Union (ovalBorderRect, ovalPath.Bounds);
				ovalBorderRect.Inflate (1, 1);

				var ovalNegativePath = UIBezierPath.FromRect (ovalBorderRect);
				ovalNegativePath.AppendPath (ovalPath);
				ovalNegativePath.UsesEvenOddFillRule = true;

				context.SaveState ();
				{
					var xOffset = shadowOffset.Width + (float)Math.Round (ovalBorderRect.Width);
					var yOffset = shadowOffset.Height;
					context.SetShadowWithColor (
						new SizeF (xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadowBlurRadius,
						shadow);

					ovalPath.AddClip ();
					var transform = CGAffineTransform.MakeTranslation (-(float)Math.Round (ovalBorderRect.Width), 0);
					ovalNegativePath.ApplyTransform (transform);
					UIColor.Gray.SetFill ();
					ovalNegativePath.Fill ();
				}
				context.RestoreState ();

				color4.SetStroke ();
				ovalPath.LineWidth = 1;
				ovalPath.Stroke ();


				//// Oval 2 Drawing
				var oval2Path = UIBezierPath.FromOval (new RectangleF (11.5f, 1.5f, 26, 6));
				color2.SetFill ();
				oval2Path.Fill ();

				////// Oval 2 Inner Shadow
				var oval2BorderRect = oval2Path.Bounds;
				oval2BorderRect.Inflate (shadowBlurRadius, shadowBlurRadius);
				oval2BorderRect.Offset (-shadowOffset.Width, -shadowOffset.Height);
				oval2BorderRect = RectangleF.Union (oval2BorderRect, oval2Path.Bounds);
				oval2BorderRect.Inflate (1, 1);

				var oval2NegativePath = UIBezierPath.FromRect (oval2BorderRect);
				oval2NegativePath.AppendPath (oval2Path);
				oval2NegativePath.UsesEvenOddFillRule = true;

				context.SaveState ();
				{
					var xOffset = shadowOffset.Width + (float)Math.Round (oval2BorderRect.Width);
					var yOffset = shadowOffset.Height;
					context.SetShadowWithColor (
						new SizeF (xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadowBlurRadius,
						shadow);

					oval2Path.AddClip ();
					var transform = CGAffineTransform.MakeTranslation (-(float)Math.Round (oval2BorderRect.Width), 0);
					oval2NegativePath.ApplyTransform (transform);
					UIColor.Gray.SetFill ();
					oval2NegativePath.Fill ();
				}
				context.RestoreState ();

				color4.SetStroke ();
				oval2Path.LineWidth = 1;
				oval2Path.Stroke ();



				// END PAINTCODE
			}
			var converted = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return converted;

		}
	}
}

