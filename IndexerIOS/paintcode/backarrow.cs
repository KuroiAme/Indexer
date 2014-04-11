using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreGraphics;

namespace no.dctapps.commons.events
{
	public class backarrow
	{
		public backarrow ()
		{
		}

		public static UIImage MakeBackArrow (){
			bool retina = (UIScreen.MainScreen.Scale > 1.0);
			if (retina) {
				UIGraphics.BeginImageContext (new System.Drawing.SizeF (44, 44));
				//BEGIN PAINTCODE RETINA

				//// Color Declarations
				UIColor gradient2Color = UIColor.FromRGBA(0.018f, 0.324f, 0.969f, 1.000f);

				//// Bezier 5 Drawing
				UIBezierPath bezier5Path = new UIBezierPath();
				bezier5Path.MoveTo(new PointF(14.5f, 16.31f));
				bezier5Path.AddLineTo(new PointF(39.5f, 16.31f));
				bezier5Path.AddLineTo(new PointF(39.5f, 26.69f));
				bezier5Path.AddLineTo(new PointF(14.5f, 26.69f));
				bezier5Path.AddLineTo(new PointF(14.5f, 41.5f));
				bezier5Path.AddLineTo(new PointF(2.5f, 20.32f));
				bezier5Path.AddLineTo(new PointF(14.5f, 1.5f));
				bezier5Path.AddLineTo(new PointF(14.5f, 16.31f));
				bezier5Path.ClosePath();
				gradient2Color.SetFill();
				bezier5Path.Fill();
				gradient2Color.SetStroke();
				bezier5Path.LineWidth = 1;
				bezier5Path.Stroke();

				//END PAINTCODE RETINA


			} else {

				UIGraphics.BeginImageContext (new System.Drawing.SizeF (22, 22));

				//start paintcode NON RETINA

				//// Color Declarations
				UIColor gradient2Color = UIColor.FromRGBA(0.018f, 0.324f, 0.969f, 1.000f);

				//// Bezier 5 Drawing
				UIBezierPath bezier5Path = new UIBezierPath();
				bezier5Path.MoveTo(new PointF(7.99f, 8.91f));
				bezier5Path.AddLineTo(new PointF(21.5f, 8.91f));
				bezier5Path.AddLineTo(new PointF(21.5f, 14.09f));
				bezier5Path.AddLineTo(new PointF(7.99f, 14.09f));
				bezier5Path.AddLineTo(new PointF(7.5f, 20.5f));
				bezier5Path.AddLineTo(new PointF(1.5f, 10.91f));
				bezier5Path.AddLineTo(new PointF(7.99f, 1.5f));
				bezier5Path.AddLineTo(new PointF(7.99f, 8.91f));
				bezier5Path.ClosePath();
				gradient2Color.SetFill();
				bezier5Path.Fill();
				gradient2Color.SetStroke();
				bezier5Path.LineWidth = 1;
				bezier5Path.Stroke();
				//END PAINTCODE NON RETINA
			}
			var converted = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return converted;

		}
	}
}

