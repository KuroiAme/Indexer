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
	[Register ("SelectContainer")]
	partial class SelectContainer
	{
		[Outlet]
		MonoTouch.UIKit.UITableView mySelectTable { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (mySelectTable != null) {
				mySelectTable.Dispose ();
				mySelectTable = null;
			}
		}
	}
}
