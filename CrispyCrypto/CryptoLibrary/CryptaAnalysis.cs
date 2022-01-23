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

        public string MostCommonPolygram(string plainText, int size)
        {
            var dict = new Dictionary<string, int>();

            foreach (var word in GetWords(plainText))
            {
                for (int i = 0; i < word.Length - size; i++)
                {
                    var diGram = word.Substring(i, size);
                    if (dict.ContainsKey(diGram))
                        dict[diGram]++;
                    else
                        dict.Add(diGram, 1);
                }
            }

            var result = new KeyValuePair<string, int>(null, 0);
            foreach (var keyValue in dict)
            {
                if (keyValue.Value > result.Value)
                {
                    result = keyValue;
                }
            }
            return result.Key;
        }

        public bool HasCommonWords(string plainText)
        {
            var count = 0;
            foreach (var word in GetWords(plainText))
            {
                if (Words.Common.Contains(word))
                    count++;
            }
            return count >= 1;
        }

        private static string[] GetWords(string plainText)
        {
            return plainText.Split(' ');
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