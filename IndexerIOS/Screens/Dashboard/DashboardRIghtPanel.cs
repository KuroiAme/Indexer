using System;
using MonoTouch.UIKit;
using System.Drawing;
using IndexerIOS;

namespace GarageIndex
{
	public class DashboardRIghtPanel : UIViewController
	{
		RectangleF myFrame;
		UIView parentView;

		const float elementHeight = 175;
		const float buffer = 10;
		const float textHeight = 30;
		float currentheight = 0;
		float rightPanelWidth;
		UIViewController ancestor;

		public DashboardRIghtPanel (float rightPanelWidth, UIViewController ancestor)
		{
			this.rightPanelWidth = rightPanelWidth;
			this.ancestor = ancestor;
		}

		static float GetPanelHeight ()
		{
			return elementHeight * 2.5f + buffer;
		}

		public SizeF getSize(){
			return new SizeF (rightPanelWidth, GetPanelHeight ());
		}

		public override void LoadView ()
		{
			base.LoadView ();

			this.View.BackgroundColor = UIColor.Clear;
			this.View.Frame = new RectangleF (0, 0, rightPanelWidth, GetPanelHeight());
			// init done, now for panel elements;

		}

		public OverSightMap MainMap;

		WordCloudIOS wordCloud;

		System.Collections.Generic.List<WordCloudItem> clouds;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			MainMap = new OverSightMap (new RectangleF (0, currentheight, rightPanelWidth, elementHeight), ancestor);
			View.AddSubview (MainMap.View);
			currentheight += elementHeight + buffer;

//			UILabel bestPlace = new UILabel (new RectangleF (0, currentheight, rightPanelWidth, textHeight));
//			bestPlace.Text = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Chosen location or best search result :", "Chosen location or best search result :");
//			bestPlace.AdjustsFontSizeToFitWidth = true;
//			View.AddSubview (bestPlace);
//			currentheight += textHeight + buffer;

//			mini = new BestLocationMiniViewController (new RectangleF (0,currentheight,rightPanelWidth, elementHeight));
//			View.AddSubview (mini.View);
			clouds = AppDelegate.bl.GetWordCloudDictionary ();
//			Console.WriteLine ("dict count at init:"+clouds.Count);
//			foreach (WordCloudItem cloud in clouds) {
//				Console.WriteLine (cloud);
//			}
			wordCloud = new WordCloudIOS (ancestor, clouds, new RectangleF (0, currentheight, rightPanelWidth, elementHeight * 1.5f));
			View.AddSubview (wordCloud.View);


		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			currentheight = 0;

			if (MainMap == null) {
				MainMap = new OverSightMap (new RectangleF (0, currentheight, rightPanelWidth, elementHeight), ancestor);

			}

			currentheight += elementHeight;

			if (clouds == null) {
				clouds = AppDelegate.bl.GetWordCloudDictionary ();
			}

			if (wordCloud == null) {
				wordCloud = new WordCloudIOS (ancestor, clouds, new RectangleF (0, currentheight, rightPanelWidth, elementHeight * 1.5f));
				View.AddSubview (wordCloud.View);
			}
		}

		void cleanup ()
		{
			MainMap = null;
			clouds = null;
			wordCloud = null;
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			if(this.IsViewLoaded && this.View.Window == null){
				cleanup ();

			}
			// Release any cached data, images, etc that aren't in use.
		}


		void RaiseSearchResult (no.dctapps.Garageindex.model.Lager find)
		{
			Console.WriteLine ("foo");
			//TOD implement me
		}
	}
}

