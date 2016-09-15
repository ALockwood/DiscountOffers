using System.Text.RegularExpressions;
using DiscountOffers.Interfaces;

namespace DiscountOffers.Classes
{
    public class AsciiTextNameParser : INameParser
    {
        //Case insensitive regex matching is used so so letters should not be duplicated in the sets below. Letters are any character in the vowel or consonant set.
        //The set of characters to be considered vowel letters. 
        private const string Vowels = "AEIOUY";
        //The set of characters to be considered vowel letters.
        private const string Consonants = "BCDFGHJKLMNPQRSTVWXZ";

        public int VowelCount(string name)
        {
            return GetLetterCount(name, Vowels);
        }

        public int ConsonantCount(string name)
        {
            return GetLetterCount(name, Consonants);
        }

        public int LetterCount(string name)
        {
            return GetLetterCount(name, Vowels + Consonants);
        }

        private static int GetLetterCount(string name, string pattern)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return 0;
            }

            return Regex.Matches(name, $"[{pattern}]", RegexOptions.IgnoreCase).Count;
        }
    }
}
