using DiscountOffers.Interfaces;

namespace DiscountOffers.Classes
{
    /// <summary>
    /// Basic implementation of ICustomer.
    /// No default constructor, use Configure to setup the object properties.
    /// </summary>
    public class Customer : ICustomer
    {
        public string CustomerName { get; private set; }

        public int ConsonantCount { get; private set; }

        public int VowelCount { get; private set; }

        public int LetterCount { get; private set; }
        
        public void Configure(string customerName, INameParser nameParser)
        {
            if (!string.IsNullOrWhiteSpace(customerName))
            {
                CustomerName = customerName;
                ConsonantCount = nameParser.ConsonantCount(customerName);
                VowelCount = nameParser.VowelCount(customerName);
                LetterCount = nameParser.LetterCount(customerName);
            }
        }
    }
}
