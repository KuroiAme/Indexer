using System;

using no.dctapps.Garageindex.model;

namespace no.dctapps.Garageindex.events
{

	public class LagerClickedEventArgs : EventArgs
	{
		public Lager Lager;
		public LagerClickedEventArgs(Lager lager){
			this.Lager = lager;
		}
	}


}

