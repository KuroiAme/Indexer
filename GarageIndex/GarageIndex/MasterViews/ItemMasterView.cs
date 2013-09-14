using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
using No.Dctapps.Garageindex.Ios.Screens;
using no.dctapps.Garageindex.events;
using no.dctapps.garageindex;
using no.dctapps.Garageindex.screens;
using No.Dctapps.GarageIndex;

namespace no.dctapps.Garageindex.screens
{
	public partial class ItemMasterView : UISplitViewController
	{
		ItemCatalogue primaryview;
		ItemDetailScreen secondaryview;

		UINavigationController primarynav;
		UINavigationController secondarynav;

		public ItemMasterView () : base ()
//		public ItemMasterView () : base ("ContainerMasterView", null)
		{
			primaryview = new ItemCatalogue();
			secondaryview = new ItemDetailScreen();

			primaryview.ActivateDetail += (object sender, ItemClickedEventArgs e) => {
				secondaryview.ShowDetails(e.Item);
			};

			secondaryview.ItemSaved += delegate(object sender, ItemSavedEventArgs e) {
				primaryview.Refresh();
			};

            secondaryview.Derez += (object sender, DerezEventArgs e) => secondarynav.PopToRootViewController(true);

			primarynav = new UINavigationController();
			primarynav.PushViewController(primaryview, false);
			
			secondarynav = new UINavigationController();
			secondarynav.PushViewController(secondaryview, false);

			ViewControllers = new UIViewController[] {primarynav, secondarynav};
		}


		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

//			primaryview = null;
//			secondaryview= null;
//
//			primarynav= null;
//			secondarynav= null;
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

//			if(primaryview == null){
//				primaryview = new ItemCatalogue();
//			}
//
//			if(secondaryview == null){
//				secondaryview = new ItemDetailScreen();
//			}
//
//			if(primarynav == null){
//				primarynav = new UINavigationController();
//				primarynav.PushViewController(primaryview, false);
//			}
//
//			if(secondarynav == null){
//				secondarynav = new UINavigationController();
//				secondarynav.PushViewController(secondaryview, false);
//			}
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

//			if(!InSimulator())
//				BlackLeatherTheme.Apply (this.View);
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public static bool InSimulator ()
		{
			return Runtime.Arch == Arch.SIMULATOR;
		}
	}
}

