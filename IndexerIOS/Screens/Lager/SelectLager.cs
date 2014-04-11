using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using no.dctapps.commons.events.model;
using no.dctapps.commons.events.events;
using no.dctapps.commons.events;

namespace no.dctapps.commons.events
{
	public partial class SelectLager : UtilityViewController
	{

//		LagerDAO dao;

		UITableView table;
		TableSourceLagerSimple lagertableSource;

		public event EventHandler<LagerClickedEventArgs> DismissEvent;

//		static bool UserInterfaceIdiomIsPhone {
//			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
//		}

		public SelectLager ()
			: base ()
		{
		}

		protected override void Dispose (bool disposing)
		{
			this.lagertableSource.Dispose ();
			this.DismissEvent = null;
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
			Background back = new Background ();
			View.AddSubview (back.View);
			View.SendSubviewToBack (back.View);
			View.BackgroundColor = UIColor.Clear;

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
			table = new UITableView(new RectangleF(0,66,View.Bounds.Width,View.Bounds.Height - 66));
			table.BackgroundColor = UIColor.Clear;
			Add (table);

			IList<Lager> tableItems = new List<Lager> ();
			try {
				tableItems = (List<Lager>) AppDelegate.dao.GetAllLagers();
			} catch (Exception e) {
				Console.WriteLine ("catastrophe avoided:"+e.ToString());
			}



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

