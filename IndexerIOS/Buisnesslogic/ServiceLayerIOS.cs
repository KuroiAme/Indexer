using System;
using MonoTouch.Foundation;
using no.dctapps.Garageindex.model;
using MonoTouch.MessageUI;
using MonoTouch.UIKit;
using no.dctapps.Garageindex.businesslogic;
using System.Collections.Generic;
using No.Dctapps.GarageIndex;
using ZXing;

namespace GarageIndex
{
	public class KeyStorageServiceIos
	{
		public KeyStorageServiceIos ()
		{
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


		public void AddQRPictureAttachment (MFMailComposeViewController mailContr, LagerObject input){
			if (this.IncludeQr()) {
				if (input != null) {
					UIImage image = AppDelegate.key.MakeQr (input);
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

		[Obsolete]
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

		public int GetActiveGalleryID ()
		{
			var store = NSUbiquitousKeyValueStore.DefaultStore;
			int id = (int)store.GetLong ("activeLocation");
			return id;
		}

		public void StoreActiveGallery(LagerObject lo){
			var store = NSUbiquitousKeyValueStore.DefaultStore;
			store.SetLong ("activeLocation", (long)lo.ID);
			store.SetString ("activeGalleryType", "LagerObject");
			store.Synchronize ();
		}

		public void StoreActiveGallery(Lager lo){
			var store = NSUbiquitousKeyValueStore.DefaultStore;
			store.SetLong ("activeLocation", (long)lo.ID);
			store.SetString ("activeGalleryType", "Lager");
			store.Synchronize ();
		}

		public string GetActiveGalleryType ()
		{
			var store = NSUbiquitousKeyValueStore.DefaultStore;
			var type = store.GetString ("activeGalleryType");
			return type;
		}
	}
}

