using System.Numerics;
using DiscountOffers.Interfaces;

namespace DiscountOffers.Classes
{
    /// <summary>
    /// Basic implementation of ISuitabilityCalculator
    /// </summary>
    public class SuitabilityCalculator : ISuitabilityCalculator
    {
        //Suitability Calculator Rules (from challenge site):
        //Product Name - Even Letters
        //  #CustomerNameVowelsCount x 1.5
        //Product Name - Odd Letters
        //  #CustomerNameConsonantsCount
        //gcd(ProductName.LetterCount & CustomerName.LetterCount) >1 then ss x 1.5

        private const double EvenProductNameMultiplier = 1.5;
        private const double GreatestCommonDivisorMultiplier = 1.5;

        public double GetSuitabilityScore(ICustomer customer, IProduct product)
        {
            if (customer == null || product == null)
            {
                return 0;
            }

            if (customer.LetterCount == 0 || product.LetterCount == 0)
            {
                return 0;
            }

            double suitabilityScore;

            //Check for parity.
            if (product.LetterCount % 2 == 0)
            {
                suitabilityScore = customer.VowelCount * EvenProductNameMultiplier;
            }
            else
            {
                suitabilityScore = customer.ConsonantCount;
            }

            //If product letter count and customer letter count share a common factor other than 1 then apply ss multipler
            if (BigInteger.GreatestCommonDivisor(product.LetterCount, customer.LetterCount) > 1)
            {
                suitabilityScore *= GreatestCommonDivisorMultiplier;
            }

            return suitabilityScore;
        }
    }
}
