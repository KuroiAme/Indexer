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
				UIColor shadowColor2 = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.454f);
				UIColor color4 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);

				//// Shadow Declarations
				var shadow = shadowColor2.ColorWithAlpha(0.5f).CGColor;
				var shadowOffset = new SizeF(0.1f, -1.1f);
				var shadowBlurRadius = 10;

				//// Group
				{
					//// Rectangle Drawing
					var rectanglePath = UIBezierPath.FromRect(new RectangleF(20.5f, 15.5f, 0, 0));
					color4.SetFill();
					rectanglePath.Fill();
					color4.SetStroke();
					rectanglePath.LineWidth = 1;
					rectanglePath.Stroke();


					//// Rectangle 2 Drawing
					UIBezierPath rectangle2Path = new UIBezierPath();
					rectangle2Path.MoveTo(new PointF(20.72f, 49.5f));
					rectangle2Path.AddCurveToPoint(new PointF(45.5f, 54.5f), new PointF(20.72f, 49.5f), new PointF(32.97f, 54.5f));
					rectangle2Path.AddCurveToPoint(new PointF(70.84f, 49.5f), new PointF(58.03f, 54.5f), new PointF(70.84f, 49.5f));
					rectangle2Path.AddLineTo(new PointF(70.5f, 9.5f));
					rectangle2Path.AddCurveToPoint(new PointF(59.27f, 15), new PointF(70.5f, 9.5f), new PointF(68.43f, 13.56f));
					rectangle2Path.AddCurveToPoint(new PointF(34.21f, 15), new PointF(50.12f, 16.44f), new PointF(41.85f, 15.58f));
					rectangle2Path.AddCurveToPoint(new PointF(20.72f, 9.25f), new PointF(26.58f, 14.41f), new PointF(20.72f, 9.25f));
					rectangle2Path.AddLineTo(new PointF(20.72f, 49.5f));
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
					ovalPath.MoveTo(new PointF(41.75f, 29.14f));
					ovalPath.AddCurveToPoint(new PointF(41.75f, 29.14f), new PointF(41.64f, 28.93f), new PointF(41.75f, 29.14f));
					ovalPath.AddCurveToPoint(new PointF(41.75f, 29.14f), new PointF(41.75f, 29.14f), new PointF(41.85f, 29.34f));
					ovalPath.ClosePath();
					ovalPath.MoveTo(new PointF(71.09f, 40.78f));
					ovalPath.AddCurveToPoint(new PointF(85.5f, 53.5f), new PointF(71.09f, 40.78f), new PointF(90.3f, 44.46f));
					ovalPath.AddCurveToPoint(new PointF(71.5f, 60.5f), new PointF(83.59f, 57.1f), new PointF(79.87f, 58.44f));
					ovalPath.AddCurveToPoint(new PointF(43.5f, 63.5f), new PointF(68.37f, 61.27f), new PointF(55.33f, 63.35f));
					ovalPath.AddCurveToPoint(new PointF(19.98f, 60.61f), new PointF(32.61f, 63.64f), new PointF(22.5f, 61.09f));
					ovalPath.AddCurveToPoint(new PointF(6.48f, 53.09f), new PointF(14.84f, 59.63f), new PointF(6.96f, 54.65f));
					ovalPath.AddCurveToPoint(new PointF(19.98f, 40.78f), new PointF(5.15f, 48.79f), new PointF(10.39f, 43.13f));
					ovalPath.AddLineTo(new PointF(19.98f, 49.64f));
					ovalPath.AddCurveToPoint(new PointF(45.5f, 53.5f), new PointF(19.98f, 49.64f), new PointF(32.72f, 53.5f));
					ovalPath.AddCurveToPoint(new PointF(71.09f, 49.64f), new PointF(58.28f, 53.5f), new PointF(71.09f, 49.64f));
					ovalPath.AddLineTo(new PointF(71.09f, 40.78f));
					ovalPath.ClosePath();
					ovalPath.MiterLimit = 6;

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
					var oval2Path = UIBezierPath.FromOval(new RectangleF(20.5f, 3.5f, 50, 12));
					color4.SetFill();
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
				var context = UIGraphics.GetCurrentContext();

				//// Color Declarations
				UIColor shadowColor2 = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.454f);
				UIColor color4 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);

				//// Shadow Declarations
				var shadow = shadowColor2.ColorWithAlpha(0.5f).CGColor;
				var shadowOffset = new SizeF(0.1f, -1.1f);
				var shadowBlurRadius = 10;

				//// Group
				{
					//// Rectangle Drawing
					var rectanglePath = UIBezierPath.FromRect(new RectangleF(10.5f, 7.5f, 0, 0));
					color4.SetFill();
					rectanglePath.Fill();
					color4.SetStroke();
					rectanglePath.LineWidth = 1;
					rectanglePath.Stroke();


					//// Rectangle 2 Drawing
					UIBezierPath rectangle2Path = new UIBezierPath();
					rectangle2Path.MoveTo(new PointF(9.81f, 24.87f));
					rectangle2Path.AddCurveToPoint(new PointF(22.97f, 27.33f), new PointF(9.81f, 24.87f), new PointF(16.32f, 27.33f));
					rectangle2Path.AddCurveToPoint(new PointF(36.42f, 24.87f), new PointF(29.62f, 27.33f), new PointF(36.42f, 24.87f));
					rectangle2Path.AddLineTo(new PointF(36.24f, 5.2f));
					rectangle2Path.AddCurveToPoint(new PointF(30.28f, 7.9f), new PointF(36.24f, 5.2f), new PointF(35.14f, 7.19f));
					rectangle2Path.AddCurveToPoint(new PointF(16.98f, 7.9f), new PointF(25.42f, 8.61f), new PointF(21.03f, 8.19f));
					rectangle2Path.AddCurveToPoint(new PointF(9.81f, 5.07f), new PointF(12.92f, 7.61f), new PointF(9.81f, 5.07f));
					rectangle2Path.AddLineTo(new PointF(9.81f, 24.87f));
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
					ovalPath.MoveTo(new PointF(20.98f, 14.86f));
					ovalPath.AddCurveToPoint(new PointF(20.98f, 14.86f), new PointF(20.92f, 14.75f), new PointF(20.98f, 14.86f));
					ovalPath.AddCurveToPoint(new PointF(20.98f, 14.86f), new PointF(20.98f, 14.86f), new PointF(21.03f, 14.96f));
					ovalPath.ClosePath();
					ovalPath.MoveTo(new PointF(36.56f, 20.58f));
					ovalPath.AddCurveToPoint(new PointF(44.2f, 26.84f), new PointF(36.56f, 20.58f), new PointF(46.75f, 22.39f));
					ovalPath.AddCurveToPoint(new PointF(36.77f, 30.28f), new PointF(43.19f, 28.61f), new PointF(41.21f, 29.27f));
					ovalPath.AddCurveToPoint(new PointF(21.91f, 31.75f), new PointF(35.11f, 30.66f), new PointF(28.18f, 31.68f));
					ovalPath.AddCurveToPoint(new PointF(9.42f, 30.33f), new PointF(16.13f, 31.82f), new PointF(10.76f, 30.57f));
					ovalPath.AddCurveToPoint(new PointF(2.25f, 26.63f), new PointF(6.69f, 29.85f), new PointF(2.51f, 27.4f));
					ovalPath.AddCurveToPoint(new PointF(9.42f, 20.58f), new PointF(1.55f, 24.52f), new PointF(4.33f, 21.73f));
					ovalPath.AddLineTo(new PointF(9.42f, 24.94f));
					ovalPath.AddCurveToPoint(new PointF(22.97f, 26.84f), new PointF(9.42f, 24.94f), new PointF(16.19f, 26.84f));
					ovalPath.AddCurveToPoint(new PointF(36.56f, 24.94f), new PointF(29.75f, 26.84f), new PointF(36.56f, 24.94f));
					ovalPath.AddLineTo(new PointF(36.56f, 20.58f));
					ovalPath.ClosePath();
					ovalPath.MiterLimit = 6;

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
					var oval2Path = UIBezierPath.FromOval(new RectangleF(10.5f, 2.5f, 25, 5));
					color4.SetFill();
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



				// END PAINTCODE
			}
			var converted = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return converted;

		}
	}
}

