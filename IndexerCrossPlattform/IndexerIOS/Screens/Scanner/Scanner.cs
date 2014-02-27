using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ZXing.Mobile;
using no.dctapps.Garageindex.model;
using System.Collections.Generic;
using no.dctapps.Garageindex.screens;
using No.Dctapps.Garageindex.Ios.Screens;
using GarageIndex;
using GoogleAnalytics.iOS;

namespace No.DCTapps.GarageIndex
{
	public partial class Scanner //: UIViewController
    {
		UIViewController parent;

        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

		public Scanner (UIViewController parent)
		{
			this.parent = parent;
		}

//        public Scanner()
//			//: base (UserInterfaceIdiomIsPhone ? "Scanner_iPhone" : "Scanner_iPad", null)
//        {
//        }

//        public override void DidReceiveMemoryWarning()
//        {
//            // Releases the view if it doesn't have a superview.
//            base.DidReceiveMemoryWarning();
//			
//            // Release any cached data, images, etc that aren't in use.
//        }
//
//        public override void ViewWillAppear(bool animated)
//        {
//            base.ViewWillAppear(animated);
//
//			//Scannit();
//        }
//			
//
//		public override void ViewDidAppear (bool animated)
//		{
//			base.ViewDidAppear (animated);
//			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Scanner Screen");
//			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
//		}

		async public void Scannit()
        {
			var scanner = new MobileBarcodeScanner ();
            var opt = new MobileBarcodeScanningOptions();
            opt.PossibleFormats.Clear();
            opt.PossibleFormats.Add(ZXing.BarcodeFormat.QR_CODE);
            var result = await scanner.Scan(opt);
            HandleResult(result);
        }

		//
		//base.ViewDidLoad();
			//itle = NSBundle.MainBundle.LocalizedString("Scanner", "Scanner");
//            Xamarin.Themes.BlackLeatherTheme.Apply(this.View);
          

            // Perform any additional setup after loading the view, typically from a nib.
			//}



        void HandleResult(ZXing.Result result){

            if (result != null)
            {
                var msg = "NO barcode!";
                msg = "barcode: " + result.Text + "was not in your database";
//                LagerDAO dao = new LagerDAO();
                int id = -1;
                try{
                    id = Convert.ToInt32(result.Text);
                }catch(Exception e){
                    Console.WriteLine(e.Message);
                }
                IList<LagerObject> lol = null;
                if(id != -1){
					lol = AppDelegate.dao.GetLagerObjectByID(id);
                }

                if (lol != null)
                {
                    if (lol.Count == 0)
                    {
                        var title = "no barcode";
                        var alert = new UIAlertView(title, msg, null, "cancel", null);
                        alert.Show();
                    }
                    else
                    {
                        LagerObject lo = lol[0];
                        if (lo.isContainer == "true")
                        {
							var cd = new no.dctapps.Garageindex.screens.ContainerDetails(lo);
							parent.PresentViewControllerAsync(cd, true);
                        }
                        else if (lo.isLargeObject == "true")
                        {
                            BigItemDetailScreen bs = new BigItemDetailScreen(lo);
							parent.PresentViewControllerAsync(bs, true);
                       
                        }
                        else
                        {
                            var alert = new UIAlertView("No such object", msg, null, "cancel", null);
                            alert.Show();
                        }
                    }
                }
            }
        }

        

    }
}

