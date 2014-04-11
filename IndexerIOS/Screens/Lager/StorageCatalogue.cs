using MonoTouch.UIKit;
using System;
using no.dctapps.commons.events.events;
using no.dctapps.commons.events.model;
using System.Collections.Generic;
using no.dctapps.commons.events;
using no.dctapps.commons.events.tables;
using GoogleAnalytics.iOS;
using System.Drawing;


namespace no.dctapps.commons.events
{
	public partial class StorageCatalogue : UtilityViewController
	{
		public event EventHandler<LagerClickedEventArgs> LagerClicked;

		UITableView table;

		protected override void Dispose (bool disposing)
		{
			table.Dispose ();
			LagerClicked = null;
			base.Dispose (disposing);
		}

		/// <summary>
		/// Release everything not in use
		/// </summary>
		void cleanup ()
		{
			Dispose();
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

		public override void LoadView ()
		{
			base.LoadView ();
			InitializeAddNewItemButton ();
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = AppDelegate.its.getTranslatedText ("locations");

			Background back = new Background ();
			View.Add (back.View);
			View.SendSubviewToBack (back.View);

			table = new UITableView (new RectangleF (0, 75, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height - 75), UITableViewStyle.Plain);
			table.BackgroundColor = UIColor.Clear;
			this.View.BackgroundColor = UIColor.Clear;
			Add (table);

			//this.NavigationController.NavigationBar.BackItem.BackBarButtonItem.Image = backarrow.MakeBackArrow() ;

			//UIButton backbutton = new UIButton(new RectangleF(10,25,48,32));
//			backbutton.SetImage (backarrow.MakeBackArrow(), UIControlState.Normal);
//			backbutton.TouchUpInside += (object sender, EventArgs e) => DismissViewControllerAsync (true);
			//Add (backbutton);

			InitializeAddNewItemButton ();

			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "StorageCatalogue Screen");
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
//			Table = new UITableView (View.Bounds);
//			Table.AutoresizingMask = UIViewAutoresizing.All;
			List<Lager> items = (List<Lager>)AppDelegate.dao.GetAllLagers();
//			items.Sort ();

			TableSourceLager source = new TableSourceLager (items);
			table.Source = source;
			
			source.LagerClicked += (object sender, LagerClickedEventArgs e) => ShowItemDetails (e.Lager);
			
			source.LagerDeleted += (object sender, LagerClickedEventArgs e) => {
				AppDelegate.dao.DeleteLager(e.Lager.ID);
				this.Refresh();
			};
		}


		void InitializeAddNewItemButton ()
		{
			this.NavigationItem.SetRightBarButtonItem (new UIBarButtonItem (UIBarButtonSystemItem.Add), false);
			this.NavigationItem.RightBarButtonItem.Clicked += (sender, e) => ShowItemDetails (new Lager ());
		}

		void ShowItemDetails (Lager lager)
		{
//			if(UserInterfaceIdiomIsPhone){
				Console.WriteLine ("call itemdetailscreen");
				
				//item.boxID = boks.ID;
				TheStorageScreen storage = new TheStorageScreen (lager);
				//this.NavigationController.PresentViewController(itemdetail, true, delegate{});
				//PresentViewController(storage, true, null);
				this.NavigationController.PushViewController (storage, true);
//			}else{
//				RaiseLagerClicked(lager);
//			}
		}

	}
}

