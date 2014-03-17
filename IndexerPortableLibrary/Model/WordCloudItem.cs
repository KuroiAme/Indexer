using System;

namespace IndexerIOS
{
	public class WordCloudItem : IComparable<WordCloudItem>
	{
		#region IComparable implementation

		public int CompareTo (WordCloudItem other)
		{
			return weight.CompareTo (other.weight);
		}
			
		#endregion

		public string word{ get; set;}
		public float weight{ get; set;} // preferably 1-100
		public float x{ get; set;}
		public float y{ get; set;}
		public float width{ get; set;}
		public float height{ get; set;}

//		public override bool Equals (Object o)
//		{
//			
//		}
		public override string ToString ()
		{
			return string.Format ("[WordCloudItem: word={0}, weight={1}, x={2}, y={3}, width={4}, height={5}]", word, weight, x, y, width, height);
		}

		public override bool Equals (object obj)
		{


			if (obj == null) {
				return false;
			}

			WordCloudItem another = (WordCloudItem)obj;

			if (string.IsNullOrEmpty(another.word)) {
				return false;
			}

			if (string.IsNullOrEmpty(word)){
				return false;
			}

			return word.Equals (another.word);
			//return base.Equals (obj);


		}

	}
}

