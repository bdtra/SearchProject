using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterSearcher.Models
{
    public class AnalyzeViewModel
    {
        [Required]
        public string Keyword { get; set; }

        [Required]
        public int SampleSize { get; set; }

        public Dictionary<string, int> Counter { get; set; }

        public AnalyzeViewModel()
        {
        }
    }
}