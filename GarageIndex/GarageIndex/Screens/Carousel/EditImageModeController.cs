using System;
using MonoTouch.UIKit;
using iCarouselSDK;
using System.Drawing;
using System.Collections.Generic;
using no.dctapps.Garageindex.events;
using MonoTouch.Foundation;
using System.IO;
using System.Linq;
using MonoTouch.CoreGraphics;
using MonoTouch.ObjCRuntime;
//using paintcode;

namespace GarageIndex
{
	public class EditImageModeController : UIViewController
	{
		GalleryObject go;

		TagGraphicsView tgv;

		public EditImageModeController (GalleryObject go)
		{
			this.go = go;
//			var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
//			var gallerydirectory = Path.Combine (documentsDirectory, "gallery");
//			string imagefilename = go.imageFileName;
//			string path = Path.Combine (gallerydirectory, imagefilename);

//			Console.WriteLine ("path:" + path);
//			UIImage currentImage = UIImage.FromFile (path);
//			UIImageView iv = new UIImageView (this.View.Bounds);
//			iv.Image = currentImage;
//			this.View.AddSubview (iv);



			tgv = new TagGraphicsView (go){Frame = UIScreen.MainScreen.Bounds};
			this.View.AddSubview (tgv);

//			this.View.SendSubviewToBack (iv);
			tgv.SetNeedsDisplay ();

			tgv.UserInteractionEnabled = true;

			var pinchGesture = new UIPinchGestureRecognizer (ScaleImage);
			//			pinchGesture.Delegate = new GestureDelegate (this);
			tgv.AddGestureRecognizer (pinchGesture);

			var panGesture = new UIPanGestureRecognizer (PanImage);
			panGesture.MaximumNumberOfTouches = 2;
			//			panGesture.Delegate = new GestureDelegate (this);
			tgv.AddGestureRecognizer (panGesture);

			var longPressGesture = new UILongPressGestureRecognizer (ShowResetMenu);
			tgv.AddGestureRecognizer (longPressGesture);

			var doubletap = new UITapGestureRecognizer (AddTag);
			doubletap.NumberOfTapsRequired = 2;
			tgv.AddGestureRecognizer (doubletap);


		}

		void AddTag (UITapGestureRecognizer gestureRecognizer){
			Console.WriteLine ("ADDTAG");
			ImageTag tag = new ImageTag ();
			tag.GalleryObjectID = go.ID;


			UIAlertView av = new UIAlertView ("input tags, comma seperated", "\n", null, "Cancel", new string[] {"Create"});
			av.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
			av.Clicked += (object sender, UIButtonEventArgs e) => {
				String tagText = av.GetTextField (0).Text;
				tag.TagString = tagText;
//				PointF center = gestureRecognizer.LocationOfTouch(0,tgv);
//				RectangleF newframe = new RectangleF(center.X,center.Y, tgv.Bounds.Width,tgv.Bounds.Height);
				tag.StoreRectangleF(gestureRecognizer.View.Bounds);
				AppDelegate.dao.SaveTag(tag);
				Console.WriteLine("tagtext:"+tag.TagString);
				Console.WriteLine("spot:"+tag.FetchAsRectangleF());
				tgv.SetNeedsDisplay();
			};

			av.Show();
		}



		void PanImage (UIPanGestureRecognizer gestureRecognizer)
		{
			AdjustAnchorPointForGestureRecognizer (gestureRecognizer);
			var image = gestureRecognizer.View;
			if (gestureRecognizer.State == UIGestureRecognizerState.Began || gestureRecognizer.State == UIGestureRecognizerState.Changed) {
				var translation = gestureRecognizer.TranslationInView (View);
				image.Center = new PointF (image.Center.X + translation.X, image.Center.Y + translation.Y);
				// Reset the gesture recognizer's translation to {0, 0} - the next callback will get a delta from the current position.
				gestureRecognizer.SetTranslation (PointF.Empty, image);
			}
		}

		void ScaleImage (UIPinchGestureRecognizer gestureRecognizer)
		{
			AdjustAnchorPointForGestureRecognizer (gestureRecognizer);
			if (gestureRecognizer.State == UIGestureRecognizerState.Began || gestureRecognizer.State == UIGestureRecognizerState.Changed) {
				gestureRecognizer.View.Transform *= CGAffineTransform.MakeScale (gestureRecognizer.Scale, gestureRecognizer.Scale);
				// Reset the gesture recognizer's scale - the next callback will get a delta from the current scale.
				gestureRecognizer.Scale = 1;
			}
		}

		void AdjustAnchorPointForGestureRecognizer (UIGestureRecognizer gestureRecognizer)
		{
			if (gestureRecognizer.State == UIGestureRecognizerState.Began) {
				var image = gestureRecognizer.View;
				var locationInView = gestureRecognizer.LocationInView (image);
				var locationInSuperview = gestureRecognizer.LocationInView (image.Superview);

				image.Layer.AnchorPoint = new PointF (locationInView.X / image.Bounds.Size.Width, locationInView.Y / image.Bounds.Size.Height);
				image.Center = locationInSuperview;
			}
		}
		UIView imageForReset;
		// Display a menu with a single item to allow the piece's transform to be reset
		[Export("ShowResetMenu")]
		void ShowResetMenu (UILongPressGestureRecognizer gestureRecognizer)
		{
			if (gestureRecognizer.State == UIGestureRecognizerState.Began) {
				var menuController = UIMenuController.SharedMenuController;
				var resetMenuItem = new UIMenuItem ("Reset", new Selector ("ResetImage"));
				var location = gestureRecognizer.LocationInView (gestureRecognizer.View);
				BecomeFirstResponder ();
				menuController.MenuItems = new [] { resetMenuItem };
				menuController.SetTargetRect (new RectangleF (location.X, location.Y, 0, 0), gestureRecognizer.View);
				menuController.MenuVisible = true;
				//                                menuController.Animated = true;
				imageForReset = gestureRecognizer.View;
			}
		}
	}
}

