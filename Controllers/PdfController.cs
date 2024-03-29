﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PdfApi.Models;
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
        [HttpGet("Dink")]
        public async Task<FileContentResult> Dink()
        {
            var model = new TemplateModel()
            {
                Name = "Ludvig"
            };
            var pdfArray = await _pdfService.GetPdf(model);

            return File(pdfArray, "application/pdf", $"{model.Name}-file.pdf");
        }

        [HttpGet("Nreco")]
        public async Task<FileContentResult> Nreco()
        {
            var model = new TemplateModel()
            {
                Name = "Ludvig"
            };
            var pdfArray = await _pdfService.GetPdf(model);

            return File(pdfArray, "application/pdf", $"{model.Name}-nreco-file.pdf");
        }
    }
}
