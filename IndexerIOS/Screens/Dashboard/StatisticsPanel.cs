using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace GarageIndex
{
	public class StatisticsPanel : UIViewController
	{

		RectangleF myFrame;

		public StatisticsPanel (RectangleF myFrame)
		{
			this.myFrame = myFrame;
		}

		public override void LoadView ()
		{
			base.LoadView ();
			this.View.Frame = myFrame;
			this.View.BackgroundColor = UIColor.Purple;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.View.BackgroundColor = UIColor.Clear;
			y = margin;
			textwidth = this.View.Bounds.Width;
			CreateStatistics ();
		}

		void CreateStatistics ()
		{
			AddOneStatistic (AppDelegate.its.getTranslatedText("Total cash"),AppDelegate.its.getTranslatedText("value"), AppDelegate.bl.GetTotalValue());
			AddOneStatistic (AppDelegate.its.getTranslatedText("number of"),AppDelegate.its.getTranslatedText("storages"), AppDelegate.dao.GetAntallLagre());
			AddOneStatistic (AppDelegate.its.getTranslatedText("number of"),AppDelegate.its.getTranslatedText("Items"), AppDelegate.dao.GetAntallTing());
			AddOneStatistic (AppDelegate.its.getTranslatedText("number of"),AppDelegate.its.getTranslatedText("Containers"), AppDelegate.dao.GetAntallBeholdere());
			AddOneStatistic (AppDelegate.its.getTranslatedText("number of"),AppDelegate.its.getTranslatedText("Large Objects"), AppDelegate.dao.GetAntallStore());
		}

		const float margin = 10;
		const float x = 0;
		float y = margin;
		float textheight = 22;
		float textwidth;


		void AddOneStatistic (string str, string str2, object getAntallLagre)
		{
			UILabel lineone = new UILabel (new RectangleF (x, y, textwidth, textheight));
			lineone.Text = str;
			y += textheight;
			this.View.AddSubview (lineone);

			UILabel linetwo = new UILabel (new RectangleF (x, y, textwidth, textheight));
			linetwo.Text = str2;
			y += textheight;
			this.View.AddSubview (linetwo);

			UILabel linethree = new UILabel (new RectangleF (x, y, textwidth, textheight));
			linetwo.Text = str2;
			y += textheight + margin;
			this.View.AddSubview (linetwo);
		}
	}
}

