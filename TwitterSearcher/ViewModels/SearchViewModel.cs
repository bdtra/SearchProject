using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TwitterSearcher.ViewModels
{
    public class SearchViewModel
    {
        [Required]
        public string Keyword { get; set; }

        [Required]
        public string SortBy { get; set; }

        [Required]
        public int SampleSize { get; set; }

        public IOrderedEnumerable<KeyValuePair<string, int>> Counter { get; set; }

        public SearchViewModel()
        {
        }
    }
}