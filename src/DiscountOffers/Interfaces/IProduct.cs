namespace DiscountOffers.Interfaces
{
    public interface IProduct
    {
        //The name of the product.
        string ProductName { get; }

        //The number of letters in the product name.
        int LetterCount { get; }

        //(re)Configure all properties
        void Configure(string CustomerName, INameParser nameParser);
    }
}
