// -----------------------------------------------------------------------
// <copyright file="DataReaderExtensions.cs" company="Plantarium Co.">
//     Plantarium, MIT
// </copyright>
// -----------------------------------------------------------------------
namespace Plantarium.Infrastructure.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using static Plantarium.Infrastructure.Internals.ReflectionUtilities;

    /// <summary>
    /// The data reader extensions.
    /// </summary>
    internal static class DataReaderExtensions
    {
        /// <summary>
        /// Maps the specified reader.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns>The list of objects mapped.</returns>
        public static IEnumerable<T> Map<T>(this IDataReader reader) where T : class, new()
        {
            var results = new List<T>(reader.FieldCount);
            var properties = CachePropertySetters<T>(reader.GetFieldNames());

            while (reader.Read())
            {
                var result = new T();

                foreach (var property in properties)
                {
                    var value = reader[property.Key];

                    if (value == DBNull.Value)
                    {
                        value = null;
                    }

                    property.Value(result, value);
                }

                results.Add(result);
            }

            return results;
        }

        /// <summary>
        /// Gets the field names.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>The list of field names.</returns>
        public static IEnumerable<string> GetFieldNames(this IDataReader reader)
        {
            var result = new List<string>(reader.FieldCount);

            for (int i = 0; i < reader.FieldCount; i++)
            {
                result.Add(reader.GetName(i));
            }

            return result;
        }
    }
}
