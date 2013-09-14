// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace no.dctapps.Garageindex.screens
{
	[Register ("SearchViewController")]
	partial class SearchResultsController
	{
		[Outlet]
		MonoTouch.UIKit.UISearchBar SearchBarx { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView SearchResultsx { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SearchBarx != null) {
				SearchBarx.Dispose ();
				SearchBarx = null;
			}

			if (SearchResultsx != null) {
				SearchResultsx.Dispose ();
				SearchResultsx = null;
			}
		}
	}
}
