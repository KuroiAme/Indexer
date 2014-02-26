using System;
using no.dctapps.Garageindex.model;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.screens;
using no.dctapps.Garageindex.events;
using No.Dctapps.Garageindex.Ios.Screens;
using System.Drawing;
using System.Text;

namespace GarageIndex
{
	public class BigItemDetailContent : UtilityViewController
	{

		LagerObject myObject;

		SelectLager sl;
		public UIPopoverController Ic;
	

		public static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		UINavigationController nc;
		public event EventHandler<BigItemSavedEventArgs> BigItemSaved;

		public BigItemDetailContent (LagerObject myObject,UINavigationController nc)
		{
			this.nc = nc;
			this.myObject = myObject;
		}

		public override void LoadView ()
		{
			base.LoadView ();
			InitView ();
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

			//TODO remember to deselect left table after showing details
			myObject = myobj;

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
				PresentViewControllerAsync(ipc,true);
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
					PresentViewControllerAsync(sl,true);
					//PresentViewController(sl,true);
					//nc.PushViewController(sl, true);
				}else{
					Console.WriteLine("ipad??");
					Ic = new UIPopoverController (sl);
					Ic.PresentFromRect (this.btnIn.Bounds, this.View, UIPopoverArrowDirection.Up, true);
				}
			};
			sl.DismissEvent += (object sender, LagerClickedEventArgs e) =>  {
				Console.WriteLine("dismiss?");
				if(UserInterfaceIdiomIsPhone){
					DismissViewControllerAsync(true);
					//nc.PopViewControllerAnimated(true);
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



		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Background back = new Background ();
			View.Add (back.View);

			ShowDetails (this.myObject);



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
			this.Add (tlc.View);
			tlc.entertag.EditingDidBegin += (object sender, EventArgs e) => {
				tlc.entertag.Placeholder = "";
			};

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

	}
}

