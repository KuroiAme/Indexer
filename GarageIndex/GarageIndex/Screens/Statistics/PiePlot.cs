/*
 * C# sample for PiePlot
 *
 * Author:
 * Berndt Hamboeck
 */
using System;
using System.Collections.Generic;

using MonoTouch.Foundation;

using CorePlot;
using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;
using System.Drawing;
using GoogleAnalytics.iOS;

namespace GarageIndex
{
	public class PlotViewController : UIViewController {
		CPTGraphHostingView host;
		protected CPTGraph Graph;

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			if (UIDevice.CurrentDevice.CheckSystemVersion (7, 0))
				EdgesForExtendedLayout = UIRectEdge.None;

			// Host the graph
			host = new CPTGraphHostingView (new RectangleF (10, 40, 300, 300)) {
				HostedGraph = Graph
			};
			View.AddSubview (host);
		}
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, "Statistics Screen");
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}
	}

	public class PiePlot : PlotViewController
	{
		CPTXYGraph graph;

		public PiePlot ()
		{
			SetupGraph ();
			SetupAxes ();
			SetupPiePlots ();
			Graph = graph;
		}

		void SetupGraph ()
		{
			/*
				Dark Gradients => Dark gradient theme.
				Plain White    => Plain white theme.
				Plain Black    => Plain black theme.
				Slate          => Slate theme.
				Stocks         => Stocks theme.
			*/

			var theme = CPTTheme.ThemeNamed (CPTTheme.PlainWhiteTheme);

			graph = new CPTXYGraph {
				PaddingLeft = 0,
				PaddingRight = 0,
				PaddingTop = 0,
				PaddingBottom = 0,
			};
			graph.ApplyTheme (theme);

			this.View.BackgroundColor = UIColor.White;
		}

		void SetupAxes ()
		{
			graph.PlotAreaFrame.MasksToBorder = false;

			var axisSet = (CPTXYAxisSet)graph.AxisSet;
			var x = axisSet.XAxis;
			x.LabelingPolicy = CPTAxisLabelingPolicy.None;

			var y = axisSet.YAxis;
			y.LabelingPolicy = CPTAxisLabelingPolicy.None;
		}

		void SetupPiePlots ()
		{
			 // Prepare a radial overlay gradient for shading/gloss
			var color = new CPTColor (new MonoTouch.CoreGraphics.CGColor (0f,0f,0f));

			var overlayGradient = new CPTGradient { GradientType = CPTGradientType.Radial };

			overlayGradient.AddColorStop (color.ColorWithAlphaComponent (0.7f), 0.0f);

			// Create a plot that uses the data source method
			var piePlot = new CPTPieChart {
				PieRadius = 80.0f,
				Identifier = (NSString) "Fordeling av størrelsesorden",
				StartAngle = (float) Math.PI / 4f, // 0.785398163397448309616f; //M_PI_4;
				SliceDirection = CPTPieDirection.CounterClockwise,
				BorderLineStyle = CPTLineStyle.LineStyle
			};
					
			float one = float.Parse(AppDelegate.dao.GetAntallBeholdere ());
			float two = float.Parse (AppDelegate.dao.GetAntallTing ());
			float three = float.Parse (AppDelegate.dao.GetAntallStore ());

			float all = one + two + three;

			float enprosent = one / all;
			float en = 120 * enprosent;

			float toprosent = two / all;
			float to = 120 * toprosent;

			float treprosent = three / all;
			float tre = 120 * treprosent;

			var inputData = new List<float> {
				tre,
				en,
				to
			};
			piePlot.DataSource = new PieSourceData (inputData);

			graph.AddPlot (piePlot);
		}
	}

	public class PieSourceData : CPTPieChartDataSource
	{
		List<CPTColor> colors = new List<CPTColor> { CPTColor.RedColor, CPTColor.BlueColor, CPTColor.GreenColor };
		static string one = NSBundle.MainBundle.LocalizedString ("Containers", "Containers");
		static string two = NSBundle.MainBundle.LocalizedString ("Items", "Items");
		static string three = NSBundle.MainBundle.LocalizedString ("Large Objects", "Large Objects");
		List<String> Labels = new List<String> {three,one, two };
		static CPTMutableTextStyle whiteText;
		List<float> data;

		public PieSourceData (List<float> yValues)
		{
			if (whiteText == null) {
				whiteText = new CPTMutableTextStyle ();
				whiteText.Color = CPTColor.BlackColor;
			}

			data = yValues;
		}

		public override int NumberOfRecordsForPlot (CPTPlot plot)
		{
			return data.Count;
		}

		public override NSNumber NumberForPlot (CPTPlot plot, CPTPlotField forFieldEnum, uint index)
		{
			if (forFieldEnum == CPTPlotField.PieChartWidth)
				return data [(int)index];
			return index;
		}

		public override CPTLayer DataLabelForPlot (CPTPlot plot, uint recordIndex)
		{
			string text = Labels[(int)recordIndex];
			return new CPTTextLayer (text, whiteText);
		}

		public override CPTFill GetSliceFill (CPTPieChart pieChart, uint recordIndex)
		{
			return new CPTFill (colors[(int)recordIndex]);
		}
	}
}
