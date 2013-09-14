
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace no.dctapps.Garageindex.events
{
	public class GotPictureEventArgs : EventArgs
	{
		public UIImage image;

		public GotPictureEventArgs(UIImage image){
			this.image = image;
		}
	}

}

