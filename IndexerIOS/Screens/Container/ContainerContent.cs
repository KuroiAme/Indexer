using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using No.Dctapps.GarageIndex;
using no.dctapps.commons.events.events;
using no.dctapps.commons.events.tables;
using no.dctapps.commons.events.model;
using no.dctapps.commons.events.screens;

namespace no.dctapps.commons.events
{
	public partial class ContainerContent : UtilityViewController
	{

		UITableView table;
		LagerObject boks;
		TableSourceItems itemtableSource;

		public event EventHandler<ContainerContentClickedEventArgs> ActivateDetail;

//		static bool UserInterfaceIdiomIsPhone {
//			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
//		}

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
			table.Dispose ();
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
			Background back = new Background ();
			View.AddSubview (back.View);
			View.SendSubviewToBack (back.View);

			this.PopulateTable ();	

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

			table = new UITableView(new RectangleF(0,66,View.Bounds.Width,View.Bounds.Height -66f));
			table.BackgroundColor = UIColor.Clear;

			IList<Item> tableItems= AppDelegate.dao.GetAllItemsInBox(boks);

			this.itemtableSource = new TableSourceItems (tableItems);

			this.itemtableSource.ItemDeleted += (object sender, ItemClickedEventArgs e) => this.DeleteTaskRow(e.Item.ID);
			this.itemtableSource.ItemClicked += (object sender, ItemClickedEventArgs e) => this.ShowItemDetails(e.Item);

			table.Source = this.itemtableSource;
			Add (table);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			PopulateTable ();
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

