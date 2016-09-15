using DiscountOffers.Classes;
using DiscountOffers.Interfaces;
using NSubstitute;
using Xunit;

namespace DiscountOffers.Tests
{
    public class CustomerTests
    {
        ICustomer _sut;
        INameParser _nameParser = Substitute.For<INameParser>();

        string _dummyString = null;
        string _customerName = "Andrew Lockwood";
        int _letterCount = 14;
        int _consonantCount = 9;
        int _vowelCount = 5;


        #region Property Setting Tests
        [Fact]
        public void Property_CustomerName_ConfiguresCorrectly_AndrewLockwood()
        {
            _sut = new Customer();
            _nameParser.LetterCount(_customerName).Returns(_letterCount);
            _nameParser.ConsonantCount(_customerName).Returns(_consonantCount);
            _nameParser.VowelCount(_customerName).Returns(_vowelCount);

            _sut.Configure(_customerName, _nameParser);

            Assert.Equal(_customerName, _sut.CustomerName);
        }

        [Fact]
        public void PropertyLetterCount_ConfiguresCorrectly()
        {
            _sut = new Customer();
            _nameParser.LetterCount(_customerName).Returns(_letterCount);
            _nameParser.ConsonantCount(_customerName).Returns(_consonantCount);
            _nameParser.VowelCount(_customerName).Returns(_vowelCount);

            _sut.Configure(_customerName, _nameParser);

            Assert.Equal(_letterCount, _sut.LetterCount);
        }

        [Fact]
        public void PropertyVowelCount_ConfiguresCorrectly()
        {
            _sut = new Customer();
            _nameParser.LetterCount(_customerName).Returns(_letterCount);
            _nameParser.ConsonantCount(_customerName).Returns(_consonantCount);
            _nameParser.VowelCount(_customerName).Returns(_vowelCount);

            _sut.Configure(_customerName, _nameParser);

            Assert.Equal(_vowelCount, _sut.VowelCount);
        }

        [Fact]
        public void PropertyConsonantCount_ConfiguresCorrectly()
        {
            _sut = new Customer();
            _nameParser.LetterCount(_customerName).Returns(_letterCount);
            _nameParser.ConsonantCount(_customerName).Returns(_consonantCount);
            _nameParser.VowelCount(_customerName).Returns(_vowelCount);

            _sut.Configure(_customerName, _nameParser);

            Assert.Equal(_consonantCount, _sut.ConsonantCount);
        }
        #endregion

        #region Negative Tests
        public void PropertyConsonantCount_Returns0()
        {
            _sut = new Customer();
            _nameParser.LetterCount(_dummyString).Returns(_letterCount);
            _nameParser.ConsonantCount(_dummyString).Returns(_consonantCount);
            _nameParser.VowelCount(_dummyString).Returns(_vowelCount);

            _sut.Configure(_dummyString, _nameParser);

            Assert.Equal(0, _sut.ConsonantCount);
        }
        #endregion
    }
}
