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
using GoogleAnalytics.iOS;
using no.dctapps.Garageindex;

namespace GarageIndex
{

	class TagClickedEventArgs : EventArgs
	{
		public ImageTag tag{ get; set; }
		public TagClickedEventArgs(ImageTag tag) : base(){
			this.tag = tag;
		} 
	}
}