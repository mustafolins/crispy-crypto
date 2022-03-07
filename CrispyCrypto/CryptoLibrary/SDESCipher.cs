using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLibrary
{
    public static class SDESCipher
    {
        /// <summary>
        /// Gets the bits for the given character.
        /// </summary>
        /// <param name="ch">The <see cref="char"/> to get bits for.</param>
        /// <param name="bytes">The number bytes to get the bits for the given <paramref name="ch"/>.</param>
        /// <returns>A <see cref="bool"/> array representing the bits of the <paramref name="ch"/>.</returns>
        public static bool[] GetBits(this char ch, int bytes = 1)
        {
            short shift = 1;
            bool[] bits = new bool[sizeof(char) * bytes * 4]; // 0-th decimal place and on [1, 2, 4, 8, 16, 32, etc.]
            for (int i = 0; i < bits.Length; i++)
            {
                bits[i] = (ch & shift) > 0 ? true : false;
                shift <<= 1;
            }

            return bits;
        }

        public static void PrintBits(this bool[] bits)
        {
            // print binary
            for (int i = bits.Length - 1; i >= 0; i--)
            {
                Console.Write(bits[i] ? "1" : "0");
                if (i % 4 == 0)
                {
                    Console.Write(' ');
                }
            }
        }

        public static bool[] LeftShift(this bool[] input)
        {
            bool[] output = new bool[input.Length];
            if (input.Length > 0)
            {
                for (int i = 0; i < input.Length - 1; i++)
                {
                    output[i + 1] = input[i];
                }
            }
            output[0] = input[input.Length - 1];
            return output;
        }

        public static bool[] P10(this bool[] input)
        {
            if (input.Length != 10)
                throw new ArgumentOutOfRangeException($"Length of bit array should be 10 but got {input.Length}");

            return new bool[]{ input[10 - 6], input[10 - 8], input[10 - 9], input[10 - 1], input[10 - 10],
                input[10 - 4], input[10 - 7], input[10 - 2], input[10 - 5], input[10 - 3 ] };
        }

        public static bool[] P8(this bool[] input)
        {
            if (input.Length != 10)
                throw new ArgumentOutOfRangeException($"Length of bit array should be 10 but got {input.Length}");

            return new bool[]{ input[10 - 9], input[10 - 10], input[10 - 5], input[10 - 8],
                input[10 - 4], input[10 - 7], input[10 - 3], input[10 - 6] };
        }

        public static (bool[] left, bool[] right) SplitKey(this bool[] input)
        {
            if (input.Length != 10)
                throw new ArgumentOutOfRangeException($"Length of bit array should be 10 but got {input.Length}");

            return (new bool[] { input[0], input[1], input[2], input[3], input[4]}, 
                new bool[] { input[5], input[6], input[7], input[8], input[9] });
        }

        public static bool[] CombineBits(this bool[] left, bool[] right)
        {
            return left.Concat(right).ToArray();
        }

        public static (bool[] key1, bool[] key2) GenerateKeys(this bool[] sessionKey)
        {
            // permutate 10
            var p10 = sessionKey.P10();
            // split bits in half
            (bool[] left, bool[] right) = p10.SplitKey();
            // left shift both halves and combine halves
            var shift = left.LeftShift().CombineBits(right.LeftShift());
            // permatate 8
            var key1 = shift.P8();
            // left shift two more times each half then combine and permutate 8
            var key2 = left.LeftShift().LeftShift().LeftShift().CombineBits(right.LeftShift().LeftShift().LeftShift()).P8();
            // return the two keys
            return (key1, key2);
        }
    }
}
