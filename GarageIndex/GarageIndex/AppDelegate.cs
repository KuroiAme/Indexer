using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.dao;
using no.dctapps.Garageindex.businesslogic;

namespace GarageIndex
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		UITabBarController tabs;
		public static LagerDAO dao;
		public static GarageindexBL bl;

		//
		// This method is invoked when the application has loaded and is ready to run. In this
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			//Initialize Global Frameworks...instead of having Dependency Injection
			dao = new LagerDAO ();
			bl = new GarageindexBL ();


			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			tabs = new TabController ();

			this.window.TintColor = UIColor.Purple;
			
			// If you have defined a root view controller, set it here:
			this.window.RootViewController = tabs; 
			
			// make the window visible
			window.MakeKeyAndVisible ();



			return true;
		}
	}
}

