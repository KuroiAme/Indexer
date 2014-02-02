using System;
using MonoTouch.UIKit;
using iCarouselSDK;
using System.Drawing;
using System.Collections.Generic;
using no.dctapps.Garageindex.events;
using MonoTouch.Foundation;
using System.IO;
using System.Linq;
using GoogleAnalytics.iOS;

namespace GarageIndex
{
	public class TaggedImageViewController : UIViewController
	{
		public iCarousel carousel;
		UIImagePickerController imagePicker;
		public UIPopoverController Pc;
		public event EventHandler<GotPictureEventArgs> GotPicture;

		public TaggedImageViewController ()
		{
		}

		public override void ViewDidLoad(){
			base.ViewDidLoad ();

			var imgView = new UIImageView(UIImage.FromBundle("background")){
				ContentMode = UIViewContentMode.ScaleToFill,
				AutoresizingMask = UIViewAutoresizing.All,
				Frame = View.Bounds
			};

			View.AddSubview (imgView);

			carousel = new iCarousel (View.Bounds) {
				CarouselType = iCarouselType.CoverFlow2,
				DataSource = new TaggedImageDataSource(),
				Delegate = new TaggedImageDelegate(this)
			};

			View.AddSubview (carousel);
			CreateAddBarButton ();

			//carousel.CurrentItemIndex



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
				it.Title = "add";
				//IS really info
				it.Clicked += (object sender, EventArgs e) => SelectSource ();
				this.NavigationItem.SetRightBarButtonItem (it, true);
			
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
			actionSheet.ShowInView (View);
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
			mySavePicture(image); //local event

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
			GalleryObject go = new GalleryObject ();
			go.Name = name;
			go.imageFileName = names [0];
			go.thumbFileName = names [1];


			AppDelegate.dao.SaveGalleryObject (go);

			carousel.ReloadData ();

		}

		public static string[] SaveGalleryImage (string name, UIImage ourpic)
		{
			if(ourpic == null)
				return new string[2]{"",""};
			Console.WriteLine ("Save");
			UIImage thumbPic = ImageHelper.ResizeImage(ourpic, 200,200); //measurements taken from CustomCell, alternatly 33x33

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
				NSData imgData = ourpic.AsPNG ();
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



	public class TaggedImageDataSource : iCarouselDataSource{

		public override uint NumberOfItems(iCarousel carousel){
			return (uint) AppDelegate.dao.GetNumberOfItemsInGallery ();
		}

		public override UIView ViewForItem(iCarousel carousel, uint index, UIView reusingView){

//			List<UILabel> tags;

			var imgView = reusingView as UIImageView;

			//create new view if none is availble fr recycling
			if (imgView == null) {
				imgView = new UIImageView(new RectangleF(0,0, 200, 200)){
					ContentMode = UIViewContentMode.ScaleAspectFit
				};
			}

			var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var gallerydirectory = Path.Combine (documentsDirectory, "gallery");

			string thumbfilename = AppDelegate.dao.GetThumbfilenameForIndex (index);
			string path = Path.Combine (gallerydirectory, thumbfilename);
			Console.WriteLine ("path:" + path);
			UIImage currentImage = UIImage.FromFile (path);

			if (currentImage == null) {
				Console.WriteLine ("Fubar image");
			}



			//imgView.CancelCurrentImageLoad (); //?????
			imgView.Image = currentImage;

			reusingView = imgView;

			return reusingView;
		}

	}

	public class TaggedImageDelegate : iCarouselDelegate{
		TaggedImageViewController tivc;

		public TaggedImageDelegate (TaggedImageViewController tivc)
		{
			this.tivc = tivc;
		}

		public override void DidSelectItem (iCarousel carousel, int index)
		{
			Console.WriteLine ("Selected: " + ++index);
			GalleryObject go = AppDelegate.dao.GetGalleryObjectByIndex (--index);
			var eimc = new EditImageModeController (go);
			tivc.NavigationController.PushViewController (eimc, true);
		}

	}
}

