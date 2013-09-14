
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace no.dctapps.Garageindex.screens
{
	public partial class SearchResultsController2 : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("SearchResultsController2");
		public static readonly UINib Nib;
		
		static SearchResultsController2 ()
		{
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
				Nib = UINib.FromName ("SearchResultsController2_iPhone", NSBundle.MainBundle);
			else
				Nib = UINib.FromName ("SearchResultsController2_iPad", NSBundle.MainBundle);
		}
		
		public SearchResultsController2 (IntPtr handle) : base (handle)
		{
		}
		
		public static SearchResultsController2 Create ()
		{
			return (SearchResultsController2)Nib.Instantiate (null, null) [0];
		}
	}
}

