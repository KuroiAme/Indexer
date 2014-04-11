using System;
using no.dctapps.commons.events.model;
using MonoTouch.UIKit;
using no.dctapps.commons.events.screens;
using no.dctapps.commons.events.events;
using System.Drawing;
using System.Text;
using no.dctapps.commons.panels;
using no.dctapps.Garageindex.model;
using no.dctapps.common;

namespace no.dctapps.commons.events
{
	public class BigItemDetailContent : UIViewController
	{

		LagerObject MyCurrentObject;

		SelectLager sl;
		public UIPopoverController Ic;
	

		public static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}
			
		public event EventHandler<BigItemSavedEventArgs> BigItemSaved;

		UIViewController ancestor;

		public BigItemDetailContent (LagerObject myObject,UIViewController ancestor)
		{
			this.ancestor = ancestor;
			this.MyCurrentObject = myObject;
		}

		protected override void Dispose (bool disposing)
		{
			MyCurrentObject = null;
			sl = null;
			Ic.Dispose ();
			ancestor = null;
			tlc.Dispose ();
			base.Dispose (disposing);
		}


		/// <summary>
		/// Release everything not in use
		/// </summary>
		void cleanup ()
		{
			//this.Dispose ();
		}


		public override void LoadView ()
		{
			base.LoadView ();
			InitView ();

		}

		private void InitView(){
//			this.View.BackgroundColor = UIColor.White;
			//			this.View.Frame = 
			if (UserInterfaceIdiomIsPhone) {
				this.View.Frame = new RectangleF (0, 0, UIScreen.MainScreen.Bounds.Width, 1200);
			} else {
				this.View.Frame = new RectangleF (0, 0, UIScreen.MainScreen.Bounds.Width, 1200);
			}
		}

		public SizeF GetContentsize ()
		{
			return this.View.Bounds.Size;
		}

		UITextField fieldBigName;
		UITextField fieldBigDescription;
		UIButton btnIn;


