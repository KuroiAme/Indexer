using System;
using MonoTouch.UIKit;
using iCarouselSDK;
using System.Drawing;
using System.Collections.Generic;
using no.dctapps.Garageindex.events;
using MonoTouch.Foundation;
using System.IO;
using System.Linq;
using MonoTouch.CoreGraphics;
using MonoTouch.ObjCRuntime;
using GoogleAnalytics.iOS;

namespace GarageIndex
{
	class EditTags : UITableViewController
	{
		TableSourceTags itemtableSource;
		GalleryObject go;

		public event EventHandler<TagClickedEventArgs> ActivateDetail;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public EditTags (GalleryObject go ){
			this.go = go;
		}

//		public override void ViewDidLoad ()
//		{
//			base.ViewDidLoad ();
////			Initialize ();
//
//			// Perform any additional setup after loading the view, typically from a nib.
//		}

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

			IList<ImageTag> tableItems = new List<ImageTag> ();
//			IList<ImageTag> tags = AppDelegate.dao.GetTagsByGalleryObjectID (go.ID);

			try{
				tableItems = AppDelegate.dao.GetTagsByGalleryObjectID(go.ID);
			}catch(Exception e){
				Console.WriteLine ("catastrophe avoided:" + e.ToString ());
			}

			//			Add (Table);

			TableSourceTags tagsource = new TableSourceTags (tableItems);

			this.TableView.Source = tagsource;
			this.itemtableSource = new TableSourceTags (tableItems);

			this.itemtableSource.TagDeleted += (object sender, TagClickedEventArgs e) => this.DeleteTagRow(e.tag.ID);
			this.itemtableSource.TagClicked += (object sender, TagClickedEventArgs e) => this.ShowTagDetails(e.tag);
			this.TableView.Source = this.itemtableSource;
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

		void ShowTagDetails (ImageTag tag)
		{
			Console.WriteLine ("call tagdetailscreen()");
			TagDetailScreen tagdetails = new TagDetailScreen (tag);
			this.NavigationController.PushViewController (tagdetails, true);
		}
	}
}
