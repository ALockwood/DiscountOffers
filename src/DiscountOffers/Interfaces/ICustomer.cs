namespace DiscountOffers.Interfaces
{
    /// <summary>
    /// Interface for customer objects. Accepts variable INameParsers.
    /// </summary>
    public interface ICustomer
    {
        //The name of the customer.
        string CustomerName { get;}

        //Returns the number of consonants in the customer name.
        int ConsonantCount { get; }
        
        //Retuns the number of vowels in the customer name.
        int VowelCount { get; }
        
        //Number of consonants and vowels in the customer name.
        int LetterCount { get; }

        //(re)Configure all properties
        void Configure(string CustomerName, INameParser nameParser);
    }
}
