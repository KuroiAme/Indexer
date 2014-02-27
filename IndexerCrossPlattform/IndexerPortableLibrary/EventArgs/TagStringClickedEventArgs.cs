using System;

namespace GarageIndex
{
	public class TagStringClickedEventArgs : EventArgs
	{
		public string tagstring;
		public int pos;

		public TagStringClickedEventArgs(string tagstring, int pos) : base(){
			this.tagstring = tagstring;
			this.pos = pos;
		}
	}
}

