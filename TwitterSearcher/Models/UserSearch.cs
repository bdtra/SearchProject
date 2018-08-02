using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterSearcher.Models
{
    public class UserSearch
    {

        public int UserID { get; set; }
        public User User { get; set; }

        public int SearchID { get; set; }
        public Search Search { get; set; }

    }
}
