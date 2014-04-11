using System;
using System.Drawing;
using System.Text;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using no.dctapps.commons.events.model;
using no.dctapps.commons.events;
using no.dctapps.commons.events.screens;
using no.dctapps.commons.events.events;
using MonoTouch.MessageUI;
using GoogleAnalytics.iOS;
using GoogleAdMobAds;


namespace No.Dctapps.Garageindex.Ios.Screens
{
	public partial class BigItemDetailScreen : UtilityViewController
	{
		LagerObject myObject;
		
		public event EventHandler<GotPictureEventArgs> GotPicture;
		public event EventHandler<BigItemSavedEventArgs> BigItemSaved;
		//public event EventHandler<DerezLargeObjectEventArgs> Derezzy;

		public BigItemDetailScreen (LagerObject myObject)
		{
			this.myObject = myObject;

		}

		public BigItemDetailScreen ()
		{

		}

		protected override void Dispose (bool disposing)
		{

			myObject = null;
			GotPicture = null;
			BigItemSaved = null;
			bidc.Dispose ();
			innerScroll.Dispose ();
			base.Dispose (disposing);
		}

		/// <summary>
		/// Release everything not in use
		/// </summary>
		void cleanup ()
		{
			//this.Dispose ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Background back = new Background ();
			View.Add (back.View);
			View.SendSubviewToBack (back.View);

			Title = AppDelegate.its.getTranslatedText ("Big details");
			ShowDetails (myObject);
			InitializeAdds ();
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
				origo = new PointF (0, UIScreen.MainScreen.Bounds.Height - 200);
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


		BigItemDetailContent bidc;
		UIScrollView innerScroll;

		public void ShowDetails (LagerObject myObject){
			this.myObject = myObject;
			bidc = new BigItemDetailContent (myObject, this);
			innerScroll = new UIScrollView (UIScreen.MainScreen.Bounds);
			innerScroll.ContentSize = bidc.GetContentsize ();
			innerScroll.AddSubview (bidc.View);
			innerScroll.UserInteractionEnabled = true;
			bidc.ShowDetails (myObject);
			View.AddSubview (innerScroll);

			bidc.BigItemSaved += (object sender, BigItemSavedEventArgs e) => {
				var handler = this.BigItemSaved;
				if(handler != null){
					handler(sender, e);
				}
			};
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			CreateEmailBarButton (myObject);
		}



		MFMailComposeViewController mailContr;

		private void CreateEmailBarButton (LagerObject myobby)
		{
			//DO NOT DELETE
			if (this.myObject != null) {
			UIBarButtonItem it = new UIBarButtonItem ();
			it.Title = "email";
			//IS really info
			it.Clicked += (object sender, EventArgs e) =>  {
				mailContr = new MFMailComposeViewController();
					mailContr.SetSubject(AppDelegate.bl.GenerateSubject(myobby));
					mailContr.SetMessageBody(AppDelegate.bl.GenerateContainerManifest(myobby),false);
					AppDelegate.key.AddPictureAttachment(mailContr, myobby);
					AppDelegate.key.AddQRPictureAttachment(mailContr, myobby);
				this.PresentViewController(mailContr, true, delegate{});
				
				mailContr.Finished += (object sender2, MFComposeResultEventArgs e2) => mailContr.DismissViewController (true, delegate {});
			};
			

			this.NavigationItem.SetRightBarButtonItem (it, true);
			}
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Big items detail Screen");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}

	}
}


