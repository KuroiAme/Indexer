using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;
using IndexerIOS;

namespace GarageIndex
{
	public class UtilityViewController : UIViewController
	{

		protected override void Dispose (bool disposing)
		{
			backOne.Dispose ();
			backAll.Dispose ();
			base.Dispose (disposing);
		}

		public static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		UIBarButtonItem backOne;

		UIBarButtonItem backAll;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

//			Background back = new Background ();
//			View.Add (back.View);

		

			backOne = new UIBarButtonItem (backarrow.MakeBackArrow (), UIBarButtonItemStyle.Plain, null);


			backAll = new UIBarButtonItem (Xmark.MakeImage (), UIBarButtonItemStyle.Plain, null);

			backOne.Clicked += (object sender, EventArgs e) => this.NavigationController.PopViewControllerAnimated (true);
			backAll.Clicked += (object sender, EventArgs e) => this.NavigationController.PopToRootViewController (true);


			UIBarButtonItem[] leftbuttons = { backOne, backAll };

			this.NavigationItem.SetLeftBarButtonItems (leftbuttons, true);

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
			

	}
}

