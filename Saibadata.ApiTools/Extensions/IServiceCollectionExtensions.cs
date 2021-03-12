using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Saibadata.ApiTools.Interfaces;

namespace Saibadata.ApiTools.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApiTools(this IServiceCollection services)
        {
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });

            services.AddScoped<IPageLinksBuilder, PageLinksBuilder>();
            

            return services;
        }
    }
}
