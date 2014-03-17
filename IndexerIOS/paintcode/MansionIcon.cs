using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;

namespace IndexerIOS
{
	public class MansionIcon
	{
		public static UIImage MakeImage (){
			bool retina = (UIScreen.MainScreen.Scale > 1.0);
			if (retina) {
				UIGraphics.BeginImageContext (new SizeF (96, 64));
				paintCodeRetina();
			}else{
				UIGraphics.BeginImageContext (new SizeF (48, 32));
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
			UIColor color3 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
			UIColor color4 = UIColor.FromRGBA(0.429f, 0.000f, 0.000f, 1.000f);
			UIColor color5 = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 1.000f);
			UIColor color6 = UIColor.FromRGBA(0.456f, 0.456f, 0.456f, 1.000f);

			//// Shadow Declarations
			var shadow = UIColor.Black.CGColor;
			var shadowOffset = new SizeF(0.1f, -1.1f);
			var shadowBlurRadius = 8.5f;
			var shadow2 = color6.CGColor;
			var shadow2Offset = new SizeF(0.1f, -0.1f);
			var shadow2BlurRadius = 3.5f;

			//// Abstracted Attributes
			//var textContent = "Hello, World!";


			//// Group
			{
				//// Bezier 2 Drawing
				UIBezierPath bezier2Path = new UIBezierPath();
				bezier2Path.MoveTo(new PointF(32.5f, 20.5f));
				bezier2Path.AddLineTo(new PointF(32.5f, 62.5f));
				bezier2Path.AddLineTo(new PointF(49.5f, 62.5f));
				bezier2Path.AddLineTo(new PointF(47.5f, 23.5f));
				bezier2Path.AddLineTo(new PointF(32.5f, 20.5f));
				bezier2Path.ClosePath();
				color4.SetFill();
				bezier2Path.Fill();

				////// Bezier 2 Inner Shadow
				var bezier2BorderRect = bezier2Path.Bounds;
				bezier2BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
				bezier2BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
				bezier2BorderRect = RectangleF.Union(bezier2BorderRect, bezier2Path.Bounds);
				bezier2BorderRect.Inflate(1, 1);

				var bezier2NegativePath = UIBezierPath.FromRect(bezier2BorderRect);
				bezier2NegativePath.AppendPath(bezier2Path);
				bezier2NegativePath.UsesEvenOddFillRule = true;

				context.SaveState();
				{
					var xOffset = shadowOffset.Width + (float)Math.Round(bezier2BorderRect.Width);
					var yOffset = shadowOffset.Height;
					context.SetShadowWithColor(
						new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadowBlurRadius,
						shadow);

					bezier2Path.AddClip();
					var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(bezier2BorderRect.Width), 0);
					bezier2NegativePath.ApplyTransform(transform);
					UIColor.Gray.SetFill();
					bezier2NegativePath.Fill();
				}
				context.RestoreState();

				UIColor.Black.SetStroke();
				bezier2Path.LineWidth = 1;
				bezier2Path.Stroke();


				//// Bezier 18 Drawing
				UIBezierPath bezier18Path = new UIBezierPath();
				bezier18Path.MoveTo(new PointF(47.5f, 62.48f));
				bezier18Path.AddLineTo(new PointF(47.5f, 25.58f));
				bezier18Path.AddLineTo(new PointF(96.5f, 22.5f));
				bezier18Path.AddLineTo(new PointF(96.5f, 63.5f));
				bezier18Path.AddLineTo(new PointF(47.5f, 62.48f));
				bezier18Path.ClosePath();
				color4.SetFill();
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

				UIColor.Black.SetStroke();
				bezier18Path.LineWidth = 1;
				bezier18Path.Stroke();


				//// Bezier 6 Drawing
				UIBezierPath bezier6Path = new UIBezierPath();
				bezier6Path.MoveTo(new PointF(85.5f, 5.5f));
				bezier6Path.AddLineTo(new PointF(95.5f, 10.79f));
				bezier6Path.AddLineTo(new PointF(95.5f, 20.5f));
				bezier6Path.AddLineTo(new PointF(85.5f, 5.5f));
				bezier6Path.ClosePath();
				color4.SetFill();
				bezier6Path.Fill();

				////// Bezier 6 Inner Shadow
				var bezier6BorderRect = bezier6Path.Bounds;
				bezier6BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
				bezier6BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
				bezier6BorderRect = RectangleF.Union(bezier6BorderRect, bezier6Path.Bounds);
				bezier6BorderRect.Inflate(1, 1);

				var bezier6NegativePath = UIBezierPath.FromRect(bezier6BorderRect);
				bezier6NegativePath.AppendPath(bezier6Path);
				bezier6NegativePath.UsesEvenOddFillRule = true;

				context.SaveState();
				{
					var xOffset = shadowOffset.Width + (float)Math.Round(bezier6BorderRect.Width);
					var yOffset = shadowOffset.Height;
					context.SetShadowWithColor(
						new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadowBlurRadius,
						shadow);

					bezier6Path.AddClip();
					var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(bezier6BorderRect.Width), 0);
					bezier6NegativePath.ApplyTransform(transform);
					UIColor.Gray.SetFill();
					bezier6NegativePath.Fill();
				}
				context.RestoreState();

				UIColor.Black.SetStroke();
				bezier6Path.LineWidth = 1;
				bezier6Path.Stroke();


				//// Bezier 5 Drawing
				UIBezierPath bezier5Path = new UIBezierPath();
				bezier5Path.MoveTo(new PointF(45.5f, 25.5f));
				bezier5Path.AddLineTo(new PointF(96.5f, 22.02f));
				bezier5Path.AddLineTo(new PointF(84.75f, 5.5f));
				bezier5Path.AddLineTo(new PointF(38.5f, 13.5f));
				bezier5Path.AddLineTo(new PointF(45.5f, 25.5f));
				bezier5Path.ClosePath();
				color4.SetFill();
				bezier5Path.Fill();

