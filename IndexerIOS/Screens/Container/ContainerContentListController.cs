using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace GarageIndex
{
	public class ContainerContentListController : UITableViewController
	{
		public ContainerContentListController () : base (UITableViewStyle.Grouped)
		{
		}

		/// <summary>
		/// Release everything not in use
		/// </summary>
		void cleanup ()
		{
			//Dispose ();
		}

		protected override void Dispose (bool disposing)
		{
			TableView.Dispose ();
			base.Dispose (disposing);
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
			TableView.Source = new ContainerContentListSource ();
		}
	}
}

