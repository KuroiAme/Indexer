using System;
using No.Dctapps.GarageIndex;
using no.dctapps.Garageindex.model;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.events;
using GoogleAnalytics.iOS;
using System.Drawing;
using System.IO;
using MonoTouch.Foundation;
using System.Linq;
using Alliance.Carousel;
using System.Collections.Generic;

namespace GarageIndex
{
	public class InsurancePhotoController : UIViewController
	{
		Item item;
		public Boolean isLargeObject;
		LagerObject lagerobject;

		public CarouselView carousel;
		UIImagePickerController imagePicker;
		public UIPopoverController Pc;
		LoadingOverlay loadingOverlay;

		public event EventHandler<GotPictureEventArgs> GotPicture;

		int currentID;

		public IList<InsurancePhoto> photos;

		public InsurancePhotoController (Item item)
		{
			this.currentID = item.ID;
			this.item = item;
			isLargeObject = false;
		}

		public InsurancePhotoController(LagerObject lagerobject){
			this.currentID = lagerobject.ID;
			this.lagerobject = lagerobject;
			isLargeObject = true;
		}


		public override void ViewDidLoad(){
			base.ViewDidLoad ();

			var imgView = new UIImageView(BlueSea.MakeBlueSea()){
				ContentMode = UIViewContentMode.ScaleToFill,
				AutoresizingMask = UIViewAutoresizing.All,
				Frame = View.Bounds
			};

			IList<InsurancePhoto> photos = AppDelegate.dao.GetInsurancePhotosByTypeAndID (isLargeObject, currentID);

			carousel = new CarouselView(View.Bounds);
			carousel.DataSource = new InsurancePhotoDataSource(this);
			carousel.Delegate = new InsurancePhotoDelegate(this);
			carousel.CarouselType = CarouselType.CoverFlow;
			carousel.ConfigureView();
			View.AddSubview(carousel);

			View.AddSubview (carousel);
			CreateAddBarButton ();
			CreateDeleteBarButton ();

			//carousel.CurrentItemIndex



		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Insurance photo Gallery");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}

		UIBarButtonItem it;

		private void CreateAddBarButton ()
		{

			it = new UIBarButtonItem ();
			var addtext = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Add", "Add");
			it.Title = addtext;
			it.Clicked += (object sender, EventArgs e) => SelectSource ();
			this.NavigationItem.SetRightBarButtonItem (it, true);

		}

		UIBarButtonItem it2;

		private void CreateDeleteBarButton ()
		{

			it2 = new UIBarButtonItem ();
			var deltext = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Delete", "Delete");
			it2.Title = deltext;
			it2.Clicked += (object sender, EventArgs e) => ReallyDelete ();

			UIBarButtonItem back = new UIBarButtonItem ();
			var backtext = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("back", "back");
			back.Title = backtext;
			back.Clicked += (object sender, EventArgs e) => this.NavigationController.PopViewControllerAnimated (false);

			UIBarButtonItem[] items = { back, it2 };
			this.NavigationItem.SetLeftBarButtonItems (items, true);
			//this.NavigationItem.SetLeftBarButtonItem (it2, true);


		}
		UIActionSheet delsheet;

		private void ReallyDelete(){
			var really = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Really Delete current image?", "Really Delete current image?");
			var myCancel = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Cancel", "Cancel");
			var OK = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("OK", "OK");
			delsheet = new UIActionSheet(really);
			delsheet.AddButton (myCancel);
			delsheet.AddButton (OK);

			delsheet.Clicked += delegate(object sender, UIButtonEventArgs e2) {
				if(e2.ButtonIndex == 0){
					Console.WriteLine("deletion canceled");
				}else if(e2.ButtonIndex == 1){
					Console.WriteLine("Deletion OK");
					DeleteCurrentPic();
				}else{
					Console.WriteLine("Unknown button");
				}
			};
			UIView x = UIApplication.SharedApplication.KeyWindow;
			delsheet.ShowInView (x);
		}

		void DeleteCurrentPic ()
		{
			int currentindex = carousel.CurrentItemIndex;
			Console.WriteLine ("currentindex:" + currentindex);
			if (isLargeObject) {
				AppDelegate.dao.DeleteInsurancePhotoByIndexAndObjectId (currentindex, lagerobject.ID);
			} else {
				AppDelegate.dao.DeleteInsurancePhotoByIndexAndObjectId(currentindex, item.ID);
			}
			carousel.ReloadData ();
		}

		UIActionSheet actionSheet;

		public void SelectSource(){
			var source = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("add image from where?", "add image from where?");
			var myCancel = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Cancel", "Cancel");
			var myCamera = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Camera", "Camera");
			var myLibrary = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Photo Library", "Photo Library");
			actionSheet = new UIActionSheet(source);
			actionSheet.AddButton(myCancel);
			actionSheet.AddButton(myCamera);
			actionSheet.AddButton(myLibrary);
			//			actionSheet.CancelButtonIndex = 0;

			actionSheet.Clicked += delegate(object sender, UIButtonEventArgs e2) {
				if(e2.ButtonIndex == 0){
					//DO nothing
				}else if(e2.ButtonIndex == 1){
					PickFromCamera();
				}else{
					PickFromLibrary();
				}
			};
			actionSheet.ShowInView (UIApplication.SharedApplication.KeyWindow);
		}

		public void PickFromCamera(){
			// create a new picker controller

			// set our source to the photo library
			imagePicker = new UIImagePickerController ();
			imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;
			// set what media types
			imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.Camera);
			imagePicker.FinishedPickingMedia += HandleFinishedPickingMedia;
			imagePicker.Canceled += Handle_Canceled;
			// show the picker
			if(UserInterfaceIdiomIsPhone){
				NavigationController.PresentViewController (imagePicker, true, delegate {});
			}else{
				Console.WriteLine("Popover");
				Pc = new UIPopoverController(imagePicker);
				Pc.PresentFromBarButtonItem(it, UIPopoverArrowDirection.Up, true);
			}
		}

