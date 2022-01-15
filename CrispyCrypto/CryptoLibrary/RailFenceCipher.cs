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
