using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using no.dctapps.Garageindex.model;
using no.dctapps.Garageindex.dao;
using no.dctapps.Garageindex.events;
using Xamarin.Themes;
using No.Dctapps.Garageindex.Ios.Screens;
using no.dctapps.Garageindex.businesslogic;
using no.dctapps.Garageindex.tables;


namespace no.dctapps.Garageindex.screens
{
	public partial class BigItemsScreen : UITableViewController
	{
//		public UITableView Table{get; set;}
		public TableSourceLagerObjects TableSource {get; set;}

		IList<LagerObject> tableItems;

		public event EventHandler<BigItemDetailClickedEventArgs> ActivateDetail;

		public UIPopoverController Pc;

		LagerDAO dao;
		GarageindexBL bl;

        public static bool UserInterfaceIdiomIsPhone {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }


		public BigItemsScreen () : base ()
//			: base (UserInterfaceIdiomIsPhone ? "BigItemsScreen_iPhone" : "BigItemsScreen_iPad", null)
		{
			dao = new LagerDAO ();
			bl = new GarageindexBL();
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			if(this.IsViewLoaded && this.View.Window == null){
				Cleanup ();
			}
			// Release any cached data, images, etc that aren't in use.
		}

		public void Cleanup(){
			dao = null;
//			Table = null;
			TableSource = null;
			tableItems = null;
		}

		public override void LoadView ()
		{
			base.LoadView ();
//			BlackLeatherTheme.Apply (this.View);
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Large Objects", "Large Objects");

			Initialize ();
			PopulateTable ();

//DO NOT DELETE
//			UIBarButtonItem it = new UIBarButtonItem();
//			it.Title = "info"; //IS really info
//
//			it.Clicked += (object sender, EventArgs e) => {
//			BigItemListInfo li = new BigItemListInfo();
////					UIViewController li = new BigItemListInfo();
//
//			if(UserInterfaceIdiomIsPhone){
////					NavigationController.PresentViewController (li, true, delegate {});
//			NavigationController.PushViewController(li,true);
//			}else{
//				Console.WriteLine("test");
//				Pc = new UIPopoverController(li);
//				Pc.PresentFromBarButtonItem(it,UIPopoverArrowDirection.Down, true);
//				}
//			};
//
//			this.NavigationItem.SetLeftBarButtonItem(it, true);
		}

		public void Refresh ()
		{
			this.PopulateTable();
		}

		void Initialize ()
		{
			this.NavigationItem.SetRightBarButtonItem (new UIBarButtonItem (UIBarButtonSystemItem.Add), false);
			this.NavigationItem.RightBarButtonItem.Clicked += (sender, e) =>  {
				LagerObject lo = new LagerObject();
				lo.isContainer = "false";
				lo.isLargeObject = "true";
				ShowBigItemDetails (lo);
			};
		}


		public virtual void ShowBigItemDetails(LagerObject item)
		{
			if(UserInterfaceIdiomIsPhone){
			BigItemDetailScreen neo = new BigItemDetailScreen (item);
			this.NavigationController.PushViewController(neo, true);
			}else{
				RaiseLagerObjectClicked(item);
			}
		}

		void RaiseLagerObjectClicked (LagerObject item)
		{
			var handler = this.ActivateDetail;
			Console.WriteLine("item:"+item.ToString());
			if (handler != null && item != null) {
				handler(this, new BigItemDetailClickedEventArgs(item));
			}
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			// reload/refresh
			this.Refresh ();
		}

		void printTableItems (IList<LagerObject> list)
		{
			Console.WriteLine ("size:" + list.Count);
			for (int i = 0; i < list.Count; i++) {
				Console.WriteLine("obj-i:"+i+"="+list[i].toString());
			}
		}		
		void PopulateTable ()
		{
			dao = new LagerDAO ();
//			this.view = new UITableView (GetDyn()); 
			if(bl.GetContainersAsLarge()){
				Console.WriteLine ("loadbigitems");
				tableItems = dao.GetAllLagerObjects();
			}else{
				Console.WriteLine ("getalllargeitems");
				tableItems = dao.GetAllLargeItems();
			}

			PopulateWithDummyData ();

//			BlackLeatherTheme.Apply (this);
//			Add (Table);
			TableSource = new TableSourceLagerObjects(tableItems);
			this.TableSource.LagerObjectDeleted += (object sender, LagerObjectClickedEventArgs e) => { this.DeleteLagerObjectRow(e.LagerObject.ID); };
			this.TableSource.LagerObjectClicked += (object sender, LagerObjectClickedEventArgs e) => { this.ShowBigItemDetails(e.LagerObject); };
			TableView.Source = this.TableSource;
//			this.TabBarItem.BadgeValue = dao.getAntallStore();
		}

		void PopulateWithDummyData ()
		{
			if (tableItems.Count == 0) {
				LagerObject sofa = new LagerObject ();
				sofa.Name = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Sofa", "Sofa");
				sofa.Description = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("nice, for sale 1000$", "nice, for sale 1000$");
				tableItems.Add (sofa);
				LagerObject lamp = new LagerObject ();
				lamp.Name = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Lamp", "Lamp");
				lamp.Description = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("scheduled for trashing", "scheduled for trashing");
				tableItems.Add (lamp);
//				if (this.NavigationController != null)
//					this.NavigationController.TabBarItem.BadgeValue = "2";
			}
		}

		protected void DeleteLagerObjectRow(int id)
		{
			dao.DeleteBigItem(id);
			this.PopulateTable();
		}
	}
}

