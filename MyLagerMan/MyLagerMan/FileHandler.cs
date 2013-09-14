using System;
using System.IO;
using LagerMan.Model;
using MonoTouch.UIKit;
using LagerMan_Model;
using System.Collections.Generic;

namespace LagerMan.iOS
{
	public class FileHandler
	{
	
			public FileHandler ()
			{
			}
//
			public Lager ReadLagerData(){
				var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
				var filename = Path.Combine (documents,"Lager.txt");

				if (File.Exists(filename)) {
					var text = File.ReadAllText (filename);
					Console.WriteLine("lest:"+text);
					char[] sep = {','};
					String[] split = text.Split (sep);
					Lager output = new Lager ();
					output.Name = split [0];
					output.DescriptionFileName = split [1];
					output.LagerImage = split [2];
					output.address = split [3];
					output.telephone = split [4];
					output.height = Convert.ToInt32 (split [5]);
					output.width = Convert.ToInt32 (split [6]);
					output.depth = Convert.ToInt32 (split [7]);
					output.postnr = split[8];
					output.poststed = split[9];
					Console.Write("File finished loading:"+output.ToString());
					return output;
				} else {
					Console.WriteLine("Catastrophe avoided");
					return null;
				}
			}



			public static void saveLager (Lager lm)
			{
				var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
				var filename = Path.Combine (documents,"Lager.txt");
				File.WriteAllText(filename, lm.ToString());
				Console.WriteLine ("file saved");
			}

//			public void DeleteBigItem (int id)
//			{
//				List<LagerObject> allLagers = loadBigItems ();
//				for (int i = 0; i < allLagers.Count; i++) {
//					if(allLagers[i].id == id){
//						allLagers.RemoveAt(i);
//						break;
//					}
//				}
//				saveBigItems (allLagers);
//			}

//			public List<LagerObject> loadBigItems ()
//			{
//				return LoadLagerObjects("BigItems.txt");
//
//			}
//
//			public List<LagerObject> LoadLagerObjects(string myFile){
//				var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
//				var filename = Path.Combine(documents, myFile);
//				Console.WriteLine (filename);
//
//				if (File.Exists (filename)) {
//					var text = File.ReadAllText(filename);
//					Console.WriteLine (text);
//
//					Console.WriteLine("HALT");
//					string[] items = File.ReadAllText(filename).Split(';');
//					int size = items.Length -1;
//					Console.WriteLine("size:"+size);
//
//
//
//					List<LagerObject> output = new List<LagerObject>();
//					for(int i = 0; i < size; i++){
//						LagerObject o = new LagerObject();
//						String[] tekst = items[i].Split (',');
//	//					o.id = Convert.ToInt32(tekst[0]);
//						o.Name = tekst[0];
//						o.subtitle = tekst[1];
//						o.type = tekst[2];
//	//					int offset = 3;
//	//					List<String> bilder = new List<string>();
//	//					for(int j = offset+1; j < tekst.Length; j++){
//	//						bilder[j-offset+1] = tekst[j];
//	//						Console.WriteLine("j="+j+":"+tekst[j]);
//	//					}
//	//					bilder.Add(tekst[3]);
//						o.imageFileName = tekst[3];
//						output.Add(o);
//					}
//
//					return output; 
//				} else {
//					return null; 
//				}
//			}
//
//
//			public void saveBigItems (List<LagerObject> saves)
//			{
//				var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
//				var filename = Path.Combine(documents, "BigItems.txt");
//				var text = "";
//				for (int i = 0; i < saves.Count; i++) {
//					text += saves[i].outputstring();
//				}
//				File.WriteAllText(filename, text);
//				Console.WriteLine (""+filename +": saved");
//
//			}

//			public void saveBigItem (LagerObject myObject)
//			{
//				Console.WriteLine ("output myObject to save:"+myObject.outputstring());
//				List<LagerObject> all = loadBigItems ();
//
//				if (all == null) {
//					Console.WriteLine("starting a new list");
//					all = new List<LagerObject>();
//				}
//
//				bool found = false;
//				int pos = -1;
//				for (int i = 0; i < all.Count; i++) {
//					if(all[i].Name.CompareTo(myObject.Name) == 0){
//						pos = i;
//						found = true;
//					}
//				}
//
//				if (!found)
//					all.Add (myObject);
//				else {
//					all[pos] = myObject;
//				}
//
//				this.saveBigItems (all);
//			}
////
//
//
					public UIImage loadUserImage (string image)
					{
						var pictures = Environment.GetFolderPath (Environment.SpecialFolder.MyPictures);
						var filename = Path.Combine (pictures, image);
						try{
							return UIImage.FromFile (filename);
						}catch(Exception e)
						{
							Console.WriteLine("error:"+e.ToString());
							return null;
							//TODO Load defaultimage?
						}
					}
				}
}
