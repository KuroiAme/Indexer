using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.tables;
using System.Collections.Generic;
using no.dctapps.Garageindex.model;
using no.dctapps.Garageindex.events;
using no.dctapps.Garageindex.screens;
using GoogleAnalytics.iOS;
using SlideDownMenu;

namespace GarageIndex
{
	public partial class LagerList : UIViewController
	{
		public event EventHandler<LagerClickedEventArgs> LagerClicked;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		UITableView table;
		//UITableViewController tablectrl;

		public LagerList ()
			: base ()
		{
		}


		public override void LoadView ()
		{
			base.LoadView ();
			Console.WriteLine ("loadview");
			//InitTable ();
		}

		void InitTable ()
		{
			Console.WriteLine ("initTable()");
			//tablectrl = new UITableViewController (UITableViewStyle.Plain);
			//tablectrl.View.Frame = new RectangleF (0, 75, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height - 75);
			table = new UITableView (new RectangleF (0, 75, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height - 75), UITableViewStyle.Plain);
			table.BackgroundColor = UIColor.Clear;
			//table.Frame = ;
			//tablectrl.TableView = table;
			//table.
			//table.View.Bounds = new RectangleF (0, 100, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
			//table.View.Frame = new RectangleF (0, 75, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height - 75);
			//table.TableView = new UITableView (new RectangleF (0, 75, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height));

//			table.AutoresizingMask = UIViewAutoresizing.FlexibleTopMargin;
//			UIEdgeInsets inset = new UIEdgeInsets(75, 0, 0, 0);
//			table.ContentInset = inset;

			//table.BackgroundColor = UIColor.Clear;
			Add (table);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var imgView = new UIImageView(BlueSea.MakeBlueSea()){
				ContentMode = UIViewContentMode.ScaleToFill,
				AutoresizingMask = UIViewAutoresizing.All,
				Frame = UIScreen.MainScreen.Bounds
			};

			View.AddSubview (imgView);
			View.SendSubviewToBack (imgView);
			//this.View.BackgroundColor = UIColor.Green;

			//Title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Storages", "Storages");
			InitTable ();
			this.PopulateTable();
			//this.Initialize ();


//			
			//this.TableView.Frame = 
			
			// Perform any additional setup after loading the view, typically from a nib.
			CreateSlideDownMenu ();
		}

		void CreateSlideDownMenu ()
		{
			var item0 = new MenuItem ("Options", UIImage.FromBundle ("frames4832.png"), (menuItem) => {
				Console.WriteLine("Item: {0}", menuItem);
			});
			item0.Tag = 0;
			var extract = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Add location", "Add location");
			var item1 = new MenuItem (extract, UIImage.FromBundle ("startree.png"), (menuItem) => {
				Console.WriteLine("Item: {0}", menuItem);
				ShowItemDetails (new Lager ());

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

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "LagerList Screen");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
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
			Console.WriteLine ("populateTable()");
			//			Table = new UITableView (View.Bounds);
			//			Table.AutoresizingMask = UIViewAutoresizing.All;
			List<Lager> items = (List<Lager>)AppDelegate.dao.GetAllLagers();
			//			items.Sort ();

			TableSourceLager source = new TableSourceLager (items);
			//this.TableView.Source = source;
//			if (this.table == null) {
//				InitTable ();
//			}


			this.table.Source = source;

			source.LagerClicked += (object sender, LagerClickedEventArgs e) => ShowItemDetails (e.Lager);

			source.LagerDeleted += (object sender, LagerClickedEventArgs e) => {
				AppDelegate.dao.DeleteLager(e.Lager.ID);
				this.Refresh();
			};
		}


//		void Initialize ()
//		{
//			this.NavigationItem.SetRightBarButtonItem (new UIBarButtonItem (UIBarButtonSystemItem.Add), false);
//			this.NavigationItem.RightBarButtonItem.Clicked += (sender, e) => ShowItemDetails (new Lager ());
//		}

		void ShowItemDetails (Lager lager)
		{
			if(UserInterfaceIdiomIsPhone){
				Console.WriteLine ("call itemdetailscreen");

				//item.boxID = boks.ID;
				TheStorageScreen storage = new TheStorageScreen (lager);
				//this.NavigationController.PresentViewController(itemdetail, true, delegate{});
				//this.NavigationController.PushViewController(storage, false);
				PresentViewControllerAsync (storage, true);
			}else{
				RaiseLagerClicked(lager);
			}
		}
	}
}
