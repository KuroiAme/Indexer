using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using GoogleAnalytics.iOS;
using System.Drawing;

namespace no.dctapps.commons.events
{
	class EditTags : UtilityViewController
	{
		UITableView table;
		TableSourceTags itemtableSource;
		GalleryObject go;

		public event EventHandler<TagClickedEventArgs> ActivateDetail;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public EditTags (GalleryObject go ){
			this.go = go;
		}

		/// <summary>
		/// Release everything not in use
		/// </summary>
		void cleanup ()
		{
			//Dispose ();
		}

		protected override void Dispose (bool disposing)
		{
			itemtableSource.Dispose ();
			go = null;
			ActivateDetail = null;
			table.Dispose ();
			base.Dispose (disposing);
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

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Edit tags");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Background back = new Background ();
			View.Add (back.View);
			View.SendSubviewToBack (back.View);
			View.BackgroundColor = UIColor.Clear;
		}

		protected void DeleteTagRow(int id)
		{	
			AppDelegate.dao.DeleteTag(id);
			this.PopulateTable();
		}

		void RaiseTagClicked (ImageTag tag)
		{
			var handler = this.ActivateDetail;
			Console.WriteLine("item:"+tag.TagString);
			if (handler != null && tag != null) {
				handler(this, new TagClickedEventArgs(tag));
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

			table = new UITableView (new RectangleF (0, 66, View.Bounds.Width, View.Bounds.Height - 66));
			table.BackgroundColor = UIColor.Clear;
			IList<ImageTag> tableItems = new List<ImageTag> ();
//			IList<ImageTag> tags = AppDelegate.dao.GetTagsByGalleryObjectID (go.ID);

			try{
				tableItems = AppDelegate.dao.GetTagsByGalleryObjectID(go.ID);
			}catch(Exception e){
				Console.WriteLine ("catastrophe avoided:" + e.ToString ());
			}

			//			Add (Table);

//			TableSourceTags tagsource = new TableSourceTags (tableItems);
//
//			this.TableView.Source = tagsource;
			this.itemtableSource = new TableSourceTags (tableItems);

			this.itemtableSource.TagDeleted += (object sender, TagClickedEventArgs e) => this.DeleteTagRow(e.tag.ID);
			this.itemtableSource.TagClicked += (object sender, TagClickedEventArgs e) => this.ShowTagDetails(e.tag);
			table.Source = this.itemtableSource;
			View.AddSubview (table);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.PopulateTable ();	
		}


//		void Initialize ()
//		{
//			this.NavigationItem.SetRightBarButtonItem (new UIBarButtonItem (UIBarButtonSystemItem.Add), false);
//			this.NavigationItem.RightBarButtonItem.Clicked += (sender, e) => ShowTagDetails (new ImageTag ());
//		}

//		Ic = new UIPopoverController (sl);
//		Ic.PresentFromRect (this.btnIn.Bounds, this.View, UIPopoverArrowDirection.Up, true);

//		UIPopoverController pc;

		void ShowTagDetails (ImageTag tag)
		{
			Console.WriteLine ("call tagdetailscreen()");
			TagDetailScreen tagdetails = new TagDetailScreen (tag);
			this.NavigationController.PushViewController (tagdetails, true);
		}
	}
}

