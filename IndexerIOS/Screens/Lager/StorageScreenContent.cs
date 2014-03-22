using System;
using MonoTouch.UIKit;
using System.Drawing;
using no.dctapps.Garageindex.model;
using MonoTouch.MessageUI;
using MonoTouch.Foundation;
using MonoTouch.MapKit;
using GarageIndex;
using MonoTouch.CoreLocation;
using System.Collections.Generic;

namespace IndexerIOS
{
	public class StorageScreenContent : UIViewController
	{
		readonly RectangleF myRect;
		Lager myLager;
		MKMapView mapView;
		MFMailComposeViewController mailContr;
		readonly UIScrollView outerScroll;

		GalleryViewController gvc;
		
		UIViewController ancestor;


		protected override void Dispose (bool disposing)
		{
			this.ancestor = null;
			outerScroll.Dispose ();
			mailContr.Dispose ();
			mapView.Dispose ();
			base.Dispose (disposing);
		}

		/// <summary>
		/// Release everything not in use
		/// </summary>
		void cleanup ()
		{
			Dispose ();
		}


		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			//cleanup only if view is loaded and not in a window.
			if(this.IsViewLoaded && this.View.Window == null){
				//cleanup ();
			}
			// Release any cached data, images, etc that aren't in use.
		}

		public StorageScreenContent (RectangleF myRect, Lager myLager, UIScrollView outerScroll, UIViewController outer)
		{
			this.outerScroll = outerScroll;
			this.myRect = myRect;
			this.myLager = myLager;
			this.ancestor = outer;
		}

		public override void LoadView ()
		{
			base.LoadView ();
			this.View.Frame = myRect;
		}

		const float topmargin = 80;
		const float margin = 10;
		float x = margin;
		float y = topmargin;
		float mywidth;
		const float myHeight = 30;
		const float cube = 300;

		public override void ViewDidLoad ()
		{
			mywidth = this.View.Bounds.Width - margin * 2;
			base.ViewDidLoad ();



			AddNameTextField ();
			AddLocationBox ();
			AddPicture ();

			UIBarButtonItem email = CreateEmailButton ();
			UIBarButtonItem galleryLink = CreateGalleryLink ();
			UIBarButtonItem[] items = { galleryLink, email };
			ancestor.NavigationItem.SetRightBarButtonItems (items, true);

		}

		UIBarButtonItem CreateGalleryLink ()
		{
			UIBarButtonItem GalleryLink = new UIBarButtonItem (UIBarButtonSystemItem.Organize, null);
			GalleryLink.Clicked += (object sender, EventArgs e) => OpenGalleryWithThisActive ();
			return GalleryLink;
		}

		UIBarButtonItem CreateEmailButton ()
		{
			UIBarButtonItem email = new UIBarButtonItem (Letter.MakeLetter (), UIBarButtonItemStyle.Plain, null);
			email.Clicked += (object sender, EventArgs e) => MakeEmail ();
			return email;
		}

		void OpenGalleryWithThisActive ()
		{
			gvc = new GalleryViewController (myLager);
			ancestor.NavigationController.PushViewController (gvc,true);
		}

		UITextField NameField;

		void AddNameTextField ()
		{
			RectangleF NameTextFieldRect = new RectangleF (x, y, mywidth, myHeight);
			NameField = new UITextField (NameTextFieldRect);
			NameField.BorderStyle = UITextBorderStyle.RoundedRect;
			NameField.Placeholder = NSBundle.MainBundle.LocalizedString ("Name of the Location", "Name of the location");
			Add (NameField);
			y += myHeight + margin;

			this.NameField.ShouldReturn += textField =>  {
				textField.ResignFirstResponder ();
				return true;
			};

			NameField.EditingDidEnd += (object sender, EventArgs e) => {
				myLager.Name = NameField.Text;
				AppDelegate.dao.SaveLager(myLager);
			};
		}

//		UISearchBar searchBar;
//		UISearchDisplayController searchController;
//		const float searchBarHeight = 50;
//
//		void AddLocationSearchBar ()
//		{
//			searchBar = new UISearchBar (new RectangleF (x, y, mywidth, searchBarHeight)) {
//				Placeholder = "Enter a search query"
//			};
//			searchController = new UISearchDisplayController (searchBar, this);
//			searchController.Delegate = new SearchDelegate (mapView);
//			SearchSource source = new SearchSource (searchController, mapView);
//			source.FoundCoords += (object sender, CoordEventArgs e) => {
//				myLager.longitude = e.Longitude;
//				myLager.latitude = e.Latitude;
//				AppDelegate.dao.SaveLager(myLager);
//				CLLocationCoordinate2D coords = new CLLocationCoordinate2D (myLager.latitude, myLager.longitude);
//				MKCoordinateSpan span = new MKCoordinateSpan (KilometresToLatitudeDegrees (10), KilometresToLongitudeDegrees (10, coords.Latitude));
//				mapView.Region = new MKCoordinateRegion (coords, span);
//				AnnotateMapWithLagerName(this.myLager);
//			};
//			searchController.SearchResultsSource = source;
//			View.AddSubview (searchBar);
//
//			y += searchBarHeight;
//		}

