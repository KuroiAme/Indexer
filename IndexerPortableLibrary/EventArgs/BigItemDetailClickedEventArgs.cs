using System;
using no.dctapps.commons.events.model;

namespace  no.dctapps.commons.events
{
	public class BigItemDetailClickedEventArgs : EventArgs{
		public LagerObject lagerobject{get; set;}
		
		public BigItemDetailClickedEventArgs(LagerObject item) : base()
		{
			this.lagerobject = item;
		}
		
	}
}

