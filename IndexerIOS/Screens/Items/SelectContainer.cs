
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using no.dctapps.commons.events.model;
using System.Collections.Generic;
using no.dctapps.commons.events.events;
using no.dctapps.commons.events;

namespace no.dctapps.commons.events
{
	public partial class SelectContainer : UITableViewController
	{
//		LagerDAO dao;

//		UITableView table;
		TableSourceLagerObjectsSimple boxtableSource;

		public event EventHandler<ContainerClickedEventArgs> DismissEvent;

		IList<LagerObject> tableItems;

		public static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public SelectContainer ()
			: base ()
		{
		}

		protected override void Dispose (bool disposing)
		{
			boxtableSource.Dispose ();
			DismissEvent = null;
			tableItems = null;
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
//			Xamarin.Themes.BlackLeatherTheme.Apply (this.View);

			this.PopulateTable();
			// Perform any additional setup after loading the view, typically from a nib.

			var imgView = new UIImageView(BlueSea.MakeBlueSea()){
				ContentMode = UIViewContentMode.ScaleToFill,
				AutoresizingMask = UIViewAutoresizing.All,
				Frame = View.Bounds
			};

			View.AddSubview (imgView);
			View.SendSubviewToBack (imgView);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.PopulateTable();
		}



		public void PopulateTable(){
//			dao = new LagerDAO ();
//            SizeF sf = new SizeF();
//			table = new UITableView(View.SizeThatFits(sf));
//			if (UserInterfaceIdiomIsPhone)
//				table = new UITableView (new RectangleF (0, 0, 300, 310)); //TODO FIx this with space for iAds
//			else {
//				table = new UITableView (new RectangleF (0, 0, 300, 800)); //TODO FIx this with space for iAds
//			}
			tableItems = new List<LagerObject> ();
			try {
				tableItems = (List<LagerObject>) AppDelegate.dao.GetAllContainers ();
			} catch (Exception e) {
				Console.WriteLine ("catastrophe avoided:"+e.ToString());
			}

//			Add (table);
			
//			BlackLeatherTheme.Apply (table, "");

			this.boxtableSource = new TableSourceLagerObjectsSimple(tableItems);
			this.boxtableSource.LagerObjectClicked += (object sender, LagerObjectClickedEventArgs e) => {
				raiseDismissal(e.LagerObject);
				DismissViewControllerAsync(true);
			};
            this.TableView.Source = this.boxtableSource;
		}

		void raiseDismissal (LagerObject lo)
		{
			var handler = this.DismissEvent;
			if(handler != null){
				handler(this, new ContainerClickedEventArgs(lo));
			}
		}
	}
}

