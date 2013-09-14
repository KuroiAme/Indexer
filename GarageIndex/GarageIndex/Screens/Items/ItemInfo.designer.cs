// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace no.dctapps.Garageindex.screens
{
	[Register ("ItemInfo")]
	partial class ItemInfo
	{
		[Outlet]
		MonoTouch.UIKit.UITextField fieldActionComment { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (fieldActionComment != null) {
				fieldActionComment.Dispose ();
				fieldActionComment = null;
			}
		}
	}
}
