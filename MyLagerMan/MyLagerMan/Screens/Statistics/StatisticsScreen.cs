using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.dao;

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
            LagerDAO dao = new LagerDAO();
            string NumberOf = NSBundle.MainBundle.LocalizedString("Number of", "Number of");
            string nom_storages = NumberOf + " " + NSBundle.MainBundle.LocalizedString("Storages","Storages") +" : "+dao.getAntallLagre();
            this.number_storages.Text = nom_storages;
			
            // Perform any additional setup after loading the view, typically from a nib.
        }


    }
}

