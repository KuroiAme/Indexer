using System;
using MonoTouch.UIKit;

namespace GarageIndex
{
	public class Background : UIViewController
	{
		public Background ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			var imgView = new UIImageView(BlueSea.MakeBlueSea()){
				ContentMode = UIViewContentMode.ScaleToFill,
				AutoresizingMask = UIViewAutoresizing.All,
				Frame = View.Bounds
			};
			this.View.BackgroundColor = UIColor.Clear;
			View.AddSubview (imgView);
		}
	}
}

