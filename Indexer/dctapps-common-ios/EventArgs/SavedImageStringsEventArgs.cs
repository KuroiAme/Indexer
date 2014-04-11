using System;

namespace no.dctapps.commons.events
{
	public class SavedImageStringsEventArgs : EventArgs
	{
		public string imageFilename;
		public string Thumbfilename;
		public SavedImageStringsEventArgs (String imageFilename, String Thumbfilename)
		{
			this.imageFilename = imageFilename;
			this.Thumbfilename = Thumbfilename;
		}
	}
}

