using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoLibrary
{
    public class CryptaAnalysis
    {
        public Dictionary<char, int> Frequencies { get; set; }
        public string CipherText { get; set; }

        public CryptaAnalysis(string cipherText)
        {
            CipherText = cipherText;
            Frequencies = new Dictionary<char, int>();
        }

        public bool HasCommonWords(string plainText)
        {
            var count = 0;
            foreach (var word in plainText.Split(' '))
            {
                if (Words.Common.Contains(word))
                    count++;
            }
            return count >= 1;
        }

        public void CalculateCharFrequencies()
        {
            foreach (var ch in CipherText)
            {
                if (char.IsLetter(ch))
                {
                    if (Frequencies.ContainsKey(ch))
                        Frequencies[ch]++;
                    else
                        Frequencies.Add(ch, 1);
                }
            }
        }

        public void PrintCharFrequencies()
        {
            var sorted = from frequency in Frequencies
                         orderby frequency.Value descending
                         select frequency;

            foreach (var frequency in sorted)
            {
                Console.Write($"{frequency.Key,3}|");
            }
            Console.WriteLine();
            foreach (var frequency in sorted)
            {
                Console.Write($"{frequency.Value,3}|");
            }
            Console.WriteLine();
        }
    }
}