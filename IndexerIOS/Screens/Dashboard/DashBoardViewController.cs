﻿using System;
using MonoTouch.UIKit;
using GoogleAnalytics.iOS;
using SatelliteMenu;
using System.Drawing;
using no.dctapps.Garageindex.model;
using IndexerIOS;
using no.dctapps.Garageindex.screens;

namespace GarageIndex
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

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = AppDelegate.its.getTranslatedText ("Indexer Dashboard");
			this.NavigationController.NavigationBar.Translucent = true;



			Background back = new Background ();
			Add (back.View);
			View.SendSubviewToBack (back.View);

			float statpanelwidth = UIScreen.MainScreen.Bounds.Width / 3;
			float rightPanelWidth = UIScreen.MainScreen.Bounds.Width - statpanelwidth - 3*buffer;
			const float headerheight = 100;
			float panelsHeight = UIScreen.MainScreen.Bounds.Height - 125;
			const float panelY = headerheight + buffer;
			statpanel = new StatisticsPanel (new RectangleF (buffer, panelY, statpanelwidth, panelsHeight));
			Add (statpanel.View);

			rightPanelRect = new RectangleF (statpanelwidth + buffer, panelY, rightPanelWidth, panelsHeight);
			rightPanel = new DashboardRightPanel (rightPanelWidth, this);

//			DashBoardHeader header = new DashBoardHeader (new RectangleF(0, 20 ,UIScreen.MainScreen.Bounds.Width, 22));
//			View.AddSubview (header.View);

			AddSearchBar ();
//			search.AutosizesSubviews = false;
//			search.SizeToFit ();

			View.AddSubview (Search);

			UIScrollView rightPanelScroll = new UIScrollView (rightPanelRect);
			rightPanelScroll.ContentSize = rightPanel.getSize ();
			rightPanelScroll.AddSubview (rightPanel.View);
			rightPanelScroll.ShowsVerticalScrollIndicator = false;
			View.AddSubview (rightPanelScroll);

			IndexerSateliteMenu menu = new IndexerSateliteMenu ("Dashboard",this);
			View.AddSubview (menu.View);

			AddHelpButton ();
			AddSettingsButton ();

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

			if (statpanel != null) {
				statpanel.UpdateStatistics ();
			}

			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Dashboard");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
			if (rightPanel != null && rightPanel.MainMap != null) {
				rightPanel.MainMap.ReloadData ();
			}
		}
	}
}

