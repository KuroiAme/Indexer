using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace GarageIndex
{
	public class GalleryIcon : UIView
	{
		public GalleryIcon(){
			Frame = new RectangleF (0, 0, 96, 64);
			this.SetNeedsDisplay ();
			this.BackgroundColor = UIColor.Clear;
		}

		public override void Draw (RectangleF rect)
		{
			base.Draw (rect);

			Console.WriteLine ("drawing ze recticle");

			//// Rectang//// General Declarations
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			UIColor color = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 1.000f);
			UIColor shadowColor2 = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.462f);
			UIColor color4 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.000f);

			//// Shadow Declarations
			var shadow = shadowColor2.CGColor;
			var shadowOffset = new SizeF(1.1f, 1.1f);
			var shadowBlurRadius = 17;

			//// Rectangle Drawing
			var rectanglePath = UIBezierPath.FromRect(new RectangleF(10.5f, 6.5f, 68, 35));
			color4.SetFill();
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

			color.SetStroke();
			rectanglePath.LineWidth = 1;
			rectanglePath.Stroke();


			//// Rectangle 2 Drawing
			var rectangle2Path = UIBezierPath.FromRect(new RectangleF(20.5f, 17.5f, 66, 36));
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

			color.SetStroke();
			rectangle2Path.LineWidth = 1;
			rectangle2Path.Stroke();



		}
	}
}
