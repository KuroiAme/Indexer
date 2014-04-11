using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace no.dctapps.commons
{
	public static class ZoomTagIcon
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
			UIColor color = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.000f);

			//// Group
			{
				//// Rectangle Drawing
				var rectangleRect = new RectangleF(0.5f, 0.5f, 43, 43);
				var rectanglePath = UIBezierPath.FromRect(rectangleRect);
				color.SetFill();
				rectanglePath.Fill();
				UIColor.Black.SetStroke();
				rectanglePath.LineWidth = 1;
				context.SaveState();
				var rectanglePattern = new float [] {3, 3, 3, 3};
				context.SetLineDash(0, rectanglePattern);
				rectanglePath.Stroke();
				context.RestoreState();
				UIColor.Black.SetFill();
				new NSString("ZOOM TAG").DrawString(RectangleF.Inflate(rectangleRect, 0, -4), UIFont.FromName("Helvetica", 14), UILineBreakMode.WordWrap, UITextAlignment.Center);
			}



		}

		static void paintCodeNonRetina ()
		{
			//// General Declarations
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			UIColor color = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.000f);

			//// Group
			{
				//// Rectangle Drawing
				var rectangleRect = new RectangleF(0.5f, 0.5f, 21, 21);
				var rectanglePath = UIBezierPath.FromRect(rectangleRect);
				color.SetFill();
				rectanglePath.Fill();
				UIColor.Black.SetStroke();
				rectanglePath.LineWidth = 1;
				context.SaveState();
				var rectanglePattern = new float [] {3, 3, 3, 3};
				context.SetLineDash(0, rectanglePattern);
				rectanglePath.Stroke();
				context.RestoreState();
				UIColor.Black.SetFill();
				new NSString("ZOOM TAG").DrawString(RectangleF.Inflate(rectangleRect, 0, -4), UIFont.FromName("Helvetica", 7), UILineBreakMode.WordWrap, UITextAlignment.Center);
			}
		}
	}
}

