using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace GarageIndex
{
	public partial class TagDetailScreen : UIViewController
	{
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}
		ImageTag tag;
		TagListController tlc;

		public TagDetailScreen (ImageTag tag)
		{
			this.tag = tag;
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			RectangleF frame = new RectangleF (0, y, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height * 0.75f);
			tlc = new TagListController (tag, frame);
			this.Add (tlc.View);
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		UIBarButtonItem it;
		private void CreateEditBarButton ()
		{

			it = new UIBarButtonItem ();
			it.Title = "Extract";
			//IS really info

			it.Clicked += (object sender, EventArgs e) => Extract ();
			NavigationItem.SetRightBarButtonItem (it, true);

		}

		void Extract ()
		{
			throw new NotImplementedException ();
		}
	}
}

