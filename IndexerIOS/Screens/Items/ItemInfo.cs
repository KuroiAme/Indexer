using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using No.Dctapps.Garageindex.Ios.Screens;
using No.Dctapps.GarageIndex;
using GarageIndex;
using GoogleAnalytics.iOS;

namespace no.dctapps.Garageindex.screens
{
	public partial class ItemInfo : UIViewController
	{
		Item item;
		//		LagerDAO dao;
		//		public event EventHandler<ItemSavedEventArgs> DismissInfo;
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public ItemInfo (Item item)
			: base (UserInterfaceIdiomIsPhone ? "ItemInfo_iPhone" : "ItemInfo_iPad", null)
		{
			this.item = item;
//			dao = new LagerDAO();
		}

		protected override void Dispose (bool disposing)
		{
			item = null;
			base.Dispose (disposing);
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
			if (this.IsViewLoaded && this.View.Window == null) {
				//cleanup ();
			}
			// Release any cached data, images, etc that aren't in use.
		}
		//		ThreeChoiceButton button;
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Item info Screen");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			if (item == null) {
				item = new Item ();
			}

			Console.WriteLine ("Details:" + item.toString ());

		}
	}
}

