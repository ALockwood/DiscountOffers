using DiscountOffers.Classes;
using DiscountOffers.Interfaces;
using NSubstitute;
using Xunit;

namespace DiscountOffers.Tests
{
    public class ProductTests
    {
        IProduct _sut;
        INameParser _nameParser = Substitute.For<INameParser>();

        string _dummyString = null;
        string _productName = "A New Pen";
        int _letterCount = 7;

        #region Property Setting Tests
        [Fact]
        public void Property_ProductName_ConfiguresCorrectly_AndrewLockwood()
        {
            _sut = new Product();
            _nameParser.LetterCount(_productName).Returns(_letterCount);

            _sut.Configure(_productName, _nameParser);

            Assert.Equal(_productName, _sut.ProductName);
        }

        [Fact]
        public void PropertyLetterCount_ConfiguresCorrectly()
        {
            _sut = new Product();
            _nameParser.LetterCount(_productName).Returns(_letterCount);

            _sut.Configure(_productName, _nameParser);

            Assert.Equal(_letterCount, _sut.LetterCount);
        }
        #endregion

        #region Negative Tests
        public void PropertyConsonantCount_Returns0()
        {
            _sut =new Product();
            _nameParser.LetterCount(_dummyString).Returns(_letterCount);

            _sut.Configure(_dummyString, _nameParser);

            Assert.Equal(0, _sut.LetterCount);
        }
        #endregion
    }
}
