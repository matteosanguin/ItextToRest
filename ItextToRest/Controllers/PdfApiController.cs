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

        [HttpPost("PdfExtract")]
        public ActionResult<byte[]> PdfExtract([FromBody] PdfApiMergeModel pdfApiMergeModel)
        {
            return PdfHandler.Extract(Convert.FromBase64String(pdfApiMergeModel.PdfSource), pdfApiMergeModel.PdfRange);
        }

        [HttpPost("GetNumberOfPages")]
        public ActionResult<Int32> GetNumberOfPages([FromBody] String pdfSource)
        {
            return PdfHandler.GetNumberOfPages(Convert.FromBase64String(pdfSource));
        }

    }
}
