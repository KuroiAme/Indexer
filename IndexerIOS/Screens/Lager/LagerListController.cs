using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace GarageIndex
{
	public class LagerListController : UITableViewController
	{
		public LagerListController () : base (UITableViewStyle.Grouped)
		{
		}

		protected override void Dispose (bool disposing)
		{

			base.Dispose (disposing);
		}

		/// <summary>
		/// Release everything not in use
		/// </summary>
		void cleanup ()
		{
			Dispose ();
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

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Register the TableView's data source
			TableView.Source = new LagerListSource ();
		}
	}
}

