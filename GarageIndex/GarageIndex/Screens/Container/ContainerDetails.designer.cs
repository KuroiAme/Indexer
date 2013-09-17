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
	[Register ("ContainerDetails")]
	partial class ContainerDetails
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnShowContent { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField fieldContainerName { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField fieldDescription { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton inStorage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView myTable { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField textType { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (fieldContainerName != null) {
				fieldContainerName.Dispose ();
				fieldContainerName = null;
			}

			if (fieldDescription != null) {
				fieldDescription.Dispose ();
				fieldDescription = null;
			}

			if (inStorage != null) {
				inStorage.Dispose ();
				inStorage = null;
			}

			if (myTable != null) {
				myTable.Dispose ();
				myTable = null;
			}

			if (btnShowContent != null) {
				btnShowContent.Dispose ();
				btnShowContent = null;
			}

			if (textType != null) {
				textType.Dispose ();
				textType = null;
			}
		}
	}
}
