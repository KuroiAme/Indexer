using System;
using no.dctapps.Garageindex.model;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.screens;
using no.dctapps.Garageindex.events;
using No.Dctapps.Garageindex.Ios.Screens;
using System.Drawing;
using System.Text;
using GoogleAdMobAds;

namespace GarageIndex
{
	public class BigItemDetailContent : UtilityViewController
	{

		LagerObject myObject;

		UIImagePickerController imagePicker;
		//		UIButton choosePhotoButton;
		//        UIButton unselectPhotoButton;

		UIImageView imageView;
		public UIImage OutputImage{ get; set;}
		public string InitialImageFileName{ get; set;}

		//		public RectangleF ir{ get; set;}
		//		public RectangleF PickerRect{ get; set;}
		//        public RectangleF UnPickerRect{ get; set;}

		//		SelectContainer sc;
		SelectLager sl;
		public UIPopoverController Ic;

		public UIPopoverController Pc;

		public event EventHandler<GotPictureEventArgs> GotPicture;
		public event EventHandler<BigItemSavedEventArgs> BigItemSaved;
		public event EventHandler<DerezLargeObjectEventArgs> Derezzy;

		UINavigationController nc;

		public BigItemDetailContent (LagerObject myObject,UINavigationController nc)
		{
			this.nc = nc;
			this.myObject = myObject;
		}


		void InitImageRectangles ()
		{
			if (UserInterfaceIdiomIsPhone) {
				ImageRectangle = new RectangleF (10, 350, 300, 300);
				//				PickerRect = new RectangleF (10, 100, 175, 30);
				//                UnPickerRect = new RectangleF(175, 100, 150, 55);
			}
			else {
				//Ipad measures for this screen
				ImageRectangle = new RectangleF (10, 400, 800, 800);
				//				PickerRect = new RectangleF (10, 100, 200, 30);
				//                UnPickerRect = new RectangleF(210, 100, 200, 55);
			}
		}


		public override void LoadView ()
		{
			base.LoadView ();
			InitView ();
			InitImageRectangles ();
			InitLegacyNib ();
			InitInsuranceInfo ();
		}

		private void InitView(){
			this.View.BackgroundColor = UIColor.White;
			//			this.View.Frame = 
			if (UserInterfaceIdiomIsPhone) {
				this.View.Frame = new RectangleF (0, 0, UIScreen.MainScreen.Bounds.Width, 800);
			} else {
				this.View.Frame = new RectangleF (0, 0, UIScreen.MainScreen.Bounds.Width, 1000);
			}
		}

		public SizeF GetContentsize ()
		{
			return this.View.Bounds.Size;
		}

