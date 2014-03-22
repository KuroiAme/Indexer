using System;
using no.dctapps.Garageindex.model;
using System.Collections.Generic;
using System.Text;
using No.Dctapps.GarageIndex;
using GarageIndex;
using no.dctapps.Garageindex.dao;
using IndexerIOS;
using System.Linq;

namespace no.dctapps.Garageindex.businesslogic
{
	public class IndexerBuisnessService
	{
		readonly LagerDAO dao;
		readonly ITranslationService translate;

		public IndexerBuisnessService (LagerDAO dao, ITranslationService translate)
		{
			this.dao = dao;
			this.translate = translate;
		}

		public double GetTotalValue ()
		{
			double cashvalue = 0;
			IList<Item> allItems = dao.GetAllItems ();
			IList<LagerObject> allLarge = dao.GetAllLargeItems ();
			foreach (Item it in allItems) {
				cashvalue += it.cashValue;
			}

			foreach (LagerObject myobj in allLarge) {
				cashvalue += myobj.cashValue;
			}

			return cashvalue;
		}

		private string getHeaderTextLagerObject ()
		{
			var name = translate.getTranslatedText ("Name");
			var desc = translate.getTranslatedText ("Description");
			StringBuilder sb = new StringBuilder ();
			sb.Append (name);
			sb.Append ("\t");
			sb.Append ("\"");
			sb.Append (desc);
			sb.Append ("\"");
			sb.Append ("\t");
			sb.AppendLine ("\n-----------------------------------");
			return sb.ToString ();
		}

		public string GenerateContainerSubject (LagerObject input)
		{
			string ContainerIndex = translate.getTranslatedText ("Manifest/index for Container");
			StringBuilder sb = new StringBuilder ();
			if (input != null)
				sb.AppendLine (ContainerIndex + ":" + input.Name);
			return sb.ToString ();
		}

		public string GenerateSubject (Lager input)
		{
			string index = translate.getTranslatedText ("Contents for location", "Contents for location");
			StringBuilder sb = new StringBuilder ();
			if (input != null)
				sb.AppendLine (index + ":" + input.Name);
			return sb.ToString ();
		}

		public string GenerateSubject (LagerObject input)
		{
			string index = translate.getTranslatedText ("Manifest/index for ", "Manifest/index for ");
			string word = "";
			if (input.isContainer == "true") {
				word = translate.getTranslatedText ("Container", "Container");
			} else if (input.isLargeObject == "true") {
				word = translate.getTranslatedText ("Large Object", "Large Object");
			} else {
				word = translate.getTranslatedText ("Item", "Item");
			}
			StringBuilder sb = new StringBuilder ();
			sb.AppendLine (index + word + ":" + input.Name);
			return sb.ToString ();
		}

		public string GenerateSubject (Item input)
		{
			string index = translate.getTranslatedText ("Manifest/index for ", "Manifest/index for ");
			string word = translate.getTranslatedText ("Item", "Item");
			StringBuilder sb = new StringBuilder ();
			sb.AppendLine (index + word + ":" + input.Name);
			return sb.ToString ();
		}

		public string GenerateManifest (Lager input)
		{

			string lo = translate.getTranslatedText ("Large Objects", "Large Objects");
			string cont = translate.getTranslatedText ("Containers", "Containers");
			string ic = translate.getTranslatedText ("Items in this container", "Items in this container");

			StringBuilder sb = new StringBuilder ();

			if (input != null) {
				int x = dao.GetAntallStore (input.ID);
				if (x > 0) {
					sb.AppendLine (lo + ":" + x);
					IList<LagerObject> storeting = dao.GetAllLargeItems (input.ID);
					sb.Append (getHeaderTextLagerObject ());
					foreach (LagerObject lobj in storeting) {
						sb.AppendLine (lobj.toString ());
					}
				}

				List<LagerObject> containers = dao.GetAllContainersFromLagerId (input.ID).ToList();
				int count = (containers.Select (i => i)).Count();

				if (count > 0) {

					sb.AppendLine ("");
					sb.AppendLine (cont + ":" + count);
					sb.AppendLine ("");



					foreach (LagerObject con in containers) {
		
						sb.AppendLine (con.toString ());
						IList<Item> items = dao.GetAllItemsInBox (con);
						sb.AppendLine (ic + ":" + items.Count);
						foreach (Item it in items) {
							sb.AppendLine ("\t" + it.Name + "," + it.Description);
						}
					}
				}
			}
			return sb.ToString ();
			
		}

