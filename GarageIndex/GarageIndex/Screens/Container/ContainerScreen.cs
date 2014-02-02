using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using no.dctapps.Garageindex;
using no.dctapps.Garageindex.events;
using no.dctapps.Garageindex.model;
using No.DCTapps.GarageIndex;
using no.dctapps.Garageindex.tables;
using GarageIndex;
using GoogleAnalytics.iOS;

namespace no.dctapps.Garageindex.screens
{
	public partial class ContainerScreen : UITableViewController
	{
//		UITableView table;
		TableSourceLagerObjects boxtableSource;
//		LagerDAO dao;

		public event EventHandler<ContainerClickedEventArgs> ActivateDetail;

        public static bool UserInterfaceIdiomIsPhone {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }


		public ContainerScreen ()
			: base ()
		{
		}

		void cleanup ()
		{
//			dao = null;
			boxtableSource = null;
//			table = null;
		}

		public void Refresh ()
		{
			PopulateTable();
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Container overview Screen");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			if(this.IsViewLoaded && this.View.Window == null){
				cleanup ();
			}
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
//			BlackLeatherTheme.Apply (this.View);

			Title = NSBundle.MainBundle.LocalizedString ("Containers", "Containers");

			this.NavigationController.Title = NSBundle.MainBundle.LocalizedString ("Containers", "Containers");

			Initialize ();
			PopulateTable ();

		}

		void Initialize ()
		{
			this.NavigationItem.SetRightBarButtonItem (new UIBarButtonItem (UIBarButtonSystemItem.Add), false);
			this.NavigationItem.RightBarButtonItem.Clicked += (sender, e) =>  {
				LagerObject x = new LagerObject();
				x.isContainer = "true";
				ShowBoxItemDetails (x);
			};
		}

		public void ShowBoxItemDetails (LagerObject box)
		{
			if(UserInterfaceIdiomIsPhone){
				Console.WriteLine ("showBoxItemDetails()");
				ContainerDetails boxdetail = new ContainerDetails (box);
				this.NavigationController.PushViewController (boxdetail, true);
			}else{
				RaiseContainerClicked(box);
			}
		}

		void RaiseContainerClicked (LagerObject box)
		{
			var handler = this.ActivateDetail;
			Console.WriteLine("container:"+box.ToString());
			if (handler != null && box != null) {
				handler(this, new ContainerClickedEventArgs(box));
			}
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.PopulateTable ();
		}

		void PopulateTable ()
		{

//			dao = new LagerDAO ();
//			if (UserInterfaceIdiomIsPhone)
//				TableView = new UITableView (new RectangleF (0, 0, 300, 310)); //TODO FIx this with space for iAds
//			else {
//				TableView = new UITableView (new RectangleF (0, 0, 300, 800)); //TODO FIx this with space for iAds
//			}
			IList<LagerObject> tableItems = new List<LagerObject> ();
			try {
				tableItems = (List<LagerObject>) AppDelegate.dao.GetAllContainers ();
			} catch (Exception e) {
				Console.WriteLine ("catastrophe avoided:"+e.ToString());
			}

			if (tableItems.Count == 0) {
				//POPULATE WITH DEFAULT ITEMS
				LagerObject boks1 = new LagerObject ();
				LagerObject boks2 = new LagerObject ();
				boks1.Description = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Comic Books", "Comic Books");
				boks2.Description = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Drawing material", "Drawing material");
				boks1.Name = "DCT01";
				boks2.Name = "DCT02";
				tableItems.Add (boks1);
				tableItems.Add (boks2);
//				this.NavigationController.TabBarItem.BadgeValue = "2";
			}


//			Add (table);

//			BlackLeatherTheme.Apply (TableView, "");
			

//			TableSourceBoxes boxsource = new TableSourceBoxes (tableItems);

			this.boxtableSource = new TableSourceLagerObjects(tableItems);
//			this.boxtableSource = new no.dctapps.garageindex.table.TableSourceBoxes (tableItems);
		
			this.boxtableSource.LagerObjectDeleted += (object sender, LagerObjectClickedEventArgs e) => this.DeleteTaskRow(e.LagerObject.ID);
			this.boxtableSource.LagerObjectClicked += (object sender, LagerObjectClickedEventArgs e) => this.ShowBoxItemDetails(e.LagerObject);

			TableView.Source = this.boxtableSource;

//			this.TabBarItem.BadgeValue = dao.getAntallBeholdere();
		}


		protected void DeleteTaskRow(int id)
		{	
			AppDelegate.dao.DeleteBox(id);
			this.PopulateTable();
		}
	}
}

