// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace no.dctapps.garageindex.ios.Screens
{
	[Register ("ContainerItemAddScreen")]
	partial class ContainerItemDetailScreen
	{
		[Outlet]
		MonoTouch.UIKit.UITextField fieldBoxName { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField fieldBoxDescription { get; set; }

		[Action ("btnSave:")]
		partial void btnSave (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (fieldBoxName != null) {
				fieldBoxName.Dispose ();
				fieldBoxName = null;
			}

			if (fieldBoxDescription != null) {
				fieldBoxDescription.Dispose ();
				fieldBoxDescription = null;
			}
		}
	}
}
