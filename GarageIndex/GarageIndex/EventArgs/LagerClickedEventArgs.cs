
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using no.dctapps.Garageindex.model;

namespace no.dctapps.Garageindex.events
{

	public class LagerClickedEventArgs : EventArgs
	{
		public Lager Lager;
		public LagerClickedEventArgs(Lager lager){
			this.Lager = lager;
		}
	}


}

