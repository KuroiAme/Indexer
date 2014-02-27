using System;

namespace GarageIndex
{
	public interface ITranslationService
	{
		string getTranslatedText(string text);
		string getTranslatedText(string text, string comment);
	}
}

