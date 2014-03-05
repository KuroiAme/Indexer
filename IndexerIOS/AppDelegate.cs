using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.businesslogic;
using no.dctapps.Garageindex.dao;
using GoogleAnalytics.iOS;
using MTiRate;
using System.IO;
using Tasky.DL.SQLiteBase;
using Tasky.DL.SQLite;

namespace GarageIndex
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		public IGAITracker Tracker;
		public static readonly string TrackingId = "UA-47719330-1";

		public static string Variant {
			get{return "PRO";}
		}

		static AppDelegate()
		{
			CurrentSystemVersion = new Version (UIDevice.CurrentDevice.SystemVersion);
			iOS7 = new Version (7, 0);

			iRate.SharedInstance.DaysUntilPrompt = 5;
			iRate.SharedInstance.UsesUntilPrompt = 3;

		}



		public static readonly Version CurrentSystemVersion;
		public static readonly Version iOS7;

		// class-level declarations
		UIWindow window;
		//UITabBarController tabs;
		public static LagerDAO dao;
		public static IndexerBuisnessService bl;
		public static KeyStorageServiceIos key;
		public static ITranslationService its;
		UINavigationController navController;
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

			iRate.SharedInstance.UserDidAttemptToRateApp += (sender, e) => {
				GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateEvent ("UserRating", "User is rating app now!","UsesCount", iRate.SharedInstance.UsesCount).Build ());
				Console.WriteLine ("User is rating app now!");
			};

			iRate.SharedInstance.UserDidDeclineToRateApp += (sender, e) => { 
				GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateEvent ("UserRating", "User does not want to rate app","UsesCount", iRate.SharedInstance.UsesCount).Build ());
				Console.WriteLine ("User does not want to rate app");
			};

			iRate.SharedInstance.UserDidRequestReminderToRateApp += (sender, e) => {
				GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateEvent ("UserRating", "User will rate app later","UsesCount", iRate.SharedInstance.UsesCount).Build ());
				Console.WriteLine ("User will rate app later");
			};

			key = new KeyStorageServiceIos ();


			var documents = Environment.GetFolderPath (Environment.SpecialFolder.LocalApplicationData);
			var pathToDatabase = Path.Combine(documents, "db_sqlite-net.db");

			Connection conn = new Connection (pathToDatabase);

			dao = new LagerDAO (conn);
			bl = new IndexerBuisnessService (dao, new TranslationServiceIos());
			its = new TranslationServiceIos ();

			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			DashBoardViewController dashboard = new DashBoardViewController ();
			navController = new UINavigationController (dashboard);
			//navController.NavigationBar.BackgroundColor = UIColor.Clear;


//			if(MonoTouch.Foundation.isi
			this.window.TintColor = UIColor.Purple;

			window.RootViewController = navController;
			// If you have defined a root view controller, set it here:
			//this.window.RootViewController = dashboard; 
			
			// make the window visible
			window.MakeKeyAndVisible ();

			return true;
		}
		
		public static bool UserInterfaceIdiomIsPhone 
		{
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}
	}
}

