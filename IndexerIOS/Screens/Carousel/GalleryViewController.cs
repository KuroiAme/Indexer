﻿using System;
using MonoTouch.UIKit;
using Alliance.Carousel;
using System.Drawing;
using System.Collections.Generic;
using no.dctapps.commons.events.events;
using MonoTouch.Foundation;
using System.IO;
using System.Linq;
using GoogleAnalytics.iOS;
using SatelliteMenu;
using No.DCTapps.GarageIndex;
using no.dctapps.commons.events.screens;
using no.dctapps.commons.events.model;
using no.dctapps.garageindex;
using no.dctapps.commons.events;
using no.dctapps.commons;

namespace no.dctapps.commons.events
{
	public class GalleryViewController : UtilityViewController
	{
		CarouselView carousel;
		UIImagePickerController imagePicker;
		public UIPopoverController Pc;
		//LoadingOverlay loadingOverlay;

		Lager ActiveLocation;

		LagerObject ActiveContainer;

		Boolean activeSet = false;
		string ActiveType;

		public event EventHandler Clear;

		public GalleryViewController ()
		{
			activeSet = false;
		}

		public GalleryViewController (Lager myLager)
		{
			ActiveLocation = myLager;
			activeSet = true;
			ActiveType = "Lager";
		}

		public GalleryViewController (LagerObject Container)
		{
			ActiveContainer = Container;
			activeSet = true;
			ActiveType = "Container";
		}

		protected override void Dispose (bool disposing)
		{

			carousel.Dispose ();
			SetActivePressed = null;
			DelImagePressed = null;
			Clear = null;
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

//		void Tapped (UITapGestureRecognizer gestureRecognizer)
//		{
//			Console.WriteLine ("tapped");
//			RaiseClear ();
//		}

		public IList<GalleryObject> items;

		public void ChangeThumb ()
		{
			carousel.ReloadData ();
		}

		//		public void setActiveLocation (Lager myLager)
//		{
//			string type = AppDelegate.key.GetActiveGalleryType ();
//			int id = AppDelegate.key.GetActiveGalleryID ();
//		
////			return AppDelegate.dao.GetAllGalleryObjects ();
//
//		}

		IList<GalleryObject> GetActiveGalleryItems ()
		{
			Console.WriteLine ("Fetching active gallery items");
			if (activeSet) {
				Console.WriteLine ("Active is Set");
				if (ActiveType == "Lager") {
//					if(!string.IsNullOrWhiteSpace(ActiveLocation.Name))
						ActiveText.Text = ActiveLocation.Name;
					return AppDelegate.dao.GetAllGalleryObjectsByTypeAndID (ActiveType, ActiveLocation.ID);
				} else if (ActiveType == "Container") {
//					if(!string.IsNullOrWhiteSpace(ActiveContainer.Name))
						ActiveText.Text = ActiveContainer.Name;
					return AppDelegate.dao.GetAllGalleryObjectsByTypeAndID (ActiveType, ActiveContainer.ID);
				} else {
					return new List<GalleryObject> ();
				}
			} else {
				string type = AppDelegate.key.GetActiveGalleryType ();
				int id = AppDelegate.key.GetActiveGalleryID ();
				Console.WriteLine ("type:" + type + ",id:" + id);
				if (string.IsNullOrEmpty (type)) {
					type = "ALL";
					ActiveText.Text = "ALL";
				}
				if (type != "ALL") {
					if (type == "Lager") {
						Lager lag = AppDelegate.dao.GetLagerById (id);
						if(lag != null)
							ActiveText.Text = lag.Name;
					} else if (type == "Container") {
						LagerObject lo = AppDelegate.dao.GetLagerObjectByID (id);
						if(lo != null)
							ActiveText.Text = lo.Name;
					}
					return AppDelegate.dao.GetAllGalleryObjectsByTypeAndID (type, id);
				} else {
					ActiveText.Text = "ALL";
					return AppDelegate.dao.GetAllGalleryObjects ();
				}
			}
		}

//		UITapGestureRecognizer tap;

		GalleryDelegate gd;

		IndexerSateliteMenu menu;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			InitActiveField ();

			items = GetActiveGalleryItems ();



			Console.WriteLine (items.Count);

			var imgView = new UIImageView (BlueSea.MakeBlueSea ()) {
				ContentMode = UIViewContentMode.ScaleToFill,
				AutoresizingMask = UIViewAutoresizing.All,
				Frame = View.Bounds
			};
			View.AddSubview (imgView);
			View.SendSubviewToBack (imgView);

			carousel = new CarouselView (UIScreen.MainScreen.Bounds);
			//carousel. = images.Count;
			carousel.DataSource = new GalleryDataSource (this);
			gd = new GalleryDelegate (this);
			carousel.Delegate = gd;
			carousel.CarouselType = CarouselType.CoverFlow;
			carousel.ConfigureView ();
			View.AddSubview (carousel);

//
//			tap = new UITapGestureRecognizer (Tapped);
//			tap.NumberOfTapsRequired = 1;
//			carousel.AddGestureRecognizer (tap);

			View.AddSubview (carousel);
			//CreateAddBarButton ();
			//CreateDeleteBarButton ();

			//carousel.CurrentItemIndex

			//InitSateliteMenu ();


			menu = new IndexerSateliteMenu ("Gallery", this);
			View.Add (menu.View);

			CreateOptions ();


			View.BringSubviewToFront (ActiveText);
		}

