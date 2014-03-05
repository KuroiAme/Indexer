using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Drawing;

namespace GarageIndex
{
	class HelpScreen : UIViewController
	{
		void cleanup ()
		{
			//DO DIDDLY FOR NOW
		}

	
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			if(this.IsViewLoaded && this.View.Window == null){
				cleanup ();

			}
			// Release any cached data, images, etc that aren't in use.
		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = AppDelegate.its.getTranslatedText ("Help");

			Background back = new Background ();
			this.View.AddSubview (back.View);
			this.View.SendSubviewToBack (back.View);


			HelpScreenInner innerViewController = new HelpScreenInner ();
			UIScrollView innerScroll = new UIScrollView (View.Bounds);
			innerScroll.ContentSize = innerViewController.GetContentSize ();
			innerScroll.AddSubview (innerViewController.View);
			innerScroll.UserInteractionEnabled = true;
			View.AddSubview (innerScroll);
		}

	}
}

