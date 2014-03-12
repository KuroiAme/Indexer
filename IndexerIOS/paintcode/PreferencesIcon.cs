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
			//// Color Declarations
			UIColor color4 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);

			//// Star Drawing
			UIBezierPath starPath = new UIBezierPath();
			starPath.MoveTo(new PointF(22, 0.5f));
			starPath.AddLineTo(new PointF(29.58f, 11.82f));
			starPath.AddLineTo(new PointF(42.45f, 15.7f));
			starPath.AddLineTo(new PointF(34.27f, 26.58f));
			starPath.AddLineTo(new PointF(34.64f, 40.3f));
			starPath.AddLineTo(new PointF(22, 35.7f));
			starPath.AddLineTo(new PointF(9.36f, 40.3f));
			starPath.AddLineTo(new PointF(9.73f, 26.58f));
			starPath.AddLineTo(new PointF(1.55f, 15.7f));
			starPath.AddLineTo(new PointF(14.42f, 11.82f));
			starPath.ClosePath();
			color4.SetFill();
			starPath.Fill();
			UIColor.Black.SetStroke();
			starPath.LineWidth = 1;
			starPath.Stroke();



		}

		static void paintCodeNonRetina ()
		{
			//// Color Declarations
			UIColor color4 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);

			//// Star Drawing
			UIBezierPath starPath = new UIBezierPath();
			starPath.MoveTo(new PointF(11, 0.5f));
			starPath.AddLineTo(new PointF(14.7f, 6.16f));
			starPath.AddLineTo(new PointF(20.99f, 8.1f));
			starPath.AddLineTo(new PointF(16.99f, 13.54f));
			starPath.AddLineTo(new PointF(17.17f, 20.4f));
			starPath.AddLineTo(new PointF(11, 18.1f));
			starPath.AddLineTo(new PointF(4.83f, 20.4f));
			starPath.AddLineTo(new PointF(5.01f, 13.54f));
			starPath.AddLineTo(new PointF(1.01f, 8.1f));
			starPath.AddLineTo(new PointF(7.3f, 6.16f));
			starPath.ClosePath();
			color4.SetFill();
			starPath.Fill();
			UIColor.Black.SetStroke();
			starPath.LineWidth = 1;
			starPath.Stroke();



		}
	}
}

