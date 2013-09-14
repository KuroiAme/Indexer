// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace no.dctapps.garageindex.screens
{
	[Register ("Email")]
	partial class Email
	{
		[Outlet]
		MonoTouch.UIKit.UISwitch switchPictureAttachments { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch switchItems { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel textPictureAttachments { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel textItems { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (switchPictureAttachments != null) {
				switchPictureAttachments.Dispose ();
				switchPictureAttachments = null;
			}

			if (switchItems != null) {
				switchItems.Dispose ();
				switchItems = null;
			}

			if (textPictureAttachments != null) {
				textPictureAttachments.Dispose ();
				textPictureAttachments = null;
			}

			if (textItems != null) {
				textItems.Dispose ();
				textItems = null;
			}
		}
	}
}
