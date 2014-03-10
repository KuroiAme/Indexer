using System;
using MonoTouch.UIKit;
using GoogleAnalytics.iOS;
using SatelliteMenu;
using System.Drawing;
using MonoTouch.MapKit;
using MonoTouch.CoreLocation;
using System.Collections.Generic;
using no.dctapps.Garageindex.model;
using MonoTouch.Foundation;
using System.IO;
using no.dctapps.Garageindex.screens;

namespace GarageIndex
{
	public class OverSightMap : UIViewController
	{
		MKMapView mapView;
		readonly RectangleF myFrame;
		
		UIViewController ancestor;

		public OverSightMap (RectangleF myFrame, UIViewController ancestor)
		{
			this.myFrame = myFrame;
			this.ancestor = ancestor;
		}

		public override void LoadView ()
		{
			base.LoadView ();
			this.View.BackgroundColor = UIColor.Orange;
			this.View.Frame = myFrame;
		}

		protected override void Dispose (bool disposing)
		{
			ancestor = null;
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

		MapDelegate mappy;

		public void ReloadData ()
		{
			if (annotationList != null) {
				mapView.RemoveAnnotations (annotationList.ToArray());
			}

			GetMinsAndMaxes ();
			SetMapViewOversight ();
			AnnotateMap ();
		}

		TheStorageScreen tss;

		Lager selected;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			mapView = new MKMapView (View.Bounds);
			mapView.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
			mappy = new MapDelegate ();
			mappy.LagerAnnotationClicked += (object sender, LagerAnnotationClicked e) => {
				selected = AppDelegate.dao.getLagerByName (e.name);
				if(selected != null){
					tss = new TheStorageScreen (selected);
//					UINavigationController nc = new UINavigationController();
//					nc.PushViewController(tss,false);
//					ancestor.PresentViewController(nc,true,null);
					ancestor.NavigationController.PushViewController(tss,false);

					//PresentViewController(tss,true,null);
				}
			};
			mapView.Delegate = mappy;
			View.AddSubview (mapView);

//			CLLocationCoordinate2D coords = new CLLocationCoordinate2D(48.857, 2.351);
//			MKCoordinateSpan span = new MKCoordinateSpan(KilometresToLatitudeDegrees(20), KilometresToLongitudeDegrees(20, coords.Latitude));
//			mapView.Region = new MKCoordinateRegion(coords, span);

			GetMinsAndMaxes ();
			SetMapViewOversight ();
			AnnotateMap ();

		}

		List<BasicMapAnnotation> annotationList;

		public void AnnotateSingleLocation (Lager myLager)
		{
//			MKPointAnnotation LagerAnnotation;
//			if (myLager.Name != null) {
//				LagerAnnotation = new MKPointAnnotation () {
//					Title = myLager.Name,
//					Coordinate = new CLLocationCoordinate2D (myLager.latitude, myLager.longitude),
//				};
//
//				mapView.AddAnnotation (LagerAnnotation);
//			}
			var annotation = new BasicMapAnnotation (myLager);
			mapView.AddAnnotation (annotation);
			annotationList.Add (annotation);

		}

		public void AnnotateMap ()
		{
			annotationList = new List<BasicMapAnnotation> ();
			foreach (Lager thisLager in lagers) {
				AnnotateSingleLocation (thisLager);
			}
		}

		/// <summary>Converts kilometres to longitudinal degrees at a specified latitude</summary>
		public double KilometresToLongitudeDegrees (double kms, double atLatitude)
		{
			double earthRadius = 6371.0; // in kms
			double degreesToRadians = Math.PI / 180.0;
			double radiansToDegrees = 180.0 / Math.PI;
			// derive the earth's radius at that point in latitude
			double radiusAtLatitude = earthRadius * Math.Cos (atLatitude * degreesToRadians);
			return (kms / radiusAtLatitude) * radiansToDegrees;
		}

		/// <summary>Converts kilometres to latitude degrees</summary>
		public double KilometresToLatitudeDegrees (double kms)
		{
			double earthRadius = 6371.0; // in kms
			double radiansToDegrees = 180.0 / Math.PI;
			return (kms / earthRadius) * radiansToDegrees;
		}

		double MinLatitude = 99999999999;
		double MaxLatitude = 0;
		double MinLongitude = 999999999999;
		double MaxLongitude = 0;
		IList<Lager> lagers;
		private Boolean empty = false;

		public void GetMinsAndMaxes ()
		{
			lagers = AppDelegate.dao.GetAllLagers ();
			if (lagers.Count == 0) {
				empty = true;
			} else {
				foreach (Lager myLager in lagers) {

					double latitude = myLager.latitude;
					double longitude = myLager.longitude;

					if (latitude < MinLatitude) {
						MinLatitude = latitude;
					}

					if (latitude > MaxLatitude) {
						MaxLatitude = latitude;
					}

					if (longitude < MinLongitude) {
						MinLongitude = longitude;
					}

					if (longitude > MaxLongitude) {
						MaxLongitude = longitude;
					}
				}
			}
		}

