using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace Saibadata.ApiTools.Mapping
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy, Dictionary<string, PropertyMappingValue> mappingDictionary = null)
        {
            PropertyMappingValue propertyMappingValue = null;

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }


            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return source;
            }

            var orderByString = string.Empty;

            var orderByAfterSplit = orderBy.Split(',');

            // apply each orderBy clause in reverse order 
            // otherwise IQueryable will be in the wrong order
            // the reverse has been removed after reading item in discussion
            foreach (var orderByClause in orderByAfterSplit)
            {
                var trimmedClause = orderByClause.Trim();
                var orderDescending = trimmedClause.EndsWith(" desc");

                // move " asc" or " desc" from the orderBy clause so we get the property 
                // name to look for in the mapping dictionary
                var indexOfFirstSpace = trimmedClause.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ? trimmedClause : trimmedClause.Remove(indexOfFirstSpace);

                if (mappingDictionary is not null)
                {
                    if (!mappingDictionary.ContainsKey(propertyName))
                    {
                        throw new ArgumentException($"Key mapping for {propertyName} is missing.");
                    }

                    propertyMappingValue = mappingDictionary[propertyName];
                    if (propertyMappingValue is null)
                    {
                        throw new ArgumentNullException("propertyMappingValue");
                    }
                }
                else
                {
                    var propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    if (propertyInfo is null)
                    {
                        throw new ArgumentException($"Property '{propertyName}' does not exist in class {typeof(T).Name}.");
                    }

                    propertyMappingValue = new PropertyMappingValue(new List<string> { propertyInfo.Name });
                }


                foreach (var destinationProperty in propertyMappingValue.DestinationProperties)
                {
                    if (propertyMappingValue.Revert)
                    {
                        orderDescending = !orderDescending;
                    }
                    orderByString = orderByString +
                        (string.IsNullOrWhiteSpace(orderByString) ? string.Empty : ", ")
                        + destinationProperty
                        + (orderDescending ? " descending" : " ascending");

                }
            }
            return source.OrderBy(orderByString);

        }
    }
}
