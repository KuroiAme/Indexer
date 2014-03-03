using System;
using MonoTouch.UIKit;
using Alliance.Carousel;
using System.Drawing;
using System.Collections.Generic;
using no.dctapps.Garageindex.events;
using MonoTouch.Foundation;
using System.IO;
using System.Linq;
using GoogleAnalytics.iOS;
using SatelliteMenu;
using No.DCTapps.GarageIndex;
using no.dctapps.Garageindex.screens;
using no.dctapps.Garageindex.model;
using no.dctapps.garageindex;
using no.dctapps.Garageindex;

namespace GarageIndex
{
	public class GalleryViewController : UIViewController
	{
		CarouselView carousel;
		UIImagePickerController imagePicker;
		public UIPopoverController Pc;
		LoadingOverlay loadingOverlay;

		public event EventHandler<GotPictureEventArgs> GotPicture;
		public event EventHandler Clear;

		public GalleryViewController ()
		{
		}

		void Tapped (UITapGestureRecognizer gestureRecognizer)
		{
			Console.WriteLine ("tapped");
			RaiseClear ();
		}

		public IList<GalleryObject> items;

		public void ChangeThumb ()
		{
			carousel.ReloadData ();
		}

		IList<GalleryObject> GetActiveGalleryItems ()
		{
			string type = AppDelegate.key.GetActiveGalleryType ();
			int id = AppDelegate.key.GetActiveGalleryID ();
			Console.WriteLine ("type:" + type + ",id:" + id);
			if (string.IsNullOrEmpty (type)) {
				type = "ALL";
			}
			if (type != "ALL") {
				return AppDelegate.dao.GetAllGalleryObjectsByTypeAndID (type, id);
			} else {
				return AppDelegate.dao.GetAllGalleryObjects ();
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

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
			GalleryDelegate gd = new GalleryDelegate (this);
			carousel.Delegate = gd;
			carousel.CarouselType = CarouselType.CoverFlow;
			carousel.ConfigureView ();
			View.AddSubview (carousel);


			var tap = new UITapGestureRecognizer (Tapped);
			tap.NumberOfTapsRequired = 1;
			carousel.AddGestureRecognizer (tap);

			View.AddSubview (carousel);
			//CreateAddBarButton ();
			//CreateDeleteBarButton ();

			//carousel.CurrentItemIndex

			//InitSateliteMenu ();
			InitActiveField ();

			IndexerSateliteMenu menu = new IndexerSateliteMenu ("Gallery", this);
			View.Add (menu.View);

			CreateOptions ();



		}

		public void Open (int index)
		{
			GalleryObject go = items [index];
			EditImageModeController eimc = new EditImageModeController (go, this);
			this.NavigationController.PushViewController (eimc, true);
		}

		UILabel active;
		LagerObject activeContainer;
		Lager activeLager;
		int activeID;
		string activeType;

		void InitActiveField ()
		{
			RectangleF activeRect = new RectangleF (100, 100, 200, 40);
			active = new UILabel (activeRect);
			active.AdjustsFontSizeToFitWidth = true;
			active.ShadowColor = UIColor.Gray;
			active.ShadowOffset = new SizeF (1.0f, 0.2f);
			active.TextColor = UIColor.White;
			activeID = AppDelegate.key.GetActiveGalleryID ();
			activeType = AppDelegate.key.GetActiveGalleryType ();

			if (activeType == null) {
				activeType = "ALL";
			}

			if (activeType.Equals ("Lager")) {
				activeContainer = null;
				activeLager = AppDelegate.dao.GetLagerById (activeID);
					
			}
			if (activeType.Equals ("Container")) {
				activeLager = null;
				activeContainer = AppDelegate.dao.GetContainerById (activeID);
			}

			if (activeType == "ALL") {
				activeLager = null;
				activeContainer = null;
			}

				
			
			Add (active);
			View.BringSubviewToFront (active);


		}

		private event EventHandler AddImagePressed;

		UIBarButtonItem addImageButton;
		UIBarButtonItem delImageButton;

		private event EventHandler DelImagePressed;

		private event EventHandler SetActivePressed;

		UIBarButtonItem setActiveButton;

		private void CreateOptions ()
		{
			List<UIBarButtonItem> buttons = new List<UIBarButtonItem> ();
			addImageButton = new UIBarButtonItem (UIImage.FromBundle ("frames4832.png"), UIBarButtonItemStyle.Bordered, this.AddImagePressed);
			addImageButton.Clicked += (object sender, EventArgs e) => SelectSource();
			//AddImagePressed += (object sender, EventArgs e) => SelectSource ();
			buttons.Add (addImageButton);

			delImageButton = new UIBarButtonItem (UIImage.FromBundle ("startree.png"), UIBarButtonItemStyle.Bordered, this.DelImagePressed);
			//DelImagePressed += (object sender, EventArgs e) => ReallyDelete ();
			delImageButton.Clicked += (object sender, EventArgs e) => ReallyDelete ();
			buttons.Add (delImageButton);

			setActiveButton = new UIBarButtonItem (UIImage.FromBundle ("house.png"), UIBarButtonItemStyle.Bordered, this.SetActivePressed);
			setActiveButton.Clicked += (object sender, EventArgs e) => SetActive ();
			//SetActivePressed += (object sender, EventArgs e) => SetActive ();
			buttons.Add (setActiveButton);

			this.NavigationItem.SetRightBarButtonItems (buttons.ToArray(), true);

		}

		public void SetActive ()
		{
			UIActionSheet activeSheet = new UIActionSheet ();
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
					Console.WriteLine ("container");
					SelectContainer sc = new SelectContainer ();
					this.NavigationController.PushViewController(sc,true);
					sc.DismissEvent += (object sender2, ContainerClickedEventArgs e2) => {
						//active.BackgroundColor = UIColor.White;
						active.Text = e2.container.Name;
						AppDelegate.key.StoreActiveGallery (e2.container);
					};
				}
				if (e.ButtonIndex == 2) {
					//select Lager
					Console.WriteLine ("location");
				}
				if (e.ButtonIndex == 3) {
					Console.WriteLine ("ALL");
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


//		SatelliteMenuButton MainButton;

//		public void InitSateliteMenu ()
//		{
//
//			var image = UIImage.FromFile ("menu.png");
//			var yPos = View.Frame.Height - image.Size.Height - 10;
//			var frame = new RectangleF (10, yPos, image.Size.Width, image.Size.Height);
//
//			var SateliteItems = new [] { 
//				new SatelliteMenuButtonItem (UIImage.FromBundle ("scanner4832.png"), 1, "Scanner"),
//				new SatelliteMenuButtonItem (Flosshatt.MakeFlosshatt (), 2, "Items"),
//				new SatelliteMenuButtonItem (UIImage.FromFile ("table4832.png"), 3, "Big Items"),
//				new SatelliteMenuButtonItem (UIImage.FromFile ("container4832.png"), 4, "Containers"),
//				new SatelliteMenuButtonItem (UIImage.FromFile ("preferences4832.png"), 5, "Preferences"),
//			};
//
//			MainButton = new SatelliteMenuButton (View, image, SateliteItems, frame);
//
//			MainButton.MenuItemClick += (_, args) => {
//				Console.WriteLine ("{0} was clicked!", args.MenuItem.Name);
//
//				if (args.MenuItem.Name == "Scanner") {
//					Scanner scanner = new Scanner (this);
//					scanner.Scannit ();
//				}
//				if (args.MenuItem.Name == "Items") {
//					if (UserInterfaceIdiomIsPhone) {
//						ItemCatalogue cat = new ItemCatalogue ();
//						PresentViewControllerAsync (cat, true);
//					} else {
//						ItemMasterView itemMaster = new ItemMasterView ();
//						PresentViewControllerAsync (itemMaster, true);
//					}
//				}
//				if (args.MenuItem.Name == "Big Items") {
//					if (UserInterfaceIdiomIsPhone) {
//						BigItemsScreen biggies = new BigItemsScreen ();
//						PresentViewControllerAsync (biggies, true);
//					} else {
//						BigItemMasterView bigMaster = new BigItemMasterView ();
//						PresentViewControllerAsync (bigMaster, true);
//					}
//				}
//				if (args.MenuItem.Name == "Containers") {
//					if (UserInterfaceIdiomIsPhone) {
//						ContainerScreen containers = new ContainerScreen ();
//						PresentViewControllerAsync (containers, true);
//					} else {
//						ContainerMasterView containerMaster = new ContainerMasterView ();
//						PresentViewControllerAsync (containerMaster, true);
//					}
//				}
//				if (args.MenuItem.Name == "Preferences") {
//					Preferences pref = new Preferences ();
//					PresentViewControllerAsync (pref, true);
//				}
//
//
//			};
//
//			View.AddSubview (MainButton);
//		}

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
			PresentViewControllerAsync (imagePicker, true);
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
			PresentViewControllerAsync (imagePicker, true);
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


			if (UserInterfaceIdiomIsPhone) {
				imagePicker.DismissViewController (true, delegate {
				});
			} else {
				Pc.Dismiss (false);
			}
		}

		public static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		void RaiseImageGotten (UIImage image)
		{
			loadingOverlay = new LoadingOverlay (UIScreen.MainScreen.Bounds);
			View.Add (loadingOverlay);
			View.BringSubviewToFront (loadingOverlay);
			mySavePicture (image); //local event
			loadingOverlay.Hide ();

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

			if (activeLager != null) {
				go.LocationID = activeLager.ID.ToString ();
				go.LocationType = "Lager";
			}

			if (activeContainer != null) {
				go.LocationID = activeContainer.ID.ToString ();
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

