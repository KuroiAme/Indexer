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

		UITableView table;

		public event EventHandler<ItemClickedEventArgs> ActivateDetail;

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
			IList<Item> items = AppDelegate.dao.GetAllItems ();

			TableSourceItems source = new TableSourceItems (items);
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

		void InitializeAddButton ()
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
				this.NavigationController.PushViewController (itemdetail, true);
				//this.NavigationController.PushViewController(itemdetail, true);
			}else{
				RaiseItemClicked(item);
			}
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

