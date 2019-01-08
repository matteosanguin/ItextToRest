using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Utils;
using ItextToRest.Extentions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ItextToRest.Handlers
{
    /// <summary>
    /// Pdf Extract, Concatenation, Split, Merge 
    /// </summary>
    public static class PdfHandler
    {
        /// <summary>
        /// Search text inside pdf 
        /// </summary>
        /// <param name="sourcePdf">The Source in base64 string format </param>
        /// <param name="RegexPattern">The Regex Pattern fro searching for </param>
        /// <returns>An Array of rows where the seach pattern is in text extracted from the pdf </returns>
        public static int[] Regex(byte[] sourcePdf, String RegexPattern)
        {
            var _textExtracted = ExtractText(sourcePdf);
            if (_textExtracted != null )
            {
                Regex _rgx = new Regex(RegexPattern,RegexOptions.IgnoreCase | RegexOptions.Multiline);
                return _rgx.Matches(_textExtracted).ToList().Select(x=>x.Index).ToArray();
            }

            return null;
        }

        /// <summary>
        /// Extract The Text Content From Pdf 
        /// </summary>
        /// <param name="sourcePdf">The Pdf in base64 string format</param>
        /// <returns>A string of lines separated by \n </returns>
        public static String ExtractText(byte[] sourcePdf)
        {
            String _textExtracted = null;
            using (PdfDocument _sourcePdfDocument = new PdfDocument(new PdfReader(new MemoryStream(sourcePdf))))
            {
                FilteredEventListener listener = new FilteredEventListener();
                SimpleTextExtractionStrategy extractionStrategy = listener.AttachEventListener(new SimpleTextExtractionStrategy());
                for (int i = 1; i < _sourcePdfDocument.GetNumberOfPages(); i++)
                {
                    new PdfCanvasProcessor(listener).ProcessPageContent(_sourcePdfDocument.GetPage(i));
                }
               

                _textExtracted = extractionStrategy.GetResultantText();
            }

            return _textExtracted;
        }

        /// <summary>
        /// Estract page range or series from a pdf e return a new pdf of the selected pages 
        /// </summary>
        /// <param name="sourcePdf">byte array of the source pdf</param>
        /// <param name="pageRange">range of pages to extract 1,3-4,5,12</param>
        /// <returns></returns>
        public static byte[] Extract(byte[] sourcePdf, String pageRange)
        {
            byte[] _resultPdf = null;

            var _lstSelPages = pageRange.ExpandRange();

            using (PdfDocument _sourcePdfDocument = new PdfDocument(new PdfReader(new MemoryStream(sourcePdf))))
            {
                MemoryStream _resultPdfStream = new MemoryStream();
                using (PdfDocument _resultPdfDocument = new PdfDocument(new PdfWriter(_resultPdfStream)))
                {
                    PdfMerger _pdfMerger = new PdfMerger(_resultPdfDocument);
                    _pdfMerger.Merge(_sourcePdfDocument, _lstSelPages.ToList());
                    _pdfMerger.Close();
                }
                _resultPdf = _resultPdfStream.ToArray();
                _resultPdfStream.Dispose();
            }

            return _resultPdf;
        }

        public static Int32 GetNumberOfPages(byte[] sourcePdf )
        {
            Int32 _numberOfPages = 0;

            using (PdfDocument _sourcePdfDocument = new PdfDocument(new PdfReader(new MemoryStream(sourcePdf))))
            {
                _numberOfPages = _sourcePdfDocument.GetNumberOfPages();
            }

            return _numberOfPages;
        }
    }
}
