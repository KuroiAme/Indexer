using System;
using No.Dctapps.GarageIndex;

namespace no.dctapps.commons.events
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