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
			bool retina = (UIScreen.MainScreen.Scale > 1.0);
			if (retina) {
				UIGraphics.BeginImageContext (new System.Drawing.SizeF (96, 64));
				//BEGIN PAINTCODE RETINA

				//// Color Declarations
				UIColor gradient2Color = UIColor.FromRGBA(0.018f, 0.324f, 0.969f, 1.000f);

				//// Bezier 5 Drawing
				UIBezierPath bezier5Path = new UIBezierPath();
				bezier5Path.MoveTo(new PointF(32.01f, 22.72f));
				bezier5Path.AddLineTo(new PointF(93.5f, 22.72f));
				bezier5Path.AddLineTo(new PointF(93.5f, 38.28f));
				bezier5Path.AddLineTo(new PointF(32.01f, 38.28f));
				bezier5Path.AddLineTo(new PointF(32.01f, 60.5f));
				bezier5Path.AddLineTo(new PointF(2.5f, 28.74f));
				bezier5Path.AddLineTo(new PointF(32.01f, 0.5f));
				bezier5Path.AddLineTo(new PointF(32.01f, 22.72f));
				bezier5Path.ClosePath();
				gradient2Color.SetFill();
				bezier5Path.Fill();
				gradient2Color.SetStroke();
				bezier5Path.LineWidth = 1;
				bezier5Path.Stroke();

				//END PAINTCODE RETINA


			} else {

				UIGraphics.BeginImageContext (new System.Drawing.SizeF (48, 32));

				//start paintcode NON RETINA

				//// Color Declarations
				UIColor gradient2Color = UIColor.FromRGBA (0.018f, 0.324f, 0.969f, 1.000f);

				//// Bezier 5 Drawing
				UIBezierPath bezier5Path = new UIBezierPath ();
				bezier5Path.MoveTo (new PointF (16.5f, 12.5f));
				bezier5Path.AddLineTo (new PointF (41.5f, 12.5f));
				bezier5Path.AddLineTo (new PointF (41.5f, 19.5f));
				bezier5Path.AddLineTo (new PointF (16.5f, 19.5f));
				bezier5Path.AddLineTo (new PointF (16.5f, 29.5f));
				bezier5Path.AddLineTo (new PointF (4.5f, 15.21f));
				bezier5Path.AddLineTo (new PointF (16.5f, 2.5f));
				bezier5Path.AddLineTo (new PointF (16.5f, 12.5f));
				bezier5Path.ClosePath ();
				gradient2Color.SetFill ();
				bezier5Path.Fill ();
				gradient2Color.SetStroke ();
				bezier5Path.LineWidth = 1;
				bezier5Path.Stroke ();
			
				//END PAINTCODE NON RETINA
			}
			var converted = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return converted;

		}
	}
}

