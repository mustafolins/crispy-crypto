using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLibrary
{
    public class ShiftCipherBruteForce
    {
        public string CipherText { get; set; }
        public string PlainText { get; set; }
        public CryptaAnalysis Analysis { get; set; }

        public ShiftCipherBruteForce(string cipherText)
        {
            CipherText = cipherText;
            Analysis = new CryptaAnalysis(cipherText);
        }

        public string BruteForce(int numOfWords = 1)
        {
            string result = CipherText;

            for (int i = 1; i < 'z' - 'a' + 1; i++)
            {
                result = result.LeftShiftString(1);
                if (Analysis.HasCommonWords(result, numOfWords))
                    return result;
            }

            return null;
        }
    }
}
