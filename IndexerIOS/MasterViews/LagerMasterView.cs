using System;
using No.Dctapps.Garageindex.Ios.Screens;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;
using no.dctapps.commons.events.screens;
using no.dctapps.commons.events.events;
using no.dctapps.commons.events;

namespace No.DCTapps.GarageIndex
{
	public class LagerMasterView : UISplitViewController
	{
		public LagerMasterView () : base()
		{
			LagerList primaryview;
			TheStorageScreen secondaryview;
			
			UINavigationController primarynav;
			UINavigationController secondarynav;
		
			primaryview = new LagerList();
			secondaryview = new TheStorageScreen();
			
			primaryview.LagerClicked += (object sender, LagerClickedEventArgs e) => secondaryview.ShowDetails (e.Lager);
			
			secondaryview.LagerSaved += delegate(object sender, LagerClickedEventArgs e) {
				primaryview.Refresh();
			};
			
			primarynav = new UINavigationController();
			primarynav.PushViewController(primaryview, false);
			
			secondarynav = new UINavigationController();
			secondarynav.PushViewController(secondaryview, false);
			
			ViewControllers = new UIViewController[] {primarynav, secondarynav};
		}

		protected override void Dispose (bool disposing)
		{
			ViewControllers = null;
			base.Dispose (disposing);
		}
			
		/// <summary>
		/// Release everything not in use
		/// </summary>
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


