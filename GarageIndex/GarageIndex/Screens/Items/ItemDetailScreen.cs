using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.events;
using System.Text;
using no.dctapps.Garageindex.model;
using No.Dctapps.GarageIndex;
using MonoTouch.MessageUI;
using GarageIndex;

namespace no.dctapps.Garageindex.screens
{
	public partial class ItemDetailScreen : UtilityViewController
	{
//		LagerDAO dao;
//		GarageindexBL bl;

		private Item item;

		UIImagePickerController imagePicker;
//		UIButton choosePhotoButton;
//        UIButton unselectPhotoButton;
	
		UIImageView imageView;
		public UIImage OutputImage{ get; set;}
		
//		public RectangleF ImageRectangle{ get; set;}
//		public RectangleF PickerRect{ get; set;}
//        public RectangleF UnPickerRect{ get; set;}
		
		public UIPopoverController Pc;
		public UIPopoverController Ic;

		public event EventHandler<GotPictureEventArgs> GotPicture;

		public event EventHandler<ItemSavedEventArgs> ItemSaved;
        public event EventHandler<DerezEventArgs> Derez;

		public ItemDetailScreen (Item item)
			: base (UserInterfaceIdiomIsPhone ? "ItemDetailScreen_iPhone" : "ItemDetailScreen_iPad")
		{
			this.item = item;

			initRectangles ();
		}

		public ItemDetailScreen ()
			: base (UserInterfaceIdiomIsPhone ? "ItemDetailScreen_iPhone" : "ItemDetailScreen_iPad")
		{
			
			initRectangles ();
		}

		void initRectangles ()
		{
			if (UserInterfaceIdiomIsPhone) {
				ImageRectangle = new RectangleF (10, 200, 300, 300);
//				PickerRect = new RectangleF (10, 100, 175, 30);
//                UnPickerRect = new RectangleF(175, 100, 150, 55);
			}
			else {
				//Ipad measures for this screen
				ImageRectangle = new RectangleF (10, 200, 800, 800);
//				PickerRect = new RectangleF (10, 100, 200, 30);
//                UnPickerRect = new RectangleF(210, 100, 200, 55);
			}
		}
//

		void cleanup ()
		{
//			item = null;
			imagePicker = null;
//			choosePhotoButton = null;
//			imageView = null;
			OutputImage = null;
			Pc = null;
//			dao = null;
		}

		void Unclean ()
		{
//			this.item = new Item();
//			this.dao = new LagerDAO();
			//initRectangles();
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
           base.DidReceiveMemoryWarning ();

			if(this.IsViewLoaded && this.View.Window == null){
				cleanup ();

			}
			// Release any cached data, images, etc that aren't in use.
		}

		void releaseKeyboard ()
		{
			this.fieldName.ShouldReturn += textField =>  {
				textField.ResignFirstResponder ();
				return true;
			};
			this.fieldDescription.ShouldReturn += textField =>  {
				textField.ResignFirstResponder ();
				return true;
			};
		}

		void SetContainerButtonLabel (Item itty)
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append (NSBundle.MainBundle.LocalizedString ("In", "In"));
			sb.Append (":");
			if (itty != null) {
				LagerObject lo = AppDelegate.dao.getContainerById (itty.boxID);
				if (lo != null) {
					if (!string.IsNullOrEmpty (lo.Name)) {
						sb.Append (lo.Name);
					}
				}
			}
			this.btnInContainer.SetTitle (sb.ToString (), UIControlState.Normal);
		}

		public void ShowDetails (Item myItem)
		{
            this.ResetImageView();

			if(myItem == null)
			{
				myItem = new Item();
			}

			this.item = myItem;

			SetContainerButtonLabel (myItem);

			if(myItem != null){
                float x = this.ImageRectangle.X;
                float y = this.ImageRectangle.Y;
				UIImageView imageholder  = LoadImage(new PointF(x,y),myItem.ImageFileName);
				this.SetImageViewImage(imageholder);
				this.item = myItem;

				this.fieldName.Text = myItem.Name;
				this.fieldDescription.Text = myItem.Description;
			
			
				if (myItem.ImageFileName != null) {
					var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
					string filename = System.IO.Path.Combine (documentsDirectory, myItem.ImageFileName);
					this.imageView.Image = UIImage.FromFile (filename);
				}
			}
		}

		public void SetImageViewImage (UIImageView imageholder)
		{
			Console.WriteLine ("setImageViewImage");
			if(imageholder != null){

				Console.WriteLine ("Not null");
				this.imageView = imageholder;
				Add (imageholder);
			}
		}

//		void HandleTouchUpInside (object sender, EventArgs e)
//		{
//			this.SaveIt();
//			this.NavigationController.PopViewControllerAnimated(true);
//		}

