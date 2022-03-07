//#define examples
#define classwork
//#define intro
//#define caesar
//#define substition
#define sdes

using CryptoLibrary;
using System;
using System.Linq;

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
#elif substition
            #region SubstitionCipher
            // vigenere cipher
            var plain = "HOW MUCH WOOD WOULD A WOODCHUCK CHUCK";
            var key = "REVOLUTION";
            Console.WriteLine($"Plain: {plain}");
            plain.PrinteVigenereEncoding(key);
            Console.WriteLine($"Cipher: {plain.VigenereEncode(key)}");

            // some random monoalphabet
            Console.WriteLine();
            var cipher = "MFV JVOM MFUNCO UN LUIV ZGV IGVV JWM SDW QZN CUTV MFVP MD MFV JUGHO ZNH MFV JVVO";
            var brute = new ShiftCipherBruteForce(cipher);
            Console.WriteLine($"Cipher: {cipher}");
            Console.WriteLine($"Most common trigram: {brute.Analysis.MostCommonPolygram(cipher, 3)}");
            Console.WriteLine($"Most common digram: {brute.Analysis.MostCommonPolygram(cipher, 2)}");

            Console.WriteLine("Cipher text statistics.");
            brute.Analysis.CalculateCharFrequencies();
            brute.Analysis.PrintCharFrequencies();

            // replace the trigram and other letters in the cipher text 
            var trigram = brute.Analysis.MostCommonPolygram(cipher, 3);
            cipher = cipher.Replace(trigram[2], 'E').Replace('T', 'V').Replace('S', 'Y').Replace('O', 'S').Replace('D', 'O').Replace('H', 'D');
            cipher = cipher.Replace(trigram[0], 'T').Replace(trigram[1], 'H');
            cipher = cipher.Replace('P', 'M').Replace('J', 'B').Replace('I', 'F').Replace('U', 'I')
                .Replace('G', 'R').Replace('C', 'G').Replace('Z', 'A').Replace('W', 'U').Replace('Q', 'C');
            Console.WriteLine(cipher);
            #endregion
#elif sdes
            char ch = 'a';

            // get bits
            bool[] bits = ch.GetBits();
            bits.PrintBits();
            Console.WriteLine();
            bits.LeftShift().PrintBits();
            Console.WriteLine("\nSession Key:");
            var sessionKey = new bool[] { false, true, false, false, false, false, false, true, false, true };
            sessionKey.PrintBits();
            Console.WriteLine("\nP10:");
            var p10 = sessionKey.P10();
            p10.PrintBits();
            Console.WriteLine("\nLeft Shift two halves and combine:");
            (bool[] left, bool[] right) = p10.SplitKey();
            var shift = left.LeftShift().CombineBits(right.LeftShift());
            shift.PrintBits();
            Console.WriteLine("\nP8 (Key 1):");
            var p8 = shift.P8();
            p8.PrintBits();
            Console.WriteLine("\nP8 (Key 2)");
            left.LeftShift().LeftShift().LeftShift().CombineBits(right.LeftShift().LeftShift().LeftShift()).P8().PrintBits();

            // library function
            Console.WriteLine("\nLibrary function:");
            (bool[] key1, bool[] key2) = sessionKey.GenerateKeys();
            Console.WriteLine("\nKey 1:");
            key1.PrintBits();
            Console.WriteLine("\nKey 2:");
            key2.PrintBits();
#endif
        }
    }
}
