

using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using PdfApi.Models;

namespace PdfApi.Services
{

    public class PdfService 
    {
        private readonly IConverter _converter;
        private readonly TemplateService _templateService;

        public PdfService(TemplateService templateService, IConverter converter)
        {
            _templateService = templateService;
            _converter = converter;
        }

        public async Task<byte[]> GetPdf(TemplateModel model)
        {
            var documentContent = await _templateService.RenderTemplateAsync("~/PdfTemplate/Pdf.cshtml", model);
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 15, Left = 15, Right = 15, Bottom = 30},
                    DocumentTitle = $"{model.Name}s pdf!?!!"
                },
                Objects = {
                    new ObjectSettings {
                        HtmlContent = documentContent
                    }
                }
            };

            var pdf = _converter.Convert(doc);

            return pdf;
        }

        public async Task<byte[]> GetPdfReco(TemplateModel model)
        {
            var documentContent = await _templateService.RenderTemplateAsync("~/PdfTemplate/Pdf.cshtml", model);

            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
            var pdfBytes = htmlToPdf.GeneratePdf(documentContent);

            return pdfBytes;
        }
    }
}
