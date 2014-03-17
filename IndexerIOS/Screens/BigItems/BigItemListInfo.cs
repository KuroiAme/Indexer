
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace no.dctapps.Garageindex.screens
{
	public partial class BigItemListInfo : UIViewController
	{
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public BigItemListInfo ()
			: base (UserInterfaceIdiomIsPhone ? "BigItemListInfo_iPhone" : "BigItemListInfo_iPad", null)
		{
		}
		
		/// <summary>
		/// Release everything not in use
		/// </summary>
		void cleanup ()
		{
		}


		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			//cleanup only if view is loaded and not in a window.
			if(this.IsViewLoaded && this.View.Window == null){
				cleanup ();
			}
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