		public string GenerateContainerManifest (LagerObject input)
		{
			string ic = translate.getTranslatedText ("Items in this container", "Items in this container");
			string name = translate.getTranslatedText ("Name", "Name");
			string desc = translate.getTranslatedText ("Description", "Description");
			string inn = translate.getTranslatedText ("In", "In");

			StringBuilder sb = new StringBuilder ();
			if (input != null) {
				Lager laggy = dao.GetLagerByID (input.LagerID);
				sb.AppendLine (name + ":" + input.Name + "--" + desc + ":" + input.Description);
				if (laggy != null) {
					sb.AppendLine (inn + ":" + laggy.Name); 
				}

				if (input.isContainer == "true") {
					sb.AppendLine (ic + ";");
					IList<Item> items = dao.GetAllItemsInBox (input);
					foreach (Item itty in items) {
						sb.AppendLine (itty.toString ());		
					}
				}

			}
			return sb.ToString ();
		}

		public string GenerateManifest (Item input)
		{
			string inn = translate.getTranslatedText ("In", "In");
		
			StringBuilder sb = new StringBuilder ();
			sb.AppendLine (input.toString ());
			LagerObject lo = dao.GetContainerById (input.boxID);
			if (lo != null) {
				sb.AppendLine (inn + ":" + lo.Name);
			}
			return sb.ToString ();
		}

		char[] sep = { ',', ' ' };

		void FetchWordsForLargeObjects (List<IndexerDictionaryItem> Dictionary)
		{
			IList<LagerObject> lagerobjects = dao.GetAllLargeItems ();
			foreach (LagerObject ox in lagerobjects) {
				IndexerDictionaryItem x = new IndexerDictionaryItem ();
				x.id = ox.ID;
				x.type = "LargeObject";
				x.value = ox.Name;
				if (x.Name != null) {
					x.Name = x.Name;
				}
				Dictionary.Add (x);
				if (ox.Description != null) {
					string[] descwords = ox.Description.Split (sep);
					foreach (string descword in descwords) {
						IndexerDictionaryItem y = new IndexerDictionaryItem ();
						y.id = ox.ID;
						y.type = "LargeObject";
						y.value = descword;
						if (ox.Name != null) {
							y.Name = ox.Name;
						}
						Dictionary.Add (y);
					}
				}
				ImageTag tag = dao.GetImageTagById (ox.ImageTagId);
				if (tag != null && tag.TagString != null) {
					string[] tags = tag.TagString.Split (sep);
					foreach (string word in tags) {
						IndexerDictionaryItem z = new IndexerDictionaryItem ();
						z.id = ox.ID;
						z.type = "LargeObject";
						z.value = word;
						if (ox.Name != null) {
							z.Name = z.Name;
						}
						Dictionary.Add (z);
					}
				}
			}
		}

		void FetchWordsForContainers (List<IndexerDictionaryItem> Dictionary)
		{
			IList<LagerObject> lagerobjects = dao.GetAllContainers ();
			foreach (LagerObject ox in lagerobjects) {
				IndexerDictionaryItem x = new IndexerDictionaryItem ();
				x.id = ox.ID;
				x.type = "Container";
				x.value = ox.Name;
				if (x.Name != null) {
					x.Name = x.Name;
				}
				Dictionary.Add (x);
				if (ox.Description != null) {
					string[] descwords = ox.Description.Split (sep);
					foreach (string descword in descwords) {
						IndexerDictionaryItem y = new IndexerDictionaryItem ();
						y.id = ox.ID;
						y.type = "Container";
						y.value = descword;
						if (ox.Name != null) {
							y.Name = ox.Name;
						}
						Dictionary.Add (y);
					}
				}
				ImageTag tag = dao.GetImageTagById (ox.ImageTagId);
				if (tag != null && tag.TagString != null) {
					string[] tags = tag.TagString.Split (sep);
					foreach (string word in tags) {
						IndexerDictionaryItem z = new IndexerDictionaryItem ();
						z.id = ox.ID;
						z.type = "Container";
						z.value = word;
						if (ox.Name != null) {
							z.Name = z.Name;
						}
						Dictionary.Add (z);
					}
				}
			}
		}

