// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace No.Dctapps.Garageindex.Ios.Screens
{
	[Register ("BigItemAddScreen")]
	partial class BigItemDetailScreen
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnBigPickImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnIn { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnRemoveImg { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField fieldBigDescription { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField fieldBigName { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIScrollView scrollContent { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnBigPickImage != null) {
				btnBigPickImage.Dispose ();
				btnBigPickImage = null;
			}

			if (btnIn != null) {
				btnIn.Dispose ();
				btnIn = null;
			}

			if (btnRemoveImg != null) {
				btnRemoveImg.Dispose ();
				btnRemoveImg = null;
			}

			if (fieldBigDescription != null) {
				fieldBigDescription.Dispose ();
				fieldBigDescription = null;
			}

			if (fieldBigName != null) {
				fieldBigName.Dispose ();
				fieldBigName = null;
			}

			if (scrollContent != null) {
				scrollContent.Dispose ();
				scrollContent = null;
			}
		}
	}
}