		public void Open (int index)
		{
			GalleryObject go = items [index];
			EditImageModeController eimc = new EditImageModeController (go, this);
			this.NavigationController.PushViewController (eimc, true);
		}

		UILabel ActiveText;
		int activeID;

		void InitActiveField ()
		{
			RectangleF activeRect = new RectangleF (0, 66, View.Bounds.Width, 40);
			ActiveText = new UILabel (activeRect);
			ActiveText.Font = UIFont.FromName ("Helvetica", 24);
			ActiveText.ShadowColor = UIColor.Gray;
			ActiveText.ShadowOffset = new SizeF (1.0f, 0.2f);
			ActiveText.TextColor = UIColor.Black;
			ActiveText.TextAlignment = UITextAlignment.Center;

			if (!activeSet) {

				activeID = AppDelegate.key.GetActiveGalleryID ();

				if (ActiveType == null) {
					ActiveType = AppDelegate.key.GetActiveGalleryType ();
				}

				if (ActiveType == null) {
					ActiveType = "ALL";
				}

				if (ActiveType.Equals ("Lager")) {
					ActiveContainer = null;
					ActiveLocation = AppDelegate.dao.GetLagerById (activeID);
					if (ActiveLocation != null && ActiveLocation.Name != null) {
						ActiveText.Text = ActiveLocation.Name;
					}
				}


				if (ActiveType.Equals ("Container")) {
					ActiveLocation = null;
					ActiveContainer = AppDelegate.dao.GetContainerById (activeID);
					ActiveText.Text = ActiveContainer.Name;
				}

				if (ActiveType == "ALL") {
					ActiveLocation = null;
					ActiveContainer = null;
					ActiveText.Text = "ALL";
				}

				View.AddSubview(ActiveText);

			}
		}

		//private event EventHandler AddImagePressed;

		UIBarButtonItem addImageButton;
		UIBarButtonItem delImageButton;

		private event EventHandler DelImagePressed;

		private event EventHandler SetActivePressed;

		UIBarButtonItem setActiveButton;

