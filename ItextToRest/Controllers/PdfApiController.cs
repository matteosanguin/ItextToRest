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
        /// <param name="mergeModel"></param>
        /// <returns></returns>
        [HttpPost("Extract")]
        public ActionResult<byte[]> Extract([FromBody] MergeModel mergeModel)
        {
            return PdfHandler.Extract(Convert.FromBase64String(mergeModel.Source), mergeModel.Range);
        }

        [HttpPost("RegexAbsolute")]
        public ActionResult<List<int>> RegexAbsolute([FromBody] RegexModel regexModel)
        {
            return PdfHandler.RegexAbsolute(Convert.FromBase64String(regexModel.Source), regexModel.Pattern);
        }

        [HttpPost("RegexPaged")]
        public ActionResult<List<RegexFound>> RegexPaged([FromBody] RegexModel regexModel)
        {
            return PdfHandler.RegexPaged(Convert.FromBase64String(regexModel.Source), regexModel.Pattern);
        }


        [HttpPost("TextExtract")]
        public ActionResult<String> TextExtract([FromBody] TextExtractModel textExtract)
        {
            return PdfHandler.ExtractText(Convert.FromBase64String(textExtract.Source) );
        }

        [HttpPost("TextExtractPaged")]
        public ActionResult<List<String>> TextExtractPaged([FromBody] TextExtractModel textExtract)
        {
            return PdfHandler.ExtractTextPaged(Convert.FromBase64String(textExtract.Source));
        }

        [HttpPost("GetNumberOfPages")]
        public ActionResult<Int32> GetNumberOfPages([FromBody] String source)
        {
            return PdfHandler.GetNumberOfPages(Convert.FromBase64String(source));
        }

    }
}
