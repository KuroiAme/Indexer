using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using No.Dctapps.GarageIndex;
using no.dctapps.Garageindex.events;
using no.dctapps.Garageindex.tables;
using no.dctapps.Garageindex.model;
using no.dctapps.Garageindex.screens;

namespace GarageIndex
{
	public partial class ContainerContent : UITableViewController
	{

//		LagerDAO dao;
		LagerObject boks;
		TableSourceItems itemtableSource;

		public event EventHandler<ContainerContentClickedEventArgs> ActivateDetail;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public ContainerContent (LagerObject boks)
			: base ()
		{
			this.boks = boks;
		}

		protected override void Dispose (bool disposing)
		{
			this.boks = null;
			ActivateDetail = null;
			itemtableSource.Dispose ();
			base.Dispose (disposing);
		}

		/// <summary>
		/// Release everything not in use
		/// </summary>
		void cleanup ()
		{
			//Dispose ();
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
			//Initialize ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		protected void DeleteTaskRow(int id)
		{	
			AppDelegate.dao.DeleteItem(id);
			this.PopulateTable();
		}

		void RaiseContainerItemClicked (Item item)
		{
			var handler = this.ActivateDetail;
			Console.WriteLine("item:"+item.ToString());
			if (handler != null && item != null) {
				handler(this, new ContainerContentClickedEventArgs(item));
			}
		}

		public void PopulateTable ()
		{
			Console.WriteLine("PopulateTable ()");
//			AppDelegate.dao = new LagerDAO ();
//			RectangleF rect;

			//			if(UserInterfaceIdiomIsPhone){
			//				Console.WriteLine("phone?");
			//				rect = new RectangleF(10,100,300,240);
			//			}else{
			//				Console.WriteLine("ipad!");
			//				rect = new RectangleF(10,160,300,800);
			//			}
			//            this.myTable;
			//			Table = new UITableView(rect);

			//			table.AutoresizingMask = UIViewAutoresizing.All;

			IList<Item> tableItems = new List<Item> ();

			try{
				tableItems = AppDelegate.dao.GetAllItemsInBox(boks);
			}catch(Exception e){
				Console.WriteLine ("catastrophe avoided:" + e.ToString ());
			}

			//			Add (Table);

			TableSourceItems itemsource = new TableSourceItems (tableItems);

			this.TableView.Source = itemsource;
			this.itemtableSource = new TableSourceItems (tableItems);

			this.itemtableSource.ItemDeleted += (object sender, ItemClickedEventArgs e) => this.DeleteTaskRow(e.Item.ID);
			this.itemtableSource.ItemClicked += (object sender, ItemClickedEventArgs e) => this.ShowItemDetails(e.Item);
			this.TableView.Source = this.itemtableSource;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.PopulateTable ();	
		}

		
		void Initialize ()
		{
			this.NavigationItem.SetRightBarButtonItem (new UIBarButtonItem (UIBarButtonSystemItem.Add), false);
			this.NavigationItem.RightBarButtonItem.Clicked += (sender, e) => ShowItemDetails (new Item ());
		}

		void ShowItemDetails (Item item)
		{
			Console.WriteLine ("call itemdetailscreen");
			item.boxID = boks.ID;
			ItemDetailScreen itemdetail = new ItemDetailScreen (item);
			this.NavigationController.PushViewController (itemdetail, true);
			//this.NavigationController.PushViewController(itemdetail, true);
		}
	}
}

