using System;
using MonoTouch.UIKit;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;

namespace IndexerIOS
{
	public class WordCloudView : UIView
	{
		List<WordCloudItem> words;
		public WordCloudView (RectangleF myframe, List<WordCloudItem> words)
		{
			Frame = myframe;
			this.words = words;
		}

		public WordCloudView (RectangleF myframe)
		{
			Frame = myframe;

		}

		protected override void Dispose (bool disposing)
		{
			this.words = null;
			base.Dispose (disposing);
		}

		void PaintWords ()
		{
			foreach (WordCloudItem word in words) {
				DrawWord (word);
			}
		}

		public override void Draw (RectangleF rect)
		{
			base.Draw (rect);
			this.BackgroundColor = UIColor.Clear;
			PaintWords ();

		}

		public UIImage RerenderAsUIImage ()
		{
			UIGraphics.BeginImageContext (Bounds.Size);
			PaintWords ();
			var converted = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return converted;
		}

		void DrawWord (WordCloudItem word)
		{
			Console.WriteLine ("drawing word:"+word);
			RectangleF myRect = IndexerUtils.GetRectangleF (word);
			UIFont myFont = UIFont.FromName("Helvetica-BoldOblique", IndexerUtils.GetWordWeight(word));
			string myText = word.word;

			//BEGIN PAINTCODE;
			// Color Declarations
			UIColor color = UIColor.FromRGBA(0.000f, 0.429f, 0.143f, 1.000f);
			UIColor color3 = UIColor.FromRGBA(0.819f, 0.069f, 0.069f, 1.000f);
			UIColor color2 = UIColor.FromRGBA(0.035f, 0.018f, 0.343f, 1.000f);
			UIColor color4 = UIColor.FromRGBA(0.925f, 0.040f, 0.704f, 1.000f);

			Random rnd = new Random ();
			int index = rnd.Next (1,4);

			//NON PAINTCODE SECTION

			if (index == 1) {
				color.SetFill();
			}

			if (index == 2) {
				color2.SetFill();
			}

			if (index == 3) {
				color3.SetFill();
			}

			if (index == 4) {
				color4.SetFill();
			}


			//END NON PAINTCODE SECTION


			// Abstracted Attributes
//			var hello1Content = "Hello, World!";
//			var hello2Content = "Hello, World!";


			// Hello1 Drawing
			//var hello1Rect = new RectangleF(34, 31, 110, 16);

			new NSString(myText).DrawString(myRect, myFont, UILineBreakMode.WordWrap, UITextAlignment.Center);


//			//// Hello2 Drawing
//			var hello2Rect = new RectangleF(44, 72, 76, 26);
//			color.SetFill();
//			new NSString(hello2Content).DrawString(hello2Rect, UIFont.FromName("Helvetica-BoldOblique", 12), UILineBreakMode.WordWrap, UITextAlignment.Center);


			// Rectangle Drawing
//			var rectangleRect = new RectangleF(143.5f, 15.5f, 42, 79);
//			var rectanglePath = UIBezierPath.FromRoundedRect(rectangleRect, 20);
//			UIColor.White.SetFill();
//			rectanglePath.Fill();
//			UIColor.Black.SetStroke();
//			rectanglePath.LineWidth = 1;
//			rectanglePath.Stroke();
//			color.SetFill();
//			new NSString("Kake").DrawString(RectangleF.Inflate(rectangleRect, -6, -7), UIFont.FromName("Helvetica", 12), UILineBreakMode.WordWrap, UITextAlignment.Center);




		}
	}
}

