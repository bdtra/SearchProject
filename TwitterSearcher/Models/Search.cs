using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterSearcher.Models
{
    public class Search
    {
        public int SearchID { get; set; }
        public List<KeyValuePair<string, int>> Counter { get; set; }
    }
}
