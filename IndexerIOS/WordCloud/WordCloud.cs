//using MonoTouch.UIKit;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Collections.Specialized;
//using System.ComponentModel;
//using System.Linq;
//using System.Drawing;
//
//namespace IndexerIOS
//{
//
//	public class WordCloud : UIViewController
//	{
//		public static ObservableCollection<WordCloudEntry> Entries{ get; set;}
//		public static double LargestSizeWidthProportion{get; set;}
//		public static double MinFontSize {get; set;}
//		public static UIColor FromColor { get; set;}
//		public static int MaxWords { get; set;}
//		public static ObservableCollection<int> SelectedItems{ get; set;}
//		public static UIColor SelectedColor{ get; set;}
//		public static int FromAlpha{ get; set;}
//		public static int ToAlpha{ get; set;}
//		public static double AngleCenterValue{ get; set;}
//		public static double MinimumLargestAngleValue{ get; set;}
//		public static double MaximumLowestAngleValue{ get; set;}
//
//		private int _delay;
//
//		private UIImage _image;
//		//private Grid _layoutRoot;
//		private int[] _mapping;
//		private double _minimumLargestValue = 6;
//		private double _minimumValue = double.NaN;
//		private Random _random;
//		//private WriteableBitmap _source;
//
//		private INotifyCollectionChanged _entries;
//		private INotifyCollectionChanged _selected;
//
//
//
//		public WordCloud()
//		{
//			AngleCenterValue = double.NaN;
//			Entries = new ObservableCollection<WordCloudEntry> ();
//			LargestSizeWidthProportion = 0.50;
//			MinFontSize = 11.0;
//			FromColor = UIColor.Black;
//			MaxWords = 150;
//			SelectedItems = new ObservableCollection<int> ();
//			SelectedColor = UIColor.White;
//			FromAlpha = 0x40;
//			ToAlpha = 0xEE;
//			MinimumLargestAngleValue = double.NaN;
//			MaximumLowestAngleValue = double.NaN;
//
////			DefaultStyleKey = typeof(WordCloud);
////			_timer.Tick += TimerTick;
//			//OnEntriesChanged(new DependencyPropertyChangedEventArgs());
//			//OnSelectedItemsChanged(new DependencyPropertyChangedEventArgs());
//		}
//
//		public double MinimumValue
//		{
//			get { return _minimumValue; }
//			set
//			{
//				_minimumValue = value;
////				OnPropertyChanged("MinimumValue");
//			}
//		}
//
//		public double MinimumLargestValue
//		{
//			get { return _minimumLargestValue; }
//			set
//			{
//				_minimumLargestValue = value;
////				OnPropertyChanged("MinimumLargestValue");
//			}
//		}
//
//		public WordCloudEntry GetEntry(PointF pt)
//		{
//			if (pt.X < 0 || pt.X >= View.Bounds.Width || pt.Y < 0 || pt.Y >= View.Bounds.Height)
//				return null;
//			int idx = _mapping[((int)(pt.Y / 4) * (View.Bounds.Width / 4)) + (int)pt.X / 4];
//			return idx == -1 ? null : Entries[idx];
//		}
//
//
//
//
//		private UIColor GetInterpolatedBrush(double value, UIColor u)
//		{
//			float red;
//			float green;
//			float blue;
//			float alpha;
//			u.GetRGBA(out red,out red,out green,out blue,out alpha);
//			UIColor neo = UIColor.FromRGBA (red, green, blue, FromAlpha + (int)(alpha * (ToAlpha - FromAlpha)));
//			return neo;
//		}
//
//
//		public UIImage GenerateCloud()
//		{
//			float mywidth = View.Bounds.Width;
//			float myheight = View.Bounds.Height;
//
//			//_source = new WriteableBitmap((int)mywidth, (int)_layoutRoot.ActualHeight);
//			UIGraphics.BeginImageContext (new System.Drawing.SizeF (myheight, myheight));
//			int arraySize = (int)((mywidth / 4) + 2) * (int)((myheight / 4) + 2);
//			_mapping = new int[arraySize];
//			for (int i = 0; i < arraySize; i++) _mapping[i] = -1;
//
//			if (Entries.Count > 2) {
//				_random = new Random (10202);
//
//				double minSize = Entries.OrderByDescending (e => e.SizeValue).Take (MaxWords).Min (e => e.SizeValue);
//				if (!double.IsNaN (MinimumValue))
//					minSize = Math.Min (MinimumValue, minSize);
//				double maxSize = Math.Max (Entries.Max (e => e.SizeValue), MinimumLargestValue);
//				double range = Math.Max (0.00001, maxSize - minSize);
//				double minColor = Entries.OrderByDescending (e => e.SizeValue).Take (MaxWords).Min (e => e.ColorValue);
//				double maxColor = Entries.OrderByDescending (e => e.SizeValue).Take (MaxWords).Max (e => e.ColorValue);
//				double maxAngle = Entries.OrderByDescending (e => e.SizeValue).Take (MaxWords).Max (e => e.Angle);
//				if (!double.IsNaN (MinimumLargestAngleValue))
//					maxAngle = Math.Max (MinimumLargestAngleValue, maxAngle);
//				double minAngle = Entries.OrderByDescending (e => e.SizeValue).Take (MaxWords).Min (e => e.Angle);
//				if (!double.IsNaN (MaximumLowestAngleValue))
//					minAngle = Math.Min (MaximumLowestAngleValue, minAngle);
//
//
//				double colorRange = Math.Max (0, maxColor - minColor);
//
//				double angleRange = Math.Max (0, maxAngle - minAngle);
//				//If there's a centre value then specify the range
//				if (!double.IsNaN (AngleCenterValue)) {
//					var lr = AngleCenterValue - minAngle;
//					var ur = maxAngle - AngleCenterValue;
//					angleRange = Math.Max (ur, lr);
//				}
//
//
////				var txt = new TextBlock {
////					FontFamily = FontFamily,
////					FontSize = 100,
////					Text = "x"
////				};
//
////				UILabel txt = new UILabel();
////				txt.Font = UIFont.FromName ("Helvetica", 100);
////				txt.Text = "x";
////				txt.
//
//				float OneLettersize = 100;
//
//
//				double areaPerLetter = ((OneLettersize)) / (range);
//
//				double targetWidth = mywidth * LargestSizeWidthProportion;
//				WordCloudEntry od = Entries.OrderByDescending (e => (e.SizeValue - minSize) * e.Word.Length).First ();
//				double maxFontSize = Math.Max (MinFontSize * 2.7, 100 / (((od.SizeValue - minSize) * od.Word.Length * areaPerLetter) / targetWidth));
//				double fontMultiplier = Math.Min ((maxFontSize - MinFontSize) / range, 200);
//
//				var points = new[] {
//					new PointF ((int)(mywidth / 2), (int)(myheight / 2)),
//					new PointF ((int)(mywidth / 4), (int)(myheight / 4)),
//					new PointF ((int)(mywidth / 4), (int)(3 * myheight / 2)),
//					new PointF ((int)(3 * mywidth / 4), (int)(myheight / 2)),
//					new PointF ((int)(3 * mywidth / 4), (int)(3 * myheight / 4))
//				};
//
//
//				int currentPoint = 0;
//				foreach (WordCloudEntry e in Entries.OrderByDescending(e => e.SizeValue).Take(MaxWords)) {
//					again:
//					double position = 0.0;
//					PointF centre = points [currentPoint];
//
//					double angle = 0.0;
//					if (double.IsNaN (AngleCenterValue)) {
//						angle = angleRange >= 0.01 ? -90 + (((e.Angle - minAngle) / angleRange) * 90) : 0;
//					} else {
//						angle = angleRange >= 0.01 ? 90 * ((e.Angle - AngleCenterValue) / angleRange) : 0;
//					}
//					WriteableBitmap bm = CreateImage (e.Word,
//						                    ((e.SizeValue - minSize) * fontMultiplier) + MinFontSize,
//						                    SelectedItems.Contains (Entries.IndexOf (e)) ? -1 : (colorRange >= 0.01 ? (e.ColorValue - minColor) / colorRange : 1), e.Color,
//						                    angle);
//					Dictionary<PointF, List<PointF>> lst = CreateCollisionList (bm);
//					bool collided = true;
//					do {
//						Point spiralPoint = GetSpiralPoint (position);
//						int offsetX = (bm.PixelWidth / 2);
//						int offsetY = (bm.PixelHeight / 2);
//						var testPoint = new Point ((int)(spiralPoint.X + centre.X - offsetX), (int)(spiralPoint.Y + centre.Y - offsetY));
//						if (position > (2 * Math.PI) * 580) {
//							if (++currentPoint >= points.Length)
//								goto done;
//							goto again;
//						}
//						int cols = CountCollisions (testPoint, lst);
//						if (cols == 0) {
//							tryagain:
//							double oldY = testPoint.Y;
//							if (Math.Abs (testPoint.X + offsetX - centre.X) > 10) {
//								if (testPoint.X + offsetX < centre.X) {
//									do {
//										testPoint.X += 2;
//									} while (testPoint.X + offsetX < centre.X && CountCollisions (testPoint, lst) == 0);
//									testPoint.X -= 2;
//								} else {
//									do {
//										testPoint.X -= 2;
//									} while (testPoint.X + offsetX > centre.X && CountCollisions (testPoint, lst) == 0);
//									testPoint.X += 2;
//								}
//							}
//							if (Math.Abs (testPoint.Y + offsetY - centre.Y) > 10) {
//								if (testPoint.Y + offsetY < centre.Y) {
//									do {
//										testPoint.Y += 2;
//									} while (testPoint.Y + offsetY < centre.Y && CountCollisions (testPoint, lst) == 0);
//									testPoint.Y -= 2;
//								} else {
//									do {
//										testPoint.Y -= 2;
//									} while (testPoint.Y + offsetY > centre.Y && CountCollisions (testPoint, lst) == 0);
//									testPoint.Y += 2;
//								}
//								if (testPoint.Y != oldY)
//									goto tryagain;
//							}
//
//
//							collided = false;
//							CopyBits (testPoint, bm, lst, Entries.IndexOf (e));
//						} else {
//							if (cols <= 2) {
//								position += (2 * Math.PI) / 100;
//							} else
//								position += (2 * Math.PI) / 40;
//						}
//					} while (collided);
//				}
//			}
////			done:
////			_image.Source = _source;
////			_image.InvalidateArrange();
//		}
//
//		private int CountCollisions(PointF testPoint, Dictionary<PointF, List<PointF>> lst)
//		{
//			int testRight = GetCollisions(new Point(testPoint.X + 2, testPoint.Y), lst);
//			int testLeft = GetCollisions(new Point(testPoint.X - 2, testPoint.Y), lst);
//			int cols = GetCollisions(testPoint, lst) + testRight + testLeft + GetCollisions(new PointF(testPoint.X, testPoint.Y + 2), lst) + GetCollisions(new PointF(testPoint.X, testPoint.Y - 2), lst);
//			return cols;
//		}
//
//
//		//Property MinimumValue
//
//
//		private void CopyBits(Point testPoint, WriteableBitmap bm, Dictionary<Point, List<Point>> lst, int index)
//		{
//			int pixelWidth = _source.PixelWidth;
//			int mapWidth = pixelWidth / 4;
//			int width = bm.PixelWidth;
//
//			foreach (Point pt in lst.SelectMany(e => e.Value))
//			{
//				int[] pixels = _source.Pixels;
//				int[] sourcePixels = bm.Pixels;
//				if ((pt.X + testPoint.X) >= 0 && (pt.X + testPoint.X) < _source.PixelWidth && (pt.Y + testPoint.Y) >= 0 && (pt.Y + testPoint.Y) < _source.PixelHeight)
//				{
//					pixels[(int)((testPoint.Y + pt.Y) * pixelWidth) + (int)(pt.X + testPoint.X)] = sourcePixels[(int)(pt.Y * width) + (int)pt.X];
//				}
//			}
//			int sx = (int)testPoint.X / 4;
//			int sy = (int)testPoint.Y / 4;
//			foreach (Point pt in lst.Select(e => e.Key))
//			{
//				_mapping[(int)(pt.Y + sy) * mapWidth + (int)(pt.X + sx)] = index;
//				_mapping[(int)(pt.Y + sy + 1) * mapWidth + (int)(pt.X + sx)] = index;
//				_mapping[(int)(pt.Y + sy + 1) * mapWidth + (int)(pt.X + 1 + sx)] = index;
//				_mapping[(int)(pt.Y + sy) * mapWidth + (int)(pt.X + 1 + sx)] = index;
//			}
//		}
//
//		private Point GetSpiralPoint(double position, double radius = 7)
//		{
//			double mult = position / (2 * Math.PI) * radius;
//			double angle = position % (2 * Math.PI);
//			return new Point((int)(mult * Math.Sin(angle)), (int)(mult * Math.Cos(angle)));
//		}
//
//		private WriteableBitmap CreateImage(string text, double size = 100, double colorValue = 1, Color wordColor = default(Color), double angle = 0)
//		{
//			if (text == string.Empty)
//				return new WriteableBitmap(0, 0);
//			var txt = new TextBlock
//			{
//				FontFamily = FontFamily,
//				FontSize = Math.Max(size, MinFontSize),
//				Text = text,
//				Foreground = colorValue >= 0 ? GetInterpolatedBrush(colorValue, wordColor) : SelectedColor
//			};
//			//   txt.Effect = new DropShadowEffect() { ShadowDepth = 2, BlurRadius = 4, Color = Colors.Red, Direction = 0 };
//
//			double largest = Math.Max(Math.Ceiling(txt.ActualHeight), Math.Ceiling(txt.ActualWidth)) * 1.2;
//			var bm = new WriteableBitmap((int)Math.Ceiling(largest), (int)Math.Ceiling(largest));
//
//			var tfg = new TransformGroup();
//			var rot = new RotateTransform
//			{
//				Angle = angle,
//				CenterX = 0.5,
//				CenterY = 0.5
//			};
//			tfg.Children.Add(rot);
//
//			var comp = new CompositeTransform
//			{
//				Rotation = angle,
//				CenterX = txt.ActualWidth / 2,
//				CenterY = txt.ActualHeight / 2,
//				TranslateY = largest > txt.ActualHeight ? ((largest - txt.ActualHeight) / 2) : 0,
//				TranslateX = largest > txt.ActualWidth ? ((largest - txt.ActualWidth) / 2) : 0
//			};
//
//			bm.Render(txt, comp);
//			bm.Invalidate();
//
//
//			return bm;
//		}
//
//
//		private Dictionary<PointF, List<Point>> CreateCollisionList(RectangleF bmp)
//		{
//			var l = new List<PointF>();
//			int pixelHeight = (float) bmp.Height;
//			var lookup = new Dictionary<PointF, List<PointF>>();
//
//			for (int y = 0; y < pixelHeight; y++)
//			{
//				int pixelWidth = (int) bmp.Width;
//				for (int x = 0; x < pixelWidth; x++)
//				{
//					int[] pixels = bmp.Pixels;
//					if (pixels[y * pixelWidth + x] != 0)
//					{
//						var detailPoint = new Point(x, y);
//						l.Add(detailPoint);
//						var blockPoint = new Point(((x / 4)), ((y / 4)));
//						if (!lookup.ContainsKey(blockPoint))
//						{
//							lookup[blockPoint] = new List<PointF>();
//						}
//						lookup[blockPoint].Add(detailPoint);
//					}
//				}
//			}
//			return lookup;
//		}
//
//		private int GetCollisions(PointF pt, Dictionary<PointF, List<PointF>> list)
//		{
//			int[] pixels = _source.Pixels;
//			int pixelWidth = _source.PixelWidth;
//			int mapWidth = (_source.PixelWidth / 4);
//
//			int c = 0;
//			foreach (var pair in list)
//			{
//				var testPt = new Point(pt.X + pair.Key.X * 4, pt.Y + pair.Key.Y * 4);
//				if (testPt.X < 0 || testPt.X >= _source.PixelWidth || testPt.Y < 0 || testPt.Y >= _source.PixelHeight)
//					return 1;
//				int pos = ((((int)pair.Key.Y + (int)(pt.Y / 4)) * mapWidth) + (int)pair.Key.X + ((int)(pt.X / 4)));
//				try
//				{
//					if (_mapping[pos] != -1 || _mapping[pos + 1] != -1 || _mapping[pos + mapWidth] != -1 || _mapping[pos + mapWidth + 1] != -1)
//					{
//						foreach (Point p in pair.Value)
//						{
//							var nx = (int)(p.X + pt.X);
//							var ny = (int)(pt.Y + p.Y);
//							if (nx < 0 || nx >= _source.PixelWidth || ny < 0 || ny >= _source.PixelHeight)
//								return 1;
//							if (pixels[ny * pixelWidth + nx] != 0) return 1;
//						}
//					}
//				}
//				catch (Exception)
//				{
//					return 1;
//				}
//			}
//			return 0;
//		}
//
////		private static void CloudWidthChanged(
////			object sender,
////			DependencyPropertyChangedEventArgs e)
////		{
////			var owner = (WordCloud)sender;
////			owner.OnCloudWidthChanged(e);
////		}
//
////		protected void OnCloudWidthChanged(DependencyPropertyChangedEventArgs e)
////		{
////			RegenerateCloud();
////		}
////
//
////		public override void OnApplyTemplate()
////		{
////			_image = GetTemplateChild("Image") as Image ?? new Image();
////			_layoutRoot = GetTemplateChild("LayoutRoot") as Grid ?? new Grid();
////			_layoutRoot.SizeChanged += LayoutRootSizeChanged;
////			RegenerateCloud();
////			base.OnApplyTemplate();
////		}
//
////		private void LayoutRootSizeChanged(object sender, SizeChangedEventArgs e)
////		{
////			RegenerateCloud();
////			OnPropertyChanged("WordCloudImage");
////		}
////
////
////		public void OnPropertyChanged(string name)
////		{
////			PropertyChangedEventHandler handler = PropertyChanged;
////			if (handler != null) handler(this, new PropertyChangedEventArgs(name));
////		}
////
//		#region Nested type: WordCloudEntry
//
//		public class WordCloudEntry
//		{
//			public string Word { get; set; }
//			public double SizeValue { get; set; }
//			public double ColorValue { get; set; }
//			public object Tag { get; set; }
//			public double Angle { get; set; }
//			public Color Color { get; set; }
//		}
//
//		#endregion
//	}
//}
//
