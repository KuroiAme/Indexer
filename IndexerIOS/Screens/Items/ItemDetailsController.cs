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
	

//		public event EventHandler<GotPictureEventArgs> GotPicture;

		public event EventHandler<ItemSavedEventArgs> ItemSaved;
		public event EventHandler ItemDeleted;
//		public event EventHandler<DerezEventArgs> Derez;
		public event EventHandler InContainerTouched;
		public event EventHandler InLocationTouched;

		public Item currentItem;

		//UINavigationController nc;
		UIViewController ancestor;

		public ItemDetailsController (UIViewController parent)
		{
			this.ancestor = parent;
		}


//		public ItemDetailsController (UINavigationController nc, UIViewController parent)
//		{
//			this.parent = parent;
//			this.nc = nc;
//
//			initRectangles ();
//
//		}


		public ItemDetailsController (Item item, UIViewController parent)
		{
			this.ancestor = parent;
			currentItem = item;
//			initRectangles ();
			this.ancestor = parent;
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
				x = 10;
				y = 620;
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
//		UIButton btnUnpickImageItem;
//		UIButton btnPickImageItem;

		private void InitButtons (){
			RectangleF btnInContainerRect;
			RectangleF btnInLocationRect;
//			RectangleF btnUnpickImageItemRect;
//			RectangleF btnPickImageItemRect;

			if (UserInterfaceIdiomIsPhone) {
				btnInContainerRect = new RectangleF (30, 330, 250, 22);
				btnInLocationRect = new RectangleF (30, 360, 250, 22);
//				btnUnpickImageItemRect = new RectangleF (30, 360, 150, 20);
//				btnPickImageItemRect = new RectangleF (200, 360, 100, 20);
			} else {
				btnInContainerRect = new RectangleF (250, 170, 250, 30);
				btnInLocationRect = new RectangleF (250, 220, 250, 30);
//				btnUnpickImageItemRect = new RectangleF (10, 360, 150, 20);
//				btnPickImageItemRect = new RectangleF (150, 360, 100, 20);
			}

			btnInContainer = new UIButton (UIButtonType.RoundedRect);
			btnInContainer.Frame = btnInContainerRect;
			//btnInContainer.BackgroundColor = UIColor.Purple;
			Add (btnInContainer);

			btnInLocation = new UIButton (UIButtonType.RoundedRect);
			btnInLocation.Frame = btnInLocationRect;
			//btnInLocation.BackgroundColor = UIColor.Purple;
			Add (btnInLocation);

//			btnUnpickImageItem = new UIButton (UIButtonType.RoundedRect);
//			btnUnpickImageItem.Frame = btnUnpickImageItemRect;
//			Add (btnUnpickImageItem);
//
//			btnPickImageItem = new UIButton (UIButtonType.RoundedRect);
//			btnPickImageItem.Frame = btnPickImageItemRect;
//			Add (btnPickImageItem);

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
			deleteBtn.TouchUpInside += (object sender, EventArgs e) => {
				ass = new UIActionSheet (really, null, "Cancel", "Yes", null);
				ass.Clicked += delegate (object a, UIButtonEventArgs c) {
					if (c.ButtonIndex == 0) {
						AppDelegate.dao.DeleteItem (myItem.ID);
						RaiseItemDeleted ();
					}
				};
				ass.ShowInView (ancestor.View);
			};
		}


		public void ShowDetails (Item myItem)
		{
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
			cashValue.EditingDidEnd += (object sender, EventArgs e) => {
				try{
					double newvalue = double.Parse(cashValue.Text);
					currentItem.cashValue = newvalue;
					AppDelegate.dao.SaveItem(currentItem);
					Console.WriteLine("saved cashValue:"+currentItem.cashValue);
				}
				catch(Exception ex){
					Console.WriteLine("coudlnt parse;"+cashValue.Text+"ex:"+ex.ToString());
					cashValue.Text = currentItem.cashValue.ToString();
				}
			};

			currency.Text = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Currency", "Currency");


			if (myItem != null) {
				Console.WriteLine ("myitem er ikke null");

				this.fieldName.Text = myItem.Name;
				this.fieldDescription.Text = myItem.Description;


				if (myItem.ImageFileName != null) {
					imp.SetNewImageName (myItem.ImageFileName);
				} else {
					Console.WriteLine ("imagefilename er null");
				}
				AddTagList ();
			} else {
				Console.WriteLine ("myitem er null");
			}

			showReceipts.SetTitle (NSBundle.MainBundle.LocalizedString ("Show Receipts", "Show Receipts"), UIControlState.Normal);
			showReceipts.TouchUpInside += (object sender, EventArgs e) => {
				InsurancePhotoController ipc = new InsurancePhotoController(myItem);
				ancestor.NavigationController.PushViewController(ipc,true);
			};
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
				frame = new RectangleF (10, 170, View.Bounds.Width -20, 125);
			} else {
				frame = new RectangleF (10, 240, View.Bounds.Width -20, 125);			
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

		SelectContainer sc;
		public UIPopoverController Ic;
		UIPopoverController pc;

		void initializePlaceObject ()
		{

			sc = new SelectContainer ();

			this.btnInContainer.TouchUpInside += (object sender, EventArgs e) =>  {
				if(UserInterfaceIdiomIsPhone){
					//PresentViewControllerAsync(sc,true);
					//nc.PushViewController(sc, true);
					ancestor.NavigationController.PushViewController(sc,true);
				}else{
					Ic = new UIPopoverController (sc);
					Ic.PresentFromRect (this.btnInContainer.Bounds, this.View, UIPopoverArrowDirection.Up, true);
				}
			};

			sc.DismissEvent += (object sender, ContainerClickedEventArgs e) => {
				if(UserInterfaceIdiomIsPhone){
					ancestor.NavigationController.DismissViewController(true,null);
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
					//PresentViewControllerAsync(sl,true);
					ancestor.NavigationController.PushViewController(sl,true);
				}else{
					pc = new UIPopoverController (sl);
					pc.PresentFromRect (this.btnInLocation.Bounds, this.View, UIPopoverArrowDirection.Up, true);
				}
			};

			sl.DismissEvent += (object sender, LagerClickedEventArgs e) => {
				if(UserInterfaceIdiomIsPhone){
					ancestor.NavigationController.DismissViewController(true,null);
				}else{
					pc.Dismiss (true);
				}
				this.currentItem.LagerID = e.Lager.ID;
				SetLocationButtonLabel (this.currentItem);
				AppDelegate.dao.SaveItem(this.currentItem);
			};
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




			releaseKeyboard ();

			imp = new ImagePanel (new RectangleF (10, 400, UIScreen.MainScreen.Bounds.Width - 20, 200), this.ancestor);

			imp.ImageDeleted += (object sender, EventArgs e) => {
				currentItem.ImageFileName = null;
				currentItem.ThumbFileName = null;
				AppDelegate.dao.SaveItem(currentItem);
				RaiseItemSaved();
			};

			imp.ImageSaved += (object sender, SavedImageStringsEventArgs e) => {
				currentItem.ImageFileName = e.imageFilename;
				currentItem.ThumbFileName = e.Thumbfilename;
				AppDelegate.dao.SaveItem(currentItem);
				RaiseItemSaved();
			};

			View.Add (imp.View);

			ShowDetails (this.currentItem);





		}

		ImagePanel imp;


		void RaiseItemSaved ()
		{
			var handler = this.ItemSaved;
			if (handler != null) {
				handler(this, new ItemSavedEventArgs());
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



		public override void ViewDidAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.ShowDetails (this.currentItem);

		}

	}
}

