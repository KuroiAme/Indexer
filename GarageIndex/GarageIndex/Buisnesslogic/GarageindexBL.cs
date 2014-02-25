using System;
using no.dctapps.Garageindex.model;
using System.Collections.Generic;
using System.Text;
using MonoTouch.Foundation;
using MonoTouch.MessageUI;
using MonoTouch.UIKit;
using No.Dctapps.GarageIndex;
using ZXing;
using GarageIndex;

namespace no.dctapps.Garageindex.businesslogic
{
	public class GarageindexBL
	{

		public GarageindexBL ()
		{
		}

		private static string getHeaderTextLagerObject(){
			var name = NSBundle.MainBundle.LocalizedString ("Name", "Name");
			var desc = NSBundle.MainBundle.LocalizedString ("Description", "Description");
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
			string ContainerIndex = NSBundle.MainBundle.LocalizedString ("Manifest/index for Container","Manifest/index for Container");
			StringBuilder sb = new StringBuilder();
			if(input != null)
				sb.AppendLine(ContainerIndex + ":" + input.Name);
			return sb.ToString();
		}

		public string GenerateSubject(Lager input){
			string index = NSBundle.MainBundle.LocalizedString ("Manifest/index for storage","Manifest/index for storage");
			StringBuilder sb = new StringBuilder();
			if(input != null)
				sb.AppendLine(index + ":" + input.Name);
			return sb.ToString();
		}

		public string GenerateSubject(LagerObject input){
			string index = NSBundle.MainBundle.LocalizedString ("Manifest/index for ","Manifest/index for ");
			string word = "";
			if (input.isContainer == "true") {
				word = NSBundle.MainBundle.LocalizedString ("Container", "Container");
			} else if (input.isLargeObject == "true") {
				word = NSBundle.MainBundle.LocalizedString ("Large Object", "Large Object");
			} else {
				word = NSBundle.MainBundle.LocalizedString ("Item", "Item");
			}
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(index + word + ":" + input.Name);
			return sb.ToString();
		}

		public string GenerateSubject(Item input){
			string index = NSBundle.MainBundle.LocalizedString ("Manifest/index for ","Manifest/index for ");
			string word = NSBundle.MainBundle.LocalizedString ("Item", "Item");
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(index + word + ":" + input.Name);
			return sb.ToString();
		}

		public void AddPictureAttachment (MFMailComposeViewController mailContr, LagerObject input){
			var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			if (!string.IsNullOrEmpty (input.imageFileName)) {
				string jpg = input.Name + ".jpg";
				string filename = System.IO.Path.Combine (documentsDirectory, input.imageFileName);
				UIImage image = UIImage.FromFile (filename);
				NSData imagedata = image.AsJPEG ();
				mailContr.AddAttachmentData (imagedata, "image/jpeg", jpg);
			}
		}

		public void AddPictureAttachment (MFMailComposeViewController mailContr, Item input){
			var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			if (!string.IsNullOrEmpty (input.ImageFileName)) {
				string jpg = input.Name + ".jpg";
				string filename = System.IO.Path.Combine (documentsDirectory, input.ImageFileName);
				UIImage image = UIImage.FromFile (filename);
				NSData imagedata = image.AsJPEG ();
				mailContr.AddAttachmentData (imagedata, "image/jpeg", jpg);
			}
		}

//		public void AddPictureAttachment (MFMailComposeViewController mailContr, Item input){
//			var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
//			if (!string.IsNullOrEmpty (input.ImageFileName)) {
//				string jpg = input.Name + ".jpg";
//				string filename = System.IO.Path.Combine (documentsDirectory, input.ImageFileName);
//				UIImage image = UIImage.FromFile (filename);
//				NSData imagedata = image.AsJPEG ();
//				mailContr.AddAttachmentData (imagedata, "image/jpeg", jpg);
//			}
//		}



