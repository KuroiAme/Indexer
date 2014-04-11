using System;

namespace no.dctapps.commons.events
{

	public class TagClickedEventArgs : EventArgs
	{
		public ImageTag tag{ get; set; }
		public TagClickedEventArgs(ImageTag tag) : base(){
			this.tag = tag;
		} 
	}
}