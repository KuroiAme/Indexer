
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using No.Dctapps.Garageindex.Ios.Screens;
using No.Dctapps.GarageIndex;
using GarageIndex;

namespace no.dctapps.Garageindex.screens
{
	public partial class ItemInfo : UIViewController
	{
		Item item;
//		LagerDAO dao;

//		public event EventHandler<ItemSavedEventArgs> DismissInfo;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public ItemInfo (Item item)
			: base (UserInterfaceIdiomIsPhone ? "ItemInfo_iPhone" : "ItemInfo_iPad", null)
		{
			this.item = item;
//			dao = new LagerDAO();
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

//		ThreeChoiceButton button;
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			if(item == null){
				item = new Item();
			}

			Console.WriteLine("Details:"+item.toString());



//			var position = new PointF (10, 10);
//			button = new ThreeChoiceButton (position);
//
//			button.LeftText = "Keep";
//			button.MiddleText = "Sell";
//			button.RightText = "Toss";
//
//			button.State = ThreeChoiceButtonState.Left; //TODO load from item object instead of staticing it
//			
//			button.StateChanged += delegate {
//				Console.WriteLine ("State changed to: {0}", button.State);
//
//				switch(button.State.ToString()){
//				case "Middle":
//					item.Action = "sell";
//					break;
//				case "Left":
//					item.Action = "keep";
//					break;
//				case "Right":
//					item.Action = "toss";
//					break;
//				}
//				Console.WriteLine("Action:"+item.Action);
//				dao.SaveItem(item);
//			};

			this.fieldActionComment.ResignFirstResponder();

			this.fieldActionComment.Ended += (object sender, EventArgs e) => {
				Console.WriteLine("action comment:"+fieldActionComment.Text);
				item.ActionComment = this.fieldActionComment.Text;
				AppDelegate.dao.SaveItem(item);
			};

//			this.btnDismiss.TouchUpInside += (object sender, EventArgs e) => {
//				raiseDismissalevent();
//			};
			
//			View.AddSubview (button);
//			BlackLeatherTheme.Apply(this.View);
			// Perform any additional setup after loading the view, typically from a nib.
		}

//		void raiseDismissalevent ()
//		{
//			var handler = this.DismissInfo;
//			if(handler != null){
//				handler(this, new ItemSavedEventArgs());
//			}
//		}
	}
}

