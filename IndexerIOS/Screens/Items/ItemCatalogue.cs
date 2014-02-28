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
using SlideDownMenu;
using System.Drawing;

namespace no.dctapps.garageindex
{
	public partial class ItemCatalogue : UIViewController
	{
//		UITableView Table;
//		LagerDAO dao;

		UITableView table;

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
			//Title = NSBundle.MainBundle.LocalizedString ("Items","Items");
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
			table.ReloadData ();
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
			table.Source = source;

			source.ItemClicked += (object sender, ItemClickedEventArgs e) => this.ShowItemDetails(e.Item);

			source.ItemDeleted += (object sender, ItemClickedEventArgs e) => {
				AppDelegate.dao.DeleteItem(e.Item.ID);
				this.Refresh();
			};

			UIButton backbutton = new UIButton(new RectangleF(10,25,48,32));
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
				PresentViewControllerAsync (itemdetail, true);
				//this.NavigationController.PushViewController(itemdetail, true);
			}else{
				RaiseItemClicked(item);
			}
		}
		


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Background back = new Background ();

			View.AddSubview (back.View);
			View.SendSubviewToBack (back.View);

			table = new UITableView (new RectangleF (0, 75, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height - 75), UITableViewStyle.Plain);
			table.BackgroundColor = UIColor.Clear;
			this.View.BackgroundColor = UIColor.Clear;
			Add (table);

			PopulateTable ();
			CreateSlideDownMenu ();
			// Perform any additional setup after loading the view, typically from a nib.
		}
		
		void CreateSlideDownMenu ()
		{
			var item0 = new MenuItem ("Options", UIImage.FromBundle ("frames4832.png"), (menuItem) => {
				Console.WriteLine("Item: {0}", menuItem);
			});
			item0.Tag = 0;
					var extract = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Add Item", "Add Item");
			var item1 = new MenuItem (extract, UIImage.FromBundle ("startree.png"), (menuItem) => {
				Console.WriteLine("Item: {0}", menuItem);
				ShowItemDetails (new Item ());

			});
			item1.Tag = 1;
			var item2 = new MenuItem ("Dismiss", UIImage.FromBundle ("frames4832.png"), (menuItem) => {
				Console.WriteLine("Item: {0}", menuItem);
				this.DismissViewControllerAsync(true);
			});
			item2.Tag = 2;


			//item0.tag = 0;

			var slideMenu = new SlideMenu (new List<MenuItem> { item0, item1, item2});
			slideMenu.Center = new PointF (slideMenu.Center.X, slideMenu.Center.Y + 25);
			this.View.AddSubview (slideMenu);
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

