using System;
using System.IO;
using SQLite;
using System.Collections.Generic;
using System.Collections;
using no.dctapps.Garageindex.model;
using No.Dctapps.GarageIndex;
using GarageIndex;

namespace no.dctapps.Garageindex.dao
{
	public class LagerDAO
	{
		const int topID = 1;
		private string pathToDatabase;

		public LagerDAO ()
		{
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.LocalApplicationData);
			pathToDatabase = Path.Combine(documents, "db_sqlite-net.db");

			using (var conn= new SQLiteConnection(pathToDatabase)){
				conn.CreateTable<Item>();
				conn.CreateTable<LagerObject>();
				conn.CreateTable<Lager>();
				conn.CreateTable<GalleryObject> ();
				conn.CreateTable<ImageTag> ();
				conn.CreateTable<InsurancePhoto> ();
			}
		}



		public void DeleteLager (int id)
		{
			using (var conn = new SQLite.SQLiteConnection(pathToDatabase)) {
				Lager deleteme = new Lager ();
				deleteme.ID = id;
				int deleted = conn.Delete (deleteme);
				Console.Write("deleted:"+deleted);
			}
		}

		public void DeleteTag (int id)
		{
			using (var conn = new SQLite.SQLiteConnection(pathToDatabase)) {
				ImageTag deleteme = new ImageTag ();
				deleteme.ID = id;
				int deleted = conn.Delete (deleteme);
				Console.Write("deleted:"+deleted);
			}
		}

		public void SaveGalleryObject (GalleryObject myObject)
		{
			const int maxGalleryObjects = 10;
			if (AppDelegate.IsLite) {
				int x = this.GetNumberOfItemsInGallery ();
				if (x <= maxGalleryObjects) {
					saveGalleryObjectInner (myObject);
				} else {
					// DO NOTHING AT ALL!!! (LITE VERSION LIMITATION)
				}
			} else {
				saveGalleryObjectInner (myObject);

			}
		}

		void saveGalleryObjectInner (GalleryObject myObject)
		{
			Console.WriteLine (myObject.ID);
			GalleryObject item = this.GetGalleryObjectByID (myObject.ID);
			using (var conn = new SQLite.SQLiteConnection (pathToDatabase)) {
				if (item == null) {
					Console.WriteLine ("insert");
					conn.Insert (myObject);
				}
				else {
					Console.WriteLine ("update");
					conn.Update (myObject);
				}
			}
		}

