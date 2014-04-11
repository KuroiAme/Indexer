using System;

using no.dctapps.commons.events.model;

namespace no.dctapps.commons.events
{

	public class LagerClickedEventArgs : EventArgs
	{
		public Lager Lager;
		public LagerClickedEventArgs(Lager lager){
			this.Lager = lager;
		}
	}


}

