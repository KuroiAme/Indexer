// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace LagerMan
{
	[Register ("HomeScreen")]
	partial class HomeScreen
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnLagerInfo { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnBigThings { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnBoxes { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnOverview { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnLagerInfo != null) {
				btnLagerInfo.Dispose ();
				btnLagerInfo = null;
			}

			if (btnBigThings != null) {
				btnBigThings.Dispose ();
				btnBigThings = null;
			}

			if (btnBoxes != null) {
				btnBoxes.Dispose ();
				btnBoxes = null;
			}

			if (btnOverview != null) {
				btnOverview.Dispose ();
				btnOverview = null;
			}
		}
	}
}
