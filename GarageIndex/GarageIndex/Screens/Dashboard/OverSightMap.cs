using System;
using MonoTouch.UIKit;
using GoogleAnalytics.iOS;
using SatelliteMenu;
using System.Drawing;
using MonoTouch.MapKit;
using MonoTouch.CoreLocation;

namespace GarageIndex
{
	public class OverSightMap : UIViewController
	{
		MKMapView mapView;
		RectangleF myFrame;

		public OverSightMap (RectangleF myFrame)
		{
			this.myFrame = myFrame;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.View.BackgroundColor = UIColor.Orange;
			this.View.Frame = myFrame;
			mapView = new MKMapView (View.Bounds);
			mapView.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
			View.AddSubview(mapView);

			CLLocationCoordinate2D coords = new CLLocationCoordinate2D(48.857, 2.351);
			MKCoordinateSpan span = new MKCoordinateSpan(KilometresToLatitudeDegrees(20), KilometresToLongitudeDegrees(20, coords.Latitude));
			mapView.Region = new MKCoordinateRegion(coords, span);

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

