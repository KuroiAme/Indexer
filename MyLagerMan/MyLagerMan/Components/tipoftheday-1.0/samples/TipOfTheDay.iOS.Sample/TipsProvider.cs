using System;

using TipOfTheDay;

namespace App
{
	public class TipsProvider : ITipsProvider
	{
		static readonly string yoda = "Yoda is a character in the Star Wars universe, appearing in the second and third original films, as well as all three prequel trilogy films. " +
			"A renowned Jedi master, Yoda made his first on-screen appearance in Star Wars Episode V: " +
				"The Empire Strikes Back where he is responsible for training Luke Skywalker in the ways of the Jedi. " +
				"His final chronological appearance is Star Wars Episode VI: Return of the Jedi, when he dies peacefully at the age of 900. " +
				"He also appears extensively in the two animated Clone Wars series, as well as the Star Wars Expanded Universe of novels and comic books." +
				Environment.NewLine + Environment.NewLine +
				"In the original films, he trains Luke Skywalker to fight against the evil Galactic Empire. " +
				"In the prequel films, he serves as Grand Master of the Jedi Council and as a general in the Clone Wars." +
				Environment.NewLine + Environment.NewLine +
				"In 2008, Yoda was selected by Empire magazine as the 25th greatest movie character of all time. " +
				"On their list of the 100 Greatest Fictional Characters, Fandomania.com ranked Yoda at number 60." +
				Environment.NewLine + Environment.NewLine +
				"A lifesize statue of Yoda greets visitors to Lucasfilm's Letterman Digital Arts Center in San Francisco's Presidio, the headquarters of Industrial Light and Magic.";
		static readonly string vader = "Darth Vader (born Anakin Skywalker) is the central character of the Star Wars saga, " +
			"appearing as one of the main antagonists of the original trilogy and as the main protagonist of the prequel trilogy." +
				Environment.NewLine + Environment.NewLine +
				"The character was created by George Lucas and numerous actors have portrayed him. " +
				"His appearances span all six Star Wars films, and he is an important character in the expanded universe of television series, video games, novels, literature and comic books. " +
				"The films establish that there was originally a Jedi Knight named Anakin who fell to the dark side of the Force; " +
				"he is also revealed to be the father of both Luke Skywalker and Princess Leia Organa, the two main protagonists of the original trilogy. " +
				"After turning to the Dark Side, Vader is a ruthless henchman of Emperor Palpatine; he secretly intends to overthrow Palpatine to establish himself as ruler of the Empire. " +
				"Vader is ultimately revealed to have personal honor in Star Wars Episode VI: Return of the Jedi, when he sacrifices himself to save his son, Luke.";
		static readonly string luke = "Luke Skywalker is a fictional character and the main protagonist of the original film trilogy of the Star Wars franchise, " +
			"where he is portrayed by Mark Hamill. He is introduced in Star Wars Episode IV: " +
				"A New Hope, in which he is forced to leave home, and finds himself apprenticed to the Jedi master Obi-Wan Kenobi. " +
				"He soon becomes an important figure in the Rebel Alliance, leading the struggle against the Galactic Empire. " +
				"As the son of former Queen of Naboo and Republic Senator Padmé Amidala and her husband, " +
				"Anakin Skywalker—a fallen Jedi also known as the Sith Lord Darth Vader—Luke is heir to a family deeply powerful in the Force. " +
				"He is also the twin brother of Princess Leia Organa of the planet Alderaan, " +
				"foster daughter of Senator Bail Organa and a leader of the Rebellion who recruited her brother into the cause. " +
				"The Expanded Universe depicts him as a powerful Jedi Master and eventually the Grand Master of the New Jedi Order, " +
				"as well as the father of Ben Skywalker, maternal uncle of Jacen Solo and ancestor of Cade Skywalker." +
				Environment.NewLine + Environment.NewLine +
				"In 2008, Luke Skywalker was selected by Empire magazine as the 54th greatest movie character of all time. " +
				"On their list of the 100 Greatest Fictional Characters, Fandomania.com ranked Luke at number 14.";
		readonly string[] tips = new string[]{ yoda, vader, luke };
		
		public TipsProvider ()
		{
		}
		
		public string GetTip (int index)
		{
			return tips [index];
		}
		
		public int TipsCount {
			get {
				return tips.Length;
			}
		}
	}
}

