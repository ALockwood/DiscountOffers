using System;
using System.Linq;
using System.Text;
using DiscountOffers.Classes;
using Xunit;

namespace DiscountOffers.Tests
{
    public class AsciiTextNameParserTests
    {
        #region Setup
        private AsciiTextNameParser _sut = new AsciiTextNameParser();

        private const string NullName = null;
        private const string EmptyName = "";

        //By using all 7 bit ASCII characters we test that upper and lower case letters are coutned and all other chars are ignored.
        //This produces 128 characters with 52 letters, 40 consonants, and 12 vowels, accounting for upper and lower case letters.
        private static readonly byte[] All7BitAsciiCharsArray = Enumerable.Range(0, 127).Select(x => BitConverter.GetBytes(x)[0]).ToArray();
        private static readonly ASCIIEncoding AsciiEncoder = new ASCIIEncoding();
        private static readonly string AllAsciiChars = AsciiEncoder.GetString(All7BitAsciiCharsArray);
        #endregion

        #region LetterCount Tests
        [Theory]
        [InlineData(NullName)]
        [InlineData(EmptyName)]
        public void LetterCount_NullOrWhiteSpaceName_ZeroLetters(string value)
        {
            Assert.Equal(0, _sut.LetterCount(value));
        }

        [Fact]
        public void LetterCount_All7BitAsciiCharacters_52Letters()
        {
            Assert.Equal(52, _sut.LetterCount(AllAsciiChars));
        }
        #endregion

        #region VowelCount Tests
        [Fact]
        public void VowelCount_All7BitAsciiCharacters_12Vowels()
        {
            Assert.Equal(12, _sut.VowelCount(AllAsciiChars));
        }

        [Theory]
        [InlineData(NullName)]
        [InlineData(EmptyName)]
        public void VowelCount_NullOrWhiteSpaceName_ZeroVowels(string value)
        {
            Assert.Equal(0, _sut.VowelCount(value));
        }
        #endregion

        #region ConsonantCount Tests
        [Fact]
        public void ConconantCount_CaseInsensitiveMatching_6Vowels()
        {
            Assert.Equal(40, _sut.ConsonantCount(AllAsciiChars));
        }

        [Theory]
        [InlineData(NullName)]
        [InlineData(EmptyName)]
        public void ConsonantCount_NullOrWhiteSpaceName_ZeroConsonants(string value)
        {
            Assert.Equal(0, _sut.ConsonantCount(value));
        }
        #endregion
    }
}
