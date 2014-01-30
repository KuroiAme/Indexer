using System;
using MonoTouch.UIKit;
using iCarouselSDK;
using System.Drawing;
using System.Collections.Generic;
using no.dctapps.Garageindex.events;
using MonoTouch.Foundation;
using System.IO;
using System.Linq;
using SQLite;

namespace GarageIndex
{
	public class GalleryObject
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public string Name{get; set;}
		public string imageFileName{ get; set;}

		public string thumbFileName {
			get;
			set;
		}
	}
}

