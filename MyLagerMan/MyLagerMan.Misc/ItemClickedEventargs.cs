using System;
using no.dctapps.Garageindex.model;
using No.Dctapps.GarageIndex;

namespace no.dctapps.Garageindex.events
{
	public class ItemClickedEventArgs : EventArgs
	{
		public Item Item { get; set;}

			public ItemClickedEventArgs (Item item) : base()
			{
				this.Item = item;
			}

		public ItemClickedEventArgs (string str)
		{
			throw new NotImplementedException ();
		}
	}
}