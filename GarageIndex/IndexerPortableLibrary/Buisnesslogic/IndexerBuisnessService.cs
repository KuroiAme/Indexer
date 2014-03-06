using System;
using no.dctapps.Garageindex.model;
using System.Collections.Generic;
using System.Text;
using No.Dctapps.GarageIndex;
using GarageIndex;
//using no.dctapps.Garageindex.dao;
using no.dctapps.Garageindex.dao;

namespace no.dctapps.Garageindex.businesslogic
{
	public class IndexerBuisnessService
	{
		LagerDAO dao;
		readonly ITranslationService translate;

		public IndexerBuisnessService (LagerDAO dao, ITranslationService translate)
		{
			this.dao = dao;
			this.translate = translate;
		}

		private string getHeaderTextLagerObject(){
			var name = translate.getTranslatedText ("Name");
			var desc = translate.getTranslatedText ("Description");
			StringBuilder sb = new StringBuilder();
			sb.Append (name);
			sb.Append ("\t");
			sb.Append ("\"");
			sb.Append (desc);
			sb.Append ("\"");
			sb.Append ("\t");
			sb.AppendLine("\n-----------------------------------");
			return sb.ToString();
		}

		public string GenerateContainerSubject (LagerObject input)
		{
			string ContainerIndex = translate.getTranslatedText ("Manifest/index for Container");
			StringBuilder sb = new StringBuilder();
			if(input != null)
				sb.AppendLine(ContainerIndex + ":" + input.Name);
			return sb.ToString();
		}

		public string GenerateSubject(Lager input){
			string index = translate.getTranslatedText ("Manifest/index for storage","Manifest/index for storage");
			StringBuilder sb = new StringBuilder();
			if(input != null)
				sb.AppendLine(index + ":" + input.Name);
			return sb.ToString();
		}

		public string GenerateSubject(LagerObject input){
			string index = translate.getTranslatedText ("Manifest/index for ","Manifest/index for ");
			string word = "";
			if (input.isContainer == "true") {
				word = translate.getTranslatedText ("Container", "Container");
			} else if (input.isLargeObject == "true") {
				word = translate.getTranslatedText ("Large Object", "Large Object");
			} else {
				word = translate.getTranslatedText ("Item", "Item");
			}
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(index + word + ":" + input.Name);
			return sb.ToString();
		}

		public string GenerateSubject(Item input){
			string index = translate.getTranslatedText ("Manifest/index for ","Manifest/index for ");
			string word = translate.getTranslatedText ("Item", "Item");
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(index + word + ":" + input.Name);
			return sb.ToString();
		}
			
		public string GenerateManifest(Lager input){

			string lo = translate.getTranslatedText ("Large Objects","Large Objects");
			string cont = translate.getTranslatedText ("Containers","Containers");
			string ic = translate.getTranslatedText ("Items in this container","Items in this container");

			StringBuilder sb = new StringBuilder();
			if(input != null){
			sb.AppendLine(lo+":"+dao.GetAntallStore(input.ID));
			IList<LagerObject> storeting = dao.GetAllLargeItems(input.ID);
			sb.Append(getHeaderTextLagerObject());
			foreach(LagerObject lobj in storeting){
				sb.AppendLine(lobj.toString());
			}
			
				sb.AppendLine ("");
			sb.AppendLine("-----------------------------------");
			sb.AppendLine(cont+":"+dao.GetAntallBeholdere());
				sb.AppendLine ("");


			IList<LagerObject> containers = dao.GetAllContainers(input.ID);
			foreach(LagerObject con in containers){
				sb.AppendLine("-----------------------------------");
				sb.AppendLine(con.toString());
				IList<Item> items = dao.GetAllItemsInBox(con);
				sb.AppendLine (ic+":"+items.Count);
				sb.AppendLine("++++++++++++++++++++++++");
				foreach(Item it in items){
					sb.AppendLine(it.toString());
				}
			}
			}
			return sb.ToString();
			
		}

		public string GenerateContainerManifest(LagerObject input){
			string ic = translate.getTranslatedText ("Items in this container","Items in this container");
			string name = translate.getTranslatedText ("Name","Name");
			string desc = translate.getTranslatedText ("Description", "Description");
			string inn = translate.getTranslatedText ("In", "In");

			StringBuilder sb = new StringBuilder();
			if (input != null) {
				Lager laggy = dao.GetLagerByID (input.LagerID);
				sb.AppendLine(name + ":" + input.Name +"--"+ desc + ":"+input.Description );
				if(laggy != null){
					sb.AppendLine(inn+":"+laggy.Name); 
				}

				if (input.isContainer == "true") {
					sb.AppendLine (ic+";");
					IList<Item> items = dao.GetAllItemsInBox (input);
					foreach (Item itty in items) {
						sb.AppendLine (itty.toString());		
					}
				}

			}
			return sb.ToString();
		}

		public string GenerateManifest(Item input){
			string inn = translate.getTranslatedText ("In","In");
		
			StringBuilder sb = new StringBuilder();
			sb.AppendLine (input.toString());
			LagerObject lo = dao.GetContainerById (input.boxID);
			if(lo != null){
				sb.AppendLine (inn +":"+lo.Name);
			}
			return sb.ToString();
		}



		public List<Item> SearchItems (string text)
		{

			//TODO implement async here when async (mono 3.0) gets out of BETA
			List<Item> res = new List<Item>();

			res.AddRange(dao.GetItemsWithName(text));
			res.AddRange(dao.GetItemsWithDesc(text));

			//Console.WriteLine("Found "+res.Count + " items "); 
			return res;
		}




	}
}

