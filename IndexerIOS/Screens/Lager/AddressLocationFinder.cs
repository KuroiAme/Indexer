using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.MapKit;
using System.Collections.Generic;
using MonoTouch.CoreLocation;
using MonoTouch.Foundation;

namespace IndexerIOS
{
	public class AddressLocationFinder : UIViewController
	{
		StorageScreenContent storageScreenContent;

		public AddressLocationFinder (StorageScreenContent storageScreenContent)
		{
			this.storageScreenContent = storageScreenContent;
		}


		UISearchBar searchBar;
		UISearchDisplayController searchController;
		MKMapView map;
		public event EventHandler<CoordEventArgs> FoundCoords;

		protected override void Dispose (bool disposing)
		{
			storageScreenContent.Dispose ();
			searchBar.Dispose ();
			searchController.Dispose ();
			map.Dispose ();
			FoundCoords = null;
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

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			FoundCoords += (object sender, CoordEventArgs e) => {
				storageScreenContent.SetCoords(e.Latitude,e.Longitude);
			};

			map = new MKMapView (UIScreen.MainScreen.Bounds);
			View = map;


			// create search controller
			searchBar = new UISearchBar (new RectangleF (0, 0, View.Frame.Width, 50)) {
				Placeholder = "Enter a search query"
			};
			searchController = new UISearchDisplayController (searchBar, this);
			searchController.Delegate = new SearchDelegate (map);
			SearchSource source = new SearchSource (searchController, map);
			searchController.SearchResultsSource = source;
			source.FoundCoords += (object sender, CoordEventArgs e) => {
				var handler = FoundCoords;
				if(handler != null){
					handler(this,e);
				}
				this.DismissViewController(true,null);
			};
			View.AddSubview (searchBar);
		}

	}

	class SearchSource : UITableViewSource
	{
		static readonly string mapItemCellId = "mapItemCellId";
		UISearchDisplayController searchController;
		MKMapView map;
		public event EventHandler<CoordEventArgs> FoundCoords;

		public List<MKMapItem> MapItems { get; set; }

		public SearchSource (UISearchDisplayController searchController, MKMapView map)
		{
			this.searchController = searchController;
			this.map = map;

			MapItems = new List<MKMapItem> ();
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return MapItems.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (mapItemCellId);

			if (cell == null)
				cell = new UITableViewCell ();

			cell.TextLabel.Text = MapItems [indexPath.Row].Name;
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			searchController.SetActive (false, true);

			// add item to map
			CLLocationCoordinate2D coord = MapItems [indexPath.Row].Placemark.Location.Coordinate;
			map.AddAnnotation (new MKPointAnnotation () {
				Title = MapItems [indexPath.Row].Name, 
				Coordinate = coord
			});

			map.SetCenterCoordinate (coord, true);

			RaiseFoundCoords (coord);
		}

		void RaiseFoundCoords (CLLocationCoordinate2D coord)
		{
			var handler = this.FoundCoords;
			if (handler != null) {
				handler(this,new CoordEventArgs(coord.Longitude, coord.Latitude));
			}
		}
	}

	class SearchDelegate : UISearchDisplayDelegate
	{
		MKMapView map;

		public SearchDelegate (MKMapView map)
		{
			this.map = map;
		}

		List<MKMapItem> GetList (MKMapItem[] mapItems)
		{
			List<MKMapItem> items = new List<MKMapItem> ();
			foreach (MKMapItem item in mapItems) {
				items.Add (item);
			}
			return items;
		}

		public override bool ShouldReloadForSearchString (UISearchDisplayController controller, string forSearchString)
		{
			// create search request
			var searchRequest = new MKLocalSearchRequest ();
			searchRequest.NaturalLanguageQuery = forSearchString;
			searchRequest.Region = new MKCoordinateRegion (map.UserLocation.Coordinate, new MKCoordinateSpan (0.25, 0.25));

			// perform search
			var localSearch = new MKLocalSearch (searchRequest);
			localSearch.Start (delegate (MKLocalSearchResponse response, NSError error) {
				if (response != null && error == null) {
					((SearchSource)controller.SearchResultsSource).MapItems = GetList(response.MapItems);
					controller.SearchResultsTableView.ReloadData();
				} else {
					Console.WriteLine ("local search error: {0}", error);
				}
			});

			return true;
		}
	}
}

