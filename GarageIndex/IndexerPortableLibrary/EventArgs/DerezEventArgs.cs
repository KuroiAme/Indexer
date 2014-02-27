using System;
using No.Dctapps.GarageIndex;

namespace no.dctapps.Garageindex.events
{
	public class DerezEventArgs : EventArgs
	{
		public Item item;
		public DerezEventArgs(Item item){
			this.item = item;
		}


	}

}