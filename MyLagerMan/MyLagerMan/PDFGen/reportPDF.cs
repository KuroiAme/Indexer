using System;
using System.Drawing;
using System.IO;
using System.Text;
using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;

namespace no.dctapps.Garageindex.Pdfgen
{
	
	public class PDFgenerator : IDisposable {
		
		// need to setup all the colours here, normal alternate etc.
		
		private UIColor                 foreColour              = UIColor.Black;
		private UIColor                 altForeColour   		= UIColor.FromRGB(4,0,143);
		private UIColor                 DescriptionColour 		= UIColor.Red;
		private float                   PdfWidth                = 595; // standard width of A4
		private float                   PdfHeight               = 841; // standard height of A4
		private float                   PdfCentreWidth  		= 595/2;
		private int                     CurrentPage     		= 0;
		private int                     CurrentPos              = 0;
		private int                     MaxPos                  = 760; // max position down the page before starting a new page
		
		public PDFgenerator (){ }
		
		void IDisposable.Dispose ()
		{
			if (foreColour != null) {
				foreColour.Dispose ();
				foreColour = null;
			}
			if (altForeColour != null) {
				altForeColour.Dispose ();
				altForeColour = null;
			}
			if (DescriptionColour != null) {
				DescriptionColour.Dispose ();
				DescriptionColour = null;
			}
		}

		/// <summary>
		/// Generates the insurance report.
		/// </summary>
		/// <param name="pdfDocument">Pdf document filename. eg. mypdf.pdf</param>
		/// <param name="pRevision_Set">P revision_ set. no idea</param>
		public void generateInsuranceReport(string pdfDocument, long pRevision_Set) {
			string fullPdfName = "";                // PDF document name
			string pointName = "";
			string roadStreet = "";
			bool alternateColour = false;  
			UIColor currentColour;                  // current colour to write the pdf in.
//			StringBuilder sql = new StringBuilder();
			fullPdfName     = Path.Combine(Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments),pdfDocument);
			// I deliberately leave the pdf in the documents folder so that the user if they are really stuck for
			// a way of getting the report e.g. no local printer (air print) and no email, they can use iTunes
			// to get the PDF.
			if (File.Exists(fullPdfName) == true) {
				File.Delete(fullPdfName);
			}
			// create document to the right size for A4 paper.
			RectangleF canvasRect =  new RectangleF(0, 0, PdfWidth, PdfHeight);
			CGPDFInfo info = new CGPDFInfo();
			info.AllowsPrinting = true;
			UIGraphics.BeginPDFContext (fullPdfName, canvasRect, info);
			UIGraphics.BeginPDFPage ();
			CurrentPage++;
			CGContext gctx = UIGraphics.GetCurrentContext ();
			gctx.ScaleCTM (1, -1);
			gctx.TranslateCTM (0, -25f);
			alignLeft(gctx,"Produced. " + DateTime.Now.ToLongDateString(),"Helvetica", 6, 20, 0, false, UIColor.Black);
			alignCentre(gctx, "GarageIndex Manifest","Times New Roman", 20, PdfCentreWidth, 5, false, UIColor.Black);

			UIImage image = UIImage.FromFile("icon.png");
			RectangleF imageRect = new RectangleF(595-78, -50,60,60);
//			if (Core.Instance.showLogoPdf == true) {
				gctx.DrawImage(imageRect, image.CGImage);
//			}
//			sql.Append("select tblpoints.ppoint, tblpoints.road_street_no, tblpoints.bike_notes, tblpoints.list_run, ");
//			sql.Append("tblpoints.road_street, tblpoints.postcode, tblpoints.times_asked, tblpoints.street_view_link, ");
//			sql.Append("tblpoints.point_name, tblpoints.pstatus_type, tblpoint_status.status_description, ");
//			sql.Append("tblrevision_set_point.score ");
//			sql.Append("from tblrevision_set_point left outer join ");
//			sql.Append("tblpoints on tblpoints.ppoint = tblrevision_set_point.ppoint left outer join ");
//			sql.Append("tblpoint_status on tblpoint_status.pstatus_type = tblpoints.pstatus_type ");
//			sql.Append("where (tblrevision_set_point.prevision_set=" + pRevision_Set.ToString() + ") ");
//			sql.Append("order by tblrevision_set_point.list_order, tblrevision_set_point.prevision_ppoint ");
			// need to draw the header fields and a line across the page


			CurrentPos = 55;
			alignLeft(gctx, "Point Name","Helvetica", 12, 20, CurrentPos, true, UIColor.Black);
			alignLeft(gctx, "Road","Helvetica", 12, 280, CurrentPos, true, UIColor.Black);
			alignRight(gctx, "Run/List","Helvetica", 12, 470, CurrentPos, true, UIColor.Black);
			alignRight(gctx, "P/C","Helvetica", 12, 500, CurrentPos, true, UIColor.Black);
			alignRight(gctx, "T/A","Helvetica", 12, 540, CurrentPos, true, UIColor.Black);
			alignRight(gctx, "Id","Helvetica", 12, 570, CurrentPos, true, UIColor.Black);
			CurrentPos+=14;



