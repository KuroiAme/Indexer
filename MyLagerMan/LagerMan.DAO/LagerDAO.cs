using System;
using System.IO;
using SQLite;
using System.Collections.Generic;
using System.Collections;
using no.dctapps.Garageindex.model;
using No.Dctapps.GarageIndex;

namespace no.dctapps.Garageindex.dao
{
	public class LagerDAO
	{
		const int topID = 1;
		private string pathToDatabase;

		public LagerDAO ()
		{
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.LocalApplicationData);
			pathToDatabase = Path.Combine (documents, "db_sqlite-net.db");

			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)){
				Console.WriteLine("dbpath:"+pathToDatabase);
				conn.CreateTable<Item>();
				conn.CreateTable<LagerObject>();
				conn.CreateTable<Lager>();
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

		public void saveLagerObject (LagerObject myObject)
		{
			Console.WriteLine(myObject.ID);
			IList<LagerObject> thelist = this.getLagerObjectByID (myObject.ID);
			Console.WriteLine(thelist.Count);

			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
				if(thelist.Count == 0){
					Console.WriteLine("insert");
					conn.Insert (myObject);
				}else{
					Console.WriteLine("update");
					conn.Update (myObject);
				}
			}
		}

		public IList<LagerObject> getLagerObjectByID (int ID)
		{
			IList<LagerObject> myList = new List<LagerObject>();
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
				myList = conn.Query<LagerObject> ("select * from LagerObject where ID = ?", ID);
			}
			return myList;
		}



		public IList<Item> getItemsWithName (string text)
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
			if(item != null && item.Name != ""){
				IList<Item> items = GetItemById (item.ID);
				using (var conn= new SQLite.SQLiteConnection(pathToDatabase)) {
			
					if(items.Count == 0){
					conn.Insert (item);
					}else{
						conn.Update (item);
					}
				}
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

		public void insertLager (object lm)
		{
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)){
				conn.Insert (lm);
			}
		}

		public void updateLager (Lager lm)
		{
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)){
				conn.Update (lm);
			}
		}

		public string getAntallTing ()
		{
			IList<Item> list = getAllItems();
			return list.Count.ToString();
		}

		public string getAntallStore ()
		{
			IList<LagerObject> list = GetAllLargeItems();
			return list.Count.ToString();
		}

		public string getAntallStore (int lagerID)
		{
			IList<LagerObject> list = GetAllLargeItems(lagerID);
			return list.Count.ToString();
		}

		public string getAntallBeholdere ()
		{
			IList<LagerObject> mylist = getAllContainers();
			return mylist.Count.ToString();
		}

		public string getAntallLagre ()
		{
			IList<Lager> mylist = getAllLagers();
			return mylist.Count.ToString();
		}

		public LagerObject getContainerById (int ID)
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

		public IList<Item> getAllItemsInBox (LagerObject boks)
		{

			SQLiteConnection conn = new SQLiteConnection (pathToDatabase);

			IList<Item> output =  conn.Query<Item>("select * from Item where boxID = ?", boks.ID);
			Console.WriteLine ("boksid=" + boks.ID);
			printAllBoxes (output);
			conn.Dispose ();
			return output;
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

		public IList<LagerObject> loadBigItems ()
		{

			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)){
				string value = "true";
				return conn.Query<LagerObject>("select * from LagerObject where isLargeObject = ?", value);
			}
		}

		public IList<Item> getAllItems ()
		{
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)){
				return conn.Query<Item>("select * from Item");
			}
		}

		public IList<LagerObject> getAllContainers(){
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)){
				string x = "true";
				return conn.Query<LagerObject>("select * from LagerObject where isContainer = ?", x);
			}
		}

		public IList<LagerObject> getAllContainers(int lagerid){
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)){
				string x = "true";
				return conn.Query<LagerObject>("select * from LagerObject where isContainer = ? and LagerID = ?", x, lagerid);
			}
		}

		public IList<Lager> getAllLagers(){
			using (var conn= new SQLite.SQLiteConnection(pathToDatabase)){
				return conn.Query<Lager>("select * from Lager");
			}
		}

		public Lager getLagerByID(int ID){
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

