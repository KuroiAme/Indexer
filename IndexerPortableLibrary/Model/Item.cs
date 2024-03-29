using System;
using System.Text;
using Tasky.DL.SQLite;

namespace No.Dctapps.GarageIndex
{
	public class Item : IComparable
	{
		#region IComparable implementation
		public int CompareTo (Object o)
		{
			if(Name == null){
				return 1;
			}

			Item y = (Item) o;


			return Name.CompareTo(y.Name);

		}
		#endregion

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name{ get; set;}
		public string Description{ get; set;}
		public string ImageFileName{ get; set;}
		public string boxName{get; set;}
		public int boxID{ get; set;}
		public int ImageTagId{ get; set;}
		public int LagerID{get; set;}

		public double cashValue {
			get;
			set;
		}

		public double antall {
			get;
			set;
		}

		public string Action {
			get;
			set;
		}

		public string ActionComment {
			get;
			set;
		}

		public string ThumbFileName {
			get;
			set;
		}

		public string toString ()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(Name);
			sb.Append ("\t");
			sb.Append ("\""+Description+"\"");
			sb.Append ("\t");
//			sb.Append(ImageFileName);
//			sb.Append ("\t");
//			sb.Append(boxName);
//			sb.Append ("\t");
//			sb.Append(boxID);
//			sb.Append ("\t");
//			sb.Append(ThumbFileName);
//			sb.Append ("\t");
			return sb.ToString();
		}

		public Item ()
		{
			cashValue = 0;
		}
	}
}