		private void CreateOptions ()
		{
			List<UIBarButtonItem> buttons = new List<UIBarButtonItem> ();
			addImageButton = new UIBarButtonItem (UIBarButtonSystemItem.Add, null);
			addImageButton.Clicked += (object sender, EventArgs e) => SelectSource();
			//AddImagePressed += (object sender, EventArgs e) => SelectSource ();
			buttons.Add (addImageButton);

			delImageButton = new UIBarButtonItem (GarbageBin.MakeImage(), UIBarButtonItemStyle.Plain, this.DelImagePressed);
			//DelImagePressed += (object sender, EventArgs e) => ReallyDelete ();
			delImageButton.Clicked += (object sender, EventArgs e) => ReallyDelete ();
			buttons.Add (delImageButton);

			setActiveButton = new UIBarButtonItem (SetActiveNavbarIcon.MakeImage(), UIBarButtonItemStyle.Plain, this.SetActivePressed);
			setActiveButton.Clicked += (object sender, EventArgs e) => SetActive ();
			//SetActivePressed += (object sender, EventArgs e) => SetActive ();
			buttons.Add (setActiveButton);

			this.NavigationItem.SetRightBarButtonItems (buttons.ToArray(), true);

		}

		public void SetActive ()
		{
			UIActionSheet activeSheet = new UIActionSheet (AppDelegate.its.getTranslatedText("Set active to what?"));
			activeSheet.AddButton ("Cancel");
			activeSheet.AddButton ("Container");
			activeSheet.AddButton ("Location");
			activeSheet.AddButton ("All");
			activeSheet.Clicked += (object sender, UIButtonEventArgs e) => {
				if (e.ButtonIndex == 0) {
					Console.WriteLine ("Cancel");
				}
				if (e.ButtonIndex == 1) {
					//select container
					Console.WriteLine ("a container");
					SelectContainer sc = new SelectContainer ();
					this.NavigationController.PushViewController(sc,true);
					sc.DismissEvent += (object sender2, ContainerClickedEventArgs e2) => {
						//active.BackgroundColor = UIColor.White;
						ActiveText.Text = e2.container.Name;
						AppDelegate.key.StoreActiveGallery (e2.container);
						this.items = AppDelegate.dao.GetAllGalleryObjectsByTypeAndID("Container",e2.container.ID); //TODO check if type needs to be "LAGEROBJECT"
						carousel.ReloadData();
						sc.NavigationController.PopViewControllerAnimated(true);
					};
				}
				if (e.ButtonIndex == 2) {
					//select Lager
					Console.WriteLine ("a location");
					SelectLager sl = new SelectLager();
					this.NavigationController.PushViewController(sl,true);
					sl.DismissEvent += (object sender2, LagerClickedEventArgs e2) => {
						ActiveText.Text = e2.Lager.Name;
						AppDelegate.key.StoreActiveGallery (e2.Lager);
						this.items = AppDelegate.dao.GetAllGalleryObjectsByTypeAndID("Lager",e2.Lager.ID);
						carousel.ReloadData();
						sl.NavigationController.PopViewControllerAnimated(true);
					};
				}
				if (e.ButtonIndex == 3) {
					Console.WriteLine ("ALL");
					ActiveText.Text = "ALL";
					AppDelegate.key.StoreActiveGalleryType("ALL");
					this.items = AppDelegate.dao.GetAllGalleryObjects ();
					carousel.ReloadData ();
			
				}
			
			};
			activeSheet.ShowInView (this.View);
		}


		void RaiseClear ()
		{
			var handler = this.Clear;
			if (handler != null) {
				handler (this, new EventArgs ());
			}
		}
			
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Gallery");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}

		UIBarButtonItem it;

		private void CreateAddBarButton ()
		{

			it = new UIBarButtonItem ();
			var addtext = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Add", "Add");
			it.Title = addtext;
			it.Clicked += (object sender, EventArgs e) => SelectSource ();
			this.NavigationItem.SetRightBarButtonItem (it, true);
			
		}

		UIBarButtonItem it2;

		private void CreateDeleteBarButton ()
		{

			it2 = new UIBarButtonItem ();
			var deltext = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Delete", "Delete");
			it2.Title = deltext;
			it2.Clicked += (object sender, EventArgs e) => ReallyDelete ();
			this.NavigationItem.SetLeftBarButtonItem (it2, true);

		}

		UIActionSheet delsheet;

