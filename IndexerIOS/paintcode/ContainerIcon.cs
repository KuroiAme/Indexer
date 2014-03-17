using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace IndexerIOS
{
	public class ContainerIcon
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
			UIColor shadowColor2 = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.448f);
			UIColor color3 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);

			//// Shadow Declarations
			var shadow = shadowColor2.CGColor;
			var shadowOffset = new SizeF(0.1f, -0.1f);
			var shadowBlurRadius = 16;

			//// Group
			{
				//// Group 2
				{
					//// Bezier Drawing
					UIBezierPath bezierPath = new UIBezierPath();
					bezierPath.MoveTo(new PointF(5.1f, 1.11f));
					bezierPath.AddCurveToPoint(new PointF(22.7f, 7.78f), new PointF(21.8f, 7.78f), new PointF(22.7f, 7.78f));
					bezierPath.AddLineTo(new PointF(91.5f, 7.5f));
					bezierPath.AddLineTo(new PointF(71.89f, 1.11f));
					bezierPath.AddLineTo(new PointF(5.1f, 1.11f));
					bezierPath.ClosePath();
					bezierPath.MiterLimit = 6.5f;

					UIColor.Orange.SetFill();
					bezierPath.Fill();

					////// Bezier Inner Shadow
					var bezierBorderRect = bezierPath.Bounds;
					bezierBorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
					bezierBorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
					bezierBorderRect = RectangleF.Union(bezierBorderRect, bezierPath.Bounds);
					bezierBorderRect.Inflate(1, 1);

					var bezierNegativePath = UIBezierPath.FromRect(bezierBorderRect);
					bezierNegativePath.AppendPath(bezierPath);
					bezierNegativePath.UsesEvenOddFillRule = true;

					context.SaveState();
					{
						var xOffset = shadowOffset.Width + (float)Math.Round(bezierBorderRect.Width);
						var yOffset = shadowOffset.Height;
						context.SetShadowWithColor(
							new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
							shadowBlurRadius,
							shadow);

						bezierPath.AddClip();
						var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(bezierBorderRect.Width), 0);
						bezierNegativePath.ApplyTransform(transform);
						UIColor.Gray.SetFill();
						bezierNegativePath.Fill();
					}
					context.RestoreState();

					color3.SetStroke();
					bezierPath.LineWidth = 1;
					bezierPath.Stroke();


					//// Rectangle Drawing
					var rectanglePath = UIBezierPath.FromRect(new RectangleF(23.5f, 7.5f, 68, 52));
					UIColor.Orange.SetFill();
					rectanglePath.Fill();

					////// Rectangle Inner Shadow
					var rectangleBorderRect = rectanglePath.Bounds;
					rectangleBorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
					rectangleBorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
					rectangleBorderRect = RectangleF.Union(rectangleBorderRect, rectanglePath.Bounds);
					rectangleBorderRect.Inflate(1, 1);

					var rectangleNegativePath = UIBezierPath.FromRect(rectangleBorderRect);
					rectangleNegativePath.AppendPath(rectanglePath);
					rectangleNegativePath.UsesEvenOddFillRule = true;

					context.SaveState();
					{
						var xOffset = shadowOffset.Width + (float)Math.Round(rectangleBorderRect.Width);
						var yOffset = shadowOffset.Height;
						context.SetShadowWithColor(
							new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
							shadowBlurRadius,
							shadow);

						rectanglePath.AddClip();
						var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(rectangleBorderRect.Width), 0);
						rectangleNegativePath.ApplyTransform(transform);
						UIColor.Gray.SetFill();
						rectangleNegativePath.Fill();
					}
					context.RestoreState();

					color3.SetStroke();
					rectanglePath.LineWidth = 1;
					rectanglePath.Stroke();


					//// Rectangle 2 Drawing
					UIBezierPath rectangle2Path = new UIBezierPath();
					rectangle2Path.MoveTo(new PointF(4.29f, 42.45f));
					rectangle2Path.AddLineTo(new PointF(23.38f, 59.62f));
					rectangle2Path.AddLineTo(new PointF(23.38f, 8.11f));
					rectangle2Path.AddLineTo(new PointF(4.29f, 0.38f));
					rectangle2Path.AddLineTo(new PointF(4.29f, 42.45f));
					rectangle2Path.ClosePath();
					UIColor.Orange.SetFill();
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

					color3.SetStroke();
					rectangle2Path.LineWidth = 1;
					rectangle2Path.Stroke();
				}
			}


			//// Oval Drawing
			var ovalPath = UIBezierPath.FromOval(new RectangleF(32.5f, 20.5f, 0, 0));
			UIColor.White.SetFill();
			ovalPath.Fill();
			UIColor.Black.SetStroke();
			ovalPath.LineWidth = 1;
			ovalPath.Stroke();



		}

		static void paintCodeNonRetina ()
		{
			//// General Declarations
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			UIColor shadowColor2 = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.448f);
			UIColor color3 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);

			//// Shadow Declarations
			var shadow = shadowColor2.CGColor;
			var shadowOffset = new SizeF(0.1f, -0.1f);
			var shadowBlurRadius = 16;

			//// Group 2
			{
				//// Bezier Drawing
				UIBezierPath bezierPath = new UIBezierPath();
				bezierPath.MoveTo(new PointF(2.56f, 2.54f));
				bezierPath.AddCurveToPoint(new PointF(11.56f, 5.76f), new PointF(11.1f, 5.76f), new PointF(11.56f, 5.76f));
				bezierPath.AddLineTo(new PointF(46.74f, 5.62f));
				bezierPath.AddLineTo(new PointF(36.72f, 2.54f));
				bezierPath.AddLineTo(new PointF(2.56f, 2.54f));
				bezierPath.ClosePath();
				bezierPath.MiterLimit = 6.5f;

				UIColor.Orange.SetFill();
				bezierPath.Fill();

				////// Bezier Inner Shadow
				var bezierBorderRect = bezierPath.Bounds;
				bezierBorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
				bezierBorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
				bezierBorderRect = RectangleF.Union(bezierBorderRect, bezierPath.Bounds);
				bezierBorderRect.Inflate(1, 1);

				var bezierNegativePath = UIBezierPath.FromRect(bezierBorderRect);
				bezierNegativePath.AppendPath(bezierPath);
				bezierNegativePath.UsesEvenOddFillRule = true;

				context.SaveState();
				{
					var xOffset = shadowOffset.Width + (float)Math.Round(bezierBorderRect.Width);
					var yOffset = shadowOffset.Height;
					context.SetShadowWithColor(
						new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadowBlurRadius,
						shadow);

					bezierPath.AddClip();
					var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(bezierBorderRect.Width), 0);
					bezierNegativePath.ApplyTransform(transform);
					UIColor.Gray.SetFill();
					bezierNegativePath.Fill();
				}
				context.RestoreState();

				color3.SetStroke();
				bezierPath.LineWidth = 1;
				bezierPath.Stroke();


				//// Rectangle Drawing
				var rectanglePath = UIBezierPath.FromRect(new RectangleF(12.5f, 5.5f, 34, 25));
				UIColor.Orange.SetFill();
				rectanglePath.Fill();

				////// Rectangle Inner Shadow
				var rectangleBorderRect = rectanglePath.Bounds;
				rectangleBorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
				rectangleBorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
				rectangleBorderRect = RectangleF.Union(rectangleBorderRect, rectanglePath.Bounds);
				rectangleBorderRect.Inflate(1, 1);

				var rectangleNegativePath = UIBezierPath.FromRect(rectangleBorderRect);
				rectangleNegativePath.AppendPath(rectanglePath);
				rectangleNegativePath.UsesEvenOddFillRule = true;

				context.SaveState();
				{
					var xOffset = shadowOffset.Width + (float)Math.Round(rectangleBorderRect.Width);
					var yOffset = shadowOffset.Height;
					context.SetShadowWithColor(
						new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadowBlurRadius,
						shadow);

					rectanglePath.AddClip();
					var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(rectangleBorderRect.Width), 0);
					rectangleNegativePath.ApplyTransform(transform);
					UIColor.Gray.SetFill();
					rectangleNegativePath.Fill();
				}
				context.RestoreState();

				color3.SetStroke();
				rectanglePath.LineWidth = 1;
				rectanglePath.Stroke();


				//// Rectangle 2 Drawing
				UIBezierPath rectangle2Path = new UIBezierPath();
				rectangle2Path.MoveTo(new PointF(2.15f, 22.52f));
				rectangle2Path.AddLineTo(new PointF(11.91f, 30.82f));
				rectangle2Path.AddLineTo(new PointF(11.91f, 5.92f));
				rectangle2Path.AddLineTo(new PointF(2.15f, 2.18f));
				rectangle2Path.AddLineTo(new PointF(2.15f, 22.52f));
				rectangle2Path.ClosePath();
				UIColor.Orange.SetFill();
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

				color3.SetStroke();
				rectangle2Path.LineWidth = 1;
				rectangle2Path.Stroke();
			}



		}
	}
}

