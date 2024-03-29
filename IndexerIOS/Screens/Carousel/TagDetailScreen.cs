﻿using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using No.Dctapps.GarageIndex;
using no.dctapps.commons.events.screens;
using System.IO;
using MonoTouch.CoreGraphics;
using System.Linq;
using no.dctapps.commons.events.model;
using No.Dctapps.Garageindex.Ios.Screens;
using GoogleAnalytics.iOS;
using System.Collections.Generic;
using no.dctapps.commons;
using System.Text;

namespace no.dctapps.commons.events
{
	public partial class TagDetailScreen : UIViewController
	{
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}
		ImageTag tag;
		TagListController tlc;

		RectangleF fetcher;

		//public event EventHandler<BackClickedEventArgs> backpush;
		EditImageModeController ancestor{ get; set;}

		public TagDetailScreen (ImageTag tag, EditImageModeController ancestor) : base (UserInterfaceIdiomIsPhone ? "TagDetailScreen_iPhone" : "TagDetailScreen_iPad",null)
		{
			this.tag = tag;
			this.ancestor = ancestor;
			Console.WriteLine ("nib:"+this.NibName);
		}

		public TagDetailScreen (ImageTag tag) : base (UserInterfaceIdiomIsPhone ? "TagDetailScreen_iPhone" : "TagDetailScreen_iPad",null)
		{
			this.tag = tag;
		}

		public TagDetailScreen () : base (UserInterfaceIdiomIsPhone ? "TagDetailScreen_iPhone" : "TagDetailScreen_iPad", null){
			//ipad constructor
			Console.WriteLine ("nib:"+this.NibName);
		}

		protected override void Dispose (bool disposing)
		{
			tag = null;
			tlc = null;
			//backpush = null;
			extractButton.Dispose ();
			ExtractPressed = null;
			ancestor = null;
			base.Dispose (disposing);
		}

		/// <summary>
		/// Release everything not in use
		/// </summary>
		void cleanup ()
		{
			//Dispose ();
		}


		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			//cleanup only if view is loaded and not in a window.
			if(this.IsViewLoaded && this.View.Window == null){
				//cleanup ();
			}
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Tag Detail screen");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}

		UIBarButtonItem backOne;

		UIBarButtonItem backAll;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			RectangleF frame;

			Background back = new Background ();
			View.AddSubview (back.View);
			View.SendSubviewToBack (back.View);

			if (UserInterfaceIdiomIsPhone) {
				frame = new RectangleF (10, 110, UIScreen.MainScreen.Bounds.Width - 20, 125);
			} else {
				frame = new RectangleF (10, 450, UIScreen.MainScreen.Bounds.Width - 20, 125);
			}
			tlc = new TagListController (tag, frame);
			this.Add (tlc.View);
			backOne = new UIBarButtonItem (backarrow.MakeBackArrow (), UIBarButtonItemStyle.Plain, null);


			backAll = new UIBarButtonItem (Xmark.MakeImage (), UIBarButtonItemStyle.Plain, null);

			backOne.Clicked += (object sender, EventArgs e) => {
				if(ancestor != null){
					ancestor.ReleaseLock();
				}
				this.NavigationController.PopViewControllerAnimated (true);
			};
			backAll.Clicked += (object sender, EventArgs e) => {
				if(ancestor != null){
					ancestor.ReleaseLock();
				}
				this.NavigationController.PopToRootViewController (true);
			};


			UIBarButtonItem[] leftbuttons = { backOne, backAll };

			this.NavigationItem.SetLeftBarButtonItems (leftbuttons, true);


			this.ShowDetails (tag);
			CreateMenuOptions ();

//			CreateSlideDownMenu ();

//			UIButton backbutton = new UIButton(new RectangleF(10,25,48,32));
//			backbutton.SetImage (backarrow.MakeBackArrow(), UIControlState.Normal);
//			backbutton.TouchUpInside += (object sender, EventArgs e) => {
//				var handler = this.backpush;
//				if (handler != null) {
//					handler (this, new BackClickedEventArgs ());
//				}
//				DismissViewControllerAsync (true);
//			}; 
//			Add (backbutton);


		}

		UIBarButtonItem extractButton;

		public event EventHandler ExtractPressed;

		//EditTags tags;

		void CreateMenuOptions ()
		{
			List<UIBarButtonItem> buttons = new List<UIBarButtonItem> ();

			extractButton = new UIBarButtonItem (ScissorsIcon.MakeImage(), UIBarButtonItemStyle.Bordered, null);
			extractButton.Clicked += (object sender, EventArgs e) => {
				Console.WriteLine("trying to extract");
				Extract ();
			};

			buttons.Add (extractButton);

			this.NavigationItem.SetRightBarButtonItems (buttons.ToArray(), true);
		}
			

		void ShowDetails (ImageTag mytag)
		{
			this.tag = mytag;
			TagUtility tu = new TagUtility (tag);
			fetcher = tu.FetchAsRectangleF ();
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
					tu.StoreRectangleF(fetcher);
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
					tu.StoreRectangleF(fetcher);
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
					tu.StoreRectangleF(fetcher);
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
					tu.StoreRectangleF(fetcher);
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
			TagUtility tu = new TagUtility (tag);
			CGImage neo = cg.WithImageInRect (tu.FetchAsRectangleF());

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

		char[] sep = {' ', ','};

		string Recombine ()
		{
			StringBuilder sb = new StringBuilder ();
			string[] fromTag = tag.TagString.Split (sep);
			foreach (string s in fromTag) {
				sb.Append (s);
				sb.Append (" ");
			}
			return sb.ToString ();
		}

		void ExtractSmall ()
		{
			Item item = new Item ();
			String recomb = Recombine ();
			item.Name = recomb;
			item.Description = recomb;

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
			String recomb = Recombine ();
			lo.Name = recomb;
			lo.Description = recomb;
			lo.type = recomb;

			lo.ImageTagId = tag.ID;
			string[] res = ExtractTagImages (tag);
			lo.thumbFileName = res [1];
			lo.imageFileName = res [0];
			lo.isContainer = "true";
			lo.isLargeObject = "false";
			ContainerDetails cd = new ContainerDetails (lo);
			this.NavigationController.PushViewController (cd, true);
		}

		void ExtractLarge ()
		{
			LagerObject lo = new LagerObject ();

			String recomb = Recombine ();
			lo.Name = recomb;
			lo.Description = recomb;

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

