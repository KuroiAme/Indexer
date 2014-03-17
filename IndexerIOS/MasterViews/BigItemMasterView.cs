
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;
using No.Dctapps.Garageindex.Ios.Screens;
using no.dctapps.Garageindex.screens;
using no.dctapps.Garageindex.events;

namespace no.dctapps.garageindex
{
	public partial class BigItemMasterView : UISplitViewController
	{

		BigItemsScreen masterView;
		BigItemDetailScreen detailview;

		UINavigationController masternav;
		UINavigationController detailnav;

		public BigItemMasterView () : base ()
		{
			masterView = new BigItemsScreen();
			detailview = new BigItemDetailScreen();

			masterView.ActivateDetail += (object sender, BigItemDetailClickedEventArgs e) => detailview.ShowDetails (e.lagerobject);


			detailview.BigItemSaved += (object sender, BigItemSavedEventArgs e) => masterView.Refresh ();

//			detailview.Derezzy += (object sender, DerezLargeObjectEventArgs e) => {
//                detailnav.PopViewControllerAnimated(true);
//                masterView.Refresh();
//            };

			detailview.GotPicture += (object sender, GotPictureEventArgs e) => masterView.Refresh ();

			masternav = new UINavigationController();
			masternav.PushViewController(masterView, false);

			detailnav = new UINavigationController();
			detailnav.PushViewController(detailview, false);

			//always last
			ViewControllers = new UIViewController[] {masternav, detailnav};
		}
		
		protected override void Dispose (bool disposing)
		{
			masterView.Dispose ();
			detailview.Dispose ();
			masternav.Dispose ();
			detailnav.Dispose ();
			ViewControllers = null;
			base.Dispose (disposing);
		}
		void cleanup ()
		{
			//this.Dispose ();
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
//			if(!InSimulator())
//				BlackLeatherTheme.Apply (this.View);

//			AdsViewController ads = new AdsViewController();
//			Add(ads.View);
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public static bool InSimulator ()
		{
			return Runtime.Arch == Arch.SIMULATOR;
		}
	}
}

