using System;
using MonoTouch.UIKit;
using GoogleAnalytics.iOS;
using SatelliteMenu;
using System.Drawing;
using no.dctapps.Garageindex.model;

namespace GarageIndex
{
	public class BestLocationMiniViewController : UIViewController
	{
		RectangleF myFrame;
		Lager myLager;
		public BestLocationMiniViewController (RectangleF myFrame)
		{
			this.myFrame = myFrame;
		}

		public void SetActiveLocation (Lager myLager){
			this.myLager = myLager;
			//RELOAD VIEW
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.View.BackgroundColor = UIColor.Magenta;
			this.View.Frame = myFrame;
		}


	}
}

