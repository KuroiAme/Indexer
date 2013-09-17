using System;
using MonoTouch.UIKit;
using No.DCTapps.GarageIndex;
using no.dctapps.garageindex;
using no.dctapps.Garageindex.screens;

namespace GarageIndex
{
	public class TabController : UITabBarController
	{
		public UtilityViewController homescreen = null;
		public UITableViewController boxesscreen = null;
		public UITableViewController bigitemscreen = null;
		public UITableViewController ItemCatalogue = null;
		public UtilityViewController preferences = null;
		public UITableViewController LagerList = null;
		StatisticsScreen statscreen = null;
		public Scanner scanner;


		public BigItemMasterView bigMaster;
		public no.dctapps.Garageindex.ContainerMasterView containerMaster;
		public ItemMasterView ItemMaster;
		public LagerMasterView LagerMaster;

		UINavigationController lagerNav;
		UINavigationController StatNav;
		public UINavigationController homeNav,bigNav,boxNav, ItemNav, prefNav;
		public UINavigationController scanNav;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			InitItemScreen();
			InitBoxesScreen();
			InitBigItemScreen ( );
			InitPreferencesScreen();
			InitScanner();
			InitLagerCatalogueScreen();
			InitStatisticsScreen();


			initTabArray ();
		}

		public void initTabArray ()
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

		public void InitItemScreen ()
		{
			var lbtext = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Items", "Items");

			if(UserInterfaceIdiomIsPhone){
				ItemCatalogue = new ItemCatalogue();
				ItemNav = new UINavigationController();
				ItemNav.TabBarItem = new UITabBarItem ();
				ItemNav.TabBarItem.Title = lbtext;
				ItemNav.TabBarItem.Image = UIImage.FromBundle("stampy.png");
				ItemNav.PushViewController(ItemCatalogue, false);
			}else{
				ItemMaster = new ItemMasterView();
				ItemMaster.TabBarItem = new UITabBarItem ();
				ItemMaster.TabBarItem.Title = lbtext;
				ItemMaster.TabBarItem.Image = UIImage.FromBundle("stampy.png");
			}
		}

		public void InitBigItemScreen ()
		{
			var lbtext = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Large Objects", "Large Objects");

			if(UserInterfaceIdiomIsPhone){
				bigitemscreen = new BigItemsScreen();
				bigNav = new UINavigationController();
				bigNav.TabBarItem = new UITabBarItem ();
				bigNav.TabBarItem.Title = lbtext;
				bigNav.TabBarItem.Image = UIImage.FromBundle("table.png");
				bigNav.PushViewController(bigitemscreen, false);
			}else{
				bigMaster = new BigItemMasterView();
				bigMaster.TabBarItem = new UITabBarItem ();
				bigMaster.TabBarItem.Title = lbtext;
				bigMaster.TabBarItem.Image = UIImage.FromBundle("table.png");
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
				boxNav.TabBarItem.Image = UIImage.FromBundle ("box.png");
				boxNav.PushViewController(boxesscreen, false);
			}else{
				//				//init splitviewController
				containerMaster = new no.dctapps.Garageindex.ContainerMasterView();
				containerMaster.TabBarItem = new UITabBarItem();
				containerMaster.TabBarItem.Title = box;
				containerMaster.TabBarItem.Image = UIImage.FromBundle("box.png");
			}
		}

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

		void InitStatisticsScreen(){
			var stats = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Statistics", "Statistics");

			statscreen = new StatisticsScreen();
			StatNav = new UINavigationController();
			StatNav.TabBarItem = new UITabBarItem();
			StatNav.TabBarItem.Title = stats;
			StatNav.TabBarItem.Image = UIImage.FromBundle("eye.png");
			StatNav.PushViewController(statscreen, true);

		}

		void InitLagerCatalogueScreen ()
		{
			var lagre = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Storages", "Storages");

			if(UserInterfaceIdiomIsPhone){
				LagerList = new LagerList();
				lagerNav = new UINavigationController();

				lagerNav.TabBarItem = new UITabBarItem();
				lagerNav.TabBarItem.Title = lagre;
//				lagerNav.TabBarItem.Image = UIImage.FromBundle("lagre.png");
				lagerNav.PushViewController(LagerList, false);
			}else{
				//				//init splitviewController
				LagerMaster = new LagerMasterView();
				LagerMaster.TabBarItem = new UITabBarItem();
				LagerMaster.TabBarItem.Title = lagre;
//				LagerMaster.TabBarItem.Image = UIImage.FromBundle("lagre.png");
			}
		}


	}
}

