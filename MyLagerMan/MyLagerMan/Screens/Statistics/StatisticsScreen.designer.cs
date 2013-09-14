// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace No.DCTapps.GarageIndex
{
	[Register ("StatisticsScreen")]
	partial class StatisticsScreen
	{
		[Outlet]
		MonoTouch.UIKit.UILabel number_containers { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel number_items { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel number_large { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel number_storages { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (number_storages != null) {
				number_storages.Dispose ();
				number_storages = null;
			}

			if (number_containers != null) {
				number_containers.Dispose ();
				number_containers = null;
			}

			if (number_items != null) {
				number_items.Dispose ();
				number_items = null;
			}

			if (number_large != null) {
				number_large.Dispose ();
				number_large = null;
			}
		}
	}
}
