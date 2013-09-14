using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
//using TipOfTheDay;
using System.Drawing;
using GoogleAdMobAds;
using MonoTouch.ObjCRuntime;
using No.Dctapps.Garageindex.Ios;
using no.dctapps.Garageindex.tables;

namespace no.dctapps.Garageindex
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
//		bool pro = true;
		// class-level declarations
		UIWindow window;
		UITabBarController tabs;
//		UINavigationController navController;
//		TabController tabs;
//		HomeScreen home;

		const string AdmobID_iphone = "a151431a930a1e9";
		const string AdmobID_ipad = "a151431e078e783";
		
//		GADBannerView adView;
//		bool viewOnScreen = false;
//		
//		PointF iphone;
//		PointF ipad;
		
		void Setup ()
		{
			float y = UIScreen.MainScreen.Bounds.Bottom - 100;
			float y2 = UIScreen.MainScreen.Bounds.Bottom - 160;
			Console.WriteLine ("adsY:" + y);
//			iphone = new PointF (0, y);
//			ipad = new PointF (50, y2);

			Console.WriteLine("Y2="+y2);
		}

		public static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

//		static void Workaround ()
//		{
//			Xamarin.Themes.BlackLeatherTheme.Apply
//				(UINavigationBar.Appearance);
//			Xamarin.Themes.BlackLeatherTheme.Apply (UITabBar.Appearance);
//			Xamarin.Themes.BlackLeatherTheme.Apply (UIToolbar.Appearance);
//			Xamarin.Themes.BlackLeatherTheme.Apply
//				(UIBarButtonItem.Appearance);
//			Xamarin.Themes.BlackLeatherTheme.Apply (UISlider.Appearance);
//			Xamarin.Themes.BlackLeatherTheme.Apply
//				(UISegmentedControl.Appearance);
//			Xamarin.Themes.BlackLeatherTheme.Apply (UIProgressView.Appearance);
//			Xamarin.Themes.BlackLeatherTheme.Apply (UISearchBar.Appearance);
//			Xamarin.Themes.BlackLeatherTheme.Apply (UISwitch.Appearance);
//			Xamarin.Themes.BlackLeatherTheme.Apply
//				(UIRefreshControl.Appearance);
//			//Xamarin.Themes.BlackLeatherTheme.Apply (UITableView.Appearance);
//			UITableView.Appearance.BackgroundColor = UIColor.Black;
//		}

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
//			if (!InSimulator ())
//			Workaround ();
			// create a new window instance based on the screen size
			this.window = new UIWindow (UIScreen.MainScreen.Bounds); 
			
			//---- instantiate a new navigation controller 


//			if(!pro) {
//				tabs = new TabController ();
//				Setup();
//				if(UserInterfaceIdiomIsPhone){
//					loadIphoneAds();
//				}else{
//					loadIPadAds();
////					TipOfTheDayControl<GarageDBTipsProvider>.Show (window);
//				}
//			}else{
//				TipOfTheDayControl<GarageDBTipsProvider>.Show (window);
			tabs = new TabControllerPro ();
//			}



//			var rootNavigationController = new UINavigationController(); 
			
			//---- instantiate a new home screen 
//			HomeScreen homeScreen = new HomeScreen(); 
//			homeScreen.Title = "GarageKeeper";
			
			//---- add the home screen to the navigation controller 
			// (it'll be the top most screen) 
//			rootNavigationController.PushViewController(tabs, false); 
			
			//---- set the root view controller on the window. the nav 
			// controller will handle the rest 
			this.window.RootViewController = tabs; 

			this.window.MakeKeyAndVisible (); 

          


//            window.tintColor = [UIColor purpleColor];
//            this.window.
//            this.GetViewController.
//            this.view = UIColor.Purple;
            this.window.TintColor = UIColor.Purple;


//			KuroStyle.apply();


			return true; 
		}

//		static bool InSimulator ()
//		{
//			return Runtime.Arch == Arch.SIMULATOR;
//		}

	

//		void loadIphoneAds ()
//		{
//			adView = new GADBannerView (size: GADAdSizeCons.Banner, origin: iphone) {
//				AdUnitID = AdmobID_iphone,
//				RootViewController = tabs
//			};
//		
//			adView.DidReceiveAd += (sender, args) => {
//				if (!viewOnScreen) this.window.AddSubview (adView);
//				viewOnScreen = true;
//			};
//				
//			adView.LoadRequest (new GADRequest ());
//		}
//	
//		void loadIPadAds ()
//		{
//			adView = new GADBannerView (size: GADAdSizeCons.Leaderboard, origin: ipad) {
//				AdUnitID = AdmobID_ipad,
//				RootViewController = tabs
//			};
//		
//			adView.DidReceiveAd += (sender, args) => {
//				if (!viewOnScreen) this.window.AddSubview (adView);
//				viewOnScreen = true;
//			};
//		
//			adView.LoadRequest (new GADRequest ());
//		}
	}


}

