using System;
using MonoTouch.UIKit;

namespace no.dctapps.Garageindex.themes
{
	public class KuroStyle
	{
		public static void apply(){

//			UIView.Appearance.BackgroundColor = UIColor.FromRGB(255,239,213); //ISbad? Correct, this setting is Horribly BAD to use

//			UIImage blackLeather = UIImage.FromFile("navbarDefaultSize.png");
//			UIImage comicWood = UIImage.FromBundle("ComicWood.png");
			UIImage darkleather = UIImage.FromBundle("darkleathernavbar.png");
//			UIImage darkleatherIcon = UIImage.FromBundle("dark-leather-icon.png");
			UIImage darkleatherIcon = UIImage.FromBundle("dli.png");
//			UINavigationBar.Appearance.SetBackgroundImage(blackLeather, UIBarMetrics.Default);

//			UIButton.Appearance.SetBackgroundImage(blackLeather, UIControlState.Normal);

//			UIWindow.Appearance.BackgroundColor = UIColor.FromRGB(34,139,34); //forrest green
//			UIWindow.Appearance.BackgroundColor = UIColor.FromRGB(32,178,170); //Light sea green
//			UITableView.Appearance.BackgroundColor = UIColor.FromRGB(32,178,170); //light sea green that too

//			UIImage image = UIImage.FromBundle("crinkledPaper.png");
//			UIColor color = UIColor.FromPatternImage (image);
//			UITableView.Appearance.BackgroundColor = color;

//			UITableView.Appearance.BackgroundColor = UIColor.Clear;


			var textAttr = new UITextAttributes();
			textAttr.Font = UIFont.FromName ("Cochin-BoldItalic", 22f);
//			textAttr.TextShadowColor = UIColor.
		
//			textAttr.TextColor = UIColor.FromRGB(173,255,47); //Green yellow
//			textAttr.TextColor = UIColor.FromRGB(255,140,0); //Dark orange
//			textAttr.TextColor = UIColor.FromRGB(255,0,0);
			textAttr.TextColor = UIColor.White;
			textAttr.TextShadowColor = UIColor.FromRGB(255,165,0); // lime green
			
	
			UINavigationBar.Appearance.SetTitleTextAttributes(textAttr);
//			UINavigationBar.Appearance.TintColor = UIColor.FromRGB(32,178,170);
			UITextView.Appearance.BackgroundColor = UIColor.Clear;

			UINavigationBar.Appearance.SetBackgroundImage(darkleather, UIBarMetrics.Default);
			UIBarButtonItem.Appearance.SetBackgroundImage(darkleatherIcon, UIControlState.Normal, UIBarMetrics.Default);

//			var imageView = new UIImageView(UIImage.FromFile("navbarDefaultSize.png")); // bare et eksempel


//			UINavigationBar.Appearance.
//			UINavigationBar.Appearance.ShadowImage = blackLeather;

//			UIButton.Appearance.SetBackgroundImage(blackLeather, UIControlState.Normal);
		}
	}
}