		SelectContainer sc;

		void initializeMoveContainer ()
		{

			sc = new SelectContainer ();

			this.btnInContainer.TouchUpInside += (object sender, EventArgs e) =>  {
				if(UserInterfaceIdiomIsPhone){
					this.NavigationController.PushViewController(sc, true);
				}else{
					Ic = new UIPopoverController (sc);
					Ic.PresentFromRect (this.btnInContainer.Bounds, this.View, UIPopoverArrowDirection.Up, true);
				}
			};
			sc.DismissEvent += (object sender, ContainerClickedEventArgs e) =>  {
				if(UserInterfaceIdiomIsPhone){
					NavigationController.PopToViewController(this, true);
				}else{
					Ic.Dismiss (true);
				}
				int id = this.item.ID;
				this.item.boxID = e.container.ID;
				SetContainerButtonLabel (this.item);
				AppDelegate.dao.SaveItem(this.item);
			};
		}

		MFMailComposeViewController mailContr;

		private void CreateEmailBarButton ()
		{
			//DO NOT DELETE
			UIBarButtonItem it2 = new UIBarButtonItem ();
			it2.Title = "email";
			//IS really info
			it2.Clicked += (object sender, EventArgs e) =>  {
				mailContr = new MFMailComposeViewController();
				mailContr.SetSubject(AppDelegate.bl.GenerateSubject(this.item));
				mailContr.SetMessageBody(AppDelegate.bl.GenerateManifest(this.item),false);
				AppDelegate.bl.AddPictureAttachment(mailContr, this.item);
				this.PresentViewController(mailContr, true, delegate{});
				mailContr.Finished += (object sender2, MFComposeResultEventArgs e2) => mailContr.DismissViewController (true, delegate{});
			};


			this.NavigationItem.SetRightBarButtonItem (it2, true);
		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();



			initializeMoveContainer();

			Title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Item details", "Item details");

			this.fieldName.Placeholder = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Item Name", "Item Name");
			this.fieldDescription.Placeholder = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Item description", "Item description");

			this.fieldName.Ended += (object sender, EventArgs e) => this.SaveIt();

			this.fieldDescription.Ended += (object sender, EventArgs e) => this.SaveIt();

			ShowDetails (this.item);


			releaseKeyboard ();

			InitializeImagePicker ();
            InitializeUnpickImage();



			//DO NOT DELETE
//			UIBarButtonItem it = new UIBarButtonItem();
//			
//			it.Title = "more"; 
//			ItemInfo li = new ItemInfo(this.item);
//			if(!UserInterfaceIdiomIsPhone){
//				Pc = new UIPopoverController(li);
//			}
//			
//
//			it.Clicked += (object sender, EventArgs e) => {
//
//				
//			if(UserInterfaceIdiomIsPhone){
////				NavigationController.PresentViewController (li, true, delegate {});
//					NavigationController.PushViewController(li,true);
//				}else{
//					Pc.PresentFromBarButtonItem(it,UIPopoverArrowDirection.Up, true);
//					}
//			};

//			li.DismissInfo += (object sender, ItemSavedEventArgs e) => {
//				Pc.Dismiss(true);
//			};
			
//			this.NavigationItem.SetRightBarButtonItem(it, true);
		}

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
			if (this.fieldName.Text != null)
				item.Name = this.fieldName.Text;
			if (this.fieldDescription.Text != null)
				item.Description = this.fieldDescription.Text;
			string[] output = SaveImage (item.Name, image);
			item.ImageFileName = output [0];
			item.ThumbFileName = output [1];
			AppDelegate.dao.SaveItem (item);
			RaiseItemSaved ();
		}

		void RaiseItemSaved ()
		{
			var handler = this.ItemSaved;
			if (handler != null) {
				handler(this, new ItemSavedEventArgs());
			}
		}

        void RaiseDerez ()
        {
            Console.WriteLine("Raising Derez");
            var handler = this.Derez;
            if (handler != null) {
                handler(this, new DerezEventArgs());
            }
        }

		void SaveIt ()
		{
			if(item == null){
				item = new Item();
			}

			if (this.fieldName.Text != null)
				item.Name = this.fieldName.Text;
			if (this.fieldDescription.Text != null)
				item.Description = this.fieldDescription.Text;
			AppDelegate.dao.SaveItem (item);
			RaiseItemSaved();
		}
	
        public void DeletePic(){
            if (item != null)
            {
                DeleteImage(item.Name);
                item.ImageFileName = null;
                item.ThumbFileName = null;
            }
        }

        UIActionSheet actionSheet;

        public void InitializeUnpickImage(){
            var unpicky = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("remove picture", "remove picture");

			this.btnUnpickImageItem.SetTitle(unpicky, UIControlState.Normal);

			this.btnUnpickImageItem.TouchUpInside += (sender, e) => {
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
                            //ASSUME IPAD
                            RaiseDerez();
                        }
                    }
                };
                actionSheet.ShowFromTabBar(this.TabBarController.TabBar);
            };
        }


		//Bold attempt
		public void ResetImageView(){
			Console.WriteLine ("reset image");
			if(this.imageView != null){
				this.imageView.Image = null;
			}
		}
	
		void RaiseImageGottenClicked (UIImage image)
		{
			this.mySavePicture (image);

			this.imageView.Image = null;
			var handler = this.GotPicture;
			if (handler != null && image != null) {
				handler(this, new GotPictureEventArgs(image));
			}
		}
	
	
	
