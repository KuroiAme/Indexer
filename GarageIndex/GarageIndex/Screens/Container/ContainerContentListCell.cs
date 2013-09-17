using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace GarageIndex
{
	public class ContainerContentListCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("ContainerContentListCell");

		public ContainerContentListCell () : base (UITableViewCellStyle.Value1, Key)
		{
			// TODO: add subviews to the ContentView, set various colors, etc.
			TextLabel.Text = "TextLabel";
		}
	}
}