		public void Cleanup ()
		{
			//			this.View = null;
			//			dao = null;
			imagePicker = null;
			//			choosePhotoButton = null;
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

		//Bold attempt
		public void ResetImageView(){
			Console.WriteLine ("reset image");
			if(imageView != null){
				imageView.Image = null;
			}
		}
		UITextField fieldBigName;
		UITextField fieldBigDescription;
		UIButton btnIn;
		UIButton btnRemoveImg;
		UIButton btnBigPickImage;

		public void InitLegacyNib(){
			RectangleF fieldBigNameRect;
			RectangleF fieldBIgDescriptionRect;
			RectangleF btnInRect;
			RectangleF btnRemoveImgRect;
			RectangleF btnBigPickImageRect;

			float x;
			float y;
			if (UserInterfaceIdiomIsPhone) {
				x = 10;
				y = 50; 
			} else {
				x = 30;
				y = 80;
			}
			const float margin = 0;
			const float broad = 250;
			const float lineheight = 30;
			const float linebuffer = 10;
			const float tagsheight = 150;
			int counter = 0;
			fieldBigNameRect = new RectangleF (x, y + lineheight * counter + linebuffer * counter, broad, lineheight);
			counter++;
			fieldBIgDescriptionRect = new RectangleF(x, y + lineheight * counter + linebuffer * counter, broad, lineheight);
			counter++;
			btnInRect = new RectangleF(x, y + lineheight * counter + linebuffer * counter +tagsheight, broad, lineheight);
			counter++;
			btnRemoveImgRect = new RectangleF(x, y + lineheight * counter + linebuffer * counter + tagsheight, broad / 2, lineheight);
			btnBigPickImageRect = new RectangleF(x + broad / 2, y + lineheight * counter + linebuffer * counter + tagsheight, broad / 2, lineheight);

			fieldBigName = new UITextField (fieldBigNameRect);
			fieldBigName.BorderStyle = UITextBorderStyle.RoundedRect;
			Add (fieldBigName);

			fieldBigDescription = new UITextField (fieldBIgDescriptionRect);
			fieldBigDescription.BorderStyle = UITextBorderStyle.RoundedRect;
			Add (fieldBigDescription);

			btnIn = new UIButton (UIButtonType.RoundedRect);
			btnIn.Frame = btnInRect;
			Add (btnIn);

			btnRemoveImg = new UIButton (UIButtonType.RoundedRect);
			btnRemoveImg.Frame = btnRemoveImgRect;
			Add (btnRemoveImg);

			btnBigPickImage = new UIButton (UIButtonType.RoundedRect);
			btnBigPickImage.Frame = btnBigPickImageRect;
			Add (btnBigPickImage);
		}

		UIStepper antallStepper;
		UITextField cashValue;
		UILabel antall;
		UILabel currency;
		UIButton showReceipts;

		private void InitInsuranceInfo(){
			RectangleF antallStepperRect;
			RectangleF cashValueRect;
			RectangleF antallRect;
			RectangleF currencyRect;
			RectangleF showReceiptsRect;
			float x;
			float y;
			if (UserInterfaceIdiomIsPhone) {
				//x and y under the representative image;
				x = 30;
				y = 650;
			} else {
				//on the right in a second collumn on iPad
				x = 500;
				y = 80;
			}
			const float margin = 0;
			const float broad = 100;
			const float lineheight = 30;
			const float linebuffer = 10;
			antallRect = new RectangleF (x, y, broad, lineheight);
			antallStepperRect = new RectangleF (x + broad, y, broad, lineheight);

			currencyRect = new RectangleF (x + margin, y + linebuffer + lineheight, broad, lineheight);
			cashValueRect = new RectangleF (x + margin +broad, y + linebuffer + lineheight, broad, lineheight);
			showReceiptsRect = new RectangleF (x + margin, y + linebuffer * 2 + lineheight * 2, broad * 2, lineheight);

			antall = new UILabel (antallRect);
			Add (antall);
			antallStepper = new UIStepper (antallStepperRect);
			Add (antallStepper);
			currency = new UILabel (currencyRect);
			Add (currency);
			this.cashValue = new UITextField (cashValueRect);
			//this.cashValue.KeyboardType = UIKeyboardType.DecimalPad; ///this pissant keyboard has no dissmiss dammit
			Add (cashValue);

			showReceipts = new UIButton (UIButtonType.RoundedRect);
			showReceipts.Frame = showReceiptsRect;
			Add (showReceipts);
		}

		public void ShowDetails (LagerObject myobj)
		{
			ResetImageView();
			//TODO remember to deselect left table after showing details
			myObject = myobj;
			PointF zulu = new PointF (this.ImageRectangle.X, this.ImageRectangle.Y);
			Console.WriteLine ("zulu:"+zulu.ToString());
			Title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Big Items Details", "Big Items Details");
			this.fieldBigName.Placeholder = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Name", "Name");
			this.fieldBigDescription.Placeholder = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Description", "Description");
			var count = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Count", "Count");
			count = count + ":";
			if (myobj != null) {
				Console.WriteLine("ID:"+myobj.ID);
				if(myobj.imageFileName != null){
					UIImageView imageholder = this.LoadImage(zulu, myobj.imageFileName);
					this.SetImageViewImage(imageholder);
				}
				this.fieldBigName.Text = myobj.Name;
				this.fieldBigName.Font = UIFont.FromName ("Cochin-BoldItalic", 22f);
				this.fieldBigDescription.Text = myobj.Description;
				this.fieldBigDescription.Font = UIFont.FromName ("AmericanTypewriter", 12f);

				antall.Text = count + myObject.antall.ToString ();
			}
			AddTagList ();
			releaseKeyboard ();

			if (myObject != null) {
				this.antallStepper.Value = myObject.antall;
				this.antallStepper.ValueChanged += (object sender, EventArgs e) => {
					double ant = this.antallStepper.Value;
					antall.Text = count + ant;
					myObject.antall = ant;
					AppDelegate.dao.SaveLagerObject (myObject);
				};
				cashValue.Text = myObject.cashValue.ToString ();
				cashValue.ValueChanged += (object sender, EventArgs e) => {
					try {
						double newvalue = double.Parse (cashValue.Text);
						myObject.cashValue = newvalue;
						AppDelegate.dao.SaveLagerObject (myObject);
					} catch (Exception ex) {
						Console.WriteLine ("coudlnt parse;" + cashValue.Text + "ex:" + ex.ToString ());
						cashValue.Text = myObject.cashValue.ToString ();
					}
				};
			}

			currency.Text = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Currency", "Currency");

			showReceipts.SetTitle (MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Show Receipts", "Show Receipts"), UIControlState.Normal);
			showReceipts.TouchUpInside += (object sender, EventArgs e) => {
				InsurancePhotoController ipc = new InsurancePhotoController(myobj);
				nc.PushViewController(ipc,false);
			};

			makeCornersRound ();

		}

		void initializeMoveLager ()
		{
			Console.WriteLine("initializemovelager");
			sl = new SelectLager ();

			this.btnIn.TouchUpInside += (object sender, EventArgs e) =>  {
				Console.WriteLine("touchupinside");
				if(UserInterfaceIdiomIsPhone){
					Console.WriteLine("iphone??");
					nc.PushViewController(sl, true);
				}else{
					Console.WriteLine("ipad??");
					Ic = new UIPopoverController (sl);
					Ic.PresentFromRect (this.btnIn.Bounds, this.View, UIPopoverArrowDirection.Up, true);
				}
			};
			sl.DismissEvent += (object sender, LagerClickedEventArgs e) =>  {
				Console.WriteLine("dismiss?");
				if(UserInterfaceIdiomIsPhone){
					nc.PopViewControllerAnimated(true);
				}else{
					Ic.Dismiss (true);
				}
				if(myObject != null){
					this.myObject.LagerID = e.Lager.ID;
					SetLagerButtonLabel (this.myObject);
					AppDelegate.dao.SaveLagerObject(this.myObject);
					RaiseSavedEvent();
				}
			};
		}

		public void SetLagerButtonLabel (LagerObject itty)
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append (MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("In", "In"));
			sb.Append (":");
			if (itty != null) {
				Lager lager = AppDelegate.dao.GetLagerById (itty.LagerID);
				//				boks.LagerID = lo.ID;
				//				SaveIt ();
				if (lager != null) {
					if (!string.IsNullOrEmpty (lager.Name)) {
						sb.Append (lager.Name);
					}
				}
			}
			this.btnIn.SetTitle (sb.ToString (), UIControlState.Normal);
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

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			ShowDetails (this.myObject);

			InitializeImagePicker ();
			InitializeUnpickImage();

			SetLagerButtonLabel (this.myObject);
			initializeMoveLager ();

		}


		void AddTagList ()
		{
			RectangleF frame;
			ImageTag tag = null;

			if (UserInterfaceIdiomIsPhone) {
				frame = new RectangleF (30, 150, 300, 125);
			} else {
				frame = new RectangleF (30, 170, 300, 125);
			}

			Console.WriteLine("frame:"+frame);
			if (myObject != null) {
				tag = AppDelegate.dao.GetImageTagById (this.myObject.ImageTagId);
			}
			if (tag == null) {
				Console.WriteLine ("Tag er null");
				tag = new ImageTag ();
				int key = AppDelegate.dao.SaveTag (tag);
				tag.ID = key;
				if (myObject != null) {
					this.myObject.ImageTagId = key;
					AppDelegate.dao.SaveLagerObject (myObject);
				}
			}

			TagListController tlc = new TagListController (tag, frame);
			this.View.AddSubview (tlc.View);

		}


		void releaseKeyboard ()
		{
			this.fieldBigName.ShouldReturn += textField =>  {
				textField.ResignFirstResponder ();
				return true;
			};
			this.fieldBigDescription.ShouldReturn += textField =>  {
				textField.ResignFirstResponder ();
				return true;
			};
			this.cashValue.ShouldReturn += textField =>  {
				textField.ResignFirstResponder ();
				return true;
			};
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			ShowDetails (this.myObject);

			this.fieldBigName.Ended += (object sender, EventArgs e) => this.SaveIt ();

			this.fieldBigDescription.Ended += (object sender, EventArgs e) => this.SaveIt ();

			GotPicture += (object sender, GotPictureEventArgs e) => this.imageView.Image = e.image;
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			this.SaveIt();
		}

		void RaiseSavedEvent ()
		{
			var handler = this.BigItemSaved;
			Console.WriteLine("saved");
			if (handler != null) {
				handler(this, new BigItemSavedEventArgs());
			}
		}

		public void SaveIt(){
			if(myObject != null){
				myObject.Name = this.fieldBigName.Text;
				myObject.Description = this.fieldBigDescription.Text;
				AppDelegate.dao.SaveLagerObject (myObject);
				RaiseSavedEvent();
			}
		}

		public void DeletePic(){
			if (myObject != null)
			{
				DeleteImage(myObject.Name);
				imageView.Image = null;
				myObject.imageFileName = null;
				myObject.thumbFileName = null;
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

		private void mySavePicture(UIImage image){
			if (this.fieldBigName.Text != null)
				myObject.Name = this.fieldBigName.Text;
			if (this.fieldBigDescription.Text != null)
				myObject.Description = this.fieldBigDescription.Text;
			image = image.Scale(image.Size);
			string[] output = SaveImage (myObject.Name, image);
			myObject.imageFileName = output [0];
			myObject.thumbFileName = output [1];
			AppDelegate.dao.SaveLagerObject (myObject);
			RaiseSavedEvent ();
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

		public void InitializeImagePicker ()
		{

			var picky = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("pick image", "pick image");
			imageView = new UIImageView (ImageRectangle);
			btnBigPickImage.SetTitle (picky, UIControlState.Normal);
			btnBigPickImage.TouchUpInside += (s, e) => SelectSource ();
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
				nc.PresentViewController (imagePicker, true, delegate {});
			}else{
				Console.WriteLine("Popover");
				Pc = new UIPopoverController(imagePicker);
				Pc.PresentFromRect(this.btnBigPickImage.Frame,(UIView) this.View, UIPopoverArrowDirection.Up, true);
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
				nc.PresentViewController (imagePicker, true, delegate{});
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
							nc.PopViewControllerAnimated(true);
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

	}
}

