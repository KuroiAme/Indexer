//
//using System;
//using System.Drawing;
//
//using MonoTouch.Foundation;
//using MonoTouch.UIKit;
//using no.dctapps.garageindex.ads;
//using no.dctapps.garageindex.events;
//
//namespace no.dctapps.garagedb
//{
//	public partial class ImagePickerController : AdsViewController
//	{
//
//		UIImagePickerController imagePicker;
//		UIButton choosePhotoButton;
//
//		UIImage target = null;
//		UIImageView imageView;
//		public UIImage outputImage{ get; set;}
//		public string InitialImageFileName{ get; set;}
//		
//		public RectangleF ir{ get; set;}
//		public RectangleF pickerRect{ get; set;}
//
//		public UIPopoverController pc;
//
//		public event EventHandler<GotPictureEventArgs> gotPicture;
//
//
//		public ImagePickerController (string nib)
//			: base (nib)
//		{
//
//		}
//		
//		public override void DidReceiveMemoryWarning ()
//		{
//			// Releases the view if it doesn't have a superview.
//			base.DidReceiveMemoryWarning ();
//			this.imageView = null;
//			
//			// Release any cached data, images, etc that aren't in use.
//		}
//		
//		public override void ViewDidLoad ()
//		{
//			base.ViewDidLoad ();
//
//			if(pro){
//
//				imageView = new UIImageView(ir);
//				Add (imageView);
//			
//				if (InitialImageFileName != null) {
//					var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
//					string filename = System.IO.Path.Combine (documentsDirectory, InitialImageFileName);
//					this.imageView.Image = UIImage.FromFile (filename);
//					}
//
//				InitializeImagePicker ();
//
//			}else{
//				//Image handling needs more work aka moved to pro version?
//				throw new NotImplementedException();
//			}
//			// Perform any additional setup after loading the view, typically from a nib.
//		}
//
//		public string SaveImage (string name)
//		{
//			if (pro) {
//				this.imageView.Image = target;
//				UIImage ourpic = null;
//				Console.WriteLine ("Save");
//				imageView.Image = outputImage;
//				ourpic = outputImage;
//				if (ourpic != null) {
//					var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
//					string picname = name + ".png";
//					string pngfileName = System.IO.Path.Combine (documentsDirectory, picname);
//					NSData imgData = ourpic.AsPNG ();
//					NSError err = null;
//					if (imgData.Save (pngfileName, false, out err)) {
//						//					myObject.imageFileNames = new List<string>();
////						myObject.imageFileName = picname;
//						Console.WriteLine ("saved as " + pngfileName);
//						return picname;
//
//					} else {
//						Console.WriteLine ("NOT saved as " + pngfileName + " because" + err.LocalizedDescription);
//						return "";
//					}
//				}
//			}
//			return "";
//		}
//
//		//Bold attempt
//		public void resetImageView(){
//			Console.WriteLine ("reset image");
//			imageView.Image = null;
//		}
//
//		void RaiseImageGottenClicked (UIImage image)
//		{
//		
//			//This is event only for ipad?
////			if(UserInterfaceIdiomIsPhone){
//				this.imageView.Image = null;
//				var handler = this.gotPicture;
//				if (handler != null && image != null) {
//					handler(this, new GotPictureEventArgs(image));
//				}
////			}
//		}
//
//
//
//
//		public void InitializeImagePicker ()
//		{
//
//			var picky = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Select an image", "Select an image");
//		
//			View.BackgroundColor = UIColor.White;
//			imageView = new UIImageView (ir);
//			Add (imageView);
//			choosePhotoButton = UIButton.FromType (UIButtonType.RoundedRect);
//			choosePhotoButton.Frame = pickerRect;
//			choosePhotoButton.SetTitle (picky, UIControlState.Normal);
//			choosePhotoButton.TouchUpInside += (s, e) =>  {
//				// create a new picker controller
//				imagePicker = new UIImagePickerController ();
//				// set our source to the photo library
//				imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
//				// set what media types
//				imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.PhotoLibrary);
//				imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
//				imagePicker.Canceled += Handle_Canceled;
//				// show the picker
//				if(UserInterfaceIdiomIsPhone){
//					NavigationController.PresentViewController (imagePicker, true, delegate {});
//				}else{
//					Console.WriteLine("Popover");
//					pc = new UIPopoverController(imagePicker);
//					pc.PresentFromRect(pickerRect,(UIView) this.View, UIPopoverArrowDirection.Up, true);
//				}
//					
//			};
//			View.Add (choosePhotoButton);
//		}
//
//		// Do something when the 
//		void Handle_Canceled (object sender, EventArgs e) {
//			Console.WriteLine ("picker cancelled");
//			imagePicker.DismissViewController(true, delegate {});
//		}
//
//		
//		// This is a sample method that handles the FinishedPickingMediaEvent
//		protected void Handle_FinishedPickingMedia (object sender, UIImagePickerMediaPickedEventArgs e)
//		{
//			// determine what was selected, video or image
//			bool isImage = false;
//			switch(e.Info[UIImagePickerController.MediaType].ToString())
//			{
//			case "public.image":
//				Console.WriteLine("Image selected");
//				isImage = true;
//				break;
//				
//			case "public.video":
//				Console.WriteLine("Video selected");
//				break;
//			}
//			
//			Console.Write("Reference URL: [" + UIImagePickerController.ReferenceUrl + "]");
//			
//			// get common info (shared between images and video)
//			NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
//			if (referenceURL != null) 
//				Console.WriteLine(referenceURL.ToString ());
//			
//			// if it was an image, get the other image info
//			if(isImage) {
//				
//				// get the original image
//				UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
//				if(originalImage != null) {
//					// do something with the image
//					Console.WriteLine ("got the original image");
//					imageView.Image = originalImage;
//					outputImage = originalImage;
//					RaiseImageGottenClicked(originalImage);
//				}
//				
//				// get the edited image
//				UIImage editedImage = e.Info[UIImagePickerController.EditedImage] as UIImage;
//				if(editedImage != null) {
//					// do something with the image
//					Console.WriteLine ("got the edited image");
//					imageView.Image = editedImage;
//					outputImage = editedImage;
//					RaiseImageGottenClicked(editedImage);
//				}
//				
//				//- get the image metadata
//				NSDictionary imageMetadata = e.Info[UIImagePickerController.MediaMetadata] as NSDictionary;
//				if(imageMetadata != null) {
//					// do something with the metadata
//					Console.WriteLine ("got image metadata");
//				}
//				
//			}
//			// if it's a video
//			else {
//				// get video url
//				NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
//				if(mediaURL != null) {
//					//
//					Console.WriteLine(mediaURL.ToString());
//				}
//			}
//
//
//			
//			if(UserInterfaceIdiomIsPhone){
//				imagePicker.DismissViewController (true, delegate{});
//			}else{
//				pc.Dismiss(false);
//			}
//		}
//
//		public override void ViewWillAppear (bool animated)
//		{
//			base.ViewWillAppear (animated);
//			//TODO refresh view to display image?
//		}
//
//	}
//}
//
