using System;
using no.dctapps.commons.events.model;


namespace no.dctapps.commons.events
{
	public class BoxClickedEventArgs : EventArgs
	{
		public LagerObject Box { get; set;}

			public BoxClickedEventArgs (LagerObject box) : base()
			{
				this.Box = box;
			}
	}
}