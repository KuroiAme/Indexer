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
	public partial class ContainerDetails : UtilityViewController
	{
		LagerObject boks;
//		public UITableView Table;
		//		IList<Item> tableitems;

		public UIPopoverController popme;
		SelectLager lagerselect;
		const bool test = true;
		//		UIBarButtonItem edit, done, insert;
		
		//		public event EventHandler<GotPictureEventArgs> GotPicture;

		public event EventHandler<LagerObjectSavedEventArgs> LagerObjectSaved;
		
		
		public ContainerDetails (LagerObject boks)
			: base (UserInterfaceIdiomIsPhone ? "ContainerDetails_iPhone" : "ContainerDetails_iPad")
		{
			this.boks = boks;
		}
		
		public ContainerDetails () 
			: base (UserInterfaceIdiomIsPhone ? "ContainerDetails_iPhone" : "ContainerDetails_iPad")
		{
		}
		
		void RaiseSavedEvent ()
		{
			var handler = this.LagerObjectSaved;
			Console.WriteLine("saved");
			if (handler != null) {
				handler(this, new LagerObjectSavedEventArgs());
			}
		}
		
		/// <summary>
		/// Release everything not in use
		/// </summary>
		void cleanup ()
		{
			boks = null;
//			dao = null;
			//			table = null;
			//			itemtableSource = null;
		}
		
//		void Unclean ()
//		{
//			if(dao == null)
//			{
//				dao = new LagerDAO();
//			}
//
//		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Container detail Screen");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			//cleanup only if view is loaded and not in a window.
			if(this.IsViewLoaded && this.View.Window == null){
				cleanup ();
			}
			// Release any cached data, images, etc that aren't in use.
		}
		
		/// <summary>
		/// Releases the keyboard aka. resign first responder. Otherwise keyboard wont go away.
		/// </summary>
		public void ReleaseKeyboard ()
		{
			this.fieldContainerName.ShouldReturn += textField =>  {
				textField.ResignFirstResponder ();
				return true;
			};
			this.fieldDescription.ShouldReturn += textField =>  {
				textField.ResignFirstResponder ();
				return true;
			};
			this.textType.ShouldReturn += textField => {
				textField.ResignFirstResponder();
				return true;
			};
		}

		void initializeMoveLager ()
		{

			lagerselect = new SelectLager ();

			this.inStorage.TouchUpInside += (object sender, EventArgs e) =>  {
				if(UserInterfaceIdiomIsPhone){
					this.NavigationController.PushViewController(lagerselect, true);
				}else{
					popme = new UIPopoverController (lagerselect);
					popme.PresentFromRect (this.inStorage.Bounds, this.View, UIPopoverArrowDirection.Up, true);
				}
			};
			lagerselect.DismissEvent += (object sender, LagerClickedEventArgs e) =>  {
				if(UserInterfaceIdiomIsPhone){
					NavigationController.PopToViewController(this, true);
				}else{
					popme.Dismiss (true);
				}
				this.boks.LagerID = e.Lager.ID;
				SetLagerButtonLabel (this.boks);
				AppDelegate.dao.SaveLagerObject(this.boks);
			};
		}

		void SetLagerButtonLabel (LagerObject itty)
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
			this.inStorage.SetTitle (sb.ToString (), UIControlState.Normal);
		}

		MFMailComposeViewController mailContr;

		private void CreateEmailBarButton ()
		{
			//DO NOT DELETE
			UIBarButtonItem it = new UIBarButtonItem ();
			mailContr = new MFMailComposeViewController();
			mailContr.SetSubject(AppDelegate.bl.GenerateContainerSubject(this.boks));
			mailContr.SetMessageBody(AppDelegate.bl.GenerateContainerManifest(this.boks),false);
			AppDelegate.bl.AddQRPictureAttachment(mailContr, this.boks);
//			bl.AddPictureAttachment(mailContr, this.boks);
			it.Title = "email";
			//IS really info
			it.Clicked += (object sender, EventArgs e) => this.PresentViewController (mailContr, true, delegate {});
			mailContr.Finished += (object sender, MFComposeResultEventArgs e) => mailContr.DismissViewController (true, delegate{});

			this.NavigationItem.SetRightBarButtonItem (it, true);
		}
		
		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.ShowDetails (this.boks);
//			Unclean();
		}

		public void ShowDetails (LagerObject myBox)
		{
			if(myBox == null){
				myBox = new LagerObject();
				myBox.isContainer = "true";
			}

			boks = myBox;

			
			this.fieldContainerName.Text = myBox.Name;
			this.fieldDescription.Text = myBox.Description;
			this.textType.Text = myBox.type;
			
			//luckily no picture to display

			this.CreateEmailBarButton ();
			this.AddTagList ();
		}

		void AddTagList ()
		{
			RectangleF frame;

			if (UserInterfaceIdiomIsPhone) {
				frame = new RectangleF (30, 185, UIScreen.MainScreen.Bounds.Width, 125);
			} else {
				frame = new RectangleF (100, 180, UIScreen.MainScreen.Bounds.Width, 125);
			}

			Console.WriteLine("frame:"+frame);
			ImageTag tag = AppDelegate.dao.GetImageTagById (this.boks.ImageTagId);
			if (tag == null) {
				Console.WriteLine ("Tag er null");
				tag = new ImageTag ();
				int key = AppDelegate.dao.SaveTag (tag);
				tag.ID = key;
				Console.WriteLine ("key:" + key);
				this.boks.ImageTagId = key;
				AppDelegate.dao.SaveLagerObject (boks);
			}

			TagListController tlc = new TagListController (tag, frame);
			this.View.AddSubview (tlc.View);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			ShowDetails (this.boks);
			
			Title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Container details", "Container details");
			this.fieldContainerName.Placeholder = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Box Identifier", "Box Identifier");
			this.fieldDescription.Placeholder = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Description", "Description");
			this.textType.Placeholder = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Type","Type");

//			var title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("save", "save");
//			this.save.SetTitle(title, UIControlState.Normal);
			
//			this.save.TouchUpInside += HandleTouchUpInside;

			this.btnShowContent.SetTitle(MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Show Content", "Show content"),UIControlState.Normal);

			this.btnShowContent.TouchUpInside += (object sender, EventArgs e) => {
				ContainerContent cc = new ContainerContent(boks);
				this.NavigationController.PushViewController(cc, true);
			};

			if(!UserInterfaceIdiomIsPhone){
				this.fieldContainerName.Ended += (object sender, EventArgs e) => SaveIt ();

				this.fieldDescription.Ended += (object sender, EventArgs e) => SaveIt ();
				this.textType.Ended += (object sender, EventArgs e) => SaveIt();
			}
			

			ReleaseKeyboard ();
//			this.NavigationController.Title = "Box Details";
			SetLagerButtonLabel (this.boks);
			initializeMoveLager ();
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			if(UserInterfaceIdiomIsPhone){
				SaveIt();
			}
		}
		
		void SaveIt ()
		{
			this.boks.Name = this.fieldContainerName.Text;
			this.boks.Description = this.fieldDescription.Text;
			this.boks.type = this.textType.Text;
			
			AppDelegate.dao.SaveLagerObject (boks);
			RaiseSavedEvent();
//			this.NavigationController.PopToRootViewController(true);
		}
	}
}

