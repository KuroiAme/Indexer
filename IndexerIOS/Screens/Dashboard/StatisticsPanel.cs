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

		UILabel value;

		UILabel store;

		UILabel lagre;

		UILabel ting;

		UILabel beholdere;

		void CreateStatistics ()
		{
			value = AddOneStatistic (AppDelegate.its.getTranslatedText ("Total cash"), AppDelegate.its.getTranslatedText ("value"), AppDelegate.bl.GetTotalValue ().ToString ());
			lagre = AddOneStatistic (AppDelegate.its.getTranslatedText ("number of"), AppDelegate.its.getTranslatedText ("storages"), AppDelegate.dao.GetAntallLagre ());
			ting = AddOneStatistic (AppDelegate.its.getTranslatedText ("number of"), AppDelegate.its.getTranslatedText ("Items"), AppDelegate.dao.GetAntallTing ());
			beholdere = AddOneStatistic (AppDelegate.its.getTranslatedText ("number of"), AppDelegate.its.getTranslatedText ("Containers"), AppDelegate.dao.GetAntallBeholdere ());
			store = AddOneStatistic (AppDelegate.its.getTranslatedText ("number of"), AppDelegate.its.getTranslatedText ("Large Objects"), AppDelegate.dao.GetAntallStore ());
		}

//		public static void ReloadData ()
//		{
//			UpdateStatistics ();
//		}

		public void UpdateStatistics ()
		{
			value.Text = AppDelegate.bl.GetTotalValue ().ToString ();
			lagre.Text = AppDelegate.dao.GetAntallLagre ();
			ting.Text =  AppDelegate.dao.GetAntallTing ();
			beholdere.Text = AppDelegate.dao.GetAntallBeholdere ();
			store.Text = AppDelegate.dao.GetAntallStore ();
		}

		const float margin = 10;
		const float x = 0;
		float y = margin;
		const float textheight = 22;
		float textwidth;


		UILabel AddOneStatistic (string str, string str2, string number)
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
			linethree.Text = number;
			y += textheight + margin;
			this.View.AddSubview (linethree);
			return linethree;
		}

//		public override void ViewDidAppear (bool animated)
//		{
//			base.ViewDidAppear (animated);
//			UpdateStatistics ();
//
//		}
	}
}

