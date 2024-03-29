using System.Collections.Generic;
using no.dctapps.commons.events.model;
using No.Dctapps.GarageIndex;
using no.dctapps.commons.events;
using Tasky.DL.SQLiteBase;
using System;
using System.Linq;

namespace no.dctapps.commons.events
{
	public class LagerDAO
	{
		const int topID = 1;
		SQLiteConnection conn;
		bool limitedSave = false;
		const int limitedSaves = 5;

		public event EventHandler LimitExceeded;

		public LagerDAO (SQLiteConnection conn, string version)
		{
			this.conn = conn;
			conn.CreateTable<Item>();
			conn.CreateTable<LagerObject>();
			conn.CreateTable<Lager>();
			conn.CreateTable<GalleryObject> ();
			conn.CreateTable<ImageTag> ();
			conn.CreateTable<InsurancePhoto> ();
			if (version == "LITE") {
				limitedSave = true;
			}
		}

		public LagerDAO (SQLiteConnection conn)
		{
			this.conn = conn;
			conn.CreateTable<Item>();
			conn.CreateTable<LagerObject>();
			conn.CreateTable<Lager>();
			conn.CreateTable<GalleryObject> ();
			conn.CreateTable<ImageTag> ();
			conn.CreateTable<InsurancePhoto> ();
			
		}

		public Lager getLagerByName (string name)
		{
			IList<Lager> myList = new List<Lager>();

			myList = conn.Query<Lager>("select * from Lager where Name = ?", name);
			if (myList.Count > 0) {
				return myList [0];
			} else {
				return null;
			}

		}

		public void DeleteLager (int id)
		{
				Lager deleteme = new Lager ();
				deleteme.ID = id;
				conn.Delete (deleteme);
		}

		public void DeleteTag (int id)
		{
				ImageTag deleteme = new ImageTag ();
				deleteme.ID = id;
				conn.Delete (deleteme);
		}

		void SaveGalleryObjectInner (GalleryObject myObject, GalleryObject item)
		{
			if (item == null) {
				conn.Insert (myObject);
			}
			else {
				conn.Update (myObject);
			}
		}

		void RaiseLimitExceeded ()
		{
			if (LimitExceeded != null) {
				var handler = LimitExceeded;
				handler (this, new EventArgs ());
			}
		}

		public void SaveGalleryObject (GalleryObject myObject)
		{
			GalleryObject item = this.GetGalleryObjectByID (myObject.ID);

			if (limitedSave) {
				IList<GalleryObject> gos = GetAllGalleryObjects ();
				int count = (from g in gos
							select g).Count();
				if (count > limitedSaves) {
					RaiseLimitExceeded ();
				} else {
					SaveGalleryObjectInner (myObject, item);
				}
			} else {
				SaveGalleryObjectInner (myObject, item);
			}
		}

		void SaveInsurancePhotoInner (InsurancePhoto photo, InsurancePhoto item)
		{
			if (item == null) {
				conn.Insert (photo);
			}
			else {
				conn.Update (photo);
			}
		}

		int GetAntallForsikringsBilder ()
		{
			IList<InsurancePhoto> myList;
			myList = conn.Query<InsurancePhoto>("select * from InsurancePhoto");
			return (from p in myList
			        select p).Count ();
		}

		int GetNumberOfTags ()
		{
			IList<ImageTag> myList;
			myList = conn.Query<ImageTag>("select * from ImageTag");
			return (from p in myList
				select p).Count ();
		}

		int getLagerOBjectsCount ()
		{
			IList<LagerObject> myList;
			myList = conn.Query<LagerObject>("select * from LagerObject");
			return (from p in myList
				select p).Count ();
		}

		public void SaveInsurancePhoto (InsurancePhoto photo)
		{
			InsurancePhoto item = this.InsurancePhotoByID (photo.ID);

			if (limitedSave) {
				int count = GetAntallForsikringsBilder ();
				if (count > limitedSaves) {
					RaiseLimitExceeded ();
				} else {
					SaveInsurancePhotoInner (photo, item);
				}
			}else{

				SaveInsurancePhotoInner (photo, item);
			}
		}

		void SaveLagerObjectInner (LagerObject myObject)
		{
			LagerObject obj = this.GetLagerObjectByID (myObject.ID);
			if (obj == null) {
				conn.Insert (myObject);
			}
			else {
				conn.Update (myObject);
			}
		}

