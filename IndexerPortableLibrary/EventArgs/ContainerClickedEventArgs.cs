
using System;



using no.dctapps.commons.events.model;

namespace no.dctapps.commons.events
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