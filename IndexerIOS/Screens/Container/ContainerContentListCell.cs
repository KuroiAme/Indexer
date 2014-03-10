using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace GarageIndex
{
	public class ContainerContentListCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("ContainerContentListCell");

//		protected override void Dispose (bool disposing)
//		{
//			Key = null;
//			base.Dispose (disposing);
//		}

		public ContainerContentListCell () : base (UITableViewCellStyle.Value1, Key)
		{
			// TODO: add subviews to the ContentView, set various colors, etc.
			TextLabel.Text = "TextLabel";
		}
	}
}

