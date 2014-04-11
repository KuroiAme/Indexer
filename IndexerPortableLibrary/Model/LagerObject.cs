using System;
using System.Text;
using Tasky.DL.SQLite;

namespace  no.dctapps.commons.events
{
	public class LagerObject
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set;}
		public String Name{ get; set;}
		public string Description{ get; set;}
		public string type{ get; set;}
		public string imageFileName{ get; set;}
		public string isContainer{get; set;}
		public string isLargeObject{get; set;}
		public int LagerID{get; set;}
		public int ImageTagId{ get; set;}

		public double cashValue {
			get;
			set;
		}

		public double antall {
			get;
			set;
		}

		public string thumbFileName {
			get;
			set;
		}



		public string toString(){
			StringBuilder sb = new StringBuilder ();
			sb.Append (Name);
			sb.Append ("\t");
			sb.Append ("\""+Description+"\"");
			sb.Append ("\t");
//			sb.Append (type);
//			sb.Append ("\t");
//			sb.Append (imageFileName);
//			sb.Append ("\t");
//			sb.Append (thumbFileName);
//			sb.Append ("\t");
//			sb.Append ("\n");

			return sb.ToString();
		}

		public LagerObject ()
		{
			cashValue = 0;
		}
	}
}