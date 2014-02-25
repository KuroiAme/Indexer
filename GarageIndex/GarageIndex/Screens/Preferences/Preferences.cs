using System;
using MonoTouch.Foundation;
using no.dctapps.Garageindex.businesslogic;
using GarageIndex;
using GoogleAnalytics.iOS;
using MonoTouch.UIKit;
using System.Drawing;
using GoogleAdMobAds;

namespace no.dctapps.Garageindex.screens
{
	public partial class Preferences : UIViewController
	{
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public Preferences ()
		//: base (UserInterfaceIdiomIsPhone ? "Preferences_iPhone" : "Preferences_iPad")
		{
//			bl = new GarageindexBL();
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Preferences Screen");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}

		UILabel textLargeObjects;

		UILabel textQR;

		UILabel textGAI;

		UISwitch switchLO;

		UISwitch switchQR;

		UISwitch switchGAI;

		public override void LoadView ()
		{
			base.LoadView ();
			this.View.BackgroundColor = UIColor.White;

			RectangleF LargeObjectsRect;
			RectangleF QRRect;
			RectangleF GARect;
			RectangleF rect_lo;
			RectangleF rect_qr;
			RectangleF rect_gai;
			RectangleF buyRect;
			SizeF rect_size = new SizeF (20, 20);
			PointF lop;
			PointF qrp;
			PointF gaip;

			if (UserInterfaceIdiomIsPhone) {
				LargeObjectsRect = new RectangleF (10, 100, 250, 20);
				QRRect = new RectangleF (10, 140, 250, 20);
				GARect = new RectangleF (10, 180, 250, 20);
				lop = new PointF (260, 100);
				qrp = new PointF (260, 140);
				gaip = new PointF (260, 180);

				rect_lo = new RectangleF (lop, rect_size);
				rect_qr = new RectangleF (qrp, rect_size);
				rect_gai = new RectangleF (gaip, rect_size);
			} else {
				//ipad
				LargeObjectsRect = new RectangleF (10, 100, 250, 20);
				QRRect = new RectangleF (10, 140, 250, 20);
				GARect = new RectangleF (10, 180, 250, 20);
				lop = new PointF (260, 100);
				qrp = new PointF (260, 140);
				gaip = new PointF (260, 180);
				rect_lo = new RectangleF (lop, rect_size);
				rect_qr = new RectangleF (qrp, rect_size);
				rect_gai = new RectangleF (gaip, rect_size);
			}

			textLargeObjects = new UILabel (LargeObjectsRect);
			Add (textLargeObjects);

			textQR = new UILabel (QRRect);
			Add (textQR);

			textGAI = new UILabel (GARect);
			Add (textGAI);

			buyRect = new RectangleF (10, 280, 250, 22);
			UIButton buy = new UIButton (UIButtonType.RoundedRect);
			buy.Frame = buyRect;
			buy.SetTitle (NSBundle.MainBundle.LocalizedString ("Buy full version", "Buy full version"), UIControlState.Normal);
			buy.TouchUpInside += (object sender, EventArgs e) => UIApplication.SharedApplication.OpenUrl (new NSUrl("https://itunes.apple.com/app/id647311169"));
			Add (buy);

			RectangleF pitchRect = new RectangleF (10, 310, 300, 22);
			UILabel pitch = new UILabel (pitchRect);
			pitch.Text = NSBundle.MainBundle.LocalizedString ("No ads, unlimited storage","No ads, unlimited storage");
			Add (pitch);




			Title = NSBundle.MainBundle.LocalizedString ("Preferences","Preferences");
			textLargeObjects.Text = NSBundle.MainBundle.LocalizedString ("List containers in the large object list","List containers in the large object list");
			textQR.Text = NSBundle.MainBundle.LocalizedString ("Include QRcode in emails where applicable", "Include QRcode in emails where applicable"); 
			textGAI.Text = NSBundle.MainBundle.LocalizedString ("allow anonymous statistics", "allow anonymous statistics");


			switchLO = new UISwitch (rect_lo);
			Add (switchLO);

			switchQR = new UISwitch (rect_qr);
			Add (switchQR);

			switchGAI = new UISwitch (rect_gai);
			Add (switchGAI);



        }
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.switchLO.On = AppDelegate.bl.GetContainersAsLarge();
			this.switchQR.On = AppDelegate.bl.IncludeQr();
			this.switchGAI.On = AppDelegate.bl.StatsEnabled ();

			this.switchLO.ValueChanged += (object sender, EventArgs e) => {
				Console.WriteLine("Value changed:"+switchLO.On.ToString());
				AppDelegate.bl.SaveContainersAsLarge(switchLO.On);
			};

			this.switchQR.ValueChanged += (object sender, EventArgs e) => {
				Console.WriteLine("Value changed:"+switchQR.On.ToString());
				AppDelegate.bl.SaveIncludeQR(switchQR.On);
            };

			this.switchGAI.ValueChanged += (object sender, EventArgs e) => {
				AppDelegate.bl.SaveStatsEnabled(switchGAI.On);
				if(switchGAI.On == true){
					GAI.SharedInstance.Logger.LogLevel = GAILogLevel.Verbose;
				}else{
					GAI.SharedInstance.Logger.LogLevel = GAILogLevel.None;
				}
			};
			InitializeAdds ();
		}

		GADBannerView adView;
		bool viewOnScreen = false;

		void InitializeAdds ()
		{
			PointF origo;
			GADAdSize type;
			if (UserInterfaceIdiomIsPhone) {
				origo = new PointF (0, UIScreen.MainScreen.Bounds.Height -200);
				type = GADAdSizeCons.Banner;
			} else {
				origo = new PointF (0, UIScreen.MainScreen.Bounds.Height - 200);
				type = GADAdSizeCons.FullBanner;
			}

			adView = new GADBannerView (size: type, origin: origo) {
				AdUnitID = AppDelegate.AdmobID,
				RootViewController = this
			};

			adView.DidReceiveAd += (sender, args) => {
				if (!viewOnScreen) View.AddSubview (adView);
				viewOnScreen = true;
			};

			adView.LoadRequest (GADRequest.Request);
		}

	}
}

