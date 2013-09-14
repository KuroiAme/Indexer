
using System;
using System.Collections.Generic;

using System.Drawing;
using MonoTouch.Foundation;

using MonoTouch.UIKit;

using no.dctapps.Garageindex.model;
using no.dctapps.Garageindex.events;

namespace no.dctapps.Garageindex.events
{
	public class ContainerClickedEventArgs : EventArgs
	{
		public LagerObject container{get; set;}

		public ContainerClickedEventArgs(LagerObject container) : base()
		{
			this.container = container;
		}
	}

}