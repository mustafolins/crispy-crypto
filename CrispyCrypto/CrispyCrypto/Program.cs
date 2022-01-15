//#define examples
#define classwork

using CryptoLibrary;
using System;

namespace CrispyCrypto
{
    class Program
    {
        static void Main(string[] args)
        {
#if examples
            // examples
            CipherExamples();
#endif

#if classwork
            // class work
            ClassWork(); 
#endif
        }

        private static void ClassWork()
        {
            Console.WriteLine("--- Class Work ---");

            // try all shifts
            string cipher = "IWTHT PGT IWT IXBTH IWPI IGN BTCH HDJAH";
            Console.WriteLine($"Cipher: {cipher}");
            for (int i = 1; i < 'z' - 'a' + 1; i++)
            {
                string decoded = CaeserCipher.Decode(cipher);
                Console.WriteLine($"{i}\t{decoded}");
                cipher = decoded;
            }

            Console.WriteLine("\nRailFence of 4");
            string plainText = "meet me after the toga party";
            Console.WriteLine(plainText);
            cipher = RailFenceCipher.Encode(plainText, 4);
            Console.WriteLine(cipher);

            Console.WriteLine("\nTransposition:");
            cipher = "TSTESTIHAOTPIPITFROOHSETASNEISHNICR";
            Console.WriteLine(cipher);
            var col = cipher.Length / 7;
            cipher.UnTransposeToMatrix(col).PrintMatrix();
            cipher = TranspositionCipher.UnTranspose(cipher, col);
            Console.WriteLine(cipher);
        }

        private static void CipherExamples()
        {
            string cipher, decoded;
            var plainText = "Hello Worldabc";
            Console.WriteLine(plainText);
            cipher = CaeserCipher.Encode(plainText);
            Console.WriteLine(cipher);
            decoded = CaeserCipher.Decode(cipher);
            Console.WriteLine(decoded);

            Console.WriteLine("--- Transposition ---");

            plainText.TransposeToMatrix("man").PrintMatrix();
            cipher = TranspositionCipher.Encode(plainText, "man");
            Console.WriteLine(cipher);
            cipher.UnTransposeToMatrix("man".Length).PrintMatrix();

            Console.WriteLine("--- Rail Fence ---");

            plainText.RailFenceToMatrix(3).PrintMatrix();
            cipher = RailFenceCipher.Encode(plainText, 3);
            Console.WriteLine(cipher);
        }
    }
}
