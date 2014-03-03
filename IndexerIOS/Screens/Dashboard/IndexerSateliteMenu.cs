using System;
using MonoTouch.UIKit;
using SatelliteMenu;
using No.DCTapps.GarageIndex;
using System.Drawing;
using no.dctapps.garageindex;
using no.dctapps.Garageindex.screens;
using no.dctapps.Garageindex;
using System.Collections.Generic;

namespace GarageIndex
{
	public class IndexerSateliteMenu : UIViewController
	{
		String excludeItem;

		UIViewController ancestor;

		public IndexerSateliteMenu (string item, UIViewController ancestor)
		{
			this.excludeItem = item;
			this.ancestor = ancestor;
		}

		public IndexerSateliteMenu ()
		{
		}

		SatelliteMenuButton SateliteButton;

		public static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			InitSateliteMenu ();
			//this.View.BackgroundColor = UIColor.Red;
		}

		public override void LoadView ()
		{
			float cube = 190;
			base.LoadView ();
			this.View.Frame = new RectangleF (0, ancestor.View.Bounds.Height - cube, cube, cube);

		}

		private SatelliteMenuButtonItem[] GetMenuItems ()
		{
			int counter = 1;

			List<SatelliteMenuButtonItem> myList = new List<SatelliteMenuButtonItem> ();

			if (excludeItem != "Gallery") {
				myList.Add (new SatelliteMenuButtonItem (UIImage.FromBundle ("frames4832.png"), counter, "Gallery"));
				counter++;
			}

			if (excludeItem != "Dashboard") {
				myList.Add (new SatelliteMenuButtonItem (UIImage.FromBundle ("math.png"), counter, "Dashboard"));
				counter++;
			}




			myList.Add (new SatelliteMenuButtonItem (UIImage.FromBundle ("scanner4832.png"), counter, "Scanner"));
			counter++;
			myList.Add (new SatelliteMenuButtonItem (Flosshatt.MakeFlosshatt (), counter, "Items"));
			counter++;
			myList.Add (new SatelliteMenuButtonItem (UIImage.FromFile ("table4832.png"), counter, "Big Items"));
			counter++;
			myList.Add (new SatelliteMenuButtonItem (UIImage.FromFile ("container4832.png"), counter, "Containers"));
			counter++;
			myList.Add (new SatelliteMenuButtonItem (UIImage.FromFile ("preferences4832.png"), counter, "Preferences"));
			counter++;
			myList.Add (new SatelliteMenuButtonItem (UIImage.FromBundle ("uchi4832.png"), counter, "Locations"));
			
			return myList.ToArray ();
		}

		public void InitSateliteMenu(){

			var image = UIImage.FromFile ("menu.png");
			var yPos = View.Frame.Height - image.Size.Height - 10;
			var frame = new RectangleF (10, yPos, image.Size.Width, image.Size.Height);

			var items = GetMenuItems ();


			SateliteButton = new SatelliteMenuButton (View, image, items, frame);

			SateliteButton.MenuItemClick += (_, args) => {
				Console.WriteLine ("{0} was clicked!", args.MenuItem.Name);

				if(args.MenuItem.Name == "Scanner"){
					Scanner scanner = new Scanner(this);
					scanner.Scannit();
				}
				if(args.MenuItem.Name == "Items"){
					if(UserInterfaceIdiomIsPhone){
						ItemCatalogue cat = new ItemCatalogue();
						//PresentViewControllerAsync(cat, true);
						ancestor.NavigationController.PushViewController(cat,true);
					}else{
						ItemMasterView itemMaster = new ItemMasterView();
						ancestor.NavigationController.PushViewController(itemMaster,true);
					}
				}
				if(args.MenuItem.Name == "Big Items"){
					if(UserInterfaceIdiomIsPhone){
						BigItemsScreen biggies = new BigItemsScreen();
						ancestor.NavigationController.PushViewController(biggies, true);
					}else{
						BigItemMasterView bigMaster = new BigItemMasterView();
						ancestor.NavigationController.PushViewController(bigMaster, true);
					}
				}
				if(args.MenuItem.Name == "Containers"){
					if(UserInterfaceIdiomIsPhone){
						ContainerScreen containers = new ContainerScreen();
						ancestor.NavigationController.PushViewController(containers,true);
					}else{
						ContainerMasterView containerMaster = new ContainerMasterView();
						ancestor.NavigationController.PushViewController(containerMaster, true);
					}
				}
				if(args.MenuItem.Name == "Preferences"){
					Preferences pref = new Preferences();
					ancestor.NavigationController.PushViewController(pref,true);
				}

				if(args.MenuItem.Name == "Gallery"){
					GalleryViewController tagGallery = new GalleryViewController();
					ancestor.NavigationController.PushViewController(tagGallery,true);
				}

				if(args.MenuItem.Name == "Locations"){
					StorageCatalogue sc = new StorageCatalogue();
					ancestor.NavigationController.PushViewController(sc,true);
				}

				if(args.MenuItem.Name == "Dashboard"){
					DashBoardViewController dash = new DashBoardViewController();
					ancestor.NavigationController.PushViewController(dash,true);
				}


			};
			this.View.BackgroundColor = UIColor.Clear;
			View.AddSubview (SateliteButton);
		}

	}
}

