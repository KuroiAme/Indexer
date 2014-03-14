using System;
using MonoTouch.UIKit;
using System.Drawing;
using IndexerIOS;
using Xamarin.Social.Services;
using Xamarin.Social;
using System.Collections.Generic;

namespace GarageIndex
{
	public class DashboardRightPanel : UIViewController
	{
		RectangleF myFrame;
		UIView parentView;

		const float elementHeight = 175;
		const float buffer = 10;
		const float textHeight = 30;
		float currentheight = 0;
		float rightPanelWidth;
		
		UIViewController ancestor;

		public DashboardRightPanel (float rightPanelWidth, UIViewController ancestor)
		{
			this.rightPanelWidth = rightPanelWidth;
			this.ancestor = ancestor;
		}

		static float GetPanelHeight ()
		{
			return elementHeight + buffer;
		}

		public SizeF getSize(){
			return new SizeF (rightPanelWidth, GetPanelHeight ());
		}

		protected override void Dispose (bool disposing)
		{
			parentView = null;
			ancestor = null;
			clouds = null;
			wordCloud = null;

			base.Dispose (disposing);
		}

		/// <summary>
		/// Release everything not in use
		/// </summary>
		void cleanup ()
		{
			Dispose ();
		}


		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			//cleanup only if view is loaded and not in a window.
			if(this.IsViewLoaded && this.View.Window == null){
				//cleanup ();
			}
			// Release any cached data, images, etc that aren't in use.
		}

		public override void LoadView ()
		{
			base.LoadView ();

			this.View.BackgroundColor = UIColor.Clear;
			this.View.Frame = new RectangleF (0, 0, rightPanelWidth, GetPanelHeight());
			// init done, now for panel elements;

		}



		WordCloudIOS wordCloud;

		System.Collections.Generic.List<WordCloudItem> clouds;

		UITapGestureRecognizer doubletap;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
//			MainMap = new OverSightMap (new RectangleF (0, currentheight, rightPanelWidth, elementHeight), ancestor);
//			View.AddSubview (MainMap.View);
//			currentheight += elementHeight + buffer;

			clouds = AppDelegate.bl.GetWordCloudDictionary ();

			wordCloud = new WordCloudIOS (ancestor, clouds, new RectangleF (0, currentheight, rightPanelWidth, elementHeight * 1.5f));
			View.AddSubview (wordCloud.View);

			doubletap = new UITapGestureRecognizer (Share);
			doubletap.NumberOfTapsRequired = 2;
			wordCloud.View.AddGestureRecognizer (doubletap);
			wordCloud.View.UserInteractionEnabled = true;
			this.View.UserInteractionEnabled = true;


		}

		UIActionSheet shareOptions;

		private void Share(UIGestureRecognizer recognizer){
			shareOptions = new UIActionSheet (AppDelegate.its.getTranslatedText ("share the WordCloud on what social network?"));
			shareOptions.AddButton (AppDelegate.its.getTranslatedText ("cancel"));
			shareOptions.AddButton ("Facebook");
			shareOptions.AddButton ("Twitter");
			shareOptions.Clicked += (object sender, UIButtonEventArgs e) => {
				if(e.ButtonIndex == 0){
					//cancel
					Console.WriteLine("user didnt want to share");
				}
				if(e.ButtonIndex == 1){
					ShareOnFacebook();
				}
				if(e.ButtonIndex == 2){
					shareOnTwitter();
				}


			};

			shareOptions.ShowInView (ancestor.View);
		}

		IList<ImageData> GetWordCloudAsImage ()
		{
			IList<ImageData> images = new List<ImageData> ();
			ImageData myCloudImage = new ImageData (wordCloud.GetCloudAsImage());
			images.Add (myCloudImage);
			return images;
		}

//		UIImage ImageWithView (UIView view){
//			UIGraphics.BeginImageContextWithOptions (wordCloud.View.Bounds.Size, view.Opaque, 1.0f);
//			view.Layer.RenderInContext (UIGraphics.GetCurrentContext ());
//			UIImage img = UIGraphics.GetImageFromCurrentImageContext ();
//			UIGraphics.EndImageContext ();
//			return img;
//		}

		void ShareOnFacebook ()
		{
			// 1. Create the service
			var facebook = new FacebookService {
				ClientId = "662403690489016",
				RedirectUrl = new Uri ("https://apps.facebook.com/no_dctapps_indexer/")
			};

			// 2. Create an item to share
			var item = new Item { Text = "This is my stuff from indexer as a wordcloud!" };
			item.Links.Add (new Uri ("http://bit.ly/1hEfVgs"));
			item.Images = GetWordCloudAsImage (); 

			// 3. Present the UI on iOS
			var shareController = facebook.GetShareUI (item, result => {
				// result lets you know if the user shared the item or canceled
				ancestor.NavigationController.DismissViewController(true,null);
			});
			ancestor.NavigationController.PresentViewController (shareController, true, null);
		}

		async void DeleteMyTwitterAccount (TwitterService Twitter)
		{
			IEnumerable<Xamarin.Auth.Account> accounts = await Twitter.GetAccountsAsync ();
			foreach (Xamarin.Auth.Account account in accounts) {
				Twitter.DeleteAccount (account);
			}
		}

		void shareOnTwitter ()
		{
			TwitterService Twitter = new TwitterService {
				ConsumerKey = "8Jmm1RRHPN6wlEVlqjnQQ",
				ConsumerSecret = "kgp6Wzm0W2CtUHvkUgWqxUTJTRAUoFsgeu1tLo0N4",
				CallbackUrl = new Uri ("http://www.dctapps.no")
			};

			var item = new Item { Text = "This is my stuff from #indexer as a wordcloud!" };
			item.Links.Add (new Uri ("http://bit.ly/1hEfVgs"));
			item.Images = GetWordCloudAsImage (); 

			//DeleteMywitteryAccount (Twitter);

			// 3. Present the UI on iOS
			var shareController = Twitter.GetShareUI (item, result => {
				// result lets you know if the user shared the item or canceled
				ancestor.NavigationController.DismissViewController(true,null);
			});
			ancestor.NavigationController.PresentViewController (shareController, true, null);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			currentheight = 0;



			currentheight += elementHeight;

			if (clouds == null) {
				clouds = AppDelegate.bl.GetWordCloudDictionary ();
			}

			if (wordCloud == null) {
				wordCloud = new WordCloudIOS (ancestor, clouds, new RectangleF (0, currentheight, rightPanelWidth, elementHeight * 1.5f));
				View.AddSubview (wordCloud.View);
			}
		}






		void RaiseSearchResult (no.dctapps.Garageindex.model.Lager find)
		{
			Console.WriteLine ("foo");
			//TOD implement me
		}
	}
}

