using System;
using MonoTouch.UIKit;
using System.Drawing;
using System.Collections.Generic;
using no.dctapps.Garageindex.events;
using MonoTouch.Foundation;
using System.IO;
using System.Linq;
using MonoTouch.CoreGraphics;
using MonoTouch.ObjCRuntime;
using GoogleAnalytics.iOS;
using SlideDownMenu;

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
		GalleryViewController gvc;

		public EditImageModeController (GalleryObject go, GalleryViewController galleryViewController)
		{
			this.go = go;
			//finds = new List<ImageTag> ();
			mylock = false;
			found = false;
			this.gvc = galleryViewController;
		}

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
			AddLongPress ();
		}

		void AddLongPress ()
		{
			var longPressGesture = new UILongPressGestureRecognizer (EditTagFrame);
			scrollView.AddGestureRecognizer (longPressGesture);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			if (gvc != null) {
				this.ThumbChanged += (object sender, ThumbChangedEventArgs e) => gvc.ChangeThumb ();
			}

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
			Add(scrollView);

			scrollView.ViewForZoomingInScrollView += (UIScrollView sv) => blend;

			var doubletap = new UITapGestureRecognizer (AddTag);
			doubletap.NumberOfTapsRequired = 2;
			scrollView.AddGestureRecognizer (doubletap);

			AddLongPress ();

			CreateEditBarButton ();

			ExtractNewThumbnail();

			CreateSlideDownMenu ();

			UIButton backbutton = new UIButton(new RectangleF(10,25,48,32));
			backbutton.SetImage (backarrow.MakeBackArrow(), UIControlState.Normal);
			backbutton.TouchUpInside += (object sender, EventArgs e) => DismissViewControllerAsync (true);
			Add (backbutton);

		}

		void CreateSlideDownMenu ()
		{
			var item0 = new MenuItem ("Options", UIImage.FromBundle ("frames4832.png"), (menuItem) => {
				Console.WriteLine("Item: {0}", menuItem);
			});
			item0.Tag = 0;
			var extract = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Add tag at zoomed object", "Add tag at zoomed object");
			var item1 = new MenuItem (extract, UIImage.FromBundle ("startree.png"), (menuItem) => {
				Console.WriteLine("Item: {0}", menuItem);
				AddTagInner();

			});
			item1.Tag = 1;
			var item2 = new MenuItem ("List of tags", UIImage.FromBundle ("frames4832.png"), (menuItem) => {
				Console.WriteLine("Item: {0}", menuItem);
				EditTags tags = new EditTags(this.go);
				PresentViewControllerAsync(tags,true);
			});
			item2.Tag = 2;


			//item0.tag = 0;

			var slideMenu = new SlideMenu (new List<MenuItem> { item0, item1, item2});
			slideMenu.Center = new PointF (slideMenu.Center.X, slideMenu.Center.Y + 25);
			this.View.AddSubview (slideMenu);
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
					TagUtility tu = new TagUtility (Tags [i]);
					Rects.Add (tu.FetchAsRectangleF ());
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
						this.PresentViewControllerAsync (tds, true);
						//this.NavigationController.PushViewController (tds, false);
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

		async void ExtractNewThumbnail ()
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

		UIImage ImageWithView (UIView view){
			UIGraphics.BeginImageContextWithOptions (view.Bounds.Size, view.Opaque, 0.0f);
			view.Layer.RenderInContext (UIGraphics.GetCurrentContext ());
			UIImage img = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return img;
		}

//		public UIImage ImageWithViewAsync (UIView view){
//			UIGraphics.BeginImageContextWithOptions (view.Bounds.Size, view.Opaque, 0.0f);
//			view.Layer.RenderInContext (UIGraphics.GetCurrentContext ());
//			UIImage img = UIGraphics.GetImageFromCurrentImageContext ();
//			UIGraphics.EndImageContext ();
//			return img;
//		}

		UIBarButtonItem it;
//		UIPopoverController uipc;

		private void CreateEditBarButton ()
		{

			it = new UIBarButtonItem ();
			var tagstext = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Tags", "Tags");
			it.Title = tagstext;
			//IS really info

			it.Clicked += (object sender, EventArgs e) => {
				EditTags tagedit = new EditTags(go);
				this.NavigationController.PushViewController(tagedit,true);
				
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

		void AddTagInner ()
		{
			ImageTag tag = new ImageTag ();
			tag.GalleryObjectID = go.ID;
			var cr8 = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Create", "Create");
			var input = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("input tags, comma seperated", "input tags, comma seperated");
			var abort = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Cancel", "Cancel");
			UIAlertView av = new UIAlertView (input, "\n", null, abort, new string[] {
				cr8
			});
			av.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
			int Create = av.FirstOtherButtonIndex;
			av.Clicked += (object sender, UIButtonEventArgs e) =>  {
				if (e.ButtonIndex == Create) {
					String tagText = av.GetTextField (0).Text;
					tag.TagString = tagText;
					var scale = scrollView.ZoomScale;
					const float heightmod = 0.70f;
					//float widthmod = 1f;
					RectangleF contentFrame = new RectangleF (scrollView.ContentOffset.X / scale, scrollView.ContentOffset.Y / scale, scrollView.Frame.Size.Width / scale, scrollView.Frame.Size.Height / scale * heightmod);
					//contentFrame.Y = contentFrame.Y + this.NavigationController.View.Bounds.Y;
					//contentFrame.X = contentFrame.X + this.NavigationController.View.Bounds.Bottom;
					contentFrame.Y = contentFrame.Y + 90;
					TagUtility tu = new TagUtility(tag);
					tu.StoreRectangleF (contentFrame);
					AppDelegate.dao.SaveTag (tag);
//					Console.WriteLine ("tagtext:" + tag.TagString);
//					Console.WriteLine ("spot:" + tag.FetchAsRectangleF ());
					tgv.SetNeedsDisplay ();

				}
			};
			av.Show ();
		}
			
		void AddTag (UITapGestureRecognizer gestureRecognizer){
			Console.WriteLine ("ADDTAG");
			AddTagInner ();
			ExtractNewThumbnail ();

		}

		public static bool UserInterfaceIdiomIsPhone 
		{
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}


	}
}

