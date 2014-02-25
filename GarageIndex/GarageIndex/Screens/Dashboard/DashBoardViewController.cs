using System;
using MonoTouch.UIKit;
using GoogleAnalytics.iOS;
using SatelliteMenu;
using System.Drawing;
using no.dctapps.Garageindex.model;

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

		DashboardRIghtPanel rightPanel;

		public UISearchBar search;

		public override void LoadView ()
		{
			base.LoadView ();
			this.View.Frame = new RectangleF (0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			Background back = new Background ();
			Add (back.View);
			View.SendSubviewToBack (back.View);

			float statpanelwidth = UIScreen.MainScreen.Bounds.Width / 3;
			float rightPanelWidth = UIScreen.MainScreen.Bounds.Width - statpanelwidth - 3*buffer;
			const float headerheight = 74;
			float panelsHeight = UIScreen.MainScreen.Bounds.Height - 125;
			const float panelY = headerheight + buffer;
			StatisticsPanel statpanel = new StatisticsPanel (new RectangleF(buffer, panelY, statpanelwidth , panelsHeight));
			Add (statpanel.View);

			rightPanelRect = new RectangleF (statpanelwidth + buffer, panelY, rightPanelWidth, panelsHeight);
			rightPanel = new DashboardRIghtPanel (rightPanelWidth);

			DashBoardHeader header = new DashBoardHeader (new RectangleF(0, 20 ,UIScreen.MainScreen.Bounds.Width, 22));
			View.AddSubview (header.View);

			search = new UISearchBar (new RectangleF (0, 42, UIScreen.MainScreen.Bounds.Width, 40));
			search.SearchButtonClicked += (object sender, EventArgs e) => {
				var find = AppDelegate.bl.GetBestLocationForSearchTerm(search.Text);
				search.ResignFirstResponder();
			};
//			search.AutosizesSubviews = false;
//			search.SizeToFit ();

			View.AddSubview (search);

			UIScrollView rightPanelScroll = new UIScrollView (rightPanelRect);
			rightPanelScroll.ContentSize = rightPanel.getSize ();
			rightPanelScroll.AddSubview (rightPanel.View);
			View.AddSubview (rightPanelScroll);

			IndexerSateliteMenu menu = new IndexerSateliteMenu ("Dashboard");
			View.AddSubview (menu.View);

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.View.BackgroundColor = UIColor.Clear;



		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Dashboard");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}
	}
}

