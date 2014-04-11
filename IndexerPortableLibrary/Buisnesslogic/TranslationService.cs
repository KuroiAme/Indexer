using System;

namespace no.dctapps.commons.events
{
	public interface ITranslationService
	{
		string getTranslatedText(string text);
		string getTranslatedText(string text, string comment);
	}
}

