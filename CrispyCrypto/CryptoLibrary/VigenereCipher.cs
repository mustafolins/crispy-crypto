using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLibrary
{
    public static class VigenereCipher
    {
        public static string VigenereEncode(this string plainText, string key)
        {
            var results = new StringBuilder();

            var keyPosition = 0;
            foreach (var ch in plainText)
            {
                if (char.IsLetter(ch))
                {
                    if (keyPosition == key.Length)
                    {
                        keyPosition = 0;
                    }

                    results.Append(ch.VigenereEncodeChar(key[keyPosition]));

                    keyPosition++; 
                }
            }

            return results.ToString();
        }

        public static char VigenereEncodeChar(this char plain, char key)
        {
            return (char)((char.ToLower(plain) - 'a' + (char.ToLower(key) - 'a')) % 26 + 'a');
        }

        public static void PrinteVigenereEncoding(this string plainText, string key)
        {
            Console.Write("Plain:\t");
            foreach (var ch in plainText)
            {
                if (char.IsLetter(ch))
                {
                    Console.Write($"{char.ToLower(ch),2}|");
                }
            }
            Console.Write("\nKey:\t");
            var keyPosition = 0;
            foreach (var ch in plainText)
            {
                if (char.IsLetter(ch))
                {
                    if (keyPosition == key.Length)
                    {
                        keyPosition = 0;
                    }

                    Console.Write($"{key[keyPosition],2}|");

                    keyPosition++;
                }
            }
            Console.Write("\nCipher:\t");
            foreach (var ch in plainText.VigenereEncode(key))
            {
                Console.Write($"{char.ToLower(ch),2}|");
            }
            Console.WriteLine();
        }
    }
}
