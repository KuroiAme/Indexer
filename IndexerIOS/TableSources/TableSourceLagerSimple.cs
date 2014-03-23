using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using no.dctapps.Garageindex.model;
using no.dctapps.Garageindex.events;


namespace no.dctapps.Garageindex.screens
{
	public class TableSourceLagerSimple : UITableViewSource
	{
		IList<Lager> tableItems;
		string cellIdentifier = "TableCell";

		public event EventHandler<LagerClickedEventArgs> LagerClicked;

		public TableSourceLagerSimple (IList<Lager> items)
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
			Lager o = tableItems [indexPath.Row];
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
			DCTCell cell = tableView.DequeueReusableCell (cellIdentifier) as DCTCell;



			if (cell == null)
				cell = new DCTCell ((MonoTouch.Foundation.NSString)o.Name);
//			cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
//			Xamarin.Themes.BlackLeatherTheme.Apply (cell);

//			if (exists)
//				cell.UpdateCell (o.Name, image, o.Description);
//			else
			cell.UpdateCell (o.Name, o.address);

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
			var handler = this.LagerClicked;
			var item = this.tableItems[row];
			Console.WriteLine("item:"+item.ToString());
			if (handler != null && item != null) {
				handler(this, new LagerClickedEventArgs(item));
			}
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			this.RaiseLagerObjectClicked(indexPath.Row);
			tableView.DeselectRow(indexPath, true);
		}
	}
}

