using System.Collections.Generic;
using pfm.Models;

namespace pfm.Helper
{
     public class PageSortedList<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public SortOrder? SortOrder { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; }
    }
}