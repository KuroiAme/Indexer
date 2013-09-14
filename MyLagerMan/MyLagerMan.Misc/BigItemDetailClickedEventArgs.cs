using System;
using no.dctapps.Garageindex.model;

namespace  no.dctapps.Garageindex.events
{
	public class BigItemDetailClickedEventArgs : EventArgs{
		public LagerObject lagerobject{get; set;}
		
		public BigItemDetailClickedEventArgs(LagerObject item) : base()
		{
			this.lagerobject = item;
		}
		
	}
}

