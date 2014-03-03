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

		public DashboardRIghtPanel ()
		{
			//this.myFrame = myFrame;
			//this.parentView = parentView;
		}

		BestLocationMiniViewController mini;



		public SizeF getSize(){
			return new SizeF (this.View.Frame.Width, this.View.Frame.Height);
		}

		public override void LoadView ()
		{
			base.LoadView ();

			this.View.BackgroundColor = UIColor.Clear;
			this.View.Frame = new RectangleF (0, 0, rightPanelWidth, elementHeight*5);
			// init done, now for panel elements;

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			OverSightMap mainMap = new OverSightMap (new RectangleF (0,currentheight, rightPanelWidth, elementHeight),ancestor);
			View.AddSubview (mainMap.View);
			currentheight += elementHeight + buffer;

//			UILabel bestPlace = new UILabel (new RectangleF (0, currentheight, rightPanelWidth, textHeight));
//			bestPlace.Text = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Chosen location or best search result :", "Chosen location or best search result :");
//			bestPlace.AdjustsFontSizeToFitWidth = true;
//			View.AddSubview (bestPlace);
//			currentheight += textHeight + buffer;

//			mini = new BestLocationMiniViewController (new RectangleF (0,currentheight,rightPanelWidth, elementHeight));
//			View.AddSubview (mini.View);
			var clouds = AppDelegate.bl.GetWordCloudDictionary ();
			Console.WriteLine ("dict count at init:"+clouds.Count);
			foreach (WordCloudItem cloud in clouds) {
				Console.WriteLine (cloud);
			}
			WordCloudIOS wordCloud = new WordCloudIOS(ancestor,clouds,new RectangleF(0,currentheight,rightPanelWidth,elementHeight *1.5f));
			View.AddSubview (wordCloud.View);


		}

		void RaiseSearchResult (no.dctapps.Garageindex.model.Lager find)
		{
			Console.WriteLine ("foo");
			//TOD implement me
		}
	}
}

