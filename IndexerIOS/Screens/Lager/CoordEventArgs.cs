using System;
using MonoTouch.UIKit;
using System.Drawing;
using no.dctapps.Garageindex.model;
using SlideDownMenu;
using MonoTouch.MessageUI;
using MonoTouch.Foundation;
using MonoTouch.MapKit;
using GarageIndex;
using MonoTouch.CoreLocation;
using System.Collections.Generic;

namespace IndexerIOS
{
	public class CoordEventArgs : EventArgs
	{
		public double Latitude;
		public double Longitude;
		public CoordEventArgs (double longitude, double latitude)
		{
			this.Latitude = latitude;
			this.Longitude = longitude;
		}
	}
}