		private void ReallyDelete ()
		{
			var really = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Really Delete current image?", "Really Delete current image?");
			var myCancel = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Cancel", "Cancel");
			var OK = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("OK", "OK");
			delsheet = new UIActionSheet (really);
			delsheet.AddButton (myCancel);
			delsheet.AddButton (OK);

			delsheet.Clicked += delegate(object sender, UIButtonEventArgs e2) {
				if (e2.ButtonIndex == 0) {
					Console.WriteLine ("deletion canceled");
				} else if (e2.ButtonIndex == 1) {
					Console.WriteLine ("Deletion OK");
					DeleteCurrentPic ();
				} else {
					Console.WriteLine ("Unknown button");
				}
			};
			delsheet.ShowInView (View);
		}

		void DeleteCurrentPic ()
		{
			int currentindex = carousel.CurrentItemIndex;
			Console.WriteLine ("currentindex:" + currentindex);
			GalleryObject del = items [currentindex];
			items.RemoveAt (currentindex);
			AppDelegate.dao.DeleteGalleryObject (del);
			carousel.ReloadData ();
		}

		UIActionSheet actionSheet;

		public void SelectSource ()
		{
			var source = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("add image from where?", "add image from where?");
			var myCancel = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Cancel", "Cancel");
			var myCamera = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Camera", "Camera");
			var myLibrary = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Photo Library", "Photo Library");
			actionSheet = new UIActionSheet (source);
			actionSheet.AddButton (myCancel);
			actionSheet.AddButton (myCamera);
			actionSheet.AddButton (myLibrary);
			//			actionSheet.CancelButtonIndex = 0;

			actionSheet.Clicked += delegate(object sender, UIButtonEventArgs e2) {
				if (e2.ButtonIndex == 0) {
					//DO nothing
				} else if (e2.ButtonIndex == 1) {
					PickFromCamera ();
				} else {
					PickFromLibrary ();
				}
			};
			actionSheet.ShowInView (View);
		}

		public void PickFromCamera ()
		{
			// create a new picker controller

			// set our source to the photo library
			imagePicker = new UIImagePickerController ();
			imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;
			// set what media types
			imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.Camera);
			imagePicker.FinishedPickingMedia += HandleFinishedPickingMedia;
			imagePicker.Canceled += Handle_Canceled;
			// show the picker
			PresentViewController (imagePicker, true,null);
//			if(UserInterfaceIdiomIsPhone){
//				NavigationController.PresentViewController (imagePicker, true, delegate {});
//			}else{
//				Console.WriteLine("Popover");
//				Pc = new UIPopoverController(imagePicker);
//				Pc.PresentFromBarButtonItem(it, UIPopoverArrowDirection.Up, true);
//			}
		}

