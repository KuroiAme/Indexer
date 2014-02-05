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


		public float ax{ get; set;}
		public float ay{ get; set;}

		public float bx{ get; set;}
		public float by{ get; set;}

		public float cx{ get; set;}
		public float cy{ get; set;}

		public float dx{ get; set;}
		public float dy{ get; set;}



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

		public void StorePoints (PointF a, PointF b, PointF c, PointF d)
		{
			ax = a.X;
			ay = a.Y;

			ax = a.X;
			ay = a.Y;

			ax = a.X;
			ay = a.Y;

			ax = a.X;
			ay = a.Y;

		}

		public CGPath FetchAsPath ()
		{
			try{
			PointF[] pointarray = {
									new PointF(ax,ay),
									new PointF(bx,by),
									new PointF(cx,cy),
									new PointF(dx,dy),
									};

			CGPath path = new CGPath();
			path.AddLines(pointarray);
			path.CloseSubpath();
			return path;
			}catch(Exception ex){
				return null;
			}
		}
	}
}

