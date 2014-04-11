using System;
using MonoTouch.UIKit;
using GoogleAnalytics.iOS;
using SatelliteMenu;
using System.Drawing;
using no.dctapps.commons.events.model;
using no.dctapps.commons;
using no.dctapps.commons.events.screens;
using GoogleAdMobAds;

namespace no.dctapps.commons.events
{
	public class DashBoardViewController : UIViewController
	{
		const float statbarHeight = 25;
		const float buffer = 10;

		public DashBoardViewController ()
		{
		}

		RectangleF rightPanelRect;

		DashboardRightPanel rightPanel;

		public UISearchBar Search;

		GADBannerView adView;
		bool viewOnScreen = false;

		protected override void Dispose (bool disposing)
		{
			rightPanel.Dispose ();
			Search.Dispose ();
			base.Dispose (disposing);
		}

		void cleanup ()
		{
			Dispose ();
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			if(this.IsViewLoaded && this.View.Window == null){
				//cleanup ();

			}
			// Release any cached data, images, etc that aren't in use.
		}

		public override void LoadView ()
		{
			base.LoadView ();
			this.View.Frame = new RectangleF (0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
		}

		void AddSearchBar ()
		{
			Search = new UISearchBar (new RectangleF (0, 65, UIScreen.MainScreen.Bounds.Width, 40));
			Search.SearchButtonClicked += (object sender, EventArgs e) =>  {
				Search.ResignFirstResponder ();
				SearchScreen ss = new SearchScreen (Search.Text);
				this.NavigationController.PushViewController (ss, true);
			};
		}

		StatisticsPanel statpanel;
		UIScrollView statpanelScroll;
		public OverSightMap MainMap;

		UIScrollView rightPanelScroll;

		IndexerSateliteMenu menu;

		public static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = AppDelegate.its.getTranslatedText ("Indexer Dashboard");
			this.NavigationController.NavigationBar.Translucent = true;



			Background back = new Background ();
			Add (back.View);
			View.SendSubviewToBack (back.View);

			float mapHeight = 200;
			const float navbarHeight = 100;
			const float panelContentHeight = 1000;

			if (!UserInterfaceIdiomIsPhone) {
				mapHeight = 450;
			}

			MainMap = new OverSightMap (new RectangleF (10, navbarHeight, UIScreen.MainScreen.Bounds.Width - buffer * 2, mapHeight), this);
			View.AddSubview (MainMap.View);

			if (AppDelegate.Variant == "LITE") {
				adView = new GADBannerView (size: GADAdSizeCons.Banner, origin: new PointF (0, navbarHeight + mapHeight)) {
					AdUnitID = AppDelegate.AdmobID,
					RootViewController = this
				};

				adView.DidReceiveAd += (sender, args) => {
					if (!viewOnScreen) View.AddSubview (adView);
					viewOnScreen = true;
				};

				adView.LoadRequest (GADRequest.Request);
			}

			float statpanelwidth = UIScreen.MainScreen.Bounds.Width / 3;
			float rightPanelWidth = UIScreen.MainScreen.Bounds.Width - statpanelwidth - 3*buffer;
			const float headerheight = 100;
			float panelsHeight = UIScreen.MainScreen.Bounds.Height - mapHeight - buffer;
			const float panelY = headerheight + buffer;
			statpanel = new StatisticsPanel (new SizeF(statpanelwidth,panelContentHeight));
			statpanelScroll = new UIScrollView (new RectangleF(buffer, mapHeight + buffer + navbarHeight, statpanelwidth, panelsHeight));
			statpanelScroll.AddSubview (statpanel.View);
//			foreach (UIGestureRecognizer uig in statpanel.View.GestureRecognizers) {
//				uig.Delegate = new SwipeDelegate ();
//			}
			statpanelScroll.ContentSize = new SizeF (statpanelwidth, panelContentHeight);
			statpanelScroll.UserInteractionEnabled = true;
			View.AddSubview (statpanelScroll);

			rightPanelRect = new RectangleF (statpanelwidth + buffer, mapHeight + buffer + navbarHeight, rightPanelWidth, panelsHeight);
			rightPanel = new DashboardRightPanel (rightPanelWidth, this);

//			DashBoardHeader header = new DashBoardHeader (new RectangleF(0, 20 ,UIScreen.MainScreen.Bounds.Width, 22));
//			View.AddSubview (header.View);

			AddSearchBar ();
//			search.AutosizesSubviews = false;
//			search.SizeToFit ();

			View.AddSubview (Search);

			rightPanelScroll = new UIScrollView (rightPanelRect);
			rightPanelScroll.ContentSize = rightPanel.getSize ();
			rightPanelScroll.AddSubview (rightPanel.View);
			rightPanelScroll.ShowsVerticalScrollIndicator = false;
			rightPanelScroll.UserInteractionEnabled = true;
			View.AddSubview (rightPanelScroll);

			menu = new IndexerSateliteMenu ("Dashboard", this);
			menu.View.UserInteractionEnabled = true;
//			foreach (UIGestureRecognizer uig in menu.View.GestureRecognizers) {
//				uig.Delegate = new SwipeDelegate ();
//			}
			View.AddSubview (menu.View);

			AddHelpButton ();
			AddSettingsButton ();

			curtainsIsDown = true;
			PullDownCurtain ();
			if (UserInterfaceIdiomIsPhone) {
				menu.SateliteButton.TouchUpInside += (object sender, EventArgs e) => ToggleCurtains ();
			}
		}
//		public static bool UserInterfaceIdiomIsPhone {
//			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
//		}

