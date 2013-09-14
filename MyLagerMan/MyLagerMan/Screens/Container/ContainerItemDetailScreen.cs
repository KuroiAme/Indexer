using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using no.dctapps.garageindex.ads;
using no.dctapps.garageindex.dao;
using no.dctapps.garageindex.model;

namespace no.dctapps.garageindex.ios.Screens
{
	public partial class ContainerItemDetailScreen : UtilityViewController
	{
		LagerDAO dao = new LagerDAO ();
		Box boks;

		public ContainerItemDetailScreen (Box boks)
			: base (UserInterfaceIdiomIsPhone ? "ContainerItemDetailScreen_iPhone" : "ContainerItemDetailScreen_iPad")
		{
			this.boks = boks;
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			Xamarin.Themes.BlackLeatherTheme.Apply (this.View);
			Xamarin.Themes.BlackLeatherTheme.Apply (this.fieldBoxDescription);
			Xamarin.Themes.BlackLeatherTheme.Apply (this.fieldBoxName);

			Title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Container Item Details", "Container Item Details");
			base.ViewDidLoad ();
			this.fieldBoxName.Text = boks.Name;

			this.fieldBoxName.Placeholder = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Name", "Name");
			this.fieldBoxDescription.Text = boks.Description;
			this.fieldBoxDescription.Placeholder = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Description", "Description");
//			table = new UITableView(new RectangleF)
//			UIImage image = UIImage.FromBundle("crinkledPaper.png");
//			UIColor color = UIColor.FromPatternImage (image);
//			this.View.BackgroundColor = color;
		}

		partial void btnSave(MonoTouch.Foundation.NSObject sender){
			this.boks.Name = this.fieldBoxName.Text;
			this.boks.Description = this.fieldBoxDescription.Text;
			if(this.boks.isNewItem){
				boks.isNewItem = false;
				dao.insertBox(boks);
			}else{
				dao.updateBox(boks);
			}
		}
	}
}

