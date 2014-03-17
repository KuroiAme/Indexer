using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace GarageIndex
{
	public class GalleryIcon : UIView
	{
		public GalleryIcon(){
		}


		public static UIImage MakeGallery (){
			bool retina = (UIScreen.MainScreen.Scale > 1.0);
			if (retina) {
				UIGraphics.BeginImageContext (new SizeF (96, 64));
				PaintCodeDrawGalleryIconRetina ();
			}else{
				UIGraphics.BeginImageContext (new SizeF (48, 32));
				PaintCodeDrawGalleryIconNonRetina ();
			}

			var converted = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return converted;

		}

		public static void PaintCodeDrawGalleryIconRetina(){

		//// General Declarations
		var context = UIGraphics.GetCurrentContext();

		//// Color Declarations
		UIColor color = UIColor.FromRGBA(0.657f, 0.000f, 0.219f, 1.000f);
		UIColor color2 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
		UIColor color4 = UIColor.FromRGBA(0.000f, 0.429f, 0.143f, 1.000f);

		//// Shadow Declarations
		var shadow = UIColor.Clear.CGColor;
		var shadowOffset = new SizeF(-6.1f, -6.1f);
		var shadowBlurRadius = 5;
		var shadow2 = UIColor.Black.CGColor;
		var shadow2Offset = new SizeF(1.1f, 1.1f);
		var shadow2BlurRadius = 5;

		//// Group
		{
			//// Rectangle Drawing
			var rectanglePath = UIBezierPath.FromRect(new RectangleF(0.5f, 0.5f, 83, 51));
			color.SetFill();
			rectanglePath.Fill();

			////// Rectangle Inner Shadow
			var rectangleBorderRect = rectanglePath.Bounds;
			rectangleBorderRect.Inflate(shadow2BlurRadius, shadow2BlurRadius);
			rectangleBorderRect.Offset(-shadow2Offset.Width, -shadow2Offset.Height);
			rectangleBorderRect = RectangleF.Union(rectangleBorderRect, rectanglePath.Bounds);
			rectangleBorderRect.Inflate(1, 1);

			var rectangleNegativePath = UIBezierPath.FromRect(rectangleBorderRect);
			rectangleNegativePath.AppendPath(rectanglePath);
			rectangleNegativePath.UsesEvenOddFillRule = true;

			context.SaveState();
			{
				var xOffset = shadow2Offset.Width + (float)Math.Round(rectangleBorderRect.Width);
				var yOffset = shadow2Offset.Height;
				context.SetShadowWithColor(
					new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
					shadow2BlurRadius,
					shadow2);

				rectanglePath.AddClip();
				var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(rectangleBorderRect.Width), 0);
				rectangleNegativePath.ApplyTransform(transform);
				UIColor.Gray.SetFill();
				rectangleNegativePath.Fill();
			}
			context.RestoreState();

			color2.SetStroke();
			rectanglePath.LineWidth = 1;
			rectanglePath.Stroke();


			//// Rectangle 2 Drawing
			var rectangle2Path = UIBezierPath.FromRect(new RectangleF(12.5f, 9.5f, 79, 51));
			context.SaveState();
			context.SetShadowWithColor(shadowOffset, shadowBlurRadius, shadow);
			color4.SetFill();
			rectangle2Path.Fill();

			////// Rectangle 2 Inner Shadow
			var rectangle2BorderRect = rectangle2Path.Bounds;
			rectangle2BorderRect.Inflate(shadow2BlurRadius, shadow2BlurRadius);
			rectangle2BorderRect.Offset(-shadow2Offset.Width, -shadow2Offset.Height);
			rectangle2BorderRect = RectangleF.Union(rectangle2BorderRect, rectangle2Path.Bounds);
			rectangle2BorderRect.Inflate(1, 1);

			var rectangle2NegativePath = UIBezierPath.FromRect(rectangle2BorderRect);
			rectangle2NegativePath.AppendPath(rectangle2Path);
			rectangle2NegativePath.UsesEvenOddFillRule = true;

			context.SaveState();
			{
				var xOffset = shadow2Offset.Width + (float)Math.Round(rectangle2BorderRect.Width);
				var yOffset = shadow2Offset.Height;
				context.SetShadowWithColor(
					new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
					shadow2BlurRadius,
					shadow2);

				rectangle2Path.AddClip();
				var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(rectangle2BorderRect.Width), 0);
				rectangle2NegativePath.ApplyTransform(transform);
				UIColor.Gray.SetFill();
				rectangle2NegativePath.Fill();
			}
			context.RestoreState();

			context.RestoreState();

			color2.SetStroke();
			rectangle2Path.LineWidth = 1;
			rectangle2Path.Stroke();
		}


	}


		public static void PaintCodeDrawGalleryIconNonRetina(){
			//// General Declarations
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			UIColor color = UIColor.FromRGBA(0.657f, 0.000f, 0.219f, 1.000f);
			UIColor color2 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
			UIColor color4 = UIColor.FromRGBA(0.000f, 0.429f, 0.143f, 1.000f);

			//// Shadow Declarations
			var shadow = UIColor.Clear.CGColor;
			var shadowOffset = new SizeF(-6.1f, -6.1f);
			var shadowBlurRadius = 5;
			var shadow2 = UIColor.Black.CGColor;
			var shadow2Offset = new SizeF(1.1f, 1.1f);
			var shadow2BlurRadius = 5;

			//// Group
			{
				//// Rectangle Drawing
				var rectanglePath = UIBezierPath.FromRect(new RectangleF(2.5f, 2.5f, 39, 22));
				color.SetFill();
				rectanglePath.Fill();

				////// Rectangle Inner Shadow
				var rectangleBorderRect = rectanglePath.Bounds;
				rectangleBorderRect.Inflate(shadow2BlurRadius, shadow2BlurRadius);
				rectangleBorderRect.Offset(-shadow2Offset.Width, -shadow2Offset.Height);
				rectangleBorderRect = RectangleF.Union(rectangleBorderRect, rectanglePath.Bounds);
				rectangleBorderRect.Inflate(1, 1);

				var rectangleNegativePath = UIBezierPath.FromRect(rectangleBorderRect);
				rectangleNegativePath.AppendPath(rectanglePath);
				rectangleNegativePath.UsesEvenOddFillRule = true;

				context.SaveState();
				{
					var xOffset = shadow2Offset.Width + (float)Math.Round(rectangleBorderRect.Width);
					var yOffset = shadow2Offset.Height;
					context.SetShadowWithColor(
						new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadow2BlurRadius,
						shadow2);

					rectanglePath.AddClip();
					var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(rectangleBorderRect.Width), 0);
					rectangleNegativePath.ApplyTransform(transform);
					UIColor.Gray.SetFill();
					rectangleNegativePath.Fill();
				}
				context.RestoreState();

				color2.SetStroke();
				rectanglePath.LineWidth = 1;
				rectanglePath.Stroke();


				//// Rectangle 2 Drawing
				var rectangle2Path = UIBezierPath.FromRect(new RectangleF(8.5f, 6.5f, 37, 22));
				context.SaveState();
				context.SetShadowWithColor(shadowOffset, shadowBlurRadius, shadow);
				color4.SetFill();
				rectangle2Path.Fill();

				////// Rectangle 2 Inner Shadow
				var rectangle2BorderRect = rectangle2Path.Bounds;
				rectangle2BorderRect.Inflate(shadow2BlurRadius, shadow2BlurRadius);
				rectangle2BorderRect.Offset(-shadow2Offset.Width, -shadow2Offset.Height);
				rectangle2BorderRect = RectangleF.Union(rectangle2BorderRect, rectangle2Path.Bounds);
				rectangle2BorderRect.Inflate(1, 1);

				var rectangle2NegativePath = UIBezierPath.FromRect(rectangle2BorderRect);
				rectangle2NegativePath.AppendPath(rectangle2Path);
				rectangle2NegativePath.UsesEvenOddFillRule = true;

				context.SaveState();
				{
					var xOffset = shadow2Offset.Width + (float)Math.Round(rectangle2BorderRect.Width);
					var yOffset = shadow2Offset.Height;
					context.SetShadowWithColor(
						new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadow2BlurRadius,
						shadow2);

					rectangle2Path.AddClip();
					var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(rectangle2BorderRect.Width), 0);
					rectangle2NegativePath.ApplyTransform(transform);
					UIColor.Gray.SetFill();
					rectangle2NegativePath.Fill();
				}
				context.RestoreState();

				context.RestoreState();

				color2.SetStroke();
				rectangle2Path.LineWidth = 1;
				rectangle2Path.Stroke();
			}

		}
	}
}