		Boolean curtainsIsDown = true;

		void ToggleCurtains ()
		{
			//Console.WriteLine ("curtains toggled:"+curtainsIsDown);
			if (curtainsIsDown) {
				curtainsIsDown = false;
				PullUpCurtains ();
			} else {
				curtainsIsDown = true;
				PullDownCurtain ();
			}

		}

		void PullUpCurtains ()
		{
			rightPanelScroll.ScrollRectToVisible (rightPanel.getBottom (),true);
			statpanelScroll.ScrollRectToVisible (statpanel.getBottom (),true);
		}

		void PullDownCurtain ()
		{
			rightPanelScroll.ScrollRectToVisible (rightPanel.getTop (),true);
			statpanelScroll.ScrollRectToVisible (statpanel.getTop (), true);
		}

		void AddHelpButton ()
		{
			UIBarButtonItem help = new UIBarButtonItem (AppDelegate.its.getTranslatedText ("Help"), UIBarButtonItemStyle.Plain, null);
			help.Clicked += (object sender, EventArgs e) => {
				HelpScreen hs = new HelpScreen();
				this.NavigationController.PushViewController(hs,true);
			};
			this.NavigationItem.SetRightBarButtonItem (help, true);
		}

		void AddSettingsButton ()
		{
			UIBarButtonItem prefbutton = new UIBarButtonItem (PreferencesIcon.MakeImage (), UIBarButtonItemStyle.Plain, null);
			prefbutton.Clicked += (object sender, EventArgs e) => {
				Preferences pref = new Preferences();
				this.NavigationController.PushViewController(pref,true);
			};
			this.NavigationItem.SetLeftBarButtonItem (prefbutton, true);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.View.BackgroundColor = UIColor.Clear;
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			if (Search == null) {
				AddSearchBar ();
			}
			if (menu != null && menu.SateliteButton != null) {
				if (!menu.SateliteButton.Enabled) {
					curtainsIsDown = true;
					PullDownCurtain ();
				}
			}

			if (statpanel != null) {
				statpanel.UpdateStatistics ();
			}

//			if (MainMap == null) {
//				MainMap = new OverSightMap (new RectangleF (0, currentheight, rightPanelWidth, elementHeight), ancestor);
//
//			}

			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Dashboard");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
			if (MainMap != null) {
				MainMap.ReloadData ();
			}

		
		}
	}
}