			// TODO try/catch
//			using (WizPR.cls_db mydb = new WizPR.cls_db()) {
//				using (SqliteDataReader bikeList = mydb.OpenTable(sql.ToString())) {
//					while (bikeList.Read() == true) {
						pointName = "";
						roadStreet = "";
//						pointName = bikeList["point_name"].ToString();
//						roadStreet = bikeList["road_street"].ToString();
						// this is to truncate long strings, it does not matter to the end users if I do this
						// as all the important stuff is at the start of the line.
						if (pointName.Length > 60) {
							pointName = pointName.Substring(0,60);
						}
						if (roadStreet.Length > 22) {
							roadStreet = roadStreet.Substring(0,22);
						}
						if (CurrentPos >= MaxPos) {
							// create a new page
							UIGraphics.BeginPDFPage ();
							CurrentPage++;
							gctx.ScaleCTM (1, -1);
							gctx.TranslateCTM (0, -25f);
							gctx.ShowTextAtPoint(0,0,"",0);
							alignLeft(gctx,"Produced. " + DateTime.Now.ToLongDateString(),"Helvetica", 6, 20, 0, false, UIColor.Black);
							alignCentre(gctx, "WizAnn Bike List","Times New Roman", 20, PdfCentreWidth, 5, false, UIColor.Black);
//							if (Core.Instance.showLogoPdf == true) {
								gctx.DrawImage(imageRect, image.CGImage);
//							}
							CurrentPos = 55;
							alignLeft(gctx, "Point Name","Helvetica", 12, 20, CurrentPos, true, UIColor.Black);
							alignLeft(gctx, "Road","Helvetica", 12, 280, CurrentPos, true, UIColor.Black);
							alignRight(gctx, "Run/List","Helvetica", 12, 470, CurrentPos, true, UIColor.Black);
							alignRight(gctx, "P/C","Helvetica", 12, 500, CurrentPos, true, UIColor.Black);
							alignRight(gctx, "T/A","Helvetica", 12, 540, CurrentPos, true, UIColor.Black);
							alignRight(gctx, "Id","Helvetica", 12, 570, CurrentPos, true, UIColor.Black);
							CurrentPos+=14;
						}
						// I have a setting in the database for users that have black and white printers.
						// setting the text to black only for them means that the printout works properly.
//						if (Core.Instance.colourPdf == true) {
							// this is to rotate the colours on alternate lines.
							if (alternateColour == false){
								currentColour = foreColour;
							} else {
								currentColour = altForeColour;
							}
//						} else {
							currentColour = foreColour;
//						}
						alignLeft(gctx, pointName,"Helvetica", 10, 20, CurrentPos, false, currentColour);
						alignLeft(gctx, roadStreet,"Helvetica", 10, 280, CurrentPos, false, currentColour);
//						alignRight(gctx, bikeList["list_run"].ToString(),"Helvetica", 9, 470, CurrentPos, false, currentColour);
//						alignRight(gctx, bikeList["postcode"].ToString(),"Helvetica", 9, 500, CurrentPos, false, currentColour);
//						alignRight(gctx, bikeList["times_asked"].ToString(),"Helvetica", 9, 540, CurrentPos, false, currentColour);
//						alignRight(gctx, bikeList["ppoint"].ToString(),"Helvetica", 9, 570, CurrentPos, false, currentColour);
//						if (bikeList["bike_notes"].ToString() != "") {
							CurrentPos+=11;
//							alignLeft(gctx, bikeList["bike_notes"].ToString(),"Helvetica", 9, 20, CurrentPos, false, bikeNotesColour);
//		   					}
						alternateColour =! alternateColour;
						CurrentPos+=14;
//					}
//				}
//			}
			UIGraphics.EndPDFContent ();
		}
		
		// member functions for text alignment
		public void alignCentre (CGContext gctx, string text, string fontName, float fontSize, float X, float Y, bool Bold, UIColor textColour)
		{
			if (Bold == true) {
				gctx.SelectFont (fontName + "-Bold", fontSize, CGTextEncoding.MacRoman);
			} else {
				gctx.SelectFont (fontName, fontSize, CGTextEncoding.MacRoman);
			}
			gctx.SetTextDrawingMode(CGTextDrawingMode.Invisible);
			gctx.ShowTextAtPoint(X, Y, text, text.Length);
			float start = gctx.TextPosition.X;
			gctx.SetFillColor(textColour.CGColor);
			gctx.SetTextDrawingMode(CGTextDrawingMode.Fill);
			gctx.ShowTextAtPoint( X-(start-X)/2, (0-(Y+5)), text, text.Length);
		}
		public void alignRight (CGContext gctx, string text, string fontName, float fontSize, float X, float Y, bool Bold, UIColor textColour)
		{
			string strippedText = text.Trim ();
			gctx.SetTextDrawingMode (CGTextDrawingMode.Invisible);
			gctx.ShowTextAtPoint (X, Y, "", 0);
			PointF origPoint = gctx.TextPosition;
			if (Bold == true) {
				gctx.SelectFont (fontName + "-Bold", fontSize, CGTextEncoding.MacRoman);
			} else {
				gctx.SelectFont (fontName, fontSize, CGTextEncoding.MacRoman);
			}
			gctx.ShowTextAtPoint(origPoint.X, origPoint.Y, strippedText, strippedText.Length);
			PointF newPoint = gctx.TextPosition;
			float textWidthX = newPoint.X - origPoint.X;
			gctx.SetFillColor(textColour.CGColor);
			gctx.SetTextDrawingMode(CGTextDrawingMode.Fill);
			gctx.ShowTextAtPoint(origPoint.X - textWidthX, (0-(Y+5)), strippedText, strippedText.Length);
		}
		public void alignLeft (CGContext gctx, string text, string fontName, float fontSize, float X, float Y, bool Bold, UIColor textColour)
		{
			if (Bold == true) {
				gctx.SelectFont (fontName + "-Bold", fontSize, CGTextEncoding.MacRoman);
			} else {
				gctx.SelectFont (fontName, fontSize, CGTextEncoding.MacRoman);
			}
			gctx.SetFillColor(textColour.CGColor);
			gctx.SetTextDrawingMode(CGTextDrawingMode.Fill);
			gctx.ShowTextAtPoint(X, (0-(Y+5)), text, text.Length);
		}
		
	}
}


