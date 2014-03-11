using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Drawing;
using IndexerIOS;

namespace GarageIndex
{
	public class HelpScreenInner : UIViewController
	{
		IList<UIImageView> ikons;

		void cleanup ()
		{
			if (ikons != null) {
				foreach (UIImageView image in ikons) {
					image.Dispose ();
				}
			}
		}

		protected override void Dispose (bool disposing)
		{
			ikons = null;
			base.Dispose (disposing);
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			if(this.IsViewLoaded && this.View.Window == null){
				//cleanup ();

			}
			// Release any cached data, images, etc that aren't in use.
		}

		float currentheight;
		const float margin = 10;
		const float imagecube = 100;

		public SizeF GetContentSize ()
		{
			return new SizeF (UIScreen.MainScreen.Bounds.Width, 1600);
		}

		public override void LoadView ()
		{
			base.LoadView ();
			this.View.Frame = new RectangleF (0, 0, UIScreen.MainScreen.Bounds.Width, 1600);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			ikons = new List<UIImageView> ();

			currentheight = 100;

			AddHeaders ();
			AddIconAndDescription (Flosshatt.MakeFlosshatt(),AppDelegate.its.getTranslatedText("Small items that fit in a container"));
			AddIconAndDescription (GalleryIcon.MakeGallery (), AppDelegate.its.getTranslatedText ("Picture Gallery"));
			AddIconAndDescription (Eye.MakeImage (), AppDelegate.its.getTranslatedText ("QR code scanner"));
			AddIconAndDescription (PreferencesIcon.MakeImage(),AppDelegate.its.getTranslatedText ("Settings"));
			AddIconAndDescription (TableIcon.MakeImage(),AppDelegate.its.getTranslatedText("Large items too big for a container"));
			AddIconAndDescription (ContainerIcon.MakeImage (), AppDelegate.its.getTranslatedText ("Container for items"));
			AddIconAndDescription (MansionIcon.MakeImage (), AppDelegate.its.getTranslatedText ("Locations"));
			AddIconAndDescription (backarrow.MakeBackArrow(), AppDelegate.its.getTranslatedText ("Back one screen"));
			AddIconAndDescription (Xmark.MakeImage(), AppDelegate.its.getTranslatedText ("Back to the dashboard"));
			AddIconAndDescription (GarbageBin.MakeImage (), AppDelegate.its.getTranslatedText ("Delete"));


			AddWhatToDo ();

		}

		void AddHeaders ()
		{
			UILabel hlap = new UILabel (new RectangleF (10, currentheight, UIScreen.MainScreen.Bounds.Width, 22));
			currentheight += 22 + margin;
			hlap.Text = AppDelegate.its.getTranslatedText ("Guide to indexer");
			hlap.TextAlignment = UITextAlignment.Center;
			hlap.AdjustsFontSizeToFitWidth = true;
			View.AddSubview (hlap);

			UILabel subheading = new UILabel (new RectangleF (10, currentheight, UIScreen.MainScreen.Bounds.Width / 2, 22));
			currentheight += 22 +margin;
			hlap.Text = AppDelegate.its.getTranslatedText ("Icons used in indexer");
			hlap.TextAlignment = UITextAlignment.Center;
			hlap.AdjustsFontSizeToFitWidth = true;
			View.AddSubview (subheading);
		}

		void AddIconAndDescription (UIImage image, string str)
		{
			UIImageView ikon = new UIImageView(new RectangleF(10,currentheight,96, 64));
			ikon.Image = image;
			ikons.Add (ikon);
			View.Add (ikon);	
		
			UILabel desc = new UILabel (new RectangleF (10 +100 + 10, currentheight, UIScreen.MainScreen.Bounds.Width / 2, 100));
			desc.Text = AppDelegate.its.getTranslatedText (str);
			desc.TextAlignment = UITextAlignment.Center;
			desc.AdjustsFontSizeToFitWidth = true;
			desc.Lines = 3;
			desc.LineBreakMode = UILineBreakMode.WordWrap;
			View.AddSubview (desc);

			currentheight += imagecube +margin;
		}

		void AddWhatToDo ()
		{
			UILabel desc = new UILabel (new RectangleF (10, currentheight, UIScreen.MainScreen.Bounds.Width -20 , 200));
			desc.Text = AppDelegate.its.getTranslatedText ("Take photographs with your device camera and import them to the gallery in the app. Zoom into the object you would like to be tagged just like faces on facebook. Doubletap to tag. To inspect each tag press long with your finger on the tag. Here you can edit the tag or choose to extract it to a more detailed item, container or large item.");
			desc.Lines = 15;
			desc.LineBreakMode = UILineBreakMode.WordWrap;
			desc.AdjustsFontSizeToFitWidth = true;
			View.AddSubview (desc);

			currentheight += imagecube +margin;
		}
	}
}