		RectangleF MapViewBoxRect;

		SwipeDelegate dtdelegate;

		UITapGestureRecognizer doubletap;

		void AddLocationBox (){
			MapViewBoxRect = new RectangleF (x, y, mywidth, cube);
			mapView = new MKMapView (MapViewBoxRect);
			mapView.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
			mapView.ShowsUserLocation = true;
			View.AddSubview(mapView);

			if (Double.IsNaN (this.myLager.latitude)) {
				SetMapToUserLocation ();
			}

			doubletap = new UITapGestureRecognizer (AddLocation);
			doubletap.NumberOfTapsRequired = 2;
			this.dtdelegate = new SwipeDelegate ();
			doubletap.Delegate = dtdelegate;

			mapView.AddGestureRecognizer (doubletap);

			y += cube;
		}

		public class SwipeDelegate : UIGestureRecognizerDelegate
		{
			public override bool ShouldRecognizeSimultaneously (UIGestureRecognizer gestureRecognizer, UIGestureRecognizer otherGestureRecognizer)
			{
				return true;
			}
		}

		ImagePanel image;

		public static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		void AddPicture ()
		{
			if (UserInterfaceIdiomIsPhone) {
				image = new ImagePanel (new RectangleF (x, y, cube, cube), this.ancestor);
			} else {
				image = new ImagePanel (new RectangleF (x, y + 10, UIScreen.MainScreen.Bounds.Width - 20, cube * 2 - 50), this.ancestor);
			}

			image.ImageSaved += (object sender, SavedImageStringsEventArgs e) => {
				myLager.thumbFileName = e.Thumbfilename;
				myLager.LagerImage = e.imageFilename;
				AppDelegate.dao.SaveLager(myLager);
			};

			image.ImageDeleted += (object sender, EventArgs e) => {
				myLager.thumbFileName = null;
				myLager.LagerImage = null;
				AppDelegate.dao.SaveLager(myLager);
			};

			y += cube;
			Add (image.View);
		}

		private void AddLocation (UIGestureRecognizer gest){
			string[] other = { AppDelegate.its.getTranslatedText("from current position"), AppDelegate.its.getTranslatedText("from address")};
			UIActionSheet inquire = new UIActionSheet (AppDelegate.its.getTranslatedText("Location from"),
														null,
														AppDelegate.its.getTranslatedText("cancel"),
														AppDelegate.its.getTranslatedText("delete current location"),
														other);

		
			inquire.Clicked += (object sender, UIButtonEventArgs e) => {
				if(e.ButtonIndex == 3){
					Console.WriteLine("cancel:"+e.ButtonIndex);
					//CANCEL
				}
				if(e.ButtonIndex == 0){
					Console.WriteLine("delete:"+e.ButtonIndex);
//					myLager.longitude = double.NaN;
//					myLager.latitude = double.NaN;
//					AppDelegate.dao.SaveLager(myLager);
//
					//DEFAULT TO USERS LOCATION;
					SetMapToUserLocation();
				}
				if(e.ButtonIndex == 1){
					Console.WriteLine("UserLocation:"+e.ButtonIndex);
					SetMapToUserLocation ();
				}

				if(e.ButtonIndex == 2){
					Console.WriteLine("Address:"+e.ButtonIndex);
					AddressLocationFinder arfl = new AddressLocationFinder (this);
					ancestor.PresentViewController (arfl, true, null);
				}

			};

			inquire.ShowInView (ancestor.View);

		}

		void SetMapToUserLocation ()
		{
			var uloc = mapView.UserLocation;
			myLager.longitude = uloc.Coordinate.Longitude;
			myLager.latitude = uloc.Coordinate.Latitude;
			AppDelegate.dao.SaveLager (myLager);
			CLLocationCoordinate2D coords = new CLLocationCoordinate2D (myLager.latitude, myLager.longitude);
			MKCoordinateSpan span = new MKCoordinateSpan (KilometresToLatitudeDegrees (10), KilometresToLongitudeDegrees (10, coords.Latitude));
			mapView.Region = new MKCoordinateRegion (coords, span);
			if (LagerAnnotation != null) {
				mapView.RemoveAnnotation (LagerAnnotation);
			}
			AnnotateMapWithLagerName (this.myLager, myLager.latitude, myLager.latitude);
			mapView.ReloadInputViews ();
			mapView.SetNeedsDisplay ();
		}

		public void SetCoords (double latitude, double longitude)
		{
			Console.WriteLine("found geolocation");
			CLLocationCoordinate2D coord = new CLLocationCoordinate2D(latitude, longitude);
			myLager.longitude = coord.Longitude;
			myLager.latitude = coord.Latitude;
			AppDelegate.dao.SaveLager(myLager);
			MKCoordinateSpan span = new MKCoordinateSpan (KilometresToLatitudeDegrees (10), KilometresToLongitudeDegrees (10, coord.Latitude));
			mapView.Region = new MKCoordinateRegion (coord, span);
			if (LagerAnnotation != null) {
				mapView.RemoveAnnotation (LagerAnnotation); 
			}
			AnnotateMapWithLagerName(this.myLager,latitude,longitude);
			mapView.ReloadInputViews ();
		}			

