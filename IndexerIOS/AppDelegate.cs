using GoogleAnalytics.iOS;
using MTiRate;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Tasky.DL.SQLite;
using no.dctapps.commons.events.dao;
using no.dctapps.commons.events.businesslogic;
using System;
using System.IO;

namespace no.dctapps.commons.events
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
			get{return "LITE";}
		}

		public static string AdmobID = "<Get your ID at google.com/ads/admob>";

		static AppDelegate()
		{
			CurrentSystemVersion = new Version (UIDevice.CurrentDevice.SystemVersion);
			iOS7 = new Version (7, 0);

			iRate.SharedInstance.DaysUntilPrompt = 5;
			iRate.SharedInstance.UsesUntilPrompt = 3;

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
		//UITabBarController tabs;
		public static LagerDAO dao;
		public static IndexerBuisnessService bl;
		public static KeyStorageServiceIos key;
		public static ITranslationService its;
		UINavigationController navController;

		public static DashBoardViewController Dashboard;

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

			dao = new LagerDAO (conn,Variant);

			dao.LimitExceeded += (object sender, EventArgs e) => PleaseBuyFullVersion ();


			bl = new IndexerBuisnessService (dao, new TranslationServiceIos());
			its = new TranslationServiceIos ();

			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			Dashboard = new DashBoardViewController ();
			navController = new UINavigationController (Dashboard);
			//navController.NavigationBar.BackgroundColor = UIColor.Clear;


//			if(MonoTouch.Foundation.isi
//			window.TintColor = UIColor.Purple;

			window.RootViewController = navController;
			// If you have defined a root view controller, set it here:
			//this.window.RootViewController = dashboard; 
			
			// make the window visible
			window.MakeKeyAndVisible ();

			return true;
		}

		UIActionSheet pleaseBuy;

		void PleaseBuyFullVersion ()
		{
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateEvent ("Limit Exceeded", "promting user to buy!","UsesCount", iRate.SharedInstance.UsesCount).Build ());
			pleaseBuy = new UIActionSheet ("You have exceeded the number of things you can store in the lite version, please buy the full version");
			pleaseBuy.AddButton ("buy the full version");
			pleaseBuy.AddButton ("cancel");
			pleaseBuy.Clicked += (object sender, UIButtonEventArgs e) => {
				if(e.ButtonIndex == 1){
					GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateEvent ("Limit Exceeded", "User didnt want to buy","UsesCount", iRate.SharedInstance.UsesCount).Build ());
				}
				if(e.ButtonIndex == 0){
					GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateEvent ("Limit Exceeded", "User bought the app at exceeded limit","UsesCount", iRate.SharedInstance.UsesCount).Build ());
					UIApplication.SharedApplication.OpenUrl (new NSUrl("https://itunes.apple.com/app/id647311169"));
				}
			};
			pleaseBuy.ShowInView (UIApplication.SharedApplication.KeyWindow);
		}
		
		public static bool UserInterfaceIdiomIsPhone 
		{
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations (UIApplication application, UIWindow forWindow)
		{
			return UIInterfaceOrientationMask.Portrait;
		}
			
//		public override UIInterfaceOrientation PreferredInterfaceOrientationForPresentation ()
//		{
//			return UIInterfaceOrientation.Portrait;
//		}
//
//		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
//		{
//			return UIInterfaceOrientationMask.Portrait;
//		}
	}
}

