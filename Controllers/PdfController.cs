using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PdfApi.Services;

namespace PdfApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PdfController : ControllerBase
    {
        private readonly PdfService _pdfService;
        public PdfController(PdfService pdfService)
        {
            _pdfService = pdfService;
        }
        [HttpGet]
        public async Task<FileContentResult> Get()
        {
            var pdfArray = await _pdfService.GetStuff();

            return File(pdfArray, "application/pdf", "minroligapdf.pdf");
        }
    }
}
