using System.Collections.Generic;

namespace DiscountOffers.Interfaces
{

    /// <summary>
    /// Class that handles finding optimized total suitability scores for customer & prooduct sets.
    /// </summary>
    public interface ISuitabilityOptimizer
    {
        /// <summary>
        /// Returns the optimized suitability scores for given customer and product sets and provided ISuitabilityCalculator.
        /// </summary>
        /// <param name="customers">List of customers in the set.</param>
        /// <param name="products">List of products in the set.</param>
        /// <param name="suitabilityCalculator">An implemented suitability calculator. Used to calculate individual customer:product suitability scores.</param>
        /// <returns>The maximum suitability score possible for the set.</returns>
        double MaximizeSuitabilityScore(IEnumerable<ICustomer> customers, IEnumerable<IProduct> products, ISuitabilityCalculator suitabilityCalculator);
    }
}
