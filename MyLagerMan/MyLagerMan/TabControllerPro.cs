using System;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.dao;
using no.dctapps.Garageindex;
using No.DCTapps.GarageIndex;
using no.dctapps.Garageindex.screens;

namespace no.dctapps.Garageindex.tables
{
	public class TabControllerPro : TabController
		{
//			LagerDAO dao;
//			protected UtilityViewController homescreen = null;
//			UtilityViewController boxesscreen = null;
//			UtilityViewController bigitemscreen = null;
//		UtilityViewController emailscreen = null;
		UtilityViewController storageCatalogue = null;
        StatisticsScreen statscreen = null;

		public LagerMasterView LagerMaster;
//

		UINavigationController lagerNav;
        UINavigationController StatNav;

//			public TabControllerPro()
//			{
//				
//			}
//
			static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
			}

			//Inits array of tabs of type derived from UIViewController
			

		public override void ViewDidLoad ()
		{
//			InitEmailScreen();
			InitLagerCatalogueScreen();
            this.InitStatisticsScreen();


			base.ViewDidLoad ();


//
//			dao = new LagerDAO();

//				InitHomeScreen ();
//				InitBoxesScreen();
//				InitBigItemScreen ( );

//
//				initTabArray ();
		}

//		public override bool ShouldAutorotate ()
//		{
//			this.NavigationController.ShouldAutorotate ();
//		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
			if (UserInterfaceIdiomIsPhone) {
				return base.GetSupportedInterfaceOrientations ();
			} else {
				return UIInterfaceOrientationMask.Landscape;
			}
		}

		public override void initTabArray ()
		{
			UIViewController[] viewControllers = null;
			if (UserInterfaceIdiomIsPhone) {
				viewControllers = new UIViewController[] {
					ItemNav,
					boxNav,
					bigNav,
					lagerNav,
					prefNav,
                    scanNav,
                    StatNav
				};
			}
			else {
				Console.WriteLine("init viewcontrollers for ipad");
				viewControllers = new UIViewController[] {
					ItemMaster,
					containerMaster,
					bigMaster,
					LagerMaster,
					prefNav,
                    scanNav,
                    StatNav
				};
			}
			
			ViewControllers = viewControllers;
			CustomizableViewControllers = new UIViewController[] {
				
			};
			if (UserInterfaceIdiomIsPhone) {
				SelectedViewController = ItemNav;
			} else {
				SelectedViewController = ItemMaster;
			}

		}

		void InitLagerCatalogueScreen ()
		{
			var lagre = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Storages", "Storages");
			
			if(UserInterfaceIdiomIsPhone){
				storageCatalogue = new StorageCatalogue();
				lagerNav = new UINavigationController();
				
				lagerNav.TabBarItem = new UITabBarItem();
				lagerNav.TabBarItem.Title = lagre;
				lagerNav.TabBarItem.Image = UIImage.FromBundle("lagre.png");
				lagerNav.PushViewController(storageCatalogue, false);
			}else{
				//				//init splitviewController
				LagerMaster = new LagerMasterView();
				LagerMaster.TabBarItem = new UITabBarItem();
				LagerMaster.TabBarItem.Title = lagre;
				LagerMaster.TabBarItem.Image = UIImage.FromBundle("lagre.png");
			}
		}

        void InitStatisticsScreen(){
            var stats = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Statistics", "Statistics");

            statscreen = new StatisticsScreen();
            StatNav = new UINavigationController();
            StatNav.TabBarItem = new UITabBarItem();
            StatNav.TabBarItem.Title = stats;
            StatNav.TabBarItem.Image = UIImage.FromBundle("eye.png");
            StatNav.PushViewController(statscreen, true);

        }

//		void InitEmailScreen ()
//		{
//
//			var title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Email", "Email");
//				
//				//email tab
//				if (UserInterfaceIdiomIsPhone) {
//					emailscreen = new Email();
//					emailscreen.Title = title;
//				}else{
//					emailscreen = new Email();
//				}
//				
//				emailNav = new UINavigationController();
//				emailNav.TabBarItem = new UITabBarItem();
//				emailNav.TabBarItem.Title = title;
//				emailNav.TabBarItem.Image = UIImage.FromBundle("email.png");
//				
//				emailNav.PushViewController(emailscreen, false);
//
//		}
	}
}

