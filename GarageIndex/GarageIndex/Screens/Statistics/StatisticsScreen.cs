using MonoTouch.Foundation;
using MonoTouch.UIKit;
using GarageIndex;
using GoogleAdMobAds;
using System.Drawing;

namespace No.DCTapps.GarageIndex
{
    public partial class StatisticsScreen : UIViewController
    {
        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public StatisticsScreen()
			: base (UserInterfaceIdiomIsPhone ? "StatisticsScreen_iPhone" : "StatisticsScreen_iPad", null)
        {

        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
			
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			Title = NSBundle.MainBundle.LocalizedString ("Statistics","Statistics");
//			this.populateStats ();
			populateStats ();
            // Perform any additional setup after loading the view, typically from a nib.
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

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			populateStats ();
		}

		private void populateStats(){
			string NumberOf = NSBundle.MainBundle.LocalizedString("Number of", "Number of");
			string nom_storages = NumberOf + " " + NSBundle.MainBundle.LocalizedString("Storages","Storages") +" : "+AppDelegate.dao.GetAntallLagre();
			this.number_storages.Text = nom_storages;

			string nom_containers = NumberOf + " " + NSBundle.MainBundle.LocalizedString ("Containers", "Containers") + " : " + AppDelegate.dao.GetAntallBeholdere ();
			this.number_containers.Text = nom_containers;

			string nom_items = NumberOf + " " + NSBundle.MainBundle.LocalizedString ("Items", "Items") + " : " + AppDelegate.dao.GetAntallTing();
			this.number_items.Text = nom_items;

			string nom_large = NumberOf + " " + NSBundle.MainBundle.LocalizedString ("Large Objects", "Large Objects") + " : " + AppDelegate.dao.GetAntallStore ();
			this.number_large.Text = nom_large;

		}


    }
}

