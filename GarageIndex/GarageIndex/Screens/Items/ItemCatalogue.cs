using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using no.dctapps.Garageindex;
using No.Dctapps.Garageindex.Ios.Screens;
using MonoTouch.Foundation;
using No.DCTapps.GarageIndex;
using GarageIndex;
using no.dctapps.Garageindex.events;
using No.Dctapps.GarageIndex;
using no.dctapps.Garageindex.tables;
using no.dctapps.Garageindex.screens;
using GoogleAnalytics.iOS;

namespace no.dctapps.garageindex
{
	public partial class ItemCatalogue : UITableViewController
	{
//		UITableView Table;
//		LagerDAO dao;


		public event EventHandler<ItemClickedEventArgs> ActivateDetail;

//		public ItemCatalogue ()
//			: base (UserInterfaceIdiomIsPhone ? "ItemCatalogue_iPhone" : "ItemCatalogue_iPad")
//		{
//			dao = new LagerDAO();
//		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void LoadView ()
		{
			Title = NSBundle.MainBundle.LocalizedString ("Items","Items");
			base.LoadView ();
//			BlackLeatherTheme.Apply(this.View);
			Initialize();
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Item Catalogue Screen");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}

		public void Refresh ()
		{
			this.TableView.ReloadData ();
			PopulateTable();
		}

		void PopulateTable ()
		{
//			Table = new UITableView (View.Bounds);
//			Table.AutoresizingMask = UIViewAutoresizing.All;

			List<Item> items = (List<Item>)AppDelegate.dao.GetAllItems ();
			items.Sort ();
			List<String> strlist = new List<String> ();
			foreach (Item it in items) {
				strlist.Add (it.Name);
			}
			string[] tableItems = strlist.ToArray ();
			TableSourceItemsIndexed source = new TableSourceItemsIndexed (tableItems);
			TableView.Source = source;

			source.ItemClicked += (object sender, ItemClickedEventArgs e) => this.ShowItemDetails(e.Item);

			source.ItemDeleted += (object sender, ItemClickedEventArgs e) => {
				AppDelegate.dao.DeleteItem(e.Item.ID);
				this.Refresh();
			};

//			Add (Table);
//			BlackLeatherTheme.Apply(Table);
//			this.TabBarItem.BadgeValue = items.Count.ToString();
		}

		void RaiseItemClicked (Item item)
		{
			var handler = this.ActivateDetail;
			Console.WriteLine("container:"+item.ToString());
			if (handler != null && item != null) {
				handler(this, new ItemClickedEventArgs(item));
			}
		}

		void Initialize ()
		{
			this.NavigationItem.SetRightBarButtonItem (new UIBarButtonItem (UIBarButtonSystemItem.Add), false);
			this.NavigationItem.RightBarButtonItem.Clicked += (sender, e) => ShowItemDetails(new Item());
		}


		void ShowItemDetails (Item item)
		{
			if(UserInterfaceIdiomIsPhone){
			Console.WriteLine ("call itemdetailscreen");

				//item.boxID = boks.ID;
				ItemDetailScreen itemdetail = new ItemDetailScreen (item);
				//this.NavigationController.PresentViewController(itemdetail, true, delegate{});
				this.NavigationController.PushViewController(itemdetail, true);
			}else{
				RaiseItemClicked(item);
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			PopulateTable ();
			// Perform any additional setup after loading the view, typically from a nib.
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

