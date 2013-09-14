using System;
using System.Linq;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;

namespace TipOfTheDay.Android.Sample {
	[Activity (Label = "TipOfTheDay.Android.Sample", MainLauncher = true)]
	public class Activity1 : Activity {

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			var button = FindViewById (Resource.Id.ShowTipButton);
			button.Click += (s, e) => TipOfTheDay.TipOfTheDayControl<TipsProvider>.ForceActivate (this);

			TipOfTheDay.TipOfTheDayControl<TipsProvider>.Activate (this);
		}
	}
}


