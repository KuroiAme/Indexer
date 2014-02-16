using System;
using MonoTouch.UIKit;
using System.Drawing;
using System.IO;

namespace GarageIndex
{
	public class ViewInsurancePhoto : UIViewController
	{

		UIScrollView scrollView;
		InsurancePhoto photo;
		UIImageView iv;


		public ViewInsurancePhoto (InsurancePhoto photo)
		{
			this.photo = photo;
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

			scrollView.ContentSize = iv.Frame.Size;



			scrollView.MaximumZoomScale = 3f;
			scrollView.MinimumZoomScale = .1f;
			scrollView.SetZoomScale (1f, true);

//			blend = new UIView (Canvas);
//			blend.Frame = Canvas;
//			blend.Opaque = true;
//			blend.BackgroundColor = UIColor.Clear;
//			blend.AddSubview(iv);
//			blend.AddSubview(tgv);

			scrollView.AddSubview (iv);
			this.View = scrollView;

			scrollView.ViewForZoomingInScrollView += (UIScrollView sv) => iv;
		}
	}
}

