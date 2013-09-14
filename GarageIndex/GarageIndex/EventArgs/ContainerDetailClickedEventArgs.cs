using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.model;
using no.dctapps.Garageindex.events;
using No.Dctapps.GarageIndex;

namespace no.dctapps.Garageindex.events
{
	public class ContainerDetailClickedEventArgs : EventArgs
	{
		public Item item{get; set;}

		public ContainerDetailClickedEventArgs(Item item) : base()
		{
			this.item = item;
		}
	}
}