				////// Bezier 5 Inner Shadow
				var bezier5BorderRect = bezier5Path.Bounds;
				bezier5BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
				bezier5BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
				bezier5BorderRect = RectangleF.Union(bezier5BorderRect, bezier5Path.Bounds);
				bezier5BorderRect.Inflate(1, 1);

				var bezier5NegativePath = UIBezierPath.FromRect(bezier5BorderRect);
				bezier5NegativePath.AppendPath(bezier5Path);
				bezier5NegativePath.UsesEvenOddFillRule = true;

				context.SaveState();
				{
					var xOffset = shadowOffset.Width + (float)Math.Round(bezier5BorderRect.Width);
					var yOffset = shadowOffset.Height;
					context.SetShadowWithColor(
						new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadowBlurRadius,
						shadow);

					bezier5Path.AddClip();
					var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(bezier5BorderRect.Width), 0);
					bezier5NegativePath.ApplyTransform(transform);
					UIColor.Gray.SetFill();
					bezier5NegativePath.Fill();
				}
				context.RestoreState();

				UIColor.Black.SetStroke();
				bezier5Path.LineWidth = 1;
				bezier5Path.Stroke();


				//// Bezier Drawing
				UIBezierPath bezierPath = new UIBezierPath();
				bezierPath.MoveTo(new PointF(7.5f, 26.5f));
				bezierPath.AddLineTo(new PointF(7.5f, 62.5f));
				bezierPath.AddLineTo(new PointF(31.5f, 62.5f));
				bezierPath.AddLineTo(new PointF(31.5f, 22.5f));
				bezierPath.AddLineTo(new PointF(7.5f, 26.5f));
				bezierPath.ClosePath();
				color4.SetFill();
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

				UIColor.Black.SetStroke();
				bezierPath.LineWidth = 1;
				bezierPath.Stroke();


				//// Text Drawing
				var textRect = new RectangleF(32, 23, 0, 0);
				//UIColor.Black.SetFill();
				//new NSString(textContent).DrawString(textRect, UIFont.FromName("Helvetica", 12), UILineBreakMode.WordWrap, UITextAlignment.Center);


				//// Bezier 3 Drawing
				UIBezierPath bezier3Path = new UIBezierPath();
				bezier3Path.MoveTo(new PointF(32.5f, 21.5f));
				bezier3Path.AddLineTo(new PointF(2.5f, 26.5f));
				bezier3Path.AddLineTo(new PointF(32.5f, 0.5f));
				bezier3Path.AddLineTo(new PointF(32.5f, 21.5f));
				color4.SetFill();
				bezier3Path.Fill();

				////// Bezier 3 Inner Shadow
				var bezier3BorderRect = bezier3Path.Bounds;
				bezier3BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
				bezier3BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
				bezier3BorderRect = RectangleF.Union(bezier3BorderRect, bezier3Path.Bounds);
				bezier3BorderRect.Inflate(1, 1);

				var bezier3NegativePath = UIBezierPath.FromRect(bezier3BorderRect);
				bezier3NegativePath.AppendPath(bezier3Path);
				bezier3NegativePath.UsesEvenOddFillRule = true;

