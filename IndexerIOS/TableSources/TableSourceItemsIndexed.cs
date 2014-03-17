using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Linq;
using no.dctapps.Garageindex.events;
using no.dctapps.Garageindex.model;
using No.Dctapps.GarageIndex;
using no.dctapps.Garageindex.dao;
using GarageIndex;

namespace no.dctapps.Garageindex.tables
{
	public class TableSourceItemsIndexed : UITableViewSource {

		string cellIdentifier = "TableCell";
		Dictionary<string, List<string>> indexedTableItems;
		string[] keys;

		public event EventHandler<ItemClickedEventArgs> ItemDeleted;
		public event EventHandler<ItemClickedEventArgs> ItemClicked;

		public TableSourceItemsIndexed (string[] items)
		{

			indexedTableItems = new Dictionary<string, List<string>>();
			foreach (var t in items) {
				if (!string.IsNullOrEmpty (t)) {
					if (indexedTableItems.ContainsKey (t [0].ToString ().ToLower())) {
						indexedTableItems [t [0].ToString ().ToLower()].Add (t);
					} else {
						indexedTableItems.Add (t [0].ToString ().ToLower(), new List<string> () {t});
					}
				}
			}
			keys = indexedTableItems.Keys.ToArray ();
		}
		
		/// <summary>
		/// Called by the TableView to determine how many sections(groups) there are.
		/// </summary>
		public override int NumberOfSections (UITableView tableView)
		{
			return keys.Length;
		}
		
		/// <summary>
		/// Called by the TableView to determine how many cells to create for that particular section.
		/// </summary>
		public override int RowsInSection (UITableView tableview, int section)
		{
			return indexedTableItems[keys[section]].Count;
		}
		
		/// <summary>
		/// Sections the index titles.
		/// </summary>
		public override string[] SectionIndexTitles (UITableView tableView)
		{
			return indexedTableItems.Keys.ToArray ();
		}

		void RaiseTaskClicked (Item it)
		{
			var handler = this.ItemClicked;
			
			if (handler != null) {
				handler(this, new ItemClickedEventArgs(it));
			}
		}

		
		/// <summary>
		/// Called when a row is touched
		/// </summary>
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
//			new UIAlertView("Row Selected"
//			                , indexedTableItems[keys[indexPath.Section]][indexPath.Row]
//			                , null, "OK", null).Show();
			

			string input = indexedTableItems[keys[indexPath.Section]][indexPath.Row];

			IList<Item> R = AppDelegate.dao.GetItemsWithName(input);
			foreach(Item X in R){
				Console.WriteLine(X.toString());
			}
			Item x = R[0];
			RaiseTaskClicked(x);

			tableView.DeselectRow (indexPath, true);
		}
		
		/// <summary>
		/// Called by the TableView to get the actual UITableViewCell to render for the particular row
		/// </summary>
		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
//			// request a recycled cell to save memory
//			UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);
//			// if there are no cells to reuse, create a new one
//			if (cell == null)
//				cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier);
//
			CustomLagerCell cell = tableView.DequeueReusableCell (cellIdentifier) as CustomLagerCell;
			
			
			
			if (cell == null)
				cell = new CustomLagerCell ((MonoTouch.Foundation.NSString)cellIdentifier);

			//cell.Editing = true; // Make the cell deletable
			//cell.ShouldIndentWhileEditing = true;
			//cell.ShowingDeleteConfirmation = true;
			cell.TextLabel.Text = indexedTableItems[keys[indexPath.Section]][indexPath.Row];
			//cell.


//			BlackLeatherTheme.Apply(cell);
			cell.Layer.CornerRadius = 7.0f;
			cell.Layer.MasksToBounds = true;
			cell.Layer.BorderWidth = 2.0f;
			cell.Layer.BorderColor = UIColor.FromRGBA (34f,139f,34f, 0.9f).CGColor;
			cell.TintColor = UIColor.Orange;
			cell.BackgroundColor = UIColor.FromRGBA (34f,139f,34f, 0.5f);
			return cell;
		}

//		void RaiseItemDeleted (Item raised)
//		{
//			var handler = this.ItemDeleted;
//
//			if (handler != null){
//				handler(this, new ItemClickedEventArgs(raised));
//			}
//
//		}

//		void RaiseTaskDeleted (int row)
//		{
//			var handler = this.ItemDeleted;
//			
//			if (handler != null) {
//				handler(this, new ItemClickedEventArgs(this.tableItems[row]));
//			}
//		}
//
//		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, MonoTouch.Foundation.NSIndexPath indexPath)
//		{
//			switch (editingStyle) {
//			case UITableViewCellEditingStyle.Delete:
//				// remove the item from the underlying data source
//				
//				string input = indexedTableItems[keys[indexPath.Section]][indexPath.Row];
//						
//				IList<Item> R = dao.GetItemsWithName(input);
//				Item x = R[0];
//
//				this.RaiseItemDeleted(x);
//
////				this.indexedTableItems.RemoveAt(indexPath.Row);
//				this.indexedTableItems.Remove(x.Name);
//				
//				// delete the row from the table
////				tableView.DeleteRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
//				
//				
//				break;
//				
////			case UITableViewCellEditingStyle.Insert:
////				//---- create a new item and add it to our underlying data
////				
////				LagerObject obj = new LagerObject();
////				obj.Name = "Inserted";
////				obj.imageFileName = "second.png";
////				
////				tableItems.Insert (indexPath.Row, obj);
////				
////				
////				
////				
////				//---- insert a new row in the table
////				tableView.InsertRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
////				break;
//				
//			case UITableViewCellEditingStyle.None:
//				Console.WriteLine ("CommitEditingStyle:None called");
//				break;
//			}
//		}
	}
}



