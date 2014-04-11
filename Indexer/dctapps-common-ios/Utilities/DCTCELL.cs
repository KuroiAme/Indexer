using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace no.dctapps.commons
{
	public class DCTCell : UITableViewCell  {
		UILabel headingLabel;
		UILabel subheadingLabel;
		UIImageView imageView;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public DCTCell (NSString cellId) : base (UITableViewCellStyle.Subtitle, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;
			imageView = new UIImageView();
			headingLabel = new UILabel () {
				Font = UIFont.FromName("Cochin-BoldItalic", 22f),
				TextColor = UIColor.FromRGB (127, 51, 0),
				BackgroundColor = UIColor.Clear
			};
			subheadingLabel = new UILabel () {
				Font = UIFont.FromName("AmericanTypewriter", 12f),
				TextColor = UIColor.FromRGB (38, 127, 0),
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.Clear,
			};
			ContentView.Add (headingLabel);
			ContentView.Add (subheadingLabel);
			ContentView.Add (imageView);
		}
		public void UpdateCell (string caption,UIImage image, string subtitle)
		{
			imageView.Image = image;
			headingLabel.Text = caption;
			subheadingLabel.Text = subtitle;
		}

		public void UpdateCell (string caption, string subtitle)
		{
			headingLabel.Text = caption;
			subheadingLabel.Text = subtitle;
		}
		public override void LayoutSubviews ()
		{

			base.LayoutSubviews ();
//
//			foreach (UIView subview in this.Subviews) {
//				Console.WriteLine(subview.GetType().Name);
//				if(subview is UITableViewCellDeleteConfirmationControl_Legacy){
//					Console.WriteLine ("found!");
//					RectangleF newFrame = subview.Frame;
//					newFrame.X = subview.Frame.X - 50;
//					subview.Frame = newFrame;
//				}
//			}

			if (UserInterfaceIdiomIsPhone) {
				imageView.Frame = new RectangleF (ContentView.Bounds.Width - 63, 5, 50, 50);
				headingLabel.Frame = new RectangleF (5, 4, ContentView.Bounds.Width - 63, 25);
				subheadingLabel.Frame = new RectangleF (100, 18, 200, 20);
			} else {
//				imageView.Frame = new RectangleF (ContentView.Bounds.Width - 63, 5, 33, 33);
				imageView.Frame = new RectangleF (ContentView.Bounds.Width - 63, 5, 50, 50);
				headingLabel.Frame = new RectangleF (5, 4, ContentView.Bounds.Width - 63, 25);
				subheadingLabel.Frame = new RectangleF (100, 18, ContentView.Bounds.Width - 63, 20);
			}

		}
	}
}

