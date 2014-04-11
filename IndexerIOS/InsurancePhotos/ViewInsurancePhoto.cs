using System;
using MonoTouch.UIKit;
using System.Drawing;
using System.IO;

namespace no.dctapps.commons.events
{
	public class ViewInsurancePhoto : UtilityViewController
	{

		UIScrollView scrollView;
		InsurancePhoto photo;
		UIImageView iv;


		public ViewInsurancePhoto (InsurancePhoto photo)
		{
			this.photo = photo;
		}

		protected override void Dispose (bool disposing)
		{

			scrollView.Dispose ();
			iv.Dispose ();
			photo = null;
			base.Dispose (disposing);
		}

		/// <summary>
		/// Release everything not in use
		/// </summary>
		void cleanup ()
		{
			//this.Dispose ();
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

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			RectangleF myBounds = UIScreen.MainScreen.Bounds;

			scrollView = new UIScrollView (myBounds);


			var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var gallerydirectory = Path.Combine (documentsDirectory, "insurancePhotos");
			string imagefilename = photo.ImageFileName;
			string path = Path.Combine (gallerydirectory, imagefilename);
			UIImage image = UIImage.FromFile (path);
			var CanvasSize = image.Size;
			//RectangleF Canvas = new RectangleF (new PointF (0, 0), CanvasSize);

			//			string thumbfilename = AppDelegate.dao.GetThumbfilenameForIndex (index);
			//			string path = Path.Combine (gallerydirectory, thumbfilename);
			//			Console.WriteLine ("path:" + path);
			//			UIImage currentImage = UIImage.FromFile (path);
			//			SizeF dim = currentImage.Size;

			//create new view if none is availble fr recycling
			//			if (iv == null) {
			iv = new UIImageView(new RectangleF(0,0, CanvasSize.Width,CanvasSize.Height)){
				ContentMode = UIViewContentMode.ScaleAspectFill
			};
			//			}
			iv.Image = image;

			scrollView.ContentSize = image.Size;





//			blend = new UIView (Canvas);
//			blend.Frame = Canvas;
//			blend.Opaque = true;
//			blend.BackgroundColor = UIColor.Clear;
//			blend.AddSubview(iv);
//			blend.AddSubview(tgv);

			scrollView.AddSubview (iv);
			this.View = scrollView;

			scrollView.ViewForZoomingInScrollView += (UIScrollView sv) => iv;


			scrollView.MaximumZoomScale = 3f;
			scrollView.MinimumZoomScale = .3f;
			scrollView.SetZoomScale (0.3f, true);
		}
	}
}