		public void SaveLagerObject (LagerObject myObject)
		{
			if (limitedSave && getLagerOBjectsCount () > limitedSaves) {
				RaiseLimitExceeded ();
			} else {
				SaveLagerObjectInner (myObject);
			}
		}

		public IList<ImageTag> GetTagsByGalleryObjectID (int iD)
		{
			IList<ImageTag> myList = new List<ImageTag>();

				myList = conn.Query<ImageTag>("select * from ImageTag where GalleryObjectID = ?", iD);

			return myList;
		}

		public  int GetNumberofInsurancePhotosForLargeID (int iD)
		{
			IList<InsurancePhoto> myList =  GetLargeObjectInsurancePhotosByID (iD);
			return myList.Count;
		}

		public int GetNumberOfInsurancePhotosForItemId (int iD)
		{
			IList<InsurancePhoto> myList = GetItemInsurancePhotosByID (iD);
			return myList.Count;
		}

		public  IList<InsurancePhoto> GetLargeObjectInsurancePhotosByID (int iD)
		{
			IList<InsurancePhoto> myList = new List<InsurancePhoto>();

				myList =  conn.Query<InsurancePhoto> ("select * from InsurancePhoto where ObjectReferenceID = ? and IsLargeObject = ? ORDER BY ID", iD, "true");

			return myList;
		}

		public  InsurancePhoto getInsurancePhotoLargeByIdandIndex (int iD, uint index)
		{
			IList<InsurancePhoto> myList = new List<InsurancePhoto>();

				myList =  conn.Query<InsurancePhoto> ("select * from InsurancePhoto where ObjectReferenceID = ? and IsLargeObject = ? ORDER BY ID", iD, "true");

			return myList[(int)index];
		}

		[Obsolete]
		public  InsurancePhoto getInsurancePhotoItemByIdAndIndex (int iD, uint index)
		{
			IList<InsurancePhoto> myList = new List<InsurancePhoto>();

				myList =  conn.Query<InsurancePhoto> ("select * from InsurancePhoto where ObjectReferenceID = ? and IsLargeObject = ? ORDER BY ID", iD, "false");

			return myList[(int) index];
		}

		[Obsolete]
		public  IList<InsurancePhoto> GetItemInsurancePhotosByID (int iD)
		{
			IList<InsurancePhoto> myList = new List<InsurancePhoto>();

				myList =  conn.Query<InsurancePhoto> ("select * from InsurancePhoto where ObjectReferenceID = ? and IsLargeObject = ? ORDER BY ID", iD, "false");

			return myList;
		}

		public IList<InsurancePhoto> GetInsurancePhotosByTypeAndID (bool isLargeObject, int currentID)
		{
			IList<InsurancePhoto> myList;

			string s = "false";
			if (isLargeObject) {
				s = "true";
			}

			myList =  conn.Query<InsurancePhoto> ("select * from InsurancePhoto where ObjectReferenceID = ? and IsLargeObject = ?", currentID, s);

			return myList;
		}

		public string GetNumberOfItemsForLager (Lager myLager)
		{
			if (myLager == null) {
				return "0";
			}

			IList<Item> myList =  conn.Query<Item> ("select * from Item where LagerID = ?", myLager.ID);

			IList<LagerObject> myList2 =  conn.Query<LagerObject> ("select * from LagerObject where LagerID = ?", myLager.ID);

			return (myList.Count + myList2.Count).ToString ();
		}

		public  InsurancePhoto InsurancePhotoByID (int iD)
		{
			IList<InsurancePhoto> myList = new List<InsurancePhoto>();

				myList =  conn.Query<InsurancePhoto> ("select * from InsurancePhoto where ID = ? ORDER BY ID", iD);

			if (myList.Count > 0) {
				return myList [0];
			} else {
				return null;
			}
		}

		public  ImageTag GetImageTagById (int ID)
		{
			IList<ImageTag> myList = new List<ImageTag>();

				myList =  conn.Query<ImageTag> ("select * from ImageTag where ID = ?", ID);

			if (myList.Count == 1) {
				return myList [0];
			} else {
				return null;
			}
		}

		public LagerObject GetLagerObjectByID (int ID)
		{

			IList<LagerObject> myList = new List<LagerObject>();
		
				myList =  conn.Query<LagerObject> ("select * from LagerObject where ID = ?", ID);
			if (myList.Count > 0)
				return myList [0];
			else
				return null;
		}

		[Obsolete]
		public  GalleryObject GetGalleryObjectByIndex (int index)
		{
			IList<GalleryObject> myList = new List<GalleryObject>();

				myList =  conn.Query<GalleryObject> ("select * from GalleryObject");

			return myList[index];
		}

