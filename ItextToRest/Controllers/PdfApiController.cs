using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItextToRest.Handlers;
using ItextToRest.Models;
using Microsoft.AspNetCore.Mvc;

namespace ItextToRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfApiController : ControllerBase
    {

        /// <summary>
        /// Extract Pages From Pdf  --- You Can Use single pages , list of pages or Page Range or any combination of : 1 or 1,2,3,4 or 1-4,5-9 or combination of  1,5,8,9-15,6-30 
        /// </summary>
        /// <param name="pdfApiMergeModel"></param>
        /// <returns></returns>
        [HttpPost("PdfExtract")]
        public ActionResult<byte[]> PdfExtract([FromBody] PdfApiMergeModel pdfApiMergeModel)
        {
            return PdfHandler.Extract(Convert.FromBase64String(pdfApiMergeModel.PdfSource), pdfApiMergeModel.PdfRange);
        }

        [HttpPost("PdfRegex")]
        public ActionResult<int[]> PdfRegex([FromBody] PdfApiRegexModel pdfApiRegexModel)
        {
            return PdfHandler.Regex(Convert.FromBase64String(pdfApiRegexModel.PdfSource), pdfApiRegexModel.PdfPattern);
        }


        [HttpPost("PdfTextExtract")]
        public ActionResult<String> PdfTextExtract([FromBody] PdfApiTextExtract pdfApiTextExtract)
        {
            return PdfHandler.ExtractText(Convert.FromBase64String(pdfApiTextExtract.PdfSource) );
        }

        [HttpPost("GetNumberOfPages")]
        public ActionResult<Int32> GetNumberOfPages([FromBody] String pdfSource)
        {
            return PdfHandler.GetNumberOfPages(Convert.FromBase64String(pdfSource));
        }

    }
}
