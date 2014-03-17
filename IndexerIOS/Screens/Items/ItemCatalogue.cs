using MonoTouch.UIKit;
using GarageIndex;
using no.dctapps.Garageindex.events;
using No.Dctapps.GarageIndex;
using no.dctapps.Garageindex.tables;
using no.dctapps.Garageindex.screens;
using GoogleAnalytics.iOS;
using System.Drawing;
using System;
using System.Collections.Generic;

namespace no.dctapps.garageindex
{
	public partial class ItemCatalogue : UtilityViewController
	{

		UITableView table;

		public event EventHandler<ItemClickedEventArgs> ActivateDetail;

		protected override void Dispose (bool disposing)
		{
			ActivateDetail = null;
			table.Dispose ();
			source = null;
			backbutton = null;
			itemdetail = null;
			base.Dispose (disposing);
		}
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

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Item Catalogue Screen");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}

		public void Refresh ()
		{
			table.ReloadData ();
			PopulateTable();
		}

		TableSourceItems source;

		UIButton backbutton;

		void PopulateTable ()
		{
			IList<Item> items = AppDelegate.dao.GetAllItems ();

			source = new TableSourceItems (items);
			table.Source = source;

			source.ItemClicked += (object sender, ItemClickedEventArgs e) => this.ShowItemDetails(e.Item);

			source.ItemDeleted += (object sender, ItemClickedEventArgs e) => {
				AppDelegate.dao.DeleteItem(e.Item.ID);
				this.Refresh();
			};

			backbutton = new UIButton (new RectangleF (10, 25, 48, 32));
			backbutton.SetImage (backarrow.MakeBackArrow(), UIControlState.Normal);
			backbutton.TouchUpInside += (object sender, EventArgs e) => DismissViewControllerAsync (true);
			Add (backbutton);

		}

		void RaiseItemClicked (Item item)
		{
			var handler = this.ActivateDetail;
			Console.WriteLine("container:"+item.ToString());
			if (handler != null && item != null) {
				handler(this, new ItemClickedEventArgs(item));
			}
		}

		void InitializeAddButton ()
		{
			this.NavigationItem.SetRightBarButtonItem (new UIBarButtonItem (UIBarButtonSystemItem.Add), false);
			this.NavigationItem.RightBarButtonItem.Clicked += (sender, e) => ShowItemDetails(new Item());
		}

		ItemDetailScreen itemdetail;

		void ShowItemDetails (Item item)
		{
//			if(UserInterfaceIdiomIsPhone){
			Console.WriteLine ("call itemdetailscreen");

				//item.boxID = boks.ID;
				itemdetail = new ItemDetailScreen (item);
				//this.NavigationController.PresentViewController(itemdetail, true, delegate{});
				this.NavigationController.PushViewController (itemdetail, true);
				//this.NavigationController.PushViewController(itemdetail, true);
//			}else{
//				RaiseItemClicked(item);
//			}
		}
		


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			InitializeAddButton();

			Background back = new Background ();
			View.AddSubview (back.View);
			View.SendSubviewToBack (back.View);

			table = new UITableView (new RectangleF (0, 75, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height - 75), UITableViewStyle.Plain);
			table.BackgroundColor = UIColor.Clear;
			this.View.BackgroundColor = UIColor.Clear;
			Add (table);

			PopulateTable ();

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			PopulateTable ();
		}

		public static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}
	}
}