		public void SetMapViewOversight ()
		{
			if (!empty) {
//		// pad our map by 10% around the farthest annotations
//		#define MAP_PADDING 1.1
				const float MAP_PADDING = 1.6f;
//
//		// we'll make sure that our minimum vertical span is about a kilometer
//		// there are ~111km to a degree of latitude. regionThatFits will take care of
//		// longitude, which is more complicated, anyway. 
//		#define MINIMUM_VISIBLE_LATITUDE 0.01
				const float MINIMUM_VISIBLE_LATITUDE = 0.01f;
				MKCoordinateRegion region = new MKCoordinateRegion ();
				double LatitudeCenter = (this.MinLatitude + this.MaxLatitude) / 2;
				double LongitudeCenter = (this.MinLongitude + this.MaxLongitude) / 2;
				region.Center = new CLLocationCoordinate2D (LatitudeCenter, LongitudeCenter);
				region.Span.LatitudeDelta = (MaxLatitude - MinLatitude) * MAP_PADDING;
				if (region.Span.LatitudeDelta < MINIMUM_VISIBLE_LATITUDE) {
					region.Span.LatitudeDelta = MINIMUM_VISIBLE_LATITUDE;
				}
				region.Span.LongitudeDelta = (MaxLongitude - MinLongitude) * MAP_PADDING;
				MKCoordinateRegion scaledRegion = mapView.RegionThatFits (region);
				mapView.SetRegion (scaledRegion, true);
			}
		}
	}

	class MapDelegate : MKMapViewDelegate
	{
		protected string annotationIdentifier = "BasicAnnotation";
		UIButton detailButton;
		public event EventHandler<LagerAnnotationClicked> LagerAnnotationClicked;
		// avoid GC
		public override MKAnnotationView GetViewForAnnotation (MKMapView mapView, NSObject annotation)
		{
			// try and dequeue the annotation view
			MKAnnotationView annotationView = mapView.DequeueReusableAnnotation (annotationIdentifier);   
			// if we couldn't dequeue one, create a new one
			if (annotationView == null)
				annotationView = new MKPinAnnotationView (annotation, annotationIdentifier);
			else // if we did dequeue one for reuse, assign the annotation to it
				annotationView.Annotation = annotation;

			// configure our annotation view properties
			annotationView.CanShowCallout = true;
			(annotationView as MKPinAnnotationView).AnimatesDrop = true;
			(annotationView as MKPinAnnotationView).PinColor = MKPinAnnotationColor.Green;
			annotationView.Selected = true;

			// you can add an accessory view; in this case, a button on the right and an image on the left
			detailButton = UIButton.FromType (UIButtonType.DetailDisclosure);
			detailButton.TouchUpInside += (s, e) => {
				Console.WriteLine ("Clicked");
				var handler = LagerAnnotationClicked;
				if(handler != null){
					handler(this,new LagerAnnotationClicked((annotation as MKAnnotation).Title));
				}

//				new UIAlertView ("Annotation Clicked", "You clicked on " +
//				(annotation as MKAnnotation).Coordinate.Latitude.ToString () + ", " +
//				(annotation as MKAnnotation).Coordinate.Longitude.ToString (), null, "OK", null).Show ();
			};

			annotationView.RightCalloutAccessoryView = detailButton;

			var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			string file = (annotation as BasicMapAnnotation).MyLager.thumbFileName;
			if (file != null) {
					string filename = System.IO.Path.Combine (documentsDirectory, file);
				bool exists = true;
				UIImage image = null;
				if (File.Exists (filename)) {
					image = UIImage.FromFile (filename);
				} else {
					exists = false;
				}
				if (exists) {
					annotationView.LeftCalloutAccessoryView = new UIImageView (image);
				}
			}

			return annotationView;
		}
		// as an optimization, you should override this method to add or remove annotations as the
		// map zooms in or out.
		public override void RegionChanged (MKMapView mapView, bool animated)
		{
		}
	}

	class BasicMapAnnotation : MKAnnotation
	{
		/// <summary>
		/// The location of the annotation
		/// </summary>
		public override CLLocationCoordinate2D Coordinate { get; set; }

		protected string title;
		protected string subtitle;

		/// <summary>
		/// The title text
		/// </summary>
		public override string Title
		{ get { return title; } }

		/// <summary>
		/// The subtitle text
		/// </summary>
		public override string Subtitle
		{ get { return subtitle; } }

//		public string ImageFilename{ get; }
//
		public Lager MyLager{ get; set; }

		public BasicMapAnnotation (Lager myLager)
			: base ()
		{
			this.Coordinate = new CLLocationCoordinate2D (myLager.latitude, myLager.longitude);
			this.title = myLager.Name;
			this.subtitle = AppDelegate.its.getTranslatedText ("location");
			this.MyLager = myLager;
		}
	}
}

