using DiscountOffers.Interfaces;

/// <summary>
/// Basic implementation of IProduct.
/// No default constructor, use Configure to setup the object properties.
/// </summary>
namespace DiscountOffers.Classes
{
    public class Product : IProduct
    {
        public string ProductName { get; private set; }

        public int LetterCount { get; private set; }

        public void Configure(string productName, INameParser nameParser)
        {
            if (!string.IsNullOrWhiteSpace(productName))
            {
                ProductName = productName;
                LetterCount = nameParser.LetterCount(productName);
            }
        }
    }
}
