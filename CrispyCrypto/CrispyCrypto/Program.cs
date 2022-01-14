using CryptoLibrary;
using System;

namespace CrispyCrypto
{
    class Program
    {
        static void Main(string[] args)
        {
            var plainText = "Hello Worldabc";
            Console.WriteLine(plainText);
            var cipher = CaeserCipher.Encode(plainText);
            Console.WriteLine(cipher);
            var decoded = CaeserCipher.Decode(cipher);
            Console.WriteLine(decoded);

            Console.WriteLine("--- Transposition ---");

            plainText.TransposeToMatrix("man").PrintMatrix();
            cipher = TranspositionCipher.Encode(plainText, "man");
            Console.WriteLine(cipher);

            Console.WriteLine("--- Rail Fence ---");

            plainText.RailFenceToMatrix(3).PrintMatrix();
            cipher = RailFenceCipher.Encode(plainText, 3);
            Console.WriteLine(cipher);

            Console.WriteLine("--- Class Work ---");

            // try all shifts
            cipher = "IWTHT PGT IWT IXBTH IWPI IGN BTCH HDJAH";
            for (int i = 0; i < 'z' - 'a' + 1; i++)
            {
                decoded = cipher.LeftShiftString(i);
                Console.WriteLine($"{i + 1}\t{decoded}");
                cipher = decoded;
            }

            Console.WriteLine();
            cipher = RailFenceCipher.Encode("meet me after the toga party", 4);
            Console.WriteLine(cipher);
        }
    }
}
