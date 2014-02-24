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

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.View.Frame = myFrame;
		}
	}
}

