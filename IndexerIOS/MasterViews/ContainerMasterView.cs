using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.events;
using no.dctapps.Garageindex.screens;

namespace no.dctapps.Garageindex
{
	public partial class ContainerMasterView : UISplitViewController
	{
			readonly ContainerScreen primaryview;
			readonly ContainerDetails secondaryview;
			
			readonly UINavigationController primarynav;
			readonly UINavigationController secondarynav;
	
			public ContainerMasterView () : base ()
	//		public ContainerMasterView () : base ("ContainerMasterView", null)
			{
				primaryview = new ContainerScreen();
				secondaryview = new ContainerDetails();
	
				primaryview.ActivateDetail += (object sender, ContainerClickedEventArgs e) => secondaryview.ShowDetails (e.container);
	
				secondaryview.LagerObjectSaved += delegate(object sender, LagerObjectSavedEventArgs e) {
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
			primarynav.Dispose ();
			secondarynav.Dispose ();
			secondaryview.Dispose ();
			primaryview.Dispose ();
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
				
	//			if(primaryview == null){
	//				primaryview = new ContainerScreen();
	//			}
	//			
	//			if(secondaryview == null){
	//				secondaryview = new ContainerDetails();
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

