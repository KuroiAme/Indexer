using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.dao;
using System.Collections.Generic;
using no.dctapps.Garageindex.model;
using no.dctapps.Garageindex.events;
using GarageIndex;

namespace no.dctapps.Garageindex.screens
{
	public partial class SelectLager : UtilityViewController
	{

		LagerDAO dao;

		UITableView table;
		TableSourceLagerSimple lagertableSource;

		public event EventHandler<LagerClickedEventArgs> DismissEvent;

//		static bool UserInterfaceIdiomIsPhone {
//			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
//		}

		public SelectLager ()
			: base (UserInterfaceIdiomIsPhone ? "SelectLager_iPhone" : "SelectLager_iPad")
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
			dao = new LagerDAO ();
			table = new UITableView(View.Bounds);
			//			if (UserInterfaceIdiomIsPhone)
			//				table = new UITableView (new RectangleF (0, 0, 300, 310)); //TODO FIx this with space for iAds
			//			else {
			//				table = new UITableView (new RectangleF (0, 0, 300, 800)); //TODO FIx this with space for iAds
			//			}
			IList<Lager> tableItems = new List<Lager> ();
			try {
				tableItems = (List<Lager>) dao.getAllLagers();
			} catch (Exception e) {
				Console.WriteLine ("catastrophe avoided:"+e.ToString());
			}

			Add (table);

			this.lagertableSource = new TableSourceLagerSimple(tableItems);
			this.lagertableSource.LagerClicked += (object sender, LagerClickedEventArgs e) => raiseDismissal (e.Lager);

			table.Source = this.lagertableSource;
		}

		void raiseDismissal (Lager l)
		{
			var handler = this.DismissEvent;
			if(handler != null){
				handler(this, new LagerClickedEventArgs(l));
			}
		}
	}
}

