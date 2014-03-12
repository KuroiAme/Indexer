using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace IndexerIOS
{
	public class SetActiveNavbarIcon
	{
		public static UIImage MakeImage (){
			bool retina = (UIScreen.MainScreen.Scale > 1.0);
			if (retina) {
				UIGraphics.BeginImageContext (new System.Drawing.SizeF (44, 44));
				paintCodeRetina();
			}else{
				UIGraphics.BeginImageContext (new System.Drawing.SizeF (22, 22));
				paintCodeNonRetina();
			}

			var converted = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return converted;

		}

		static void paintCodeRetina ()
		{
			//// Color Declarations
			UIColor color = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);

			//// Bezier 2 Drawing
			UIBezierPath bezier2Path = new UIBezierPath();
			bezier2Path.MoveTo(new PointF(19.88f, 4.59f));
			bezier2Path.AddCurveToPoint(new PointF(28.6f, 6.25f), new PointF(22.84f, 4.27f), new PointF(25.87f, 4.82f));
			bezier2Path.AddLineTo(new PointF(30.02f, 5.82f));
			bezier2Path.AddLineTo(new PointF(31.53f, 3.5f));
			bezier2Path.AddLineTo(new PointF(33.04f, 5.82f));
			bezier2Path.AddLineTo(new PointF(35.61f, 6.61f));
			bezier2Path.AddLineTo(new PointF(33.97f, 8.83f));
			bezier2Path.AddLineTo(new PointF(34.03f, 10.88f));
			bezier2Path.AddCurveToPoint(new PointF(35.44f, 13.03f), new PointF(34.55f, 11.56f), new PointF(35.02f, 12.28f));
			bezier2Path.AddLineTo(new PointF(36.77f, 11.5f));
			bezier2Path.AddLineTo(new PointF(38.79f, 13.82f));
			bezier2Path.AddLineTo(new PointF(42.21f, 14.61f));
			bezier2Path.AddLineTo(new PointF(40.03f, 16.83f));
			bezier2Path.AddLineTo(new PointF(40.13f, 19.64f));
			bezier2Path.AddLineTo(new PointF(37.47f, 18.9f));
			bezier2Path.AddCurveToPoint(new PointF(36.87f, 27.6f), new PointF(37.95f, 21.79f), new PointF(37.75f, 24.79f));
			bezier2Path.AddLineTo(new PointF(39.42f, 28.65f));
			bezier2Path.AddLineTo(new PointF(37.79f, 31.61f));
			bezier2Path.AddLineTo(new PointF(37.86f, 35.35f));
			bezier2Path.AddLineTo(new PointF(35.34f, 34.1f));
			bezier2Path.AddLineTo(new PointF(32.82f, 35.35f));
			bezier2Path.AddLineTo(new PointF(32.84f, 34.53f));
			bezier2Path.AddCurveToPoint(new PointF(30.74f, 36.39f), new PointF(32.18f, 35.22f), new PointF(31.48f, 35.84f));
			bezier2Path.AddLineTo(new PointF(30.8f, 36.56f));
			bezier2Path.AddLineTo(new PointF(32.79f, 37.26f));
			bezier2Path.AddLineTo(new PointF(31.53f, 39.24f));
			bezier2Path.AddLineTo(new PointF(31.58f, 41.74f));
			bezier2Path.AddLineTo(new PointF(29.62f, 40.9f));
			bezier2Path.AddLineTo(new PointF(27.66f, 41.74f));
			bezier2Path.AddLineTo(new PointF(27.72f, 39.24f));
			bezier2Path.AddLineTo(new PointF(27.18f, 38.4f));
			bezier2Path.AddCurveToPoint(new PointF(21.39f, 39.5f), new PointF(25.32f, 39.15f), new PointF(23.36f, 39.52f));
			bezier2Path.AddLineTo(new PointF(21.46f, 42.45f));
			bezier2Path.AddLineTo(new PointF(18.66f, 41.3f));
			bezier2Path.AddLineTo(new PointF(15.86f, 42.45f));
			bezier2Path.AddLineTo(new PointF(15.95f, 39.02f));
			bezier2Path.AddLineTo(new PointF(15.41f, 38.21f));
			bezier2Path.AddCurveToPoint(new PointF(10.07f, 34.37f), new PointF(13.46f, 37.35f), new PointF(11.64f, 36.08f));
			bezier2Path.AddLineTo(new PointF(10.01f, 34.31f));
			bezier2Path.AddLineTo(new PointF(10.03f, 35.35f));
			bezier2Path.AddLineTo(new PointF(7.23f, 34.1f));
			bezier2Path.AddLineTo(new PointF(4.43f, 35.35f));
			bezier2Path.AddLineTo(new PointF(4.51f, 31.61f));
			bezier2Path.AddLineTo(new PointF(2.7f, 28.65f));
			bezier2Path.AddLineTo(new PointF(5.55f, 27.59f));
			bezier2Path.AddLineTo(new PointF(5.95f, 26.84f));
			bezier2Path.AddCurveToPoint(new PointF(5.33f, 22.44f), new PointF(5.57f, 25.41f), new PointF(5.36f, 23.92f));
			bezier2Path.AddLineTo(new PointF(3.67f, 22.83f));
			bezier2Path.AddLineTo(new PointF(3.76f, 20.65f));
			bezier2Path.AddLineTo(new PointF(1.77f, 18.92f));
			bezier2Path.AddLineTo(new PointF(4.91f, 18.3f));
			bezier2Path.AddLineTo(new PointF(5.9f, 17.33f));
			bezier2Path.AddLineTo(new PointF(5.58f, 17.45f));
			bezier2Path.AddLineTo(new PointF(5.67f, 14.02f));
			bezier2Path.AddLineTo(new PointF(3.67f, 11.3f));
			bezier2Path.AddLineTo(new PointF(6.81f, 10.33f));
			bezier2Path.AddLineTo(new PointF(8.66f, 7.5f));
			bezier2Path.AddLineTo(new PointF(10.06f, 9.64f));
			bezier2Path.AddCurveToPoint(new PointF(12.57f, 7.41f), new PointF(10.85f, 8.79f), new PointF(11.68f, 8.05f));
			bezier2Path.AddLineTo(new PointF(12.23f, 5.99f));
			bezier2Path.AddLineTo(new PointF(16.76f, 1.5f));
			bezier2Path.AddLineTo(new PointF(19.88f, 4.59f));
			bezier2Path.ClosePath();
			bezier2Path.MoveTo(new PointF(14.79f, 15.14f));
			bezier2Path.AddCurveToPoint(new PointF(14.79f, 27.86f), new PointF(11.06f, 18.65f), new PointF(11.06f, 24.35f));
			bezier2Path.AddCurveToPoint(new PointF(28.26f, 27.86f), new PointF(18.51f, 31.38f), new PointF(24.54f, 31.38f));
			bezier2Path.AddCurveToPoint(new PointF(28.26f, 15.14f), new PointF(31.98f, 24.35f), new PointF(31.98f, 18.65f));
			bezier2Path.AddCurveToPoint(new PointF(14.79f, 15.14f), new PointF(24.54f, 11.62f), new PointF(18.51f, 11.62f));
			bezier2Path.ClosePath();
			color.SetFill();
			bezier2Path.Fill();
			UIColor.Black.SetStroke();
			bezier2Path.LineWidth = 1;
			bezier2Path.Stroke();



		}

		static void paintCodeNonRetina ()
		{
			//// Color Declarations
			UIColor color = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);

			//// Bezier 2 Drawing
			UIBezierPath bezier2Path = new UIBezierPath();
			bezier2Path.MoveTo(new PointF(10.03f, 2.93f));
			bezier2Path.AddCurveToPoint(new PointF(14, 3.7f), new PointF(11.38f, 2.78f), new PointF(12.76f, 3.04f));
			bezier2Path.AddLineTo(new PointF(14.65f, 3.5f));
			bezier2Path.AddLineTo(new PointF(15.34f, 2.43f));
			bezier2Path.AddLineTo(new PointF(16.03f, 3.5f));
			bezier2Path.AddLineTo(new PointF(17.2f, 3.86f));
			bezier2Path.AddLineTo(new PointF(16.45f, 4.89f));
			bezier2Path.AddLineTo(new PointF(16.48f, 5.84f));
			bezier2Path.AddCurveToPoint(new PointF(17.12f, 6.84f), new PointF(16.72f, 6.16f), new PointF(16.93f, 6.49f));
			bezier2Path.AddLineTo(new PointF(17.73f, 6.13f));
			bezier2Path.AddLineTo(new PointF(18.65f, 7.2f));
			bezier2Path.AddLineTo(new PointF(20.21f, 7.57f));
			bezier2Path.AddLineTo(new PointF(19.21f, 8.6f));
			bezier2Path.AddLineTo(new PointF(19.26f, 9.89f));
			bezier2Path.AddLineTo(new PointF(18.04f, 9.55f));
			bezier2Path.AddCurveToPoint(new PointF(17.78f, 13.58f), new PointF(18.26f, 10.89f), new PointF(18.17f, 12.28f));
			bezier2Path.AddLineTo(new PointF(18.93f, 14.06f));
			bezier2Path.AddLineTo(new PointF(18.19f, 15.43f));
			bezier2Path.AddLineTo(new PointF(18.22f, 17.17f));
			bezier2Path.AddLineTo(new PointF(17.08f, 16.59f));
			bezier2Path.AddLineTo(new PointF(15.93f, 17.17f));
			bezier2Path.AddLineTo(new PointF(15.93f, 16.78f));
			bezier2Path.AddCurveToPoint(new PointF(14.98f, 17.65f), new PointF(15.63f, 17.1f), new PointF(15.31f, 17.39f));
			bezier2Path.AddLineTo(new PointF(15.01f, 17.72f));
			bezier2Path.AddLineTo(new PointF(15.91f, 18.05f));
			bezier2Path.AddLineTo(new PointF(15.34f, 18.97f));
			bezier2Path.AddLineTo(new PointF(15.36f, 20.12f));
			bezier2Path.AddLineTo(new PointF(14.47f, 19.73f));
			bezier2Path.AddLineTo(new PointF(13.57f, 20.12f));
			bezier2Path.AddLineTo(new PointF(13.6f, 18.97f));
			bezier2Path.AddLineTo(new PointF(13.36f, 18.58f));
			bezier2Path.AddCurveToPoint(new PointF(10.72f, 19.08f), new PointF(12.51f, 18.92f), new PointF(11.61f, 19.09f));
			bezier2Path.AddLineTo(new PointF(10.75f, 20.45f));
			bezier2Path.AddLineTo(new PointF(9.47f, 19.92f));
			bezier2Path.AddLineTo(new PointF(8.2f, 20.45f));
			bezier2Path.AddLineTo(new PointF(8.23f, 18.86f));
			bezier2Path.AddLineTo(new PointF(7.99f, 18.49f));
			bezier2Path.AddCurveToPoint(new PointF(5.55f, 16.71f), new PointF(7.1f, 18.09f), new PointF(6.27f, 17.5f));
			bezier2Path.AddLineTo(new PointF(5.53f, 16.68f));
			bezier2Path.AddLineTo(new PointF(5.54f, 17.17f));
			bezier2Path.AddLineTo(new PointF(4.26f, 16.59f));
			bezier2Path.AddLineTo(new PointF(2.98f, 17.17f));
			bezier2Path.AddLineTo(new PointF(3.02f, 15.43f));
			bezier2Path.AddLineTo(new PointF(2.19f, 14.06f));
			bezier2Path.AddLineTo(new PointF(3.49f, 13.57f));
			bezier2Path.AddLineTo(new PointF(3.68f, 13.23f));
			bezier2Path.AddCurveToPoint(new PointF(3.39f, 11.19f), new PointF(3.5f, 12.56f), new PointF(3.41f, 11.88f));
			bezier2Path.AddLineTo(new PointF(2.64f, 11.37f));
			bezier2Path.AddLineTo(new PointF(2.68f, 10.36f));
			bezier2Path.AddLineTo(new PointF(1.77f, 9.56f));
			bezier2Path.AddLineTo(new PointF(3.2f, 9.27f));
			bezier2Path.AddLineTo(new PointF(3.65f, 8.82f));
			bezier2Path.AddLineTo(new PointF(3.51f, 8.88f));
			bezier2Path.AddLineTo(new PointF(3.55f, 7.29f));
			bezier2Path.AddLineTo(new PointF(2.64f, 6.04f));
			bezier2Path.AddLineTo(new PointF(4.07f, 5.59f));
			bezier2Path.AddLineTo(new PointF(4.91f, 4.28f));
			bezier2Path.AddLineTo(new PointF(5.55f, 5.27f));
			bezier2Path.AddCurveToPoint(new PointF(6.69f, 4.24f), new PointF(5.91f, 4.87f), new PointF(6.29f, 4.53f));
			bezier2Path.AddLineTo(new PointF(6.54f, 3.58f));
			bezier2Path.AddLineTo(new PointF(8.6f, 1.5f));
			bezier2Path.AddLineTo(new PointF(10.03f, 2.93f));
			bezier2Path.ClosePath();
			bezier2Path.MoveTo(new PointF(7.7f, 7.81f));
			bezier2Path.AddCurveToPoint(new PointF(7.7f, 13.7f), new PointF(6.01f, 9.44f), new PointF(6.01f, 12.07f));
			bezier2Path.AddCurveToPoint(new PointF(13.85f, 13.7f), new PointF(9.4f, 15.33f), new PointF(12.15f, 15.33f));
			bezier2Path.AddCurveToPoint(new PointF(13.85f, 7.81f), new PointF(15.54f, 12.07f), new PointF(15.54f, 9.44f));
			bezier2Path.AddCurveToPoint(new PointF(7.7f, 7.81f), new PointF(12.15f, 6.18f), new PointF(9.4f, 6.18f));
			bezier2Path.ClosePath();
			color.SetFill();
			bezier2Path.Fill();
			UIColor.Black.SetStroke();
			bezier2Path.LineWidth = 1;
			bezier2Path.Stroke();



		}
	}
}

