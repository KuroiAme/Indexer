using System;
using NUnit.Framework;
using MonoTouch.UIKit;
using QRCodeGen;
using MonoTouch.Foundation;

namespace GarageIndexTests
{
	[TestFixture]
	public class QRCodeGenTests
	{
		[Test]
		public void Pass ()
		{
			UIImage result = QREncoder.encode((NSString)"Test");
			bool nonempty = (result != null);
			Assert.True (nonempty);
		}

		[Test]
		public void Fail ()
		{
			Assert.False (true);
		}

		[Test]
		[Ignore ("another time")]
		public void Ignore ()
		{
			Assert.True (false);
		}
	}
}