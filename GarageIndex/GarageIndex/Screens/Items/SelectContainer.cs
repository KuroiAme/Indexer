
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.model;
using System.Collections.Generic;
using no.dctapps.Garageindex.events;
using GarageIndex;

namespace no.dctapps.Garageindex.screens
{
	public partial class SelectContainer : UITableViewController
	{
//		LagerDAO dao;

//		UITableView table;
		TableSourceLagerObjectsSimple boxtableSource;

		public event EventHandler<ContainerClickedEventArgs> DismissEvent;

		public static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public SelectContainer ()
			: base ()
		{
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
//			Xamarin.Themes.BlackLeatherTheme.Apply (this.View);

			this.PopulateTable();
			// Perform any additional setup after loading the view, typically from a nib.
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
			IList<LagerObject> tableItems = new List<LagerObject> ();
			try {
				tableItems = (List<LagerObject>) AppDelegate.dao.GetAllContainers ();
			} catch (Exception e) {
				Console.WriteLine ("catastrophe avoided:"+e.ToString());
			}

//			Add (table);
			
//			BlackLeatherTheme.Apply (table, "");

			this.boxtableSource = new TableSourceLagerObjectsSimple(tableItems);
			this.boxtableSource.LagerObjectClicked += (object sender, no.dctapps.Garageindex.events.LagerObjectClickedEventArgs e) => raiseDismissal(e.LagerObject);
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

