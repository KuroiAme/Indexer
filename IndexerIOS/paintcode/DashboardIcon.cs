using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace IndexerIOS
{
	public static class DashboardIcon
	{
		public static UIImage MakeIconImage (){
			bool retina = (UIScreen.MainScreen.Scale > 1.0);
			if (retina) {
				UIGraphics.BeginImageContext (new SizeF (96, 64));
				PaintCodeDrawRetina ();
			}else{
				UIGraphics.BeginImageContext (new SizeF (48, 32));
				PaintCodeDrawNonRetina ();
			}

			var converted = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return converted;

		}

		static void PaintCodeDrawRetina ()
		{
			//// General Declarations
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			UIColor color = UIColor.FromRGBA(0.833f, 0.833f, 0.833f, 1.000f);
			UIColor color3 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);

			//// Shadow Declarations
			var shadow = UIColor.Black.ColorWithAlpha(0.66f).CGColor;
			var shadowOffset = new SizeF(0.1f, -0.1f);
			var shadowBlurRadius = 17;

			//// Group 2
			{
				//// Group
				{
					//// Oval Drawing
					var ovalPath = UIBezierPath.FromOval(new RectangleF(16.5f, 3.5f, 60, 57));
					color.SetFill();
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

					color3.SetStroke();
					ovalPath.LineWidth = 3;
					ovalPath.Stroke();


					//// Oval 2 Drawing
					var oval2Path = UIBezierPath.FromOval(new RectangleF(26.5f, 37.5f, 5, 6));
					UIColor.LightGray.SetFill();
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

					UIColor.Black.SetStroke();
					oval2Path.LineWidth = 1;
					oval2Path.Stroke();


					//// Oval 4 Drawing
					var oval4Path = UIBezierPath.FromOval(new RectangleF(65.5f, 28.5f, 2, 3));
					UIColor.LightGray.SetFill();
					oval4Path.Fill();

					////// Oval 4 Inner Shadow
					var oval4BorderRect = oval4Path.Bounds;
					oval4BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
					oval4BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
					oval4BorderRect = RectangleF.Union(oval4BorderRect, oval4Path.Bounds);
					oval4BorderRect.Inflate(1, 1);

					var oval4NegativePath = UIBezierPath.FromRect(oval4BorderRect);
					oval4NegativePath.AppendPath(oval4Path);
					oval4NegativePath.UsesEvenOddFillRule = true;

					context.SaveState();
					{
						var xOffset = shadowOffset.Width + (float)Math.Round(oval4BorderRect.Width);
						var yOffset = shadowOffset.Height;
						context.SetShadowWithColor(
							new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
							shadowBlurRadius,
							shadow);

						oval4Path.AddClip();
						var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(oval4BorderRect.Width), 0);
						oval4NegativePath.ApplyTransform(transform);
						UIColor.Gray.SetFill();
						oval4NegativePath.Fill();
					}
					context.RestoreState();

					UIColor.Black.SetStroke();
					oval4Path.LineWidth = 1;
					oval4Path.Stroke();


					//// Oval 5 Drawing
					var oval5Path = UIBezierPath.FromOval(new RectangleF(25.5f, 30.5f, 2, 2));
					UIColor.LightGray.SetFill();
					oval5Path.Fill();

					////// Oval 5 Inner Shadow
					var oval5BorderRect = oval5Path.Bounds;
					oval5BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
					oval5BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
					oval5BorderRect = RectangleF.Union(oval5BorderRect, oval5Path.Bounds);
					oval5BorderRect.Inflate(1, 1);

					var oval5NegativePath = UIBezierPath.FromRect(oval5BorderRect);
					oval5NegativePath.AppendPath(oval5Path);
					oval5NegativePath.UsesEvenOddFillRule = true;

					context.SaveState();
					{
						var xOffset = shadowOffset.Width + (float)Math.Round(oval5BorderRect.Width);
						var yOffset = shadowOffset.Height;
						context.SetShadowWithColor(
							new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
							shadowBlurRadius,
							shadow);

						oval5Path.AddClip();
						var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(oval5BorderRect.Width), 0);
						oval5NegativePath.ApplyTransform(transform);
						UIColor.Gray.SetFill();
						oval5NegativePath.Fill();
					}
					context.RestoreState();

					UIColor.Black.SetStroke();
					oval5Path.LineWidth = 1;
					oval5Path.Stroke();


					//// Oval 6 Drawing
					var oval6Path = UIBezierPath.FromOval(new RectangleF(63.5f, 20.5f, 3, 3));
					UIColor.LightGray.SetFill();
					oval6Path.Fill();

					////// Oval 6 Inner Shadow
					var oval6BorderRect = oval6Path.Bounds;
					oval6BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
					oval6BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
					oval6BorderRect = RectangleF.Union(oval6BorderRect, oval6Path.Bounds);
					oval6BorderRect.Inflate(1, 1);

					var oval6NegativePath = UIBezierPath.FromRect(oval6BorderRect);
					oval6NegativePath.AppendPath(oval6Path);
					oval6NegativePath.UsesEvenOddFillRule = true;

					context.SaveState();
					{
						var xOffset = shadowOffset.Width + (float)Math.Round(oval6BorderRect.Width);
						var yOffset = shadowOffset.Height;
						context.SetShadowWithColor(
							new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
							shadowBlurRadius,
							shadow);

						oval6Path.AddClip();
						var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(oval6BorderRect.Width), 0);
						oval6NegativePath.ApplyTransform(transform);
						UIColor.Gray.SetFill();
						oval6NegativePath.Fill();
					}
					context.RestoreState();

					UIColor.Black.SetStroke();
					oval6Path.LineWidth = 1;
					oval6Path.Stroke();


					//// Oval 7 Drawing
					var oval7Path = UIBezierPath.FromOval(new RectangleF(26.5f, 20.5f, 3, 3));
					UIColor.LightGray.SetFill();
					oval7Path.Fill();

					////// Oval 7 Inner Shadow
					var oval7BorderRect = oval7Path.Bounds;
					oval7BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
					oval7BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
					oval7BorderRect = RectangleF.Union(oval7BorderRect, oval7Path.Bounds);
					oval7BorderRect.Inflate(1, 1);

					var oval7NegativePath = UIBezierPath.FromRect(oval7BorderRect);
					oval7NegativePath.AppendPath(oval7Path);
					oval7NegativePath.UsesEvenOddFillRule = true;

					context.SaveState();
					{
						var xOffset = shadowOffset.Width + (float)Math.Round(oval7BorderRect.Width);
						var yOffset = shadowOffset.Height;
						context.SetShadowWithColor(
							new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
							shadowBlurRadius,
							shadow);

						oval7Path.AddClip();
						var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(oval7BorderRect.Width), 0);
						oval7NegativePath.ApplyTransform(transform);
						UIColor.Gray.SetFill();
						oval7NegativePath.Fill();
					}
					context.RestoreState();

					UIColor.Black.SetStroke();
					oval7Path.LineWidth = 1;
					oval7Path.Stroke();


					//// Oval 8 Drawing
					var oval8Path = UIBezierPath.FromOval(new RectangleF(59.5f, 15.5f, 2, 2));
					UIColor.LightGray.SetFill();
					oval8Path.Fill();

					////// Oval 8 Inner Shadow
					var oval8BorderRect = oval8Path.Bounds;
					oval8BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
					oval8BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
					oval8BorderRect = RectangleF.Union(oval8BorderRect, oval8Path.Bounds);
					oval8BorderRect.Inflate(1, 1);

					var oval8NegativePath = UIBezierPath.FromRect(oval8BorderRect);
					oval8NegativePath.AppendPath(oval8Path);
					oval8NegativePath.UsesEvenOddFillRule = true;

					context.SaveState();
					{
						var xOffset = shadowOffset.Width + (float)Math.Round(oval8BorderRect.Width);
						var yOffset = shadowOffset.Height;
						context.SetShadowWithColor(
							new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
							shadowBlurRadius,
							shadow);

						oval8Path.AddClip();
						var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(oval8BorderRect.Width), 0);
						oval8NegativePath.ApplyTransform(transform);
						UIColor.Gray.SetFill();
						oval8NegativePath.Fill();
					}
					context.RestoreState();

					UIColor.Black.SetStroke();
					oval8Path.LineWidth = 1;
					oval8Path.Stroke();


					//// Oval 9 Drawing
					var oval9Path = UIBezierPath.FromOval(new RectangleF(31.5f, 15.5f, 2, 2));
					UIColor.LightGray.SetFill();
					oval9Path.Fill();

					////// Oval 9 Inner Shadow
					var oval9BorderRect = oval9Path.Bounds;
					oval9BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
					oval9BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
					oval9BorderRect = RectangleF.Union(oval9BorderRect, oval9Path.Bounds);
					oval9BorderRect.Inflate(1, 1);

					var oval9NegativePath = UIBezierPath.FromRect(oval9BorderRect);
					oval9NegativePath.AppendPath(oval9Path);
					oval9NegativePath.UsesEvenOddFillRule = true;

					context.SaveState();
					{
						var xOffset = shadowOffset.Width + (float)Math.Round(oval9BorderRect.Width);
						var yOffset = shadowOffset.Height;
						context.SetShadowWithColor(
							new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
							shadowBlurRadius,
							shadow);

						oval9Path.AddClip();
						var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(oval9BorderRect.Width), 0);
						oval9NegativePath.ApplyTransform(transform);
						UIColor.Gray.SetFill();
						oval9NegativePath.Fill();
					}
					context.RestoreState();

					UIColor.Black.SetStroke();
					oval9Path.LineWidth = 1;
					oval9Path.Stroke();


					//// Oval 10 Drawing
					var oval10Path = UIBezierPath.FromOval(new RectangleF(50.5f, 12.5f, 4, 3));
					UIColor.LightGray.SetFill();
					oval10Path.Fill();

					////// Oval 10 Inner Shadow
					var oval10BorderRect = oval10Path.Bounds;
					oval10BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
					oval10BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
					oval10BorderRect = RectangleF.Union(oval10BorderRect, oval10Path.Bounds);
					oval10BorderRect.Inflate(1, 1);

					var oval10NegativePath = UIBezierPath.FromRect(oval10BorderRect);
					oval10NegativePath.AppendPath(oval10Path);
					oval10NegativePath.UsesEvenOddFillRule = true;

					context.SaveState();
					{
						var xOffset = shadowOffset.Width + (float)Math.Round(oval10BorderRect.Width);
						var yOffset = shadowOffset.Height;
						context.SetShadowWithColor(
							new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
							shadowBlurRadius,
							shadow);

						oval10Path.AddClip();
						var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(oval10BorderRect.Width), 0);
						oval10NegativePath.ApplyTransform(transform);
						UIColor.Gray.SetFill();
						oval10NegativePath.Fill();
					}
					context.RestoreState();

					UIColor.Black.SetStroke();
					oval10Path.LineWidth = 1;
					oval10Path.Stroke();


					//// Oval 11 Drawing
					var oval11Path = UIBezierPath.FromOval(new RectangleF(41.5f, 12.5f, 3, 3));
					UIColor.LightGray.SetFill();
					oval11Path.Fill();

					////// Oval 11 Inner Shadow
					var oval11BorderRect = oval11Path.Bounds;
					oval11BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
					oval11BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
					oval11BorderRect = RectangleF.Union(oval11BorderRect, oval11Path.Bounds);
					oval11BorderRect.Inflate(1, 1);

					var oval11NegativePath = UIBezierPath.FromRect(oval11BorderRect);
					oval11NegativePath.AppendPath(oval11Path);
					oval11NegativePath.UsesEvenOddFillRule = true;

					context.SaveState();
					{
						var xOffset = shadowOffset.Width + (float)Math.Round(oval11BorderRect.Width);
						var yOffset = shadowOffset.Height;
						context.SetShadowWithColor(
							new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
							shadowBlurRadius,
							shadow);

						oval11Path.AddClip();
						var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(oval11BorderRect.Width), 0);
						oval11NegativePath.ApplyTransform(transform);
						UIColor.Gray.SetFill();
						oval11NegativePath.Fill();
					}
					context.RestoreState();

					UIColor.Black.SetStroke();
					oval11Path.LineWidth = 1;
					oval11Path.Stroke();


					//// Bezier 2 Drawing
					UIBezierPath bezier2Path = new UIBezierPath();
					bezier2Path.MoveTo(new PointF(52.58f, 28.38f));
					bezier2Path.AddCurveToPoint(new PointF(49.54f, 35.62f), new PointF(54.65f, 31.67f), new PointF(52.52f, 34.42f));
					bezier2Path.AddCurveToPoint(new PointF(41.94f, 32.73f), new PointF(41.69f, 38.8f), new PointF(42.25f, 33.53f));
					bezier2Path.AddCurveToPoint(new PointF(43.46f, 18.23f), new PointF(41.33f, 31.13f), new PointF(43.46f, 18.23f));
					bezier2Path.AddCurveToPoint(new PointF(52.58f, 28.38f), new PointF(43.46f, 18.23f), new PointF(50.51f, 25.08f));
					bezier2Path.ClosePath();
					color3.SetFill();
					bezier2Path.Fill();
					UIColor.Black.SetStroke();
					bezier2Path.LineWidth = 1;
					bezier2Path.Stroke();


					//// Rectangle Drawing
					var rectanglePath = UIBezierPath.FromRect(new RectangleF(37.5f, 48.5f, 5, 4));
					color.SetFill();
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

					UIColor.Black.SetStroke();
					rectanglePath.LineWidth = 1;
					rectanglePath.Stroke();


					//// Rectangle 2 Drawing
					var rectangle2Path = UIBezierPath.FromRect(new RectangleF(45.5f, 48.5f, 3, 4));
					color.SetFill();
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

					UIColor.Black.SetStroke();
					rectangle2Path.LineWidth = 1;
					rectangle2Path.Stroke();


					//// Rectangle 3 Drawing
					var rectangle3Path = UIBezierPath.FromRect(new RectangleF(51.5f, 48.5f, 6, 4));
					color.SetFill();
					rectangle3Path.Fill();

					////// Rectangle 3 Inner Shadow
					var rectangle3BorderRect = rectangle3Path.Bounds;
					rectangle3BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
					rectangle3BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
					rectangle3BorderRect = RectangleF.Union(rectangle3BorderRect, rectangle3Path.Bounds);
					rectangle3BorderRect.Inflate(1, 1);

					var rectangle3NegativePath = UIBezierPath.FromRect(rectangle3BorderRect);
					rectangle3NegativePath.AppendPath(rectangle3Path);
					rectangle3NegativePath.UsesEvenOddFillRule = true;

					context.SaveState();
					{
						var xOffset = shadowOffset.Width + (float)Math.Round(rectangle3BorderRect.Width);
						var yOffset = shadowOffset.Height;
						context.SetShadowWithColor(
							new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
							shadowBlurRadius,
							shadow);

						rectangle3Path.AddClip();
						var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(rectangle3BorderRect.Width), 0);
						rectangle3NegativePath.ApplyTransform(transform);
						UIColor.Gray.SetFill();
						rectangle3NegativePath.Fill();
					}
					context.RestoreState();

					UIColor.Black.SetStroke();
					rectangle3Path.LineWidth = 1;
					rectangle3Path.Stroke();


					//// Oval 3 Drawing
					var oval3Path = UIBezierPath.FromOval(new RectangleF(63.5f, 37.5f, 4, 6));
					UIColor.LightGray.SetFill();
					oval3Path.Fill();

					////// Oval 3 Inner Shadow
					var oval3BorderRect = oval3Path.Bounds;
					oval3BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
					oval3BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
					oval3BorderRect = RectangleF.Union(oval3BorderRect, oval3Path.Bounds);
					oval3BorderRect.Inflate(1, 1);

					var oval3NegativePath = UIBezierPath.FromRect(oval3BorderRect);
					oval3NegativePath.AppendPath(oval3Path);
					oval3NegativePath.UsesEvenOddFillRule = true;

					context.SaveState();
					{
						var xOffset = shadowOffset.Width + (float)Math.Round(oval3BorderRect.Width);
						var yOffset = shadowOffset.Height;
						context.SetShadowWithColor(
							new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
							shadowBlurRadius,
							shadow);

						oval3Path.AddClip();
						var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(oval3BorderRect.Width), 0);
						oval3NegativePath.ApplyTransform(transform);
						UIColor.Gray.SetFill();
						oval3NegativePath.Fill();
					}
					context.RestoreState();

					UIColor.Black.SetStroke();
					oval3Path.LineWidth = 1;
					oval3Path.Stroke();
				}
			}



		}

		static void PaintCodeDrawNonRetina ()
		{
			//// General Declarations
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			UIColor color = UIColor.FromRGBA(0.833f, 0.833f, 0.833f, 1.000f);
			UIColor color3 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);

			//// Shadow Declarations
			var shadow = UIColor.Black.ColorWithAlpha(0.66f).CGColor;
			var shadowOffset = new SizeF(0.1f, -0.1f);
			var shadowBlurRadius = 17;

			//// Oval Drawing
			var ovalPath = UIBezierPath.FromOval(new RectangleF(0.5f, 0.5f, 47, 31));
			color.SetFill();
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

			color3.SetStroke();
			ovalPath.LineWidth = 3;
			ovalPath.Stroke();


			//// Oval 2 Drawing
			var oval2Path = UIBezierPath.FromOval(new RectangleF(8.5f, 19.5f, 2, 3));
			UIColor.LightGray.SetFill();
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

			UIColor.Black.SetStroke();
			oval2Path.LineWidth = 1;
			oval2Path.Stroke();


			//// Oval 4 Drawing
			var oval4Path = UIBezierPath.FromOval(new RectangleF(39.5f, 14.5f, 2, 2));
			UIColor.LightGray.SetFill();
			oval4Path.Fill();

			////// Oval 4 Inner Shadow
			var oval4BorderRect = oval4Path.Bounds;
			oval4BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
			oval4BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
			oval4BorderRect = RectangleF.Union(oval4BorderRect, oval4Path.Bounds);
			oval4BorderRect.Inflate(1, 1);

			var oval4NegativePath = UIBezierPath.FromRect(oval4BorderRect);
			oval4NegativePath.AppendPath(oval4Path);
			oval4NegativePath.UsesEvenOddFillRule = true;

			context.SaveState();
			{
				var xOffset = shadowOffset.Width + (float)Math.Round(oval4BorderRect.Width);
				var yOffset = shadowOffset.Height;
				context.SetShadowWithColor(
					new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
					shadowBlurRadius,
					shadow);

				oval4Path.AddClip();
				var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(oval4BorderRect.Width), 0);
				oval4NegativePath.ApplyTransform(transform);
				UIColor.Gray.SetFill();
				oval4NegativePath.Fill();
			}
			context.RestoreState();

			UIColor.Black.SetStroke();
			oval4Path.LineWidth = 1;
			oval4Path.Stroke();


			//// Oval 5 Drawing
			var oval5Path = UIBezierPath.FromOval(new RectangleF(6.5f, 14.5f, 2, 2));
			UIColor.LightGray.SetFill();
			oval5Path.Fill();

			////// Oval 5 Inner Shadow
			var oval5BorderRect = oval5Path.Bounds;
			oval5BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
			oval5BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
			oval5BorderRect = RectangleF.Union(oval5BorderRect, oval5Path.Bounds);
			oval5BorderRect.Inflate(1, 1);

			var oval5NegativePath = UIBezierPath.FromRect(oval5BorderRect);
			oval5NegativePath.AppendPath(oval5Path);
			oval5NegativePath.UsesEvenOddFillRule = true;

			context.SaveState();
			{
				var xOffset = shadowOffset.Width + (float)Math.Round(oval5BorderRect.Width);
				var yOffset = shadowOffset.Height;
				context.SetShadowWithColor(
					new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
					shadowBlurRadius,
					shadow);

				oval5Path.AddClip();
				var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(oval5BorderRect.Width), 0);
				oval5NegativePath.ApplyTransform(transform);
				UIColor.Gray.SetFill();
				oval5NegativePath.Fill();
			}
			context.RestoreState();

			UIColor.Black.SetStroke();
			oval5Path.LineWidth = 1;
			oval5Path.Stroke();


			//// Oval 6 Drawing
			var oval6Path = UIBezierPath.FromOval(new RectangleF(37.5f, 9.5f, 2, 2));
			UIColor.LightGray.SetFill();
			oval6Path.Fill();

			////// Oval 6 Inner Shadow
			var oval6BorderRect = oval6Path.Bounds;
			oval6BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
			oval6BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
			oval6BorderRect = RectangleF.Union(oval6BorderRect, oval6Path.Bounds);
			oval6BorderRect.Inflate(1, 1);

			var oval6NegativePath = UIBezierPath.FromRect(oval6BorderRect);
			oval6NegativePath.AppendPath(oval6Path);
			oval6NegativePath.UsesEvenOddFillRule = true;

			context.SaveState();
			{
				var xOffset = shadowOffset.Width + (float)Math.Round(oval6BorderRect.Width);
				var yOffset = shadowOffset.Height;
				context.SetShadowWithColor(
					new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
					shadowBlurRadius,
					shadow);

				oval6Path.AddClip();
				var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(oval6BorderRect.Width), 0);
				oval6NegativePath.ApplyTransform(transform);
				UIColor.Gray.SetFill();
				oval6NegativePath.Fill();
			}
			context.RestoreState();

			UIColor.Black.SetStroke();
			oval6Path.LineWidth = 1;
			oval6Path.Stroke();


			//// Oval 7 Drawing
			var oval7Path = UIBezierPath.FromOval(new RectangleF(8.5f, 9.5f, 2, 2));
			UIColor.LightGray.SetFill();
			oval7Path.Fill();

			////// Oval 7 Inner Shadow
			var oval7BorderRect = oval7Path.Bounds;
			oval7BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
			oval7BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
			oval7BorderRect = RectangleF.Union(oval7BorderRect, oval7Path.Bounds);
			oval7BorderRect.Inflate(1, 1);

			var oval7NegativePath = UIBezierPath.FromRect(oval7BorderRect);
			oval7NegativePath.AppendPath(oval7Path);
			oval7NegativePath.UsesEvenOddFillRule = true;

			context.SaveState();
			{
				var xOffset = shadowOffset.Width + (float)Math.Round(oval7BorderRect.Width);
				var yOffset = shadowOffset.Height;
				context.SetShadowWithColor(
					new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
					shadowBlurRadius,
					shadow);

				oval7Path.AddClip();
				var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(oval7BorderRect.Width), 0);
				oval7NegativePath.ApplyTransform(transform);
				UIColor.Gray.SetFill();
				oval7NegativePath.Fill();
			}
			context.RestoreState();

			UIColor.Black.SetStroke();
			oval7Path.LineWidth = 1;
			oval7Path.Stroke();


			//// Oval 8 Drawing
			var oval8Path = UIBezierPath.FromOval(new RectangleF(32.5f, 6.5f, 3, 2));
			UIColor.LightGray.SetFill();
			oval8Path.Fill();

			////// Oval 8 Inner Shadow
			var oval8BorderRect = oval8Path.Bounds;
			oval8BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
			oval8BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
			oval8BorderRect = RectangleF.Union(oval8BorderRect, oval8Path.Bounds);
			oval8BorderRect.Inflate(1, 1);

			var oval8NegativePath = UIBezierPath.FromRect(oval8BorderRect);
			oval8NegativePath.AppendPath(oval8Path);
			oval8NegativePath.UsesEvenOddFillRule = true;

			context.SaveState();
			{
				var xOffset = shadowOffset.Width + (float)Math.Round(oval8BorderRect.Width);
				var yOffset = shadowOffset.Height;
				context.SetShadowWithColor(
					new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
					shadowBlurRadius,
					shadow);

				oval8Path.AddClip();
				var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(oval8BorderRect.Width), 0);
				oval8NegativePath.ApplyTransform(transform);
				UIColor.Gray.SetFill();
				oval8NegativePath.Fill();
			}
			context.RestoreState();

			UIColor.Black.SetStroke();
			oval8Path.LineWidth = 1;
			oval8Path.Stroke();


			//// Oval 9 Drawing
			var oval9Path = UIBezierPath.FromOval(new RectangleF(12.5f, 6.5f, 2, 2));
			UIColor.LightGray.SetFill();
			oval9Path.Fill();

			////// Oval 9 Inner Shadow
			var oval9BorderRect = oval9Path.Bounds;
			oval9BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
			oval9BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
			oval9BorderRect = RectangleF.Union(oval9BorderRect, oval9Path.Bounds);
			oval9BorderRect.Inflate(1, 1);

			var oval9NegativePath = UIBezierPath.FromRect(oval9BorderRect);
			oval9NegativePath.AppendPath(oval9Path);
			oval9NegativePath.UsesEvenOddFillRule = true;

			context.SaveState();
			{
				var xOffset = shadowOffset.Width + (float)Math.Round(oval9BorderRect.Width);
				var yOffset = shadowOffset.Height;
				context.SetShadowWithColor(
					new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
					shadowBlurRadius,
					shadow);

				oval9Path.AddClip();
				var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(oval9BorderRect.Width), 0);
				oval9NegativePath.ApplyTransform(transform);
				UIColor.Gray.SetFill();
				oval9NegativePath.Fill();
			}
			context.RestoreState();

			UIColor.Black.SetStroke();
			oval9Path.LineWidth = 1;
			oval9Path.Stroke();


			//// Oval 10 Drawing
			var oval10Path = UIBezierPath.FromOval(new RectangleF(26.5f, 4.5f, 2, 2));
			UIColor.LightGray.SetFill();
			oval10Path.Fill();

			////// Oval 10 Inner Shadow
			var oval10BorderRect = oval10Path.Bounds;
			oval10BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
			oval10BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
			oval10BorderRect = RectangleF.Union(oval10BorderRect, oval10Path.Bounds);
			oval10BorderRect.Inflate(1, 1);

			var oval10NegativePath = UIBezierPath.FromRect(oval10BorderRect);
			oval10NegativePath.AppendPath(oval10Path);
			oval10NegativePath.UsesEvenOddFillRule = true;

			context.SaveState();
			{
				var xOffset = shadowOffset.Width + (float)Math.Round(oval10BorderRect.Width);
				var yOffset = shadowOffset.Height;
				context.SetShadowWithColor(
					new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
					shadowBlurRadius,
					shadow);

				oval10Path.AddClip();
				var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(oval10BorderRect.Width), 0);
				oval10NegativePath.ApplyTransform(transform);
				UIColor.Gray.SetFill();
				oval10NegativePath.Fill();
			}
			context.RestoreState();

			UIColor.Black.SetStroke();
			oval10Path.LineWidth = 1;
			oval10Path.Stroke();


			//// Oval 11 Drawing
			var oval11Path = UIBezierPath.FromOval(new RectangleF(19.5f, 4.5f, 2, 2));
			UIColor.LightGray.SetFill();
			oval11Path.Fill();

			////// Oval 11 Inner Shadow
			var oval11BorderRect = oval11Path.Bounds;
			oval11BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
			oval11BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
			oval11BorderRect = RectangleF.Union(oval11BorderRect, oval11Path.Bounds);
			oval11BorderRect.Inflate(1, 1);

			var oval11NegativePath = UIBezierPath.FromRect(oval11BorderRect);
			oval11NegativePath.AppendPath(oval11Path);
			oval11NegativePath.UsesEvenOddFillRule = true;

			context.SaveState();
			{
				var xOffset = shadowOffset.Width + (float)Math.Round(oval11BorderRect.Width);
				var yOffset = shadowOffset.Height;
				context.SetShadowWithColor(
					new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
					shadowBlurRadius,
					shadow);

				oval11Path.AddClip();
				var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(oval11BorderRect.Width), 0);
				oval11NegativePath.ApplyTransform(transform);
				UIColor.Gray.SetFill();
				oval11NegativePath.Fill();
			}
			context.RestoreState();

			UIColor.Black.SetStroke();
			oval11Path.LineWidth = 1;
			oval11Path.Stroke();


			//// Bezier 2 Drawing
			UIBezierPath bezier2Path = new UIBezierPath();
			bezier2Path.MoveTo(new PointF(28.78f, 14));
			bezier2Path.AddCurveToPoint(new PointF(26.39f, 18), new PointF(30.41f, 15.82f), new PointF(28.73f, 17.34f));
			bezier2Path.AddCurveToPoint(new PointF(20.41f, 16.4f), new PointF(20.21f, 19.75f), new PointF(20.65f, 16.84f));
			bezier2Path.AddCurveToPoint(new PointF(21.61f, 8.4f), new PointF(19.93f, 15.52f), new PointF(21.61f, 8.4f));
			bezier2Path.AddCurveToPoint(new PointF(28.78f, 14), new PointF(21.61f, 8.4f), new PointF(27.16f, 12.18f));
			bezier2Path.ClosePath();
			color3.SetFill();
			bezier2Path.Fill();
			UIColor.Black.SetStroke();
			bezier2Path.LineWidth = 1;
			bezier2Path.Stroke();


			//// Rectangle Drawing
			var rectanglePath = UIBezierPath.FromRect(new RectangleF(16.5f, 24.5f, 5, 3));
			color.SetFill();
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

			UIColor.Black.SetStroke();
			rectanglePath.LineWidth = 1;
			rectanglePath.Stroke();


			//// Rectangle 2 Drawing
			var rectangle2Path = UIBezierPath.FromRect(new RectangleF(23.5f, 24.5f, 3, 3));
			color.SetFill();
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

			UIColor.Black.SetStroke();
			rectangle2Path.LineWidth = 1;
			rectangle2Path.Stroke();


			//// Rectangle 3 Drawing
			var rectangle3Path = UIBezierPath.FromRect(new RectangleF(28.5f, 24.5f, 4, 3));
			color.SetFill();
			rectangle3Path.Fill();

			////// Rectangle 3 Inner Shadow
			var rectangle3BorderRect = rectangle3Path.Bounds;
			rectangle3BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
			rectangle3BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
			rectangle3BorderRect = RectangleF.Union(rectangle3BorderRect, rectangle3Path.Bounds);
			rectangle3BorderRect.Inflate(1, 1);

			var rectangle3NegativePath = UIBezierPath.FromRect(rectangle3BorderRect);
			rectangle3NegativePath.AppendPath(rectangle3Path);
			rectangle3NegativePath.UsesEvenOddFillRule = true;

			context.SaveState();
			{
				var xOffset = shadowOffset.Width + (float)Math.Round(rectangle3BorderRect.Width);
				var yOffset = shadowOffset.Height;
				context.SetShadowWithColor(
					new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
					shadowBlurRadius,
					shadow);

				rectangle3Path.AddClip();
				var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(rectangle3BorderRect.Width), 0);
				rectangle3NegativePath.ApplyTransform(transform);
				UIColor.Gray.SetFill();
				rectangle3NegativePath.Fill();
			}
			context.RestoreState();

			UIColor.Black.SetStroke();
			rectangle3Path.LineWidth = 1;
			rectangle3Path.Stroke();


			//// Oval 3 Drawing
			var oval3Path = UIBezierPath.FromOval(new RectangleF(37.5f, 19.5f, 2, 3));
			UIColor.LightGray.SetFill();
			oval3Path.Fill();

			////// Oval 3 Inner Shadow
			var oval3BorderRect = oval3Path.Bounds;
			oval3BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
			oval3BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
			oval3BorderRect = RectangleF.Union(oval3BorderRect, oval3Path.Bounds);
			oval3BorderRect.Inflate(1, 1);

			var oval3NegativePath = UIBezierPath.FromRect(oval3BorderRect);
			oval3NegativePath.AppendPath(oval3Path);
			oval3NegativePath.UsesEvenOddFillRule = true;

			context.SaveState();
			{
				var xOffset = shadowOffset.Width + (float)Math.Round(oval3BorderRect.Width);
				var yOffset = shadowOffset.Height;
				context.SetShadowWithColor(
					new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
					shadowBlurRadius,
					shadow);

				oval3Path.AddClip();
				var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(oval3BorderRect.Width), 0);
				oval3NegativePath.ApplyTransform(transform);
				UIColor.Gray.SetFill();
				oval3NegativePath.Fill();
			}
			context.RestoreState();

			UIColor.Black.SetStroke();
			oval3Path.LineWidth = 1;
			oval3Path.Stroke();



		}
	}
}

