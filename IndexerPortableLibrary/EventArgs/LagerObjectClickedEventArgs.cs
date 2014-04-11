using System;
using no.dctapps.commons.events.model;

namespace no.dctapps.commons.events
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