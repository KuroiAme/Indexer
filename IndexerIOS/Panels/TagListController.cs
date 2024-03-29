﻿using System;
using MonoTouch.UIKit;
using System.Drawing;
using System.Collections.Generic;

namespace no.dctapps.commons.events
{
	public class TagListController : UtilityViewController
	{
		ImageTag tag;
		String[] taglist;
		char[] sep = {' ',','};
		RectangleF area;
		//float heightmod;

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

		protected override void Dispose (bool disposing)
		{
			tag = null;
			taglist = null;
			sep = null;
			base.Dispose (disposing);
		}

		/// <summary>
		/// Release everything not in use
		/// </summary>
		void cleanup ()
		{
			//this.Dispose ();
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

		public TagListView tlv;
		public UITextField entertag;

		public override void LoadView ()
		{
			base.LoadView ();
			RectangleF myFrame = new RectangleF (area.X, area.Y, area.Width, area.Height + 30);
			this.View.Frame = myFrame;
			this.View.BackgroundColor = UIColor.Clear;
		}

		public override void ViewDidLoad ()
		{
			Console.WriteLine ("taglist():viewDidLoad()");
			base.ViewDidLoad ();
			//float y = 50f; //TODO get dynamic value, this is a hack.
//			RectangleF frame = new RectangleF (0, y, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height * 0.75f);
			//this.NavigationController.Title = "edit tag";
			tlv = new TagListView (this.View.Bounds, taglist);
			View.AddSubview (tlv);
			//this.View.BackgroundColor = UIColor.White;
			tlv.TagStringClicked += (object sender, TagStringClickedEventArgs e) => EditTagString (e.tagstring, e.pos);

//			var doubletap = new UITapGestureRecognizer (AddTag);
//			doubletap.NumberOfTapsRequired = 2;
//			tlv.AddGestureRecognizer (doubletap);
//			tlv.SetNeedsDisplay ();

			entertag = new UITextField(new RectangleF(area.X, area.Height, area.Width, 22));
			var def = entertag.Text;
			var enter = MonoTouch.Foundation.NSBundle.MainBundle.LocalizedString ("Enter tag text", "Enter tag text");
			entertag.TextAlignment = UITextAlignment.Center;
			entertag.Placeholder = enter;
			entertag.Ended += (object sender, EventArgs e) => {
				var text = entertag.Text;
				entertag.Text = def;
				saveTagText(text);
				entertag.Placeholder = enter;
			};
			entertag.EditingDidBegin += (object sender, EventArgs e) => entertag.Placeholder = "";
			this.entertag.ShouldReturn += (textField) => {
				textField.ResignFirstResponder();
				return true;
			};


			Add (entertag);

		}

		void saveTagText (string tagText)
		{
			AddTagString (tagText);
			tlv.UpdateTagList (taglist);
			tag.StoreTagList (taglist);
			AppDelegate.dao.SaveTag (tag);
		}

		void AddTag (UITapGestureRecognizer gestureRecognizer){
			Console.WriteLine ("addsubtag()");

			UIAlertView av = new UIAlertView ("input tags, comma seperated", "\n", null, "Cancel", new string[] {"Create"});
			Console.WriteLine(gestureRecognizer.LocationOfTouch (0, tlv));
			av.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
			av.Clicked += (object sender, UIButtonEventArgs e) => {
				String tagText = av.GetTextField (0).Text;
				saveTagText (tagText);
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
