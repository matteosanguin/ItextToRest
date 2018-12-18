using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using ItextToRest.Extentions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ItextToRest.Handlers
{
    /// <summary>
    /// Pdf Extract, Concatenation, Split, Merge 
    /// </summary>
    public static class PdfHandler
    {
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
