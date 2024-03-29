using Tasky.DL.SQLite;


namespace no.dctapps.common
{
	public class GalleryObject
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public string Name{get; set;}
		public string imageFileName{ get; set;}

		public string thumbFileName {
			get;
			set;
		}

		public string LocationType{get; set;}
		public int LocationID{get; set;}
	}
}