		public void PickFromLibrary ()
		{
			imagePicker = new UIImagePickerController ();
			imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			// set what media types
			imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.PhotoLibrary);
			extractImage ();
		}

		void extractImage ()
		{
			imagePicker.FinishedPickingMedia += HandleFinishedPickingMedia;
			imagePicker.Canceled += Handle_Canceled;
			PresentViewController (imagePicker, true,null);
			// show the picker
//			if (UserInterfaceIdiomIsPhone) {
//				NavigationController.PresentViewController (imagePicker, true, delegate{});
//			}
//			else {
//				Console.WriteLine ("Popover");
//				Pc = new UIPopoverController (imagePicker);
//				Pc.PresentFromBarButtonItem (it, UIPopoverArrowDirection.Up, true);
//			}
		}
		// Do something when the
		void Handle_Canceled (object sender, EventArgs e)
		{
			Console.WriteLine ("picker cancelled");
			imagePicker.DismissViewController (true, delegate {
			});
			imagePicker = null;
		}
		// This is a sample method that handles the FinishedPickingMediaEvent
		protected void HandleFinishedPickingMedia (object sender, UIImagePickerMediaPickedEventArgs e)
		{
			// determine what was selected, video or image
			bool isImage = false;
			switch (e.Info [UIImagePickerController.MediaType].ToString ()) {
			case "public.image":
				Console.WriteLine ("Image selected");
				isImage = true;
				break;

			case "public.video":
				Console.WriteLine ("Video selected");
				break;
			}

			//			Console.Write("Reference URL: [" + UIImagePickerController.ReferenceUrl + "]");

			// get common info (shared between images and video)
			//			NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
			//			if (referenceURL != null) 
			//				Console.WriteLine(referenceURL.ToString ());

			// if it was an image, get the other image info
			if (isImage) {

				// get the original image
				UIImage originalImage = e.Info [UIImagePickerController.OriginalImage] as UIImage;
				if (originalImage != null) {
					// do something with the image
					//					Console.WriteLine ("got the original image");
					//					imageView.Image = originalImage;
					//					OutputImage = originalImage;
					RaiseImageGotten (originalImage);
				} else {

					// get the edited image
					UIImage editedImage = e.Info [UIImagePickerController.EditedImage] as UIImage;
					if (editedImage != null) {
						// do something with the image
						//						Console.WriteLine ("got the edited image");
						//						imageView.Image = editedImage;
						//					OutputImage = editedImage;
						RaiseImageGotten (editedImage);
					}
				}

				//				//- get the image metadata
				//				NSDictionary imageMetadata = e.Info[UIImagePickerController.MediaMetadata] as NSDictionary;
				//				if(imageMetadata != null) {
				//					// do something with the metadata
				//					Console.WriteLine ("got image metadata");
				//				}

			}
			// if it's a video
			//			else {
			//				// get video url
			//				NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
			//				if(mediaURL != null) {
			//					//
			//					Console.WriteLine(mediaURL.ToString());
			//				}
			//			}
			//			


//			if (UserInterfaceIdiomIsPhone) {
				imagePicker.DismissViewController (true, delegate {

				});
				imagePicker = null;
//			} else {
//				Pc.Dismiss (false);
//			}
		}

