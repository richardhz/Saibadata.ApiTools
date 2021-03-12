using Microsoft.AspNetCore.Mvc;
using Saibadata.ApiTools.Interfaces;
using System.Collections.Generic;

namespace Saibadata.ApiTools
{
    public class PageLinksBuilder : IPageLinksBuilder
    {
        private readonly IUrlHelper _urlHelper;

        public PageLinksBuilder(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }
        public void AddPaginationHeader(string metaData)
        {
            _urlHelper.ActionContext.HttpContext.Response.Headers.Add("X-Pagination", metaData);
        }

        public IEnumerable<UriLink> CreatePageNavigationLinks<T>(T resourceParameters, string linkName, bool hasNext, bool hasPrevious) where T : ResourceParametersBase
        {
            var links = new List<UriLink>
            {
                new UriLink
                {
                    Href = CreatePageNavigationResourceUri(resourceParameters, UriPageType.CurrentPage, linkName),
                    Rel = "self",
                    Method = "GET"
                }
            };
            if (hasNext)
            {
                links.Add(new UriLink
                {
                    Href = CreatePageNavigationResourceUri(resourceParameters, UriPageType.NextPage, linkName),
                    Rel = "nextPage",
                    Method = "GET"
                });
            }
            if (hasPrevious)
            {
                links.Add(new UriLink
                {
                    Href = CreatePageNavigationResourceUri(resourceParameters, UriPageType.PreviousPage, linkName),
                    Rel = "previousPage",
                    Method = "GET"
                });
            }
            return links;
        }

        private string CreatePageNavigationResourceUri<T>(T resourceParameters, UriPageType pageType, string linkName) where T : ResourceParametersBase
        {
            switch (pageType)
            {
                case UriPageType.PreviousPage:
                    resourceParameters.PageNumber--;
                    var link1 = _urlHelper.Link(linkName, resourceParameters);
                    resourceParameters.PageNumber++;
                    return link1;
                case UriPageType.NextPage:
                    resourceParameters.PageNumber++;
                    var link2 = _urlHelper.Link(linkName, resourceParameters);
                    resourceParameters.PageNumber--;
                    return link2;
                case UriPageType.CurrentPage:
                    return _urlHelper.Link(linkName, resourceParameters);
                default:
                    return _urlHelper.Link(linkName, resourceParameters);
            }
        }
    }
}
