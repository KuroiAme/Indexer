//using System;
//using MonoTouch.UIKit;
//using MonoTouch.Foundation;
//
//using System.Collections.Generic;
//using MyLagerMan;
//using no.dctapps.garageindex.model;
//using no.dctapps.garageindex.events;
//
//namespace no.dctapps.garageindex.table
//{
//	public class TableSourceBoxes : UITableViewSource
//	{
//		IList<LagerObject> tableItems;
//		string cellIdentifier = "TableCell";
//
//		public event EventHandler<BoxClickedEventArgs> BoxClicked;
//		public event EventHandler<BoxClickedEventArgs> BoxDeleted;
//
//		public TableSourceBoxes (IList<LagerObject> items)
//		{
//			tableItems = items;
//		}
//
//		public override int RowsInSection(UITableView tableview, int section)
//		{
//			if (tableItems != null) {
//				return tableItems.Count;
//			} else {
//				return 0;
//			}
//		}
//
//		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
//		{
////			FileHandler fh = new FileHandler ();
////			LagerObject o = tableItems [indexPath.Row];
//			LagerObject box = tableItems[indexPath.Row];
////			string sub = o.subtitle; 
////			UIImage image = fh.loadUserImage (imageFile);
////			cell.ImageView.Image = UIImage.FromFile ("Images/" +tableItems[indexPath.Row].ImageName);
////			var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
////			string filename = System.IO.Path.Combine (documentsDirectory, o.imageFileName);
//			UIImage image = UIImage.FromFile ("first.png"); //TODO erstatt denne med et bilde av en bananeske.
//
////			var cellStyle = UITableViewCellStyle.Subtitle;
//			CustomLagerCell cell = tableView.DequeueReusableCell (cellIdentifier) as CustomLagerCell;
//
//
//
//			if (cell == null)
//				cell = new CustomLagerCell ((MonoTouch.Foundation.NSString)box.Name);
////			cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
//
//			Xamarin.Themes.BlackLeatherTheme.Apply (cell);
//
//			cell.UpdateCell (box.Name, image, box.Description);
//			return cell;
//
//		}
//
//		void RaiseTaskClicked (int row)
//		{
//			var handler = this.BoxClicked;
//
//			if (handler != null) {
//				handler(this, new BoxClickedEventArgs(this.tableItems[row]));
//			}
//		}
//
//		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
//		{
//
//			this.RaiseTaskClicked(indexPath.Row);
//
//		}
//
//		public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
//		{
//			return true; // return false if you wish to disable editing for a specific indexPath or for all rows
//		}
//
//
//		public override UITableViewCellEditingStyle EditingStyleForRow (UITableView tableView, NSIndexPath indexPath)
//		{
//			if (tableView.Editing) {
//				if (indexPath.Row == tableView.NumberOfRowsInSection (0) - 1)
//					return UITableViewCellEditingStyle.Insert;
//				else
//					return UITableViewCellEditingStyle.Delete;
//			} else // not in editing mode, enable swipe-to-delete for all rows
//				return UITableViewCellEditingStyle.Delete;
//		}
//		public override NSIndexPath CustomizeMoveTarget (UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath proposedIndexPath)
//		{
//			var numRows = tableView.NumberOfRowsInSection (0) - 1; // less the (add new) one
//			if (proposedIndexPath.Row >= numRows)
//				return NSIndexPath.FromRowSection(numRows - 1, 0);
//			else
//				return proposedIndexPath;
//		} 
//		public override bool CanMoveRow (UITableView tableView, NSIndexPath indexPath)
//		{
//			return indexPath.Row < tableView.NumberOfRowsInSection (0) - 1;
//		}
//
//		//TODO is this method really used anymore?
//		public void WillBeginTableEditing (UITableView tableView)
//		{
//			tableView.BeginUpdates ();
//			// insert the 'ADD NEW' row at the end of table display
//			tableView.InsertRows (new NSIndexPath[] { 
//				NSIndexPath.FromRowSection (tableView.NumberOfRowsInSection (0), 0) 
//			}, UITableViewRowAnimation.Fade);
//			// create a new item and add it to our underlying data (it is not intended to be permanent)
//			LagerObject o = new LagerObject ();
//			o.Name = "(add new)";
////			o.imageFileNames = new List<string> ();
////			o.imageFileName = "first.png";
//			tableItems.Add (o);
//			tableView.EndUpdates (); // applies the changes
//		}
//
//		public void DidFinishTableEditing (UITableView tableView)
//		{
//			tableView.BeginUpdates ();
//			// remove our 'ADD NEW' row from the underlying data
//			tableItems.RemoveAt (tableView.NumberOfRowsInSection (0) - 1); // zero based :)
//			// remove the row from the table display
//			tableView.DeleteRows (new NSIndexPath[] { NSIndexPath.FromRowSection (tableView.NumberOfRowsInSection (0) - 1, 0) }, UITableViewRowAnimation.Fade);
//			tableView.EndUpdates (); // applies the changes
//		}
//
//		void RaiseTaskDeleted (int row)
//		{
//			var handler = this.BoxDeleted;
//
//			if (handler != null) {
//				handler(this, new BoxClickedEventArgs(this.tableItems[row]));
//			}
//		}
//
//		//TODO is this method really called anymore?
//		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, MonoTouch.Foundation.NSIndexPath indexPath)
//		{
//			switch (editingStyle) {
//			case UITableViewCellEditingStyle.Delete:
//				// remove the item from the underlying data source
//				this.RaiseTaskDeleted(indexPath.Row);
//				tableItems.RemoveAt(indexPath.Row);
//				// delete the row from the table
//				tableView.DeleteRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
//
//
//				break;
//				
//			case UITableViewCellEditingStyle.Insert:
//				//---- create a new item and add it to our underlying data
//
//				LagerObject obj = new LagerObject();
//				obj.Name = "Inserted";
//				obj.Description ="Placeholder";
////				obj.imageFileName = "second.png";
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
//			case UITableViewCellEditingStyle.None:
//				Console.WriteLine ("CommitEditingStyle:None called");
//				break;
//			}
//		}
//
//
//
//
//	}
//}
//
