using System;
using No.Dctapps.GarageIndex;

namespace no.dctapps.commons.events
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