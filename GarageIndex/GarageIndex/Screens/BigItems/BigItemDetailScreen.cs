using System;
using System.Drawing;
using System.Text;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.model;
using GarageIndex;
using no.dctapps.Garageindex.screens;
using no.dctapps.Garageindex.events;
using MonoTouch.MessageUI;
using GoogleAnalytics.iOS;


namespace No.Dctapps.Garageindex.Ios.Screens
{
	public partial class BigItemDetailScreen : UtilityViewController
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
        public event EventHandler<DerezEventArgs> Derezzy;

		public BigItemDetailScreen (LagerObject myObject)
			: base (UserInterfaceIdiomIsPhone ? "BigItemDetailScreen_iPhone" : "BigItemDetailScreen_iPad")
		{
//			dao = new LagerDAO();
//			bl = new GarageindexBL ();

			this.myObject = myObject;

//			InitialImageFileName = myObject.imageFileName;

			if(UserInterfaceIdiomIsPhone){
				ImageRectangle = new RectangleF (10, 300, 500, 275);
//				PickerRect = new RectangleF (20, 95, 130, 55);
//                UnPickerRect = new RectangleF(150, 95, 150, 55);
			}else{
				ImageRectangle = new RectangleF (100, 300, 500, 400);
//				PickerRect = new RectangleF (100, 120, 200, 55);
//                UnPickerRect = new RectangleF(320, 170, 200, 55);
			}
		}

		public BigItemDetailScreen ()
			: base (UserInterfaceIdiomIsPhone ? "BigItemDetailScreen_iPhone" : "BigItemDetailScreen_iPad")
		{
			//empty use with Con()
//			dao = new LagerDAO();
//			bl = new GarageindexBL ();

			if(UserInterfaceIdiomIsPhone){
				ImageRectangle = new RectangleF (10, 220, 200, 200);
//				PickerRect = new RectangleF (20, 95, 250, 45);
//                UnPickerRect = new RectangleF(150, 95, 150, 55);
			}else{
				ImageRectangle = new RectangleF (50, 300, 500, 500);
//				PickerRect = new RectangleF (50, 120, 250, 45);
//                UnPickerRect = new RectangleF(320, 120, 200, 55);
			}
		}

		MFMailComposeViewController mailContr;

		private void CreateEmailBarButton (LagerObject myobby)
		{
			//DO NOT DELETE
			if (this.myObject != null) {
			UIBarButtonItem it = new UIBarButtonItem ();
			it.Title = "email";
			//IS really info
			it.Clicked += (object sender, EventArgs e) =>  {
				mailContr = new MFMailComposeViewController();
					mailContr.SetSubject(AppDelegate.bl.GenerateSubject(myobby));
					mailContr.SetMessageBody(AppDelegate.bl.GenerateContainerManifest(myobby),false);
					AppDelegate.bl.AddPictureAttachment(mailContr, myobby);
					AppDelegate.bl.AddQRPictureAttachment(mailContr, myobby);
				this.PresentViewController(mailContr, true, delegate{});
				
				mailContr.Finished += (object sender2, MFComposeResultEventArgs e2) => mailContr.DismissViewController (true, delegate {});
			};
			

			this.NavigationItem.SetRightBarButtonItem (it, true);
			}
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Big items detail Screen");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
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
//		PointF origsin;
		/// <summary>
		/// Defines the origin point of the imageview in x,y coords.
		/// </summary>
//		void getOrigin ()
//		{
//			if (UserInterfaceIdiomIsPhone) {
//				origin = new PointF (10, 150);
//			}
//			else {
//				//iPad
//				origin = new PointF (30, 200);
//			}
//		}

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
				AddTagList ();
			}
			releaseKeyboard ();
			CreateEmailBarButton (myobj);


		}

		void initializeMoveLager ()
		{
			Console.WriteLine("initializemovelager");
			sl = new SelectLager ();

			this.btnIn.TouchUpInside += (object sender, EventArgs e) =>  {
				Console.WriteLine("touchupinside");
				if(UserInterfaceIdiomIsPhone){
					Console.WriteLine("iphone??");
					this.NavigationController.PushViewController(sl, true);
				}else{
					Console.WriteLine("ipad??");
					Ic = new UIPopoverController (sl);
					Ic.PresentFromRect (this.btnIn.Bounds, this.View, UIPopoverArrowDirection.Up, true);
				}
			};
			sl.DismissEvent += (object sender, LagerClickedEventArgs e) =>  {
				Console.WriteLine("dismiss?");
				if(UserInterfaceIdiomIsPhone){
					NavigationController.PopToViewController(this, true);
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
			if (UserInterfaceIdiomIsPhone) {
				frame = new RectangleF (30, 150, UIScreen.MainScreen.Bounds.Width, 125);
			} else {
				frame = new RectangleF (30, 200, UIScreen.MainScreen.Bounds.Width, 125);
			}

			Console.WriteLine("frame:"+frame);
			ImageTag tag = AppDelegate.dao.GetImageTagById (this.myObject.ImageTagId);
			if (tag == null) {
				Console.WriteLine ("Tag er null");
				tag = new ImageTag ();
				int key = AppDelegate.dao.SaveTag (tag);
				tag.ID = key;
				this.myObject.ImageTagId = key;
				AppDelegate.dao.SaveLagerObject (myObject);
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
            string[] output = SaveImage (myObject.Name, image);
            myObject.imageFileName = output [0];
            myObject.thumbFileName = output [1];
			AppDelegate.dao.SaveLagerObject (myObject);
			RaiseSavedEvent ();
        }

        void RaiseDerez(){
            var handler = this.Derezzy;
            if (handler != null){
                handler(this, new DerezEventArgs());
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
				NavigationController.PresentViewController (imagePicker, true, delegate {});
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
				NavigationController.PresentViewController (imagePicker, true, delegate{});
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
                            this.NavigationController.PopViewControllerAnimated(true);
                        }else{
                            RaiseDerez();
                        }
                    }
                };
                actionSheet.ShowFromTabBar(this.TabBarController.TabBar);
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


