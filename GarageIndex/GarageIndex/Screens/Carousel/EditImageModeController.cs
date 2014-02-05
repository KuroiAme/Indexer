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
using GoogleAnalytics.iOS;

namespace GarageIndex
{
	public class EditImageModeController : UIViewController
	{
		GalleryObject go;

		TagGraphicsView tgv;

		UIScrollView scrollView;

		UIView blend;

		public EditImageModeController (GalleryObject go)
		{
			this.go = go;

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			RectangleF myBounds = UIScreen.MainScreen.Bounds;

			scrollView = new UIScrollView (myBounds);


			var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var gallerydirectory = Path.Combine (documentsDirectory, "gallery");
			string imagefilename = go.imageFileName;
			string path = Path.Combine (gallerydirectory, imagefilename);
			UIImage image = UIImage.FromFile (path);
			var CanvasSize = image.Size;
			RectangleF Canvas = new RectangleF (new PointF (0, 0), CanvasSize);
			UIImageView iv = new UIImageView (image);


			tgv = new TagGraphicsView (go, Canvas);

			scrollView.ContentSize = image.Size;



			scrollView.MaximumZoomScale = 3f;
			scrollView.MinimumZoomScale = .1f;
			scrollView.SetZoomScale (1f, true);

			blend = new UIView (Canvas);
			blend.Frame = Canvas;
			blend.Opaque = true;
			blend.BackgroundColor = UIColor.Clear;
			blend.AddSubview(iv);
			blend.AddSubview(tgv);

			scrollView.AddSubview (blend);
			this.View = scrollView;

//			blend.SetNeedsDisplay ();

//			scrollView.SetNeedsDisplay ();
//			blend.SetNeedsDisplay ();


			scrollView.ViewForZoomingInScrollView += (UIScrollView sv) => blend;

			var doubletap = new UITapGestureRecognizer (AddTag);
			doubletap.NumberOfTapsRequired = 2;
			scrollView.AddGestureRecognizer (doubletap);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Gallery Edit Image mode");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
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
				PointF origo = gestureRecognizer.LocationInView(blend);
				var image = gestureRecognizer.View;
				var imageRect = image.Bounds;
				Console.WriteLine("imageRect:"+imageRect);
				Console.WriteLine("locInView():"+gestureRecognizer.LocationInView(this.blend));
				var woot = scrollView.ContentOffset;
				var woot2 = scrollView.ContentSize;
				RectangleF mywoot = new RectangleF(woot, woot2);
				Console.WriteLine("mywoot:"+mywoot);
				Console.WriteLine("image:"+image);
				//				tag.StoreRectangleF(gestureRecognizer.LocationInView();)
				tag.StoreRectangleF(imageRect);
				AppDelegate.dao.SaveTag(tag);
				Console.WriteLine("tagtext:"+tag.TagString);
				Console.WriteLine("spot:"+tag.FetchAsRectangleF());
				tgv.SetNeedsDisplay();
			};

			av.Show();
		}
	}
}