		void FetchWordsForGalleryObjects (List<IndexerDictionaryItem> Dictionary)
		{
			IList<GalleryObject> gals = dao.GetAllGalleryObjects ();
			foreach (GalleryObject go in gals) {
				IList<ImageTag> tags = dao.GetTagsByGalleryObjectID (go.ID);
				foreach (ImageTag tag in tags) {
					string[] words = tag.TagString.Split (sep);
					foreach (string word in words) {
						IndexerDictionaryItem x = new IndexerDictionaryItem ();
						x.id = go.ID;
						x.type = "GalleryObject";
						x.value = word;
						if (go.Name != null) {
							x.Name = go.Name;
						}
						Dictionary.Add (x);
					}
				}
			}
		}

		void FetchWordsForItems (List<IndexerDictionaryItem> Dictionary)
		{
			IList<Item> items = dao.GetAllItems ();
			foreach (Item i in items) {
				IndexerDictionaryItem x = new IndexerDictionaryItem ();
				x.id = i.ID;
				x.type = "Item";
				x.value = i.Name;
				if (x.Name != null) {
					x.Name = i.Name;
				}
				Dictionary.Add (x);
				if (i.Description != null) {
					string[] descwords = i.Description.Split (sep);
					foreach (string descword in descwords) {
						IndexerDictionaryItem y = new IndexerDictionaryItem ();
						y.id = i.ID;
						y.type = "Item";
						y.value = descword;
						if (i.Name != null) {
							y.Name = i.Name;
						}
						Dictionary.Add (y);
					}
				}
				ImageTag tag = dao.GetImageTagById (i.ImageTagId);
				if (tag != null && tag.TagString != null) {
					string[] tags = tag.TagString.Split (sep);
					foreach (string word in tags) {
						IndexerDictionaryItem z = new IndexerDictionaryItem ();
						z.id = i.ID;
						z.type = "Item";
						z.value = word;
						if (i.Name != null) {
							z.Name = i.Name;
						}
						Dictionary.Add (z);
					}
				}
			}
		}

		public List<IndexerDictionaryItem> GetAllSearchableWordsDictionary ()
		{
			List<IndexerDictionaryItem> Dictionary = new List<IndexerDictionaryItem> ();

			FetchWordsForGalleryObjects (Dictionary);

			FetchWordsForItems (Dictionary);

			FetchWordsForLargeObjects (Dictionary);

			FetchWordsForContainers (Dictionary);

			return Dictionary;

		}
		//		public List<Item> SearchItems (string text)
		//		{
		//
		//			//TODO implement async here when async (mono 3.0) gets out of BETA
		//			List<Item> res = new List<Item>();
		//
		//			res.AddRange(dao.GetItemsWithName(text));
		//			res.AddRange(dao.GetItemsWithDesc(text));
		//
		//			//Console.WriteLine("Found "+res.Count + " items ");
		//			return res;
		//		}
		public List<WordCloudItem> GetWordCloudDictionary ()
		{
			List<WordCloudItem> dictionary = new List<WordCloudItem> ();
			List<IndexerDictionaryItem> origin = GetAllSearchableWordsDictionary ();
			foreach (IndexerDictionaryItem item in origin) {
				WordCloudItem it = new WordCloudItem ();
				it.word = item.value;
				Boolean found = false;
				foreach (WordCloudItem cloudItem in dictionary) {
					if (cloudItem != null && it != null) {
						if (cloudItem.Equals (it)) {
							cloudItem.weight++;
							found = true;
						}
					}
				}
				if (!found) {
					it.weight = 1;
					dictionary.Add (it);
				}
			}

			return dictionary;
		}
	}
}