				context.SaveState();
				{
					var xOffset = shadowOffset.Width + (float)Math.Round(bezier3BorderRect.Width);
					var yOffset = shadowOffset.Height;
					context.SetShadowWithColor(
						new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadowBlurRadius,
						shadow);

					bezier3Path.AddClip();
					var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(bezier3BorderRect.Width), 0);
					bezier3NegativePath.ApplyTransform(transform);
					UIColor.Gray.SetFill();
					bezier3NegativePath.Fill();
				}
				context.RestoreState();

				color3.SetStroke();
				bezier3Path.LineWidth = 1;
				bezier3Path.Stroke();


				//// Bezier 4 Drawing
				UIBezierPath bezier4Path = new UIBezierPath();
				bezier4Path.MoveTo(new PointF(31.5f, 0.5f));
				bezier4Path.AddLineTo(new PointF(50.5f, 25.5f));
				bezier4Path.AddLineTo(new PointF(31.5f, 22.5f));
				bezier4Path.AddLineTo(new PointF(31.5f, 0.5f));
				bezier4Path.ClosePath();
				color4.SetFill();
				bezier4Path.Fill();

				////// Bezier 4 Inner Shadow
				var bezier4BorderRect = bezier4Path.Bounds;
				bezier4BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
				bezier4BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
				bezier4BorderRect = RectangleF.Union(bezier4BorderRect, bezier4Path.Bounds);
				bezier4BorderRect.Inflate(1, 1);

				var bezier4NegativePath = UIBezierPath.FromRect(bezier4BorderRect);
				bezier4NegativePath.AppendPath(bezier4Path);
				bezier4NegativePath.UsesEvenOddFillRule = true;

				context.SaveState();
				{
					var xOffset = shadowOffset.Width + (float)Math.Round(bezier4BorderRect.Width);
					var yOffset = shadowOffset.Height;
					context.SetShadowWithColor(
						new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadowBlurRadius,
						shadow);

					bezier4Path.AddClip();
					var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(bezier4BorderRect.Width), 0);
					bezier4NegativePath.ApplyTransform(transform);
					UIColor.Gray.SetFill();
					bezier4NegativePath.Fill();
				}
				context.RestoreState();

				UIColor.Black.SetStroke();
				bezier4Path.LineWidth = 1;
				bezier4Path.Stroke();


				//// Rectangle 2 Drawing
				var rectangle2Path = UIBezierPath.FromRect(new RectangleF(11.5f, 32.5f, 9, 19));
				color3.SetFill();
				rectangle2Path.Fill();
				UIColor.Black.SetStroke();
				rectangle2Path.LineWidth = 1;
				rectangle2Path.Stroke();


				//// Bezier 7 Drawing
				UIBezierPath bezier7Path = new UIBezierPath();
				bezier7Path.MoveTo(new PointF(20.5f, 35.5f));
				bezier7Path.AddCurveToPoint(new PointF(11.5f, 35.5f), new PointF(12.5f, 35.5f), new PointF(11.5f, 35.5f));
				color5.SetStroke();
				bezier7Path.LineWidth = 1;
				bezier7Path.Stroke();


				//// Bezier 8 Drawing
				UIBezierPath bezier8Path = new UIBezierPath();
				bezier8Path.MoveTo(new PointF(20.5f, 39.5f));
				bezier8Path.AddCurveToPoint(new PointF(11.5f, 39.5f), new PointF(12.5f, 39.5f), new PointF(11.5f, 39.5f));
				color5.SetStroke();
				bezier8Path.LineWidth = 1;
				bezier8Path.Stroke();


				//// Bezier 9 Drawing
				UIBezierPath bezier9Path = new UIBezierPath();
				bezier9Path.MoveTo(new PointF(20.5f, 43.5f));
				bezier9Path.AddCurveToPoint(new PointF(11.5f, 43.5f), new PointF(12.5f, 43.5f), new PointF(11.5f, 43.5f));
				color5.SetStroke();
				bezier9Path.LineWidth = 1;
				bezier9Path.Stroke();


				//// Bezier 10 Drawing
				UIBezierPath bezier10Path = new UIBezierPath();
				bezier10Path.MoveTo(new PointF(20.5f, 46.5f));
				bezier10Path.AddCurveToPoint(new PointF(11.5f, 46.5f), new PointF(12.5f, 46.5f), new PointF(11.5f, 46.5f));
				color5.SetStroke();
				bezier10Path.LineWidth = 1;
				bezier10Path.Stroke();


				//// Bezier 11 Drawing
				UIBezierPath bezier11Path = new UIBezierPath();
				bezier11Path.MoveTo(new PointF(18.5f, 32.5f));
				bezier11Path.AddCurveToPoint(new PointF(18.5f, 51.5f), new PointF(18.5f, 51.5f), new PointF(18.5f, 51.5f));
				color5.SetStroke();
				bezier11Path.LineWidth = 1;
				bezier11Path.Stroke();


				//// Bezier 12 Drawing
				UIBezierPath bezier12Path = new UIBezierPath();
				bezier12Path.MoveTo(new PointF(14.5f, 32.5f));
				bezier12Path.AddCurveToPoint(new PointF(14.5f, 51.5f), new PointF(14.5f, 51.5f), new PointF(14.5f, 51.5f));
				color5.SetStroke();
				bezier12Path.LineWidth = 1;
				bezier12Path.Stroke();


				//// Rectangle 3 Drawing
				var rectangle3Path = UIBezierPath.FromRect(new RectangleF(38.5f, 39.5f, 6, 21));
				color3.SetFill();
				rectangle3Path.Fill();
				UIColor.Black.SetStroke();
				rectangle3Path.LineWidth = 1;
				rectangle3Path.Stroke();


				//// Bezier 13 Drawing
				UIBezierPath bezier13Path = new UIBezierPath();
				bezier13Path.MoveTo(new PointF(38.5f, 42.5f));
				bezier13Path.AddLineTo(new PointF(44.5f, 42.5f));
				color5.SetFill();
				bezier13Path.Fill();
				color5.SetStroke();
				bezier13Path.LineWidth = 1;
				bezier13Path.Stroke();


				//// Bezier 14 Drawing
				UIBezierPath bezier14Path = new UIBezierPath();
				bezier14Path.MoveTo(new PointF(38.5f, 46.5f));
				bezier14Path.AddLineTo(new PointF(44.5f, 46.5f));
				color5.SetFill();
				bezier14Path.Fill();
				color5.SetStroke();
				bezier14Path.LineWidth = 1;
				bezier14Path.Stroke();


				//// Bezier 15 Drawing
				UIBezierPath bezier15Path = new UIBezierPath();
				bezier15Path.MoveTo(new PointF(38.5f, 51.5f));
				bezier15Path.AddLineTo(new PointF(44.5f, 51.5f));
				color5.SetFill();
				bezier15Path.Fill();
				color5.SetStroke();
				bezier15Path.LineWidth = 1;
				bezier15Path.Stroke();


				//// Bezier 16 Drawing
				UIBezierPath bezier16Path = new UIBezierPath();
				bezier16Path.MoveTo(new PointF(38.5f, 56.5f));
				bezier16Path.AddLineTo(new PointF(44.5f, 56.5f));
				color5.SetFill();
				bezier16Path.Fill();
				color5.SetStroke();
				bezier16Path.LineWidth = 1;
				bezier16Path.Stroke();


				//// Bezier 17 Drawing
				UIBezierPath bezier17Path = new UIBezierPath();
				bezier17Path.MoveTo(new PointF(41.5f, 39.5f));
				bezier17Path.AddLineTo(new PointF(41.5f, 60.5f));
				color5.SetFill();
				bezier17Path.Fill();
				color5.SetStroke();
				bezier17Path.LineWidth = 1;
				bezier17Path.Stroke();


				//// Rectangle 8 Drawing
				var rectangle8Path = UIBezierPath.FromRect(new RectangleF(79.5f, 53.5f, 6, 9));
				color3.SetFill();
				rectangle8Path.Fill();

				////// Rectangle 8 Inner Shadow
				var rectangle8BorderRect = rectangle8Path.Bounds;
				rectangle8BorderRect.Inflate(shadow2BlurRadius, shadow2BlurRadius);
				rectangle8BorderRect.Offset(-shadow2Offset.Width, -shadow2Offset.Height);
				rectangle8BorderRect = RectangleF.Union(rectangle8BorderRect, rectangle8Path.Bounds);
				rectangle8BorderRect.Inflate(1, 1);

				var rectangle8NegativePath = UIBezierPath.FromRect(rectangle8BorderRect);
				rectangle8NegativePath.AppendPath(rectangle8Path);
				rectangle8NegativePath.UsesEvenOddFillRule = true;

				context.SaveState();
				{
					var xOffset = shadow2Offset.Width + (float)Math.Round(rectangle8BorderRect.Width);
					var yOffset = shadow2Offset.Height;
					context.SetShadowWithColor(
						new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadow2BlurRadius,
						shadow2);

					rectangle8Path.AddClip();
					var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(rectangle8BorderRect.Width), 0);
					rectangle8NegativePath.ApplyTransform(transform);
					UIColor.Gray.SetFill();
					rectangle8NegativePath.Fill();
				}
				context.RestoreState();

				UIColor.Black.SetStroke();
				rectangle8Path.LineWidth = 1;
				rectangle8Path.Stroke();


				//// Rectangle 5 Drawing
				var rectangle5Path = UIBezierPath.FromRect(new RectangleF(53.5f, 29.5f, 6, 7));
				color3.SetFill();
				rectangle5Path.Fill();
				UIColor.Black.SetStroke();
				rectangle5Path.LineWidth = 1;
				rectangle5Path.Stroke();


				//// Bezier 19 Drawing
				UIBezierPath bezier19Path = new UIBezierPath();
				bezier19Path.MoveTo(new PointF(53.5f, 31.5f));
				bezier19Path.AddCurveToPoint(new PointF(59.5f, 31.5f), new PointF(59.5f, 31.5f), new PointF(59.5f, 31.5f));
				color5.SetStroke();
				bezier19Path.LineWidth = 1;
				bezier19Path.Stroke();


				//// Bezier 20 Drawing
				UIBezierPath bezier20Path = new UIBezierPath();
				bezier20Path.MoveTo(new PointF(53.5f, 35.5f));
				bezier20Path.AddLineTo(new PointF(59.5f, 35.5f));
				color5.SetStroke();
				bezier20Path.LineWidth = 1;
				bezier20Path.Stroke();


				//// Bezier 21 Drawing
				UIBezierPath bezier21Path = new UIBezierPath();
				bezier21Path.MoveTo(new PointF(56.5f, 29.5f));
				bezier21Path.AddLineTo(new PointF(56.5f, 36.5f));
				color5.SetStroke();
				bezier21Path.LineWidth = 1;
				bezier21Path.Stroke();


				//// Rectangle Drawing
				var rectanglePath = UIBezierPath.FromRect(new RectangleF(64.5f, 29.5f, 6, 7));
				color3.SetFill();
				rectanglePath.Fill();
				UIColor.Black.SetStroke();
				rectanglePath.LineWidth = 1;
				rectanglePath.Stroke();


				//// Bezier 22 Drawing
				UIBezierPath bezier22Path = new UIBezierPath();
				bezier22Path.MoveTo(new PointF(64.5f, 31.5f));
				bezier22Path.AddCurveToPoint(new PointF(70.5f, 31.5f), new PointF(70.5f, 31.5f), new PointF(70.5f, 31.5f));
				color5.SetStroke();
				bezier22Path.LineWidth = 1;
				bezier22Path.Stroke();


				//// Bezier 23 Drawing
				UIBezierPath bezier23Path = new UIBezierPath();
				bezier23Path.MoveTo(new PointF(64.5f, 35.5f));
				bezier23Path.AddLineTo(new PointF(70.5f, 35.5f));
				color5.SetStroke();
				bezier23Path.LineWidth = 1;
				bezier23Path.Stroke();


				//// Bezier 24 Drawing
				UIBezierPath bezier24Path = new UIBezierPath();
				bezier24Path.MoveTo(new PointF(67.5f, 29.5f));
				bezier24Path.AddLineTo(new PointF(67.5f, 36.5f));
				color5.SetStroke();
				bezier24Path.LineWidth = 1;
				bezier24Path.Stroke();


				//// Rectangle 4 Drawing
				var rectangle4Path = UIBezierPath.FromRect(new RectangleF(76.5f, 28.5f, 6, 7));
				color3.SetFill();
				rectangle4Path.Fill();
				UIColor.Black.SetStroke();
				rectangle4Path.LineWidth = 1;
				rectangle4Path.Stroke();


				//// Bezier 25 Drawing
				UIBezierPath bezier25Path = new UIBezierPath();
				bezier25Path.MoveTo(new PointF(76.5f, 30.5f));
				bezier25Path.AddCurveToPoint(new PointF(82.5f, 30.5f), new PointF(82.5f, 30.5f), new PointF(82.5f, 30.5f));
				color5.SetStroke();
				bezier25Path.LineWidth = 1;
				bezier25Path.Stroke();


				//// Bezier 26 Drawing
				UIBezierPath bezier26Path = new UIBezierPath();
				bezier26Path.MoveTo(new PointF(76.5f, 34.5f));
				bezier26Path.AddLineTo(new PointF(82.5f, 34.5f));
				color5.SetStroke();
				bezier26Path.LineWidth = 1;
				bezier26Path.Stroke();


				//// Bezier 27 Drawing
				UIBezierPath bezier27Path = new UIBezierPath();
				bezier27Path.MoveTo(new PointF(79.5f, 28.5f));
				bezier27Path.AddLineTo(new PointF(79.5f, 35.5f));
				color5.SetStroke();
				bezier27Path.LineWidth = 1;
				bezier27Path.Stroke();
			}



		}

		static void paintCodeNonRetina ()
		{
			//// General Declarations
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			UIColor color3 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
			UIColor color4 = UIColor.FromRGBA(0.429f, 0.000f, 0.000f, 1.000f);
			UIColor color5 = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 1.000f);
			UIColor color6 = UIColor.FromRGBA(0.456f, 0.456f, 0.456f, 1.000f);

			//// Shadow Declarations
			var shadow = UIColor.Black.CGColor;
			var shadowOffset = new SizeF(0.1f, -1.1f);
			var shadowBlurRadius = 8.5f;
			var shadow2 = color6.CGColor;
			var shadow2Offset = new SizeF(0.1f, -0.1f);
			var shadow2BlurRadius = 3.5f;

			//// Abstracted Attributes
			//var textContent = "Hello, World!";


			//// Group
			{
				//// Bezier 2 Drawing
				UIBezierPath bezier2Path = new UIBezierPath();
				bezier2Path.MoveTo(new PointF(16.77f, 10.57f));
				bezier2Path.AddLineTo(new PointF(16.77f, 32.23f));
				bezier2Path.AddLineTo(new PointF(25, 32.23f));
				bezier2Path.AddLineTo(new PointF(24.03f, 12.12f));
				bezier2Path.AddLineTo(new PointF(16.77f, 10.57f));
				bezier2Path.ClosePath();
				color4.SetFill();
				bezier2Path.Fill();

				////// Bezier 2 Inner Shadow
				var bezier2BorderRect = bezier2Path.Bounds;
				bezier2BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
				bezier2BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
				bezier2BorderRect = RectangleF.Union(bezier2BorderRect, bezier2Path.Bounds);
				bezier2BorderRect.Inflate(1, 1);

				var bezier2NegativePath = UIBezierPath.FromRect(bezier2BorderRect);
				bezier2NegativePath.AppendPath(bezier2Path);
				bezier2NegativePath.UsesEvenOddFillRule = true;

				context.SaveState();
				{
					var xOffset = shadowOffset.Width + (float)Math.Round(bezier2BorderRect.Width);
					var yOffset = shadowOffset.Height;
					context.SetShadowWithColor(
						new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadowBlurRadius,
						shadow);

					bezier2Path.AddClip();
					var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(bezier2BorderRect.Width), 0);
					bezier2NegativePath.ApplyTransform(transform);
					UIColor.Gray.SetFill();
					bezier2NegativePath.Fill();
				}
				context.RestoreState();

				UIColor.Black.SetStroke();
				bezier2Path.LineWidth = 1;
				bezier2Path.Stroke();


				//// Bezier 18 Drawing
				UIBezierPath bezier18Path = new UIBezierPath();
				bezier18Path.MoveTo(new PointF(24.03f, 32.21f));
				bezier18Path.AddLineTo(new PointF(24.03f, 13.19f));
				bezier18Path.AddLineTo(new PointF(47.76f, 11.6f));
				bezier18Path.AddLineTo(new PointF(47.76f, 32.74f));
				bezier18Path.AddLineTo(new PointF(24.03f, 32.21f));
				bezier18Path.ClosePath();
				color4.SetFill();
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

				UIColor.Black.SetStroke();
				bezier18Path.LineWidth = 1;
				bezier18Path.Stroke();


				//// Bezier 6 Drawing
				UIBezierPath bezier6Path = new UIBezierPath();
				bezier6Path.MoveTo(new PointF(42.43f, 2.84f));
				bezier6Path.AddLineTo(new PointF(47.27f, 5.57f));
				bezier6Path.AddLineTo(new PointF(47.27f, 10.57f));
				bezier6Path.AddLineTo(new PointF(42.43f, 2.84f));
				bezier6Path.ClosePath();
				color4.SetFill();
				bezier6Path.Fill();

				////// Bezier 6 Inner Shadow
				var bezier6BorderRect = bezier6Path.Bounds;
				bezier6BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
				bezier6BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
				bezier6BorderRect = RectangleF.Union(bezier6BorderRect, bezier6Path.Bounds);
				bezier6BorderRect.Inflate(1, 1);

				var bezier6NegativePath = UIBezierPath.FromRect(bezier6BorderRect);
				bezier6NegativePath.AppendPath(bezier6Path);
				bezier6NegativePath.UsesEvenOddFillRule = true;

				context.SaveState();
				{
					var xOffset = shadowOffset.Width + (float)Math.Round(bezier6BorderRect.Width);
					var yOffset = shadowOffset.Height;
					context.SetShadowWithColor(
						new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadowBlurRadius,
						shadow);

					bezier6Path.AddClip();
					var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(bezier6BorderRect.Width), 0);
					bezier6NegativePath.ApplyTransform(transform);
					UIColor.Gray.SetFill();
					bezier6NegativePath.Fill();
				}
				context.RestoreState();

				UIColor.Black.SetStroke();
				bezier6Path.LineWidth = 1;
				bezier6Path.Stroke();


				//// Bezier 5 Drawing
				UIBezierPath bezier5Path = new UIBezierPath();
				bezier5Path.MoveTo(new PointF(23.06f, 13.15f));
				bezier5Path.AddLineTo(new PointF(47.76f, 11.35f));
				bezier5Path.AddLineTo(new PointF(42.07f, 2.84f));
				bezier5Path.AddLineTo(new PointF(19.67f, 6.96f));
				bezier5Path.AddLineTo(new PointF(23.06f, 13.15f));
				bezier5Path.ClosePath();
				color4.SetFill();
				bezier5Path.Fill();

				////// Bezier 5 Inner Shadow
				var bezier5BorderRect = bezier5Path.Bounds;
				bezier5BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
				bezier5BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
				bezier5BorderRect = RectangleF.Union(bezier5BorderRect, bezier5Path.Bounds);
				bezier5BorderRect.Inflate(1, 1);

				var bezier5NegativePath = UIBezierPath.FromRect(bezier5BorderRect);
				bezier5NegativePath.AppendPath(bezier5Path);
				bezier5NegativePath.UsesEvenOddFillRule = true;

				context.SaveState();
				{
					var xOffset = shadowOffset.Width + (float)Math.Round(bezier5BorderRect.Width);
					var yOffset = shadowOffset.Height;
					context.SetShadowWithColor(
						new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadowBlurRadius,
						shadow);

					bezier5Path.AddClip();
					var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(bezier5BorderRect.Width), 0);
					bezier5NegativePath.ApplyTransform(transform);
					UIColor.Gray.SetFill();
					bezier5NegativePath.Fill();
				}
				context.RestoreState();

				UIColor.Black.SetStroke();
				bezier5Path.LineWidth = 1;
				bezier5Path.Stroke();


				//// Bezier Drawing
				UIBezierPath bezierPath = new UIBezierPath();
				bezierPath.MoveTo(new PointF(4.66f, 13.66f));
				bezierPath.AddLineTo(new PointF(4.66f, 32.23f));
				bezierPath.AddLineTo(new PointF(16.28f, 32.23f));
				bezierPath.AddLineTo(new PointF(16.28f, 11.6f));
				bezierPath.AddLineTo(new PointF(4.66f, 13.66f));
				bezierPath.ClosePath();
				color4.SetFill();
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

				UIColor.Black.SetStroke();
				bezierPath.LineWidth = 1;
				bezierPath.Stroke();


				//// Text Drawing
