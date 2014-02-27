using System;

namespace GarageIndex
{

	public class TagClickedEventArgs : EventArgs
	{
		public ImageTag tag{ get; set; }
		public TagClickedEventArgs(ImageTag tag) : base(){
			this.tag = tag;
		} 
	}
}