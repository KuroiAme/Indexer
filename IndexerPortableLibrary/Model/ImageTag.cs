using System;
using System.Text;
using Tasky.DL.SQLite;

namespace no.dctapps.commons.events
{
	public class ImageTag
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public string TagString{get; set;}

		//public RectangleF Spot{get; set;}
		public int GalleryObjectID{ get; set;}
		public float x{ get; set;}
		public float y{ get; set;}
		public float width { get; set;}
		public float height{ get; set;}

		public override string ToString ()
		{
			return string.Format ("[ImageTag: ID={0}, TagString={1}, GalleryObjectID={2}, x={3}, y={4}, width={5}, height={6}]", ID, TagString, GalleryObjectID, x, y, width, height);
		}

		public void StoreTagList (string[] taglist)
		{
			StringBuilder sb = new StringBuilder ();
			for (int i = 0; i < taglist.Length; i++) {
				if (i != 0) {
					sb.Append (",");
				}
				sb.Append (taglist [i]);
			}
			TagString = sb.ToString ();
		}
	}
}

