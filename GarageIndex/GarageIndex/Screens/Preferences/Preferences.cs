using System;
using MonoTouch.Foundation;
using no.dctapps.Garageindex.businesslogic;
using GarageIndex;
using GoogleAnalytics.iOS;

namespace no.dctapps.Garageindex.screens
{
	public partial class Preferences : UtilityViewController
	{
//		GarageindexBL bl;

//		static bool UserInterfaceIdiomIsPhone {
//			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
//		}

		public Preferences ()
			: base (UserInterfaceIdiomIsPhone ? "Preferences_iPhone" : "Preferences_iPad")
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

		public override void LoadView ()
		{
			base.LoadView ();
			Title = NSBundle.MainBundle.LocalizedString ("Preferences","Preferences");
            this.textContainersInLarge.Text = NSBundle.MainBundle.LocalizedString ("List containers in the large object list","List containers in the large object list");
            this.textIncludeQR.Text = NSBundle.MainBundle.LocalizedString ("Include QRcode in emails where applicable", "Include QRcode in emails where applicable"); 
        }
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.switchContainers.On = AppDelegate.bl.GetContainersAsLarge();
			this.switchQR.On = AppDelegate.bl.IncludeQr();

			this.switchContainers.ValueChanged += (object sender, EventArgs e) => {
				Console.WriteLine("Value changed:"+switchContainers.On.ToString());
				AppDelegate.bl.SaveContainersAsLarge(switchContainers.On);
			};

            this.switchQR.ValueChanged += (object sender, EventArgs e) => {
                Console.WriteLine("Value changed:"+switchQR.On.ToString());
				AppDelegate.bl.SaveIncludeQR(switchQR.On);
            };

//			Xamarin.Themes.BlackLeatherTheme.Apply(this.View);
//			Xamarin.Themes.BlackLeatherTheme.Apply(this.textContainersInLarge);
//            Xamarin.Themes.BlackLeatherTheme.Apply(this.textIncludeQR);
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

