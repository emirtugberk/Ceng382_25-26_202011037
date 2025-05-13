using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ceng382_25_26_202011037.Helpers
{
    /// <summary>
    /// Singleton utility for exporting any enumerable of T to JSON,
    /// optionally filtering by selected column names.
    /// </summary>
    public sealed class Utils
    {
        private static readonly Lazy<Utils> _instance = new Lazy<Utils>(() => new Utils());
        public static Utils Instance => _instance.Value;
        private Utils() { }

        /// <summary>
        /// Serializes the provided data to indented JSON.
        /// If selectedColumns is null or empty, serializes full objects.
        /// Otherwise includes only those properties.
        /// </summary>
        public string ExportToJson<T>(IEnumerable<T> data, string[]? selectedColumns)
        {
            // No column filter: serialize whole objects
            if (selectedColumns == null || selectedColumns.Length == 0)
            {
                return JsonSerializer.Serialize(
                    data,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    }
                );
            }

            // Build list of only selected properties
            var props = typeof(T)
                .GetProperties()
                .Where(p => selectedColumns.Contains(p.Name, StringComparer.OrdinalIgnoreCase))
                .ToArray();

            var projected = data.Select(item =>
            {
                var dict = new Dictionary<string, object?>();
                foreach (var prop in props)
                {
                    dict[prop.Name] = prop.GetValue(item);
                }
                return dict;
            });

            return JsonSerializer.Serialize(
                projected,
                new JsonSerializerOptions { WriteIndented = true }
            );
        }
    }
}
