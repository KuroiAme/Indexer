
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.dao;
using Xamarin.Themes;
using System.Collections.Generic;
using no.dctapps.Garageindex.model;
using No.Dctapps.Garageindex.Ios.Screens;
using no.dctapps.Garageindex.events;
using no.dctapps.Garageindex.tables;

namespace no.dctapps.Garageindex.screens
{
	public partial class StorageCatalogue : UtilityViewController
	{
		UITableView Table;
		LagerDAO dao;

		public event EventHandler<LagerClickedEventArgs> LagerClicked;

//		static bool UserInterfaceIdiomIsPhone {
//			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
//		}

		public StorageCatalogue ()
			: base (UserInterfaceIdiomIsPhone ? "StorageCatalogue_iPhone" : "StorageCatalogue_iPad")
		{
			dao = new LagerDAO();
		}

		
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
			BlackLeatherTheme.Apply(this.View);
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Storages", "Storages");
			this.PopulateTable();
			
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
			Table = new UITableView (View.Bounds);
			Table.AutoresizingMask = UIViewAutoresizing.All;
			List<Lager> items = (List<Lager>)dao.getAllLagers();
//			items.Sort ();

			TableSourceLager source = new TableSourceLager (items);
			Table.Source = source;
			
			source.LagerClicked += (object sender, LagerClickedEventArgs e) => {
				ShowItemDetails (e.Lager); 
			};
			
			source.LagerDeleted += (object sender, LagerClickedEventArgs e) => {
				dao.DeleteLager(e.Lager.ID);
				this.Refresh();
			};
			
			Add (Table);
//			BlackLeatherTheme.Apply(Table);
			this.TabBarItem.BadgeValue = items.Count.ToString();
		}


		void Initialize ()
		{
			this.NavigationItem.SetRightBarButtonItem (new UIBarButtonItem (UIBarButtonSystemItem.Add), false);
			this.NavigationItem.RightBarButtonItem.Clicked += (sender, e) =>  {
				ShowItemDetails (new Lager ());
			};
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


	}
}

