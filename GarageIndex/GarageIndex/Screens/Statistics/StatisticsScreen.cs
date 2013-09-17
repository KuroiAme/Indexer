using MonoTouch.Foundation;
using MonoTouch.UIKit;
using GarageIndex;

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
//			this.populateStats ();
			populateStats ();
            // Perform any additional setup after loading the view, typically from a nib.
        }

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			populateStats ();
		}

		private void populateStats(){
			string NumberOf = NSBundle.MainBundle.LocalizedString("Number of", "Number of");
			string nom_storages = NumberOf + " " + NSBundle.MainBundle.LocalizedString("Storages","Storages") +" : "+AppDelegate.dao.getAntallLagre();
			this.number_storages.Text = nom_storages;

			string nom_containers = NumberOf + " " + NSBundle.MainBundle.LocalizedString ("Containers", "Containers") + " : " + AppDelegate.dao.getAntallBeholdere ();
			this.number_containers.Text = nom_containers;

			string nom_items = NumberOf + " " + NSBundle.MainBundle.LocalizedString ("Items", "Items") + " : " + AppDelegate.dao.getAntallTing();
			this.number_items.Text = nom_items;

			string nom_large = NumberOf + " " + NSBundle.MainBundle.LocalizedString ("Large Items", "Large Items") + " : " + AppDelegate.dao.getAntallStore ();
			this.number_large.Text = nom_large;

		}


    }
}

