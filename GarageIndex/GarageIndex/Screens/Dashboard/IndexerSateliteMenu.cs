using System;
using MonoTouch.UIKit;
using SatelliteMenu;
using No.DCTapps.GarageIndex;
using System.Drawing;
using no.dctapps.garageindex;
using no.dctapps.Garageindex.screens;
using no.dctapps.Garageindex;

namespace GarageIndex
{
	public class IndexerSateliteMenu : UIViewController
	{
		String excludeItem;

		public IndexerSateliteMenu (string item)
		{
			this.excludeItem = item;
		}

		public IndexerSateliteMenu ()
		{
		}

		SatelliteMenuButton MainButton;

		public static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public void InitSateliteMenu(){
			this.View.Frame = new RectangleF (0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);

			var image = UIImage.FromFile ("menu.png");
			var yPos = View.Frame.Height - image.Size.Height - 10;
			var frame = new RectangleF (10, yPos, image.Size.Width, image.Size.Height);

			var items = new [] { 
				new SatelliteMenuButtonItem (UIImage.FromBundle ("scanner4832.png"), 1, "Scanner"),
				new SatelliteMenuButtonItem (Flosshatt.MakeFlosshatt(), 2, "Items"),
				new SatelliteMenuButtonItem (UIImage.FromFile ("table4832.png"), 3, "Big Items"),
				new SatelliteMenuButtonItem (UIImage.FromFile ("container4832.png"), 4, "Containers"),
				new SatelliteMenuButtonItem (UIImage.FromFile ("preferences4832.png"), 5, "Preferences"),
			};

			MainButton = new SatelliteMenuButton (View, image, items, frame);

			MainButton.MenuItemClick += (_, args) => {
				Console.WriteLine ("{0} was clicked!", args.MenuItem.Name);

				if(args.MenuItem.Name == "Scanner"){
					Scanner scanner = new Scanner(this);
					scanner.Scannit();
				}
				if(args.MenuItem.Name == "Items"){
					if(UserInterfaceIdiomIsPhone){
						ItemCatalogue cat = new ItemCatalogue();
						PresentViewControllerAsync(cat, true);
					}else{
						ItemMasterView itemMaster = new ItemMasterView();
						PresentViewControllerAsync(itemMaster, true);
					}
				}
				if(args.MenuItem.Name == "Big Items"){
					if(UserInterfaceIdiomIsPhone){
						BigItemsScreen biggies = new BigItemsScreen();
						PresentViewControllerAsync(biggies, true);
					}else{
						BigItemMasterView bigMaster = new BigItemMasterView();
						PresentViewControllerAsync(bigMaster, true);
					}
				}
				if(args.MenuItem.Name == "Containers"){
					if(UserInterfaceIdiomIsPhone){
						ContainerScreen containers = new ContainerScreen();
						PresentViewControllerAsync(containers,true);
					}else{
						ContainerMasterView containerMaster = new ContainerMasterView();
						PresentViewControllerAsync(containerMaster,true);
					}
				}
				if(args.MenuItem.Name == "Preferences"){
					Preferences pref = new Preferences();
					PresentViewControllerAsync(pref, true);
				}


			};
			this.View.BackgroundColor = UIColor.Clear;
			View.AddSubview (MainButton);
		}

	}
}

