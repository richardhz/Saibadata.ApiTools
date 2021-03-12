using System.Collections.Generic;

namespace Saibadata.ApiTools.Mapping
{
    public interface IPropertyMapping
    {
        Dictionary<string, PropertyMappingValue> MappingDictionary { get; }
    }
}
