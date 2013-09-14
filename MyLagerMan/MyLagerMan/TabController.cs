using System;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.dao;
using No.Dctapps.Garageindex.Ios.Screens;
using no.dctapps.Garageindex;
using No.DCTapps.GarageIndex;
using no.dctapps.garageindex;
using no.dctapps.Garageindex.screens;

namespace no.dctapps.Garageindex
{
	public class TabController : UITabBarController
		{
			public UtilityViewController homescreen = null;
            public UITableViewController boxesscreen = null;
			public UITableViewController bigitemscreen = null;
			public UtilityViewController ItemCatalogue = null;
			public UtilityViewController preferences = null;

			public BigItemMasterView bigMaster;
			public ContainerMasterView containerMaster;
			public ItemMasterView ItemMaster;
			

			public UINavigationController homeNav,bigNav,boxNav, ItemNav, prefNav;

			static bool UserInterfaceIdiomIsPhone {
				get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
			}

		//Inits array of tabs of type derived from UIViewController
		public virtual void initTabArray ()
		{
			UIViewController[] viewControllers = null;
			if (UserInterfaceIdiomIsPhone) {
				viewControllers = new UIViewController[] {
					ItemNav,
					boxNav,
					bigNav,
					homeNav,
					prefNav
				};
			}
			else {
				Console.WriteLine("init viewcontrollers for ipad");
					viewControllers = new UIViewController[] {
						ItemMaster,
						containerMaster,
						bigMaster, //skal egentlig være boxMaster
						homeNav,
						prefNav
					};
			}

			ViewControllers = viewControllers;
			CustomizableViewControllers = new UIViewController[] {
			};
			SelectedViewController = homeNav;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
//			dao = new LagerDAO();

//				InitSearchScreen();
				InitItemScreen();
//				InitHomeScreen ();
				InitBoxesScreen();
				InitBigItemScreen ( );
				InitPreferencesScreen();
                InitScanner();


				initTabArray ();
		}

		public void InitItemScreen ()
		{
			var lbtext = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Items", "Items");
			
			if(UserInterfaceIdiomIsPhone){
				ItemCatalogue = new ItemCatalogue();
				ItemNav = new UINavigationController();
				ItemNav.TabBarItem = new UITabBarItem ();
				ItemNav.TabBarItem.Title = lbtext;
				ItemNav.TabBarItem.Image = UIImage.FromBundle("frimerke.png");
//				ItemNav.TabBarItem.BadgeValue = dao.getAntallTing();
				ItemNav.PushViewController(ItemCatalogue, false);
			}else{
				ItemMaster = new ItemMasterView();
				ItemMaster.TabBarItem = new UITabBarItem ();
				ItemMaster.TabBarItem.Title = lbtext;
				ItemMaster.TabBarItem.Image = UIImage.FromBundle("frimerke.png");
//				ItemMaster.TabBarItem.BadgeValue = dao.getAntallTing();
			}
		}

//		public void InitSearchScreen ()
//		{
//			var lbtext = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Item-search", "Item-search");
//			
//			if(UserInterfaceIdiomIsPhone){
//				SearchScreen = new SearchResultsController();
//				SearchNav = new UINavigationController();
//				SearchNav.TabBarItem = new UITabBarItem ();
//				SearchNav.TabBarItem.Title = lbtext;
//				SearchNav.TabBarItem.Image = UIImage.FromBundle("bord.png");
////				SearchNav.TabBarItem.BadgeValue = dao.getAntallStore();
//				SearchNav.PushViewController(SearchScreen, false);
//			}else{
//				Console.WriteLine("que pasa?");
//				bigMaster = new BigItemMasterView();
//				bigMaster.TabBarItem = new UITabBarItem ();
//				bigMaster.TabBarItem.Title = lbtext;
//				bigMaster.TabBarItem.Image = UIImage.FromBundle("bord.png");
////				bigMaster.TabBarItem.BadgeValue = dao.getAntallStore();
//			}
//		}


