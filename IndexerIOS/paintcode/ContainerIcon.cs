using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;

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

					UIColor.Orange.SetStroke();
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

					UIColor.Orange.SetStroke();
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

					UIColor.Orange.SetStroke();
					rectangle2Path.LineWidth = 1;
					rectangle2Path.Stroke();
				}
			}



		}

		static void paintCodeNonRetina ()
		{
			//// General Declarations
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			UIColor shadowColor2 = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.448f);

			//// Shadow Declarations
			var shadow = shadowColor2.CGColor;
			var shadowOffset = new SizeF(0.1f, -0.1f);
			var shadowBlurRadius = 16;

			//// Abstracted Attributes
			var textContent = "DCTAPPS";


			//// Rectangle Drawing
			var rectanglePath = UIBezierPath.FromRect(new RectangleF(12.5f, 6.5f, 31, 23));
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

			UIColor.Orange.SetStroke();
			rectanglePath.LineWidth = 1;
			rectanglePath.Stroke();


			//// Rectangle 2 Drawing
			UIBezierPath rectangle2Path = new UIBezierPath();
			rectangle2Path.MoveTo(new PointF(4.13f, 22.1f));
			rectangle2Path.AddLineTo(new PointF(12.81f, 29.83f));
			rectangle2Path.AddLineTo(new PointF(12.81f, 6.65f));
			rectangle2Path.AddLineTo(new PointF(4.13f, 3.17f));
			rectangle2Path.AddLineTo(new PointF(4.13f, 22.1f));
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

			UIColor.Orange.SetStroke();
			rectangle2Path.LineWidth = 1;
			rectangle2Path.Stroke();


			//// Text Drawing
			var textRect = new RectangleF(28, 35, 212, 63);
			UIColor.Black.SetFill();
			new NSString(textContent).DrawString(textRect, UIFont.FromName("Helvetica", 41), UILineBreakMode.WordWrap, UITextAlignment.Center);


			//// Bezier Drawing
			UIBezierPath bezierPath = new UIBezierPath();
			bezierPath.MoveTo(new PointF(4.5f, 3.5f));
			bezierPath.AddCurveToPoint(new PointF(12.5f, 6.5f), new PointF(12.09f, 6.5f), new PointF(12.5f, 6.5f));
			bezierPath.AddLineTo(new PointF(42.5f, 6.5f));
			bezierPath.AddLineTo(new PointF(34.86f, 3.5f));
			bezierPath.AddLineTo(new PointF(4.5f, 3.5f));
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

			UIColor.Orange.SetStroke();
			bezierPath.LineWidth = 1;
			bezierPath.Stroke();



		}
	}
}

