using System;
using Tasky.DL.SQLite;

namespace no.dctapps.commons.events
{
	public class InsurancePhoto
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string ImageFileName{ get; set;}
		public string ThumbFileName {get;set;}
		public int ObjectReferenceID{ get; set;}
		public string IsLargeObject{ get; set;}

	}
}

