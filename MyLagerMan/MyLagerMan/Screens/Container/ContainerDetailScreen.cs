//using System;
//using System.Drawing;
//using MonoTouch.Foundation;
//using MonoTouch.UIKit;
//using System.Collections.Generic;
//using no.dctapps.garageindex.model;
//using no.dctapps.garageindex.dao;
//using no.dctapps.garageindex.table;
//using no.dctapps.garageindex.ios.screens;
//using no.dctapps.garageindex.events;
//using Xamarin.Themes;
//using No.Dctapps.Garageindex.Ios.Screens;
//using no.dctapps.garageindex;
//using no.dctapps.garageindex.ios.Screens;
//
//namespace No.Dctapps.GarageIndex.Ios.Screens
//{
//	public partial class ContainerDetailScreen : UtilityViewController
//	{
//		LagerObject boks;
//		LagerDAO dao;
//		public UITableView Table;
////		IList<Item> tableitems;
//		TableSourceItems itemtableSource;
//		const bool test = true;
////		UIBarButtonItem edit, done, insert;
//
////		public event EventHandler<GotPictureEventArgs> GotPicture;
//		public event EventHandler<ContainerDetailClickedEventArgs> ActivateDetail;
//		public event EventHandler<LagerObjectSavedEventArgs> LagerObjectSaved;
//
//
//		public ContainerDetailScreen (LagerObject boks)
//			: base (UserInterfaceIdiomIsPhone ? "ContainerDetailScreen_iPhone" : "ContainerDetailScreen_iPad")
//		{
//			this.boks = boks;
//			this.dao = new LagerDAO ();
//		}
//
//		public ContainerDetailScreen () 
//			: base (UserInterfaceIdiomIsPhone ? "ContainerDetailScreen_iPhone" : "ContainerDetailScreen_iPad")
//		{
//			this.dao = new LagerDAO ();
//		}
//
//		void RaiseSavedEvent ()
//		{
//			var handler = this.LagerObjectSaved;
//			Console.WriteLine("saved");
//			if (handler != null) {
//				handler(this, new LagerObjectSavedEventArgs());
//			}
//		}
//
//		/// <summary>
//		/// Release everything not in use
//		/// </summary>
//		void cleanup ()
//		{
//			boks = null;
//			dao = null;
////			table = null;
////			itemtableSource = null;
//		}
//
//		void Unclean ()
//		{
//			if(dao == null)
//			{
//				dao = new LagerDAO();
//			}
//		}
//		
//		public override void DidReceiveMemoryWarning ()
//		{
//			// Releases the view if it doesn't have a superview.
//			base.DidReceiveMemoryWarning ();
//
//			//cleanup only if view is loaded and not in a window.
//			if(this.IsViewLoaded && this.View.Window == null){
//				cleanup ();
//			}
//			// Release any cached data, images, etc that aren't in use.
//		}
//
//		/// <summary>
//		/// Releases the keyboard aka. resign first responder. Otherwise keyboard wont go away.
//		/// </summary>
//		public void ReleaseKeyboard ()
//		{
//			this.fieldBoxName.ShouldReturn += textField =>  {
//				textField.ResignFirstResponder ();
//				return true;
//			};
//			this.fieldBoxDescription.ShouldReturn += textField =>  {
//				textField.ResignFirstResponder ();
//				return true;
//			};
//		}
//
//		public void PopulateTable ()
//		{
//			Console.WriteLine("PopulateTable ()");
//			dao = new LagerDAO ();
//			RectangleF rect;
//
//			if(UserInterfaceIdiomIsPhone){
//				Console.WriteLine("phone?");
//				rect = new RectangleF(10,80,300,240);
//			}else{
//				Console.WriteLine("ipad!");
//				rect = new RectangleF(10,50,300,800);
//			}
//
//			Table = new UITableView(rect);
//
////			table.AutoresizingMask = UIViewAutoresizing.All;
//
//			IList<Item> tableItems = new List<Item> ();
//
//			try{
//				tableItems = dao.getAllItemsInBox(boks);
//			}catch(Exception e){
//				Console.WriteLine ("catastrophe avoided:" + e.ToString ());
//			}
//
//			Add (Table);
//
//			BlackLeatherTheme.Apply (Table, "");
//
//			TableSourceItems itemsource = new TableSourceItems (tableItems);
//			Table.Source = itemsource;
//			this.itemtableSource = new no.dctapps.garageindex.table.TableSourceItems (tableItems);
//
//			this.itemtableSource.ItemDeleted += (object sender, ItemClickedEventArgs e) => {
//				this.DeleteTaskRow (e.Item.ID); 
//			};
//			this.itemtableSource.ItemClicked += (object sender, ItemClickedEventArgs e) => {
//				this.ShowItemDetails (e.Item); 
//			};
//			Table.Source = this.itemtableSource;
//
//		}
//
//		protected void DeleteTaskRow(int id)
//		{	
//			dao.DeleteItem(id);
//			this.PopulateTable();
//		}
//
//		void ShowItemDetails (Item item)
//		{
////			if(UserInterfaceIdiomIsPhone){
//			Console.WriteLine ("call itemdetailscreen");
//			item.boxID = boks.ID;
//			ItemDetailScreen itemdetail = new ItemDetailScreen (item);
////			this.NavigationController.PresentViewController(itemdetail, true, delegate{});
//			this.NavigationController.PushViewController(itemdetail, true);
////			}else{
////				RaiseContainerItemClicked(item);
////			}
//		}
//
//		void RaiseContainerItemClicked (Item item)
//		{
//			var handler = this.ActivateDetail;
//			Console.WriteLine("item:"+item.ToString());
//			if (handler != null && item != null) {
//				handler(this, new ContainerDetailClickedEventArgs(item));
//			}
//		}
//
//
//		public override void ViewWillAppear (bool animated)
//		{
//			base.ViewWillAppear (animated);
//			this.PopulateTable ();	
//			Unclean();
//		}
//
//		void Initialize ()
//		{
//			this.NavigationItem.SetRightBarButtonItem (new UIBarButtonItem (UIBarButtonSystemItem.Add), false);
//			this.NavigationItem.RightBarButtonItem.Clicked += (sender, e) =>  {
//				ShowItemDetails (new Item ());
//			};
//		}
//
//		public void ShowDetails (LagerObject myBox)
//		{
//			boks = myBox;
//
//			this.fieldBoxName.Text = myBox.Name;
//			this.fieldBoxDescription.Text = myBox.Description;
//			
//			//luckily no picture to display
//
//			//change the global property to match the incoming one
//			
//			//but there is a table to repop.
//			this.PopulateTable();
//		}
//
//		void HandleTouchUpInside (object sender, EventArgs e)
//		{
//			this.SaveIt();
//		}
//
//
//		public override void ViewDidLoad ()
//		{
//			base.ViewDidLoad ();
//
//			ShowDetails (this.boks);
//
//
//			Xamarin.Themes.BlackLeatherTheme.Apply (this.View);
//			Xamarin.Themes.BlackLeatherTheme.Apply (this.fieldBoxName);
//			Xamarin.Themes.BlackLeatherTheme.Apply (this.fieldBoxDescription);
//			Xamarin.Themes.BlackLeatherTheme.Apply (this.btnSaveIt);
//
//
//			Title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Container Details", "Container Details");
//			this.fieldBoxName.Placeholder = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Box Identifier", "Box Identifier");
//			this.fieldBoxDescription.Placeholder = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Description", "Description");
//			var title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("save", "save");
//			this.btnSaveIt.SetTitle(title, UIControlState.Normal);
//
//			this.btnSaveIt.TouchUpInside += HandleTouchUpInside;
//
//			Initialize ();
//			ReleaseKeyboard ();
//			this.NavigationController.Title = "Box Details";
//
//		}
//
//		void SaveIt ()
//		{
//			this.boks.Name = this.fieldBoxName.Text;
//			this.boks.Description = this.fieldBoxDescription.Text;
//
//			dao.saveLagerObject (boks);
//			RaiseSavedEvent();
//		}
//	}
//}