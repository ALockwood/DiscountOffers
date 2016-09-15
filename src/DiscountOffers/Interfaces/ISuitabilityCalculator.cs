namespace DiscountOffers.Interfaces
{
    /// <summary>
    /// Represents a class that is capable of calculating the suitability score (ss) for a given customer and product.
    /// Allows easy change of ss algorithms.
    /// </summary>
    public interface ISuitabilityCalculator
    {
        /// <summary>
        /// Provides the suitability score for a customer & product match.
        /// </summary>
        /// <param name="customer">A customer</param>
        /// <param name="product">A product</param>
        /// <returns>Suitability score.</returns>
        double GetSuitabilityScore(ICustomer customer, IProduct product);
    }
}