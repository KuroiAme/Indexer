// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace no.dctapps.commons.events
{
	[Register ("TagDetailScreen")]
	partial class TagDetailScreen
	{
		[Outlet]
		MonoTouch.UIKit.UILabel Label_Height { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel tagIdLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField TextField_height { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel WidthLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField WidthTextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel Xlabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField xTextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel Ylabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField yTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Label_Height != null) {
				Label_Height.Dispose ();
				Label_Height = null;
			}

			if (tagIdLabel != null) {
				tagIdLabel.Dispose ();
				tagIdLabel = null;
			}

			if (WidthLabel != null) {
				WidthLabel.Dispose ();
				WidthLabel = null;
			}

			if (WidthTextField != null) {
				WidthTextField.Dispose ();
				WidthTextField = null;
			}

			if (Xlabel != null) {
				Xlabel.Dispose ();
				Xlabel = null;
			}

			if (xTextField != null) {
				xTextField.Dispose ();
				xTextField = null;
			}

			if (Ylabel != null) {
				Ylabel.Dispose ();
				Ylabel = null;
			}

			if (yTextField != null) {
				yTextField.Dispose ();
				yTextField = null;
			}

			if (TextField_height != null) {
				TextField_height.Dispose ();
				TextField_height = null;
			}
		}
	}
}
