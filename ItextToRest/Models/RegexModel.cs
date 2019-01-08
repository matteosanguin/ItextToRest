using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItextToRest.Models
{
    public class RegexModel
    {
        public String Source { get; set; }
        public String Pattern { get; set; }
        public Boolean ExtractPageNumber { get; set; }
    }
}
