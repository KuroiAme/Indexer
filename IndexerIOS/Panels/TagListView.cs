﻿using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.CoreGraphics;
using System.Drawing;

namespace no.dctapps.commons.events
{
	public class TagListView : UIView
	{
		string[] taglist;
		List<RectangleF> tagframes;
		RectangleF myframe;
		public event EventHandler<TagStringClickedEventArgs> TagStringClicked;
		public event EventHandler MaxLinesReached;
		List<RectangleF> hitTable;

		protected override void Dispose (bool disposing)
		{
			tagframes = null;
			taglist = null;
			TagStringClicked = null;
			MaxLinesReached = null;
			hitTable = null;
			base.Dispose (disposing);
		}



		public TagListView (RectangleF frame, string[] taglist)
		{
			Frame = frame;
			Console.WriteLine ("Frame=" + Frame);
			myframe = frame;
			this.BackgroundColor = UIColor.Clear;
			if (taglist != null) {
				this.taglist = taglist;
			} else {
				taglist = new string[] { };
			}
			tagframes = new List<RectangleF> ();
			hitTable = new List<RectangleF> ();
		}

		public override void Draw (RectangleF rect)
		{
			Console.WriteLine ("Taglistview:draw()");
			this.charNumber = 0;
			this.lineNumber = 0;
			base.Draw (rect);
			DrawContainer3 ();
			if (taglist != null) {
				for (int i = 0; i < taglist.Length; i++) {
					DrawShadedTag2 (taglist [i]);
				}
			}
		}

		public void UpdateTagList (string[] taglist)
		{
			Console.WriteLine ("updateTagList()");
			this.taglist = taglist;
			this.charNumber = 0;
			this.lineNumber = 0;
			tagframes = new List<RectangleF> ();
			hitTable = new List<RectangleF> ();
			this.SetNeedsDisplay ();
		}

		void RaiseEditTagString (string str, int pos)
		{
			var handler = this.TagStringClicked;
			Console.WriteLine("item:"+str+"pos:"+pos);
			if (handler != null && str != null) {
				handler(this, new TagStringClickedEventArgs(str,pos));
			}
		}

