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
	public class ImageController : UIViewController
	{

		UIImagePickerController imagePicker;
		UIImageView imageView;
		public UIImage OutputImage{ get; set;}
		public string InitialImageFileName{ get; set;}
		RectangleF btnRemoveImgRect;
		RectangleF btnBigPickImageRect;

		public UIPopoverController Pc;


		public event EventHandler<GotPictureEventArgs> GotPicture;

		public event EventHandler<DerezLargeObjectEventArgs> Derezzy;

		public event EventHandler<SavedImageStringsEventArgs> ImageSaved;

		Lager inputLager;
		LagerObject inputLagerObject;
		Item inputItem;

		UIButton btnRemoveImg;
		UIButton btnBigPickImage;

		RectangleF myFrame;

		public ImageController (Lager inputLager, RectangleF myFrame)
		{
			this.inputLager = inputLager;
			this.myFrame = myFrame;
		}

		public ImageController(LagerObject inputLagerObject, RectangleF myFrame){
			this.inputLagerObject = inputLagerObject;
			this.myFrame = myFrame;
		}

		public ImageController(Item inputItem, RectangleF myFrame){
			this.inputItem = inputItem;
			this.myFrame = myFrame;
		}

		//Bold attempt
		public void ResetImageView(){
			Console.WriteLine ("reset image");
			if(imageView != null){
				imageView.Image = null;
			}
		}

	

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			//if stuff is loaded but not active a window it will be cleaned out.
			if(this.IsViewLoaded && this.View.Window == null){
				Cleanup ();
			}
		}

		public void Cleanup ()
		{
			//			this.View = null;
			//			dao = null;
			imagePicker = null;
			//			choosePhotoButton = null;
		}

		public void SetImageViewImage (UIImageView imageholder)
		{
			Console.WriteLine ("setImageViewImage");
			if(imageholder != null){

				Console.WriteLine ("Not null");
				this.imageView = imageholder;
				//				if(imageholder.Image != null){
				//				this.scrollContent.ContentSize = imageholder.Image.Size;
				//
				//				this.scrollContent.AutosizesSubviews = true;
				//				this.scrollContent.BringSubviewToFront(imageholder);
				//				this.scrollContent.AddSubview (imageholder);
				Add (imageholder);
			}
		}

		public override void LoadView ()
		{
			base.LoadView ();
			this.View.Frame = myFrame;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			ShowDetails (inputLager.ImageFileName);
			InitImageRectangles ();
			InitializeImagePicker ();
			InitializeUnpickImage();
		}

		const float margin = 10;
		const int lineheight = 30;
		const int broad = 300;
		const int iphone_qube = 300;
		const int ipad_qube = 800;
		const int counter = 0;
		const int linebuffer = 10;
		const int tagsheight = 0;
		int x = 0;
		int y = 0;
		readonly float picturebroad = this.myFrame.Width - 20f;

		public void InitLegacyNib(){
			btnRemoveImgRect = new RectangleF(x, y + lineheight * counter + linebuffer * counter + tagsheight, broad / 2, lineheight);
			btnBigPickImageRect = new RectangleF(x + broad / 2, y + lineheight * counter + linebuffer * counter + tagsheight, broad / 2, lineheight);
			btnRemoveImg = new UIButton (UIButtonType.RoundedRect);
			btnRemoveImg.Frame = btnRemoveImgRect;
			Add (btnRemoveImg);

			btnBigPickImage = new UIButton (UIButtonType.RoundedRect);
			btnBigPickImage.Frame = btnBigPickImageRect;
			Add (btnBigPickImage);
		}

		void InitImageRectangles ()
		{
			if (UserInterfaceIdiomIsPhone) {
				ImageRectangle = new RectangleF (margin, lineheight + margin, picturebroad, iphone_qube);
				//				PickerRect = new RectangleF (10, 100, 175, 30);
				//                UnPickerRect = new RectangleF(175, 100, 150, 55);
			}
			else {
				//Ipad measures for this screen
				ImageRectangle = new RectangleF (margin, lineheight + margin, picturebroad, ipad_qube);
				//				PickerRect = new RectangleF (10, 100, 200, 30);
				//                UnPickerRect = new RectangleF(210, 100, 200, 55);
			}
		}

		public void ShowDetails(string imagefilename){
			ResetImageView();
			PointF zulu = new PointF (this.ImageRectangle.X, this.ImageRectangle.Y);
			Console.WriteLine ("zulu:"+zulu.ToString());
		
			if (!string.IsNullOrEmpty(imagefilename)) {
				UIImageView imageholder = this.LoadImage (zulu, imagefilename);
				this.SetImageViewImage (imageholder);
				makeCornersRound ();
			}
			

		}

		void RaiseSavedImageEvent (string imagefilename, string thumbfilename)
		{
			var handler = this.ImageSaved;
			if (handler != null) {
				handler(this,new SavedImageStringsEventArgs(imagefilename, thumbfilename));
			}
		}

		private void mySavePicture(UIImage image){
			if (inputLager != null) {
				image = image.Scale(image.Size);
				string[] output = SaveImage (RandomGeneratedName(), image);
				RaiseSavedImageEvent (output[0], output[1]);
			}
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

		void makeCornersRound(){
			if (this.imageView != null) {
				this.imageView.Layer.CornerRadius = 7;
				this.imageView.ClipsToBounds = true;
			}
		}

		void RaiseDerez(){
			var handler = this.Derezzy;
			if (handler != null){
				handler(this, new DerezLargeObjectEventArgs());
			}
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			GotPicture += (object sender, GotPictureEventArgs e) => this.imageView.Image = e.image;
		}


		public void SelectSource(){
			var source = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("pick image from where?", "pick image from where?");
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
				PresentViewControllerAsync (imagePicker, true);
				//nc.PresentViewController (imagePicker, true, delegate {});
			}else{
				Console.WriteLine("Popover");
				Pc = new UIPopoverController(imagePicker);
				Pc.PresentFromRect(this.btnBigPickImage.Frame,(UIView) this.View, UIPopoverArrowDirection.Up, true);
			}
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

			if(isImage) {
			UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
			if (originalImage != null) {

				RaiseImageGotten (originalImage);
			} else {

				UIImage editedImage = e.Info [UIImagePickerController.EditedImage] as UIImage;
				if (editedImage != null) {
					RaiseImageGotten (editedImage);
				}
			}

			}

			if(UserInterfaceIdiomIsPhone){
				imagePicker.DismissViewController (true, delegate{});
			}else{
				Pc.Dismiss(false);
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
					PresentViewControllerAsync (imagePicker, true);
					//nc.PresentViewController (imagePicker, true, delegate{});
				}
				else {
					Console.WriteLine ("Popover");
					Pc = new UIPopoverController (imagePicker);
					Pc.PresentFromRect (this.btnBigPickImage.Frame, (UIView)this.View, UIPopoverArrowDirection.Up, true);
				}
			}

			UIActionSheet actionSheet;

			public void InitializeUnpickImage(){
				var unpicky = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("remove picture", "remove picture");
				//            unselectPhotoButton = UIButton.FromType(UIButtonType.RoundedRect);
				//            unselectPhotoButton.Frame = UnPickerRect;
				this.btnRemoveImg.SetTitle(unpicky, UIControlState.Normal);
				//            Xamarin.Themes.BlackLeatherTheme.Apply(unselectPhotoButton);

				btnRemoveImg.TouchUpInside += (sender, e) => {
					var rmText = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("remove picture?","remove picture?");
					var rmDel = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Delete", "Delete");
					var rmCancel = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Cancel", "Cancel");
					actionSheet = new UIActionSheet(rmText, null, rmCancel, rmDel, null);
					actionSheet.Clicked += delegate(object a, UIButtonEventArgs b) {
						Console.WriteLine("button" + b.ButtonIndex.ToString() + " clicked");
						if(b.ButtonIndex == 0){
							Console.WriteLine("deleting picture!");
							//DELETE IMAGE FROM APP.
							DeletePic();
							//POP this view to refresh.
							if(UserInterfaceIdiomIsPhone){
								DismissViewControllerAsync(true);
								//nc.PopViewControllerAnimated(true);
							}else{
								RaiseDerez();
							}
						}
					};
					actionSheet.ShowInView(this.View);
				};

				//            View.AddSubview(unselectPhotoButton);
			}


			// Do something when the 
			void Handle_Canceled (object sender, EventArgs e) {
				Console.WriteLine ("picker cancelled");
				imagePicker.DismissViewController(true, delegate {});
			}

		public void DeletePic(){
			if (inputLager != null)
			{
				DeleteImage(inputLager.Name);
				imageView.Image = null;
				inputLager.ImageFileName = null;
				inputLager.thumbFileName = null;
				AppDelegate.dao.SaveLager (inputLager);
			}
		}

		//        public void SetImageViewImage(UIImage image){
		//            if(imageView == null){
		//                imageView = new UIImageView(ImageRectangle);
		//            }
		//            this.imageView.Image = image;
		//        }

		void RaiseImageGotten (UIImage image)
		{
			mySavePicture(image); //local event

			this.imageView.Image = null;

			var handler = this.GotPicture;
			if (handler != null && image != null) {
				handler(this, new GotPictureEventArgs(image));
			}
		}


		public void InitializeImagePicker ()
		{

			var picky = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("pick image", "pick image");
			imageView = new UIImageView (ImageRectangle);
			btnBigPickImage.SetTitle (picky, UIControlState.Normal);
			btnBigPickImage.TouchUpInside += (s, e) => SelectSource ();
		}

		public static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public static string[] SaveImage (string name, UIImage ourpic)
		{
			if(ourpic == null)
				return new string[2]{"",""};
			Console.WriteLine ("Save");
			UIImage thumbPic = ourpic.Scale(new SizeF(50,50)); //measurements taken from CustomCell, alternatly 33x33

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




	}
}

