using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace GarageIndex
{
	public class UtilityViewController : UIViewController
	{
		public UtilityViewController (string nib) : base (nib, null)
		{
		}

		public UtilityViewController () : base ()
		{
		}

//		public static bool UserInterfaceIdiomIsPhone {
//			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
//		}
//
//		public static string[] SaveImage (string name, UIImage ourpic)
//		{
//			if(ourpic == null)
//				return new string[2]{"",""};
//			Console.WriteLine ("Save");
//			UIImage thumbPic = ourpic.Scale(new SizeF(50,50)); //measurements taken from CustomCell, alternatly 33x33
//
//			if (ourpic != null) {
//				var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
//				var picname = name + ".png";
//				var thumbpicname = name + "_thumb.png";
//				string pngfileName = System.IO.Path.Combine (documentsDirectory, picname);
//				string thumbpngfileName = System.IO.Path.Combine (documentsDirectory, thumbpicname);
//				NSData imgData = ourpic.AsPNG ();
//				NSData img2Data = thumbPic.AsPNG();
//
//				NSError err = null;
//				if (imgData.Save (pngfileName, false, out err)) {
//					Console.WriteLine ("saved as " + pngfileName);
//				} else {
//					Console.WriteLine ("NOT saved as " + pngfileName + " because" + err.LocalizedDescription);
//				}
//
//				err = null;
//				if (img2Data.Save (thumbpngfileName, false, out err)) {
//					Console.WriteLine ("saved as " + thumbpngfileName);
//					string[] result = new string[2] {picname,thumbpicname};
//					return result;
//
//				} else {
//					Console.WriteLine ("NOT saved as " + thumbpngfileName + " because" + err.LocalizedDescription);
//					return null;
//				}
//			}
//			return new string[2]{"",""};
//		}
//
//		public static void DeleteImage(string name){
//			var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
//			var picname = name + ".png";
//			var thumbpicname = name + "_thumb.png";
//			string pngfileName = System.IO.Path.Combine (documentsDirectory, picname);
//			string thumbpngfileName = System.IO.Path.Combine (documentsDirectory, thumbpicname);
//
//			NSFileManager fm = new NSFileManager();
//
//			NSError err = null;
//
//			if (fm.IsDeletableFile(pngfileName))
//			{
//				fm.Remove(pngfileName, out err);   
//				//TODO use error for something sensible
//			}
//
//			err = null;
//
//			if (fm.IsDeletableFile(thumbpngfileName))
//			{
//				fm.Remove(thumbpngfileName, out err );
//				//TODO use errormsg for something sensible.
//			}
//		}
//
//		public RectangleF ImageRectangle{ get; set;}
//
//		public UIImageView LoadImage(PointF origin,string filename){
//			UIImageView view = new UIImageView();
//			Console.WriteLine("loadImage():"+filename);
//			if (!string.IsNullOrEmpty (filename)) {
//				var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
//				string ffilename = System.IO.Path.Combine (documentsDirectory, filename);
//				UIImage image = UIImage.FromFile (ffilename);
//				if (image != null) {
//					if (UserInterfaceIdiomIsPhone) {
//						ImageRectangle = new RectangleF (origin.X, origin.Y, 300, 200);
//					} else {
//						ImageRectangle = new RectangleF (origin.X, origin.Y, 500, 500);
//					}
//					view = new UIImageView (ImageRectangle);
//					view.Image = image;
//					return view;
//				} else {
//					return new UIImageView (ImageRectangle);
//				}
//			} else
//				return new UIImageView (ImageRectangle);
//		}
//

	}
}

