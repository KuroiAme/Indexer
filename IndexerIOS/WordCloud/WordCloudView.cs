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

		public override void Draw (RectangleF rect)
		{
			base.Draw (rect);
			this.BackgroundColor = UIColor.Clear;
			foreach (WordCloudItem word in words) {
				DrawWord (word);
			}

		}

		void DrawWord (WordCloudItem word)
		{
			Console.WriteLine ("drawing word:"+word);
			RectangleF myRect = IndexerUtils.GetRectangleF (word);
			UIFont myFont = UIFont.FromName("Helvetica-BoldOblique", word.weight + 10);
			string myText = word.word;

			//BEGIN PAINTCODE;
			//// Color Declarations
			UIColor color = UIColor.FromRGBA(0.000f, 0.429f, 0.143f, 1.000f);

			//// Abstracted Attributes
//			var hello1Content = "Hello, World!";
//			var hello2Content = "Hello, World!";


			//// Hello1 Drawing
			//var hello1Rect = new RectangleF(34, 31, 110, 16);
			color.SetFill();
			new NSString(myText).DrawString(myRect, myFont, UILineBreakMode.WordWrap, UITextAlignment.Center);


//			//// Hello2 Drawing
//			var hello2Rect = new RectangleF(44, 72, 76, 26);
//			color.SetFill();
//			new NSString(hello2Content).DrawString(hello2Rect, UIFont.FromName("Helvetica-BoldOblique", 12), UILineBreakMode.WordWrap, UITextAlignment.Center);


			//// Rectangle Drawing
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

