using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLibrary
{
    public static class Words
    {
        public static HashSet<string> Common { get; set; }
            = new HashSet<string> { "the", "be", "to", "of", "and", "in", "that", "have", "it", "not", "for", "on", "with", "an" };
    }
}
