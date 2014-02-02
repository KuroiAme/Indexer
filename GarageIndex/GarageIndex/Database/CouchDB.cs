//using System;
//using System.Linq;
//using Couchbase;
//using MonoTouch.Foundation;
//using MonoTouch.UIKit;
//
//namespace GarageIndex
//{
//	public class CouchDB
//	{
//		private Database Database { get; set; }
//		CBLUITableSource Datasource { get; set; }
//		private  const string DefaultViewName = "byDate";
//		private  const string DocumentDisplayPropertyName = "text";
//		internal static readonly NSString CreationDatePropertyName = (NSString)"created_at";
//		internal static readonly NSString DeletedKey = (NSString)"_deleted";
//		Query DoneQuery { get; set; }
//
//		public CouchDB ()
//		{
//			InitializeDatabase ();
//			InitializeCouchbaseView ();
//			InitializeCouchbaseSummaryView ();
//			InitializeDatasource ();
//		}
//
//		void InitializeDatabase ()
//		{
//			NSError error;
//			var db = Manager.SharedInstance.CreateDatabaseNamed ("mystuff-carousel-sync", out error);
//			if (error != null) {
//				Console.WriteLine ("errpr initialize database");
//				throw new ApplicationException (error.Description);
//			}
//			else if (db == null)
//				throw new ApplicationException ("Could not create database");
//
//			Database = db;
//		}
//
//		void InitializeDatasource(){
//			var view = Database.ViewNamed (DefaultViewName);
//			if (view == null) {
//				Console.WriteLine ("View is null");
//			}
//
//			LiveQuery query = view.Query.AsLiveQuery;
//			query.Descending = true;
//
//			if (Datasource == null) {
//				Console.WriteLine ("Datasource er null, trying to create anew one");
//				Datasource = new CBLUITableSource ();
//			}
//
//			Datasource.Query = query;
//			Datasource.LabelProperty = DocumentDisplayPropertyName; // Document property to display in the cell label
//
////			DoneQuery = Database.ViewNamed ("Done").Query.AsLiveQuery;
////			DoneQuery.AddObserver (this, (NSString)"rows", NSKeyValueObservingOptions.New, IntPtr.Zero);
//		}
//
//		void InitializeCouchbaseView ()
//		{
//			var view = Database.ViewNamed (DefaultViewName);
//
//			var mapBlock = new MapBlock ((doc, emit) => {
//
//				var date = doc.ObjectForKey (CreationDatePropertyName);
//				var deleted = doc.ObjectForKey (DeletedKey);
//
//				if (date != null && deleted == null) {
//					emit (date, doc);
//				}
//			});
//
//			view.SetMapBlock (mapBlock, "1.1");
//
//			var validationBlock = new ValidationBlock ((revision, context) => {
//				if (revision.IsDeleted)
//					return true;
//
//				NSObject date = revision.Properties.ObjectForKey (CreationDatePropertyName);
//				return (date != null);
//			});
//
//			Database.DefineValidation (CreationDatePropertyName, validationBlock);
//
//		}
//
//		void InitializeCouchbaseSummaryView ()
//		{
//
//			var view = Database.ViewNamed ("Done");
//
//			var mapBlock = new MapBlock ((doc, emit) => {
//				var date = doc.ObjectForKey (CreationDatePropertyName);
//				var checkedOff = doc.ObjectForKey ((NSString)"check");
//
//				if (date != null) {
//					emit (NSArray.FromNSObjects (checkedOff, date), null);
//				}
//			});
//
//			var reduceBlock = new ReduceBlock ((keys, values, rereduce) => {
//				var keyArray = NSArray.FromArray<NSArray> (keys);
//				var key = keyArray.Sum(data => 1 - data.GetItem<NSNumber> (0).IntValue);
//
//				var result = new NSMutableDictionary ();
//				result.SetValueForKey ((NSString)"Items Remaining", (NSString)"Label");
//				result.SetValueForKey ((NSString)key.ToString (), (NSString)"Count");
//
//				return result;
//			});
//
//			view.SetMapBlock (mapBlock, reduceBlock, "1.1");
//
//		}
//
//
//
//		private NSMutableDictionary getDict(uint index){
//			var row = Datasource.RowAtIndex(index);
//			var doc = row.Document;
//
//			// Toggle the document's 'checked' property
//			var docContent = doc.Properties.MutableCopy () as NSMutableDictionary;
//			return docContent;
//		}
//
//		Document GetDocumentForIndex (uint index)
//		{
//			var row = Datasource.RowAtIndex (index);
//			return row.Document;
//		}
//
//		public uint GetNumberOfImages ()
//		{
//			//TEMporary implementation TODO implement properly
//			return Database.DocumentCount;
//		}
//
//		public UIImage GetImageForIndex (uint index)
//		{
//			Document doc = GetDocumentForIndex (index);
//			UIImage image = (UIImage)doc.PropertyForKey ("image");
//			if (image == null) {
//				//in case of blank return default
//				image = UIImage.FromBundle ("placeholer");
//			}
//			return image;
//		}
//
//		public UIImage GetThumbnailForIndex (uint index)
//		{
//			Document doc = GetDocumentForIndex (index);
//			UIImage image = (UIImage)doc.PropertyForKey ("thumbnail");
//			if (image == null) {
//				//in case of blank return default
//				image = UIImage.FromBundle ("placeholer");
//			}
//			return image;
//		}
//
//		public void SaveImages (UIImage image, UIImage thumbnail)
//		{
//			Console.WriteLine ("SaveImages():");
////			var vals = NSDictionary.FromObjectsAndKeys (
////				new NSObject[] { image, thumbnail},
////				new NSObject[] { new NSString("image"), new NSString("thumbnail")}
////			);
//			Document doc = Database.UntitledDocument;
////			NSError err;
//
//
//			doc.SetValueForKey ((NSObject) image, new NSString("image"));
//			doc.SetValueForKey( thumbnail, new NSString("thumbnail")); 
//
//
//
//			Console.WriteLine ("saved images");
//
////			var result = doc.PutProperties (vals, out err);
////			if (result == null) {
////				throw new ApplicationException ("failed to save a new document" + err.Description);
////			}
//		}
//	}
//}
//