		public void PickFromLibrary(){
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
			// show the picker
			if (UserInterfaceIdiomIsPhone) {
				NavigationController.PresentViewController (imagePicker, true, delegate{});
			}
			else {
				Console.WriteLine ("Popover");
				Pc = new UIPopoverController (imagePicker);
				Pc.PresentFromBarButtonItem (it, UIPopoverArrowDirection.Up, true);
			}
		}

		// Do something when the 
		void Handle_Canceled (object sender, EventArgs e) {
			Console.WriteLine ("picker cancelled");
			imagePicker.DismissViewController(true, delegate {});
		}


		// This is a sample method that handles the FinishedPickingMediaEvent
		protected void HandleFinishedPickingMedia (object sender, UIImagePickerMediaPickedEventArgs e)
		{
			// determine what was selected, video or image
			bool isImage = false;
			switch(e.Info[UIImagePickerController.MediaType].ToString())
			{
			case "public.image":
				Console.WriteLine("Image selected");
				isImage = true;
				break;

			case "public.video":
				Console.WriteLine("Video selected");
				break;
			}

			//			Console.Write("Reference URL: [" + UIImagePickerController.ReferenceUrl + "]");

			// get common info (shared between images and video)
			//			NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
			//			if (referenceURL != null) 
			//				Console.WriteLine(referenceURL.ToString ());

			// if it was an image, get the other image info
			if(isImage) {

			// get the original image
			UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
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


			if(UserInterfaceIdiomIsPhone){
				imagePicker.DismissViewController (true, delegate{});
			}else{
				Pc.Dismiss(false);
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
			mySavePicture(image); //local event
			loadingOverlay.Hide ();

			//			this.imageView.Image = null;

			//			var handler = this.GotPicture;
			//			if (handler != null && image != null) {
			//				handler(this, new GotPictureEventArgs(image));
			//			}
		}



		private void mySavePicture(UIImage image){


			Console.WriteLine ("mySavePicture()");
			string name = RandomGeneratedName ();
			string[] names = SaveGalleryImage (name, image);
			InsurancePhoto photo = new InsurancePhoto ();
			photo.ImageFileName = names [0];
			photo.ThumbFileName = names [1];
			if (isLargeObject) {
				photo.IsLargeObject = "true";
				photo.ObjectReferenceID = lagerobject.ID; 
			} else {
				photo.IsLargeObject = "false";
				photo.ObjectReferenceID = item.ID;
			}
			AppDelegate.dao.SaveInsurancePhoto (photo);

			carousel.ReloadData ();

		}

		public static string[] SaveGalleryImage (string name, UIImage ourpic)
		{
			if(ourpic == null)
				return new string[2]{"",""};
			Console.WriteLine ("Save");
			float aspectRatio = ourpic.Size.Width / ourpic.Size.Height;
			Console.WriteLine ("ratio:" + aspectRatio);
			float sc = 200;
			SizeF newSize = new SizeF (sc, sc / aspectRatio);
			UIImage thumbPic = ourpic.Scale (newSize); //measurements taken from CustomCell, alternatly 33x33
			UIImage resImage = ourpic.Scale (new SizeF (ourpic.Size.Width, ourpic.Size.Height));

			if (ourpic != null) {
				var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
				var gallerydirectory = Path.Combine (documentsDirectory, "insurancePhotos");

				if (!Directory.Exists (gallerydirectory)) {
					Directory.CreateDirectory (gallerydirectory);
				}



				var picname = name + ".png";
				var thumbpicname = name + "_thumb.png";
				string pngfileName = System.IO.Path.Combine (gallerydirectory, picname);
				string thumbpngfileName = System.IO.Path.Combine (gallerydirectory, thumbpicname);
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
	

	}

	public class InsurancePhotoDataSource : CarouselViewDataSource
	{
		InsurancePhotoController vc;

		public InsurancePhotoDataSource(InsurancePhotoController vc)
		{
			this.vc = vc;
		}

		public override uint NumberOfItems(CarouselView carousel)
		{
			return (uint)vc.photos.Count;
		}

		public override UIView ViewForItem(CarouselView carousel, uint index, UIView reusingView)
		{

			var imgView = reusingView as UIImageView;



			var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var gallerydirectory = Path.Combine (documentsDirectory, "insurancePhotos");

			InsurancePhoto photo = vc.photos[(int)index];
			string thumbfilename = photo.ThumbFileName;
			string path = Path.Combine (gallerydirectory, thumbfilename);
			Console.WriteLine ("path:" + path);
			UIImage currentImage = UIImage.FromFile (path);
			SizeF dim = currentImage.Size;

			//create new view if none is availble fr recycling
			if (imgView == null) {
				imgView = new UIImageView(new RectangleF(0,0, dim.Width,dim.Height)){
					ContentMode = UIViewContentMode.ScaleAspectFit
				};
			}

			if (currentImage == null) {
				Console.WriteLine ("Fubar image");
			}



			//imgView.CancelCurrentImageLoad (); //?????
			imgView.Image = currentImage;

			reusingView = imgView;

			return reusingView;
		}
	}



	public class InsurancePhotoDelegate : CarouselViewDelegate
	{
		InsurancePhotoController vc;

		public InsurancePhotoDelegate(InsurancePhotoController vc)
		{
			this.vc = vc;
		}

		public override float ValueForOption(CarouselView carousel, CarouselOption option, float aValue)
		{
			if (option == CarouselOption.Spacing)
			{
				return aValue * 1.1f;
			}
			return aValue;
		}

		public override void DidSelectItem(CarouselView carousel, int index)
		{
			Console.WriteLine ("Selected: " + ++index);
			InsurancePhoto photo = vc.photos [index];

			ViewInsurancePhoto vip = new ViewInsurancePhoto (photo);
			vc.NavigationController.PushViewController (vip, false);
		}
	}

}

