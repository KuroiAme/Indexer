using System;
using MonoTouch.UIKit;

namespace no.dctapps.commons.events
{
	public class GotPictureEventArgs : EventArgs
	{
		public UIImage image;

		public GotPictureEventArgs(UIImage image){
			this.image = image;
		}
	}

}

