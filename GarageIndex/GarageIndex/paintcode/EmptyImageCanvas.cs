using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;

namespace GarageIndex
{
	public class EmptyImageCanvas
	{
		public EmptyImageCanvas ()
		{
		}

		public static UIImage MakeEmptyCanvas (){
			UIGraphics.BeginImageContext (new System.Drawing.SizeF (305, 305));

			//BEGIN PAINTCODE
			//// General Declarations
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			UIColor lightBlue = UIColor.FromRGBA(0.000f, 0.000f, 1.000f, 0.797f);
			UIColor darkBlueTint = UIColor.FromRGBA(0.018f, 0.102f, 0.364f, 0.538f);
			UIColor color3 = UIColor.FromRGBA(0.000f, 1.000f, 1.000f, 1.000f);

			//// Shadow Declarations
			var shadow = color3.CGColor;
			var shadowOffset = new SizeF(0.1f, -0.1f);
			var shadowBlurRadius = 8.5f;

			//// Abstracted Attributes
			var textContent = "NO IMAGE, DOUBLETAP TO ADD, LONGPRESS TO DELETE";


			//// Rectangle Drawing
			var rectanglePath = UIBezierPath.FromRoundedRect(new RectangleF(8.5f, 6.5f, 289, 291), 10);
			darkBlueTint.SetFill();
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

			lightBlue.SetStroke();
			rectanglePath.LineWidth = 5;
			rectanglePath.Stroke();


			//// Text Drawing
			var textRect = new RectangleF(53.5f, 101.5f, 198, 102);
			var textPath = UIBezierPath.FromRoundedRect(textRect, 10);
			context.SaveState();
			context.SetShadowWithColor(shadowOffset, shadowBlurRadius, shadow);
			lightBlue.SetStroke();
			textPath.LineWidth = 1;
			textPath.Stroke();
			context.RestoreState();
			lightBlue.SetFill();
			new NSString(textContent).DrawString(textRect, UIFont.SystemFontOfSize(UIFont.ButtonFontSize), UILineBreakMode.WordWrap, UITextAlignment.Center);



			//END PAINTCODE

			var converted = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return converted;
		}

	}
}

