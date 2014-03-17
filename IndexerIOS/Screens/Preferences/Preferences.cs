using System;
using MonoTouch.Foundation;
using no.dctapps.Garageindex.businesslogic;
using GarageIndex;
using GoogleAnalytics.iOS;
using MonoTouch.UIKit;
using System.Drawing;
using MTiRate;

namespace no.dctapps.Garageindex.screens
{
	public partial class Preferences : UtilityViewController
	{
		UILabel textLargeObjects;

		UILabel textQR;

		UILabel textGAI;

		UISwitch switchLO;

		UISwitch switchQR;

		UISwitch switchGAI;

//		static bool UserInterfaceIdiomIsPhone {
//			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
//		}

		public Preferences ()
		{

		}

		protected override void Dispose (bool disposing)
		{
			textLargeObjects.Dispose ();
			textQR.Dispose ();
			textGAI.Dispose ();
			switchLO.Dispose ();
			switchQR.Dispose ();
			switchGAI.Dispose ();
			base.Dispose (disposing);
		}
		

		void cleanup ()
		{
			this.Dispose ();
		}


		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			//cleanup only if view is loaded and not in a window.
			if(this.IsViewLoaded && this.View.Window == null){
				//cleanup ();
			}
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Preferences Screen");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}



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
			SizeF rect_size = new SizeF (20, 20);
			PointF lop;
			PointF qrp;
			PointF gaip;
			if (UserInterfaceIdiomIsPhone) {
				LargeObjectsRect = new RectangleF (10, 100, 250, 20);
				QRRect = new RectangleF (10, 140, 250, 20);
				GARect = new RectangleF (10, 180, 250, 44);
				lop = new PointF (260, 100);
				qrp = new PointF (260, 140);
				gaip = new PointF (260, 180);
				rect_lo = new RectangleF (lop, rect_size);
				rect_qr = new RectangleF (qrp, rect_size);
				rect_gai = new RectangleF (gaip, rect_size);
			} else {
				//ipad
				LargeObjectsRect = new RectangleF (10, 100, 400, 20);
				QRRect = new RectangleF (10, 140, 400, 20);
				GARect = new RectangleF (10, 180, 400, 60);
				lop = new PointF (410, 100);
				qrp = new PointF (410, 140);
				gaip = new PointF (410, 180);
				rect_lo = new RectangleF (lop, rect_size);
				rect_qr = new RectangleF (qrp, rect_size);
				rect_gai = new RectangleF (gaip, rect_size);
			}

			textLargeObjects = new UILabel (LargeObjectsRect);
			Add (textLargeObjects);

			textQR = new UILabel (QRRect);
			Add (textQR);

			textGAI = new UILabel (GARect);




			Title = NSBundle.MainBundle.LocalizedString ("Preferences","Preferences");
			textLargeObjects.Text = NSBundle.MainBundle.LocalizedString ("List containers in the large object list","List containers in the large object list");
			textQR.Text = NSBundle.MainBundle.LocalizedString ("Include QRcode in emails where applicable", "Include QRcode in emails where applicable"); 
			textGAI.Text = NSBundle.MainBundle.LocalizedString ("help me improve the app: allow anonymous statistics", "help me improve the app: allow anonymous statistics");
			textGAI.Lines = 2;
			textGAI.LineBreakMode = UILineBreakMode.WordWrap;
			textGAI.AdjustsFontSizeToFitWidth = true;
			Add (textGAI);


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

			Background back = new Background ();
			View.AddSubview (back.View);
			View.SendSubviewToBack (back.View);

//			UIButton backbutton = new UIButton(new RectangleF(10,25,48,32));
//			backbutton.SetImage (UIImage.FromBundle ("backarrow.png"), UIControlState.Normal);
//			backbutton.TouchUpInside += (object sender, EventArgs e) => DismissViewControllerAsync (true);
//			Add (backbutton);

			this.switchLO.On = AppDelegate.key.GetContainersAsLarge();
			this.switchQR.On = AppDelegate.key.IncludeQr();
			this.switchGAI.On = AppDelegate.key.StatsEnabled ();

			this.switchLO.ValueChanged += (object sender, EventArgs e) => {
				Console.WriteLine("Value changed:"+switchLO.On.ToString());
				AppDelegate.key.SaveContainersAsLarge(switchLO.On);
				GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateEvent ("setting", "ContainersAsLarge", switchLO.On.ToString(), 1).Build ());
			};

			this.switchQR.ValueChanged += (object sender, EventArgs e) => {
				Console.WriteLine("Value changed:"+switchQR.On.ToString());
				AppDelegate.key.SaveIncludeQR(switchQR.On);
				GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateEvent ("setting", "QR", switchLO.On.ToString(), 1).Build ());
            };

			this.switchGAI.ValueChanged += (object sender, EventArgs e) => {
				AppDelegate.key.SaveStatsEnabled(switchGAI.On);
				if(switchGAI.On == true){
					GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateEvent ("logging", "Verbose", AppDelegate.Variant, 1).Build ());
					GAI.SharedInstance.Logger.LogLevel = GAILogLevel.Verbose;
				}else{
					GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateEvent ("logging", "None", AppDelegate.Variant, 1).Build ());
					GAI.SharedInstance.Logger.LogLevel = GAILogLevel.Error;
				}
			};
		}
	}
}

