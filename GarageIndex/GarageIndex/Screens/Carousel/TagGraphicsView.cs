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
		public RectangleF Canvas;
		public SizeF CanvasSize;

		public TagGraphicsView (GalleryObject go, RectangleF canvas)
		{
			this.go = go;
			Frame = canvas;
			BackgroundColor = UIColor.Clear;
			Opaque = false;
		}
			
		public override void Draw (RectangleF rect)
		{
			base.Draw (rect);
//			
			Console.WriteLine ("draw():"+rect);
			IList<ImageTag> tags = AppDelegate.dao.GetTagsByGalleryObjectID (go.ID);
			RenderTags (tags);
		}

		private void RenderTags(IList<ImageTag> tags) {
			foreach(ImageTag tag in tags){
				RenderTag (tag);
			}
		}

		void DrawCoreText (RectangleF myframe, string tagString)
		{
			using (CGContext gctx = UIGraphics.GetCurrentContext ()) {
				gctx.TranslateCTM (0, myframe.Height);
				gctx.ScaleCTM (1f, -1f);
				//gctx.RotateCTM ((float)Math.PI * 120 / 180);
				gctx.SetFillColor (UIColor.Green.CGColor);
				var attributedString = new NSAttributedString (tagString, 
					                      new MonoTouch.CoreText.CTStringAttributes {
						ForegroundColorFromContext = true,
						Font = new MonoTouch.CoreText.CTFont ("ArialMT", 48)
					});
				gctx.TextPosition = new PointF (myframe.X, myframe.Y);
				using (var textLine = new CTLine (attributedString)) {
					textLine.Draw (gctx);
				}
			}
		}

		void RenderTag (ImageTag tag)
		{
			Console.WriteLine ("Rendertag()");
			Console.WriteLine (tag.TagString);
			RectangleF tagFrame = tag.FetchAsRectangleF ();
			Console.WriteLine (tagFrame);
//			var savedContext = UIGraphics.GetCurrentContext;

			if (tagFrame != null) {
				using (CGContext g = UIGraphics.GetCurrentContext ()) {
					g.MoveTo (0, 0);
					g.SetLineWidth (4);
					UIColor.Yellow.SetStroke ();
					g.SetLineDash (0, new float[] { 10, 4 });

					UIBezierPath path = UIBezierPath.FromRoundedRect (tagFrame, 30);

					g.AddPath (path.CGPath);

					g.DrawPath (CGPathDrawingMode.Stroke);
				}
				DrawCoreText (tagFrame, tag.TagString);
			}
		}
	}
}

