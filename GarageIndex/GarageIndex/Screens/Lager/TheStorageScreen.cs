using System;
using MonoTouch.Foundation;
using Xamarin.Themes;
using no.dctapps.Garageindex.model;
using no.dctapps.Garageindex.dao;
using no.dctapps.Garageindex.events;
using MonoTouch.UIKit;
using MonoTouch.MessageUI;
using no.dctapps.Garageindex.businesslogic;

namespace no.dctapps.Garageindex.screens
{
	public partial class TheStorageScreen : UtilityViewController
	{
		Lager lm;
		LagerDAO dao;
		GarageindexBL bl;

		public event EventHandler<LagerClickedEventArgs> LagerSaved;


//		public TheStorageScreen ()
//			: base (UserInterfaceIdiomIsPhone ? "TheStorageScreen_iPhone" : "TheStorageScreen_iPad")
//		{
//			dao = new LagerDAO();
//			bl = new GarageindexBL();
//			lm = bl.GetActiveActiveLager ();
//		}

		public TheStorageScreen (Lager lager)
			: base (UserInterfaceIdiomIsPhone ? "TheStorageScreen_iPhone" : "TheStorageScreen_iPad")
		{
			dao = new LagerDAO();
//			bl = new GarageindexBL();
			lm = lager;
		}

		public TheStorageScreen ()
			: base (UserInterfaceIdiomIsPhone ? "TheStorageScreen_iPhone" : "TheStorageScreen_iPad")
		{
			dao = new LagerDAO();
			//			bl = new GarageindexBL();
		}

		MFMailComposeViewController mailContr;

		private void CreateEmailBarButton ()
		{
			//DO NOT DELETE
			UIBarButtonItem it = new UIBarButtonItem ();
			it.Title = "email";
			//IS really info
			it.Clicked += (object sender, EventArgs e) =>  {
                mailContr = new MFMailComposeViewController();
                mailContr.SetSubject(bl.GenerateSubject(lm));
                mailContr.SetMessageBody(bl.GenerateManifest(lm),false);
				this.PresentViewController(mailContr, true, delegate{});
                mailContr.Finished += (object sender2, MFComposeResultEventArgs e2) => {
                    mailContr.DismissViewController(true, delegate{});
                };
			};
			this.NavigationItem.SetRightBarButtonItem (it, true);
		}


//		mailContr = new MFMailComposeViewController();
//
//		mailContr.Finished += (object sender, MFComposeResultEventArgs args) => {
//
//			Console.WriteLine(args.Result.ToString());
//			args.Controller.DismissViewController(true, delegate{});
//		};
//
//		SendMailBtn.TouchUpInside += (sender,e) => {
//
//			mailContr.SetSubject(bl.GenerateSubject(lm));
//			mailContr.SetMessageBody(bl.GenerateManifest(lm), false);
//
//			//				if(switchPictureAttachments.On){
//			//					Console.WriteLine("adding picture attachments");
//			//					bl.AddPictureAttachments(mailContr, this.switchItems.On);
//			//				}
//
//			this.PresentViewController(mailContr, true, delegate{});
//		};
//

//		RectangleF mail;
//		RectangleF rec;

		public override void LoadView ()
		{
			base.LoadView ();

//			rec = new RectangleF (10, 10, 10, 10);
//			mail = new RectangleF (10, 10, 10, 10);
//			if(UserInterfaceIdiomIsPhone)
//			{
//				rec = new RectangleF (20, 260, 100, 57);
//				mail = new RectangleF(120, 260, 200, 57);
//			}else{
//				rec = new RectangleF (500, 20, 100, 57);
//				mail = new RectangleF(500, 80, 200, 57);
//			}

//			UIButton SaveButton = new UIButton (rec);
//
//			string savetitle = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("save", "save");
//
//			SaveButton.SetTitle (savetitle, UIControlState.Normal);
//
//			SaveButton.TouchUpInside += HandleTouchUpInside;






//			View.AddSubview (SaveButton);

//			if(!InSimulator()){
//				BlackLeatherTheme.Apply (SaveButton, "");
				BlackLeatherTheme.Apply(this.storageName, "");
//			}

		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			//TODO change this class to use this.fieldname.valuechanged instead of viewWilldissapear.
			this.SaveAll();
		}

		void HandleTouchUpInside (object sender, EventArgs e)
		{
			SaveAll();
		}

		void cleanup ()
		{
//			lm = null;
			dao = null;
		}

