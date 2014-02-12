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
		UIImageView iv;

		bool mylock;
		bool found;

		public event EventHandler<ThumbChangedEventArgs> ThumbChanged;

		public EditImageModeController (GalleryObject go)
		{
			this.go = go;
			//finds = new List<ImageTag> ();
			mylock = false;
			found = false;

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			tgv.SetNeedsDisplay ();
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


			tgv = new TagGraphicsView (go, Canvas);


			scrollView.ContentSize = iv.Frame.Size;



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

			scrollView.ViewForZoomingInScrollView += (UIScrollView sv) => blend;

			var doubletap = new UITapGestureRecognizer (AddTag);
			doubletap.NumberOfTapsRequired = 2;
			scrollView.AddGestureRecognizer (doubletap);

			var longPressGesture = new UILongPressGestureRecognizer (EditTagFrame);
			scrollView.AddGestureRecognizer (longPressGesture);

			CreateEditBarButton ();

			ExtractNewThumbnail ();
		}

		void EditTagFrame (UIGestureRecognizer gestureRecognizer){
			Console.WriteLine ("edittagframe triggered");
			if (mylock == false) {
				Console.WriteLine ("mutex aquired");
				mylock = true;
				PointF point = gestureRecognizer.LocationInView (this.blend);
//			Console.WriteLine ("presspoint:" + point);
//			PointF center = scrollView.Center;
//			Console.WriteLine("center:"+scrollView.Center);
//			float scale = scrollView.ZoomScale;
//			PointF con = new PointF (scrollView.ContentOffset.X / scale, scrollView.ContentOffset.Y / scale);
//			PointF con = new PointF (scrollView.ContentOffset.X, scrollView.ContentOffset.Y);
//			Console.WriteLine ("con:" + con);
//			PointF guess = new PointF (con.X + point.X, con.Y + point.Y);
//			Console.WriteLine ("guess:" + guess);

				List<RectangleF> Rects = new List<RectangleF> ();
				IList<ImageTag> Tags = AppDelegate.dao.GetTagsByGalleryObjectID (go.ID);
				for (int i = 0; i < Tags.Count; i++) {
					Rects.Add (Tags [i].FetchAsRectangleF ());
				}
				for (int j = 0; j < Rects.Count; j++) {
					if (Rects [j].Contains (point)) {
//						if (finds.Exists (x => x.ID == Tags [j].ID)) {
//							Console.WriteLine ("pushing:" + Tags [j]);
//							finds.Remove (Tags [j]);
						TagDetailScreen tds = new TagDetailScreen (Tags [j]);

						found = true; 
						tds.backpush += (object sender, BackClickedEventArgs e) => {
							Console.WriteLine("mutex released");
							mylock = false;
						};
						this.NavigationController.PushViewController (tds, false);
						break;
//						} else {
//							Console.WriteLine ("adder:" + Tags [j]);
//							Console.WriteLine ("finds:" + finds.Count);
//							finds.Add (Tags [j]);
//							break;
//						}
					}
				}
				if (!found) {
					Console.WriteLine ("releasing mutex");
					mylock = false;
				}
			}
		}

		void ExtractNewThumbnail ()
		{
			Console.WriteLine ("extracting thumbnail");
			//UIView snapshotview = blend.SnapshotView (true);
			UIImage render = ImageWithView (blend);
			string res = SaveGalleryThumbnail (go.Name, render);
			go.thumbFileName = res;
			RaiseThumbchanged ();
		}

		void RaiseThumbchanged ()
		{
			var handler = this.ThumbChanged;
			Console.WriteLine ("thumb changed");
			if (handler != null) {
				handler (this, new ThumbChangedEventArgs ());
			}
		}

		public static string SaveGalleryThumbnail (string name, UIImage ourpic)
		{
			if (ourpic == null)
				return "";
			Console.WriteLine ("Save");
			float aspectRatio = ourpic.Size.Width / ourpic.Size.Height;
			Console.WriteLine ("ratio:" + aspectRatio);
			float sc = 200;
			SizeF newSize = new SizeF (sc, sc / aspectRatio);
			UIImage thumbPic = ourpic.Scale (newSize); //measurements taken from CustomCell, alternatly 33x33

			if (ourpic != null) {
				var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
				var gallerydirectory = Path.Combine (documentsDirectory, "gallery");

				if (!Directory.Exists (gallerydirectory)) {
					Directory.CreateDirectory (gallerydirectory);
				}

				var thumbpicname = name + "_thumb.png";
				string thumbpngfileName = System.IO.Path.Combine (gallerydirectory, thumbpicname);

				NSData img2Data = thumbPic.AsPNG();

				NSError err = null;
				if (img2Data.Save (thumbpngfileName, false, out err)) {
					Console.WriteLine ("saved as " + thumbpngfileName);
					string result = thumbpicname;
					return result;

				} else {
					Console.WriteLine ("NOT saved as " + thumbpngfileName + " because" + err.LocalizedDescription);
					return null;
				}
			}
			return "";
		}


//		static string RandomGeneratedName ()
//		{
//			var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
//			var random = new Random();
//			var result = new string(
//				Enumerable.Repeat(chars, 8)
//				.Select(s => s[random.Next(s.Length)])
//				.ToArray());
//			return result;
//		}



		UIImage ImageWithView (UIView view){
			UIGraphics.BeginImageContextWithOptions (view.Bounds.Size, view.Opaque, 0.0f);
			view.Layer.RenderInContext (UIGraphics.GetCurrentContext ());
			UIImage img = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return img;
		}

		UIBarButtonItem it;
//		UIPopoverController uipc;

		private void CreateEditBarButton ()
		{

			it = new UIBarButtonItem ();
			it.Title = "Tags";
			//IS really info

			it.Clicked += (object sender, EventArgs e) => {
				EditTags tagedit = new EditTags(go);
				if(UserInterfaceIdiomIsPhone){
					this.NavigationController.PushViewController(tagedit,true);
				}else{
					this.NavigationController.PresentViewController(tagedit,true,null);
//					uipc = new UIPopoverController (tagedit);
//					uipc.PresentFromBarButtonItem (this.it,UIPopoverArrowDirection.Up, true);
				}
			};
			NavigationItem.SetRightBarButtonItem (it, true);

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
			int Create = av.FirstOtherButtonIndex;
			av.Clicked += (object sender, UIButtonEventArgs e) => {
				if(e.ButtonIndex == Create){
					String tagText = av.GetTextField (0).Text;
					tag.TagString = tagText;
					var scale = scrollView.ZoomScale;
					const float heightmod = 0.70f;
					//float widthmod = 1f;

					RectangleF contentFrame = new RectangleF(scrollView.ContentOffset.X / scale, scrollView.ContentOffset.Y / scale, scrollView.Frame.Size.Width / scale, scrollView.Frame.Size.Height /scale * heightmod);
					//contentFrame.Y = contentFrame.Y + this.NavigationController.View.Bounds.Y;
					//contentFrame.X = contentFrame.X + this.NavigationController.View.Bounds.Bottom;
					contentFrame.Y = contentFrame.Y + 90;
					tag.StoreRectangleF(contentFrame);
					AppDelegate.dao.SaveTag(tag);
					Console.WriteLine("tagtext:"+tag.TagString);
					Console.WriteLine("spot:"+tag.FetchAsRectangleF());
					tgv.SetNeedsDisplay();
					ExtractNewThumbnail ();
				}
			};

			av.Show();
		}

		public static bool UserInterfaceIdiomIsPhone 
		{
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}


	}
}

