using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace IndexerIOS
{
	public class QRIcon
	{
		public static UIImage MakeQR (){
			bool retina = (UIScreen.MainScreen.Scale > 1.0);
			if (retina) {
				UIGraphics.BeginImageContext (new System.Drawing.SizeF (96, 64));
				paintCodeDrawQRRETINA();
			}else{
				UIGraphics.BeginImageContext (new System.Drawing.SizeF (48, 32));
				paintCodeDrawQRNONRetina();
			}

			var converted = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return converted;

		}

		public static void paintCodeDrawQRNONRetina(){
			//// Color Declarations
			UIColor color2 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);

			//// Rectangle Drawing
			var rectanglePath = UIBezierPath.FromRect(new RectangleF(3.5f, 2.5f, 10, 8));
			UIColor.White.SetFill();
			rectanglePath.Fill();
			UIColor.Black.SetStroke();
			rectanglePath.LineWidth = 1;
			rectanglePath.Stroke();


			//// Rectangle 2 Drawing
			var rectangle2Path = UIBezierPath.FromRect(new RectangleF(6.5f, 4.5f, 4, 4));
			color2.SetFill();
			rectangle2Path.Fill();
			UIColor.Black.SetStroke();
			rectangle2Path.LineWidth = 1;
			rectangle2Path.Stroke();


			//// Rectangle 3 Drawing
			var rectangle3Path = UIBezierPath.FromRect(new RectangleF(3.5f, 18.5f, 10, 8));
			UIColor.White.SetFill();
			rectangle3Path.Fill();
			UIColor.Black.SetStroke();
			rectangle3Path.LineWidth = 1;
			rectangle3Path.Stroke();


			//// Rectangle 4 Drawing
			var rectangle4Path = UIBezierPath.FromRect(new RectangleF(6.5f, 20.5f, 4, 4));
			color2.SetFill();
			rectangle4Path.Fill();
			UIColor.Black.SetStroke();
			rectangle4Path.LineWidth = 1;
			rectangle4Path.Stroke();


			//// Rectangle 5 Drawing
			var rectangle5Path = UIBezierPath.FromRect(new RectangleF(30.5f, 2.5f, 10, 8));
			UIColor.White.SetFill();
			rectangle5Path.Fill();
			UIColor.Black.SetStroke();
			rectangle5Path.LineWidth = 1;
			rectangle5Path.Stroke();


			//// Rectangle 6 Drawing
			var rectangle6Path = UIBezierPath.FromRect(new RectangleF(33.5f, 4.5f, 4, 4));
			color2.SetFill();
			rectangle6Path.Fill();
			UIColor.Black.SetStroke();
			rectangle6Path.LineWidth = 1;
			rectangle6Path.Stroke();


			//// Star Drawing
			UIBezierPath starPath = new UIBezierPath();
			starPath.MoveTo(new PointF(26.5f, 8.5f));
			starPath.AddLineTo(new PointF(29.67f, 13.39f));
			starPath.AddLineTo(new PointF(35.06f, 15.06f));
			starPath.AddLineTo(new PointF(31.64f, 19.76f));
			starPath.AddLineTo(new PointF(31.79f, 25.69f));
			starPath.AddLineTo(new PointF(26.5f, 23.7f));
			starPath.AddLineTo(new PointF(21.21f, 25.69f));
			starPath.AddLineTo(new PointF(21.36f, 19.76f));
			starPath.AddLineTo(new PointF(17.94f, 15.06f));
			starPath.AddLineTo(new PointF(23.33f, 13.39f));
			starPath.ClosePath();
			UIColor.White.SetFill();
			starPath.Fill();
			UIColor.Black.SetStroke();
			starPath.LineWidth = 2.5f;
			starPath.Stroke();


			//// Star 2 Drawing
			UIBezierPath star2Path = new UIBezierPath();
			star2Path.MoveTo(new PointF(20.5f, 2.5f));
			star2Path.AddLineTo(new PointF(21.56f, 4.56f));
			star2Path.AddLineTo(new PointF(23.35f, 5.26f));
			star2Path.AddLineTo(new PointF(22.21f, 7.24f));
			star2Path.AddLineTo(new PointF(22.26f, 9.74f));
			star2Path.AddLineTo(new PointF(20.5f, 8.9f));
			star2Path.AddLineTo(new PointF(18.74f, 9.74f));
			star2Path.AddLineTo(new PointF(18.79f, 7.24f));
			star2Path.AddLineTo(new PointF(17.65f, 5.26f));
			star2Path.AddLineTo(new PointF(19.44f, 4.56f));
			star2Path.ClosePath();
			UIColor.White.SetFill();
			star2Path.Fill();
			UIColor.Black.SetStroke();
			star2Path.LineWidth = 2;
			star2Path.Stroke();


			//// Star 3 Drawing
			UIBezierPath star3Path = new UIBezierPath();
			star3Path.MoveTo(new PointF(41.5f, 21.5f));
			star3Path.AddLineTo(new PointF(42.91f, 23.56f));
			star3Path.AddLineTo(new PointF(45.3f, 24.26f));
			star3Path.AddLineTo(new PointF(43.78f, 26.24f));
			star3Path.AddLineTo(new PointF(43.85f, 28.74f));
			star3Path.AddLineTo(new PointF(41.5f, 27.9f));
			star3Path.AddLineTo(new PointF(39.15f, 28.74f));
			star3Path.AddLineTo(new PointF(39.22f, 26.24f));
			star3Path.AddLineTo(new PointF(37.7f, 24.26f));
			star3Path.AddLineTo(new PointF(40.09f, 23.56f));
			star3Path.ClosePath();
			UIColor.White.SetFill();
			star3Path.Fill();
			UIColor.Black.SetStroke();
			star3Path.LineWidth = 1.5f;
			star3Path.Stroke();



		}

		public static void paintCodeDrawQRRETINA(){
			//// Color Declarations
			UIColor color2 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);

			//// Rectangle Drawing
			var rectanglePath = UIBezierPath.FromRect(new RectangleF(11.5f, 7.5f, 13, 14));
			UIColor.White.SetFill();
			rectanglePath.Fill();
			UIColor.Black.SetStroke();
			rectanglePath.LineWidth = 1;
			rectanglePath.Stroke();


			//// Rectangle 2 Drawing
			var rectangle2Path = UIBezierPath.FromRect(new RectangleF(14.5f, 10.5f, 7, 8));
			color2.SetFill();
			rectangle2Path.Fill();
			UIColor.Black.SetStroke();
			rectangle2Path.LineWidth = 1;
			rectangle2Path.Stroke();


			//// Rectangle 3 Drawing
			var rectangle3Path = UIBezierPath.FromRect(new RectangleF(57.5f, 7.5f, 13, 14));
			UIColor.White.SetFill();
			rectangle3Path.Fill();
			UIColor.Black.SetStroke();
			rectangle3Path.LineWidth = 1;
			rectangle3Path.Stroke();


			//// Rectangle 4 Drawing
			var rectangle4Path = UIBezierPath.FromRect(new RectangleF(60.5f, 10.5f, 7, 8));
			color2.SetFill();
			rectangle4Path.Fill();
			UIColor.Black.SetStroke();
			rectangle4Path.LineWidth = 1;
			rectangle4Path.Stroke();


			//// Rectangle 5 Drawing
			var rectangle5Path = UIBezierPath.FromRect(new RectangleF(11.5f, 36.5f, 13, 14));
			UIColor.White.SetFill();
			rectangle5Path.Fill();
			UIColor.Black.SetStroke();
			rectangle5Path.LineWidth = 1;
			rectangle5Path.Stroke();


			//// Rectangle 6 Drawing
			var rectangle6Path = UIBezierPath.FromRect(new RectangleF(14.5f, 39.5f, 7, 8));
			color2.SetFill();
			rectangle6Path.Fill();
			UIColor.Black.SetStroke();
			rectangle6Path.LineWidth = 1;
			rectangle6Path.Stroke();


			//// Star Drawing
			UIBezierPath starPath = new UIBezierPath();
			starPath.MoveTo(new PointF(41.5f, 21.5f));
			starPath.AddLineTo(new PointF(45.73f, 27.42f));
			starPath.AddLineTo(new PointF(52.91f, 29.45f));
			starPath.AddLineTo(new PointF(48.35f, 35.13f));
			starPath.AddLineTo(new PointF(48.55f, 42.3f));
			starPath.AddLineTo(new PointF(41.5f, 39.9f));
			starPath.AddLineTo(new PointF(34.45f, 42.3f));
			starPath.AddLineTo(new PointF(34.65f, 35.13f));
			starPath.AddLineTo(new PointF(30.09f, 29.45f));
			starPath.AddLineTo(new PointF(37.27f, 27.42f));
			starPath.ClosePath();
			UIColor.White.SetFill();
			starPath.Fill();
			color2.SetStroke();
			starPath.LineWidth = 3.5f;
			starPath.Stroke();


			//// Star 2 Drawing
			UIBezierPath star2Path = new UIBezierPath();
			star2Path.MoveTo(new PointF(36.5f, 28.5f));
			star2Path.AddLineTo(new PointF(36.5f, 28.5f));
			star2Path.AddLineTo(new PointF(36.5f, 28.5f));
			star2Path.AddLineTo(new PointF(36.5f, 28.5f));
			star2Path.AddLineTo(new PointF(36.5f, 28.5f));
			star2Path.AddLineTo(new PointF(36.5f, 28.5f));
			star2Path.AddLineTo(new PointF(36.5f, 28.5f));
			star2Path.AddLineTo(new PointF(36.5f, 28.5f));
			star2Path.AddLineTo(new PointF(36.5f, 28.5f));
			star2Path.AddLineTo(new PointF(36.5f, 28.5f));
			star2Path.ClosePath();
			UIColor.White.SetFill();
			star2Path.Fill();
			UIColor.Black.SetStroke();
			star2Path.LineWidth = 1;
			star2Path.Stroke();


			//// Star 3 Drawing
			UIBezierPath star3Path = new UIBezierPath();
			star3Path.MoveTo(new PointF(32.5f, 11));
			star3Path.AddLineTo(new PointF(34.62f, 13.83f));
			star3Path.AddLineTo(new PointF(38.21f, 14.8f));
			star3Path.AddLineTo(new PointF(35.92f, 17.52f));
			star3Path.AddLineTo(new PointF(36.03f, 20.95f));
			star3Path.AddLineTo(new PointF(32.5f, 19.8f));
			star3Path.AddLineTo(new PointF(28.97f, 20.95f));
			star3Path.AddLineTo(new PointF(29.08f, 17.52f));
			star3Path.AddLineTo(new PointF(26.79f, 14.8f));
			star3Path.AddLineTo(new PointF(30.38f, 13.83f));
			star3Path.ClosePath();
			UIColor.White.SetFill();
			star3Path.Fill();
			UIColor.Black.SetStroke();
			star3Path.LineWidth = 2.5f;
			star3Path.Stroke();


			//// Star 4 Drawing
			UIBezierPath star4Path = new UIBezierPath();
			star4Path.MoveTo(new PointF(50, 11.5f));
			star4Path.AddLineTo(new PointF(51.94f, 14.07f));
			star4Path.AddLineTo(new PointF(55.23f, 14.95f));
			star4Path.AddLineTo(new PointF(53.14f, 17.43f));
			star4Path.AddLineTo(new PointF(53.23f, 20.55f));
			star4Path.AddLineTo(new PointF(50, 19.5f));
			star4Path.AddLineTo(new PointF(46.77f, 20.55f));
			star4Path.AddLineTo(new PointF(46.86f, 17.43f));
			star4Path.AddLineTo(new PointF(44.77f, 14.95f));
			star4Path.AddLineTo(new PointF(48.06f, 14.07f));
			star4Path.ClosePath();
			UIColor.White.SetFill();
			star4Path.Fill();
			UIColor.Black.SetStroke();
			star4Path.LineWidth = 2.5f;
			star4Path.Stroke();


			//// Star 5 Drawing
			UIBezierPath star5Path = new UIBezierPath();
			star5Path.MoveTo(new PointF(62.5f, 36.5f));
			star5Path.AddLineTo(new PointF(64.97f, 40.1f));
			star5Path.AddLineTo(new PointF(69.16f, 41.34f));
			star5Path.AddLineTo(new PointF(66.49f, 44.8f));
			star5Path.AddLineTo(new PointF(66.61f, 49.16f));
			star5Path.AddLineTo(new PointF(62.5f, 47.7f));
			star5Path.AddLineTo(new PointF(58.39f, 49.16f));
			star5Path.AddLineTo(new PointF(58.51f, 44.8f));
			star5Path.AddLineTo(new PointF(55.84f, 41.34f));
			star5Path.AddLineTo(new PointF(60.03f, 40.1f));
			star5Path.ClosePath();
			UIColor.White.SetFill();
			star5Path.Fill();
			UIColor.Black.SetStroke();
			star5Path.LineWidth = 2.5f;
			star5Path.Stroke();



		}
	}
}