		public override UIView HitTest (PointF point, UIEvent uievent)
		{
			for(int i = 0; i < tagframes.Count; i++){
				if (tagframes [i].Contains (point)) {
					try{
						if(hitTable.Contains(tagframes[i])){
							hitTable.Remove(tagframes[i]);
							RaiseEditTagString(taglist[i],i);
						}else{
							hitTable.Add(tagframes[i]);
						}
					}catch(Exception e){
						Console.WriteLine ("coudnt raise edit tag for:" + i + "RM("+taglist.Length+", error:" + e.ToString ());
					}
					break; //TODO this is a hack, could select more than one?
				}
			}
			Console.WriteLine ("does it come to this?");
			return base.HitTest (point, uievent);
		}

//		static  UIColor color2 = UIColor.FromRGBA (0.833f, 0.833f, 0.833f, 1.000f);
//		static  CGColor shadow = UIColor.Black.CGColor;
//		static  SizeF shadowOffset = new SizeF (1.1f, 1.1f);
//		static  float shadowBlurRadius = 5;

//		void DrawContainer ()
//		{
//			//240x120
//			CGContext context = UIGraphics.GetCurrentContext ();
//			// Rectangle Drawing
//
////			RectangleF dynamicFrameRect = new RectangleF (myframe.X + 20.5f,myframe.Y + 16.5f,myframe.Width - 42, myframe.Height - 26);
//			RectangleF dynamicFrameRect = new RectangleF (myframe.X + 20.5f,myframe.Y + 16.5f,myframe.Width - 42, myframe.Height);
//			Console.WriteLine ("DynamicRect:" + dynamicFrameRect);
//
//			var rectanglePath = UIBezierPath.FromRoundedRect (dynamicFrameRect, 30);
//			context.SaveState ();
//			context.SetShadowWithColor (shadowOffset, shadowBlurRadius, shadow);
//			UIColor.White.SetFill ();
//			rectanglePath.Fill ();
//			// Rectangle Inner Shadow
//			var rectangleBorderRect = rectanglePath.Bounds;
//			rectangleBorderRect.Inflate (shadowBlurRadius, shadowBlurRadius);
//			rectangleBorderRect.Offset (-shadowOffset.Width, -shadowOffset.Height);
//			rectangleBorderRect = RectangleF.Union (rectangleBorderRect, rectanglePath.Bounds);
//			rectangleBorderRect.Inflate (1, 1);
//			var rectangleNegativePath = UIBezierPath.FromRect (rectangleBorderRect);
//			rectangleNegativePath.AppendPath (rectanglePath);
//			rectangleNegativePath.UsesEvenOddFillRule = true;
//			context.SaveState ();
//			{
//				var xOffset = shadowOffset.Width + (float)Math.Round (rectangleBorderRect.Width);
//				var yOffset = shadowOffset.Height;
//				context.SetShadowWithColor (new SizeF (xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)), shadowBlurRadius, shadow);
//				rectanglePath.AddClip ();
//				var transform = CGAffineTransform.MakeTranslation (-(float)Math.Round (rectangleBorderRect.Width), 0);
//				rectangleNegativePath.ApplyTransform (transform);
//				UIColor.Gray.SetFill ();
//				rectangleNegativePath.Fill ();
//			}
//			context.RestoreState ();
//			context.RestoreState ();
//			UIColor.Black.SetStroke ();
//			rectanglePath.LineWidth = 1;
//			rectanglePath.Stroke ();
////			return context;
//		}

//		private void DrawContainer2(){
//			CGContext context = UIGraphics.GetCurrentContext ();
//			// Rectangle Drawing from 240x120
////			var rectanglePath = UIBezierPath.FromRoundedRect(myframe,30);
//			var rectanglePath = UIBezierPath.FromRoundedRect(new RectangleF(20.5f, 16.5f, 202, 87), 30);
////			RectangleF dynamicFrameRect = new RectangleF (myframe.X + 20.5f,myframe.Y + 16.5f,myframe.Width - 42, myframe.Height - 26);
////			RectangleF dynamicFrameRect = new RectangleF ();
////			Console.WriteLine ("DynamicRect:" + dynamicFrameRect);
//			context.SaveState();
//			context.SetShadowWithColor(shadowOffset, shadowBlurRadius, shadow);
//			UIColor.White.SetFill();
//			rectanglePath.Fill();
//
//////// Rectangle Inner Shadow
//			var rectangleBorderRect = rectanglePath.Bounds;
//			rectangleBorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
//			rectangleBorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
//			rectangleBorderRect = RectangleF.Union(rectangleBorderRect, rectanglePath.Bounds);
//			rectangleBorderRect.Inflate(1, 1);
//
//			var rectangleNegativePath = UIBezierPath.FromRect(rectangleBorderRect);
//			rectangleNegativePath.AppendPath(rectanglePath);
//			rectangleNegativePath.UsesEvenOddFillRule = true;
//
//			context.SaveState();
//			{
//				var xOffset = shadowOffset.Width + (float)Math.Round(rectangleBorderRect.Width);
//				var yOffset = shadowOffset.Height;
//				context.SetShadowWithColor(
//					new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
//					shadowBlurRadius,
//					shadow);
//
//				rectanglePath.AddClip();
//				var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(rectangleBorderRect.Width), 0);
//				rectangleNegativePath.ApplyTransform(transform);
//				UIColor.Gray.SetFill();
//				rectangleNegativePath.Fill();
//			}
//			context.RestoreState();
//
//			context.RestoreState();
//
//			UIColor.Black.SetStroke();
//			rectanglePath.LineWidth = 1;
//			rectanglePath.Stroke();
//
//		}


		private void DrawContainer3(){
			float margin = 0f;
			float width = Bounds.Width - margin*2;
			float height = Bounds.Height - margin*2;
			float curvature = 10;

			//BEGIN PAINTCODE

			// General Declarations
			var context = UIGraphics.GetCurrentContext();

			// Color Declarations
			UIColor color3 = UIColor.FromRGBA(0.667f, 0.667f, 0.667f, 0.449f);
			UIColor color4 = UIColor.FromRGBA(0.667f, 0.667f, 0.667f, 1.000f);
			UIColor color5 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
			UIColor color6 = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 1.000f);

			// Shadow Declarations
			var shadow = color3.CGColor;
			var shadowOffset = new SizeF(1.1f, 1.1f);
			var shadowBlurRadius = 2.5f;