		public IList<GalleryObject> GetAllGalleryObjects ()
		{
			IList<GalleryObject> myList = new List<GalleryObject>();

			myList =  conn.Query<GalleryObject> ("select * from GalleryObject");

			return myList;
		}

		public IList<GalleryObject> GetAllGalleryObjectsByTypeAndID (string type, int id)
		{
			IList<GalleryObject> myList = new List<GalleryObject>();

			myList =  conn.Query<GalleryObject> ("select * from GalleryObject where LocationType= ? and LocationID = ?", type,id);

			return myList;
		}

		[Obsolete]
		public  void DeleteGalleryObjectByIndex (int currentindex)
		{
			IList<GalleryObject> myList;

				myList =  conn.Query<GalleryObject> ("select * from GalleryObject");
				GalleryObject deleteme = myList [currentindex];
				conn.Delete (deleteme);

		}

		public void DeleteGalleryObject (GalleryObject del)
		{
			conn.Delete (del);
		}

		public  string GetThumbfilenameForIndex (uint index)
		{
			IList<GalleryObject> myList = new List<GalleryObject>();

				myList =  conn.Query<GalleryObject> ("select * from GalleryObject");

			return myList[(int) index].thumbFileName;
		}

		public  int GetNumberOfItemsInGallery(){
			IList<GalleryObject> myList = new List<GalleryObject>();

				myList =  conn.Query<GalleryObject> ("select * from GalleryObject");

			int x = myList.Count;
			return x;
		}


		public  IList<Item> GetItemsWithName (string text)
		{
			IList<Item> myList = new List<Item>();

				myList =  conn.Query<Item> ("select * from Item where Name = ?",text);

			return myList;
		}

		public  IList<Item> GetItemsWithDesc (string text)
		{
			IList<Item> myList = new List<Item>();

				myList =  conn.Query<Item> ("select * from Item where Description = ?", text);

			return myList;
		}

		public  IList<Item> GetItemById (int ID)
		{
			IList<Item> myList;

				myList =  conn.Query<Item> ("select * from Item where ID = ?", ID);

			return myList;
		}

		void SaveItemInner (Item item)
		{
			if (item != null && item.Name != "") {
				IList<Item> items = GetItemById (item.ID);
				if (items.Count == 0) {
					conn.Insert (item);
				}
				else {
					conn.Update (item);
				}
			}
		}

		public void SaveItem (Item item)
		{
			if (limitedSave && GetAntallTing () > limitedSaves) {
				RaiseLimitExceeded ();
			} else {
				SaveItemInner (item);
			}
		}

		int SaveTagInner (ImageTag tag)
		{
			if (tag != null) {
				ImageTag checktag = GetTagById (tag.ID);
				if (checktag == null) {
					conn.Insert (tag);
					return tag.ID;
				}
				else {
					conn.Update (tag);
					return tag.ID;
				}
			}
			else {
				return -1;
			}
		}

		public int SaveTag (ImageTag tag)
		{
			if (limitedSave && GetNumberOfTags () > limitedSaves) {
				RaiseLimitExceeded ();
				return -1;
			} else {
				return SaveTagInner (tag);
			}
		}

		void SaveLagerInner (Lager lager)
		{
			if (lager != null && lager.Name != "") {
				IList<Lager> items = GetLagersById (lager.ID);
				if (items.Count == 0) {
					conn.Insert (lager);
				}
				else {
					conn.Update (lager);
				}
			}
		}

		public void SaveLager (Lager lager)
		{
			if (limitedSave && GetAntallLagre() > limitedSaves) {
				RaiseLimitExceeded ();
			} else {
				SaveLagerInner (lager);
			}
		}

		public void DeleteBigItem (int id)
		{

				LagerObject deleteme = new LagerObject();
				deleteme.ID = id;
				conn.Delete(deleteme);

		}

		public void DeleteItem (int id)
		{

				Item deleteme = new Item();
				deleteme.ID = id;
				conn.Delete(deleteme);

		}

		public void DeleteBox (int id)
		{

				LagerObject deleteme = new LagerObject ();
				deleteme.ID = id;
				conn.Delete (deleteme);

		}

		public void InsertLager (object lm)
		{

				conn.Insert (lm);

		}

		public void UpdateLager (Lager lm)
		{

				conn.Update (lm);
			
		}

		public int GetAntallTing ()
		{
			IList<Item> list = GetAllItems();
			return list.Count;
		}

