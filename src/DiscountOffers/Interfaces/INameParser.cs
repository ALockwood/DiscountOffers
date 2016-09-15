namespace DiscountOffers.Interfaces
{
    /// <summary>
    /// Interface for name parsing classes. Allows easy change of name parsing rules.
    /// </summary>
    public interface INameParser
    {
        //The number of vowels counted in the name passed in.
        int VowelCount(string name);
        //The number of consonants coutned in the name passed in.
        int ConsonantCount(string name);
        //The number of letters counted in the name passed in.
        int LetterCount(string name);
    }
}
