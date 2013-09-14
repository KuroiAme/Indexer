using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using no.dctapps.Garageindex.businesslogic;
using No.Dctapps.GarageIndex;
using no.dctapps.Garageindex.tables;


namespace no.dctapps.Garageindex.screens
{
	public partial class SearchResultsController : UtilityViewController
	{
		static NSString cellId = new NSString("SearchResultCell");
		GarageindexBL bl;
		List<Item> searchresults;
		UISearchBar searchBar;
		UITableView Table;

//		public static bool UserInterfaceIdiomIsPhone {
//			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
//		}

		public SearchResultsController ()
			: base (UserInterfaceIdiomIsPhone ? "SearchViewController_iPhone" : "SearchViewController_iPad")
		{
			bl = new GarageindexBL();
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

//		public override void LoadView ()
//		{
//			base.LoadView ();
//			this.View = this.TableView;
//		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Table = new UITableView(View.Bounds);
			Table.AutoresizingMask = UIViewAutoresizing.All;



			TableSourceItems itemsource = new TableSourceItems (searchresults);
			Table.Source = itemsource;

			searchBar = new UISearchBar();

			searchBar.Placeholder = "Enter search text here";
			searchBar.SizeToFit();
			searchBar.AutocorrectionType = UITextAutocorrectionType.No;
			searchBar.AutocapitalizationType = UITextAutocapitalizationType.None;

			searchBar.SearchButtonClicked += (object sender, EventArgs e) => {
				Search();

			};
//
//			itemsource.ItemClicked += (object sender, ItemClickedEventArgs e) => {
//				this.ShowItemDetails (e.Item); 
//			};

			Table.TableHeaderView = searchBar;
//			this.SearchBar.

			Add (Table);
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		void Search ()
		{
			Console.WriteLine("Search()");
			searchresults = bl.SearchItems(this.searchBar.Text);
			searchBar.ResignFirstResponder();
			Table.ReloadData();
		}

		void ShowItemDetails (Item item)
		{
			//			if(UserInterfaceIdiomIsPhone){
			Console.WriteLine ("call itemdetailscreen:"+item.ToString());
//			item.boxID = boks.ID;
			ItemDetailScreen itemdetail = new ItemDetailScreen (item);
			//			this.NavigationController.PresentViewController(itemdetail, true, delegate{});
			this.NavigationController.PushViewController(itemdetail, true);
			//			}else{
			//				RaiseContainerItemClicked(item);
			//			}
		}

		class TableSource : UITableViewSource
		{
			SearchResultsController controller;

			public TableSource (SearchResultsController controller){
				this.controller = controller;			
			}

			public override int RowsInSection(UITableView tableView, int section){
				return controller.searchresults.Count;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath){
				UITableViewCell cell = tableView.DequeueReusableCell(cellId);

				if(cell == null)
					cell = new UITableViewCell(UITableViewCellStyle.Default, cellId);
				cell.TextLabel.Text = controller.searchresults[indexPath.Row].Name;

				return cell;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath){
				var vc = new ItemDetailScreen(controller.searchresults[indexPath.Row]);
				controller.NavigationController.PushViewController(vc,true);
			}

		}
	}
}

