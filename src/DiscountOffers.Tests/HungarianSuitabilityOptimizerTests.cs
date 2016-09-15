using System.Collections.Generic;
using DiscountOffers.Classes;
using DiscountOffers.Interfaces;
using NSubstitute;
using Xunit;

namespace DiscountOffers.Tests
{
    public class HungarianSuitabilityOptimizerTests
    {
        ISuitabilityOptimizer _sut;
        IProduct _product = Substitute.For<IProduct>();
        ICustomer _customer = Substitute.For<ICustomer>();
        ISuitabilityCalculator _suitabilityCalulator = Substitute.For<ISuitabilityCalculator>();

        IEnumerable<IProduct> _products;
        IEnumerable<ICustomer> _customers;
        
        #region Null Product/Customer Tests
        [Fact]
        public void MaximizeSuitabilityScore_NullCustomers_ValidProducts_Returns0()
        {
            _sut = new HungarianSuitabilityOptimizer();

            _product.ProductName.Returns("Test");
            _product.LetterCount.Returns(4);

            Assert.Equal(0, _sut.MaximizeSuitabilityScore(_customers, new[] { _product }, _suitabilityCalulator));
        }

        [Fact]
        public void MaximizeSuitabilityScore_ValidCustomersNullProducts_Returns0()
        {
            _sut = new HungarianSuitabilityOptimizer();

            _customer.CustomerName.Returns("Test");
            _customer.ConsonantCount.Returns(3);
            _customer.VowelCount.Returns(1);
            _customer.LetterCount.Returns(4);

            Assert.Equal(0, _sut.MaximizeSuitabilityScore(new[] { _customer }, _products, _suitabilityCalulator));
        }

        [Fact]
        public void MaximizeSuitabilityScore_NullCustomersNullProducts_Returns0()
        {
            _sut = new HungarianSuitabilityOptimizer();
            Assert.Equal(0.00, _sut.MaximizeSuitabilityScore(_customers, _products, _suitabilityCalulator));
        }

        #endregion

        #region Single Customer Single Product Tests
        [Fact]
        public void MaximizeSuitabilityScore_SingleCustomerSingleProduct_Returns4()
        {
            _sut = new HungarianSuitabilityOptimizer();

            _product.LetterCount.Returns(8);
            _product.ProductName.Returns("iPad 2 - 4-pack");

            _customer.LetterCount.Returns(11);
            _customer.ConsonantCount.Returns(7);
            _customer.VowelCount.Returns(4);
            _customer.CustomerName.Returns("Jack Abraham");

            //For a single user & product the max score is whatever is set here
            _suitabilityCalulator.GetSuitabilityScore(_customer, _product).Returns(4);

            Assert.Equal(4.0, _sut.MaximizeSuitabilityScore(new[] { _customer }, new[] { _product }, _suitabilityCalulator));
        }
        #endregion

        #region Single Customer Multiple Products Tests
        [Fact]
        public void MaximizeSuitabilityScore_SingleCustomerMultiProduct_Returns17()
        {
            _sut = new HungarianSuitabilityOptimizer();

            _product.LetterCount.Returns(8);
            _product.ProductName.Returns("iPad 2 - 4-pack");

            IProduct _product2 = Substitute.For<IProduct>();
            _product2.LetterCount.Returns(19);
            _product2.ProductName.Returns("Girl Scouts Thin Mints");

            _customer.LetterCount.Returns(11);
            _customer.ConsonantCount.Returns(7);
            _customer.VowelCount.Returns(4);
            _customer.CustomerName.Returns("Jack Abraham");

            //The max should be whatever the max score is here.
            _suitabilityCalulator.GetSuitabilityScore(_customer, _product).Returns(4);
            _suitabilityCalulator.GetSuitabilityScore(_customer, _product2).Returns(17);

            Assert.Equal(17, _sut.MaximizeSuitabilityScore(new[] { _customer }, new[] { _product, _product2 }, _suitabilityCalulator));
        }
        #endregion

