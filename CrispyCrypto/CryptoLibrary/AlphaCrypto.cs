using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLibrary
{
    public static class AlphaCrypto
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

        public static char[][] TransposeToMatrix(this string s, string key)
        {
            var rows = new char[s.Length / key.Length + 1][];

            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = new char[key.Length];
            }

            string sLowered = s.ToLower();
            for (int i = 0; i < sLowered.Length; i++)
            {
                char ch = sLowered[i];
                rows[i / key.Length][i % key.Length] = ch;
            }
            return rows;
        }

        public static void PrintMatrix(this char[][] matrix)
        {
            foreach (var row in matrix)
            {
                foreach (var col in row)
                {
                    Console.Write($"{col},");
                }
                Console.WriteLine();
            }
        }

        public static string Transpose(this string s, string key)
        {
            var matrix = s.TransposeToMatrix(key);
            var keyArray = key.ToLower().ToList();
            keyArray.Sort();

            var result = "";

            while (keyArray.Any())
            {
                int i = key.IndexOf(keyArray.First());

                for (int j = 0; j < matrix.Length; j++)
                {
                    result += matrix[j][i] != '\0' ? matrix[j][i] : ' ';
                }

                keyArray.RemoveAt(0);
            }

            return result;
        }

        public static char[][] RailFenceToMatrix(this string s, int count)
        {
            var rows = new char[count][];

            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = new char[s.Length];
            }

            string sLowered = s.ToLower();
            int row = 0, col = 0;
            bool goingDown = true;
            for (int i = 0; i < sLowered.Length; i++)
            {
                char ch = sLowered[i];
                rows[row][col] = ch;

                if (goingDown)
                {
                    row++;
                }
                else
                {
                    row--;
                }
                if (row == count)
                {
                    row = count - 2;
                    goingDown = false;
                    col++;
                }
                else if (row < 0)
                {
                    row = 1;
                    goingDown = true;
                    col++;
                }
            }
            return rows;
        }

        public static string RailFence(this string s, int count)
        {
            var matrix = s.RailFenceToMatrix(count);

            var result = "";
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    result += matrix[i][j];
                }
            }
            return result;
        }
    }
}