//		public void InitializeImagePicker ()
//			//		public static void InitializeImagePicker (RectangleF imageRect,RectangleF PickerReckt, UIView myview)
//		{
//            var picky = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("pick image", "pick image");
////			imageView = new UIImageView (ImageRectangle);
////			Add (imageView);
////			choosePhotoButton = UIButton.FromType (UIButtonType.RoundedRect);
////			choosePhotoButton.Frame = PickerRect;
//            this.btnPickImageItem.SetTitle (picky, UIControlState.Normal);
////			choosePhotoButton.SetTitle
////			Xamarin.Themes.BlackLeatherTheme.Apply (choosePhotoButton, "");
//			btnPickImageItem.TouchUpInside += (s, e) =>  {
//				// create a new picker controller
//				imagePicker = new UIImagePickerController ();
//				// set our source to the photo library
//                //TODO add opportunity to add from camera directly
//				imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
//				// set what media types
//				imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.PhotoLibrary);
//				imagePicker.FinishedPickingMedia += HandleFinishedPickingMedia;
//				imagePicker.Canceled += Handle_Canceled;
//				// show the picker
//				if (UserInterfaceIdiomIsPhone) {
//					NavigationController.PresentViewController (imagePicker, true, delegate {});
//				}
//				else {
//					Console.WriteLine ("Popover");
//					Pc = new UIPopoverController (imagePicker);
//					Pc.PresentFromRect (this.btnPickImageItem.Frame, (UIView)this.View, UIPopoverArrowDirection.Up, true);
//				}
//			};
////			View.Add (choosePhotoButton);
//		}

		public void InitializeImagePicker ()
		{

			var picky = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("pick image", "pick image");
			imageView = new UIImageView (ImageRectangle);
			this.btnPickImageItem.SetTitle (picky, UIControlState.Normal);
			this.btnPickImageItem.TouchUpInside += (s, e) => SelectSource ();
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
			actionSheet.CancelButtonIndex = 0;

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
			imagePicker = new UIImagePickerController ();
			imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;
			imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.Camera);
			this.ExtractImage ();
		}

		public void PickFromLibrary(){
			imagePicker = new UIImagePickerController ();
			imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.PhotoLibrary);
			ExtractImage ();
		}

		public void ExtractImage ()
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
				Pc.PresentFromRect (this.btnPickImageItem.Frame, (UIView)this.View, UIPopoverArrowDirection.Up, true);
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
//			
//			// get common info (shared between images and video)
//			NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
//			if (referenceURL != null) 
//				Console.WriteLine(referenceURL.ToString ());
			
			// if it was an image, get the other image info
			if(isImage) {
				
				// get the original image
				UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
				if(originalImage != null) {
					// do something with the image
					Console.WriteLine ("got the original image");
//					this.imageView.Image = originalImage;
//					OutputImage = originalImage;
					RaiseImageGotten(originalImage);
				}
				
				// get the edited image
				UIImage editedImage = e.Info[UIImagePickerController.EditedImage] as UIImage;
				if(editedImage != null) {
					// do something with the image
					Console.WriteLine ("got the edited image");
//					this.imageView.Image = editedImage;
//					OutputImage = editedImage;
					RaiseImageGotten(editedImage);
				}
			}
//			// if it's a video
//			else {
//				// get video url
//				NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
//				if(mediaURL != null) {
//					//
//					Console.WriteLine(mediaURL.ToString());
//				}
//			}

			if(UserInterfaceIdiomIsPhone){
				imagePicker.DismissViewController (true, delegate{});
			}else{
				Pc.Dismiss(false);
			}
		}
		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.ShowDetails (this.item);

			CreateEmailBarButton ();

			this.GotPicture += (object sender, GotPictureEventArgs e) => this.imageView.Image = e.image;
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			this.SaveIt();
		}
	}
}