		void Unclean ()
		{
			if(dao == null)
			{
				dao = new LagerDAO();
			}

//			if(lm == null){
//				lm = bl.GetActiveActiveLager();
//			}
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

		public void SetPlaceholders ()
		{
			var name = NSBundle.MainBundle.LocalizedString ("Name of the storage unit", "Name of the storage unit");
			var addresse = NSBundle.MainBundle.LocalizedString ("Address", "Address");
			var key = NSBundle.MainBundle.LocalizedString ("Key contact", "Key contact");
			var zip = NSBundle.MainBundle.LocalizedString ("Zip Code", "Zip Code");
			var city = NSBundle.MainBundle.LocalizedString ("City", "City");

			storageName.Placeholder = name;
			address.Placeholder = addresse;
			keyContact.Placeholder = key;
			poststedField.Placeholder = city;
			zipField.Placeholder = zip;
			textDimensions.Text = NSBundle.MainBundle.LocalizedString ("Dimensions height,width,depth e.g. meters/yards", "Dimensions height,width,depth e.g. meters/yards");
			textEstimate.Text = NSBundle.MainBundle.LocalizedString ("Your storage fill degree estimate", "Your storage fill degree estimate");
			textSpaceAvailable.Text = NSBundle.MainBundle.LocalizedString ("Space available", "Space available");

			BlackLeatherTheme.Apply(this.storageName, "");
			BlackLeatherTheme.Apply(this.address, "");
			BlackLeatherTheme.Apply(this.textDimensions, "");
			BlackLeatherTheme.Apply(this.textEstimate, "");
			BlackLeatherTheme.Apply(this.textSpaceAvailable, "");
			BlackLeatherTheme.Apply(this.keyContact, "");
			BlackLeatherTheme.Apply(this.poststedField, "");
			BlackLeatherTheme.Apply(this.zipField, "");
			BlackLeatherTheme.Apply(this.x, "");
			BlackLeatherTheme.Apply(this.y, "");
			BlackLeatherTheme.Apply(this.z, "");
			BlackLeatherTheme.Apply(this.prod, "");
			BlackLeatherTheme.Apply(this.fillEst, "");
			BlackLeatherTheme.Apply(this.remaining, "");
			BlackLeatherTheme.Apply(this.M3, "");
			BlackLeatherTheme.Apply(this.M3_2, "");
		}

//		void AddContactKeyButtons ()
//		{
//			chooseContact = UIButton.FromType (UIButtonType.RoundedRect);
//			chooseContact.Frame = new RectangleF (240, 125, 50, 25);
//			chooseContact.SetTitle ("Velg", UIControlState.Normal);
//			contactName = new UILabel {
//				Frame = new RectangleF (30, 75, 200, 35)
//			};
//			View.AddSubviews (chooseContact, contactName);
//			contactController = new ABPeoplePickerNavigationController ();
//			chooseContact.TouchUpInside += delegate {
//				this.PresentModalViewController (contactController, true);
//			};
//			contactController.Cancelled += delegate {
//				this.DismissModalViewControllerAnimated (true);
//			};
//			contactController.SelectPerson += delegate (object sender, ABPeoplePickerSelectPersonEventArgs e) {
//				contactName.Text = e.Person.GetPhones;
//				this.DismissModalViewControllerAnimated (true);
//			};
//		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			bl = new GarageindexBL ();


//			UIView mymy = this.View;
//
//			BlackLeatherTheme.Apply (this.View);

			SetPlaceholders ();
			Title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Storage Data", "Storage Data");

//			fillEst.MinValue = 0f;
//			fillEst.MaxValue = 1f;
//			fillEst.Value = 0.5f; // initial value
//			fh = new FileHandler ();
//			dao = new LagerDAO();
//			lm = dao.readTopItem ();
//			lm = fh.ReadLagerData ();

			ShowDetails (lm);

//			AddContactKeyButtons ();


			ReleaseKeyboard ();

			//TODO fix the slider event here
			fillEst.ValueChanged += HandleValueChanged;

//			storageName.CommitEditing +=(sender, e) => {
//				lm.Name = storageName.Text;
//				
//			};

//			var b = new GlassButton(bounds){
////				Font = 
//			};

			// Perform any additional setup after loading the view, typically from a nib.
		}

