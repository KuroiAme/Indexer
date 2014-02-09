using System;
using MonoTouch.UIKit;
using System.Drawing;
using System.Collections.Generic;

namespace GarageIndex
{
	public class TagListController : UIViewController
	{
		ImageTag tag;
		String[] taglist;
		char[] sep = {' ',','};
		RectangleF area;
		float heightmod;

		public TagListController (ImageTag tag, RectangleF area)
		{
//			this.heightmod = heightmod;
			this.tag = tag;
			this.area = area;
			if (tag.TagString != null) {
				taglist = tag.TagString.Split (sep);
			} else {
				Console.WriteLine ("taglist is null, making empty array");
				taglist = new string[]{ };
			}
		}

		public TagListView tlv;

		public override void ViewDidLoad ()
		{
			Console.WriteLine ("taglist():viewDidLoad()");
			base.ViewDidLoad ();
			//float y = 50f; //TODO get dynamic value, this is a hack.
//			RectangleF frame = new RectangleF (0, y, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height * 0.75f);
			//this.NavigationController.Title = "edit tag";
			tlv = new TagListView (area, taglist);
			this.View = tlv;
			//this.View.BackgroundColor = UIColor.White;
			tlv.TagStringClicked += (object sender, TagStringClickedEventArgs e) => EditTagString (e.tagstring, e.pos);

			var doubletap = new UITapGestureRecognizer (AddTag);
			doubletap.NumberOfTapsRequired = 2;
			tlv.AddGestureRecognizer (doubletap);
			tlv.SetNeedsDisplay ();
		}

		void AddTag (UITapGestureRecognizer gestureRecognizer){
			Console.WriteLine ("addsubtag()");

			UIAlertView av = new UIAlertView ("input tags, comma seperated", "\n", null, "Cancel", new string[] {"Create"});
			Console.WriteLine(gestureRecognizer.LocationOfTouch (0, tlv));
			av.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
			av.Clicked += (object sender, UIButtonEventArgs e) => {
				String tagText = av.GetTextField (0).Text;
				AddTagString(tagText);
				tlv.UpdateTagList(taglist);
				tag.StoreTagList(taglist);
				AppDelegate.dao.SaveTag(tag);
			};

			av.Show();
		}
			
		public void EditTagString (string tagstring, int pos)
		{
			UIAlertView av = new UIAlertView("edit tag", "\n", null, "Cancel", new string[] {"OK", "Delete"});
			av.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
			UITextField tf = av.GetTextField (0);
			tf.Text = tagstring;
			int OK = av.FirstOtherButtonIndex;
			int Del = OK + 1; //TODO prove, total assumption
			av.Clicked += (object sender, UIButtonEventArgs e) => {
				if(e.ButtonIndex == Del){
					DeleteTagStringOf(pos);
					tlv.UpdateTagList (taglist);
					tag.StoreTagList(taglist);
					AppDelegate.dao.SaveTag(tag);
				}
				if(e.ButtonIndex == OK){
					DeleteTagStringOf(pos);
					AddTagString(tf.Text);
					tlv.UpdateTagList (taglist);
					tag.StoreTagList(taglist);
					AppDelegate.dao.SaveTag(tag);
				}
			};
			av.Show ();
		}

		void AddTagString (string text)
		{
			Console.WriteLine ("AddTagString()");
			string[] addstuff = text.Split (sep);
			List<string> newlist = new List<string> ();
			foreach (string s in taglist) {
				if (s != string.Empty) {
					newlist.Add (s);
				}
			}
			foreach (string s in addstuff) {
				if (s != string.Empty) {
					newlist.Add (s);
				}
			}
			taglist = newlist.ToArray ();
		}

		private void DeleteTagStringOf (int pos)
		{
			Console.WriteLine ("DeleteTagStringOf()");
			List<string> newlist = new List<string> ();
			for (int i = 0; i < taglist.Length; i++) {
				if (i != pos) {
					newlist.Add (taglist [i]);
				}
			}
			taglist = newlist.ToArray ();
		}
	}
}

//
//	UIAlertView av = new UIAlertView ("input tags, comma seperated", "\n", null, "Cancel", new string[] {"Create"});
//	av.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
//	av.Clicked += (object sender, UIButtonEventArgs e) => {
//		String tagText = av.GetTextField (0).Text;
//		tag.TagString = tagText;
//		var scale = scrollView.ZoomScale;
//		const float heightmod = 0.70f;
//		//float widthmod = 1f;
//
//		RectangleF contentFrame = new RectangleF(scrollView.ContentOffset.X / scale, scrollView.ContentOffset.Y / scale, scrollView.Frame.Size.Width / scale, scrollView.Frame.Size.Height /scale * heightmod);
//		//contentFrame.Y = contentFrame.Y + this.NavigationController.View.Bounds.Y;
//		//contentFrame.X = contentFrame.X + this.NavigationController.View.Bounds.Bottom;
//		contentFrame.Y = contentFrame.Y + 90;
//		tag.StoreRectangleF(contentFrame);
//		AppDelegate.dao.SaveTag(tag);
//		Console.WriteLine("tagtext:"+tag.TagString);
//		Console.WriteLine("spot:"+tag.FetchAsRectangleF());
//		tgv.SetNeedsDisplay();
//	};
//
//	av.Show();
//}
