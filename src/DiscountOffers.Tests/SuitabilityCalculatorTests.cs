using DiscountOffers.Classes;
using DiscountOffers.Interfaces;
using NSubstitute;
using Xunit;

namespace DiscountOffers.Tests
{
    public class SuitabilityCalculatorTests
    {
        ///<see cref="SuitabilityCalculator" for logic comments/>

        #region Setup
        private SuitabilityCalculator _sut = new SuitabilityCalculator();

        private ICustomer _customer = Substitute.For<ICustomer>();
        private IProduct _product = Substitute.For<IProduct>();

        private const string ProductName_Even_16Letters_7Vowels_9Consonants = "Wirewood Symbiote";
        private const string ProductName_Odd_7Letters_2Vowels_5Consonants = "Battery";
        private const string ProductName_Odd_21Letters_6Vowels_15Consonants = "Battery Battery Battery";

        private const string CustomerName_Even_4Letters_0_Vowels_4Consonants = "DFGH";
        private const string CustomerName_Even_4Letters_4_Vowels_0Consonants = "aeio";
        private const string CustomerName_Odd_3Letters_2_Vowels_1Consonants = "aCl";
        private const string CustomerName_Odd_1Letter_1_Vowels_0Consonants = "a";
        #endregion
        
        #region Even Product Name
        [Fact]
        public void GetSuitabilityScore_EvenProductLetters_Customer4Letters0Vowels4Consonants_SharedFactor_Equals0()
        {
            _customer.LetterCount.Returns(4);
            _customer.ConsonantCount.Returns(4);
            _customer.VowelCount.Returns(0);
            _customer.CustomerName.Returns(CustomerName_Even_4Letters_0_Vowels_4Consonants);

            _product.ProductName.Returns(ProductName_Even_16Letters_7Vowels_9Consonants);
            _product.LetterCount.Returns(16);
            
            //(0 x 1.5) x 1.5 = 0
            Assert.Equal(0, _sut.GetSuitabilityScore(_customer, _product));
        }

        [Fact]
        public void GetSuitabilityScore_EvenProductLetters_Customer4Letters4Vowels0Consonants_SharedFactor_Equals9()
        {
            _customer.LetterCount.Returns(4);
            _customer.ConsonantCount.Returns(0);
            _customer.VowelCount.Returns(4);
            _customer.CustomerName.Returns(CustomerName_Even_4Letters_4_Vowels_0Consonants);

            _product.ProductName.Returns(ProductName_Even_16Letters_7Vowels_9Consonants);
            _product.LetterCount.Returns(16);

            //(4 x 1.5) x 1.5 = 9
            Assert.Equal(9, _sut.GetSuitabilityScore(_customer, _product));
        }

        [Fact]
        public void GetSuitabilityScore_EvenProductLetters_Customer4Letters4Vowels0Consonants_NoSharedFactor_Equals1_5()
        {
            _customer.LetterCount.Returns(1);
            _customer.ConsonantCount.Returns(0);
            _customer.VowelCount.Returns(1);
            _customer.CustomerName.Returns(CustomerName_Odd_1Letter_1_Vowels_0Consonants);

            _product.ProductName.Returns(ProductName_Even_16Letters_7Vowels_9Consonants);
            _product.LetterCount.Returns(16);

            //(1 x 1.5) = 1.5
            Assert.Equal(1.5, _sut.GetSuitabilityScore(_customer, _product));
        }
        #endregion

        #region Odd Product Name
        [Fact]
        public void GetSuitabilityScore_OddProductLetters_Customer4Letters4Vowels0Consonants_NoSharedFactor_Equals0()
        {
            _customer.LetterCount.Returns(4);
            _customer.ConsonantCount.Returns(0);
            _customer.VowelCount.Returns(4);
            _customer.CustomerName.Returns(CustomerName_Even_4Letters_4_Vowels_0Consonants);

            _product.ProductName.Returns(ProductName_Odd_7Letters_2Vowels_5Consonants);
            _product.LetterCount.Returns(7);

            //0 x 0 = 0
            Assert.Equal(0, _sut.GetSuitabilityScore(_customer, _product));
        }

        [Fact]
        public void GetSuitabilityScore_OddProductLetters_Customer4Letters0Vowels4Consonants_NoSharedFactor_Equals4()
        {
            _customer.LetterCount.Returns(4);
            _customer.ConsonantCount.Returns(4);
            _customer.VowelCount.Returns(0);
            _customer.CustomerName.Returns(CustomerName_Even_4Letters_0_Vowels_4Consonants);

            _product.ProductName.Returns(ProductName_Odd_7Letters_2Vowels_5Consonants);
            _product.LetterCount.Returns(7);

            //4
            Assert.Equal(4, _sut.GetSuitabilityScore(_customer, _product));
        }

        [Fact]
        public void GetSuitabilityScore_OddProductLetters_Customer3Letters2Vowels1Consonants_SharedFactor_Equals1_5()
        {
            _customer.LetterCount.Returns(3);
            _customer.ConsonantCount.Returns(1);
            _customer.VowelCount.Returns(3);
            _customer.CustomerName.Returns(CustomerName_Odd_3Letters_2_Vowels_1Consonants);

            _product.ProductName.Returns(ProductName_Odd_21Letters_6Vowels_15Consonants);
            _product.LetterCount.Returns(21);

            //1 x 1.5 = 1.5
            Assert.Equal(1.5, _sut.GetSuitabilityScore(_customer, _product));
        }
        #endregion

        #region Null Product/Customer
        [Fact]
        public void GetSuitabilityScore_EmptyProduct_Equals0()
        {
            _product = null;

            _customer.LetterCount.Returns(1);
            _customer.ConsonantCount.Returns(0);
            _customer.VowelCount.Returns(1);
            _customer.CustomerName.Returns(CustomerName_Odd_1Letter_1_Vowels_0Consonants);

            Assert.Equal(0, _sut.GetSuitabilityScore(_customer, _product));
        }

        [Fact]
        public void GetSuitabilityScore_EmptyCustomer_Equals0()
        {
            _product.ProductName.Returns(ProductName_Even_16Letters_7Vowels_9Consonants);
            _product.LetterCount.Returns(16);

            _customer = null;

            Assert.Equal(0, _sut.GetSuitabilityScore(_customer, _product));
        }
        #endregion
    }
}
