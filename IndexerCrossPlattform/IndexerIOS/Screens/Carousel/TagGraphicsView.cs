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

		UIColor color = UIColor.FromRGBA(0.886f, 0.886f, 0.000f, 1.000f);
		static UIColor color2 = UIColor.FromRGBA(0.114f, 1.000f, 1.000f, 1.000f);
		UIColor color3 = UIColor.FromRGBA(0.295f, 0.886f, 0.000f, 1.000f);

		CGColor shadow = color2.ColorWithAlpha(0.52f).CGColor;
		readonly SizeF shadowOffset = new SizeF(0.1f, 1.1f);
		readonly int shadowBlurRadius = 5;

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
			RenderTags2(tags);
		}

		private void RenderTags(IList<ImageTag> tags) {
			List<RectangleF> rects = new List<RectangleF> ();
			List<String> tagStrings = new List<string>();
			foreach(ImageTag tag in tags){
				Console.WriteLine("RenderTags():"+tag.ToString());
				TagUtility tu = new TagUtility (tag);
				rects.Add (tu.FetchAsRectangleF ());
				tagStrings.Add(tag.TagString);
			}
			PaintBoxes(rects);
			DrawTexts (tagStrings, rects);
		}

		private void RenderTags2(IList<ImageTag> tags){
			foreach (ImageTag tag in tags) {
				TagUtility tu = new TagUtility (tag);
				PaintTaggedBox (tu.FetchAsRectangleF(), tag.TagString);
			}
		}
			
		void DrawTexts (List<string> tagStrings, List<RectangleF> rects)
		{
			using (CGContext gctx = UIGraphics.GetCurrentContext ()) {
				gctx.SaveState ();
				gctx.ScaleCTM (1f, -1f);
				gctx.TranslateCTM (0, Frame.Height);
				gctx.SetFillColor (UIColor.Green.CGColor);
				for (int i = 0; i < tagStrings.Count; i++) {
					gctx.SaveState ();
					var attributedString = new NSAttributedString (tagStrings[i], 
						                      new CTStringAttributes {
							ForegroundColorFromContext = true,
							Font = new CTFont ("ArialMT", 48)
						});
					gctx.TextPosition = new PointF (rects[i].X, rects[i].Y);
					using (var textLine = new CTLine (attributedString)) {
						textLine.Draw (gctx);
					}
					gctx.RestoreState ();
				}
				gctx.RestoreState ();
			}
		}

		void PaintBoxes (List<RectangleF> rects)
		{
			Console.WriteLine ("PaintBoxes()");
			using (CGContext g = UIGraphics.GetCurrentContext ()) {
				g.SaveState ();
				g.MoveTo (0, 0);
				g.SetLineWidth (4);
				UIColor.Yellow.SetStroke ();
				g.SetLineDash (0, new float[] { 10, 4 });

				foreach (RectangleF rect in rects) {
					g.SaveState ();
					g.MoveTo (0, 0);
					UIBezierPath path = UIBezierPath.FromRoundedRect (rect, 80);
					g.AddPath (path.CGPath);
					g.DrawPath (CGPathDrawingMode.Stroke);
					g.RestoreState ();
				}
				g.RestoreState ();
			}
		}

		void PaintTaggedBox(RectangleF rect, String myText){
			var context = UIGraphics.GetCurrentContext();
			var middlePath = UIBezierPath.FromRoundedRect(rect, 22);
			context.SaveState();
			context.SetShadowWithColor(shadowOffset, shadowBlurRadius, shadow);
			color.SetStroke();
			middlePath.LineWidth = 3.5f;
			context.SaveState();
			var middlePattern = new float [] {6, 2, 6, 2};
			context.SetLineDash(0, middlePattern);
			middlePath.Stroke();
			context.RestoreState();
			context.RestoreState();
			context.SaveState();
			context.SetShadowWithColor(shadowOffset, shadowBlurRadius, shadow);
			color3.SetFill();
			new NSString(myText).DrawString(RectangleF.Inflate(rect, 0, -13), UIFont.FromName("Helvetica", 48), UILineBreakMode.WordWrap, UITextAlignment.Center);
			context.RestoreState();
		}
	}
}

