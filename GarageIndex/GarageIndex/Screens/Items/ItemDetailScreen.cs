using GoogleAnalytics.iOS;

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

		public ItemDetailsController idc;

		Item item;
		UIScrollView innerview;


		public ItemDetailScreen (Item item)
		{
			this.item = item;

		}

		public ItemDetailScreen ()
		{
			idc = new ItemDetailsController (this.NavigationController);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Item details", "Item details");
			ShowDetails (item);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			CreateEmailBarButton ();
		}



		public void ShowDetails(Item item){
			this.item = item;
			idc = new ItemDetailsController (item, this.NavigationController);
			innerview = new UIScrollView (UIScreen.MainScreen.Bounds);
			innerview.ContentSize = idc.GetContentsize ();
			innerview.AddSubview (idc.View);
			innerview.BackgroundColor = UIColor.White;
			idc.ShowDetails (item);
			this.View = innerview;



			idc.InContainerTouched += (object sender, EventArgs e) => {

			} ;







//			idc.Derez += (object sender, DerezEventArgs e) => {
//				Console.WriteLine("Raising Derez");
//				var handler = this.Derez;
//				if (handler != null) {
//					handler(this, e);
//				}
//			};
//
//			this.ItemSaved += (object sender, ItemSavedEventArgs e) => {
//				var handler = this.ItemSaved;
//				if (handler != null) {
//					handler(this, e);
//				}
//			};

//			this.GotPicture += (object sender, GotPictureEventArgs e) => {
//				var handler = this.GotPicture;
//				if (handler != null && ) {
//					handler(this, new GotPictureEventArgs());
//				}
//			};
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
				AppDelegate.bl.AddPictureAttachment(mailContr, this.item);
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