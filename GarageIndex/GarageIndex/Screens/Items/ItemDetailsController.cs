using System;
using No.Dctapps.GarageIndex;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.events;
using GoogleAnalytics.iOS;
using System.Drawing;
using System.Text;
using MonoTouch.Foundation;
using no.dctapps.Garageindex.model;
using no.dctapps.Garageindex.screens;
using MonoTouch.MessageUI;

namespace GarageIndex
{
	public class ItemDetailsController : UtilityViewController
	{
		UIImagePickerController imagePicker;
		//		UIButton choosePhotoButton;
		//        UIButton unselectPhotoButton;

		UIImageView imageView;
		public UIImage OutputImage{ get; set;}

		//		public RectangleF ImageRectangle{ get; set;}
		//		public RectangleF PickerRect{ get; set;}
		//        public RectangleF UnPickerRect{ get; set;}

		public UIPopoverController Pc;
	

		public event EventHandler<GotPictureEventArgs> GotPicture;

		public event EventHandler<ItemSavedEventArgs> ItemSaved;
		public event EventHandler ItemDeleted;
		public event EventHandler<DerezEventArgs> Derez;
		public event EventHandler InContainerTouched;
		public event EventHandler InLocationTouched;

		public Item currentItem;
		//UINavigationController nc;
		UIViewController parent;

		public ItemDetailsController (UIViewController parent)
		{
			this.parent = parent;
			initRectangles ();

		}

		public ItemDetailsController (Item item, UIViewController parent)
		{
			this.parent = parent;
			currentItem = item;
			initRectangles ();
		}

		SizeF contentSize;

		public static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		private void InitView(){
			this.View.BackgroundColor = UIColor.White;
//			this.View.Frame = 
			if (UserInterfaceIdiomIsPhone) {
				contentSize = new SizeF (UIScreen.MainScreen.Bounds.Width, 800);
				this.View.Frame = new RectangleF (0, 0, UIScreen.MainScreen.Bounds.Width, 800);
			} else {
				contentSize = new SizeF (UIScreen.MainScreen.Bounds.Width, 1000);
				this.View.Frame = new RectangleF (0, 0, UIScreen.MainScreen.Bounds.Width, 1000);
			}
		}

		UITextField fieldName;

		UITextField fieldDescription;

		UIStepper antallStepper;
		UITextField cashValue;
		UILabel antall;
		UILabel currency;
		UIButton showReceipts;
		UIButton deleteBtn;

