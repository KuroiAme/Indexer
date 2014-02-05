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
			List<RectangleF> rects = new List<RectangleF> ();
			List<String> tagStrings = new List<string>();
			foreach(ImageTag tag in tags){
				Console.WriteLine("RenderTags():"+tag.ToString());
				rects.Add (tag.FetchAsRectangleF ());
				tagStrings.Add(tag.TagString);
			}
			PaintBoxes(rects);
			DrawTexts (tagStrings, rects);
		}
			
		void DrawTexts (List<string> tagStrings, List<RectangleF> rects)
		{
			using (CGContext gctx = UIGraphics.GetCurrentContext ()) {
				//gctx.TranslateCTM (0, Bounds.Height);
				//gctx.ScaleCTM (1f, -1f);
				gctx.SetFillColor (UIColor.Green.CGColor);
				for (int i = 0; i < tagStrings.Count; i++) {
					var attributedString = new NSAttributedString (tagStrings[i], 
						                      new MonoTouch.CoreText.CTStringAttributes {
							ForegroundColorFromContext = true,
							Font = new MonoTouch.CoreText.CTFont ("ArialMT", 48)
						});
					gctx.TextPosition = new PointF (rects[i].X, rects[i].Y);
					using (var textLine = new CTLine (attributedString)) {
						textLine.Draw (gctx);
					}
				}
			}
		}

		void PaintBoxes (List<RectangleF> rects)
		{
			Console.WriteLine ("PaintBoxes()");
			using (CGContext g = UIGraphics.GetCurrentContext ()) {
				g.SetLineWidth (4);
				UIColor.Yellow.SetStroke ();
				g.SetLineDash (0, new float[] { 10, 4 });

				foreach (RectangleF rect in rects) {
					UIBezierPath path = UIBezierPath.FromRoundedRect (rect, 80);
					g.AddPath (path.CGPath);
					g.DrawPath (CGPathDrawingMode.Stroke);
				}
			}

		}
	}
}