		public void InitLegacyNib(){
			RectangleF fieldBigNameRect;
			RectangleF fieldBIgDescriptionRect;
			RectangleF btnInRect;


			float x;
			float y;
			if (UserInterfaceIdiomIsPhone) {
				x = 10;
				y = 100; 
			} else {
				x = 30;
				y = 100;
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
			btnInRect = new RectangleF(x, y + lineheight * counter + linebuffer * counter +tagsheight + linebuffer, broad, lineheight);
			counter++;


			fieldBigName = new UITextField (fieldBigNameRect);
			fieldBigName.BorderStyle = UITextBorderStyle.RoundedRect;
			Add (fieldBigName);

			fieldBigDescription = new UITextField (fieldBIgDescriptionRect);
			fieldBigDescription.BorderStyle = UITextBorderStyle.RoundedRect;
			Add (fieldBigDescription);

			btnIn = new UIButton (UIButtonType.RoundedRect);
			btnIn.Frame = btnInRect;
			Add (btnIn);


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
				y = 750;
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

			//TODO remember to deselect left table after showing details
			MyCurrentObject = myobj;

			Title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Big Items Details", "Big Items Details");
			this.fieldBigName.Placeholder = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Name", "Name");
			this.fieldBigDescription.Placeholder = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Description", "Description");
			var count = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Count", "Count");
			count = count + ":";
			if (myobj != null) {

				this.fieldBigName.Text = myobj.Name;
				this.fieldBigName.Font = UIFont.FromName ("Cochin-BoldItalic", 22f);
				this.fieldBigDescription.Text = myobj.Description;
				this.fieldBigDescription.Font = UIFont.FromName ("AmericanTypewriter", 12f);

				antall.Text = count + MyCurrentObject.antall.ToString ();
			}
			AddTagList ();
			releaseKeyboard ();

			if (MyCurrentObject != null) {
				this.antallStepper.Value = MyCurrentObject.antall;
				this.antallStepper.ValueChanged += (object sender, EventArgs e) => {
					double ant = this.antallStepper.Value;
					antall.Text = count + ant;
					MyCurrentObject.antall = ant;
					AppDelegate.dao.SaveLagerObject (MyCurrentObject);
				};
				cashValue.Text = MyCurrentObject.cashValue.ToString ();
				cashValue.EditingDidEnd += (object sender, EventArgs e) => {
					try {
						double newvalue = double.Parse (cashValue.Text);
						MyCurrentObject.cashValue = newvalue;
						AppDelegate.dao.SaveLagerObject (MyCurrentObject);
						Console.WriteLine("saved cashvalue:"+MyCurrentObject.cashValue);
					} catch (Exception ex) {
						Console.WriteLine ("coudlnt parse;" + cashValue.Text + "ex:" + ex.ToString ());
						cashValue.Text = MyCurrentObject.cashValue.ToString ();
					}
				};
				if (MyCurrentObject.imageFileName != null) {
					imp.SetNewImageName (MyCurrentObject.imageFileName);
				}
			}

			currency.Text = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Currency", "Currency");

			showReceipts.SetTitle (MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Show Receipts", "Show Receipts"), UIControlState.Normal);
			showReceipts.TouchUpInside += (object sender, EventArgs e) => {
				InsurancePhotoController ipc = new InsurancePhotoController(myobj);
				ancestor.NavigationController.PushViewController(ipc,true);
				//PresentViewController(ipc,true, null);
				//nc.PushViewController(ipc,false);
			};

		}

		void initializeMoveLager ()
		{
			Console.WriteLine("initializemovelager");

			sl = new SelectLager ();

			this.btnIn.TouchUpInside += (object sender, EventArgs e) =>  {

				Console.WriteLine("touchupinside");
				if(UserInterfaceIdiomIsPhone){
					Console.WriteLine("iphone??");
					ancestor.NavigationController.PushViewController(sl,true);
				}else{
					Console.WriteLine("ipad??");
					Ic = new UIPopoverController (sl);
					Ic.PresentFromRect (this.btnIn.Bounds, this.View, UIPopoverArrowDirection.Up, true);
				}
			};
			sl.DismissEvent += (object sender, LagerClickedEventArgs e) =>  {
				Console.WriteLine("dismiss?");
				if(UserInterfaceIdiomIsPhone){
					sl.NavigationController.DismissViewController(true,null);

					//nc.PopViewControllerAnimated(true);
				}else{
					Ic.Dismiss (true);
				}
				if(MyCurrentObject != null){
					this.MyCurrentObject.LagerID = e.Lager.ID;
					SetLagerButtonLabel (this.MyCurrentObject);
					AppDelegate.dao.SaveLagerObject(this.MyCurrentObject);
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

		ImagePanel imp;


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			InitLegacyNib ();
			InitInsuranceInfo ();

			const float imgY = 400;
			RectangleF imageRect = new RectangleF (10, imgY, UIScreen.MainScreen.Bounds.Width - 20, 300);

			imp = new ImagePanel (imageRect, this.ancestor);
			View.AddSubview (imp.View);
			imp.ImageSaved += (object sender, SavedImageStringsEventArgs e) => {
				MyCurrentObject.imageFileName = e.imageFilename;
				MyCurrentObject.thumbFileName = e.Thumbfilename;
				AppDelegate.dao.SaveLagerObject(MyCurrentObject);
			};

			imp.ImageDeleted += (object sender, EventArgs e) => {
				MyCurrentObject.imageFileName = null;
				MyCurrentObject.thumbFileName = null;
				AppDelegate.dao.SaveLagerObject(MyCurrentObject);
			};




			ShowDetails (this.MyCurrentObject);



			SetLagerButtonLabel (this.MyCurrentObject);
			initializeMoveLager ();

			this.fieldBigName.EditingDidEnd += (object sender, EventArgs e) => this.SaveIt ();

			this.fieldBigDescription.EditingDidEnd += (object sender, EventArgs e) => this.SaveIt ();



		}

		TagListController tlc;

		void AddTagList ()
		{
			RectangleF frame;
			ImageTag tag = null;

			if (UserInterfaceIdiomIsPhone) {
				frame = new RectangleF (10, 180, View.Bounds.Width -20, 125);
			} else {
				frame = new RectangleF (10, 190, View.Bounds.Width -20, 125);
			}

//			Console.WriteLine("frame:"+frame);
			if (MyCurrentObject != null) {
				tag = AppDelegate.dao.GetImageTagById (this.MyCurrentObject.ImageTagId);
			}
			if (tag == null) {
				Console.WriteLine ("Tag er null");
				tag = new ImageTag ();
				int key = AppDelegate.dao.SaveTag (tag);
				tag.ID = key;
				if (MyCurrentObject != null) {
					this.MyCurrentObject.ImageTagId = key;
					AppDelegate.dao.SaveLagerObject (MyCurrentObject);
				}
			}

			tlc = new TagListController (tag, frame);
			View.AddSubview (tlc.View);
			tlc.entertag.EditingDidBegin += (object sender, EventArgs e) => tlc.entertag.Placeholder = "";

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
			ShowDetails (this.MyCurrentObject);



		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

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
			if(MyCurrentObject != null){
				MyCurrentObject.Name = this.fieldBigName.Text;
				MyCurrentObject.Description = this.fieldBigDescription.Text;
				AppDelegate.dao.SaveLagerObject (MyCurrentObject);
				RaiseSavedEvent();
			}
		}

	}
}

