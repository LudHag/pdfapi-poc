using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace PdfApi.Services
{
    public class TemplateService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IRazorViewEngine _viewEngine;

        public TemplateService(IRazorViewEngine viewEngine, IServiceProvider serviceProvider,
            ITempDataProvider tempDataProvider)
        {
            _viewEngine = viewEngine;
            _serviceProvider = serviceProvider;
            _tempDataProvider = tempDataProvider;
        }

        public async Task<string> RenderTemplateAsync<TViewModel>(string templatePath, TViewModel viewModel)
        {
            var httpContext = new DefaultHttpContext
            {
                RequestServices = _serviceProvider
            };

            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            var outputWriter = new StringWriter();

            var viewResult = _viewEngine.GetView(templatePath, templatePath, false);

            var viewDictionary =
                new ViewDataDictionary<TViewModel>(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = viewModel
                };

            var tempDataDictionary = new TempDataDictionary(httpContext, _tempDataProvider);

            var viewContext = new ViewContext(actionContext, viewResult.View, viewDictionary, tempDataDictionary,
                outputWriter, new HtmlHelperOptions());
            await viewResult.View.RenderAsync(viewContext);

            return outputWriter.ToString();
        }
    }
}