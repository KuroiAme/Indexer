using System;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.model;
using GarageIndex;
using no.dctapps.Garageindex.events;
using MonoTouch.MessageUI;
using GoogleAnalytics.iOS;
using GoogleAdMobAds;
using System.Drawing;

namespace no.dctapps.Garageindex.screens
{
	public partial class ContainerDetails : UtilityViewController
	{
		LagerObject boks;
		ContainerDetailsContent cdc;

		public event EventHandler<LagerObjectSavedEventArgs> LagerObjectSaved;
		
		
		public ContainerDetails (LagerObject boks)
		{
			this.boks = boks;
		}
		
		public ContainerDetails () 
		{
		}

		protected override void Dispose (bool disposing)
		{
			cdc.Dispose ();
			//boks = null;
			mailContr.Dispose ();
			LagerObjectSaved = null;
			base.Dispose (disposing);
		}

		/// <summary>
		/// Release everything not in use
		/// </summary>
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

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Container detail Screen");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Container details", "Container details");
			Background back = new Background();
			View.Add(back.View);
			View.SendSubviewToBack(back.View);
			ShowDetails (boks);
			InitializeAdds ();
		}

		public static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		GADBannerView adView;
		bool viewOnScreen = false;

		void InitializeAdds ()
		{
			PointF origo;
			GADAdSize type;
			if (UserInterfaceIdiomIsPhone) {
				origo = new PointF (0, UIScreen.MainScreen.Bounds.Height);
				type = GADAdSizeCons.Banner;
			} else {
				origo = new PointF (0, UIScreen.MainScreen.Bounds.Height);
				type = GADAdSizeCons.FullBanner;
			}

			adView = new GADBannerView (size: type, origin: origo) {
				AdUnitID = AppDelegate.AdmobID,
				RootViewController = this
			};

			adView.DidReceiveAd += (sender, args) => {
				if (!viewOnScreen) View.AddSubview (adView);
				viewOnScreen = true;
			};

			adView.LoadRequest (GADRequest.Request);
		}

		UIScrollView innerview;

		public void ShowDetails (LagerObject boks)
		{
			this.boks = boks;
			cdc = new ContainerDetailsContent (boks, this);
			innerview = new UIScrollView (UIScreen.MainScreen.Bounds);
			innerview.ContentSize = cdc.GetContentsize ();
			innerview.AddSubview (cdc.View);
			innerview.UserInteractionEnabled = true;
			cdc.ShowDetails (boks);
			this.View.AddSubview (innerview);

			cdc.LagerObjectSaved += (object sender, LagerObjectSavedEventArgs e) => {
				var handler = this.LagerObjectSaved;
				if(handler != null){
					handler(this,e);
				}
			};

			//this.CreateEmailBarButton ();
		}

		MFMailComposeViewController mailContr;

		private void CreateEmailBarButton ()
		{
			//DO NOT DELETE
			UIBarButtonItem it = new UIBarButtonItem ();
			mailContr = new MFMailComposeViewController();
			mailContr.SetSubject(AppDelegate.bl.GenerateContainerSubject(this.boks));
			mailContr.SetMessageBody(AppDelegate.bl.GenerateContainerManifest(this.boks),false);
			AppDelegate.key.AddQRPictureAttachment(mailContr, this.boks);
			//			bl.AddPictureAttachment(mailContr, this.boks);
			it.Title = "email";
			//IS really info
			it.Clicked += (object sender, EventArgs e) => this.PresentViewController (mailContr, true, delegate {});
			mailContr.Finished += (object sender, MFComposeResultEventArgs e) => mailContr.DismissViewController (true, delegate{});

			this.NavigationItem.SetRightBarButtonItem (it, true);
		}
	}
}
