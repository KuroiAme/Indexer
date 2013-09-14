using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ThreeChoiceButton.Android.Sample
{
	[Activity (Label = "ThreeChoiceButton.Android.Sample", MainLauncher = true)]
	public class Activity1 : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			var threeChoiceButton1 = FindViewById<ThreeChoice.ThreeChoiceButton> (Resource.Id.threeChoiceButton1);

			/*
			 * Also you can add three choice button manually, if you want
			 *
			 * var layoutParams1 = new LinearLayout.LayoutParams (LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
			 * layoutParams1.TopMargin = 20;
			 * layoutParams1.LeftMargin = 20;
			 *
			 * var threeChoiceButton1 = new Xamarin.Controls.ThreeChoiceButton (this);
			 * threeChoiceButton1.LayoutParameters = layoutParams1;
			 *
			 * FindViewById<LinearLayout> (Resource.Id.rootLayout).AddView (threeChoiceButton1);
			 */

			threeChoiceButton1.StateChanged += StateChanged;

			var threeChoiceButton2 = FindViewById<ThreeChoice.ThreeChoiceButton> (Resource.Id.threeChoiceButton2);

			/*
			 * Also you can add three choice button manually, if you want
			 *
			 * var layoutParams2 = new LinearLayout.LayoutParams (LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
			 * layoutParams2.TopMargin = 20;
			 * layoutParams2.RightMargin = 20;
			 * layoutParams2.Gravity = GravityFlags.Right;
			 *
			 * var threeChoiceButton2 = new Xamarin.Controls.ThreeChoiceButton (this);
			 * threeChoiceButton2.LayoutParameters = layoutParams2;
			 * threeChoiceButton2.State = Xamarin.Controls.ThreeChoiceButtonState.Off;
			 * threeChoiceButton2.ExpandDirection = Xamarin.Controls.ThreeChoiceButtonExpandDirection.Left;
			 *
			 * FindViewById<LinearLayout> (Resource.Id.rootLayout).AddView (threeChoiceButton2);
			 */

			threeChoiceButton2.StateChanged += StateChanged;
		}

		void StateChanged (object sender, EventArgs e)
		{
			var button = sender as ThreeChoice.ThreeChoiceButton;
			if (button != null)
				Console.WriteLine ("Auto button {0} State {1}", button.Id, button.State);
		}
	}
}


