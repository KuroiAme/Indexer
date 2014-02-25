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
		
		}
	}
}

