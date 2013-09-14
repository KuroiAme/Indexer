
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using LagerMan.DAO;
using MyLagerMan;

namespace no.dctapps.bundlemanifest
{
	public partial class OverviewScreen : AdsViewController
	{
		LagerDAO dao;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public OverviewScreen ()
			: base (UserInterfaceIdiomIsPhone ? "OverviewScreen_iPhone" : "OverviewScreen_iPad")
		{
			dao = new LagerDAO();
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.textOversikt.Text = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Overview", "Overview");
			this.textAntallEsker.Text = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Number of boxes", "Number of boxes");
			this.textAntallStore.Text = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Number of Large Items", "Number of Large items");
			this.tekstAntallTingIEsker.Text = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Number of Items in boxes", "Number of Items in boxes");
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.tallAntallEsker.Text = dao.getAntallEsker();
			this.tallAntallStore.Text = dao.getAntallStore();
			this.tallAntTing.Text = dao.getAntallTing();
		}
	}
}