			//// Rectangle Drawing
			var rectanglePath = UIBezierPath.FromRoundedRect(new RectangleF(margin, margin, width, height), curvature);
			context.SaveState();
			context.SetShadowWithColor(shadowOffset, shadowBlurRadius, shadow);
			UIColor.White.SetFill();
			rectanglePath.Fill();

			////// Rectangle Inner Shadow
			var rectangleBorderRect = rectanglePath.Bounds;
			rectangleBorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
			rectangleBorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
			rectangleBorderRect = RectangleF.Union(rectangleBorderRect, rectanglePath.Bounds);
			rectangleBorderRect.Inflate(1, 1);

			var rectangleNegativePath = UIBezierPath.FromRect(rectangleBorderRect);
			rectangleNegativePath.AppendPath(rectanglePath);
			rectangleNegativePath.UsesEvenOddFillRule = true;

			context.SaveState();
			{
				var xOffset = shadowOffset.Width + (float)Math.Round(rectangleBorderRect.Width);
				var yOffset = shadowOffset.Height;
				context.SetShadowWithColor(
					new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
					shadowBlurRadius,
					shadow);

				rectanglePath.AddClip();
				var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(rectangleBorderRect.Width), 0);
				rectangleNegativePath.ApplyTransform(transform);
				UIColor.Gray.SetFill();
				rectangleNegativePath.Fill();
			}
			context.RestoreState();

			context.RestoreState();

