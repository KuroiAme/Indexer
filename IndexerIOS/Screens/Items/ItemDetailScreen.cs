using GoogleAnalytics.iOS;
using GoogleAdMobAds;

namespace no.dctapps.Garageindex.screens
{
	using System;
	using System.Drawing;
	using MonoTouch.Foundation;
	using MonoTouch.UIKit;
	using no.dctapps.Garageindex.events;
	using System.Text;
	using no.dctapps.Garageindex.model;
	using No.Dctapps.GarageIndex;
	using MonoTouch.MessageUI;
	using GarageIndex;

	public partial class ItemDetailScreen : UtilityViewController
      {

		public event EventHandler<GotPictureEventArgs> GotPicture;

		public event EventHandler<ItemSavedEventArgs> ItemSaved;
		public event EventHandler<DerezEventArgs> Derez;
		public event EventHandler ItemDeleted;

		public ItemDetailsController idc;

		Item item;
		UIScrollView innerScroll;


		public ItemDetailScreen (Item item)
		{
			this.item = item;

		}

		public ItemDetailScreen ()
		{
			idc = new ItemDetailsController (this);

			//idc = new ItemDetailsController (this.NavigationController,this);

		}

		protected override void Dispose (bool disposing)
		{
			GotPicture = null;
			ItemSaved = null;
			Derez = null;
			ItemDeleted = null;
			idc.Dispose ();
			item = null;
			innerScroll.Dispose ();
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

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Item details", "Item details");

			Background back = new Background ();
			View.AddSubview (back.View);
			View.SendSubviewToBack (back.View);

			ShowDetails (item);
			InitializeAdds ();
		}

		GADBannerView adView;
		bool viewOnScreen = false;

		void InitializeAdds ()
		{
			PointF origo;
			GADAdSize type;
			if (UserInterfaceIdiomIsPhone) {
				origo = new PointF (0, UIScreen.MainScreen.Bounds.Height -100);
				type = GADAdSizeCons.Banner;
			} else {
				origo = new PointF (0, UIScreen.MainScreen.Bounds.Height - 100);
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

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			CreateEmailBarButton ();
		}




		public void ShowDetails(Item item){
			this.item = item;
			idc = new ItemDetailsController (item, this);
			//idc = new ItemDetailsController (item, this.NavigationController, this);
			innerScroll = new UIScrollView (View.Bounds);
			innerScroll.ContentSize = idc.GetContentsize ();
			innerScroll.AddSubview (idc.View);
			innerScroll.UserInteractionEnabled = true;
			idc.ShowDetails (item);
			View.AddSubview (innerScroll);

//			HelpScreenInner innerViewController = new HelpScreenInner ();
//			UIScrollView innerScroll = new UIScrollView (View.Bounds);
//			innerScroll.ContentSize = innerViewController.GetContentSize ();
//			innerScroll.AddSubview (innerViewController.View);
//			innerScroll.UserInteractionEnabled = true;
//			View.AddSubview (innerScroll);

			idc.ItemSaved += (object sender, ItemSavedEventArgs e) => {
				var handler = this.ItemSaved;
				if(handler != null){
					handler(sender, e);
				}
			};

//			idc.Derez += (object sender, DerezEventArgs e) => {
//				var handler = this.Derez;
//				if(handler != null){
//					handler(sender,e);
//				}
//			};

			idc.ItemDeleted += (object sender, EventArgs e) => {
				var handler = this.ItemDeleted;
				if(handler != null){
					handler(sender,e);
				}
			};

		}

		MFMailComposeViewController mailContr;

		private void CreateEmailBarButton ()
		{
			//DO NOT DELETE
			UIBarButtonItem it2 = new UIBarButtonItem ();
			it2.Title = "email";
			//IS really info
			it2.Clicked += (object sender, EventArgs e) =>  {
				mailContr = new MFMailComposeViewController();
				mailContr.SetSubject(AppDelegate.bl.GenerateSubject(this.item));
				mailContr.SetMessageBody(AppDelegate.bl.GenerateManifest(this.item),false);
				AppDelegate.key.AddPictureAttachment(mailContr, this.item);
				this.PresentViewController(mailContr, true, delegate{});
				mailContr.Finished += (object sender2, MFComposeResultEventArgs e2) => mailContr.DismissViewController (true, delegate{});
			};


			this.NavigationItem.SetRightBarButtonItem (it2, true);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "items detail Screen");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}

	}
}