using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace GarageIndex
{
	public class UtilityViewController : UIViewController
	{
		public UtilityViewController (string nib) : base (nib, null)
		{
		}

		public UtilityViewController () : base ()
		{
		}

		public static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

	}
}

