using System;
using Tasky.DL.SQLite;

namespace no.dctapps.Garageindex.model
{
	public class Lager
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name{get; set;}
		public string DescriptionFileName{get;set;}
		public string LagerImage{get;set;}
		public string address{ get; set;}
		public string telephone{ get; set;}

		public int height{ get; set;}
		public int width{ get; set;}
		public int depth{ get; set;}

		public string postnr{ get; set;}
		public string poststed{ get; set;}

		public string thumbFileName {
			get;
			set;
		}

		public override string ToString() {
			string output = "";
			output += Name;
			output += ",";
			output += DescriptionFileName;
			output += ",";
			output += LagerImage;
			output += ",";
			output += address;
			output += ",";
			output += telephone;
			output += ",";
			output += height;
			output += ",";
			output += width;
			output += ",";
			output += depth;
			output += ",";
			output += postnr;
			output += ",";
			output += poststed;
			return output;
		}
	}		
}