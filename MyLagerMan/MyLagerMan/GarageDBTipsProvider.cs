//using System;
//using TipOfTheDay;
//
//namespace no.dctapps.garagedatabase
//{
//	public class GarageDBTipsProvider : ITipsProvider
//	{
//			static readonly string[] Tips = {
//			MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString 
//			("Box Identifier can be your initials pluss a number that you can use to write on your boxes so you know for sure",
//			 "Box Identifier can be your initials pluss a number that you can use to write on your boxes so you know for sure"
//			 ),
//			MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString 
//			("The Pro version adds support for adding images to items, includes support for multiple storages and removes adds",
//			 "The Pro version adds support for adding images to items, includes support for multiple storages and removes adds"
//			 ),
//			MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString 
//			("swipe to delete things",
//			 "swipe to delete things"
//			 )
//			};
//			
//			public string GetTip (int index)
//			{
//				return Tips[index];
//			}
//			
//			public int TipsCount {
//				get { return Tips.Length; }
//			}
//	}
//}