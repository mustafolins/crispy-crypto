using System;

namespace CryptoLibrary
{
    public static class CaeserCipher
    {
        public static string Encode(string s) => s.LeftShiftString();
        public static string Decode(string s) => s.RightShiftString();
    }
}