		private void InitInsuranceInfo(){
			RectangleF antallStepperRect;
			RectangleF cashValueRect;
			RectangleF antallRect;
			RectangleF currencyRect;
			RectangleF showReceiptsRect;
			RectangleF deleteBtnRect;
			float x;
			float y;
			if (UserInterfaceIdiomIsPhone) {
				//x and y under the representative image;
				x = 30;
				y = 550;
			} else {
				//on the right in a second collumn
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
			deleteBtnRect = new RectangleF (x + margin, y + linebuffer * 3 + lineheight * 3, broad * 2, lineheight);

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

			deleteBtn = new UIButton (UIButtonType.RoundedRect);
			deleteBtn.Frame = deleteBtnRect;
			Add (deleteBtn);
		}

		private void InitTextFields(){
			RectangleF fieldNameRect;
			RectangleF fieldDescriptionRect;
			if (UserInterfaceIdiomIsPhone) {
				fieldNameRect = new RectangleF (30, 100, 250, 22);
				fieldDescriptionRect = new RectangleF (30, 140, 250, 22);
			} else {
				fieldNameRect = new RectangleF (30, 160, 250, 30);
				fieldDescriptionRect = new RectangleF (30, 210, 250, 30);
			}


			fieldName = new UITextField (fieldNameRect);
			fieldDescription = new UITextField (fieldDescriptionRect);
			fieldName.BorderStyle = UITextBorderStyle.RoundedRect;
			fieldDescription.BorderStyle = UITextBorderStyle.RoundedRect;
			Add (fieldName);
			Add (fieldDescription);
		}

		UIButton btnInContainer;
		UIButton btnInLocation;
		UIButton btnUnpickImageItem;
		UIButton btnPickImageItem;

		private void InitButtons (){
			RectangleF btnInContainerRect;
			RectangleF btnInLocationRect;
			RectangleF btnUnpickImageItemRect;
			RectangleF btnPickImageItemRect;

			if (UserInterfaceIdiomIsPhone) {
				btnInContainerRect = new RectangleF (30, 290, 250, 22);
				btnInLocationRect = new RectangleF (30, 320, 250, 22);
				btnUnpickImageItemRect = new RectangleF (30, 360, 150, 20);
				btnPickImageItemRect = new RectangleF (200, 360, 100, 20);
			} else {
				btnInContainerRect = new RectangleF (250, 170, 250, 30);
				btnInLocationRect = new RectangleF (250, 220, 250, 30);
				btnUnpickImageItemRect = new RectangleF (10, 360, 150, 20);
				btnPickImageItemRect = new RectangleF (150, 360, 100, 20);
			}

			btnInContainer = new UIButton (UIButtonType.RoundedRect);
			btnInContainer.Frame = btnInContainerRect;
			//btnInContainer.BackgroundColor = UIColor.Purple;
			Add (btnInContainer);

			btnInLocation = new UIButton (UIButtonType.RoundedRect);
			btnInLocation.Frame = btnInLocationRect;
			//btnInLocation.BackgroundColor = UIColor.Purple;
			Add (btnInLocation);

			btnUnpickImageItem = new UIButton (UIButtonType.RoundedRect);
			btnUnpickImageItem.Frame = btnUnpickImageItemRect;
			Add (btnUnpickImageItem);

			btnPickImageItem = new UIButton (UIButtonType.RoundedRect);
			btnPickImageItem.Frame = btnPickImageItemRect;
			Add (btnPickImageItem);

		}

		public override void LoadView ()
		{
			base.LoadView ();
			InitView ();
			InitTextFields ();
			InitButtons ();
			InitInsuranceInfo ();
		}

		public SizeF GetContentsize ()
		{
			return this.View.Bounds.Size;
		}

//		public static bool UserInterfaceIdiomIsPhone {
//			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
//		}
//
		void initRectangles ()
		{
			if (UserInterfaceIdiomIsPhone) {
				ImageRectangle = new RectangleF (10, 380, 300, 300);
				//				PickerRect = new RectangleF (10, 100, 175, 30);
				//                UnPickerRect = new RectangleF(175, 100, 150, 55);
			}
			else {
				//Ipad measures for this screen
				ImageRectangle = new RectangleF (10, 390, 800, 800);
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
			this.cashValue.ShouldReturn += (textField) => {
				textField.ResignFirstResponder();
				return true;
			};
		}

		public void SetContainerButtonLabel (Item itty)
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append (NSBundle.MainBundle.LocalizedString ("In container", "In container"));
			sb.Append (":");
			if (itty != null) {
				LagerObject lo = AppDelegate.dao.GetContainerById (itty.boxID);
				if (lo != null) {
					if (!string.IsNullOrEmpty (lo.Name)) {
						sb.Append (lo.Name);
					}
				}
			}
			this.btnInContainer.SetTitle (sb.ToString (), UIControlState.Normal);
		}

		public void SetLocationButtonLabel (Item itty)
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append (NSBundle.MainBundle.LocalizedString ("In location", "In location"));
			sb.Append (":");
			if (itty != null) {
				Lager l = AppDelegate.dao.GetLagerById (itty.LagerID);
				if (l != null) {
					if (!string.IsNullOrEmpty (l.Name)) {
						sb.Append (l.Name);
					}
				}
			}
			this.btnInLocation.SetTitle (sb.ToString (), UIControlState.Normal);
		}