			UIColor.LightGray.SetStroke();
			rectanglePath.LineWidth = 1;
			rectanglePath.Stroke();
		}

		float nullconst = 10f;
		float ynullconst = 10.0f;
		//float charSize = 7.55f;
		float lineSize = 20f;
		int lineNumber = 0;
		int charNumber = 0;

		//float height = 16;

		void RaiseMaxSizeReached ()
		{
			var handler = this.MaxLinesReached;
			if (handler != null) {
				handler (this, new EventArgs ());
			}
		}

		float padding = 5;

		private void DrawShadedTag2(string paintstring){
			Console.WriteLine("DrawShadedTag():"+paintstring);

			UILabel testX = new UILabel ();
			testX.Font = UIFont.FromName ("Helvetica-BoldOblique", 12);
			testX.LineBreakMode = UILineBreakMode.WordWrap;
			testX.TextAlignment = UITextAlignment.Center;
			testX.Lines = 0;
			testX.Text = "X";
			SizeF sizeX = testX.StringSize (testX.Text, testX.Font, new SizeF (Bounds.Width, Bounds.Height));

			CGContext context = UIGraphics.GetCurrentContext ();
			int maxAntallCharacters = (int) Math.Round((Bounds.Width) / sizeX.Width);
			if (paintstring.Length > maxAntallCharacters) {
				int pos = (int) Math.Floor(paintstring.Length / 2.00);
				string a = paintstring.Substring (0, pos);
				string b = paintstring.Substring (pos, paintstring.Length);
				DrawShadedTag2 (a);
				DrawShadedTag2 (b);
			}

			if (paintstring.Length + charNumber + 3 > maxAntallCharacters) {
				//Ny linje
				lineNumber++;
				charNumber = 0;
			}

			if (lineNumber != 6) {



				float x = nullconst + (charNumber * sizeX.Width + padding);
				float y = ynullconst + (lineNumber * lineSize);
//				float width = charSize * paintstring.Length + 3;

				UIColor color3 = UIColor.FromRGBA (0.667f, 0.667f, 0.667f, 0.449f);
				UIColor color6 = UIColor.FromRGBA (1.000f, 1.000f, 1.000f, 1.000f);
				UIColor color5 = UIColor.FromRGBA (0.000f, 0.000f, 0.000f, 1.000f);

				var shadow = color3.CGColor;
				var shadowOffset = new SizeF (1.1f, 1.1f);
				const float shadowBlurRadius = 2.5f;

				UILabel test = new UILabel ();
				test.Font = UIFont.FromName ("Helvetica-BoldOblique", 12);
				test.LineBreakMode = UILineBreakMode.WordWrap;
				test.TextAlignment = UITextAlignment.Center;
				test.Lines = 0;
				test.Text = paintstring;
				SizeF size = test.StringSize (test.Text, test.Font, new SizeF (Bounds.Width, Bounds.Height));

				// nine Drawing
				var nineRect = new RectangleF (x, y, size.Width + padding, size.Height + padding);
				tagframes.Add (nineRect);
				var ninePath = UIBezierPath.FromRoundedRect (nineRect, 6.5f);
				context.SaveState ();
				context.SetShadowWithColor (shadowOffset, shadowBlurRadius, shadow);
				color6.SetFill ();
				ninePath.Fill ();

				////// nine Inner Shadow
				var nineBorderRect = ninePath.Bounds;
				nineBorderRect.Inflate (shadowBlurRadius, shadowBlurRadius);
				nineBorderRect.Offset (-shadowOffset.Width, -shadowOffset.Height);
				nineBorderRect = RectangleF.Union (nineBorderRect, ninePath.Bounds);
				nineBorderRect.Inflate (1, 1);

				var nineNegativePath = UIBezierPath.FromRect (nineBorderRect);
				nineNegativePath.AppendPath (ninePath);
				nineNegativePath.UsesEvenOddFillRule = true;

				context.SaveState ();
				{
					var xOffset = shadowOffset.Width + (float)Math.Round (nineBorderRect.Width);
					var yOffset = shadowOffset.Height;
					context.SetShadowWithColor (
						new SizeF (xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						shadowBlurRadius,
						shadow);

					ninePath.AddClip ();
					var transform = CGAffineTransform.MakeTranslation (-(float)Math.Round (nineBorderRect.Width), 0);
					nineNegativePath.ApplyTransform (transform);
					UIColor.Gray.SetFill ();
					nineNegativePath.Fill ();
				}
				context.RestoreState ();

				context.RestoreState ();

				UIColor.LightGray.SetStroke ();
				ninePath.LineWidth = 1;
				ninePath.Stroke ();
				color5.SetFill ();
				new NSString (paintstring).DrawString (nineRect, UIFont.FromName ("Helvetica-BoldOblique", 12), UILineBreakMode.WordWrap, UITextAlignment.Center);

				charNumber += paintstring.Length + 1; // +1 is for space 
			} else {
				RaiseMaxSizeReached ();
			}
		}



//		private void UnRefactoredPaintCodeCode(){
//			CGContext context = UIGraphics.GetCurrentContext ();
//		
//			UIColor color2;
//			CGColor shadow;
//			SizeF shadowOffset;
////			float shadowBlurRadius;
////			var context = DrawContainer (out color2, out shadow, out shadowOffset, out shadowBlurRadius);
//
//
//
//
//		//// Four Drawing
//		var fourRect = new RectangleF(myframe.X + 119.5f,myframe.Y + 29.5f, 36, 13);
//		var fourPath = UIBezierPath.FromRoundedRect(fourRect, 6.5f);
//		context.SaveState();
//		context.SetShadowWithColor(shadowOffset, shadowBlurRadius, shadow);
//		color2.SetFill();
//		fourPath.Fill();
//
//		////// Four Inner Shadow
//		var fourBorderRect = fourPath.Bounds;
//		fourBorderRect.Inflate(shadowBlurRadius, shadowBlurRadius);
//		fourBorderRect.Offset(-shadowOffset.Width, -shadowOffset.Height);
//		fourBorderRect = RectangleF.Union(fourBorderRect, fourPath.Bounds);
//		fourBorderRect.Inflate(1, 1);
//
//		var fourNegativePath = UIBezierPath.FromRect(fourBorderRect);
//		fourNegativePath.AppendPath(fourPath);
//		fourNegativePath.UsesEvenOddFillRule = true;
//
//		context.SaveState();
//		{
//			var xOffset = shadowOffset.Width + (float)Math.Round(fourBorderRect.Width);
//			var yOffset = shadowOffset.Height;
//			context.SetShadowWithColor(
//				new SizeF(xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
//				shadowBlurRadius,
//				shadow);
//
//			fourPath.AddClip();
//			var transform = CGAffineTransform.MakeTranslation(-(float)Math.Round(fourBorderRect.Width), 0);
//			fourNegativePath.ApplyTransform(transform);
//			UIColor.Gray.SetFill();
//			fourNegativePath.Fill();
//		}
//		context.RestoreState();
//
//		context.RestoreState();
//
//		UIColor.Black.SetStroke();
//		fourPath.LineWidth = 1;
//		fourPath.Stroke();
//		UIColor.Black.SetFill();
//		new NSString("Four").DrawString(fourRect, UIFont.FromName("Helvetica", 12), UILineBreakMode.WordWrap, UITextAlignment.Center);
//
//		}
			


	}
}

