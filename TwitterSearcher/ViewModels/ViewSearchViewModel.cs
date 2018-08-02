using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterSearcher.Models;

namespace TwitterSearcher.ViewModels
{
    public class ViewSearchViewModel
    {
        public Search Search { get; set; }
        public IList<UserSearch> Items { get; set; }
    }
}
