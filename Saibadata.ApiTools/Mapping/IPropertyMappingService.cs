using System.Collections.Generic;

namespace Saibadata.ApiTools.Mapping
{
    public interface IPropertyMappingService
    {
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
        bool ValidMappingExistsFor<TSource, TDestinatoon>(string fields);
        void Initialize<TDto, TObj>(Dictionary<string, List<string>> propertyMapping);
    }
}
