// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace no.dctapps.Garageindex.screens
{
	[Register ("Preferences")]
	partial class Preferences
	{
		[Outlet]
		MonoTouch.UIKit.UISwitch switchContainers { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel textContainersInLarge { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel textIncludeQR { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch switchQR { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (switchContainers != null) {
				switchContainers.Dispose ();
				switchContainers = null;
			}

			if (textContainersInLarge != null) {
				textContainersInLarge.Dispose ();
				textContainersInLarge = null;
			}

			if (textIncludeQR != null) {
				textIncludeQR.Dispose ();
				textIncludeQR = null;
			}

			if (switchQR != null) {
				switchQR.Dispose ();
				switchQR = null;
			}
		}
	}
}
