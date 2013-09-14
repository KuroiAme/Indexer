// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace no.dctapps.Garageindex.screens
{
	[Register ("TheStorageScreen")]
	partial class TheStorageScreen
	{
		[Outlet]
		MonoTouch.UIKit.UITextField storageName { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField address { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField keyContact { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField x { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField y { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField z { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel prod { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISlider fillEst { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel remaining { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField zipField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField poststedField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel textDimensions { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel textEstimate { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel textSpaceAvailable { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel M3 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel M3_2 { get; set; }

		[Action ("save:forEvent:")]
		partial void save (MonoTouch.Foundation.NSObject sender, MonoTouch.UIKit.UIEvent @event);
		
		void ReleaseDesignerOutlets ()
		{
			if (storageName != null) {
				storageName.Dispose ();
				storageName = null;
			}

			if (address != null) {
				address.Dispose ();
				address = null;
			}

			if (keyContact != null) {
				keyContact.Dispose ();
				keyContact = null;
			}

			if (x != null) {
				x.Dispose ();
				x = null;
			}

			if (y != null) {
				y.Dispose ();
				y = null;
			}

			if (z != null) {
				z.Dispose ();
				z = null;
			}

			if (prod != null) {
				prod.Dispose ();
				prod = null;
			}

			if (fillEst != null) {
				fillEst.Dispose ();
				fillEst = null;
			}

			if (remaining != null) {
				remaining.Dispose ();
				remaining = null;
			}

			if (zipField != null) {
				zipField.Dispose ();
				zipField = null;
			}

			if (poststedField != null) {
				poststedField.Dispose ();
				poststedField = null;
			}

			if (textDimensions != null) {
				textDimensions.Dispose ();
				textDimensions = null;
			}

			if (textEstimate != null) {
				textEstimate.Dispose ();
				textEstimate = null;
			}

			if (textSpaceAvailable != null) {
				textSpaceAvailable.Dispose ();
				textSpaceAvailable = null;
			}

			if (M3 != null) {
				M3.Dispose ();
				M3 = null;
			}

			if (M3_2 != null) {
				M3_2.Dispose ();
				M3_2 = null;
			}
		}
	}
}
