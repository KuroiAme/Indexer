using System;
using no.dctapps.Garageindex.model;

namespace no.dctapps.Garageindex.events
{
	public class LagerObjectClickedEventArgs : EventArgs
	{
		public LagerObject LagerObject{ get; set;}

		public LagerObjectClickedEventArgs (LagerObject lagerobject) : base()
		{
			this.LagerObject = lagerobject;
		}
	}
}