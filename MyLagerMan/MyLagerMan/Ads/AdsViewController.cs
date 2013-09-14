//using GoogleAdMobAds;
//using System;
//using System.Drawing;
//
//using MonoTouch.Foundation;
//using MonoTouch.UIKit;
//
//namespace no.dctapps.garageindex.ads
//{
//	public partial class AdsViewController : UtilityViewController
//	{
//
//		const string AdmobID_iphone = "a151431a930a1e9";
//		const string AdmobID_ipad = "a151431e078e783";
//
//		GADBannerView adView;
//		bool viewOnScreen = false;
//
//		PointF iphone;
//		PointF ipad;
//
//		void Setup ()
//		{
//			float y = UIScreen.MainScreen.Bounds.Bottom - 160;
//			float y2 = UIScreen.MainScreen.Bounds.Bottom - 500;
//			Console.WriteLine ("adsY:" + y);
//			iphone = new PointF (0, y);
//			ipad = new PointF (0, y2);
//		}
//
//		public AdsViewController (string nib)
//			: base (nib)
//		{
//			Setup ();
//		}
//
//		public AdsViewController() : base(){
//
//			Setup();
//		}
//		
//		public override void DidReceiveMemoryWarning ()
//		{
//			// Releases the view if it doesn't have a superview.
//			base.DidReceiveMemoryWarning ();
//			
//			// Release any cached data, images, etc that aren't in use.
//		}
//
//
//		
//		public override void ViewDidLoad ()
//		{
//			base.ViewDidLoad ();
//
////			if(!pro) {	
////				if(UserInterfaceIdiomIsPhone){
////					loadIphoneAds();
////				}else{
////					loadIPadAds();
////				}
////			}
//			// Perform any additional setup after loading the view, typically from a nib.
//		}
//
//		void loadIphoneAds ()
//		{
//			adView = new GADBannerView (size: GADAdSizeCons.Banner, origin: iphone) {
//				AdUnitID = AdmobID_iphone,
//				RootViewController = this
//			};
//			
//			adView.DidReceiveAd += (sender, args) => {
//				if (!viewOnScreen) View.AddSubview (adView);
//				viewOnScreen = true;
//			};
//			
//			adView.LoadRequest (new GADRequest ());
//		}
//
//		void loadIPadAds ()
//		{
//			adView = new GADBannerView (size: GADAdSizeCons.Leaderboard, origin: ipad) {
//				AdUnitID = AdmobID_ipad,
//				RootViewController = this
//			};
//			
//			adView.DidReceiveAd += (sender, args) => {
//				if (!viewOnScreen) View.AddSubview (adView);
//				viewOnScreen = true;
//			};
//			
//			adView.LoadRequest (new GADRequest ());
//		}
//	}
//}
//
