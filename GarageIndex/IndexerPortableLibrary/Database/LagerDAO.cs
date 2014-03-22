using System.Collections.Generic;
using no.dctapps.Garageindex.model;
using No.Dctapps.GarageIndex;
using GarageIndex;
using Tasky.DL.SQLiteBase;
using System;

namespace no.dctapps.Garageindex.dao
{
	public class LagerDAO
	{
		const int topID = 1;
		SQLiteConnection conn;

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

		public void SaveGalleryObject (GalleryObject myObject)
		{
			GalleryObject item = this.GetGalleryObjectByID (myObject.ID);

				if(item == null){
					conn.Insert (myObject);
				}else{
					conn.Update (myObject);
				}
			
		}

		public void SaveInsurancePhoto (InsurancePhoto photo)
		{
			InsurancePhoto item = this.InsurancePhotoByID (photo.ID);

				if(item == null){
					conn.Insert (photo);
				}else{
					conn.Update (photo);
				}
		}

		public void SaveLagerObject (LagerObject myObject)
		{
			IList<LagerObject> thelist = this.GetLagerObjectByID(myObject.ID);

				if(thelist.Count == 0){
					conn.Insert (myObject);
				}else{
					conn.Update  (myObject);
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

			myList =  conn.Query<InsurancePhoto> ("select * from InsurancePhoto where ObjectReferenceID = ? and IsLargeObject = ?", currentID, isLargeObject.ToString());

			return myList;
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

		public  IList<LagerObject> GetLagerObjectByID (int ID)
		{

			IList<LagerObject> myList = new List<LagerObject>();
		
				myList =  conn.Query<LagerObject> ("select * from LagerObject where ID = ?", ID);

			return myList;
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

		public void SaveItem (Item item)
		{
			if(item != null && item.Name != ""){
				IList<Item> items = GetItemById (item.ID);

			
					if(items.Count == 0){
					conn.Insert (item);
					}else{
						conn.Update (item);
					}
				}

		}

		public int SaveTag (ImageTag tag)
		{
			if (tag != null) {
				ImageTag checktag = GetTagById (tag.ID);


					if (checktag == null) {
						conn.Insert (tag);
						return tag.ID;
					} else {
						conn.Update (tag);
						return tag.ID;
					}
				}
			 else {
				return -1;
			}
		}

		public void SaveLager (Lager lager)
		{
			if(lager != null && lager.Name != ""){
				IList<Lager> items = GetLagersById (lager.ID);


					if(items.Count == 0){
						conn.Insert (lager);
					}else{
						conn.Update (lager);
					}

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

		public  string GetAntallTing ()
		{
			IList<Item> list =  GetAllItems();
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

		public  IList<LagerObject> GetAllContainers(int lagerid){

				string x = "true";
				return  conn.Query<LagerObject>("select * from LagerObject where isContainer = ? and LagerID = ?", x, lagerid);

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

