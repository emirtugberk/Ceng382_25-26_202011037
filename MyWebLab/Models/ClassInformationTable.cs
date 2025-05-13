using System.Collections.Generic;

namespace Ceng382_25_26_202011037.Models
{
    /// <summary>
    /// Represents a paginated and optionally filtered view of ClassInformationModel for display in the table.
    /// </summary>
    public class ClassInformationTable
    {
        /// <summary>
        /// The collection of items to be displayed (subset after filtering & paging).
        /// </summary>
        public IEnumerable<ClassInformationModel> Items { get; set; } = new List<ClassInformationModel>();

        /// <summary>
        /// Current page index (1-based).
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// Number of items per page.
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Total number of pages based on item count & page size.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Total item count (before paging).
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Optional filter term (e.g., search string).
        /// </summary>
        public string? SearchTerm { get; set; }

        /// <summary>
        /// Selected column names for JSON export.
        /// </summary>
        public string[]? SelectedColumns { get; set; }
    }
}
