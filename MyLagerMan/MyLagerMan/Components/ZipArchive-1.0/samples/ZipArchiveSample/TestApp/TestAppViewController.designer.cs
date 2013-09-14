// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace ZipArchiveSample
{
	[Register ("TestAppViewController")]
	partial class TestAppViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnUnzip { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnZip { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnUnzip != null) {
				btnUnzip.Dispose ();
				btnUnzip = null;
			}

			if (btnZip != null) {
				btnZip.Dispose ();
				btnZip = null;
			}
		}
	}
}