		static bool haveStoredLocation (Lager myLager)
		{
			return !double.IsNaN (myLager.longitude) && !double.IsNaN (myLager.latitude);
		}

		MKPointAnnotation LagerAnnotation;

		void AnnotateMapWithLagerName(Lager myLager,double latitude, double longitude)
		{

			if (myLager.Name != null) {
				LagerAnnotation = new MKPointAnnotation () {
					Title = myLager.Name,
					Coordinate = new CLLocationCoordinate2D (latitude, longitude),
				};

				mapView.AddAnnotation (LagerAnnotation);
			}
		}

		void ResetLocation ()
		{
			myLager.latitude = 48.857;
			myLager.longitude = 2.351;
		}

		void SetLocationFromLager (Lager myLager)
		{
			//ResetLocation ();

			try{
				CLLocationCoordinate2D coords = new CLLocationCoordinate2D (myLager.latitude, myLager.longitude);
				MKCoordinateSpan span = new MKCoordinateSpan (KilometresToLatitudeDegrees (10), KilometresToLongitudeDegrees (10, coords.Latitude));
				mapView.Region = new MKCoordinateRegion (coords, span);
				AnnotateMapWithLagerName (myLager,myLager.latitude,myLager.longitude);
				}catch(Exception e){
				//invalid coords, delete;
				myLager.latitude = double.NaN;
				myLager.longitude = double.NaN;
				AppDelegate.dao.SaveLager (myLager);
			}
		}





		public void ShowDetails (Lager myLager)
		{
			this.myLager = myLager;

			if (myLager != null) {
				NameField.Text = myLager.Name;

				if (haveStoredLocation (myLager)) {
					SetLocationFromLager (myLager);
				}

				if (myLager.LagerImage != null) {
					image.SetNewImageName (myLager.LagerImage);
				}



//				address.Text = myLager.address;
//				keyContact.Text = myLager.telephone;
//				x.Text = myLager.height.ToString ();
//				y.Text = myLager.width.ToString ();
//				z.Text = myLager.depth.ToString ();
//				this.poststedField.Text = myLager.poststed;
//				this.zipField.Text = myLager.postnr;
				this.CreateEmailButton ();
			}
		}

//		void CreateSlideDownMenu ()
//		{
//			var item0 = new MenuItem ("Options", UIImage.FromBundle ("frames4832.png"), (menuItem) => {
//				Console.WriteLine("Item: {0}", menuItem);
//			});
//			item0.Tag = 0;
//			var email = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString("Email", "Email");
//			var item1 = new MenuItem (email, UIImage.FromBundle ("startree.png"), (menuItem) => {
//				Console.WriteLine("Item: {0}", menuItem);
//				MakeEmail();
//
//			});
//			item1.Tag = 1;
//			var item2 = new MenuItem ("Dismiss", UIImage.FromBundle ("frames4832.png"), (menuItem) => {
//				Console.WriteLine("Item: {0}", menuItem);
//				outer.DismissViewController(false,null);
//			});
//			item2.Tag = 2;
//
//
//			//item0.tag = 0;
//
//			var slideMenu = new SlideMenu (new List<MenuItem> { item0, item1, item2});
//			slideMenu.Center = new PointF (slideMenu.Center.X, slideMenu.Center.Y + 25);
//			this.View.AddSubview (slideMenu);
//		}

		void MakeEmail ()
		{
			mailContr = new MFMailComposeViewController ();
			mailContr.SetSubject (AppDelegate.bl.GenerateSubject (myLager));
			mailContr.SetMessageBody (AppDelegate.bl.GenerateManifest (myLager), false);
			ancestor.NavigationController.PresentViewController (mailContr, true, null);
			mailContr.Finished += (object sender2, MFComposeResultEventArgs e2) => mailContr.DismissViewController (true, delegate {
			});
		}

		/// <summary>Converts kilometres to longitudinal degrees at a specified latitude</summary>
		public double KilometresToLongitudeDegrees(double kms, double atLatitude)
		{
			double earthRadius = 6371.0; // in kms
			double degreesToRadians = Math.PI/180.0;
			double radiansToDegrees = 180.0/Math.PI;
			// derive the earth's radius at that point in latitude
			double radiusAtLatitude = earthRadius * Math.Cos(atLatitude * degreesToRadians);
			return (kms / radiusAtLatitude) * radiansToDegrees;
		}

		/// <summary>Converts kilometres to latitude degrees</summary>
		public double KilometresToLatitudeDegrees(double kms)
		{
			double earthRadius = 6371.0; // in kms
			double radiansToDegrees = 180.0/Math.PI;
			return (kms/earthRadius) * radiansToDegrees;
		}


	}




}

