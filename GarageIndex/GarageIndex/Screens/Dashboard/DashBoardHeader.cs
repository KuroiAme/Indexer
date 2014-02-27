using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace GarageIndex
{
	public class DashBoardHeader : UIViewController
	{
		RectangleF myFrame;
		UIView parentView;
		public DashBoardHeader (RectangleF myFrame)
		{
//			this.parentView = parentView;
			this.myFrame = myFrame;
		}

		public override void LoadView ()
		{
			base.LoadView ();
			this.View.Frame = myFrame;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.View.BackgroundColor = UIColor.Clear;
			AddHeadingLabels ();
		}

//		UILabel nameBox;

		void AddHeadingLabels ()
		{
			var headingRect = new RectangleF(0, 0, View.Bounds.Width, 22);
			UILabel heading = new UILabel (headingRect);
			heading.AdjustsFontSizeToFitWidth = true;
			heading.Text = "Indexer Dashboard";
			heading.TextAlignment = UITextAlignment.Center;
			heading.TextColor = UIColor.White;
			Add (heading);

//			var nameBoxRect = new RectangleF(0, 30, View.Bounds.Width, 22);
//			nameBox = new UILabel (nameBoxRect);
//			nameBox.AdjustsFontSizeToFitWidth = true;
//			nameBox.Text = AppDelegate.bl.GetUserName ();
//			nameBox.TextAlignment = UITextAlignment.Center;
//			nameBox.TextColor = UIColor.White;
//			Add (nameBox);
//
//			var doubletap = new UITapGestureRecognizer (ChangeName);
//			doubletap.NumberOfTapsRequired = 2;
//			nameBox.AddGestureRecognizer (doubletap);


		}

//		void ChangeName (UITapGestureRecognizer gestureRecognizer){
//			Console.WriteLine ("Name doubletapped");
//			var cr8 = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Done", "Done");
//			var input = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Enter your name", "Enter your name");
//			var abort = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Cancel", "Cancel");
//			UIAlertView av = new UIAlertView (input, "\n", null, abort, new string[] {
//				cr8
//			});
//			av.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
//			int Create = av.FirstOtherButtonIndex;
//			av.Clicked += (object sender, UIButtonEventArgs e) =>  {
//				if (e.ButtonIndex == Create) {
//					String nameText = av.GetTextField (0).Text;
//					AppDelegate.bl.SaveUserName(nameText);
//					nameBox.Text = nameText;
//				}
//			};
//			av.Show ();
//		}

	}
}

