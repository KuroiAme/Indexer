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

		RectangleF MakeRectFromCenter (PointF again, SizeF meh)
		{
			PointF origoRect = new PointF (again.X - meh.Width, again.Y - meh.Height);
			return new RectangleF (origoRect, meh);
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
				var scale = scrollView.ZoomScale;
				RectangleF contentFrame = new RectangleF(scrollView.ContentOffset.X / scale, scrollView.ContentOffset.Y / scale, scrollView.Frame.Size.Width / scale, scrollView.Frame.Size.Height /scale);
				contentFrame.Y = contentFrame.Y + this.NavigationController.View.Bounds.Y;
				contentFrame.X = contentFrame.X + this.NavigationController.View.Bounds.X;
				tag.StoreRectangleF(contentFrame);
				AppDelegate.dao.SaveTag(tag);
				Console.WriteLine("tagtext:"+tag.TagString);
				Console.WriteLine("spot:"+tag.FetchAsRectangleF());
				tgv.SetNeedsDisplay();
			};

			av.Show();
		}
	}
}

