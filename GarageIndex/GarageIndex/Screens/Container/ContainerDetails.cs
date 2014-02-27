using System;
using System.Drawing;
using MonoTouch.UIKit;
using System.Collections.Generic;
using no.dctapps.Garageindex.model;
using GarageIndex;
using no.dctapps.Garageindex.tables;
using no.dctapps.Garageindex.events;
using System.Text;
using No.Dctapps.GarageIndex;
using MonoTouch.MessageUI;
using no.dctapps.Garageindex.businesslogic;
using GoogleAnalytics.iOS;

namespace no.dctapps.Garageindex.screens
{
	public partial class ContainerDetails : UIViewController
	{
		LagerObject boks;
		ContainerDetailsContent cdc;

		public event EventHandler<LagerObjectSavedEventArgs> LagerObjectSaved;
		
		
		public ContainerDetails (LagerObject boks)
		{
			this.boks = boks;
			cdc = new ContainerDetailsContent (boks);
		}
		
		public ContainerDetails () 
		{
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Container detail Screen");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			//Title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Container details", "Container details");
			ShowDetails (boks);
		}

		UIScrollView innerview;

		public void ShowDetails (LagerObject boks)
		{
			this.boks = boks;
			cdc = new ContainerDetailsContent (boks, NavigationController);
			innerview = new UIScrollView (UIScreen.MainScreen.Bounds);
			innerview.ContentSize = cdc.GetContentsize ();
			innerview.AddSubview (cdc.View);
			innerview.BackgroundColor = UIColor.White;
			cdc.ShowDetails (boks);
			this.View = innerview;

			cdc.LagerObjectSaved += (object sender, LagerObjectSavedEventArgs e) => {
				var handler = this.LagerObjectSaved;
				if(handler != null){
					handler(this,e);
				}
			};
<<<<<<< HEAD
			//this.CreateEmailBarButton ();
=======
			CreateEmailBarButton ();
>>>>>>> PCL
		}

		MFMailComposeViewController mailContr;

		private void CreateEmailBarButton ()
		{
			//DO NOT DELETE
			UIBarButtonItem it = new UIBarButtonItem ();
			mailContr = new MFMailComposeViewController();
			mailContr.SetSubject(AppDelegate.bl.GenerateContainerSubject(this.boks));
			mailContr.SetMessageBody(AppDelegate.bl.GenerateContainerManifest(this.boks),false);
			AppDelegate.key.AddQRPictureAttachment(mailContr, this.boks);
			//			bl.AddPictureAttachment(mailContr, this.boks);
			it.Title = "email";
			//IS really info
			it.Clicked += (object sender, EventArgs e) => this.PresentViewController (mailContr, true, delegate {});
			mailContr.Finished += (object sender, MFComposeResultEventArgs e) => mailContr.DismissViewController (true, delegate{});

			this.NavigationItem.SetRightBarButtonItem (it, true);
		}
	}
}
