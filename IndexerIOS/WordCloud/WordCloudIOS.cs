using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Drawing;

namespace IndexerIOS
{
	public class WordCloudIOS : UIViewController
	{
		UIViewController ancestor;
		List<WordCloudItem> words;
		RectangleF myframe;

		public WordCloudIOS (UIViewController ancestor, List<WordCloudItem> words, RectangleF myframe)
		{
			this.words = words;
			this.ancestor = ancestor;
			this.myframe = myframe;
		}

		public override void LoadView ()
		{
			base.LoadView ();
			this.View.Frame = myframe;
			this.View.BackgroundColor = UIColor.Clear;
		}

		WordCloudView cloud;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.View.BackgroundColor = UIColor.Clear;

			if (words != null) {

				this.words = IndexerUtils.CalculateBoxes (words, View.Bounds);

				cloud = new WordCloudView (View.Bounds, words);
				cloud.BackgroundColor = UIColor.Clear;
				this.View.AddSubview (cloud);
			}
		}

		public UIImage GetCloudAsImage ()
		{
			return cloud.RerenderAsUIImage ();
		}
	}
}

