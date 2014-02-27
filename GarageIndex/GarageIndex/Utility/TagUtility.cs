using System;
using System.Drawing;

namespace GarageIndex
{
	public class TagUtility
	{
		ImageTag input;
		public TagUtility (ImageTag input)
		{
			this.input = input;
		}

		public RectangleF FetchAsRectangleF(){
			RectangleF myF = new RectangleF (input.x, input.y, input.width, input.height);
			return myF;
		}

		public void StoreRectangleF(RectangleF miffed){
			input.x = miffed.X;
			input.y = miffed.Y;
			input.width = miffed.Width;
			input.height = miffed.Height;
		}
	}
}

