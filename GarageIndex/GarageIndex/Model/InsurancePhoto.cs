using System;
using SQLite;

namespace GarageIndex
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

