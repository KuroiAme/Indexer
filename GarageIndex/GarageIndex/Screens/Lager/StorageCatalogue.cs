using MonoTouch.UIKit;
using System;
using no.dctapps.Garageindex.events;
using no.dctapps.Garageindex.model;
using System.Collections.Generic;
using GarageIndex;
using no.dctapps.Garageindex.tables;


namespace no.dctapps.Garageindex.screens
{
	public partial class StorageCatalogue : UITableViewController
	{
		public event EventHandler<LagerClickedEventArgs> LagerClicked;

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void LoadView ()
		{
			base.LoadView ();
			Initialize ();
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.PopulateTable ();
		}

		void RaiseLagerClicked (Lager lager)
		{
			var handler = LagerClicked;
			if (handler != null){
				handler(this, new LagerClickedEventArgs(lager));
			}
		}

		public void Refresh ()
		{
			this.PopulateTable();
		}

		void PopulateTable ()
		{
//			Table = new UITableView (View.Bounds);
//			Table.AutoresizingMask = UIViewAutoresizing.All;
			List<Lager> items = (List<Lager>)AppDelegate.dao.getAllLagers();
//			items.Sort ();

			TableSourceLager source = new TableSourceLager (items);
			this.TableView.Source = source;
			
			source.LagerClicked += (object sender, LagerClickedEventArgs e) => ShowItemDetails (e.Lager);
			
			source.LagerDeleted += (object sender, LagerClickedEventArgs e) => {
				AppDelegate.dao.DeleteLager(e.Lager.ID);
				this.Refresh();
			};
		}


		void Initialize ()
		{
			this.NavigationItem.SetRightBarButtonItem (new UIBarButtonItem (UIBarButtonSystemItem.Add), false);
			this.NavigationItem.RightBarButtonItem.Clicked += (sender, e) => ShowItemDetails (new Lager ());
		}

		void ShowItemDetails (Lager lager)
		{
			if(UserInterfaceIdiomIsPhone){
				Console.WriteLine ("call itemdetailscreen");
				
				//item.boxID = boks.ID;
				TheStorageScreen storage = new TheStorageScreen (lager);
				//this.NavigationController.PresentViewController(itemdetail, true, delegate{});
				this.NavigationController.PushViewController(storage, true);
			}else{
				RaiseLagerClicked(lager);
			}
		}

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

	}
}

