using System;
using MonoTouch.Foundation;
using no.dctapps.Garageindex.model;
using no.dctapps.Garageindex.events;
using MonoTouch.UIKit;
using MonoTouch.MessageUI;
using no.dctapps.Garageindex.businesslogic;
using GarageIndex;
using GoogleAnalytics.iOS;
using System.Drawing;
using System.Collections.Generic;
using IndexerIOS;

namespace no.dctapps.Garageindex.screens
{
	public partial class TheStorageScreen : UtilityViewController
	{
		Lager lm;
//		LagerDAO dao;
//		GarageindexBL bl;

		StorageScreenContent innerViewController;

		public event EventHandler<LagerClickedEventArgs> LagerSaved;

		protected override void Dispose (bool disposing)
		{
			lm = null;
			innerViewController.Dispose ();
			LagerSaved = null;
			base.Dispose (disposing);
		}

		void cleanup ()
		{
			Dispose ();
		}


		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			//cleanup only if view is loaded and not in a window.
			if(this.IsViewLoaded && this.View.Window == null){
				//cleanup ();
			}
			// Release any cached data, images, etc that aren't in use.
		}


		public static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public TheStorageScreen (Lager lager)
		{
			lm = lager;
		}

		public TheStorageScreen ()
		{
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Storage edit Screen");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}

		public void ShowDetails (Lager lm)
		{
			this.lm = lm;
			this.innerViewController.ShowDetails (lm);
		}




//		private void CreateEmailBarButton ()
//		{
//			//DO NOT DELETE
//			UIBarButtonItem it = new UIBarButtonItem ();
//			it.Title = "email";
//			//IS really info
//			it.Clicked += (object sender, EventArgs e) =>  {
//                MakeEmail ();
//			};
//			this.NavigationItem.SetRightBarButtonItem (it, true);
//		}


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

//		public override void LoadView ()
//		{
//			base.LoadView ();

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



//		void cleanup ()
//		{
//
//		}
//
//		void Unclean ()
//		{
//
//		}
//
//		public override void DidReceiveMemoryWarning ()
//		{
//			// Releases the view if it doesn't have a superview.
//			base.DidReceiveMemoryWarning ();
//
//			if(this.IsViewLoaded && this.View.Window == null){
//				//cleanup ();
//			}
//			
//			// Release any cached data, images, etc that aren't in use.
//		}
//
//		public void SetPlaceholders ()
//		{
//			var addresse = NSBundle.MainBundle.LocalizedString ("Address", "Address");
//			var key = NSBundle.MainBundle.LocalizedString ("Key contact", "Key contact");
//			var zip = NSBundle.MainBundle.LocalizedString ("Zip Code", "Zip Code");
//			var city = NSBundle.MainBundle.LocalizedString ("City", "City");
//
//			address.Placeholder = addresse;
//			keyContact.Placeholder = key;
//			poststedField.Placeholder = city;
//			zipField.Placeholder = zip;
//			textDimensions.Text = NSBundle.MainBundle.LocalizedString ("Dimensions height,width,depth e.g. meters/yards", "Dimensions height,width,depth e.g. meters/yards");
//			textEstimate.Text = NSBundle.MainBundle.LocalizedString ("Your storage fill degree estimate", "Your storage fill degree estimate");
//			textSpaceAvailable.Text = NSBundle.MainBundle.LocalizedString ("Space available", "Space available");

//			BlackLeatherTheme.Apply(this.storageName, "");
//			BlackLeatherTheme.Apply(this.address, "");
//			BlackLeatherTheme.Apply(this.textDimensions, "");
//			BlackLeatherTheme.Apply(this.textEstimate, "");
//			BlackLeatherTheme.Apply(this.textSpaceAvailable, "");
//			BlackLeatherTheme.Apply(this.keyContact, "");
//			BlackLeatherTheme.Apply(this.poststedField, "");
//			BlackLeatherTheme.Apply(this.zipField, "");
//			BlackLeatherTheme.Apply(this.x, "");
//			BlackLeatherTheme.Apply(this.y, "");
//			BlackLeatherTheme.Apply(this.z, "");
//			BlackLeatherTheme.Apply(this.prod, "");
//			BlackLeatherTheme.Apply(this.fillEst, "");
//			BlackLeatherTheme.Apply(this.remaining, "");
//			BlackLeatherTheme.Apply(this.M3, "");
//			BlackLeatherTheme.Apply(this.M3_2, "");
//		}

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

		public override void LoadView ()
		{
			base.LoadView ();
			this.View.Frame = new RectangleF (0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
		}

		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.NavigationController.NavigationBar.BackgroundColor = UIColor.Clear;

			RectangleF neo = new RectangleF (0, 0, UIScreen.MainScreen.Bounds.Width, 1000);

			var imgView = new UIImageView(BlueSea.MakeBlueSea()){
				ContentMode = UIViewContentMode.ScaleToFill,
				AutoresizingMask = UIViewAutoresizing.All,
				Frame = neo
			};


			UIScrollView scrollview = new UIScrollView (View.Bounds);
			innerViewController = new StorageScreenContent (View.Bounds, lm, scrollview, this);
			innerViewController.View.UserInteractionEnabled = true;
			scrollview.UserInteractionEnabled = true;
			this.View.UserInteractionEnabled = true;
			scrollview.ContentSize = neo.Size;
			scrollview.AddSubview (innerViewController.View);
			scrollview.AddSubview (imgView);
			scrollview.SendSubviewToBack (imgView);
			scrollview.BackgroundColor = UIColor.Clear;
			Add (scrollview);

//			innerview.ContentSize = cdc.GetContentsize ();
//			innerview.AddSubview (cdc.View);
//			innerview.BackgroundColor = UIColor.Clear;

		


//			UIView mymy = this.View;
//
//			BlackLeatherTheme.Apply (this.View);

//			SetPlaceholders ();
//			Title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Storage Data", "Storage Data");

//			fillEst.MinValue = 0f;
//			fillEst.MaxValue = 1f;
//			fillEst.Value = 0.5f; // initial value
//			fh = new FileHandler ();
//			dao = new LagerDAO();
//			lm = dao.readTopItem ();
//			lm = fh.ReadLagerData ();

			ShowDetails (lm);

//			AddContactKeyButtons ();


	

//			storageName.CommitEditing +=(sender, e) => {
//				lm.Name = storageName.Text;
//				
//			};

//			var b = new GlassButton(bounds){
////				Font = 
//			};

			// Perform any additional setup after loading the view, typically from a nib.

		}



//		void HandleValueChanged (object sender, EventArgs e)
//		{   // display the value in a label
//			//			label.Text = slider.Value.ToString ();
//			Calculate ();
//		}
//
//		public override void ViewWillAppear (bool animated){
//			base.ViewWillAppear (animated);
//			Calculate ();
////			Unclean();
//			
//		}

//		void ReleaseKeyboard ()
//		{
//			this.storageName.ShouldReturn += textField =>  {
//				textField.ResignFirstResponder ();
//				return true;
//			};
//			this.keyContact.ShouldReturn += textField =>  {
//				textField.ResignFirstResponder ();
//				return true;
//			};
//			this.address.ShouldReturn += (textField) => { 
//				textField.ResignFirstResponder();
//				return true; 
//			};
//			this.x.ShouldReturn += (textField) => { 
//				textField.ResignFirstResponder();
//				return true; 
//			};
//			this.y.ShouldReturn += (textField) => { 
//				textField.ResignFirstResponder();
//				return true; 
//			};
//			this.z.ShouldReturn += (textField) => { 
//				textField.ResignFirstResponder();
//				return true; 
//			};
//		}

//		void Calculate ()
//		{
//			try{
//				lm.height = Convert.ToInt32(this.x.Text);
//				lm.width = Convert.ToInt32(this.y.Text);
//				lm.depth = Convert.ToInt32 (this.z.Text);
//			}catch(Exception e){
//				Console.WriteLine("disaster avoided:"+e.ToString());
//			}
//
//			if (lm != null) {
//				int dm3 = lm.height * lm.depth * lm.width;
//				this.prod.Text = "" + dm3;
//				double percentile = (double)fillEst.Value / 100;
//				int estRemaining = (int)(dm3 - (percentile * dm3));
//				this.remaining.Text = "" + estRemaining;
//			}
//		}




//
//		void raiseLagerSaved (Lager lager)
//		{
//			var handler = this.LagerSaved;
//			if (handler != null) {
//				handler(this, new LagerClickedEventArgs(lager));
//			}
//		}
	}
}

