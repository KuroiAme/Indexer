using System;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.events;
using no.dctapps.Garageindex.screens;
using No.Dctapps.Garageindex.Ios.Screens;
using no.dctapps.Garageindex.model;
using System.Drawing;
using MonoTouch.Foundation;
using No.Dctapps.GarageIndex;
using System.Linq;

namespace GarageIndex
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

