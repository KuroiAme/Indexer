using System;
using System.Drawing;
using MonoTouch.AssetsLibrary;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace ImageView {

	public class ImageViewController : UIViewController {
		
		UIImagePickerController imagePicker;
		UIButton choosePhotoButton;
		UIImageView imageView;
		public UIImage someImage{ get; set;}
		public UIImage outputImage{ get; set;}
		UIImage target;
		public RectangleF imageRect{ get; set;}
		public RectangleF pickerRect{ get; set;}

//		RectangleF imageRect =  new RectangleF(10,80,300,300);
//		RectangleF pickerRect = new RectangleF(10, 20, 100,40);

		public ImageViewController ()
		{
		
		}

		//TESTING METHOD
//		void copyResourceImage (string file)
//		{
//			someImage = UIImage.FromFile(file);
// 			someImage.SaveToPhotosAlbum((image, error) => {
//				var o = image as UIImage;
//				Console.WriteLine("error:" + error);
//			});
//		}
//		partial void btnSave (MonoTouch.Foundation.NSObject sender);

//		void btnSave(MonoTouch.Foundation.NSObject sender, MonoTouch.UIKit.UIEvent @event){
//		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

//			copyResourceImage ("second.png");

			InitializeImagePicker ();


		}

		void InitializeImagePicker ()
		{
			Title = "Choose Photo";
			View.BackgroundColor = UIColor.White;
			imageView = new UIImageView (imageRect);
			Add (imageView);
			choosePhotoButton = UIButton.FromType (UIButtonType.RoundedRect);
			choosePhotoButton.Frame = pickerRect;
			choosePhotoButton.SetTitle ("Plukk et bilde", UIControlState.Normal);
			choosePhotoButton.TouchUpInside += (s, e) =>  {
				// create a new picker controller
				imagePicker = new UIImagePickerController ();
				// set our source to the photo library
				imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
				// set what media types
				imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.PhotoLibrary);
				imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
				imagePicker.Canceled += Handle_Canceled;
				// show the picker
				NavigationController.PresentModalViewController (imagePicker, true);
			};
			View.Add (choosePhotoButton);
		}
		
		// Do something when the 
		void Handle_Canceled (object sender, EventArgs e) {
			Console.WriteLine ("picker cancelled");
			imagePicker.DismissViewController(true, delegate{});
		}

		// This is a sample method that handles the FinishedPickingMediaEvent
		protected void Handle_FinishedPickingMedia (object sender, UIImagePickerMediaPickedEventArgs e)
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
			
			Console.Write("Reference URL: [" + UIImagePickerController.ReferenceUrl + "]");
			
			// get common info (shared between images and video)
			NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
			if (referenceURL != null) 
				Console.WriteLine(referenceURL.ToString ());
			
			// if it was an image, get the other image info
			if(isImage) {
				
				// get the original image
				UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
				if(originalImage != null) {
					// do something with the image
					Console.WriteLine ("got the original image");
				imageView.Image = originalImage;
				outputImage = originalImage;
				}
				
				// get the edited image
				UIImage editedImage = e.Info[UIImagePickerController.EditedImage] as UIImage;
				if(editedImage != null) {
					// do something with the image
					Console.WriteLine ("got the edited image");
				imageView.Image = editedImage;
				outputImage = editedImage;
				}
				
				//- get the image metadata
				NSDictionary imageMetadata = e.Info[UIImagePickerController.MediaMetadata] as NSDictionary;
				if(imageMetadata != null) {
					// do something with the metadata
					Console.WriteLine ("got image metadata");
				}
				
			}
			// if it's a video
			else {
				// get video url
				NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
				if(mediaURL != null) {
					//
					Console.WriteLine(mediaURL.ToString());
				}
			}
			
			// dismiss the picker
			imagePicker.DismissViewController(true, delegate{});
		}
	}
}