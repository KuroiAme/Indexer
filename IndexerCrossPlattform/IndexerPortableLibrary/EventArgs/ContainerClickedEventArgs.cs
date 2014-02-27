
using System;



using no.dctapps.Garageindex.model;

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