		public void ShowDetails (Lager myLager)
		{
			lm = myLager;

			if (myLager != null) {
				storageName.Text = myLager.Name;
				address.Text = myLager.address;
				keyContact.Text = myLager.telephone;
				x.Text = myLager.height.ToString ();
				y.Text = myLager.width.ToString ();
				z.Text = myLager.depth.ToString ();
				this.poststedField.Text = myLager.poststed;
				this.zipField.Text = myLager.postnr;
                CreateEmailBarButton ();
			}
		}

		void HandleValueChanged (object sender, EventArgs e)
		{   // display the value in a label
			//			label.Text = slider.Value.ToString ();
			Calculate ();
		}

		public override void ViewWillAppear (bool animated){
			base.ViewWillAppear (animated);
			Calculate ();
			Unclean();
			
		}

		void ReleaseKeyboard ()
		{
			this.storageName.ShouldReturn += textField =>  {
				textField.ResignFirstResponder ();
				return true;
			};
			this.keyContact.ShouldReturn += textField =>  {
				textField.ResignFirstResponder ();
				return true;
			};
			this.address.ShouldReturn += (textField) => { 
				textField.ResignFirstResponder();
				return true; 
			};
			this.x.ShouldReturn += (textField) => { 
				textField.ResignFirstResponder();
				return true; 
			};
			this.y.ShouldReturn += (textField) => { 
				textField.ResignFirstResponder();
				return true; 
			};
			this.z.ShouldReturn += (textField) => { 
				textField.ResignFirstResponder();
				return true; 
			};
		}

		void Calculate ()
		{
			try{
				lm.height = Convert.ToInt32(this.x.Text);
				lm.width = Convert.ToInt32(this.y.Text);
				lm.depth = Convert.ToInt32 (this.z.Text);
			}catch(Exception e){
				Console.WriteLine("disaster avoided:"+e.ToString());
			}

			if (lm != null) {
				int dm3 = lm.height * lm.depth * lm.width;
				this.prod.Text = "" + dm3;
				double percentile = (double)fillEst.Value / 100;
				int estRemaining = (int)(dm3 - (percentile * dm3));
				this.remaining.Text = "" + estRemaining;
			}
		}

//		partial void save (NSObject sender, MonoTouch.UIKit.UIEvent @event)
//		{
//
//			ReleaseKeyboard ();
//
//			lm.Name = storageName.Text;
//			Console.WriteLine("write Name to memory;"+lm.Name);
//			lm.telephone = this.keyContact.Text;
//			Console.WriteLine("write Telephone# to memory;"+lm.telephone);
//			lm.address = this.address.Text;
//			Console.WriteLine("write add to mem:"+lm.address);
//			lm.height = -1;
//			lm.width = -1;
//			lm.depth = -1; 
//			try{
//				lm.height = Convert.ToInt32(this.x.Text);
//				lm.width = Convert.ToInt32(this.y.Text);
//				lm.depth = Convert.ToInt32 (this.z.Text);
//			}catch(Exception e){
//				Console.WriteLine("err:"+e.ToString());
//				this.textDimensions.Text = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Dimensions must be in numbers only", "Dimensions must be in numbers only");
//			}
//
//			lm.postnr = this.zipField.Text;
//			lm.poststed = this.poststedField.Text;
//
//
//			dao.updateLager(lm);
//
//		}

		public void SaveAll(){


			ReleaseKeyboard ();

			if(lm != null){
			lm.Name = storageName.Text;
			Console.WriteLine("write Name to memory;"+lm.Name);
			lm.telephone = this.keyContact.Text;
			Console.WriteLine("write Telephone# to memory;"+lm.telephone);
			lm.address = this.address.Text;
			Console.WriteLine("write add to mem:"+lm.address);
			lm.height = -1;
			lm.width = -1;
			lm.depth = -1; 
			try{
				lm.height = Convert.ToInt32(this.x.Text);
				lm.width = Convert.ToInt32(this.y.Text);
				lm.depth = Convert.ToInt32 (this.z.Text);
			}catch(Exception e){
				Console.WriteLine("err:"+e.ToString());
				this.textDimensions.Text = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Dimensions must be in numbers only", "Dimensions must be in numbers only");
			}
			
			lm.postnr = this.zipField.Text;
			lm.poststed = this.poststedField.Text;

			dao.SaveLager(lm);
			raiseLagerSaved (lm);
//			dao.updateLager(lm);
			}
		}

		void raiseLagerSaved (Lager lager)
		{
			var handler = this.LagerSaved;
			if (handler != null) {
				handler(this, new LagerClickedEventArgs(lager));
			}
		}
	}
}

