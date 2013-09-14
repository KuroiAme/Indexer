// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace no.dctapps.bundlemanifest
{
	[Register ("OverviewScreen")]
	partial class OverviewScreen
	{
		[Outlet]
		MonoTouch.UIKit.UILabel textOversikt { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel textAntallStore { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel textAntallEsker { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel tekstAntallTingIEsker { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel tallAntallStore { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel tallAntallEsker { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel tallAntTing { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (textOversikt != null) {
				textOversikt.Dispose ();
				textOversikt = null;
			}

			if (textAntallStore != null) {
				textAntallStore.Dispose ();
				textAntallStore = null;
			}

			if (textAntallEsker != null) {
				textAntallEsker.Dispose ();
				textAntallEsker = null;
			}

			if (tekstAntallTingIEsker != null) {
				tekstAntallTingIEsker.Dispose ();
				tekstAntallTingIEsker = null;
			}

			if (tallAntallStore != null) {
				tallAntallStore.Dispose ();
				tallAntallStore = null;
			}

			if (tallAntallEsker != null) {
				tallAntallEsker.Dispose ();
				tallAntallEsker = null;
			}

			if (tallAntTing != null) {
				tallAntTing.Dispose ();
				tallAntTing = null;
			}
		}
	}
}
