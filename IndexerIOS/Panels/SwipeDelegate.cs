using System;
using MonoTouch.UIKit;

namespace IndexerIOS
{
	public class SwipeDelegate : UIGestureRecognizerDelegate
	{
		public override bool ShouldRecognizeSimultaneously (UIGestureRecognizer gestureRecognizer, UIGestureRecognizer otherGestureRecognizer)
		{
			return true;
		}
	}
}

