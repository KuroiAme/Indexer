using System;
using No.Dctapps.Garageindex.Ios.Screens;
using MonoTouch.UIKit;
using Xamarin.Themes;
using MonoTouch.ObjCRuntime;
using no.dctapps.Garageindex.screens;
using no.dctapps.Garageindex.events;

namespace No.DCTapps.GarageIndex
{
	public class LagerMasterView : UISplitViewController
	{
		public LagerMasterView () : base()
		{
			StorageCatalogue primaryview;
			TheStorageScreen secondaryview;
			
			UINavigationController primarynav;
			UINavigationController secondarynav;
		
			primaryview = new StorageCatalogue();
			secondaryview = new TheStorageScreen();
			
			primaryview.LagerClicked += (object sender, LagerClickedEventArgs e) => {
				secondaryview.ShowDetails(e.Lager);
			};
			
			secondaryview.LagerSaved += delegate(object sender, LagerClickedEventArgs e) {
				primaryview.Refresh();
			};
			
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

				
				// Release any cached data, images, etc that aren't in use.
			}
			
			public override void ViewWillAppear (bool animated)
			{
				base.ViewWillAppear (animated);
				

			}
			
			public override void ViewDidLoad ()
			{
				base.ViewDidLoad ();
				
//				if(!InSimulator())
//					BlackLeatherTheme.Apply (this.View);
				
				// Perform any additional setup after loading the view, typically from a nib.
			}
			
			public static bool InSimulator ()
			{
				return Runtime.Arch == Arch.SIMULATOR;
			}
	 	}
}


