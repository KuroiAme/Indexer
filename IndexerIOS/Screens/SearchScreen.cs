using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using no.dctapps.commons.events;
using System.Linq;
using MonoTouch.Foundation;
using no.dctapps.commons.events.screens;
using No.Dctapps.GarageIndex;
using no.dctapps.commons.events.model;
using No.Dctapps.Garageindex.Ios.Screens;
using System.Drawing;
using no.dctapps.commons.events;

namespace no.dctapps.commons 
{
	public class SearchScreen : UtilityViewController
	{
		string initialSearch;
		public SearchScreen (string initialSearch)
		{
			this.initialSearch = initialSearch;
		}

		IList<IndexerDictionaryItem> dictionary;
		//List<String> simpleDictionary;
		WordsTableSource tableSource = null;
		UITableView resultsTable;

		/// <summary>
		/// Release everything not in use
		/// </summary>
		void cleanup ()
		{

		}

		protected override void Dispose (bool disposing)
		{
			dictionary = null;
			initialSearch = null;
			tableSource.Dispose ();
			resultsTable.Dispose ();
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

		public override void LoadView ()
		{
			base.LoadView ();
		}

		UISearchBar searchBar;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			LoadWords ();

			Background back = new Background();
			View.Add(back.View);
			View.SendSubviewToBack (back.View);

			RectangleF resultsRect = new RectangleF (0, 75, View.Bounds.Width, View.Bounds.Height - 75);
			resultsTable = new UITableView (resultsRect);
			resultsTable.BackgroundColor = UIColor.Clear;
			Add (resultsTable);
			searchBar = new UISearchBar (new RectangleF(0, 0, View.Bounds.Width, 40));
			searchBar.Text = this.initialSearch;
			Add (searchBar);


			tableSource = new WordsTableSource(this);
			resultsTable.Source = tableSource;

			searchBar.SearchButtonClicked += (s, e) => searchBar.ResignFirstResponder ();

			// refine the search results every time the text changes
			searchBar.TextChanged += (s, e) => RefineSearchItems ();

			RefineSearchItems ();
		}

		void LoadWords ()
		{
			dictionary = AppDelegate.bl.GetAllSearchableWordsDictionary ();
		}

		protected void RefineSearchItems()
		{
			// select our words
			if (searchBar.Text != null) {
				tableSource.Words = (from x in dictionary
										where x.value != null && x.value.Contains (searchBar.Text)
				                    select x).ToList ();

				// refresh the table
				resultsTable.ReloadData ();
			}
		}

		class WordsTableSource : UITableViewSource
		{
			protected string cellIdentifier = "wordsCell";
			
			UIViewController ancestor;

			public WordsTableSource (UIViewController ancestor)
			{
				this.ancestor = ancestor;
			}

			/// <summary>
			/// The words to display in the table
			/// </summary>
			public List<IndexerDictionaryItem> Words
			{
				get { return words; }
				set { words = value; }
			}
			protected List<IndexerDictionaryItem> words = new List<IndexerDictionaryItem>();

			/// <summary>
			/// called by the table to determine how many rows to create, in our case, it's the number
			/// of words.
			/// </summary>
			public override int RowsInSection (UITableView tableview, int section)
			{
				return words.Count;
//				if (section == 0) {
//					int y =(from x in words
//						where x.type == "GalleryObject"
//						select x).Count();
//					return y;
//				}
//
//				if (section == 1) {
//					int y =(from x in words
//						where x.type == "Item"
//						select x).Count();
//					return y;
//				}
//
//				if (section == 2) {
//					int y =(from x in words
//						where x.type == "Container"
//						select x).Count();
//					return y;
//				}
//
//				if (section == 3) {
//					int y =(from x in words
//						where x.type == "LargeObject"
//						select x).Count();
//					return y;
//				}
//
//				return 1; // this shoudnt happen
			}

			/// <summary>
			/// called by the table to determine how many sections to create, in this case, we just have one
			/// </summary>
			public override int NumberOfSections (UITableView tableView)
			{
				return 1;
			}

			/// <summary>
			/// called by the table to generate the cell to display. in this case, it's very simple
			/// and just displays the word.
			/// </summary>
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				// declare vars
				DCTCell cell = (DCTCell)tableView.DequeueReusableCell (cellIdentifier);
				string word = words[indexPath.Row].value;

				// if there are no cells to reuse, create a new one
				if (cell == null)
					cell = new DCTCell (new NSString (word));

				if (words [indexPath.Row].Name == null) {
					cell.UpdateCell (words [indexPath.Row].value, words [indexPath.Row].type);
				} else {
					cell.UpdateCell (words [indexPath.Row].Name, words [indexPath.Row].type);
				}

				// set the item text

				cell.Layer.CornerRadius = 7.0f;
				cell.Layer.MasksToBounds = true;
				cell.Layer.BorderWidth = 2.0f;
				cell.Layer.BorderColor = UIColor.FromRGBA (34f,139f,34f, 0.9f).CGColor;
				cell.TintColor = UIColor.Orange;
				cell.BackgroundColor = UIColor.FromRGBA (34f,139f,34f, 0.5f);

				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				IndexerDictionaryItem word = words [indexPath.Row];
				if (word.type == "GalleryObject") {
					GalleryObject go = AppDelegate.dao.GetGalleryObjectByID (word.id);
					if (go != null) {
						EditImageModeController ec = new EditImageModeController (go);
						ancestor.NavigationController.PushViewController (ec, true);
					}
				}
				if (word.type == "Item") {
					IList<Item> items = AppDelegate.dao.GetItemById (word.id);
					if (items.Count > 0) {
						Item it = items [0];
						if (it != null) {
							ItemDetailScreen ic = new ItemDetailScreen (it);
							ancestor.NavigationController.PushViewController (ic, true);
						}
					}
				}
				if (word.type == "Container") {
					LagerObject lo = AppDelegate.dao.GetLagerObjectByID (word.id);
						if (lo != null) {
							ContainerDetails cd = new ContainerDetails (lo);
							ancestor.NavigationController.PushViewController (cd, true);
					}
				}

				if (word.type == "LargeObject") {
					LagerObject lo = AppDelegate.dao.GetLagerObjectByID (word.id);
					if (lo != null) {
						BigItemDetailScreen bids = new BigItemDetailScreen (lo);
						ancestor.NavigationController.PushViewController (bids, true);
					}
				}
				

				tableView.DeselectRow (indexPath, true);
			}

//			public override int SectionFor (UITableView tableView, string title, int atIndex)
//			{
//				IndexerDictionaryItem word = words [atIndex];
//				if (word.type == "GalleryObject") {
//					return 0;
//				}
//				if (word.type == "Item") {
//					return 1;
//				}
//				if (word.type == "Container") {
//					return 2;
//				}
//				if (word.type == "LargeObject") {
//					return 3;
//				}
//				return 1;
//			}

//			public override string[] SectionIndexTitles (UITableView tableView)
//			{
//				string[] titles = { "GalleryObjects", "Items", "Containers", "LargeObjects" };
//				return titles;
//			}
		}
	}
}

