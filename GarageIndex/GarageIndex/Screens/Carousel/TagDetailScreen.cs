using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using No.Dctapps.GarageIndex;
using no.dctapps.Garageindex.screens;
using System.IO;
using MonoTouch.CoreGraphics;
using System.Linq;
using no.dctapps.Garageindex.model;
using No.Dctapps.Garageindex.Ios.Screens;

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
			RectangleF frame = new RectangleF (0, 50f, UIScreen.MainScreen.Bounds.Width, 150);
			tlc = new TagListController (tag, frame);
			this.Add (tlc.View);
			CreateExtractBarButton ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		UIBarButtonItem it;
		private void CreateExtractBarButton ()
		{

			it = new UIBarButtonItem ();
			it.Title = "Extract";
			//IS really info

			it.Clicked += (object sender, EventArgs e) => Extract ();
			NavigationItem.SetRightBarButtonItem (it, true);

		}

		UIActionSheet actionSheet;

		string[] ExtractTagImages (ImageTag tag)
		{
			GalleryObject go = AppDelegate.dao.GetGalleryObjectByID (tag.GalleryObjectID);

			var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var gallerydirectory = Path.Combine (documentsDirectory, "gallery");
			string imagefilename = go.imageFileName;
			string path = Path.Combine (gallerydirectory, imagefilename);
			UIImage MasterImage = UIImage.FromFile (path);

			CGImage cg = MasterImage.CGImage;
			CGImage neo = cg.WithImageInRect (tag.FetchAsRectangleF());

			UIImage cutout = UIImage.FromImage (neo);
			//UIImage thumbnail = cutout.Scale (new SizeF(50, 50));
			return SaveCutout (RandomGeneratedName (), cutout);
		}

		static string RandomGeneratedName ()
		{
			var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			var random = new Random();
			var result = new string(
				Enumerable.Repeat(chars, 8)
				.Select(s => s[random.Next(s.Length)])
				.ToArray());
			return result;
		}

		public static string[] SaveCutout (string name, UIImage ourpic)
		{
			if(ourpic == null)
				return new string[2]{"",""};
			Console.WriteLine ("Save");
			SizeF newSize = new SizeF (50, 50);
			UIImage thumbPic = ourpic.Scale (newSize); //measurements taken from CustomCell, alternatly 33x33
			UIImage resImage = ourpic.Scale (new SizeF (ourpic.Size.Width, ourpic.Size.Height));

			if (ourpic != null) {
				var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
				var picname = name + ".png";
				var thumbpicname = name + "_thumb.png";
				string pngfileName = System.IO.Path.Combine (documentsDirectory, picname);
				string thumbpngfileName = System.IO.Path.Combine (documentsDirectory, thumbpicname);
				NSData imgData = resImage.AsPNG ();
				NSData img2Data = thumbPic.AsPNG();

				NSError err = null;
				if (imgData.Save (pngfileName, false, out err)) {
					Console.WriteLine ("saved as " + pngfileName);
				} else {
					Console.WriteLine ("NOT saved as " + pngfileName + " because" + err.LocalizedDescription);
				}

				err = null;
				if (img2Data.Save (thumbpngfileName, false, out err)) {
					Console.WriteLine ("saved as " + thumbpngfileName);
					string[] result = new string[2] {picname,thumbpicname};
					return result;

				} else {
					Console.WriteLine ("NOT saved as " + thumbpngfileName + " because" + err.LocalizedDescription);
					return null;
				}
			}
			return new string[2]{"",""};
		}

		void ExtractSmall ()
		{
			Item item = new Item ();

			item.ImageTagId = tag.ID;
			String[] res = ExtractTagImages (tag);
			item.ThumbFileName = res [1];
			item.ImageFileName = res [0];
			ItemDetailScreen ids = new ItemDetailScreen (item);
			this.NavigationController.PushViewController (ids,true);
		}

		void ExtractContainer ()
		{
			LagerObject lo = new LagerObject ();
			lo.ImageTagId = tag.ID;
			string[] res = ExtractTagImages (tag);
			lo.thumbFileName = res [1];
			lo.imageFileName = res [0];
			lo.isContainer = "true";
			ContainerDetails cd = new ContainerDetails (lo);
			this.NavigationController.PushViewController (cd, true);
		}

		void ExtractLarge ()
		{
			LagerObject lo = new LagerObject ();
			lo.ImageTagId = tag.ID;
			string[] res = ExtractTagImages (tag);
			lo.thumbFileName = res [1];
			lo.imageFileName = res [0];
			lo.isContainer = "false";
			lo.isLargeObject = "true";
			BigItemDetailScreen bids = new BigItemDetailScreen (lo);
			this.NavigationController.PushViewController (bids, true);
		}

		public void Extract(){
			var source = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("What type of object is it?", "What type of object is it?");
			var myCancel = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Cancel", "Cancel");
			var mySmall = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("small", "small");
			var myContainer = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("container", "container");
			var myLarge = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("large object", "large object");
			actionSheet = new UIActionSheet(source);
			actionSheet.AddButton(myCancel);
			actionSheet.AddButton(mySmall);
			actionSheet.AddButton(myContainer);
			actionSheet.AddButton (myLarge);
			//			actionSheet.CancelButtonIndex = 0;

			actionSheet.Clicked += delegate(object sender, UIButtonEventArgs e2) {
				if(e2.ButtonIndex == 0){
					//DO nothing
				}else if(e2.ButtonIndex == 1){
					ExtractSmall();
				}else if(e2.ButtonIndex == 2){
					ExtractContainer();
				}else if(e2.ButtonIndex == 3){
					ExtractLarge();
				}
			};
			actionSheet.ShowInView (View);
		}
	}
}

