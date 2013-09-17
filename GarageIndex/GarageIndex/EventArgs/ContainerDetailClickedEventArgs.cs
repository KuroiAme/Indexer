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
	public class ContainerContentClickedEventArgs : EventArgs
	{
		public Item item{get; set;}

		public ContainerContentClickedEventArgs(Item item) : base()
		{
			this.item = item;
		}
	}
}