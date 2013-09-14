using System;
using no.dctapps.Garageindex.model;


namespace no.dctapps.Garageindex.events
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