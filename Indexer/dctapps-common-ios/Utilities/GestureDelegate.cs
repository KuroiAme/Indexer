using System;
using MonoTouch.UIKit;

namespace no.dctapps.commons
{
	public class GestureDelegate : UIGestureRecognizerDelegate
	{
		public override bool ShouldRecognizeSimultaneously (UIGestureRecognizer gestureRecognizer, UIGestureRecognizer otherGestureRecognizer)
		{
			return true;
		}
	}
}

