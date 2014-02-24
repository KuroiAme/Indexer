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
		public DashBoardViewController ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Background back = new Background ();
			Add (back.View);


			IndexerSateliteMenu menu = new IndexerSateliteMenu ("Dashboard");
			Add (menu.View);

			StatisticsPanel statpanel = new StatisticsPanel (new RectangleF (12.5f, 147.5f, 86, 643));
			Add (statpanel.View);

			AddHeadingLabels ();

			OverSightMap mainMap = new OverSightMap (new RectangleF (116.5f, 189.5f, 397, 288));
			Add (mainMap.View);

			UILabel bestPlace = new UILabel (new RectangleF (116, 574, 392, 28));


			UISearchBar search = new UISearchBar (new RectangleF (115.5f, 500.5f, 397, 56));
			search.SearchButtonClicked += (object sender, EventArgs e) => {
				var find = AppDelegate.bl.GetBestLocationForSearchTerm(search.Text);
			};
			Add (search);

		}

		UILabel nameBox;

		void AddHeadingLabels ()
		{
			var headingRect = new RectangleF(1, 23, 639, 69);
			UILabel heading = new UILabel (headingRect);
			heading.AdjustsFontSizeToFitWidth = true;
			heading.Text = "Indexer Dashboard";
			heading.TextAlignment = UITextAlignment.Center;
			heading.TextColor = UIColor.White;

			var nameBoxRect = new RectangleF(117, 105, 370, 43);
			nameBox = new UILabel (nameBoxRect);
			nameBox.AdjustsFontSizeToFitWidth = true;
			nameBox.Text = AppDelegate.bl.GetUserName ();
			nameBox.TextAlignment = UITextAlignment.Center;
			nameBox.TextColor = UIColor.White;

			var doubletap = new UITapGestureRecognizer (ChangeName);
			doubletap.NumberOfTapsRequired = 2;
			nameBox.AddGestureRecognizer (doubletap);


		}

		void ChangeName (UITapGestureRecognizer gestureRecognizer){
			Console.WriteLine ("Name doubletapped");
			var cr8 = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Done", "Done");
			var input = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Enter your name", "Enter your name");
			var abort = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Cancel", "Cancel");
			UIAlertView av = new UIAlertView (input, "\n", null, abort, new string[] {
				cr8
			});
			av.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
			int Create = av.FirstOtherButtonIndex;
			av.Clicked += (object sender, UIButtonEventArgs e) =>  {
				if (e.ButtonIndex == Create) {
					String nameText = av.GetTextField (0).Text;
					AppDelegate.bl.SaveUserName(nameText);
					nameBox.Text = nameText;
				}
			};
			av.Show ();
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Dashboard");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}
	}
}

