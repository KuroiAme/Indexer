using System;
using System.Drawing;
using System.IO;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MiniZip.ZipArchive;

namespace ZipArchiveSample
{
	public partial class TestAppViewController : UIViewController
	{
		public TestAppViewController () : base ("TestAppViewController", null)
		{
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string AppPath = Path.Combine (documentsPath,"..","ZipArchiveSample.app");
			string LibPath = Path.Combine (documentsPath, "..", "Library");

			string ZipFileName = documentsPath + "/file.zip";

			this.btnZip.TouchUpInside += (sender, e) => {
				var Zip = new ZipArchive();
				NSData data = Zip;
				string[] Paths = {AppPath, LibPath};
				Zip.EasyZip(ZipFileName, Paths, "/", "");
			};

			this.btnUnzip.TouchUpInside += (sender, e) => {
				var Zip = new ZipArchive();
				Zip.EasyUnzip (ZipFileName, documentsPath + "/backup", true, "");
			};

		}

	}
}

