using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace IndexerIOS
{
	public static class IndexerUtils
	{
		const int MAX_NUMBER_OF_WORDS = 250;

		public static List<WordCloudItem> CalculateBoxes (List<WordCloudItem> words, RectangleF outer)
		{

			List<WordCloudItem> sizedwords = CalculateSizes (words, outer);
			sizedwords.Sort ();
			//fetch the top 50 most significant words
			List<WordCloudItem> limitedWords = sizedwords.Take (MAX_NUMBER_OF_WORDS).ToList();

			List<WordCloudItem> placedWords = PositionWords (limitedWords, outer);
			return placedWords;
		}

		static List<WordCloudItem> CalculateSizes (List<WordCloudItem> words, RectangleF outer)
		{
			List<WordCloudItem> calculatedWords = new List<WordCloudItem>();
			foreach (WordCloudItem word in words) {
				if (!string.IsNullOrEmpty(word.word)) {
					UILabel test = new UILabel ();
					test.Font = UIFont.FromName ("Helvetica-BoldOblique", word.weight + 10);
					test.LineBreakMode = UILineBreakMode.WordWrap;
					test.TextAlignment = UITextAlignment.Center;
					test.Lines = 0;
					test.Text = word.word;
					SizeF size = test.StringSize (test.Text, test.Font, new SizeF (outer.Width, outer.Height));
					word.width = size.Width;
					word.height = size.Height;
					calculatedWords.Add (word);
				}
			}
			return calculatedWords;
		}

		static List<WordCloudItem> PositionWords (List<WordCloudItem> words, RectangleF outer)
		{
			List<WordCloudItem> positionedWords = new List<WordCloudItem> ();
			PointF center = findcenter (outer);
			foreach (WordCloudItem word in words) {
				float x = center.X;
				float y = center.Y;
				float width = word.width;
				float height = word.height;
				RectangleF currentRect = new RectangleF (x, y, width, height);

				do {
					currentRect = FindNewPosition (currentRect, outer);
				}
				while (checkForCollisions (positionedWords, currentRect));

				word.x = currentRect.X;
				word.y = currentRect.Y;
				positionedWords.Add (word);

			}
			return positionedWords;

		}

		static RectangleF FindNewPosition (RectangleF currentRect, RectangleF outer)
		{
			Random rnd = new Random ();
			PointF center = findcenter (outer);
			int direction = rnd.Next (0,3);
			float deltaX = rnd.Next(1,(int) outer.Width / 2);
			float deltaY = rnd.Next (1, (int) outer.Height / 2);

			if (direction == 0) { // NEGATIVE NEGATIVE
				currentRect = new RectangleF (center.X - deltaX, center.Y - deltaY, currentRect.Width, currentRect.Height);
				if(outer.Contains(currentRect)){
					return currentRect;
				}
			}

			if (direction == 1) { // POSITIVE POSITIVE
				currentRect = new RectangleF (center.X + deltaX, center.X + deltaY, currentRect.Width, currentRect.Height);
					if(outer.Contains(currentRect)){
					return currentRect;
				}
			}

			if (direction == 2) { // POSITIVE NEGATIVE
				currentRect = new RectangleF (center.X + deltaX, center.X - deltaY, currentRect.Width, currentRect.Height);
				if(outer.Contains(currentRect)){
					return currentRect;
				}
			}

			if (direction == 3) { // NEGATIVE POSTIVE
				currentRect = new RectangleF (center.X - deltaX, center.X + deltaY, currentRect.Width, currentRect.Height);
				if(outer.Contains(currentRect)){
					return currentRect;
				}
			}

			Console.WriteLine ("recursive call FindNewPosition");
			return FindNewPosition (currentRect, outer);

		}

		static bool checkForCollisions (List<WordCloudItem> positionedWords, RectangleF rect)
		{
//			if (positionedWords.Count == 0) {
//				return false;
//			}

			int x = positionedWords.Count;

			for (int i = 0; i < x; i++) {
				WordCloudItem item = positionedWords [i];
				RectangleF AlreadyPositionedItemRect = GetRectangleF (item);
				if (AlreadyPositionedItemRect.IntersectsWith(rect)) {
					return true;
				}
			}

			return false;

		}

		public static RectangleF GetRectangleF (WordCloudItem item)
		{
			return new RectangleF (item.x, item.y, item.width, item.height);
		}

		static PointF findcenter (RectangleF outer)
		{
			PointF Center = new PointF (outer.Width / 2, outer.Height / 2);
			return Center;
		}


	}
}

