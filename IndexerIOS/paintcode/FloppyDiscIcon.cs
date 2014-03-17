using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;

namespace IndexerIOS
{
	public static class FloppyDiscIcon
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

			//// Color Declarations
			UIColor color = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.000f);

			//// Group
			{
				//// Rectangle Drawing
				var rectanglePath = UIBezierPath.FromRoundedRect(new RectangleF(1.5f, 0.5f, 42, 42), UIRectCorner.TopRight, new SizeF(8, 8));
				rectanglePath.ClosePath();
				color.SetFill();
				rectanglePath.Fill();
				UIColor.Black.SetStroke();
				rectanglePath.LineWidth = 1;
				rectanglePath.Stroke();


				//// Rectangle 2 Drawing
				var rectangle2Path = UIBezierPath.FromRect(new RectangleF(7.5f, 0.5f, 28, 14));
				color.SetFill();
				rectangle2Path.Fill();
				UIColor.Black.SetStroke();
				rectangle2Path.LineWidth = 1;
				rectangle2Path.Stroke();


				//// Oval Drawing
				var ovalPath = UIBezierPath.FromOval(new RectangleF(23.5f, 2.5f, 8, 10));
				color.SetFill();
				ovalPath.Fill();
				UIColor.Black.SetStroke();
				ovalPath.LineWidth = 1;
				ovalPath.Stroke();


				//// Rectangle 3 Drawing
				var rectangle3Path = UIBezierPath.FromRect(new RectangleF(7.5f, 22.5f, 30, 20));
				color.SetFill();
				rectangle3Path.Fill();
				UIColor.Black.SetStroke();
				rectangle3Path.LineWidth = 1;
				rectangle3Path.Stroke();
			}



	}

		static void paintCodeNonRetina ()
		{
			//// Color Declarations
			UIColor color = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.000f);

			//// Group
			{
				//// Rectangle Drawing
				var rectanglePath = UIBezierPath.FromRoundedRect(new RectangleF(0.5f, 0.5f, 21, 21), UIRectCorner.TopRight, new SizeF(8, 8));
				rectanglePath.ClosePath();
				color.SetFill();
				rectanglePath.Fill();
				UIColor.Black.SetStroke();
				rectanglePath.LineWidth = 1;
				rectanglePath.Stroke();


				//// Rectangle 2 Drawing
				var rectangle2Path = UIBezierPath.FromRect(new RectangleF(3.5f, 0.5f, 14, 7));
				color.SetFill();
				rectangle2Path.Fill();
				UIColor.Black.SetStroke();
				rectangle2Path.LineWidth = 1;
				rectangle2Path.Stroke();


				//// Oval Drawing
				var ovalPath = UIBezierPath.FromOval(new RectangleF(11.5f, 1.5f, 4, 5));
				color.SetFill();
				ovalPath.Fill();
				UIColor.Black.SetStroke();
				ovalPath.LineWidth = 1;
				ovalPath.Stroke();


				//// Rectangle 3 Drawing
				var rectangle3Path = UIBezierPath.FromRect(new RectangleF(3.5f, 11.5f, 15, 10));
				color.SetFill();
				rectangle3Path.Fill();
				UIColor.Black.SetStroke();
				rectangle3Path.LineWidth = 1;
				rectangle3Path.Stroke();
			}



		}
	}
}


