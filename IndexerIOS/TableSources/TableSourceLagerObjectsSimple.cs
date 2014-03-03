using System;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.events;
using no.dctapps.Garageindex.model;

namespace no.dctapps.Garageindex.screens
{
	public class TableSourceLagerObjectsSimple : UITableViewSource
	{
		IList<LagerObject> tableItems;
		string cellIdentifier = "TableCell";

		public event EventHandler<LagerObjectClickedEventArgs> LagerObjectClicked;

		public TableSourceLagerObjectsSimple (IList<LagerObject> items)
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
//			FileHandler fh = new FileHandler ();
			LagerObject o = tableItems [indexPath.Row];
//			string sub = o.subtitle; 
//			UIImage image = fh.loadUserImage (imageFile);
//			cell.ImageView.Image = UIImage.FromFile ("Images/" +tableItems[indexPath.Row].ImageName);
//			var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
//			bool exists = true;
//			UIImage image = null;
//			if (o.thumbFileName == null) {
//				o.thumbFileName = "NONEXISTANT.png";
//			}
//
//			string filename = System.IO.Path.Combine (documentsDirectory, o.thumbFileName);
//
//			if (File.Exists (filename)) {
//				image = UIImage.FromFile (filename);
//			} else {
//				exists = false;
//			}

//			var cellStyle = UITableViewCellStyle.Subtitle;
			CustomLagerCell cell = tableView.DequeueReusableCell (cellIdentifier) as CustomLagerCell;



			if (cell == null)
				cell = new CustomLagerCell ((MonoTouch.Foundation.NSString)o.Name);
//			cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
//			Xamarin.Themes.BlackLeatherTheme.Apply (cell);

//			if (exists)
//				cell.UpdateCell (o.Name, image, o.Description);
//			else
			cell.UpdateCell (o.Name, o.Description);

			cell.Layer.CornerRadius = 7.0f;
			cell.Layer.MasksToBounds = true;
			cell.Layer.BorderWidth = 2.0f;
			cell.Layer.BorderColor = UIColor.FromRGBA (34f,139f,34f, 0.9f).CGColor;
			cell.TintColor = UIColor.Orange;
			cell.BackgroundColor = UIColor.FromRGBA (34f,139f,34f, 0.5f);

			return cell;

		}

		void RaiseLagerObjectClicked (int row)
		{
			var handler = this.LagerObjectClicked;
			var item = this.tableItems[row];
			Console.WriteLine("item:"+item.ToString());
			if (handler != null && item != null) {
				handler(this, new LagerObjectClickedEventArgs(item));
			}
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			this.RaiseLagerObjectClicked(indexPath.Row);
			tableView.DeselectRow(indexPath, true);
		}
	}
}