//		public static bool UserInterfaceIdiomIsPhone {
//			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
//		}

		void RaiseImageGotten (UIImage image)
		{
			//loadingOverlay = new LoadingOverlay (UIScreen.MainScreen.Bounds);
			//.Add (loadingOverlay);
			//View.BringSubviewToFront (loadingOverlay);
			mySavePicture (image); //local event
			//loadingOverlay.Hide ();

		}

		private void mySavePicture (UIImage image)
		{


			Console.WriteLine ("mySavePicture()");
			string name = RandomGeneratedName ();
			string[] names = SaveGalleryImage (name, image);
			GalleryObject go = new GalleryObject ();
			go.Name = name;
			go.imageFileName = names [0];
			go.thumbFileName = names [1];

			if (ActiveLocation != null) {
				go.LocationID = ActiveLocation.ID;
				go.LocationType = "Lager";
			}

			if (ActiveContainer != null) {
				go.LocationID = ActiveContainer.ID;
				go.LocationType = "Container";
			}

			items.Add (go);
			AppDelegate.dao.SaveGalleryObject (go);

			carousel.ReloadData ();

		}

		public static string[] SaveGalleryImage (string name, UIImage ourpic)
		{
			if (ourpic == null)
				return new string[2]{ "", "" };
			Console.WriteLine ("Save");
			float aspectRatio = ourpic.Size.Width / ourpic.Size.Height;
			Console.WriteLine ("ratio:" + aspectRatio);

			float sc = 200;
			if (!UserInterfaceIdiomIsPhone) {
				sc = 450;
			}
			SizeF newSize = new SizeF (sc, sc / aspectRatio);
			UIImage thumbPic = ourpic.Scale (newSize); //measurements taken from CustomCell, alternatly 33x33
			UIImage resImage = ourpic.Scale (new SizeF (ourpic.Size.Width, ourpic.Size.Height));

			if (ourpic != null) {
				var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
				var gallerydirectory = Path.Combine (documentsDirectory, "gallery");

				if (!Directory.Exists (gallerydirectory)) {
					Directory.CreateDirectory (gallerydirectory);
				}



				var picname = name + ".png";
				var thumbpicname = name + "_thumb.png";
				string pngfileName = System.IO.Path.Combine (gallerydirectory, picname);
				string thumbpngfileName = System.IO.Path.Combine (gallerydirectory, thumbpicname);
				NSData imgData = resImage.AsPNG ();
				NSData img2Data = thumbPic.AsPNG ();

				NSError err = null;
				if (imgData.Save (pngfileName, false, out err)) {
					Console.WriteLine ("saved as " + pngfileName);
				} else {
					Console.WriteLine ("NOT saved as " + pngfileName + " because" + err.LocalizedDescription);
				}

				err = null;
				if (img2Data.Save (thumbpngfileName, false, out err)) {
					Console.WriteLine ("saved as " + thumbpngfileName);
					string[] result = new string[2] { picname, thumbpicname };
					return result;

				} else {
					Console.WriteLine ("NOT saved as " + thumbpngfileName + " because" + err.LocalizedDescription);
					return null;
				}
			}
			return new string[2]{ "", "" };
		}

		static string RandomGeneratedName ()
		{
			var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			var random = new Random ();
			var result = new string (
				             Enumerable.Repeat (chars, 8)
				.Select (s => s [random.Next (s.Length)])
				.ToArray ());
			return result;
		}
	}

	public class GalleryDataSource : CarouselViewDataSource
	{
		GalleryViewController vc;
		public Boolean Empty;

		public GalleryDataSource (GalleryViewController vc)
		{
			this.vc = vc;
		}

		public override uint NumberOfItems (CarouselView carousel)
		{
//			Empty = false;
			int x = vc.items.Count;
//			if (x == 0) {
//				x = 2;
//				Empty = true;
//			}
			return (uint)x;

//			Console.WriteLine ("x=" + x);
//			if (x == 0) {
//				Empty = true;
//				x = 2;
//				Console.WriteLine ("x override: " + x);
//				return 0;
//			} else {
//				Empty = false;
//			}
//			return x;
		}
		//		private UInt16 counter(IList<GalleryObject> list){
		//			UInt16 x = 0;
		//			foreach (GalleryObject o in list) {
		//				x++;
		//			}
		//			return x;
		//		}
		public override UIView ViewForItem (CarouselView carousel, uint index, UIView reusingView)
		{
//			if (!Empty) {
			Console.WriteLine ("viewForItem()index:" + index);
			var imgView = reusingView as UIImageView;

			var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var gallerydirectory = Path.Combine (documentsDirectory, "gallery");
			
			string thumbfilename = vc.items [(int)index].thumbFileName;
			string path = Path.Combine (gallerydirectory, thumbfilename);
			Console.WriteLine ("path:" + path);
			UIImage currentImage = UIImage.FromFile (path);
			SizeF dim = currentImage.Size;
			
			//create new view if none is availble fr recycling
			if (imgView == null) {
				imgView = new UIImageView (new RectangleF (0, 0, dim.Width, dim.Height)) {
					ContentMode = UIViewContentMode.ScaleAspectFit
				};
			}

			imgView.Image = currentImage;
			
			reusingView = imgView;
			
			return reusingView;
//			} else {
//				if (reusingView == null) {
//					reusingView = new UIImageView (new RectangleF(0, 0, 10, 10));
//				}
//
//				return reusingView;
//			}
		}
	}

	public class GalleryDelegate : CarouselViewDelegate
	{
		GalleryViewController vc;

		public GalleryDelegate (GalleryViewController vc)
		{
			this.vc = vc;
		}

		public override float ValueForOption (CarouselView carousel, CarouselOption option, float aValue)
		{
//			if (option == CarouselOption.Spacing)
//			{
//				return aValue * 1.1f;
//			}
//
//
//			if (option == CarouselOption.Count) {
//				return vc.items.Count;
//			}
//
			return aValue;
		}

		public override void DidSelectItem (CarouselView carousel, int index)
		{
			vc.Open (index);
		}
	}
}

