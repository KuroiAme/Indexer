using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.MessageUI;
using MonoTouch.UIKit;
using Xamarin.Themes;
using no.dctapps.Garageindex;
using No.DCTapps.GarageIndex;
using no.dctapps.Garageindex.businesslogic;
using no.dctapps.Garageindex.model;
using no.dctapps.Garageindex.screens;


namespace no.dctapps.garageindex.screens
{
	public partial class Email : UtilityViewController
	{

		UIButton SendMailBtn;
		GarageindexBL bl;
		Lager lm;
		MFMailComposeViewController mailContr;
		RectangleF mail;

//		static bool UserInterfaceIdiomIsPhone {
//			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
//		}

		public Email ()
			: base (UserInterfaceIdiomIsPhone ? "Email_iPhone" : "Email_iPad")
		{
			bl = new GarageindexBL();
			lm = bl.GetActiveActiveLager ();
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void LoadView ()
		{
			base.LoadView ();

			mail = new RectangleF (10, 10, 10, 10);
			if(UserInterfaceIdiomIsPhone)
			{
				mail = new RectangleF(60, 260, 200, 57);
			}else{
				mail = new RectangleF(250, 500, 200, 57);
			}

			SendMailBtn = UIButton.FromType(UIButtonType.RoundedRect);
			SendMailBtn.Frame = mail;
//			var mailtitle = NSBundle.MainBundle.LocalizedString ("index email", "index email");
//			textPictureAttachments.Text = NSBundle.MainBundle.LocalizedString ("Picture attachments", "Picture attachments");
//			textItems.Text = NSBundle.MainBundle.LocalizedString ("Include item images", "Include item images");
//			SendMailBtn.SetTitle(mailtitle, UIControlState.Normal);

			View.AddSubview(SendMailBtn);
//			if(!InSimulator())
//			{
//				BlackLeatherTheme.Apply(switchPictureAttachments, "");
//				BlackLeatherTheme.Apply(textPictureAttachments);
//				BlackLeatherTheme.Apply(textItems);
//				BlackLeatherTheme.Apply(SendMailBtn, "");
//				BlackLeatherTheme.Apply (this.View);
//			}
		}

		void cleanup ()
		{
			lm = null;
//			dao = null;
			SendMailBtn = null;
			bl = null;
			mailContr = null;
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			mailContr = new MFMailComposeViewController();
			
			mailContr.Finished += (object sender, MFComposeResultEventArgs args) => {
				
				Console.WriteLine(args.Result.ToString());
				args.Controller.DismissViewController(true, delegate{});
			};
			
			SendMailBtn.TouchUpInside += (sender,e) => {
				
				mailContr.SetSubject(bl.GenerateSubject(lm));
				mailContr.SetMessageBody(bl.GenerateManifest(lm), false);
				
//				if(switchPictureAttachments.On){
//					Console.WriteLine("adding picture attachments");
//					bl.AddPictureAttachments(mailContr, this.switchItems.On);
//				}
				
				this.PresentViewController(mailContr, true, delegate{});
			};
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}
	}
}

