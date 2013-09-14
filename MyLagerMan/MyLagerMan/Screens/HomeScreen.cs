
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using GoogleAdMobAds;
using no.dctapps.bundlemanifest;
using GarageDB.iOS.Screens;
using LagerMan.iOS;
using MyLagerMan;

namespace LagerMan
{
	public partial class HomeScreen : AdsViewController
	{
		TheStorageScreen mittlager;
		ViewBigItemsController _storeTing;
		BoxesScreen _esker;
		OverviewScreen oversikt;




		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public HomeScreen ()
			: base (UserInterfaceIdiomIsPhone ? "HomeScreen_iPhone" : "HomeScreen_iPad")
		{
		}


//		headingLabel = new UILabel () {
//			Font = UIFont.FromName("Cochin-BoldItalic", 22f),
//			TextColor = UIColor.FromRGB (127, 51, 0),
//			BackgroundColor = UIColor.Clear
//		};

//		UINavigationBar.Appearance.TintColor = UIColor.FromRGB (38, 117 ,255); // nice blue
//		UITextAttributes ta = new UITextAttributes();
//		ta.Font = UIFont.FromName ("AmericanTypewriter-Bold", 0f);
//		UINavigationBar.Appearance.SetTitleTextAttributes(ta);
//		ta.Font = UIFont.FromName ("AmericanTypewriter", 0f);
//		UIBarButtonItem.Appearance.SetTitleTextAttributes(ta, UIControlState.Normal);
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var boxes = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Boxes", "Boxes");
			var lobj = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Large Objects", "Large Objects");
			var info = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Storage Information", "Storage information");
			var oversikt = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Overview", "Overview");

			this.btnBoxes.SetTitle(boxes,UIControlState.Normal);
			this.btnBoxes.Font = UIFont.FromName ("Cochin-BoldItalic", 22f);

			this.btnOverview.SetTitle(oversikt,UIControlState.Normal);
			this.btnOverview.Font = UIFont.FromName ("Cochin-BoldItalic", 22f);

			this.btnBigThings.SetTitle(lobj, UIControlState.Normal);
			this.btnBigThings.Font = UIFont.FromName ("Cochin-BoldItalic", 22f);

			this.btnLagerInfo.SetTitle(info, UIControlState.Normal);
			this.btnLagerInfo.Font = UIFont.FromName ("Cochin-BoldItalic", 22f);

			this.btnLagerInfo.TouchUpInside += (sender, e) => {
				//---- instantiate a new hello world screen, if it's null
				// (it may not be null if they've navigated backwards
				if(this.mittlager == null)
				{ this.mittlager = new TheStorageScreen(); }
				//---- push our hello world screen onto the navigation
				//controller and pass a true so it navigates
				this.NavigationController.PushViewController(this.mittlager, true);
			};


			this.btnOverview.TouchUpInside += (sender, e) => {
				//---- instantiate a new hello world screen, if it's null
				// (it may not be null if they've navigated backwards
				if(this.oversikt == null)
				{ this.oversikt = new OverviewScreen(); }
				//---- push our hello world screen onto the navigation
				//controller and pass a true so it navigates
				this.NavigationController.PushViewController(this.oversikt, true);
			};

			this.btnBigThings.TouchUpInside += (sender, e) => {
				//---- instantiate a new hello world screen, if it's null
				// (it may not be null if they've navigated backwards
				if(this._storeTing == null)
				{ this._storeTing = new ViewBigItemsController(); }
				//---- push our hello world screen onto the navigation
				//controller and pass a true so it navigates
				this.NavigationController.PushViewController(_storeTing, true);
			};

			this.btnBoxes.TouchUpInside += (sender, e) => {
				//---- instantiate a new hello world screen, if it's null
				// (it may not be null if they've navigated backwards
				if(this._esker == null)
				{ this._esker = new BoxesScreen(); }
				//---- push our hello world screen onto the navigation
				//controller and pass a true so it navigates
				this.NavigationController.PushViewController(this._esker, true);
			};
			// Perform any additional setup after loading the view, typically from a nib.
		

			
			
		
		}


	}
}

