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
using MonoTouch.CoreText;

namespace GarageIndex
{
	public class TagGraphicsView : UIView
	{
		GalleryObject go;


		public TagGraphicsView (GalleryObject go)
		{
			this.go = go;	
			this.BackgroundColor = UIColor.White;
		}

		public override void Draw (RectangleF rect)
		{
			Console.WriteLine ("draw()");
			base.Draw (rect);
			DrawImage (go.imageFileName, rect);
			IList<ImageTag> tags = AppDelegate.dao.GetTagsByGalleryObjectID (go.ID);
			RenderTags (tags);


		}

		private void RenderTags(IList<ImageTag> tags) {
			foreach(ImageTag tag in tags){
				RenderTag (tag);
			}
		}

		void DrawImage (string imageFileName, RectangleF rec)
		{
			using (CGContext g = UIGraphics.GetCurrentContext ()) {
				g.ScaleCTM (1, -1);
				g.TranslateCTM (0, -Bounds.Height);

				var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
				var gallerydirectory = Path.Combine (documentsDirectory, "gallery");
				string imagefilename = go.imageFileName;
				string path = Path.Combine (gallerydirectory, imagefilename);

				g.DrawImage (rec, UIImage.FromFile (path).CGImage);
			}
		}

		void DrawCoreText (RectangleF myframe, string tagString)
		{
			var gctx = UIGraphics.GetCurrentContext ();
			gctx.TranslateCTM (0.5f * Frame.Width, 0.5f * Frame.Height);
			gctx.ScaleCTM (1, -1);
			gctx.RotateCTM ((float)Math.PI * 120 / 180);
			gctx.SetFillColor (UIColor.Green.CGColor);
			var attributedString = new NSAttributedString (tagString, 
				                       new MonoTouch.CoreText.CTStringAttributes {
					ForegroundColorFromContext = true,
					Font = new MonoTouch.CoreText.CTFont ("Arial", 48)
			});
			using (var textLine = new CTLine (attributedString)) {
				textLine.Draw (gctx);
			}
		}

		void RenderTag (ImageTag tag)
		{
			Console.WriteLine ("Rendertag()");
			Console.WriteLine (tag.TagString);
			RectangleF myframe = tag.FetchAsRectangleF ();
			Console.WriteLine (myframe);

			using (CGContext g = UIGraphics.GetCurrentContext ()) {
				g.RotateCTM (0);
				g.SetLineWidth(4);
				UIColor.Yellow.SetStroke ();
				var path = new CGPath ();
				g.SetLineDash(0, new float[] {10,4});
				path.AddRect (myframe);
				//UIBezierPath Bpath = new UIBezierPath ().BezierPathWithIOS7RoundedRect (myframe, 30);
				g.AddPath (path);
				g.DrawPath (CGPathDrawingMode.Stroke);

//				float fontSize = 35f;
//				g.TranslateCTM (0, fontSize);
//				g.SetLineWidth (1.0f);
//				g.SetStrokeColor (UIColor.Yellow.CGColor);
//				g.SetFillColor (UIColor.Red.CGColor);
//				g.SetShadowWithColor (new SizeF (5, 5), 0, UIColor.Blue.CGColor);
//
//				g.SetTextDrawingMode (CGTextDrawingMode.FillStroke);
//				g.SelectFont ("Helvetica", fontSize, CGTextEncoding.MacRoman);

				DrawCoreText (myframe, tag.TagString);



			}





		}
	}
}

