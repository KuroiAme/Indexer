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
using no.dctapps.Garageindex;

namespace GarageIndex
{
	class TableSourceTags : UITableViewSource
	{
		IList<ImageTag> tableItems;
		string cellIdentifier = "TableCell";

		public event EventHandler<TagClickedEventArgs> TagClicked;
		public event EventHandler<TagClickedEventArgs> TagDeleted;

		public TableSourceTags (IList<ImageTag> items)
		{
			tableItems = items;
		}

		public override int RowsInSection(UITableView tableview, int section)
		{
			if (tableItems != null) {
				return tableItems.Count;
			} else {
				return 0;
			}
		}

		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			ImageTag item = tableItems[indexPath.Row];
			CustomLagerCell cell = tableView.DequeueReusableCell (cellIdentifier) as CustomLagerCell;



			if (cell == null)
				cell = new CustomLagerCell ((MonoTouch.Foundation.NSString)item.ID.ToString());
				
			cell.UpdateCell(item.ID.ToString(),item.TagString);
			
			return cell;

		}

		void RaiseTaskClicked (int row)
		{
			var handler = this.TagClicked;

			if (handler != null) {
				handler(this, new TagClickedEventArgs(this.tableItems[row]));
			}
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			this.RaiseTaskClicked(indexPath.Row);
			tableView.DeselectRow(indexPath, true);
		}

		public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
		{
			return true; // return false if you wish to disable editing for a specific indexPath or for all rows
		}


		public override UITableViewCellEditingStyle EditingStyleForRow (UITableView tableView, NSIndexPath indexPath)
		{
			if (tableView.Editing) {
				if (indexPath.Row == tableView.NumberOfRowsInSection (0) - 1)
					return UITableViewCellEditingStyle.Insert;
				else
					return UITableViewCellEditingStyle.Delete;
			} else // not in editing mode, enable swipe-to-delete for all rows
				return UITableViewCellEditingStyle.Delete;
		}
		public override NSIndexPath CustomizeMoveTarget (UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath proposedIndexPath)
		{
			var numRows = tableView.NumberOfRowsInSection (0) - 1; // less the (add new) one
			if (proposedIndexPath.Row >= numRows)
				return NSIndexPath.FromRowSection(numRows - 1, 0);
			else
				return proposedIndexPath;
		} 
		public override bool CanMoveRow (UITableView tableView, NSIndexPath indexPath)
		{
			return indexPath.Row < tableView.NumberOfRowsInSection (0) - 1;
		}

		public void WillBeginTableEditing (UITableView tableView)
		{
			tableView.BeginUpdates ();
			// insert the 'ADD NEW' row at the end of table display
			tableView.InsertRows (new NSIndexPath[] { 
				NSIndexPath.FromRowSection (tableView.NumberOfRowsInSection (0), 0) 
			}, UITableViewRowAnimation.Fade);
			// create a new item and add it to our underlying data (it is not intended to be permanent)
//			ImageTag o = new ImageTag ();
//			o.Name = "(add new)";
//			//			o.imageFileNames = new List<string> ();
//			o.ImageFileName = "first.png";
//			tableItems.Add (o);
			tableView.EndUpdates (); // applies the changes
		}

		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			switch (editingStyle) {
			case UITableViewCellEditingStyle.Delete:
				// remove the item from the underlying data source
				this.RaiseTaskDeleted(indexPath.Row);
				tableItems.RemoveAt(indexPath.Row);
				// delete the row from the table
				tableView.DeleteRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);


				break;

//			case UITableViewCellEditingStyle.Insert:
//				//---- create a new item and add it to our underlying data
//
//				Item obj = new Item();
//				obj.Name = "Inserted";
//				obj.Description ="Placeholder";
//				//				obj.imageFileName = "second.png";
//
//				tableItems.Insert (indexPath.Row, obj);
//
//
//
//
//				//---- insert a new row in the table
//				tableView.InsertRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
//				break;
//
			case UITableViewCellEditingStyle.None:
				Console.WriteLine ("CommitEditingStyle:None called");
				break;
			}
		}

		public void DidFinishTableEditing (UITableView tableView)
		{
			tableView.BeginUpdates ();
			// remove our 'ADD NEW' row from the underlying data
			tableItems.RemoveAt (tableView.NumberOfRowsInSection (0) - 1); // zero based :)
			// remove the row from the table display
			tableView.DeleteRows (new NSIndexPath[] { NSIndexPath.FromRowSection (tableView.NumberOfRowsInSection (0) - 1, 0) }, UITableViewRowAnimation.Fade);
			tableView.EndUpdates (); // applies the changes
		}

		void RaiseTaskDeleted (int row)
		{
			var handler = this.TagDeleted;

			if (handler != null) {
				handler(this, new TagClickedEventArgs(this.tableItems[row]));
			}
		}

	}
}