        #region MultipleCustomer Single Product Tests
        [Fact]
        public void MaximizeSuitabilityScore_MultiCustomerSingleProduct_Returns17()
        {
            _sut = new HungarianSuitabilityOptimizer();

            _product.LetterCount.Returns(8);
            _product.ProductName.Returns("iPad 2 - 4-pack");

            _customer.LetterCount.Returns(11);
            _customer.ConsonantCount.Returns(7);
            _customer.VowelCount.Returns(4);
            _customer.CustomerName.Returns("Jack Abraham");

            ICustomer _customer2 = Substitute.For<ICustomer>();

            _customer.LetterCount.Returns(9);
            _customer.ConsonantCount.Returns(6);
            _customer.VowelCount.Returns(3);
            _customer.CustomerName.Returns("Jack Abrah");

            //The max should be whatever the max score is here.
            _suitabilityCalulator.GetSuitabilityScore(_customer, _product).Returns(17);
            _suitabilityCalulator.GetSuitabilityScore(_customer2, _product).Returns(4);

            Assert.Equal(17, _sut.MaximizeSuitabilityScore(new[] { _customer, _customer2 }, new[] { _product }, _suitabilityCalulator));
        }
        #endregion

        #region MultipleCustomer Multiple Product Tests
        [Fact]
        public void MaximizeSuitabilityScore_MultiCustomerMultiProduct_EvenMatch_Returns6_5()
        {
            _sut = new HungarianSuitabilityOptimizer();

            _product.LetterCount.Returns(8);
            _product.ProductName.Returns("iPad 2 - 4-pack");

            IProduct _product2 = Substitute.For<IProduct>();
            _product2.LetterCount.Returns(19);
            _product2.ProductName.Returns("Girl Scouts Thin Mints");

            _customer.LetterCount.Returns(11);
            _customer.ConsonantCount.Returns(7);
            _customer.VowelCount.Returns(4);
            _customer.CustomerName.Returns("Jack Abraham");

            ICustomer _customer2 = Substitute.For<ICustomer>();

            _customer.LetterCount.Returns(9);
            _customer.ConsonantCount.Returns(6);
            _customer.VowelCount.Returns(3);
            _customer.CustomerName.Returns("Jack Abrah");

            //Optimal match is 6.5 (via manual inspection)
            _suitabilityCalulator.GetSuitabilityScore(_customer, _product).Returns(1.5);
            _suitabilityCalulator.GetSuitabilityScore(_customer, _product2).Returns(1);
            _suitabilityCalulator.GetSuitabilityScore(_customer2, _product).Returns(1.6);
            _suitabilityCalulator.GetSuitabilityScore(_customer2, _product2).Returns(5);


            Assert.Equal(6.5, _sut.MaximizeSuitabilityScore(new[] { _customer, _customer2 }, new[] { _product, _product2 }, _suitabilityCalulator));
        }

        [Fact]
        public void MaximizeSuitabilityScore_MultiCustomerMultiProduct_UnevenMatch_Returns10()
        {
            _sut = new HungarianSuitabilityOptimizer();

            _product.LetterCount.Returns(8);
            _product.ProductName.Returns("iPad 2 - 4-pack");

            IProduct _product2 = Substitute.For<IProduct>();
            _product2.LetterCount.Returns(19);
            _product2.ProductName.Returns("Girl Scouts Thin Mints");

            IProduct _product3 = Substitute.For<IProduct>();
            _product3.LetterCount.Returns(12);
            _product3.ProductName.Returns("Happy Fun Ball");

            _customer.LetterCount.Returns(11);
            _customer.ConsonantCount.Returns(7);
            _customer.VowelCount.Returns(4);
            _customer.CustomerName.Returns("Jack Abraham");

            ICustomer _customer2 = Substitute.For<ICustomer>();

            _customer.LetterCount.Returns(9);
            _customer.ConsonantCount.Returns(6);
            _customer.VowelCount.Returns(3);
            _customer.CustomerName.Returns("Jack Abrah");

            //Optimal match is 10 (via manual inspection)
            _suitabilityCalulator.GetSuitabilityScore(_customer, _product).Returns(2.68);
            _suitabilityCalulator.GetSuitabilityScore(_customer, _product2).Returns(3);
            _suitabilityCalulator.GetSuitabilityScore(_customer, _product3).Returns(3);

            _suitabilityCalulator.GetSuitabilityScore(_customer2, _product).Returns(3);
            _suitabilityCalulator.GetSuitabilityScore(_customer2, _product2).Returns(2);
            _suitabilityCalulator.GetSuitabilityScore(_customer2, _product3).Returns(7);

            Assert.Equal(10, _sut.MaximizeSuitabilityScore(new[] { _customer, _customer2 }, new[] { _product, _product2, _product3 }, _suitabilityCalulator));
        }
        #endregion


    }
}
