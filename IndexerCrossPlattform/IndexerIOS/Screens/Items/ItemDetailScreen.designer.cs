// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace no.dctapps.Garageindex.screens
{
	[Register ("ItemDetailScreen")]
	partial class ItemDetailScreen
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnInContainer { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnPickImageItem { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnUnpickImageItem { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField fieldDescription { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField fieldName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnInContainer != null) {
				btnInContainer.Dispose ();
				btnInContainer = null;
			}

			if (btnPickImageItem != null) {
				btnPickImageItem.Dispose ();
				btnPickImageItem = null;
			}

			if (btnUnpickImageItem != null) {
				btnUnpickImageItem.Dispose ();
				btnUnpickImageItem = null;
			}

			if (fieldDescription != null) {
				fieldDescription.Dispose ();
				fieldDescription = null;
			}

			if (fieldName != null) {
				fieldName.Dispose ();
				fieldName = null;
			}
		}
	}
}
