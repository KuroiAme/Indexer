using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Drawing;
using GoogleAdMobAds;

namespace no.dctapps.commons.events
{
	class HelpScreen : UtilityViewController
	{
		GADBannerView adView;
		bool viewOnScreen = false;

		void cleanup ()
		{
			Dispose ();
		}

		protected override void Dispose (bool disposing)
		{

			// Brute force, remove everything
			foreach (var view in View.Subviews)
				view.RemoveFromSuperview ();
			base.Dispose ();
		}

	
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			if(this.IsViewLoaded && this.View.Window == null){
				//cleanup ();

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

			if (AppDelegate.Variant == "LITE") {
				adView = new GADBannerView (size: GADAdSizeCons.Banner, origin: new PointF (0, 66)) {
					AdUnitID = AppDelegate.AdmobID,
					RootViewController = this
				};

				adView.DidReceiveAd += (sender, args) => {
					if (!viewOnScreen) View.AddSubview (adView);
					viewOnScreen = true;
				};

				adView.LoadRequest (GADRequest.Request);
			}


			HelpScreenInner innerViewController = new HelpScreenInner ();
			UIScrollView innerScroll = new UIScrollView (View.Bounds);
			innerScroll.ContentSize = innerViewController.GetContentSize ();
			innerScroll.AddSubview (innerViewController.View);
			innerScroll.UserInteractionEnabled = true;
			View.AddSubview (innerScroll);

			UIBarButtonItem YouTube = new UIBarButtonItem ("YouTube", UIBarButtonItemStyle.Plain, null);
			YouTube.Clicked += (object sender, System.EventArgs e) => UIApplication.SharedApplication.OpenUrl (new MonoTouch.Foundation.NSUrl ("https://www.youtube.com/watch?v=aq1Ml2O8ado"));
			this.NavigationItem.SetRightBarButtonItem (YouTube,true);

		}

	}
}

