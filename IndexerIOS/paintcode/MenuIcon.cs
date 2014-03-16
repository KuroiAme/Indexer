using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace IndexerIOS
{
	public static class MenuIcon
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
			//// General Declarations
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			UIColor color2 = UIColor.FromRGBA(0.067f, 0.200f, 0.000f, 1.000f);
			UIColor color3 = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 1.000f);

			//// Shadow Declarations
			var shadow = UIColor.Black.CGColor;
			var shadowOffset = new SizeF(1.1f, 2.1f);
			var shadowBlurRadius = 5;

			//// Abstracted Attributes
			var menuTextContent = "+";


			//// Group
			{
				//// Oval Drawing
				var ovalPath = UIBezierPath.FromOval(new RectangleF(3.5f, 2.5f, 36, 38));
				context.SaveState();
				context.SetShadowWithColor(shadowOffset, shadowBlurRadius, shadow);
				color2.SetFill();
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

				context.RestoreState();

				UIColor.Black.SetStroke();
				ovalPath.LineWidth = 1;
				ovalPath.Stroke();


				//// MenuText Drawing
				var menuTextRect = new RectangleF(0, -13, 44, 57);
				color3.SetFill();
				new NSString(menuTextContent).DrawString(menuTextRect, UIFont.FromName("Helvetica", 48), UILineBreakMode.WordWrap, UITextAlignment.Center);
			}



		}

		static void paintCodeNonRetina ()
		{
			//// General Declarations
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			UIColor color2 = UIColor.FromRGBA(0.067f, 0.200f, 0.000f, 1.000f);
			UIColor color3 = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 1.000f);

			//// Shadow Declarations
			var shadow = UIColor.Black.CGColor;
			var shadowOffset = new SizeF(1.1f, 2.1f);
			var shadowBlurRadius = 5;

			//// Abstracted Attributes
			var menuTextContent = "+";


			//// Group
			{
				//// Oval Drawing
				var ovalPath = UIBezierPath.FromOval(new RectangleF(0.5f, 0.5f, 21, 21));
				context.SaveState();
				context.SetShadowWithColor(shadowOffset, shadowBlurRadius, shadow);
				color2.SetFill();
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

				context.RestoreState();

				UIColor.Black.SetStroke();
				ovalPath.LineWidth = 1;
				ovalPath.Stroke();


				//// MenuText Drawing
				var menuTextRect = new RectangleF(2, -6, 18, 30);
				color3.SetFill();
				new NSString(menuTextContent).DrawString(menuTextRect, UIFont.FromName("Helvetica", 24), UILineBreakMode.WordWrap, UITextAlignment.Center);
			}



		}
	}
}

