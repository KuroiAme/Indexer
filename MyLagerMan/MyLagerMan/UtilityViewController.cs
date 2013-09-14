using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;
using System;
using MonoTouch.ObjCRuntime;
using no.dctapps.helpers;

namespace no.dctapps.Garageindex.screens
{
	public class UtilityViewController : UIViewController
	{
		public bool Pro = true;
		private RectangleF dyn;

		public UtilityViewController (string nib) : base (nib, null)
		{
		}

		public UtilityViewController () : base ()
		{
		}

//		public void cleanup (){
//			Console.WriteLine ("cleanupmethod not implemented:"+this.ToString());
//			throw new NotImplementedException("NIH");
//		}
//
//		public override void DidReceiveMemoryWarning (){
//			base.DidReceiveMemoryWarning ();
//			if(this.IsViewLoaded && this.View.Window == null){
//				cleanup ();
//			}
//		}
//
//		public static bool InSimulator ()
//		{
//			return Runtime.Arch == Arch.SIMULATOR;
//		}

		public static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}


		public RectangleF GetDyn ()
		{
			const float offset = 0;
			float x = UIScreen.MainScreen.Bounds.X + offset;
			float y = UIScreen.MainScreen.Bounds.Y + offset;
			float width = 0;
			float height = 0;
			if (!Pro) {
				if (UserInterfaceIdiomIsPhone) {
					height = UIScreen.MainScreen.Bounds.Bottom - 160;
					width = UIScreen.MainScreen.Bounds.Width;
				}
				else {
					height = UIScreen.MainScreen.Bounds.Bottom - 160;
					width = UIScreen.MainScreen.Bounds.Width / 5 * 2;
				}
			}
			else {
				if (UserInterfaceIdiomIsPhone) {
					height = UIScreen.MainScreen.Bounds.Bottom - 100;
					width = UIScreen.MainScreen.Bounds.Width;
				}
				else {
					height = UIScreen.MainScreen.Bounds.Bottom - 160;
					width = UIScreen.MainScreen.Bounds.Width / 5 * 2;
				}
			}
			dyn = new RectangleF (x, y, width, height);
			return dyn;
		}

		public static string[] SaveImage (string name, UIImage ourpic)
		{
			if(ourpic == null)
				return new string[2]{"",""};
			Console.WriteLine ("Save");
			UIImage thumbPic = ImageHelper.ResizeImage(ourpic, 50,50); //measurements taken from CustomCell, alternatly 33x33

			if (ourpic != null) {
				var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
				var picname = name + ".png";
				var thumbpicname = name + "_thumb.png";
				string pngfileName = System.IO.Path.Combine (documentsDirectory, picname);
				string thumbpngfileName = System.IO.Path.Combine (documentsDirectory, thumbpicname);
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

        public static void DeleteImage(string name){
            var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
            var picname = name + ".png";
            var thumbpicname = name + "_thumb.png";
            string pngfileName = System.IO.Path.Combine (documentsDirectory, picname);
            string thumbpngfileName = System.IO.Path.Combine (documentsDirectory, thumbpicname);

            NSFileManager fm = new NSFileManager();

            NSError err = null;

            if (fm.IsDeletableFile(pngfileName))
            {
                fm.Remove(pngfileName, out err);   
                //TODO use error for something sensible
            }

            err = null;

            if (fm.IsDeletableFile(thumbpngfileName))
            {
                fm.Remove(thumbpngfileName, out err );
                //TODO use errormsg for something sensible.
            }
        }

		public RectangleF ImageRectangle{ get; set;}

		public UIImageView LoadImage(PointF origin,string filename){
			UIImageView view = new UIImageView();
			Console.WriteLine("loadImage():"+filename);
			if (!string.IsNullOrEmpty (filename)) {
				var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
				string ffilename = System.IO.Path.Combine (documentsDirectory, filename);
				UIImage image = UIImage.FromFile (ffilename);
				if (image != null) {
					if (UserInterfaceIdiomIsPhone) {
						ImageRectangle = new RectangleF (origin.X, origin.Y, 300, 200);
					} else {
						ImageRectangle = new RectangleF (origin.X, origin.Y, 500, 500);
					}
					view = new UIImageView (ImageRectangle);
					view.Image = image;
					return view;
				} else {
					return new UIImageView (ImageRectangle);
				}
			} else
				return new UIImageView (ImageRectangle);
		}

//		public static void InitializeImagePicker (RectangleF imageRect,RectangleF PickerReckt, UIView myview)
//		{
//			
//			var picky = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Select an image", "Select an image");
//			
//			//			View.BackgroundColor = UIColor.White;
//			imageView = new UIImageView (imageRect);
//			Add (imageView);
//			choosePhotoButton = UIButton.FromType (UIButtonType.RoundedRect);
//			choosePhotoButton.Frame = PickerReckt;
//			choosePhotoButton.SetTitle (picky, UIControlState.Normal);
//			Xamarin.Themes.BlackLeatherTheme.Apply (choosePhotoButton, "");
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
//			
//			myview.Add (choosePhotoButton);
//		}


	}
}