		public string GetAntallStore ()
		{
			IList<LagerObject> list = GetAllLargeItems();
			return list.Count.ToString();
		}

		public int GetAntallStore (int lagerID)
		{
			IList<LagerObject> list = GetAllLargeItems(lagerID);
			return list.Count;
		}

		public string GetAntallBeholdere ()
		{
			IList<LagerObject> mylist = GetAllContainers();
			return mylist.Count.ToString();
		}

		public int GetAntallLagre ()
		{
			IList<Lager> mylist = GetAllLagers();
			return mylist.Count;
		}


		public  GalleryObject GetGalleryObjectByID (object iD)
		{
			IList<GalleryObject> myList = null;

				myList =  conn.Query<GalleryObject> ("select * from GalleryObject where ID = ?", iD);

			if(myList.Count == 0){
				return null;
			}else{
				return myList [0];
			}
		}

		public  ImageTag GetTagById (int iD)
		{
			IList<ImageTag> myList = null;

				myList =  conn.Query<ImageTag> ("select * from ImageTag where ID = ?", iD);

			if(myList.Count == 0){
				return null;
			}else{
				return myList [0];
			}
		}

		public  LagerObject GetContainerById (int ID)
		{
			IList<LagerObject> myList = null;

				myList =  conn.Query<LagerObject> ("select * from LagerObject where ID = ?", ID);

			if(myList.Count == 0){
				return null;
			}else{
				return myList [0];
			}
		}

		public  Lager GetLagerById (int ID)
		{
			IList<Lager> myList = null;

				myList =  conn.Query<Lager> ("select * from Lager where ID = ?", ID);

			if (myList.Count == 0) {
				return null;
			} else {
				return myList [0];
			}
		}

		public  IList<Lager> GetLagersById (int ID)
		{
			IList<Lager> myList = null;

				myList =  conn.Query<Lager> ("select * from Lager where ID = ?", ID);


			return myList;
		}

		public  IList<Item> GetAllItemsInBox (LagerObject boks)
		{
			IList<Item> myList = null;

			
				myList =   conn.Query<Item> ("select * from Item where boxID = ?", boks.ID);


			return myList;
		}

		public  IList<LagerObject> GetAllLagerObjects ()
		{

				return  conn.Query<LagerObject>("select * from LagerObject");

		}

		public  IList<LagerObject> GetAllLargeItems ()
		{

				string value = "false";
				return  conn.Query<LagerObject>("select * from LagerObject where isContainer = ?", value);

		}

		public  IList<LagerObject> GetAllLargeItems (int lagerid)
		{

				string value = "false";
				return  conn.Query<LagerObject>("select * from LagerObject where isContainer = ? and LagerID = ?", value, lagerid);

		}

		public  void DeleteInsurancePhotoByIndexAndObjectId (int currentindex, int objectID)
		{
			IList<InsurancePhoto> myList;

				myList =  conn.Query<InsurancePhoto> ("select * from InsurancePhoto where ObjectReferenceID = ? ORDER BY ID", objectID);
				InsurancePhoto deleteme = myList [currentindex++];
				conn.Delete (deleteme);

		}

		public  IList<LagerObject> LoadBigItems ()
		{


				string value = "true";
				return  conn.Query<LagerObject>("select * from LagerObject where isLargeObject = ?", value);

		}

		public  IList<Item> GetAllItems ()
		{

				return  conn.Query <Item>("select * from Item");

		}

		public  IList<LagerObject> GetAllContainers(){

				string x = "true";
				return  conn.Query<LagerObject>("select * from LagerObject where isContainer = ?", x);

		}

		public  IEnumerable<LagerObject> GetAllContainersFromLagerId(int lagerid){

			IList<LagerObject> myList = conn.Query<LagerObject> ("select * from LagerObject");
			return (myList.Where (x => x.LagerID == lagerid).Where (x => x.isContainer == "true"));
//
//				string x = "true";
//				
//				return  conn.Query<LagerObject>("select * from LagerObject where isContainer = ? and LagerID = ?", x, lagerid);

		}

		public  IList<Lager> GetAllLagers(){

				return  conn.Query<Lager>("select * from Lager");

		}

		public  Lager GetLagerByID(int ID){
			IList<Lager> results;

				results =  conn.Query<Lager>("select * from Lager where ID = ?", ID);


			//Pick top result, only one can be chosen.
			if (results.Count == 0){
				return null;
			}else{
				return results[0];
			}
		}
	}
}

