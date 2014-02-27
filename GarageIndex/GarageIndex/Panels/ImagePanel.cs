using System;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.events;
using no.dctapps.Garageindex.screens;
using No.Dctapps.Garageindex.Ios.Screens;
using no.dctapps.Garageindex.model;
using System.Drawing;
using MonoTouch.Foundation;
using No.Dctapps.GarageIndex;
using System.Linq;

namespace GarageIndex
{
	public class ImagePanel : UIViewController
	{
		UIImagePickerController imagePicker;
		UIImageView imageView;

		public UIImage OutputImage{ get; set; }

		public string InitialImageFileName{ get; set; }

		public UIPopoverController Pc;

		private event EventHandler<GotPictureEventArgs> GotPicture;
		public event EventHandler<DerezLargeObjectEventArgs> Derezzy;
		public event EventHandler<SavedImageStringsEventArgs> ImageSaved;
		public event EventHandler ImageDeleted;

		readonly RectangleF myFrame;
		string current_imagefilename;

		public ImagePanel (RectangleF myFrame)
		{
			this.myFrame = myFrame;
		}

		public void ResetImageView ()
		{
			Console.WriteLine ("reset image");
			InitializeEmptyImage ();
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			//if stuff is loaded but not active a window it will be cleaned out.
			if (this.IsViewLoaded && this.View.Window == null) {
				Cleanup ();
			}
		}

		public void Cleanup ()
		{
			imagePicker = null;
		}

//		public void SetImageViewImage (UIImageView imageholder)
//		{
//			Console.WriteLine ("setImageViewImage");
//			if (imageholder != null) {
//
//				Console.WriteLine ("Not null");
//				this.imageView = imageholder;
//				Add (imageholder);
//			}
//		}

		public override void LoadView ()
		{
			base.LoadView ();
			this.View.Frame = myFrame;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			InitializeEmptyImage ();

			this.GotPicture += (object sender, GotPictureEventArgs e) => {
				MySavePicture (e.image); //local event
				this.imageView.Image = e.image;
				this.imageView.SetNeedsDisplay ();
			};
		}

		private void InitializeEmptyImage ()
		{
			this.imageView = new UIImageView(EmptyImageCanvas.MakeEmptyCanvas()){
				ContentMode = UIViewContentMode.ScaleToFill,
				AutoresizingMask = UIViewAutoresizing.All,
				Frame = View.Bounds
			};
			this.View.BackgroundColor = UIColor.Clear;


			var doubletap = new UITapGestureRecognizer (AddImage);
			doubletap.NumberOfTapsRequired = 2;
			imageView.AddGestureRecognizer (doubletap);

			var longpress = new UILongPressGestureRecognizer(RemoveImage);
			imageView.AddGestureRecognizer(longpress);

			Add (imageView);
			View.BringSubviewToFront (imageView);
		}

		private void AddImage (UIGestureRecognizer recognizer){
			SelectSource ();
		}

		private void RemoveImage (UIGestureRecognizer recognizer){
			ReallyRemove ();
		}

		public void SetNewImageName (string imageFileName)
		{
			this.current_imagefilename = imageFileName;

			if (!string.IsNullOrEmpty (this.current_imagefilename)) {
				UIImage image = this.LoadImage (this.current_imagefilename);
				if (image != null) {
					this.imageView.Image = image;
					this.imageView.SetNeedsDisplay ();
				}
			}
		}

		void RaiseSavedImageEvent (string imagefilename, string thumbfilename)
		{
			var handler = this.ImageSaved;
			if (handler != null) {
				handler (this, new SavedImageStringsEventArgs (imagefilename, thumbfilename));
			}
		}

		private void mySavePicture (UIImage image)
		{
			image = image.Scale (image.Size);
			string[] output = SaveImage (RandomGeneratedName (), image);
			RaiseSavedImageEvent (output [0], output [1]);
		}

