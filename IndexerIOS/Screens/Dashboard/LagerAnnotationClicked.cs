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

namespace GarageIndex
{
	public class LagerAnnotationClicked
	{
		public string name;
		public LagerAnnotationClicked (string name)
		{
			this.name = name;
		}
	}
}

