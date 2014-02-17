using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.businesslogic;
using TipOfTheDay;
using no.dctapps.Garageindex.dao;
using GoogleAnalytics.iOS;
using GoogleAdMobAds;

namespace GarageIndex
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		public static Boolean IsLite = true;
		public static int MaxAntall = 10;
		public static string AdmobID;
		public IGAITracker Tracker;
		public static readonly string TrackingId = "UA-47719330-1";

		static AppDelegate()
		{
			CurrentSystemVersion = new Version (UIDevice.CurrentDevice.SystemVersion);
			iOS7 = new Version (7, 0);

			if (UserInterfaceIdiomIsPhone) {
				AdmobID = "a151431a930a1e9";
			} else {
				AdmobID = "a151431e078e783";
			}
		}

		public static readonly Version CurrentSystemVersion;
		public static readonly Version iOS7;

		// class-level declarations
		UIWindow window;
		UITabBarController tabs;
		public static LagerDAO dao;
		public static GarageindexBL bl;
		//public static CouchDB db;

		//
		// This method is invoked when the application has loaded and is ready to run. In this
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			GAI.SharedInstance.DispatchInterval = 20;
			GAI.SharedInstance.TrackUncaughtExceptions = true;
			Tracker = GAI.SharedInstance.GetTracker (TrackingId);


			//Initialize Global Frameworks...instead of having Dependency Injection
			dao = new LagerDAO ();
			bl = new GarageindexBL ();
			//db = new CouchDB ();

//			if (!bl.StatsEnabled && !bl.firstT) {
//				GAI.SharedInstance.Logger.LogLevel = GAILogLevel.None;
//			}

			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			tabs = new TabController ();

//			if(MonoTouch.Foundation.isi
			this.window.TintColor = UIColor.Purple;
			
			// If you have defined a root view controller, set it here:
			this.window.RootViewController = tabs; 
			
			// make the window visible
			window.MakeKeyAndVisible ();
			if (UserInterfaceIdiomIsPhone) {
				if (CurrentSystemVersion >= iOS7) {
					TipOfTheDayControl<GarageIndexTipsProvider, DefaultTipsSettings>.Show (window);
				}
			}



			return true;
		}
		
		public static bool UserInterfaceIdiomIsPhone 
		{
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}
	}
}