		void AddDeleteButton (Item myItem)
		{
			deleteBtn.SetTitle (MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Delete this item", "Delete this item"), UIControlState.Normal);
			var really = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("really delete this item?", "really delete this item?");
			deleteBtn.TouchUpInside += (object sender, EventArgs e) =>  {
				ass = new UIActionSheet (really, null, "Cancel", "Yes", null);
				ass.Clicked += (object sender2, UIButtonEventArgs e2) =>  {
					if (e2.ButtonIndex == 0) {
						if (myItem != null) {
							int c = ass.CancelButtonIndex;
							ass.DismissWithClickedButtonIndex(c,true);
							AppDelegate.dao.DeleteItem (myItem.ID);
							RaiseItemDeleted ();
							DismissViewControllerAsync(true);
						}
					}
				};
				ass.ShowInView (parent.View);
			};
		}


		public void ShowDetails (Item myItem)
		{
			this.ResetImageView();

			if (myItem == null) {
				myItem = new Item ();
			} else {
				
			}

			this.currentItem = myItem;

			SetContainerButtonLabel (myItem);
			SetLocationButtonLabel (myItem);

			var count = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Count", "Count");
			count = count + ":";
			antall.Text = count + currentItem.antall.ToString ();
			this.antallStepper.Value = currentItem.antall;
			this.antallStepper.ValueChanged += (object sender, EventArgs e) => {
				double ant = this.antallStepper.Value;
				antall.Text = count + ant;
				currentItem.antall = ant;
				AppDelegate.dao.SaveItem(currentItem);
			};
			cashValue.Text = currentItem.cashValue.ToString ();
			cashValue.ValueChanged += (object sender, EventArgs e) => {
				try{
					double newvalue = double.Parse(cashValue.Text);
					currentItem.cashValue = newvalue;
					AppDelegate.dao.SaveItem(currentItem);
				}
				catch(Exception ex){
					Console.WriteLine("coudlnt parse;"+cashValue.Text+"ex:"+ex.ToString());
					cashValue.Text = currentItem.cashValue.ToString();
				}
			};

			currency.Text = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Currency", "Currency");


			if (myItem != null) {
				Console.WriteLine ("myitem er ikke null");
				float x = this.ImageRectangle.X;
				float y = this.ImageRectangle.Y;
				UIImageView imageholder = LoadImage (new PointF (x, y), myItem.ImageFileName);
				this.SetImageViewImage (imageholder);
				this.currentItem = myItem;

				this.fieldName.Text = myItem.Name;
				this.fieldDescription.Text = myItem.Description;


				if (myItem.ImageFileName != null) {
					Console.WriteLine ("imagefilename er ikke null");
					var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
					string filename = System.IO.Path.Combine (documentsDirectory, myItem.ImageFileName);
					this.imageView.Image = UIImage.FromFile (filename);
				} else {
					Console.WriteLine ("imagefilename er null");
				}
				AddTagList ();
			} else {
				Console.WriteLine ("myitem er null");
			}

			showReceipts.SetTitle (MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Show Receipts", "Show Receipts"), UIControlState.Normal);
			showReceipts.TouchUpInside += (object sender, EventArgs e) => {
				InsurancePhotoController ipc = new InsurancePhotoController(myItem);
				PresentViewControllerAsync(ipc,true);
				//nc.PushViewController(ipc,false);
			};
			makeCornersRound ();

			AddDeleteButton (myItem);
			//AddShadowToImageView ();
		}

		void RaiseItemDeleted ()
		{
			var handler = this.ItemDeleted;
			if (handler != null) {
				handler (this, new EventArgs ());
			}
		}

		UIActionSheet ass;

		void AddTagList ()
		{
			RectangleF frame;
			if (UserInterfaceIdiomIsPhone) {
				frame = new RectangleF (30, 160, 300, 125);
			} else {
				frame = new RectangleF (30, 230, 300, 125);			
			}

			Console.WriteLine ("frame:" + frame);
			ImageTag tag = AppDelegate.dao.GetImageTagById (this.currentItem.ImageTagId);
			if (tag == null) {
				Console.WriteLine ("Tag er null");
				tag = new ImageTag ();
				int key = AppDelegate.dao.SaveTag (tag);
				tag.ID = key;
				Console.WriteLine ("key:" + key);
				this.currentItem.ImageTagId = key;
				AppDelegate.dao.SaveItem (this.currentItem);
			}

			TagListController tlc = new TagListController (tag, frame);
			this.View.AddSubview (tlc.View);

		}

		public void SetImageViewImage (UIImageView imageholder)
		{
			Console.WriteLine ("setImageViewImage");
			if (imageholder != null) {

				Console.WriteLine ("Not null");
				this.imageView = imageholder;
				//				if(imageholder.Image != null){
				//				this.scrollContent.ContentSize = imageholder.Image.Size;
				//
				//				this.scrollContent.AutosizesSubviews = true;
				//				this.scrollContent.BringSubviewToFront(imageholder);
				//				this.scrollContent.AddSubview (imageholder);
				Add (imageholder);
			} else {
				Console.WriteLine ("imageholder er null");
			}
		}

		SelectContainer sc;
		public UIPopoverController Ic;
		UIPopoverController pc;

		void initializePlaceObject ()
		{

			sc = new SelectContainer ();

			this.btnInContainer.TouchUpInside += (object sender, EventArgs e) =>  {
				if(UserInterfaceIdiomIsPhone){
					PresentViewControllerAsync(sc,true);
					//nc.PushViewController(sc, true);
				}else{
					Ic = new UIPopoverController (sc);
					Ic.PresentFromRect (this.btnInContainer.Bounds, this.View, UIPopoverArrowDirection.Up, true);
				}
			};

			sc.DismissEvent += (object sender, ContainerClickedEventArgs e) => {
				if(UserInterfaceIdiomIsPhone){
					DismissViewControllerAsync(true);
					//nc.PopViewControllerAnimated(true);
				}else{
					Ic.Dismiss (true);
				}
				this.currentItem.boxID = e.container.ID;
				SetContainerButtonLabel (this.currentItem);
				AppDelegate.dao.SaveItem(this.currentItem);
			};

			SelectLager sl = new SelectLager ();

			this.btnInLocation.TouchUpInside += (object sender, EventArgs e) =>  {
				if(UserInterfaceIdiomIsPhone){
					PresentViewControllerAsync(sl,true);
				}else{
					pc = new UIPopoverController (sl);
					pc.PresentFromRect (this.btnInLocation.Bounds, this.View, UIPopoverArrowDirection.Up, true);
				}
			};

			sl.DismissEvent += (object sender, LagerClickedEventArgs e) => {
				if(UserInterfaceIdiomIsPhone){
					DismissViewControllerAsync(true);
				}else{
					pc.Dismiss (true);
				}
				this.currentItem.LagerID = e.Lager.ID;
				SetLocationButtonLabel (this.currentItem);
				AppDelegate.dao.SaveItem(this.currentItem);
			};
		}

		void AddShadowToImageView ()
		{
			this.imageView.Layer.ShadowColor = UIColor.Purple.CGColor;
			this.imageView.Layer.ShadowOffset = new SizeF (0, 1);
			this.imageView.Layer.ShadowOpacity = 1;
			this.imageView.Layer.ShadowRadius = 1.0f;
			//this.imageView.Layer.ShouldRasterize = true;
			imageView.ClipsToBounds = false;
		}

		void makeCornersRound(){
			this.imageView.Layer.CornerRadius = 7;
			this.imageView.ClipsToBounds = true;
		}




		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var imgView = new UIImageView(BlueSea.MakeBlueSea()){
				ContentMode = UIViewContentMode.ScaleToFill,
				AutoresizingMask = UIViewAutoresizing.All,
				Frame = View.Bounds
			};

			View.AddSubview (imgView);
			View.SendSubviewToBack (imgView);

			this.fieldName.Placeholder = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Item Name", "Item Name");
			this.fieldDescription.Placeholder = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Item description", "Item description");

			this.fieldName.Ended += (object sender, EventArgs e) => this.SaveIt();

			this.fieldDescription.Ended += (object sender, EventArgs e) => this.SaveIt();

			ShowDetails (this.currentItem);


			releaseKeyboard ();

			InitializeImagePicker ();
			InitializeUnpickImage();
			initializePlaceObject();

			UIButton backbutton = new UIButton(new RectangleF(10,25,48,32));
			backbutton.SetImage (backarrow.MakeBackArrow(), UIControlState.Normal);
			backbutton.TouchUpInside += (object sender, EventArgs e) => parent.DismissViewControllerAsync (true);
			Add (backbutton);

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
			//				NavigationController.PresentViewController (li, true, delegate {});
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
				this.currentItem.Name = this.fieldName.Text;
			if (this.fieldDescription.Text != null)
				this.currentItem.Description = this.fieldDescription.Text;
			string[] output = SaveImage (this.currentItem.Name, image);
			this.currentItem.ImageFileName = output [0];
			this.currentItem.ThumbFileName = output [1];
			AppDelegate.dao.SaveItem (this.currentItem);
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
				handler(this, new DerezEventArgs(this.currentItem));
			}
		}

