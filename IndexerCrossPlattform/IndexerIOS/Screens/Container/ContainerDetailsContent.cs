using System;
using no.dctapps.Garageindex.model;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.screens;
using System.Drawing;
using no.dctapps.Garageindex.events;
using System.Text;
using MonoTouch.MessageUI;

namespace GarageIndex
{
	public class ContainerDetailsContent : UtilityViewController
	{
		LagerObject boks;
		public UIPopoverController popme;
		SelectLager lagerselect;
		const bool test = true;
		UINavigationController nc;

		public event EventHandler<LagerObjectSavedEventArgs> LagerObjectSaved;

		public ContainerDetailsContent (LagerObject boks)
		{
			this.boks = boks;
		}

		public ContainerDetailsContent (LagerObject boks, UINavigationController navigationController)
		{
			this.boks = boks;
			this.nc = navigationController;
		}

		public SizeF GetContentsize ()
		{
			return this.View.Bounds.Size;
		}

		public ContainerDetailsContent (UINavigationController nc)
		{
			this.nc = nc;
		}

		public override void LoadView ()
		{
			base.LoadView ();
			InitView ();
			InitializeNibLegacy ();

		}

		private void InitView(){
			this.View.BackgroundColor = UIColor.White;
			if (UserInterfaceIdiomIsPhone) {
				this.View.Frame = new RectangleF (0, 0, UIScreen.MainScreen.Bounds.Width, 800);
			} else {
				this.View.Frame = new RectangleF (0, 0, UIScreen.MainScreen.Bounds.Width, 1000);
			}
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

		UITextField fieldContainerName;
		UITextField fieldDescription;
		UITextField fieldType;
		UIButton inStorage;
		UIButton btnShowContent;

		void InitializeNibLegacy ()
		{
			RectangleF fieldContainerNameRect;
			RectangleF fieldDescriptionRect;
			RectangleF fieldTypeRect;
			RectangleF inStorageRect;
			RectangleF btnShowContentRect;
			float x;
			float y;
			if (UserInterfaceIdiomIsPhone) {
				x = 10;
				y = 50; 
			} else {
				x = 30;
				y = 80;
			}
			float broad = 250;
			float lineheight = 30;
			float tagheight = 150;
			float linemargin = 10;
			fieldContainerNameRect = new RectangleF (x, y, broad,lineheight);
			fieldDescriptionRect = new RectangleF (x, y + lineheight + linemargin, broad, lineheight);
			fieldTypeRect = new RectangleF (x, y + lineheight * 2 + linemargin * 2, broad, lineheight);
			inStorageRect = new RectangleF (x, y + tagheight + linemargin * 3 + lineheight * 3, broad, lineheight);
			btnShowContentRect = new RectangleF (x, y + tagheight + linemargin * 4 + lineheight * 4, broad, lineheight);

			fieldContainerName = new UITextField (fieldContainerNameRect);
			fieldContainerName.BorderStyle = UITextBorderStyle.RoundedRect;
			Add (fieldContainerName);

			fieldDescription = new UITextField (fieldDescriptionRect);
			fieldDescription.BorderStyle = UITextBorderStyle.RoundedRect;
			Add (fieldDescription);

			fieldType = new UITextField (fieldTypeRect);
			fieldType.BorderStyle = UITextBorderStyle.RoundedRect;
			Add (fieldType);

			inStorage = new UIButton (UIButtonType.RoundedRect);
			inStorage.Frame = inStorageRect;
			Add (inStorage);

			btnShowContent = new UIButton (UIButtonType.RoundedRect);
			btnShowContent.Frame = btnShowContentRect;
			Add (btnShowContent);
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
			this.fieldType.ShouldReturn += textField => {
				textField.ResignFirstResponder();
				return true;
			};
		}

		void initializeMoveLager ()
		{

			lagerselect = new SelectLager ();

			this.inStorage.TouchUpInside += (object sender, EventArgs e) =>  {
				if(UserInterfaceIdiomIsPhone){
					nc.PushViewController(lagerselect, true);
				}else{
					popme = new UIPopoverController (lagerselect);
					popme.PresentFromRect (this.inStorage.Bounds, this.View, UIPopoverArrowDirection.Up, true);
				}
			};
			lagerselect.DismissEvent += (object sender, LagerClickedEventArgs e) =>  {
				if(UserInterfaceIdiomIsPhone){
					nc.PopViewControllerAnimated(true);
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
			this.fieldType.Text = myBox.type;

			//luckily no picture to display


			this.AddTagList ();
		}

		TagListController tlc;

		void AddTagList ()
		{
			RectangleF frame;

			if (UserInterfaceIdiomIsPhone) {
				frame = new RectangleF (30, 185, 300, 125);
			} else {
				frame = new RectangleF (30, 200, 300, 125);
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

			tlc = new TagListController (tag, frame);
			this.Add (tlc.View);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			ShowDetails (this.boks);

			Title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Container details", "Container details");
			this.fieldContainerName.Placeholder = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Box Identifier", "Box Identifier");
			this.fieldDescription.Placeholder = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Description", "Description");
			this.fieldType.Placeholder = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Type","Type");

			//			var title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("save", "save");
			//			this.save.SetTitle(title, UIControlState.Normal);

			//			this.save.TouchUpInside += HandleTouchUpInside;

			this.btnShowContent.SetTitle(MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Show Content", "Show content"),UIControlState.Normal);

			this.btnShowContent.TouchUpInside += (object sender, EventArgs e) => {
				ContainerContent cc = new ContainerContent(boks);
				nc.PushViewController(cc, true);
			};

			if(!UserInterfaceIdiomIsPhone){
				this.fieldContainerName.Ended += (object sender, EventArgs e) => SaveIt ();

				this.fieldDescription.Ended += (object sender, EventArgs e) => SaveIt ();
				this.fieldType.Ended += (object sender, EventArgs e) => SaveIt();
			}


			ReleaseKeyboard ();
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
			this.boks.type = this.fieldType.Text;

			AppDelegate.dao.SaveLagerObject (boks);
			RaiseSavedEvent();

		}
	}


}

