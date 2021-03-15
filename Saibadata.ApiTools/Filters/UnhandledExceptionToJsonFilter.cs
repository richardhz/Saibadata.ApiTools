using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;

namespace Saibadata.ApiTools.Filters
{
    public class UnhandledExceptionToJsonFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment env;
        private readonly ApiToolsConfig settings;
        public UnhandledExceptionToJsonFilter(IWebHostEnvironment env, IOptions<ApiToolsConfig> settings)
        {
            this.env = env;
            this.settings = settings.Value;
        }

        public void OnException(ExceptionContext context)
        {

            var error = new ApiError();
            if (env.IsDevelopment())
            {
                error.Message = context.Exception.Message;
                error.Detail = context.Exception.StackTrace;
            }
            else
            {
                error.Message = settings?.ErrorMessage ?? "A server error occured.";
                error.Detail = context.Exception.Message;
            }


            context.Result = new ObjectResult(error)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

    }
}
