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

		public override void LoadView ()
		{
			base.LoadView ();

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.View.Frame = new RectangleF (buffer, statbarHeight, UIScreen.MainScreen.Bounds.Width - buffer * 2, UIScreen.MainScreen.Bounds.Height - statbarHeight);
			this.View.BackgroundColor = UIColor.Clear;


			Background back = new Background ();
			Add (back.View);
			View.SendSubviewToBack (back.View);

			float statpanelwidth = (View.Bounds.Width / 3);
			float rightPanelWidth = (View.Bounds.Width / 3 * 2);
			const float headerheight = 74;
			float panelsHeight = View.Bounds.Height - 125;
			StatisticsPanel statpanel = new StatisticsPanel (new RectangleF(0, headerheight + buffer, statpanelwidth , panelsHeight));
			Add (statpanel.View);

			rightPanelRect = new RectangleF (statpanelwidth + buffer, headerheight + buffer, View.Bounds.Width - statpanelwidth - buffer, panelsHeight);
			rightPanel = new DashboardRIghtPanel (rightPanelWidth);

			DashBoardHeader header = new DashBoardHeader (new RectangleF(0, 0,View.Bounds.Width, 44));
			View.AddSubview (header.View);


			IndexerSateliteMenu menu = new IndexerSateliteMenu ("Dashboard");
			View.AddSubview (menu.View);

			UIScrollView rightPanelScroll = new UIScrollView (rightPanelRect);
			rightPanelScroll.ContentSize = rightPanel.getSize ();
			rightPanelScroll.AddSubview (rightPanel.View);
			View.AddSubview (rightPanelScroll);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Dashboard");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}
	}
}

