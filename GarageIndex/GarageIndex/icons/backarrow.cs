using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace GarageIndex
{
	public class backarrow
	{
		public backarrow ()
		{
		}

		public static UIImage MakeBackArrow (){
			UIGraphics.BeginImageContext(new System.Drawing.SizeF(48,32));

			//start paintcode

			//// General Declarations
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			UIColor gradient2Color = UIColor.FromRGBA(0.018f, 0.324f, 0.969f, 1.000f);
			UIColor color3 = UIColor.FromRGBA(0.000f, 0.000f, 0.429f, 1.000f);

			//// Shadow Declarations
			var shadow = color3.CGColor;
			var shadowOffset = new SizeF(2.1f, 3.1f);
			var shadowBlurRadius = 5.5f;

			//// Bezier 5 Drawing
			UIBezierPath bezier5Path = new UIBezierPath();
			bezier5Path.MoveTo(new PointF(16.5f, 12.5f));
			bezier5Path.AddLineTo(new PointF(41.5f, 12.5f));
			bezier5Path.AddLineTo(new PointF(41.5f, 19.5f));
			bezier5Path.AddLineTo(new PointF(16.5f, 19.5f));
			bezier5Path.AddLineTo(new PointF(16.5f, 29.5f));
			bezier5Path.AddLineTo(new PointF(4.5f, 15.21f));
			bezier5Path.AddLineTo(new PointF(16.5f, 2.5f));
			bezier5Path.AddLineTo(new PointF(16.5f, 12.5f));
			bezier5Path.ClosePath();
			gradient2Color.SetFill();
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

			gradient2Color.SetStroke();
			bezier5Path.LineWidth = 1;
			bezier5Path.Stroke();

			//END PAINTCODE

			var converted = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return converted;

		}
	}
}

