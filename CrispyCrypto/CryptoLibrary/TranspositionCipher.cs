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

        public static char[][] UnTransposeToMatrix(this string s, int col)
        {
            var rows = new char[s.Length / col][];

            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = new char[col];
            }

            string sLowered = s.ToLower();
            for (int i = 0; i < sLowered.Length; i++)
            {
                char ch = sLowered[i];
                rows[i % (s.Length / col)][i / (s.Length / col)] = ch;
            }
            return rows;
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

        public static string UnTranspose(this string s, int col)
        {
            var matrix = s.UnTransposeToMatrix(col);

            var result = "";

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    result += matrix[i][j] != '\0' ? matrix[i][j] : ' ';
                }
            }

            return result;
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
    }
}
