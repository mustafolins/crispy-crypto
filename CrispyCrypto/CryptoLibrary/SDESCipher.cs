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
        public static bool[] GetCharBits(this char ch, int bytes = 1)
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

        public static string GetBinaryString(this bool[] bits)
        {
            string result = "";
            for (int i = bits.Length - 1; i >= 0; i--)
            {
                result += (bits[i] ? "1" : "0");
            }
            return result;
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
            if (input == null || input.Length != 10)
                throw new ArgumentOutOfRangeException($"Length of bit array should be 10 but got {input.Length}");

            return new bool[]{ input[10 - 6], input[10 - 8], input[10 - 9], input[10 - 1], input[10 - 10],
                input[10 - 4], input[10 - 7], input[10 - 2], input[10 - 5], input[10 - 3 ] };
        }

        public static bool[] P8(this bool[] input)
        {
            if (input == null || input.Length != 10)
                throw new ArgumentOutOfRangeException($"Length of bit array should be 10 but got {input.Length}");

            return new bool[]{ input[10 - 9], input[10 - 10], input[10 - 5], input[10 - 8],
                input[10 - 4], input[10 - 7], input[10 - 3], input[10 - 6] };
        }

        public static bool[] P4(this bool[] input)
        {
            if (input == null || input.Length != 4)
                throw new ArgumentOutOfRangeException($"Length of bit array should be 4 but got {input.Length}");

            return new bool[]{ input[4 - 1], input[4 - 3], input[4 - 4], input[4 - 2] };
        }

        public static (bool[] left, bool[] right) SplitKey(this bool[] input)
        {
            if (input == null || input.Length != 10)
                throw new ArgumentOutOfRangeException($"Length of bit array should be 10 but got {input.Length}");

            return (new bool[] { input[0], input[1], input[2], input[3], input[4] },
                new bool[] { input[5], input[6], input[7], input[8], input[9] });
        }

        public static (bool[] left, bool[] right) SplitBits(this bool[] input)
        {
            if (input == null || input.Length != 8)
                throw new ArgumentOutOfRangeException($"Length of bit array should be 10 but got {input.Length}");

            return (new bool[] { input[4], input[5], input[6], input[7] },
                new bool[] { input[0], input[1], input[2], input[3] });
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

        public static bool[] InitialPermutation(this bool[] input)
        {
            if (input == null || input.Length != 8)
                throw new ArgumentOutOfRangeException($"Length of bit array should be 8 but got {input.Length}");

            return new bool[] { input[8 - 7], input[8 - 5], input[8 - 8], input[8 - 4], input[8 - 1], input[8 - 3], input[8 - 6], input[8 - 2] };
        }

        public static bool[] InitialInversePermutation(this bool[] input)
        {
            if (input == null || input.Length != 8)
                throw new ArgumentOutOfRangeException($"Length of bit array should be 8 but got {input.Length}");

            return new bool[] { input[8 - 6], input[8 - 8], input[8 - 2], input[8 - 7], input[8 - 5], input[8 - 3], input[8 - 1], input[8 - 4] };
        }

        public static bool[] ExpandAndMutate(this bool[] input)
        {
            return new bool[] { input[4 - 1], input[4 - 4], input[4 - 3], input[4 - 2], input[4 - 3], input[4 - 2], input[4 - 1], input[4- 4] };
        }

        public static bool[] XOR(this bool[] input, bool[] key)
        {
            if (input.Length != key.Length)
                throw new ArgumentException($"The length of the two arrays should be the same.");

            var results = new bool[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                results[i] = input[i] ^ key[i];
            }

            return results;
        }

        private static byte[][] S0Table = new byte[][]
        {
            new byte[] { 1, 0, 3, 2 },
            new byte[] { 3, 2, 1, 0 },
            new byte[] { 0, 2, 1, 3 },
            new byte[] { 3, 1, 3, 2 },
        };

        public static bool[] S0(this bool[] input)
        {
            var row = GetNumber(new[] { input[0], input[3] }.GetBinaryString());
            var col = GetNumber(new[] { input[1], input[2] }.GetBinaryString());

            return GetBits(S0Table[row][col]);
        }

        private static byte[][] S1Table = new byte[][]
        {
            new byte[] { 0, 1, 2, 3 },
            new byte[] { 2, 0, 1, 3 },
            new byte[] { 3, 0, 1, 0 },
            new byte[] { 2, 1, 0, 3 },
        };

        public static bool[] S1(this bool[] input)
        {
            var row = GetNumber(new[] { input[0], input[3] }.GetBinaryString());
            var col = GetNumber(new[] { input[1], input[2] }.GetBinaryString());

            return GetBits(S1Table[row][col]);
        }

        private static bool[] GetBits(byte num)
        {
            switch (num)
            {
                case 0:
                    return new bool[] { false, false };
                case 1:
                    return new bool[] { true, false };
                case 2:
                    return new bool[] { false, true };
                case 3:
                    return new bool[] { true, true };
                default:
                    throw new ArgumentException($"{num} is not valid!");
            }
        }

        private static int GetNumber(string binaryStr)
        {
            switch (binaryStr)
            {
                case "00":
                    return 0;
                case "01":
                    return 1;
                case "10":
                    return 2;
                case "11":
                    return 3;
                default:
                    throw new ArgumentException($"{binaryStr} is not valid!");
            }
        }

        public static bool[] Fk(this bool[] plainTextBits, bool[] key)
        {
            (bool[] left, bool[] right) = plainTextBits.SplitBits();
            // expand and mutate
            var em = right.ExpandAndMutate();
            // xored
            var xored = em.XOR(key);
            (bool[] xorLeft, bool[] xorRight) = xored.SplitBits();
            // combined s boxes
            var combinedSs = xorRight.S1().CombineBits(xorLeft.S0());
            // result of fk
            return combinedSs.P4().XOR(left);
        }

        public static bool[] FkRight(this bool[] fK, bool[] key, bool[] ip)
        {
            if (fK == null)
                throw new ArgumentNullException(nameof(fK));
            if (fK.Length != 4)
                throw new ArgumentOutOfRangeException($"Length of bit array for fK should be 4 but got {fK.Length}");
            bool[] swapped = Switch(ip, fK);
            return swapped.Fk(key);
        }

        private static bool[] Switch(bool[] plainTextBits, bool[] fK)
        {
            (_, bool[] right) = plainTextBits.SplitBits();
            var swapped = fK.CombineBits(right);
            return swapped;
        }

        public static bool[] Encrypt(this bool[] plainTextBits, bool[] sessionKey)
        {
            if (plainTextBits == null)
                throw new ArgumentNullException(nameof(plainTextBits));
            if (plainTextBits.Length == 0)
                return plainTextBits;

            (bool[] key1, bool[] key2) = sessionKey.GenerateKeys();

            // initial permuatation
            var ip = plainTextBits.InitialPermutation();

            var fK = ip.Fk(key1);
            var fKR = fK.FkRight(key2, ip);
            var combined = fK.CombineBits(fKR);
            return combined.InitialInversePermutation().ToArray();
        }
    }
}
