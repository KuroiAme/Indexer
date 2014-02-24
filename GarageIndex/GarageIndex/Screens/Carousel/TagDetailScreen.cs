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
using GoogleAnalytics.iOS;
using SlideDownMenu;
using System.Collections.Generic;

namespace GarageIndex
{
	public partial class TagDetailScreen : UIViewController
	{
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}
		ImageTag tag;
		TagListController tlc;

		RectangleF fetcher;

		public event EventHandler<BackClickedEventArgs> backpush;

		public TagDetailScreen (ImageTag tag) : base (UserInterfaceIdiomIsPhone ? "TagDetailScreen_iPhone" : "TagDetailScreen_iPad",null)
		{
			this.tag = tag;
			Console.WriteLine ("nib:"+this.NibName);
		}

		public TagDetailScreen () : base (UserInterfaceIdiomIsPhone ? "TagDetailScreen_iPhone" : "TagDetailScreen_iPad", null){
			//ipad constructor
			Console.WriteLine ("nib:"+this.NibName);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Tag Detail screen");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}

		private void backpress(object sender, EventArgs e){
			var handler = this.backpush;
			if(handler != null){
				handler(this, new BackClickedEventArgs());
			}
			this.NavigationController.PopViewControllerAnimated (true);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			RectangleF frame;

			var imgView = new UIImageView(BlueSea.MakeBlueSea()){
				ContentMode = UIViewContentMode.ScaleToFill,
				AutoresizingMask = UIViewAutoresizing.All,
				Frame = View.Bounds
			};

			View.AddSubview (imgView);
			View.SendSubviewToBack (imgView);

			if (UserInterfaceIdiomIsPhone) {
				frame = new RectangleF (0, 125, UIScreen.MainScreen.Bounds.Width, 125);
			} else {
				frame = new RectangleF (0, 200, UIScreen.MainScreen.Bounds.Width, 125);
			}
			tlc = new TagListController (tag, frame);
			this.Add (tlc.View);
			//CreateExtractBarButton ();

			//UIBarButtonItem back = new UIBarButtonItem ("back", UIBarButtonItemStyle.Bordered,backpress);
			//this.NavigationItem.LeftBarButtonItem = back;


			this.ShowDetails (tag);

			CreateSlideDownMenu ();

			UIButton backbutton = new UIButton(new RectangleF(10,25,48,32));
			backbutton.SetImage (backarrow.MakeBackArrow(), UIControlState.Normal);
			backbutton.TouchUpInside += (object sender, EventArgs e) => {
				var handler = this.backpush;
				if (handler != null) {
					handler (this, new BackClickedEventArgs ());
				}
				DismissViewControllerAsync (true);
			}; 
			Add (backbutton);


		}

		void CreateSlideDownMenu ()
		{
			var item0 = new MenuItem ("Options", UIImage.FromBundle ("frames4832.png"), (menuItem) => {
				Console.WriteLine("Item: {0}", menuItem);
			});
			item0.Tag = 0;
			var extract = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Extract", "Extract");
			var item1 = new MenuItem (extract, UIImage.FromBundle ("startree.png"), (menuItem) => {
				Console.WriteLine("Item: {0}", menuItem);
				Extract ();
			});
			item1.Tag = 1;
//			var item2 = new MenuItem ("Edit Tags", UIImage.FromBundle ("frames4832.png"), (menuItem) => {
//				Console.WriteLine("Item: {0}", menuItem);
//				EditTags tags = new EditTags(this
//			});
//			item2.Tag = 2;


			//item0.tag = 0;

			var slideMenu = new SlideMenu (new List<MenuItem> { item0, item1});
			slideMenu.Center = new PointF (slideMenu.Center.X, slideMenu.Center.Y + 25);
			this.View.AddSubview (slideMenu);
		}

		void ShowDetails (ImageTag mytag)
		{
			fetcher = mytag.FetchAsRectangleF ();
			string tagText = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Tag", "Tag");
			this.tagIdLabel.Text = tagText + ":"+mytag.ID;

			this.xTextField.Text = fetcher.X.ToString();
			this.yTextField.Text = fetcher.Y.ToString ();
			this.WidthLabel.Text = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Width", "Width");
			this.Label_Height.Text = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Height", "Height");
			this.TextField_height.Text = fetcher.Height.ToString ();
			this.WidthTextField.Text = fetcher.Width.ToString ();

			this.TextField_height.ValueChanged += (object sender, EventArgs e) => {
				try{
					float height = float.Parse(this.TextField_height.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
					fetcher.Height = height;
					mytag.StoreRectangleF(fetcher);
					AppDelegate.dao.SaveTag(mytag);
				}catch(Exception ex){
					Console.WriteLine("exception happend, defaulting value:"+ex.ToString());
					this.Label_Height.Text = fetcher.Height.ToString();
				}
			};

			this.WidthTextField.ValueChanged += (object sender, EventArgs e) => {
				try{
					float width = float.Parse(this.WidthTextField.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
					fetcher.Width = width;
					mytag.StoreRectangleF(fetcher);
					AppDelegate.dao.SaveTag(mytag);
				}catch(Exception ex){
					Console.WriteLine("exception happend, defaulting value:"+ex.ToString());
					this.WidthTextField.Text = fetcher.Width.ToString();
				}
			};

			this.xTextField.ValueChanged += (object sender, EventArgs e) => {
				try{
					float x = float.Parse(this.xTextField.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
					fetcher.X = x;
					mytag.StoreRectangleF(fetcher);
					AppDelegate.dao.SaveTag(mytag);
				}catch(Exception ex){
					Console.WriteLine("exception happend, defaulting value:"+ex.ToString());
					this.xTextField.Text = fetcher.X.ToString();
				}
			};

			this.yTextField.ValueChanged += (object sender, EventArgs e) => {
				try{
					float y = float.Parse(this.yTextField.Text.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
					fetcher.Y = y;
					mytag.StoreRectangleF(fetcher);
					AppDelegate.dao.SaveTag(mytag);
				}catch(Exception ex){
					Console.WriteLine("exception happend, defaulting value:"+ex.ToString());
					this.yTextField.Text = fetcher.Y.ToString();
				}
			};

			this.xTextField.ShouldReturn += textField => {
				textField.ResignFirstResponder();
				return true;
			};
			this.yTextField.ShouldReturn += textField => {
				textField.ResignFirstResponder();
				return true;
			};

			this.TextField_height.ShouldReturn += textField => {
				textField.ResignFirstResponder();
				return true;
			};
			this.WidthTextField.ShouldReturn += textField => {
				textField.ResignFirstResponder();
				return true;
			};

		}


		UIBarButtonItem it;
		private void CreateExtractBarButton ()
		{

			it = new UIBarButtonItem ();
			var text = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Extract", "Extract");
			it.Title = text;

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

			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.Event, "Extract image");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateEvent ("userAction", "extract image", "", 0).Build ());

			var source = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("What type of object is it?", "What type of object is it?");
			var myCancel = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Cancel", "Cancel");
			var mySmall = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("small", "small");
			var myContainer = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("container", "container");
			var myLarge = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("large objects", "large objects");
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

