using System;
using MonoTouch.UIKit;
using iCarouselSDK;
using System.Drawing;
using System.Collections.Generic;
using no.dctapps.Garageindex.events;
using MonoTouch.Foundation;
using System.IO;
using System.Linq;
using MonoTouch.CoreGraphics;
using MonoTouch.ObjCRuntime;
using SQLite;

namespace GarageIndex
{
	public class ImageTag
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public string TagString{get; set;}

		//public RectangleF Spot{get; set;}
		public int GalleryObjectID{ get; set;}
		public float x{ get; set;}
		public float y{ get; set;}
		public float width { get; set;}
		public float height{ get; set;}


		public RectangleF FetchAsRectangleF(){
			RectangleF myF = new RectangleF (x, y, width, height);
			return myF;
		}

		public void StoreRectangleF(RectangleF miffed){
			x = miffed.X;
			y = miffed.Y;
			width = miffed.Width;
			height = miffed.Height;
		}

	}
}

