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

		/// <summary>
		/// Release everything not in use
		/// </summary>
		void cleanup ()
		{
			Dispose ();
		}

		protected override void Dispose (bool disposing)
		{
			this.words = null;
			this.ancestor = null;
			cloud.Dispose ();
			base.Dispose (disposing);
		}


		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			//cleanup only if view is loaded and not in a window.
			if(this.IsViewLoaded && this.View.Window == null){
				//cleanup ();
			}
			// Release any cached data, images, etc that aren't in use.
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

