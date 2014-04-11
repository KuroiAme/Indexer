using System;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Drawing;

namespace no.dctapps.commons
{
	public class TableIcon
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
			UIColor shadowColor2 = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.413f);
			UIColor color5 = UIColor.FromRGBA(0.200f, 0.067f, 0.000f, 1.000f);

			//// Shadow Declarations
			var shadow = shadowColor2.CGColor;
			var shadowOffset = new SizeF(0.1f, 1.1f);
			var shadowBlurRadius = 10;

			//// Rectangle 5 Drawing
			UIBezierPath rectangle5Path = new UIBezierPath();
			rectangle5Path.MoveTo(new PointF(6.09f, 11.75f));
			rectangle5Path.AddLineTo(new PointF(6.09f, 11.85f));
			rectangle5Path.AddLineTo(new PointF(6.09f, 11.75f));
			rectangle5Path.ClosePath();
			rectangle5Path.MoveTo(new PointF(6.94f, 20.6f));
			rectangle5Path.AddLineTo(new PointF(6.94f, 2));
			rectangle5Path.AddLineTo(new PointF(9.27f, 5.28f));
			rectangle5Path.AddLineTo(new PointF(9.47f, 26.07f));
			rectangle5Path.AddLineTo(new PointF(6.94f, 20.6f));
			rectangle5Path.ClosePath();
			color5.SetFill();
			rectangle5Path.Fill();

			////// Rectangle 5 Inner Shadow
			var rectangle5BorderRect = rectangle5Path.Bounds;
			rectangle5BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
			rectangle5BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
			rectangle5BorderRect = RectangleF.Union(rectangle5BorderRect, rectangle5Path.Bounds);
			rectangle5BorderRect.Inflate(1, 1);

			var rectangle5NegativePath = UIBezierPath.FromRect(rectangle5BorderRect);
			rectangle5NegativePath.AppendPath(rectangle5Path);
			rectangle5NegativePath.UsesEvenOddFillRule = true;

			context.SaveState();
			{
				var xOffset = shadowOffset.Width + (float)Math.Round(rectangle5BorderRect.Width);
				var yOffset = shadowOffset.Height;
				context.SetShadowWithColor(
					new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
					shadowBlurRadius,
					shadow);

				rectangle5Path.AddClip();
				var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(rectangle5BorderRect.Width), 0);
				rectangle5NegativePath.ApplyTransform(transform);
				UIColor.Gray.SetFill();
				rectangle5NegativePath.Fill();
			}
			context.RestoreState();

			color5.SetStroke();
			rectangle5Path.LineWidth = 1;
			rectangle5Path.Stroke();


			//// Rectangle 4 Drawing
			UIBezierPath rectangle4Path = new UIBezierPath();
			rectangle4Path.MoveTo(new PointF(77.17f, 46.2f));
			rectangle4Path.AddLineTo(new PointF(77.17f, 46.2f));
			rectangle4Path.AddLineTo(new PointF(77.17f, 46.2f));
			rectangle4Path.ClosePath();
			rectangle4Path.MoveTo(new PointF(77.54f, 58.04f));
			rectangle4Path.AddLineTo(new PointF(77.54f, 34.62f));
			rectangle4Path.AddLineTo(new PointF(83.98f, 37.55f));
			rectangle4Path.AddLineTo(new PointF(83.98f, 63.9f));
			rectangle4Path.AddLineTo(new PointF(77.54f, 58.04f));
			rectangle4Path.ClosePath();
			color5.SetFill();
			rectangle4Path.Fill();

			////// Rectangle 4 Inner Shadow
			var rectangle4BorderRect = rectangle4Path.Bounds;
			rectangle4BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
			rectangle4BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
			rectangle4BorderRect = RectangleF.Union(rectangle4BorderRect, rectangle4Path.Bounds);
			rectangle4BorderRect.Inflate(1, 1);

			var rectangle4NegativePath = UIBezierPath.FromRect(rectangle4BorderRect);
			rectangle4NegativePath.AppendPath(rectangle4Path);
			rectangle4NegativePath.UsesEvenOddFillRule = true;

			context.SaveState();
			{
				var xOffset = shadowOffset.Width + (float)Math.Round(rectangle4BorderRect.Width);
				var yOffset = shadowOffset.Height;
				context.SetShadowWithColor(
					new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
					shadowBlurRadius,
					shadow);

				rectangle4Path.AddClip();
				var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(rectangle4BorderRect.Width), 0);
				rectangle4NegativePath.ApplyTransform(transform);
				UIColor.Gray.SetFill();
				rectangle4NegativePath.Fill();
			}
			context.RestoreState();

			color5.SetStroke();
			rectangle4Path.LineWidth = 1;
			rectangle4Path.Stroke();


			//// Rectangle 6 Drawing
			UIBezierPath rectangle6Path = new UIBezierPath();
			rectangle6Path.MoveTo(new PointF(26.89f, 48.27f));
			rectangle6Path.AddLineTo(new PointF(26.89f, 48.27f));
			rectangle6Path.AddLineTo(new PointF(26.89f, 48.27f));
			rectangle6Path.ClosePath();
			rectangle6Path.MoveTo(new PointF(27.14f, 58.73f));
			rectangle6Path.AddLineTo(new PointF(27.14f, 31));
			rectangle6Path.AddLineTo(new PointF(31.51f, 40.63f));
			rectangle6Path.AddLineTo(new PointF(31.51f, 63.9f));
			rectangle6Path.AddLineTo(new PointF(27.14f, 58.73f));
			rectangle6Path.ClosePath();
			color5.SetFill();
			rectangle6Path.Fill();

			////// Rectangle 6 Inner Shadow
			var rectangle6BorderRect = rectangle6Path.Bounds;
			rectangle6BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
			rectangle6BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
			rectangle6BorderRect = RectangleF.Union(rectangle6BorderRect, rectangle6Path.Bounds);
			rectangle6BorderRect.Inflate(1, 1);

			var rectangle6NegativePath = UIBezierPath.FromRect(rectangle6BorderRect);
			rectangle6NegativePath.AppendPath(rectangle6Path);
			rectangle6NegativePath.UsesEvenOddFillRule = true;

			context.SaveState();
			{
				var xOffset = shadowOffset.Width + (float)Math.Round(rectangle6BorderRect.Width);
				var yOffset = shadowOffset.Height;
				context.SetShadowWithColor(
					new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
					shadowBlurRadius,
					shadow);

				rectangle6Path.AddClip();
				var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(rectangle6BorderRect.Width), 0);
				rectangle6NegativePath.ApplyTransform(transform);
				UIColor.Gray.SetFill();
				rectangle6NegativePath.Fill();
			}
			context.RestoreState();

			color5.SetStroke();
			rectangle6Path.LineWidth = 1;
			rectangle6Path.Stroke();


			//// Rectangle Drawing
			var rectanglePath = UIBezierPath.FromRect(new RectangleF(31.5f, 39.5f, 11, 24));
			color5.SetFill();
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

			color5.SetStroke();
			rectanglePath.LineWidth = 1;
			rectanglePath.Stroke();


			//// Rectangle 2 Drawing
			var rectangle2Path = UIBezierPath.FromRect(new RectangleF(9.5f, 7.5f, 7, 19));
			color5.SetFill();
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

			color5.SetStroke();
			rectangle2Path.LineWidth = 1;
			rectangle2Path.Stroke();


			//// Bezier Drawing
			UIBezierPath bezierPath = new UIBezierPath();
			bezierPath.MoveTo(new PointF(7.47f, 2.1f));
			bezierPath.AddLineTo(new PointF(30.42f, 38.52f));
			bezierPath.AddLineTo(new PointF(93.81f, 38.52f));
			bezierPath.AddLineTo(new PointF(53.37f, 2.1f));
			bezierPath.AddLineTo(new PointF(10.74f, 2.1f));
			bezierPath.AddLineTo(new PointF(7.47f, 2.1f));
			bezierPath.ClosePath();
			color5.SetFill();
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

			color5.SetStroke();
			bezierPath.LineWidth = 2;
			bezierPath.Stroke();


			//// Rectangle 3 Drawing
			var rectangle3Path = UIBezierPath.FromRect(new RectangleF(83.5f, 39.5f, 11, 24));
			color5.SetFill();
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

			color5.SetStroke();
			rectangle3Path.LineWidth = 1;
			rectangle3Path.Stroke();



		}

		static void paintCodeNonRetina ()
		{
			//// General Declarations
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			UIColor shadowColor2 = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.413f);
			UIColor color5 = UIColor.FromRGBA(0.200f, 0.067f, 0.000f, 1.000f);

			//// Shadow Declarations
			var shadow = shadowColor2.CGColor;
			var shadowOffset = new SizeF(0.1f, 1.1f);
			var shadowBlurRadius = 10;

			//// Group
			{
				//// Rectangle 5 Drawing
				UIBezierPath rectangle5Path = new UIBezierPath();
				rectangle5Path.MoveTo(new PointF(4.04f, 7.93f));
				rectangle5Path.AddLineTo(new PointF(4.04f, 7.97f));
				rectangle5Path.AddLineTo(new PointF(4.04f, 7.93f));
				rectangle5Path.ClosePath();
				rectangle5Path.MoveTo(new PointF(4.43f, 11.5f));
				rectangle5Path.AddLineTo(new PointF(4.43f, 4));
				rectangle5Path.AddLineTo(new PointF(5.51f, 5.32f));
				rectangle5Path.AddLineTo(new PointF(5.6f, 13.71f));
				rectangle5Path.AddLineTo(new PointF(4.43f, 11.5f));
				rectangle5Path.ClosePath();
				color5.SetFill();
				rectangle5Path.Fill();

				////// Rectangle 5 Inner Shadow
				var rectangle5BorderRect = rectangle5Path.Bounds;
				rectangle5BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
				rectangle5BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
				rectangle5BorderRect = RectangleF.Union(rectangle5BorderRect, rectangle5Path.Bounds);
				rectangle5BorderRect.Inflate(1, 1);

				var rectangle5NegativePath = UIBezierPath.FromRect(rectangle5BorderRect);
				rectangle5NegativePath.AppendPath(rectangle5Path);
				rectangle5NegativePath.UsesEvenOddFillRule = true;

				context.SaveState();
				{
					var xOffset = shadowOffset.Width + (float)Math.Round(rectangle5BorderRect.Width);
					var yOffset = shadowOffset.Height;
					context.SetShadowWithColor(
						new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadowBlurRadius,
						shadow);

					rectangle5Path.AddClip();
					var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(rectangle5BorderRect.Width), 0);
					rectangle5NegativePath.ApplyTransform(transform);
					UIColor.Gray.SetFill();
					rectangle5NegativePath.Fill();
				}
				context.RestoreState();

				color5.SetStroke();
				rectangle5Path.LineWidth = 1;
				rectangle5Path.Stroke();


				//// Rectangle 4 Drawing
				UIBezierPath rectangle4Path = new UIBezierPath();
				rectangle4Path.MoveTo(new PointF(36.78f, 21.82f));
				rectangle4Path.AddLineTo(new PointF(36.78f, 21.82f));
				rectangle4Path.AddLineTo(new PointF(36.78f, 21.82f));
				rectangle4Path.ClosePath();
				rectangle4Path.MoveTo(new PointF(36.96f, 26.6f));
				rectangle4Path.AddLineTo(new PointF(36.96f, 17.15f));
				rectangle4Path.AddLineTo(new PointF(39.92f, 18.33f));
				rectangle4Path.AddLineTo(new PointF(39.92f, 28.96f));
				rectangle4Path.AddLineTo(new PointF(36.96f, 26.6f));
				rectangle4Path.ClosePath();
				color5.SetFill();
				rectangle4Path.Fill();

				////// Rectangle 4 Inner Shadow
				var rectangle4BorderRect = rectangle4Path.Bounds;
				rectangle4BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
				rectangle4BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
				rectangle4BorderRect = RectangleF.Union(rectangle4BorderRect, rectangle4Path.Bounds);
				rectangle4BorderRect.Inflate(1, 1);

				var rectangle4NegativePath = UIBezierPath.FromRect(rectangle4BorderRect);
				rectangle4NegativePath.AppendPath(rectangle4Path);
				rectangle4NegativePath.UsesEvenOddFillRule = true;

				context.SaveState();
				{
					var xOffset = shadowOffset.Width + (float)Math.Round(rectangle4BorderRect.Width);
					var yOffset = shadowOffset.Height;
					context.SetShadowWithColor(
						new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadowBlurRadius,
						shadow);

					rectangle4Path.AddClip();
					var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(rectangle4BorderRect.Width), 0);
					rectangle4NegativePath.ApplyTransform(transform);
					UIColor.Gray.SetFill();
					rectangle4NegativePath.Fill();
				}
				context.RestoreState();

				color5.SetStroke();
				rectangle4Path.LineWidth = 1;
				rectangle4Path.Stroke();


				//// Rectangle 6 Drawing
				UIBezierPath rectangle6Path = new UIBezierPath();
				rectangle6Path.MoveTo(new PointF(13.62f, 22.66f));
				rectangle6Path.AddLineTo(new PointF(13.62f, 22.66f));
				rectangle6Path.AddLineTo(new PointF(13.62f, 22.66f));
				rectangle6Path.ClosePath();
				rectangle6Path.MoveTo(new PointF(13.74f, 26.87f));
				rectangle6Path.AddLineTo(new PointF(13.74f, 15.69f));
				rectangle6Path.AddLineTo(new PointF(15.75f, 19.58f));
				rectangle6Path.AddLineTo(new PointF(15.75f, 28.96f));
				rectangle6Path.AddLineTo(new PointF(13.74f, 26.87f));
				rectangle6Path.ClosePath();
				color5.SetFill();
				rectangle6Path.Fill();

				////// Rectangle 6 Inner Shadow
				var rectangle6BorderRect = rectangle6Path.Bounds;
				rectangle6BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
				rectangle6BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
				rectangle6BorderRect = RectangleF.Union(rectangle6BorderRect, rectangle6Path.Bounds);
				rectangle6BorderRect.Inflate(1, 1);

				var rectangle6NegativePath = UIBezierPath.FromRect(rectangle6BorderRect);
				rectangle6NegativePath.AppendPath(rectangle6Path);
				rectangle6NegativePath.UsesEvenOddFillRule = true;

				context.SaveState();
				{
					var xOffset = shadowOffset.Width + (float)Math.Round(rectangle6BorderRect.Width);
					var yOffset = shadowOffset.Height;
					context.SetShadowWithColor(
						new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadowBlurRadius,
						shadow);

					rectangle6Path.AddClip();
					var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(rectangle6BorderRect.Width), 0);
					rectangle6NegativePath.ApplyTransform(transform);
					UIColor.Gray.SetFill();
					rectangle6NegativePath.Fill();
				}
				context.RestoreState();

				color5.SetStroke();
				rectangle6Path.LineWidth = 1;
				rectangle6Path.Stroke();


				//// Rectangle Drawing
				var rectanglePath = UIBezierPath.FromRect(new RectangleF(16.5f, 19.5f, 4, 9));
				color5.SetFill();
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

				color5.SetStroke();
				rectanglePath.LineWidth = 1;
				rectanglePath.Stroke();


				//// Rectangle 2 Drawing
				var rectangle2Path = UIBezierPath.FromRect(new RectangleF(5.5f, 6.5f, 3, 7));
				color5.SetFill();
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

				color5.SetStroke();
				rectangle2Path.LineWidth = 1;
				rectangle2Path.Stroke();


				//// Bezier Drawing
				UIBezierPath bezierPath = new UIBezierPath();
				bezierPath.MoveTo(new PointF(4.67f, 4.04f));
				bezierPath.AddLineTo(new PointF(15.25f, 18.72f));
				bezierPath.AddLineTo(new PointF(44.45f, 18.72f));
				bezierPath.AddLineTo(new PointF(25.82f, 4.04f));
				bezierPath.AddLineTo(new PointF(6.19f, 4.04f));
				bezierPath.AddLineTo(new PointF(4.67f, 4.04f));
				bezierPath.ClosePath();
				color5.SetFill();
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

				color5.SetStroke();
				bezierPath.LineWidth = 2;
				bezierPath.Stroke();


				//// Rectangle 3 Drawing
				var rectangle3Path = UIBezierPath.FromRect(new RectangleF(39.5f, 19.5f, 5, 9));
				color5.SetFill();
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

				color5.SetStroke();
				rectangle3Path.LineWidth = 1;
				rectangle3Path.Stroke();
			}



		}

	}
}

