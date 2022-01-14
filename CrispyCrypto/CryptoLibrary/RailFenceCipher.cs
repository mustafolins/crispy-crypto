using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLibrary
{
    public static class RailFenceCipher
    {
        public static string Encode(string p, int count) => p.RailFence(count);
    }
}
