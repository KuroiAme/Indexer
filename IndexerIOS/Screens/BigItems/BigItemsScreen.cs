using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using no.dctapps.Garageindex.model;
using no.dctapps.Garageindex.tables;
using GarageIndex;
using no.dctapps.Garageindex.events;
using No.Dctapps.Garageindex.Ios.Screens;
using GoogleAnalytics.iOS;
using System.Drawing;


namespace no.dctapps.Garageindex.screens
{
	public partial class BigItemsScreen : UtilityViewController
	{
//		public UITableView Table{get; set;}
		public TableSourceLagerObjects TableSource {get; set;}

		IList<LagerObject> tableItems;

		public event EventHandler<BigItemDetailClickedEventArgs> ActivateDetail;

		public UIPopoverController Pc;

		UITableView table;
	



		protected override void Dispose (bool disposing)
		{
			TableSource.Dispose ();
			tableItems = null;
			ActivateDetail = null;
			Pc.Dispose ();
			table.Dispose ();
			base.Dispose (disposing);
		}

		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Large Objects", "Large Objects");
			Background back = new Background ();
			View.AddSubview (back.View);
			View.SendSubviewToBack (back.View);

			table = new UITableView (new RectangleF (0, 75, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height - 75), UITableViewStyle.Plain);
			table.BackgroundColor = UIColor.Clear;
			this.View.BackgroundColor = UIColor.Clear;
			Add (table);

			Initialize ();
			PopulateTable ();
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
				this.NavigationController.PushViewController (neo, true);
				//this.NavigationController.PushViewController(neo, true);
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

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Big items overview Screen");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
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
//			dao = new LagerDAO ();
//			this.view = new UITableView (GetDyn()); 
			if(AppDelegate.key.GetContainersAsLarge()){
				Console.WriteLine ("loadbigitems");
				tableItems = AppDelegate.dao.GetAllLagerObjects();
			}else{
				Console.WriteLine ("getalllargeitems");
				tableItems = AppDelegate.dao.GetAllLargeItems();
			}

			//PopulateWithDummyData ();

//			BlackLeatherTheme.Apply (this);
//			Add (Table);
			TableSource = new TableSourceLagerObjects(tableItems);
			this.TableSource.LagerObjectDeleted += (object sender, LagerObjectClickedEventArgs e) => this.DeleteLagerObjectRow (e.LagerObject.ID);
			this.TableSource.LagerObjectClicked += (object sender, LagerObjectClickedEventArgs e) => this.ShowBigItemDetails (e.LagerObject);
			table.Source = this.TableSource;
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
			AppDelegate.dao.DeleteBigItem(id);
			this.PopulateTable();
		}
	}
}