		public void SaveInsurancePhoto (InsurancePhoto photo)
		{
			Console.WriteLine(photo.ID);
			InsurancePhoto item = this.InsurancePhotoByID (photo.ID);

			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
				if(item == null){
					Console.WriteLine("insert");
					conn.Insert (photo);
				}else{
					Console.WriteLine("update");
					conn.Update (photo);
				}
			}
		}

		public void SaveLagerObject (LagerObject myObject)
		{
			if (AppDelegate.IsLite) {
				const int maxLagerObjects = 10;
				int x = this.GetantallLagerObjects ();
				if (x <= maxLagerObjects) {
					saveLagerObjectInner (myObject);
				} else {
					//DO NOTHING (LITE VERSION LIMITATION)
				}
			} else {
				saveLagerObjectInner (myObject);
			}
		}

		void saveLagerObjectInner (LagerObject myObject)
		{
			IList<LagerObject> thelist = this.GetLagerObjectByID (myObject.ID);
			using (var conn = new SQLite.SQLiteConnection (pathToDatabase)) {
				if (thelist.Count == 0) {
					Console.WriteLine ("insert");
					conn.Insert (myObject);
				}
				else {
					Console.WriteLine ("update");
					conn.Update (myObject);
				}
			}
		}

		public IList<ImageTag> GetTagsByGalleryObjectID (int iD)
		{
			IList<ImageTag> myList = new List<ImageTag>();
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
				myList = conn.Query<ImageTag> ("select * from ImageTag where GalleryObjectID = ?", iD);
			}
			return myList;
		}

		public int GetNumberofInsurancePhotosForLargeID (int iD)
		{
			IList<InsurancePhoto> myList = GetLargeObjectInsurancePhotosByID (iD);
			return myList.Count;
		}

		public int GetNumberOfInsurancePhotosForItemId (int iD)
		{
			IList<InsurancePhoto> myList = GetItemInsurancePhotosByID (iD);
			return myList.Count;
		}

		IList<InsurancePhoto> GetLargeObjectInsurancePhotosByID (int iD)
		{
			IList<InsurancePhoto> myList = new List<InsurancePhoto>();
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
				myList = conn.Query<InsurancePhoto> ("select * from InsurancePhoto where ObjectReferenceID = ? and IsLargeObject = ? ORDER BY ID", iD, "true");
			}
			return myList;
		}

		public InsurancePhoto getInsurancePhotoLargeByIdandIndex (int iD, uint index)
		{
			IList<InsurancePhoto> myList = new List<InsurancePhoto>();
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
				myList = conn.Query<InsurancePhoto> ("select * from InsurancePhoto where ObjectReferenceID = ? and IsLargeObject = ? ORDER BY ID", iD, "true");
			}
			return myList[(int)index];
		}

		public InsurancePhoto getInsurancePhotoItemByIdAndIndex (int iD, uint index)
		{
			IList<InsurancePhoto> myList = new List<InsurancePhoto>();
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
				myList = conn.Query<InsurancePhoto> ("select * from InsurancePhoto where ObjectReferenceID = ? and IsLargeObject = ? ORDER BY ID", iD, "false");
			}
			return myList[(int) index];
		}

		IList<InsurancePhoto> GetItemInsurancePhotosByID (int iD)
		{
			IList<InsurancePhoto> myList = new List<InsurancePhoto>();
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
				myList = conn.Query<InsurancePhoto> ("select * from InsurancePhoto where ObjectReferenceID = ? and IsLargeObject = ? ORDER BY ID", iD, "false");
			}
			return myList;
		}

		InsurancePhoto InsurancePhotoByID (int iD)
		{
			IList<InsurancePhoto> myList = new List<InsurancePhoto>();
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
				myList = conn.Query<InsurancePhoto> ("select * from InsurancePhoto where ID = ? ORDER BY ID", iD);
			}
			if (myList.Count > 0) {
				return myList [0];
			} else {
				return null;
			}
		}

		public ImageTag GetImageTagById (int ID)
		{
			IList<ImageTag> myList = new List<ImageTag>();
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
				myList = conn.Query<ImageTag> ("select * from ImageTag where ID = ?", ID);
			}
			if (myList.Count == 1) {
				Console.WriteLine ("got one:"+myList[0]);
				return myList [0];
			} else {
				Console.WriteLine ("didnt get one");
				return null;
			}
		}

		public IList<LagerObject> GetLagerObjectByID (int ID)
		{
			IList<LagerObject> myList = new List<LagerObject>();
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
				myList = conn.Query<LagerObject> ("select * from LagerObject where ID = ?", ID);
			}
			return myList;
		}

		public GalleryObject GetGalleryObjectByIndex (int index)
		{
			IList<GalleryObject> myList = new List<GalleryObject>();
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
				myList = conn.Query<GalleryObject> ("select * from GalleryObject");
			}
			return myList[index];
		}

		public void DeleteGalleryObjectByIndex (int currentindex)
		{
			IList<GalleryObject> myList;
			using (var conn = new SQLite.SQLiteConnection(pathToDatabase)) {
				myList = conn.Query<GalleryObject> ("select * from GalleryObject");
				GalleryObject deleteme = myList [currentindex];
				int deleted = conn.Delete (deleteme);
				Console.Write("deleted:"+deleted);
			}
		}

		public string GetThumbfilenameForIndex (uint index)
		{
			IList<GalleryObject> myList = new List<GalleryObject>();
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
				myList = conn.Query<GalleryObject> ("select * from GalleryObject");
			}
			return myList[(int) index].thumbFileName;
		}

		public int GetNumberOfItemsInGallery(){
			IList<GalleryObject> myList = new List<GalleryObject>();
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
				myList = conn.Query<GalleryObject> ("select * from GalleryObject");
			}
			int x = myList.Count;
			Console.WriteLine ("Number of items in gallery:"+x);
			return x;
		}


		public IList<Item> GetItemsWithName (string text)
		{
			IList<Item> myList = new List<Item>();
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
				myList = conn.Query<Item> ("select * from Item where Name = ?",text);
			}
			return myList;
		}

		public IList<Item> GetItemsWithDesc (string text)
		{
			IList<Item> myList = new List<Item>();
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
				myList = conn.Query<Item> ("select * from Item where Description = ?", text);
			}
			return myList;
		}

		public IList<Item> GetItemById (int ID)
		{
			IList<Item> myList = new List<Item>();
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
				myList = conn.Query<Item> ("select * from Item where ID = ?", ID);
			}

			foreach(Item x in myList){
				Console.WriteLine(x.toString());
			}

			return myList;
		}

		public void SaveItem (Item item)
		{
			if (AppDelegate.IsLite) {
				const int maxItems = 10;
				int x = int.Parse(this.GetAntallTing ());
				if (x <= maxItems) {
					saveItemInner (item);
				} else {
					//DO DIDDLY (LITE VERSION LIMITATION.)
				}
			} else {
				saveItemInner (item);
			}
		}

		void saveItemInner (Item item)
		{
			if (item != null && item.Name != "") {
				IList<Item> items = GetItemById (item.ID);
				using (var conn = new SQLite.SQLiteConnection (pathToDatabase)) {
					if (items.Count == 0) {
						conn.Insert (item);
					}
					else {
						conn.Update (item);
					}
				}
			}
		}

		public int SaveTag (ImageTag tag)
		{
			if (AppDelegate.IsLite) {
				int x = this.GetantallTags ();
				if (x <= AppDelegate.MaxAntall) {
					return saveTagInner (tag);
				} else {
					return -1;
				}
			} else {
				return saveTagInner (tag);
			}

		}

		int saveTagInner (ImageTag tag)
		{
			if (tag != null) {
				ImageTag checktag = GetTagById (tag.ID);
				using (var conn = new SQLite.SQLiteConnection (pathToDatabase)) {
					if (checktag == null) {
						Console.WriteLine ("imageTag insert");
						conn.Insert (tag);
						return tag.ID;
					}
					else {
						Console.WriteLine ("imageTag update");
						conn.Update (tag);
						return tag.ID;
					}
				}
			}
			else {
				Console.WriteLine ("didnt save, returning -1 flag");
				return -1;
			}
		}

		public void SaveLager (Lager lager)
		{
			if(lager != null && lager.Name != ""){
				IList<Lager> items = GetLagersById (lager.ID);
				using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {

					if(items.Count == 0){
						conn.Insert (lager);
					}else{
						conn.Update (lager);
					}
				}
			}
		}

		public void DeleteBigItem (int id)
		{
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)){
				LagerObject deleteme = new LagerObject();
				deleteme.ID = id;
				int deleted = conn.Delete(deleteme);
				Console.WriteLine("deleted id:"+deleted);
			}
		}

		public void DeleteItem (int id)
		{
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)){
				Item deleteme = new Item();
				deleteme.ID = id;
				int deleted = conn.Delete(deleteme);
				Console.WriteLine("deleted id:"+deleted);
			}
		}

		public void DeleteBox (int id)
		{
			using (var conn = new SQLite.SQLiteConnection(pathToDatabase)) {
				LagerObject deleteme = new LagerObject ();
				deleteme.ID = id;
				int deleted = conn.Delete (deleteme);
				Console.Write("deleted:"+deleted);
			}
		}

		public void InsertLager (object lm)
		{
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)){
				conn.Insert (lm);
			}
		}

		public void UpdateLager (Lager lm)
		{
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)){
				conn.Update (lm);
			}
		}

		int GetantallTags ()
		{
			IList<ImageTag> myList = GetAllImageTags ();
			return myList.Count;
		}

		public string GetAntallTing ()
		{
			IList<Item> list = GetAllItems();
			return list.Count.ToString();
		}

		public string GetAntallStore ()
		{
			IList<LagerObject> list = GetAllLargeItems();
			return list.Count.ToString();
		}

		public string GetAntallStore (int lagerID)
		{
			IList<LagerObject> list = GetAllLargeItems(lagerID);
			return list.Count.ToString();
		}

		public string GetAntallBeholdere ()
		{
			IList<LagerObject> mylist = GetAllContainers();
			return mylist.Count.ToString();
		}

		public string GetAntallLagre ()
		{
			IList<Lager> mylist = GetAllLagers();
			return mylist.Count.ToString();
		}


		public GalleryObject GetGalleryObjectByID (object iD)
		{
			IList<GalleryObject> myList = null;
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
				myList = conn.Query<GalleryObject> ("select * from GalleryObject where ID = ?", iD);
			}
			if(myList.Count == 0){
				return null;
			}else{
				return myList [0];
			}
		}

		ImageTag GetTagById (int iD)
		{
			IList<ImageTag> myList = null;
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
				myList = conn.Query<ImageTag> ("select * from ImageTag where ID = ?", iD);
			}
			if(myList.Count == 0){
				return null;
			}else{
				return myList [0];
			}
		}

		public LagerObject GetContainerById (int ID)
		{
			IList<LagerObject> myList = null;
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
				myList = conn.Query<LagerObject> ("select * from LagerObject where ID = ?", ID);
			}
			if(myList.Count == 0){
				return null;
			}else{
				return myList [0];
			}
		}

		public Lager GetLagerById (int ID)
		{
			IList<Lager> myList = null;
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
				myList = conn.Query<Lager> ("select * from Lager where ID = ?", ID);
			}
			if (myList.Count == 0) {
				return null;
			} else {
				return myList [0];
			}
		}

		public IList<Lager> GetLagersById (int ID)
		{
			IList<Lager> myList = null;
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
				 myList = conn.Query<Lager> ("select * from Lager where ID = ?", ID);
			}

			return myList;
		}

		public void printAllBoxes (IList<Item> items)
		{
			for (int i = 0; i < items.Count; i++) {
				Console.WriteLine ("item_" + i + ":" + items[i].ToString());
			}
		}

		public IList<Item> GetAllItemsInBox (LagerObject boks)
		{

			SQLiteConnection conn = new SQLiteConnection (pathToDatabase);

			IList<Item> output =  conn.Query<Item>("select * from Item where boxID = ?", boks.ID);
			Console.WriteLine ("boksid=" + boks.ID);
			printAllBoxes (output);
			conn.Dispose ();
			return output;
		}

		int GetantallLagerObjects ()
		{
			IList<LagerObject> myList = GetAllLagerObjects ();
			return myList.Count;
		}

		IList<ImageTag> GetAllImageTags ()
		{
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)){
				return conn.Query<ImageTag>("select * from ImageTag");
			}
		}

		public IList<LagerObject> GetAllLagerObjects ()
		{
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)){
				return conn.Query<LagerObject>("select * from LagerObject");
			}
		}

		public IList<LagerObject> GetAllLargeItems ()
		{
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)){
				string value = "false";
				return conn.Query<LagerObject>("select * from LagerObject where isContainer = ?", value);
			}
		}

		public IList<LagerObject> GetAllLargeItems (int lagerid)
		{
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)){
				string value = "false";
				return conn.Query<LagerObject>("select * from LagerObject where isContainer = ? and LagerID = ?", value, lagerid);
			}
		}

		public void DeleteInsurancePhotoByIndexAndObjectId (int currentindex, int objectID)
		{
			IList<InsurancePhoto> myList;
			using (var conn = new SQLite.SQLiteConnection(pathToDatabase)) {
				myList = conn.Query<InsurancePhoto> ("select * from InsurancePhoto where ObjectReferenceID = ? ORDER BY ID", objectID);
				InsurancePhoto deleteme = myList [currentindex++];
				int deleted = conn.Delete (deleteme);
				Console.Write("deleted:"+deleted);
			}
		}

		public IList<LagerObject> LoadBigItems ()
		{

			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)){
				string value = "true";
				return conn.Query<LagerObject>("select * from LagerObject where isLargeObject = ?", value);
			}
		}

		public IList<Item> GetAllItems ()
		{
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)){
				return conn.Query<Item>("select * from Item");
			}
		}

		public IList<LagerObject> GetAllContainers(){
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)){
				string x = "true";
				return conn.Query<LagerObject>("select * from LagerObject where isContainer = ?", x);
			}
		}

		public IList<LagerObject> GetAllContainers(int lagerid){
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)){
				string x = "true";
				return conn.Query<LagerObject>("select * from LagerObject where isContainer = ? and LagerID = ?", x, lagerid);
			}
		}

		public IList<Lager> GetAllLagers(){
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)){
				return conn.Query<Lager>("select * from Lager");
			}
		}

		public Lager GetLagerByID(int ID){
			IList<Lager> results;
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)){
				results = conn.Query<Lager>("select * from Lager where ID = ?", ID);
			}

			//Pick top result, only one can be chosen.
			if (results.Count == 0){
				return null;
			}else{
				return results[0];
			}
		}
	}
}

