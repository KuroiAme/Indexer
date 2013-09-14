using System;
using System.Drawing;
using MonoTouch.UIKit;
using System.Collections.Generic;
using no.dctapps.Garageindex.model;
using no.dctapps.Garageindex.dao;
using GarageIndex;
using no.dctapps.Garageindex.tables;
using no.dctapps.Garageindex.events;
using System.Text;
using No.Dctapps.GarageIndex;

namespace no.dctapps.Garageindex.screens
{
	public partial class ContainerDetails : UtilityViewController
	{
		LagerObject boks;
		LagerDAO dao;
//		GarageindexBL bl;
		public UITableView Table;
		//		IList<Item> tableitems;
		TableSourceItems itemtableSource;
		public UIPopoverController popme;
		SelectLager lagerselect;
		const bool test = true;
		//		UIBarButtonItem edit, done, insert;
		
		//		public event EventHandler<GotPictureEventArgs> GotPicture;
		public event EventHandler<ContainerDetailClickedEventArgs> ActivateDetail;
		public event EventHandler<LagerObjectSavedEventArgs> LagerObjectSaved;
		
		
		public ContainerDetails (LagerObject boks)
			: base (UserInterfaceIdiomIsPhone ? "ContainerDetails_iPhone" : "ContainerDetails_iPad")
		{
			this.boks = boks;
			this.dao = new LagerDAO ();
//			this.bl = new GarageindexBL ();
		}
		
		public ContainerDetails () 
			: base (UserInterfaceIdiomIsPhone ? "ContainerDetails_iPhone" : "ContainerDetails_iPad")
		{
			this.dao = new LagerDAO ();
//			this.bl = new GarageindexBL ();
		}
		
		void RaiseSavedEvent ()
		{
			var handler = this.LagerObjectSaved;
			Console.WriteLine("saved");
			if (handler != null) {
				handler(this, new LagerObjectSavedEventArgs());
			}
		}
		
		/// <summary>
		/// Release everything not in use
		/// </summary>
		void cleanup ()
		{
			boks = null;
			dao = null;
			//			table = null;
			//			itemtableSource = null;
		}
		
		void Unclean ()
		{
			if(dao == null)
			{
				dao = new LagerDAO();
			}

		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			//cleanup only if view is loaded and not in a window.
			if(this.IsViewLoaded && this.View.Window == null){
				cleanup ();
			}
			// Release any cached data, images, etc that aren't in use.
		}
		
		/// <summary>
		/// Releases the keyboard aka. resign first responder. Otherwise keyboard wont go away.
		/// </summary>
		public void ReleaseKeyboard ()
		{
			this.fieldContainerName.ShouldReturn += textField =>  {
				textField.ResignFirstResponder ();
				return true;
			};
			this.fieldDescription.ShouldReturn += textField =>  {
				textField.ResignFirstResponder ();
				return true;
			};
		}

		void initializeMoveLager ()
		{

			lagerselect = new SelectLager ();

			this.inStorage.TouchUpInside += (object sender, EventArgs e) =>  {
				if(UserInterfaceIdiomIsPhone){
					this.NavigationController.PushViewController(lagerselect, true);
				}else{
					popme = new UIPopoverController (lagerselect);
					popme.PresentFromRect (this.inStorage.Bounds, this.View, UIPopoverArrowDirection.Up, true);
				}
			};
			lagerselect.DismissEvent += (object sender, LagerClickedEventArgs e) =>  {
				if(UserInterfaceIdiomIsPhone){
					NavigationController.PopToViewController(this, true);
				}else{
					popme.Dismiss (true);
				}
				this.boks.LagerID = e.Lager.ID;
				SetLagerButtonLabel (this.boks);
				dao.saveLagerObject(this.boks);
			};
		}

		void SetLagerButtonLabel (LagerObject itty)
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append (MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("In", "In"));
			sb.Append (":");
			if (itty != null) {
				Lager lager = dao.GetLagerById (itty.LagerID);
				//				boks.LagerID = lo.ID;
				//				SaveIt ();
				if (lager != null) {
					if (!string.IsNullOrEmpty (lager.Name)) {
						sb.Append (lager.Name);
					}
				}
			}
			this.inStorage.SetTitle (sb.ToString (), UIControlState.Normal);
		}
		
