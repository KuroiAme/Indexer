using System;
using MonoTouch.UIKit;

namespace no.dctapps.commons
{
	public class Background : UIViewController
	{
		public Background ()
		{
		}

//		protected override void Dispose (bool disposing)
//		{
//			// Brute force, remove everything
//			foreach (var view in View.Subviews)
//				view.RemoveFromSuperview ();
//			base.Dispose ();
//		}

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

		void cleanup ()
		{
			Dispose ();
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			//cleanup only if view is loaded and not in a window.
			if(this.IsViewLoaded && this.View.Window == null){
				//cleanup ();
			}
			// Release any cached data, images, etc that aren't in use.
		}

	}
}

