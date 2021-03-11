using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saibadata.ApiTools.Interfaces
{
    public interface IPageLinksBuilder
    {
        void AddPaginationHeader(string metaData);
        IEnumerable<UriLink> CreatePageNavigationLinks<T>(T resourceParameters, string linkName, bool hasNext, bool hasPrevious) where T : ResourceParametersBase;
    }
}
