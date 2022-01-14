using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLibrary
{
    public static class TranspositionCipher
    {
        public static string Encode(string p, string key) => p.Transpose(key);
    }
}