		public void AddPictureAttachments (MFMailComposeViewController mailContr, bool items)
		{
			var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			IList<LagerObject> storeting = AppDelegate.dao.LoadBigItems();
			foreach(LagerObject lobj in storeting){
				if (!string.IsNullOrEmpty (lobj.imageFileName)) {
					string jpg = lobj.Name + ".jpg";
					string filename = System.IO.Path.Combine (documentsDirectory, lobj.imageFileName);
					UIImage image = UIImage.FromFile (filename);
					NSData imagedata = image.AsJPEG ();
					mailContr.AddAttachmentData (imagedata, "image/jpeg", jpg);
				}
			}

			if(items){
				IList<Item> itemsList = AppDelegate.dao.GetAllItems();
				foreach(Item it in itemsList){
					if (!string.IsNullOrEmpty (it.ImageFileName)) {
						string jpg = it.Name + ".jpg";
						string filename = System.IO.Path.Combine (documentsDirectory, it.ImageFileName);
						UIImage image = UIImage.FromFile (filename);
						NSData imagedata = image.AsJPEG ();
						mailContr.AddAttachmentData (imagedata, "image/jpeg", jpg);
					}
				}
			}
		}


		public string GenerateManifest(Lager input){

			string lo = NSBundle.MainBundle.LocalizedString ("Large Objects","Large Objects");
			string cont = NSBundle.MainBundle.LocalizedString ("Containers","Containers");
			string ic = NSBundle.MainBundle.LocalizedString ("Items in this container","Items in this container");

			StringBuilder sb = new StringBuilder();
			if(input != null){
			sb.AppendLine(lo+":"+AppDelegate.dao.GetAntallStore(input.ID));
			IList<LagerObject> storeting = AppDelegate.dao.GetAllLargeItems(input.ID);
			sb.Append(getHeaderTextLagerObject());
			foreach(LagerObject lobj in storeting){
				sb.AppendLine(lobj.toString());
			}
			
				sb.AppendLine ("");
			sb.AppendLine("-----------------------------------");
			sb.AppendLine(cont+":"+AppDelegate.dao.GetAntallBeholdere());
				sb.AppendLine ("");


			IList<LagerObject> containers = AppDelegate.dao.GetAllContainers(input.ID);
			foreach(LagerObject con in containers){
				sb.AppendLine("-----------------------------------");
				sb.AppendLine(con.toString());
				IList<Item> items = AppDelegate.dao.GetAllItemsInBox(con);
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
			string ic = NSBundle.MainBundle.LocalizedString ("Items in this container","Items in this container");
			string name = NSBundle.MainBundle.LocalizedString ("Name","Name");
			string desc = NSBundle.MainBundle.LocalizedString ("Description", "Description");
			string inn = NSBundle.MainBundle.LocalizedString ("In", "In");

			StringBuilder sb = new StringBuilder();
			if (input != null) {
				Lager laggy = AppDelegate.dao.GetLagerByID (input.LagerID);
				sb.AppendLine(name + ":" + input.Name +"--"+ desc + ":"+input.Description );
				if(laggy != null){
					sb.AppendLine(inn+":"+laggy.Name); 
				}

				if (input.isContainer == "true") {
					sb.AppendLine (ic+";");
					IList<Item> items = AppDelegate.dao.GetAllItemsInBox (input);
					foreach (Item itty in items) {
						sb.AppendLine (itty.toString());		
					}
				}

			}
			return sb.ToString();
		}

		public string GenerateManifest(Item input){
			string inn = NSBundle.MainBundle.LocalizedString ("In","In");
		
			StringBuilder sb = new StringBuilder();
			sb.AppendLine (input.toString());
			LagerObject lo = AppDelegate.dao.GetContainerById (input.boxID);
			if(lo != null){
				sb.AppendLine (inn +":"+lo.Name);
			}
			return sb.ToString();
		}

		public LagerObject GetActiveGallery ()
		{
			var store = NSUbiquitousKeyValueStore.DefaultStore;
			int id = (int)store.GetLong ("activeLocation");
			IList<LagerObject> xs;
			xs = AppDelegate.dao.GetLagerObjectByID (id);
			if (xs.Count > 0) {
				return xs [0];
			}
			
			return null;
		}

		public void StoreActiveGallery(LagerObject lo){
			var store = NSUbiquitousKeyValueStore.DefaultStore;
			store.SetLong ("activeLocation", (long)lo.ID);
			store.Synchronize ();
		}

		public string GetUserName ()
		{
			var store = NSUbiquitousKeyValueStore.DefaultStore;

			string name = store.GetString ("UserName");

			if(string.IsNullOrEmpty(name)){
				return "<< Enter your name here >>";
			}

			return name;
		}

		public void SaveUserName(string username){
			var store = NSUbiquitousKeyValueStore.DefaultStore;
			store.SetString("UserName", username);
			store.Synchronize();
		}

		public bool StatsEnabled ()
		{
			var store = NSUbiquitousKeyValueStore.DefaultStore;

			bool enabled = store.GetBool("StatsEnabled");

			return enabled;
		}

		public void SaveStatsEnabled(bool on){
			var store = NSUbiquitousKeyValueStore.DefaultStore;
			store.SetBool("StatsEnabled", on);
			store.Synchronize();
		}

		public void SaveContainersAsLarge (bool on)
		{
			var store = NSUbiquitousKeyValueStore.DefaultStore;
			store.SetBool("ContainersAsLarge", on);
			store.Synchronize();
		}

		public bool GetContainersAsLarge(){
			var store = NSUbiquitousKeyValueStore.DefaultStore;

			bool enabled = store.GetBool("ContainersAsLarge");
	
			return enabled;
		}

		public Lager GetActiveActiveLager ()
		{
			var store = NSUbiquitousKeyValueStore.DefaultStore;

			int id = Convert.ToInt32(store.GetString("ActiveLagerID"));

			Lager ActiveLager = AppDelegate.dao.GetLagerByID (id);

			//In the case that theres never been stored anything
			if (ActiveLager == null) {
				ActiveLager = new Lager ();
				AppDelegate.dao.InsertLager (ActiveLager);
				store.SetString ("ActiveLagerID", ActiveLager.ID.ToString ());
				store.Synchronize ();
			}

			return ActiveLager;
		}

		public List<Item> SearchItems (string text)
		{

			//TODO implement async here when async (mono 3.0) gets out of BETA
			List<Item> res = new List<Item>();

			res.AddRange(AppDelegate.dao.GetItemsWithName(text));
			res.AddRange(AppDelegate.dao.GetItemsWithDesc(text));

			Console.WriteLine("Found "+res.Count + " items "); 
			return res;
		}

        public UIImage MakeQr(LagerObject lo){
			if (lo != null) {
				var writer = new BarcodeWriter ();
				writer.Format = BarcodeFormat.QR_CODE;
				var result = writer.Write (lo.ID.ToString ());
				return result;
			} else {
				return null;
			}
        }

        public void AddQRPictureAttachment (MFMailComposeViewController mailContr, LagerObject input){
            if (this.IncludeQr()) {
				if (input != null) {
					UIImage image = MakeQr (input);
					NSData imagedata = image.AsPNG ();
					mailContr.AddAttachmentData (imagedata, "image/png", "QR.png");
				}
            }
        }

        public bool IncludeQr()
        {
            var store = NSUbiquitousKeyValueStore.DefaultStore;

            bool enabled = store.GetBool("IncludeQR");

            return enabled;
        }
        public void SaveIncludeQR (bool on)
        {
            var store = NSUbiquitousKeyValueStore.DefaultStore;
            store.SetBool("IncludeQR", on);
            store.Synchronize();
        }

		public Lager GetBestLocationForSearchTerm (string searchTerm)
		{
			Console.WriteLine ("searchTerm:" + searchTerm);
			return null;
		}
	}
}

