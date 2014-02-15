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
			idc = new ItemDetailsController ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			ShowDetails (item);
		}

		public void ShowDetails(Item item){
			idc = new ItemDetailsController (item);
			innerview = new UIScrollView (UIScreen.MainScreen.Bounds);
			innerview.ContentSize = idc.GetContentsize ();
			innerview.AddSubview (idc.View);
			innerview.BackgroundColor = UIColor.White;
			idc.ShowDetails (item);
			this.View = innerview;

			idc.Derez += (object sender, DerezEventArgs e) => {
				Console.WriteLine("Raising Derez");
				var handler = this.Derez;
				if (handler != null) {
					handler(this, e);
				}
			};

			this.ItemSaved += (object sender, ItemSavedEventArgs e) => {
				var handler = this.ItemSaved;
				if (handler != null) {
					handler(this, e);
				}
			};

//			this.GotPicture += (object sender, GotPictureEventArgs e) => {
//				var handler = this.GotPicture;
//				if (handler != null && ) {
//					handler(this, new GotPictureEventArgs());
//				}
//			};
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "items detail Screen");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}

	}
}