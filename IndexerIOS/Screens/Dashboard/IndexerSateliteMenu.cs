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

		public void InitSateliteMenu(){

			var image = UIImage.FromFile ("menu.png");
			var yPos = View.Frame.Height - image.Size.Height - 10;
			var frame = new RectangleF (10, yPos, image.Size.Width, image.Size.Height);

			var items = new [] { 
				new SatelliteMenuButtonItem (UIImage.FromBundle ("scanner4832.png"), 1, "Scanner"),
				new SatelliteMenuButtonItem (Flosshatt.MakeFlosshatt(), 2, "Items"),
				new SatelliteMenuButtonItem (UIImage.FromFile ("table4832.png"), 3, "Big Items"),
				new SatelliteMenuButtonItem (UIImage.FromFile ("container4832.png"), 4, "Containers"),
				new SatelliteMenuButtonItem (UIImage.FromFile ("preferences4832.png"), 5, "Preferences"),
				new SatelliteMenuButtonItem (UIImage.FromBundle("frames4832.png"), 6, "Gallery"),
				new SatelliteMenuButtonItem (UIImage.FromBundle("uchi4832.png"), 7, "Locations")
			};

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


			};
			this.View.BackgroundColor = UIColor.Clear;
			View.AddSubview (SateliteButton);
		}

	}
}

