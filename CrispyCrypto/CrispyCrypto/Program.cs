//#define examples
#define classwork
//#define intro
#define caesar

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
#elif classwork
            // class work
            ClassWork();
#endif
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

        private static void ClassWork()
        {
#if intro
            #region IntroToCrypt
            Console.WriteLine("--- Class Work ---");

            // try all shifts
            string cipher = "IWTHT PGT IWT IXBTH IWPI IGN BTCH HDJAH";
            Console.WriteLine($"Cipher: {cipher}");
            for (int i = 1; i < 'z' - 'a' + 1; i++)
            {
                string decoded = CaeserCipher.Encode(cipher);
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
            #endregion
#elif caesar
            #region CaesarCipher
            string cipher = "NA NCCYR RNPU QNL XRRCF GUR QBPGBE NJNL";
            Console.WriteLine($"Cipher: {cipher}");
            var brute = new ShiftCipherBruteForce(cipher);
            Console.WriteLine(brute.BruteForce());

            Console.WriteLine();

            cipher = "CQRB URCCUN YRPPH FNWC CX CQN VJATNC";
            Console.WriteLine($"Cipher: {cipher}");
            brute = new ShiftCipherBruteForce(cipher);
            string plainText = brute.BruteForce();
            Console.WriteLine(plainText);
            Console.WriteLine("Cipher text statistics.");
            brute.Analysis.CalculateCharFrequencies();
            brute.Analysis.PrintCharFrequencies();
            Console.WriteLine("Plain text statistics.");
            brute = new ShiftCipherBruteForce(plainText);
            brute.Analysis.CalculateCharFrequencies();
            brute.Analysis.PrintCharFrequencies();

            Console.WriteLine();
            cipher = "KYVE KYV KIRMVCCVI ZE KYV URIB KYREBJ PFL WFI PFLI KZEP JGRIB";
            Console.WriteLine($"Cipher: {cipher}");
            Console.WriteLine($"Most common trigram: {brute.Analysis.MostCommonPolygram(cipher, 3)}");
            Console.WriteLine($"Most common digram: {brute.Analysis.MostCommonPolygram(cipher, 2)}");
            brute = new ShiftCipherBruteForce(cipher);
            plainText = brute.BruteForce();
            Console.WriteLine($"Plain: {plainText}");
            Console.WriteLine("Cipher text statistics.");
            brute.Analysis.CalculateCharFrequencies();
            brute.Analysis.PrintCharFrequencies();
            Console.WriteLine("Plain text statistics.");
            brute = new ShiftCipherBruteForce(plainText);
            brute.Analysis.CalculateCharFrequencies();
            brute.Analysis.PrintCharFrequencies();
            #endregion
#endif
        }
    }
}