		public void InitBigItemScreen ()
		{
			var lbtext = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Large Objects", "Large Objects");

			if(UserInterfaceIdiomIsPhone){
				bigitemscreen = new BigItemsScreen();
				bigNav = new UINavigationController();
				bigNav.TabBarItem = new UITabBarItem ();
				bigNav.TabBarItem.Title = lbtext;
				bigNav.TabBarItem.Image = UIImage.FromBundle("bord.png");
//				bigNav.TabBarItem.BadgeValue = dao.getAntallStore();
				bigNav.PushViewController(bigitemscreen, false);
			}else{
				Console.WriteLine("que pasa?");
				bigMaster = new BigItemMasterView();
				bigMaster.TabBarItem = new UITabBarItem ();
				bigMaster.TabBarItem.Title = lbtext;
				bigMaster.TabBarItem.Image = UIImage.FromBundle("bord.png");
//				bigMaster.TabBarItem.BadgeValue = dao.getAntallStore();
			}
		}


		public void InitBoxesScreen ()
		{
			var box = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Containers", "Containers");

			if(UserInterfaceIdiomIsPhone){
				boxesscreen = new ContainerScreen();
				boxNav = new UINavigationController();

				boxNav.TabBarItem = new UITabBarItem();
				boxNav.TabBarItem.Title = box;
				boxNav.TabBarItem.Image = UIImage.FromBundle("Box.png");
//				boxNav.TabBarItem.BadgeValue = dao.getAntallBeholdere();

				boxNav.PushViewController(boxesscreen, false);
			}else{
//				//init splitviewController
				containerMaster = new ContainerMasterView();
				containerMaster.TabBarItem = new UITabBarItem();
				containerMaster.TabBarItem.Title = box;
				containerMaster.TabBarItem.Image = UIImage.FromBundle("Box.png");
//				containerMaster.TabBarItem.BadgeValue = dao.getAntallBeholdere();
			}
		}

//		public void InitHomeScreen ()
//		{
//			var title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Storage Data", "Storage Data");
//
//			//Home tab
//			if (UserInterfaceIdiomIsPhone) {
//				homescreen = new TheStorageScreen();
//				homescreen.Title = title;
//			}else{
//				homescreen = new TheStorageScreen();
//			}
//
//			homeNav = new UINavigationController();
//			homeNav.TabBarItem = new UITabBarItem();
//			homeNav.TabBarItem.Title = title;
//			homeNav.TabBarItem.Image = UIImage.FromBundle("HomeScreenTab.png");
//
//			homeNav.PushViewController(homescreen, false);
//
//		}

		public void InitPreferencesScreen ()
		{
			var title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Preferences", "Preferences");
			
			//Home tab
			if (UserInterfaceIdiomIsPhone) {
				preferences = new Preferences();
				preferences.Title = title;
			}else{
				preferences = new Preferences();
			}
			
			prefNav = new UINavigationController();
			prefNav.TabBarItem = new UITabBarItem();
			prefNav.TabBarItem.Title = title;
			prefNav.TabBarItem.Image = UIImage.FromBundle("HomeScreenTab.png");
			
			prefNav.PushViewController(preferences, false);
			
		}

        public Scanner scanner;

        public UINavigationController scanNav;

        public void InitScanner ()
        {
            var title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Scanner", "Scanner");

            //Home tab
            if (UserInterfaceIdiomIsPhone) {
                scanner = new Scanner();
                scanner.Title = title;
            }else{
                scanner = new Scanner();
            }

            scanNav = new UINavigationController();
            scanNav.TabBarItem = new UITabBarItem();
            scanNav.TabBarItem.Title = title;
            scanNav.TabBarItem.Image = UIImage.FromBundle("eye.png");

            scanNav.PushViewController(scanner, true);

        }

//		void InitMoreScreen ()
//		{
//			var title = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("More", "More");
//			
//			//Home tab
//			if (UserInterfaceIdiomIsPhone) {
//				homescreen = new TheStorageScreen();
//				homescreen.Title = title;
//			}else{
//				homescreen = new TheStorageScreen();
//			}
//			
//			homeNav = new UINavigationController();
//			homeNav.TabBarItem = new UITabBarItem();
//			homeNav.TabBarItem.Title = title;
//			homeNav.TabBarItem.Image = UIImage.FromBundle("HomeScreenTab.png");
//			
//			
//			homeNav.PushViewController(homescreen, false);
//			
//			
//			
//		}
	}
}