		public void PopulateTable ()
		{
			Console.WriteLine("PopulateTable ()");
			dao = new LagerDAO ();
			RectangleF rect;
			
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
				tableItems = dao.getAllItemsInBox(boks);
			}catch(Exception e){
				Console.WriteLine ("catastrophe avoided:" + e.ToString ());
			}
			
//			Add (Table);
			
//			BlackLeatherTheme.Apply (Table, "");
			
			TableSourceItems itemsource = new TableSourceItems (tableItems);
			myTable.Source = itemsource;
			this.itemtableSource = new TableSourceItems (tableItems);
			
			this.itemtableSource.ItemDeleted += (object sender, ItemClickedEventArgs e) => this.DeleteTaskRow(e.Item.ID);
			this.itemtableSource.ItemClicked += (object sender, ItemClickedEventArgs e) => this.ShowItemDetails(e.Item);
			myTable.Source = this.itemtableSource;
			
		}
		
		protected void DeleteTaskRow(int id)
		{	
			dao.DeleteItem(id);
			this.PopulateTable();
		}
		
		void ShowItemDetails (Item item)
		{
			Console.WriteLine ("call itemdetailscreen");
			item.boxID = boks.ID;
			ItemDetailScreen itemdetail = new ItemDetailScreen (item);
			this.NavigationController.PushViewController(itemdetail, true);
		}
		
		void RaiseContainerItemClicked (Item item)
		{
			var handler = this.ActivateDetail;
			Console.WriteLine("item:"+item.ToString());
			if (handler != null && item != null) {
				handler(this, new ContainerDetailClickedEventArgs(item));
			}
		}

//		MFMailComposeViewController mailContr;
//
//		private void CreateEmailBarButton ()
//		{
//			//DO NOT DELETE
//			UIBarButtonItem it = new UIBarButtonItem ();
//			mailContr = new MFMailComposeViewController();
//			mailContr.SetSubject(bl.GenerateSubject(this.boks));
//			mailContr.SetMessageBody(bl.GenerateManifest(this.boks),false);
//			bl.AddPictureAttachment(mailContr, this.boks);
//			it.Title = "email";
//			//IS really info
//			it.Clicked += (object sender, EventArgs e) =>  {
//				this.PresentViewController(mailContr, true, delegate{});
//			};
//			mailContr.Finished += (object sender, MFComposeResultEventArgs e) => {
//				mailContr.DismissViewController(true, delegate{});
//			};
//
//			this.NavigationItem.SetRightBarButtonItem (it, true);
//		}
		
		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.PopulateTable ();	
			Unclean();

		}
		
		void Initialize ()
		{
			this.NavigationItem.SetRightBarButtonItem (new UIBarButtonItem (UIBarButtonSystemItem.Add), false);
			this.NavigationItem.RightBarButtonItem.Clicked += (sender, e) => ShowItemDetails (new Item ());
		}
		
		public void ShowDetails (LagerObject myBox)
		{
			if(myBox == null){
				myBox = new LagerObject();
				myBox.isContainer = "true";
			}

			boks = myBox;
			
			this.fieldContainerName.Text = myBox.Name;
			this.fieldDescription.Text = myBox.Description;
			
			//luckily no picture to display
			
			//but there is a table to repop.
			this.PopulateTable();
		}
		
//		void HandleTouchUpInside (object sender, EventArgs e)
//		{
//			this.SaveIt();
//		}
		
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			ShowDetails (this.boks);s
			
			myTitle = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Container details", "Container details");
			this.fieldContainerName.Placeholder = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Box Identifier", "Box Identifier");
			this.fieldDescription.Placeholder = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Description", "Description");
//			var title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("save", "save");
//			this.save.SetTitle(title, UIControlState.Normal);
			
//			this.save.TouchUpInside += HandleTouchUpInside;

			if(!UserInterfaceIdiomIsPhone){
				this.fieldContainerName.Ended += (object sender, EventArgs e) => SaveIt ();

				this.fieldDescription.Ended += (object sender, EventArgs e) => SaveIt ();
			}
			
			Initialize ();
			ReleaseKeyboard ();
//			this.NavigationController.Title = "Box Details";
			SetLagerButtonLabel (this.boks);
			initializeMoveLager ();
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			if(UserInterfaceIdiomIsPhone){
				SaveIt();
			}
		}
		
		void SaveIt ()
		{
			this.boks.Name = this.fieldContainerName.Text;
			this.boks.Description = this.fieldDescription.Text;
			
			dao.saveLagerObject (boks);
			RaiseSavedEvent();
//			this.NavigationController.PopToRootViewController(true);
		}
	}
}

