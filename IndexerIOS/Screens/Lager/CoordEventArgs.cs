using System;
using MonoTouch.UIKit;
using System.Drawing;
using no.dctapps.commons.events.model;
using MonoTouch.MessageUI;
using MonoTouch.Foundation;
using MonoTouch.MapKit;
using no.dctapps.commons.events;
using MonoTouch.CoreLocation;
using System.Collections.Generic;

namespace no.dctapps.commons
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

