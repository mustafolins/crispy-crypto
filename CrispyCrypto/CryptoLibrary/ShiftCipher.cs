using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLibrary
{
    public static class ShiftCipher
    {
        public static string LeftShiftString(this string s, int count = 3)
        {
            StringBuilder result = new StringBuilder();

            foreach (var ch in s.ToLower())
            {
                if (char.IsLetter(ch))
                {
                    result.Append(ch.LeftShiftChar(count));
                }
                else
                {
                    result.Append(ch);
                }
            }

            return result.ToString();
        }

        public static char LeftShiftChar(this char ch, int count = 3) => (ch - 'a' - count >= 0) ? (char)(ch - count) : (char)('z' - count + (ch - 'a') + 1);

        public static string RightShiftString(this string s, int count = 3)
        {
            StringBuilder result = new StringBuilder();

            foreach (var ch in s.ToLower())
            {
                if (char.IsLetter(ch))
                {
                    result.Append(ch.RightShiftChar(count));
                }
                else
                {
                    result.Append(ch);
                }
            }

            return result.ToString();
        }

        public static char RightShiftChar(this char ch, int count = 3) => LeftShiftChar(ch, 'z' - (count - 1) - 'a');
    }
}