		void SaveIt ()
		{
			if(this.currentItem == null){
				this.currentItem = new Item();
			}

			if (this.fieldName.Text != null)
				this.currentItem.Name = this.fieldName.Text;
			if (this.fieldDescription.Text != null)
				this.currentItem.Description = this.fieldDescription.Text;
			AppDelegate.dao.SaveItem (this.currentItem);
			RaiseItemSaved();
		}

		public void DeletePic(){
			if (this.currentItem != null)
			{
				DeleteImage(this.currentItem.Name);
				this.imageView.Image = null;
				this.currentItem.ImageFileName = null;
				this.currentItem.ThumbFileName = null;
			}
		}


		UIActionSheet aSheet;

		public void InitializeUnpickImage(){
			var unpicky = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("remove picture", "remove picture");

			this.btnUnpickImageItem.SetTitle(unpicky, UIControlState.Normal);

			this.btnUnpickImageItem.TouchUpInside += (sender, e) => {
				var rmText = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("remove picture?","remove picture?");
				var rmDel = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Delete", "Delete");
				var rmCancel = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Cancel", "Cancel");
				aSheet = new UIActionSheet(rmText, null, rmCancel, rmDel, null);
				aSheet.Clicked += delegate(object a, UIButtonEventArgs b) {
					Console.WriteLine("button" + b.ButtonIndex.ToString() + " clicked");
					if(b.ButtonIndex == 0){
						Console.WriteLine("deleting picture!");
						//DELETE IMAGE FROM APP.
						DeletePic();
						//POP this view to refresh.
						if(UserInterfaceIdiomIsPhone){
							DismissViewControllerAsync(true);
						}else{
							//ASSUME IPAD
							RaiseDerez();
						}
					}
				};
				aSheet.ShowInView(parent.View);
				//aSheet.ShowFromTabBar(this.TabBarController.TabBar);
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


		UIActionSheet bSheet;

		public void SelectSource(){
			var source = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("pick image from where?", "pick image from where?");
			var myCancel = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Cancel", "Cancel");
			var myCamera = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Camera", "Camera");
			var myLibrary = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Photo Library", "Photo Library");
			bSheet = new UIActionSheet(source);
			bSheet.AddButton(myCancel);
			bSheet.AddButton(myCamera);
			bSheet.AddButton(myLibrary);
			//			actionSheet.CancelButtonIndex = 0;

			bSheet.Clicked += (object sender, UIButtonEventArgs e) => { ;
				if(e.ButtonIndex == 0){
					//DO nothing
				}else if(e.ButtonIndex == 1){
					PickFromCamera();
				}else{
					PickFromLibrary();
				}
			};
			bSheet.ShowInView (parent.View);
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
				PresentViewControllerAsync (imagePicker, true);
				//nc.PresentViewController (imagePicker, true, delegate{});
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

		UIImage GotPictureHandler (GotPictureEventArgs e)
		{

			return this.imageView.Image = e.image;

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.ShowDetails (this.currentItem);

			this.GotPicture += (object sender, GotPictureEventArgs e) => {

			};
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			this.SaveIt();
		}
	}
}

