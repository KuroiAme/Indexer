using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace GarageIndex
{
	public class GalleryIcon : UIView
	{
		public GalleryIcon(){
			Frame = new RectangleF (0, 0, 50, 50);
			this.SetNeedsDisplay ();
			this.BackgroundColor = UIColor.Clear;
		}

		public override void Draw (RectangleF rect)
		{
			base.Draw (rect);

			Console.WriteLine ("drawing ze recticle");

//// Rectangle Drawing
			var rectanglePath = UIBezierPath.FromRect(new RectangleF(5.5f, 7.5f, 28, 27));
			UIColor.White.SetFill();
			rectanglePath.Fill();
			UIColor.Black.SetStroke();
			rectanglePath.LineWidth = 1;
			rectanglePath.Stroke();


//// Rectangle 2 Drawing
			var rectangle2Path = UIBezierPath.FromRect(new RectangleF(18.5f, 21.5f, 26, 24));
			UIColor.White.SetFill();
			rectangle2Path.Fill();
			UIColor.Black.SetStroke();
			rectangle2Path.LineWidth = 1;
			rectangle2Path.Stroke();


		}
	}
}