		private void  MySavePicture (UIImage image){
			image = image.Scale (image.Size);
			string[] output = SaveImage (RandomGeneratedName (), image);
			RaiseSavedImageEvent (output [0], output [1]);
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

		void makeCornersRound ()
		{
			if (this.imageView != null) {
				this.imageView.Layer.CornerRadius = 7;
				this.imageView.ClipsToBounds = true;
			}
		}

		void RaiseDerez ()
		{
			var handler = this.Derezzy;
			if (handler != null) {
				handler (this, new DerezLargeObjectEventArgs ());
			}
		}

		public void SelectSource ()
		{
			var source = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("pick image from where?", "pick image from where?");
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
//			if (UserInterfaceIdiomIsPhone) {
			PresentViewController (imagePicker, true, null);
				//nc.PresentViewController (imagePicker, true, delegate {});
//			} else {
//				Console.WriteLine ("Popover");
//				Pc = new UIPopoverController (imagePicker);
//				Pc.PresentFromRect (this.btnBigPickImage.Frame, (UIView)this.View, UIPopoverArrowDirection.Up, true);
//			}
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

			if (isImage) {
				UIImage originalImage = e.Info [UIImagePickerController.OriginalImage] as UIImage;
				if (originalImage != null) {

					RaiseImageGotten (originalImage);
				} else {

					UIImage editedImage = e.Info [UIImagePickerController.EditedImage] as UIImage;
					if (editedImage != null) {
						RaiseImageGotten (editedImage);
					}
				}

			}

			if (UserInterfaceIdiomIsPhone) {
				imagePicker.DismissViewController (true, delegate {
				});
			} else {
				Pc.Dismiss (false);
			}
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
			// show the picker
//			if (UserInterfaceIdiomIsPhone) {
			PresentViewController (imagePicker, true, null);
				//nc.PresentViewController (imagePicker, true, delegate{});
//			} else {
//				Console.WriteLine ("Popover");
//				Pc = new UIPopoverController (imagePicker);
//				Pc.PresentFromRect 
//				Pc.PresentFromRect (this.btnBigPickImage.Frame, (UIView)this.View, UIPopoverArrowDirection.Up, true);
//			}
		}

		UIActionSheet actionSheet;

		void ReallyRemove ()
		{
			var rmText = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("remove picture?", "remove picture?");
			var rmDel = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Delete", "Delete");
			var rmCancel = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Cancel", "Cancel");
			actionSheet = new UIActionSheet (rmText, null, rmCancel, rmDel, null);
			actionSheet.Clicked += delegate (object a, UIButtonEventArgs b) {
				Console.WriteLine ("button" + b.ButtonIndex.ToString () + " clicked");
				if (b.ButtonIndex == 0) {
					Console.WriteLine ("deleting picture!");
					//DELETE IMAGE FROM APP.
					DeletePic (this.current_imagefilename);
					//POP this view to refresh.
					if (UserInterfaceIdiomIsPhone) {
						DismissViewController (true, null);
						//nc.PopViewControllerAnimated(true);
					} else {
						RaiseDerez ();
					}
				}
			};
			actionSheet.ShowInView (this.View);
		}

		void Handle_Canceled (object sender, EventArgs e)
		{
			Console.WriteLine ("picker cancelled");
			imagePicker.DismissViewController (true, delegate {
			});
		}

		public void DeletePic (string imagefilename)
		{
			DeleteImage (imagefilename);
			this.imageView = new UIImageView (View.Bounds);
			this.imageView.Image = EmptyImageCanvas.MakeEmptyCanvas ();
			var handler = this.ImageDeleted;
			if (handler != null) {
				handler (this, new EventArgs ());
			}
		}

		void RaiseImageGotten (UIImage image)
		{
			var handler = this.GotPicture;
			if (handler != null && image != null) {
				handler (this, new GotPictureEventArgs (image));
			}
		}

		public static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public static string[] SaveImage (string name, UIImage ourpic)
		{
			if (ourpic == null)
				return new string[2]{ "", "" };
			Console.WriteLine ("Save");
			UIImage thumbPic = ourpic.Scale (new SizeF (50, 50)); //measurements taken from CustomCell, alternatly 33x33

			if (ourpic != null) {
				var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
				var picname = name + ".png";
				var thumbpicname = name + "_thumb.png";
				string pngfileName = System.IO.Path.Combine (documentsDirectory, picname);
				string thumbpngfileName = System.IO.Path.Combine (documentsDirectory, thumbpicname);
				NSData imgData = ourpic.AsPNG ();
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

		public static void DeleteImage (string imagefilename)
		{
			string name = imagefilename.Substring (0, imagefilename.Length - 4);
			var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var picname = name + ".png";
			var thumbpicname = name + "_thumb.png";
			string pngfileName = System.IO.Path.Combine (documentsDirectory, picname);
			string thumbpngfileName = System.IO.Path.Combine (documentsDirectory, thumbpicname);

			NSFileManager fm = new NSFileManager ();

			NSError err = null;

			if (fm.IsDeletableFile (pngfileName)) {
				fm.Remove (pngfileName, out err);   
				//TODO use error for something sensible
			}

			err = null;

			if (fm.IsDeletableFile (thumbpngfileName)) {
				fm.Remove (thumbpngfileName, out err);
				//TODO use errormsg for something sensible.
			}
		}

		public UIImage LoadImage(string filename){
			Console.WriteLine ("loadImage():" + filename);
			if (!string.IsNullOrEmpty (filename)) {
				var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
				string path = System.IO.Path.Combine (documentsDirectory, filename);
				UIImage image = UIImage.FromFile (path);
				if (image != null) {
					image = image.Scale (image.Size);
					return image;
				}
			} else {
				Console.WriteLine ("buggery");
			}
			return null;
		}
	}
}