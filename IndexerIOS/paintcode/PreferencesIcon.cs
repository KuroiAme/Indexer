using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace IndexerIOS
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
			//// General Declarations
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			UIColor color = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 1.000f);
			UIColor color2 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.000f);
			UIColor shadowColor2 = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.443f);

			//// Shadow Declarations
			var shadow = shadowColor2.ColorWithAlpha(0.78f).CGColor;
			var shadowOffset = new SizeF(-4.1f, -2.1f);
			var shadowBlurRadius = 5;

			//// Bezier Drawing
			UIBezierPath bezierPath = new UIBezierPath();
			bezierPath.MoveTo(new PointF(22.5f, 4.26f));
			bezierPath.AddLineTo(new PointF(16.45f, 13.13f));
			bezierPath.AddLineTo(new PointF(6.19f, 16.17f));
			bezierPath.AddLineTo(new PointF(12.72f, 24.7f));
			bezierPath.AddLineTo(new PointF(12.42f, 35.45f));
			bezierPath.AddLineTo(new PointF(22.5f, 31.84f));
			bezierPath.AddLineTo(new PointF(32.58f, 35.45f));
			bezierPath.AddLineTo(new PointF(32.28f, 24.7f));
			bezierPath.AddLineTo(new PointF(38.81f, 16.17f));
			bezierPath.AddLineTo(new PointF(28.55f, 13.13f));
			bezierPath.AddLineTo(new PointF(22.5f, 4.26f));
			bezierPath.ClosePath();
			bezierPath.MoveTo(new PointF(35.94f, 7.36f));
			bezierPath.AddCurveToPoint(new PointF(35.94f, 35.64f), new PointF(43.35f, 15.17f), new PointF(43.35f, 27.83f));
			bezierPath.AddCurveToPoint(new PointF(9.06f, 35.64f), new PointF(28.52f, 43.45f), new PointF(16.48f, 43.45f));
			bezierPath.AddCurveToPoint(new PointF(9.06f, 7.36f), new PointF(1.65f, 27.83f), new PointF(1.65f, 15.17f));
			bezierPath.AddCurveToPoint(new PointF(35.94f, 7.36f), new PointF(16.48f, -0.45f), new PointF(28.52f, -0.45f));
			bezierPath.ClosePath();
			color2.SetFill();
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

			color.SetStroke();
			bezierPath.LineWidth = 1;
			bezierPath.Stroke();




		}

		static void paintCodeNonRetina ()
		{
			//// General Declarations
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			UIColor color = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 1.000f);
			UIColor color2 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.000f);
			UIColor shadowColor2 = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.443f);

			//// Shadow Declarations
			var shadow = shadowColor2.ColorWithAlpha(0.78f).CGColor;
			var shadowOffset = new SizeF(-4.1f, -2.1f);
			var shadowBlurRadius = 5;

			//// Bezier Drawing
			UIBezierPath bezierPath = new UIBezierPath();
			bezierPath.MoveTo(new PointF(11, 1.95f));
			bezierPath.AddLineTo(new PointF(7.98f, 6.61f));
			bezierPath.AddLineTo(new PointF(2.85f, 8.2f));
			bezierPath.AddLineTo(new PointF(6.11f, 12.68f));
			bezierPath.AddLineTo(new PointF(5.96f, 18.32f));
			bezierPath.AddLineTo(new PointF(11, 16.43f));
			bezierPath.AddLineTo(new PointF(16.04f, 18.32f));
			bezierPath.AddLineTo(new PointF(15.89f, 12.68f));
			bezierPath.AddLineTo(new PointF(19.15f, 8.2f));
			bezierPath.AddLineTo(new PointF(14.02f, 6.61f));
			bezierPath.AddLineTo(new PointF(11, 1.95f));
			bezierPath.ClosePath();
			bezierPath.MoveTo(new PointF(17.72f, 3.58f));
			bezierPath.AddCurveToPoint(new PointF(17.72f, 18.42f), new PointF(21.43f, 7.68f), new PointF(21.43f, 14.32f));
			bezierPath.AddCurveToPoint(new PointF(4.28f, 18.42f), new PointF(14.01f, 22.53f), new PointF(7.99f, 22.53f));
			bezierPath.AddCurveToPoint(new PointF(4.28f, 3.58f), new PointF(0.57f, 14.32f), new PointF(0.57f, 7.68f));
			bezierPath.AddCurveToPoint(new PointF(17.72f, 3.58f), new PointF(7.99f, -0.53f), new PointF(14.01f, -0.53f));
			bezierPath.ClosePath();
			color2.SetFill();
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

			color.SetStroke();
			bezierPath.LineWidth = 1;
			bezierPath.Stroke();



		}
	}
}

