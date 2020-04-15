using CourseLibrary.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace CourseLibrary.API.Helpers
{
    public static class IQueryableExtensions 
    {
        private static string orderByString;

        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy,
            Dictionary<string,PropertyMappingValue> mappingDictionary)
        {
            if(source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if(mappingDictionary == null)
            {
                throw new ArgumentNullException(nameof(mappingDictionary));
            }

            if(string.IsNullOrWhiteSpace(orderBy))
            {
                return source;
            }

            // the orderBy string is seperated by ",", so we split it
            var orderByAfterSplit = orderBy.Split(',');

            //apply each orderBy clause in reverse order - otherwise the
            // IQueryable will be ordered in the wrong order
            foreach(var orderByClause in orderByAfterSplit.Reverse())
            {
                //trim the orderByClause, it might contain leading 
                // or trailing spaces. Can't trim the var in foreach,
                // so we uise another var.
                var trimmedOrderByClause = orderByClause.Trim();

                //if sort optionends with " desc", we order 
                // descending, otherwise ascending
                var orderDescending = trimmedOrderByClause.EndsWith(" desc");

                // remove " asc" or " desc" from the orderBy clause, so we
                // get the property name to look for in the mapping dictionary
                var indexOfFirstSpace = trimmedOrderByClause.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedOrderByClause : trimmedOrderByClause.Remove(indexOfFirstSpace);

                //find the matching property
                if(!mappingDictionary.ContainsKey(propertyName))
                {
                    throw new ArgumentException($"Key mapping for {propertyName} is missing");
                }

                //get the PropertyMappingValue
                var propertyMappingValue = mappingDictionary[propertyName];
                
                if(propertyMappingValue == null)
                {
                    throw new ArgumentNullException("propertyMappingValue");
                }

                //Run thru the property names
                // so the orderBy clauses are applied in correct order
                foreach(var destinationProperty in 
                    propertyMappingValue.DestinationProperties)
                {
                    //revert the sort order
                    if(propertyMappingValue.Revert)
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
