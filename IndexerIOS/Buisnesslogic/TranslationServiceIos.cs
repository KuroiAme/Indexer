using System;

namespace GarageIndex 
{
	public class TranslationServiceIos : ITranslationService
	{
		#region ITranslationService implementation

		public string getTranslatedText (string text)
		{
			return MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString (text, text);
		}

		public string getTranslatedText (string text, string comment)
		{
			return MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString (text, comment);
		}

		#endregion

		public TranslationServiceIos ()
		{
		}
	}
}