//				var textRect = new RectangleF(17, 12, 0, 0);
//				UIColor.Black.SetFill();
//				new NSString(textContent).DrawString(textRect, UIFont.FromName("Helvetica", 12), UILineBreakMode.WordWrap, UITextAlignment.Center);


				//// Bezier 3 Drawing
				UIBezierPath bezier3Path = new UIBezierPath();
				bezier3Path.MoveTo(new PointF(16.77f, 11.09f));
				bezier3Path.AddLineTo(new PointF(2.24f, 13.66f));
				bezier3Path.AddLineTo(new PointF(16.77f, 0.26f));
				bezier3Path.AddLineTo(new PointF(16.77f, 11.09f));
				color4.SetFill();
				bezier3Path.Fill();

				////// Bezier 3 Inner Shadow
				var bezier3BorderRect = bezier3Path.Bounds;
				bezier3BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
				bezier3BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
				bezier3BorderRect = RectangleF.Union(bezier3BorderRect, bezier3Path.Bounds);
				bezier3BorderRect.Inflate(1, 1);

				var bezier3NegativePath = UIBezierPath.FromRect(bezier3BorderRect);
				bezier3NegativePath.AppendPath(bezier3Path);
				bezier3NegativePath.UsesEvenOddFillRule = true;

				context.SaveState();
				{
					var xOffset = shadowOffset.Width + (float)Math.Round(bezier3BorderRect.Width);
					var yOffset = shadowOffset.Height;
					context.SetShadowWithColor(
						new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadowBlurRadius,
						shadow);

					bezier3Path.AddClip();
					var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(bezier3BorderRect.Width), 0);
					bezier3NegativePath.ApplyTransform(transform);
					UIColor.Gray.SetFill();
					bezier3NegativePath.Fill();
				}
				context.RestoreState();

				color3.SetStroke();
				bezier3Path.LineWidth = 1;
				bezier3Path.Stroke();


				//// Bezier 4 Drawing
				UIBezierPath bezier4Path = new UIBezierPath();
				bezier4Path.MoveTo(new PointF(16.28f, 0.26f));
				bezier4Path.AddLineTo(new PointF(25.48f, 13.15f));
				bezier4Path.AddLineTo(new PointF(16.28f, 11.6f));
				bezier4Path.AddLineTo(new PointF(16.28f, 0.26f));
				bezier4Path.ClosePath();
				color4.SetFill();
				bezier4Path.Fill();

				////// Bezier 4 Inner Shadow
				var bezier4BorderRect = bezier4Path.Bounds;
				bezier4BorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
				bezier4BorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
				bezier4BorderRect = RectangleF.Union(bezier4BorderRect, bezier4Path.Bounds);
				bezier4BorderRect.Inflate(1, 1);

				var bezier4NegativePath = UIBezierPath.FromRect(bezier4BorderRect);
				bezier4NegativePath.AppendPath(bezier4Path);
				bezier4NegativePath.UsesEvenOddFillRule = true;

				context.SaveState();
				{
					var xOffset = shadowOffset.Width + (float)Math.Round(bezier4BorderRect.Width);
					var yOffset = shadowOffset.Height;
					context.SetShadowWithColor(
						new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadowBlurRadius,
						shadow);

					bezier4Path.AddClip();
					var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(bezier4BorderRect.Width), 0);
					bezier4NegativePath.ApplyTransform(transform);
					UIColor.Gray.SetFill();
					bezier4NegativePath.Fill();
				}
				context.RestoreState();

				UIColor.Black.SetStroke();
				bezier4Path.LineWidth = 1;
				bezier4Path.Stroke();


				//// Rectangle 2 Drawing
				var rectangle2Path = UIBezierPath.FromRect(new RectangleF(6.5f, 16.5f, 4, 10));
				color3.SetFill();
				rectangle2Path.Fill();
				UIColor.Black.SetStroke();
				rectangle2Path.LineWidth = 1;
				rectangle2Path.Stroke();


				//// Bezier 7 Drawing
				UIBezierPath bezier7Path = new UIBezierPath();
				bezier7Path.MoveTo(new PointF(10.96f, 18.3f));
				bezier7Path.AddCurveToPoint(new PointF(6.6f, 18.3f), new PointF(7.08f, 18.3f), new PointF(6.6f, 18.3f));
				color5.SetStroke();
				bezier7Path.LineWidth = 1;
				bezier7Path.Stroke();


				//// Bezier 8 Drawing
				UIBezierPath bezier8Path = new UIBezierPath();
				bezier8Path.MoveTo(new PointF(10.96f, 20.37f));
				bezier8Path.AddCurveToPoint(new PointF(6.6f, 20.37f), new PointF(7.08f, 20.37f), new PointF(6.6f, 20.37f));
				color5.SetStroke();
				bezier8Path.LineWidth = 1;
				bezier8Path.Stroke();


				//// Bezier 9 Drawing
				UIBezierPath bezier9Path = new UIBezierPath();
				bezier9Path.MoveTo(new PointF(10.96f, 22.43f));
				bezier9Path.AddCurveToPoint(new PointF(6.6f, 22.43f), new PointF(7.08f, 22.43f), new PointF(6.6f, 22.43f));
				color5.SetStroke();
				bezier9Path.LineWidth = 1;
				bezier9Path.Stroke();


				//// Bezier 10 Drawing
				UIBezierPath bezier10Path = new UIBezierPath();
				bezier10Path.MoveTo(new PointF(10.96f, 23.98f));
				bezier10Path.AddCurveToPoint(new PointF(6.6f, 23.98f), new PointF(7.08f, 23.98f), new PointF(6.6f, 23.98f));
				color5.SetStroke();
				bezier10Path.LineWidth = 1;
				bezier10Path.Stroke();


				//// Bezier 11 Drawing
				UIBezierPath bezier11Path = new UIBezierPath();
				bezier11Path.MoveTo(new PointF(9.99f, 16.76f));
				bezier11Path.AddCurveToPoint(new PointF(9.99f, 26.55f), new PointF(9.99f, 26.55f), new PointF(9.99f, 26.55f));
				color5.SetStroke();
				bezier11Path.LineWidth = 1;
				bezier11Path.Stroke();


				//// Bezier 12 Drawing
				UIBezierPath bezier12Path = new UIBezierPath();
				bezier12Path.MoveTo(new PointF(8.05f, 16.76f));
				bezier12Path.AddCurveToPoint(new PointF(8.05f, 26.55f), new PointF(8.05f, 26.55f), new PointF(8.05f, 26.55f));
				color5.SetStroke();
				bezier12Path.LineWidth = 1;
				bezier12Path.Stroke();


				//// Rectangle 3 Drawing
				var rectangle3Path = UIBezierPath.FromRect(new RectangleF(19.5f, 20.5f, 3, 11));
				color3.SetFill();
				rectangle3Path.Fill();
				UIColor.Black.SetStroke();
				rectangle3Path.LineWidth = 1;
				rectangle3Path.Stroke();


				//// Bezier 13 Drawing
				UIBezierPath bezier13Path = new UIBezierPath();
				bezier13Path.MoveTo(new PointF(19.67f, 21.91f));
				bezier13Path.AddLineTo(new PointF(22.58f, 21.91f));
				color5.SetFill();
				bezier13Path.Fill();
				color5.SetStroke();
				bezier13Path.LineWidth = 1;
				bezier13Path.Stroke();


				//// Bezier 14 Drawing
				UIBezierPath bezier14Path = new UIBezierPath();
				bezier14Path.MoveTo(new PointF(19.67f, 23.98f));
				bezier14Path.AddLineTo(new PointF(22.58f, 23.98f));
				color5.SetFill();
				bezier14Path.Fill();
				color5.SetStroke();
				bezier14Path.LineWidth = 1;
				bezier14Path.Stroke();


				//// Bezier 15 Drawing
				UIBezierPath bezier15Path = new UIBezierPath();
				bezier15Path.MoveTo(new PointF(19.67f, 26.55f));
				bezier15Path.AddLineTo(new PointF(22.58f, 26.55f));
				color5.SetFill();
				bezier15Path.Fill();
				color5.SetStroke();
				bezier15Path.LineWidth = 1;
				bezier15Path.Stroke();


				//// Bezier 16 Drawing
				UIBezierPath bezier16Path = new UIBezierPath();
				bezier16Path.MoveTo(new PointF(19.67f, 29.13f));
				bezier16Path.AddLineTo(new PointF(22.58f, 29.13f));
				color5.SetFill();
				bezier16Path.Fill();
				color5.SetStroke();
				bezier16Path.LineWidth = 1;
				bezier16Path.Stroke();


				//// Bezier 17 Drawing
				UIBezierPath bezier17Path = new UIBezierPath();
				bezier17Path.MoveTo(new PointF(21.13f, 20.37f));
				bezier17Path.AddLineTo(new PointF(21.13f, 31.2f));
				color5.SetFill();
				bezier17Path.Fill();
				color5.SetStroke();
				bezier17Path.LineWidth = 1;
				bezier17Path.Stroke();


				//// Rectangle 8 Drawing
				var rectangle8Path = UIBezierPath.FromRect(new RectangleF(39.5f, 27.5f, 3, 5));
				color3.SetFill();
				rectangle8Path.Fill();

				////// Rectangle 8 Inner Shadow
				var rectangle8BorderRect = rectangle8Path.Bounds;
				rectangle8BorderRect.Inflate(shadow2BlurRadius, shadow2BlurRadius);
				rectangle8BorderRect.Offset(-shadow2Offset.Width, -shadow2Offset.Height);
				rectangle8BorderRect = RectangleF.Union(rectangle8BorderRect, rectangle8Path.Bounds);
				rectangle8BorderRect.Inflate(1, 1);

				var rectangle8NegativePath = UIBezierPath.FromRect(rectangle8BorderRect);
				rectangle8NegativePath.AppendPath(rectangle8Path);
				rectangle8NegativePath.UsesEvenOddFillRule = true;

				context.SaveState();
				{
					var xOffset = shadow2Offset.Width + (float)Math.Round(rectangle8BorderRect.Width);
					var yOffset = shadow2Offset.Height;
					context.SetShadowWithColor(
						new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadow2BlurRadius,
						shadow2);

					rectangle8Path.AddClip();
					var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(rectangle8BorderRect.Width), 0);
					rectangle8NegativePath.ApplyTransform(transform);
					UIColor.Gray.SetFill();
					rectangle8NegativePath.Fill();
				}
				context.RestoreState();

				UIColor.Black.SetStroke();
				rectangle8Path.LineWidth = 1;
				rectangle8Path.Stroke();


				//// Rectangle 5 Drawing
				var rectangle5Path = UIBezierPath.FromRect(new RectangleF(26.5f, 15.5f, 3, 3));
				color3.SetFill();
				rectangle5Path.Fill();
				UIColor.Black.SetStroke();
				rectangle5Path.LineWidth = 1;
				rectangle5Path.Stroke();


				//// Bezier 19 Drawing
				UIBezierPath bezier19Path = new UIBezierPath();
				bezier19Path.MoveTo(new PointF(26.94f, 16.24f));
				bezier19Path.AddCurveToPoint(new PointF(29.84f, 16.24f), new PointF(29.84f, 16.24f), new PointF(29.84f, 16.24f));
				color5.SetStroke();
				bezier19Path.LineWidth = 1;
				bezier19Path.Stroke();


				//// Bezier 20 Drawing
				UIBezierPath bezier20Path = new UIBezierPath();
				bezier20Path.MoveTo(new PointF(26.94f, 18.3f));
				bezier20Path.AddLineTo(new PointF(29.84f, 18.3f));
				color5.SetStroke();
				bezier20Path.LineWidth = 1;
				bezier20Path.Stroke();


				//// Bezier 21 Drawing
				UIBezierPath bezier21Path = new UIBezierPath();
				bezier21Path.MoveTo(new PointF(28.39f, 15.21f));
				bezier21Path.AddLineTo(new PointF(28.39f, 18.82f));
				color5.SetStroke();
				bezier21Path.LineWidth = 1;
				bezier21Path.Stroke();


				//// Rectangle Drawing
				var rectanglePath = UIBezierPath.FromRect(new RectangleF(32.5f, 15.5f, 3, 3));
				color3.SetFill();
				rectanglePath.Fill();
				UIColor.Black.SetStroke();
				rectanglePath.LineWidth = 1;
				rectanglePath.Stroke();


				//// Bezier 22 Drawing
				UIBezierPath bezier22Path = new UIBezierPath();
				bezier22Path.MoveTo(new PointF(32.26f, 16.24f));
				bezier22Path.AddCurveToPoint(new PointF(35.17f, 16.24f), new PointF(35.17f, 16.24f), new PointF(35.17f, 16.24f));
				color5.SetStroke();
				bezier22Path.LineWidth = 1;
				bezier22Path.Stroke();


				//// Bezier 23 Drawing
				UIBezierPath bezier23Path = new UIBezierPath();
				bezier23Path.MoveTo(new PointF(32.26f, 18.3f));
				bezier23Path.AddLineTo(new PointF(35.17f, 18.3f));
				color5.SetStroke();
				bezier23Path.LineWidth = 1;
				bezier23Path.Stroke();


				//// Bezier 24 Drawing
				UIBezierPath bezier24Path = new UIBezierPath();
				bezier24Path.MoveTo(new PointF(33.72f, 15.21f));
				bezier24Path.AddLineTo(new PointF(33.72f, 18.82f));
				color5.SetStroke();
				bezier24Path.LineWidth = 1;
				bezier24Path.Stroke();


				//// Rectangle 4 Drawing
				var rectangle4Path = UIBezierPath.FromRect(new RectangleF(38.5f, 14.5f, 2, 4));
				color3.SetFill();
				rectangle4Path.Fill();
				UIColor.Black.SetStroke();
				rectangle4Path.LineWidth = 1;
				rectangle4Path.Stroke();


				//// Bezier 25 Drawing
				UIBezierPath bezier25Path = new UIBezierPath();
				bezier25Path.MoveTo(new PointF(38.07f, 15.73f));
				bezier25Path.AddCurveToPoint(new PointF(40.98f, 15.73f), new PointF(40.98f, 15.73f), new PointF(40.98f, 15.73f));
				color5.SetStroke();
				bezier25Path.LineWidth = 1;
				bezier25Path.Stroke();


				//// Bezier 26 Drawing
				UIBezierPath bezier26Path = new UIBezierPath();
				bezier26Path.MoveTo(new PointF(38.07f, 17.79f));
				bezier26Path.AddLineTo(new PointF(40.98f, 17.79f));
				color5.SetStroke();
				bezier26Path.LineWidth = 1;
				bezier26Path.Stroke();


				//// Bezier 27 Drawing
				UIBezierPath bezier27Path = new UIBezierPath();
				bezier27Path.MoveTo(new PointF(39.53f, 14.7f));
				bezier27Path.AddLineTo(new PointF(39.53f, 18.3f));
				color5.SetStroke();
				bezier27Path.LineWidth = 1;
				bezier27Path.Stroke();
			}



		}
	}
}

