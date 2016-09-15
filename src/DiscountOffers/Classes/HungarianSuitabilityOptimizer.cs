using System;
using System.Collections.Generic;
using System.Linq;
using DiscountOffers.Interfaces;

namespace DiscountOffers.Classes
{
    /// <summary>
    /// Implemenataion of an ISuitabilityOptimizer. This implementation specifically implements "The Hungarian Algorithm".
    /// See comments in HungarianAlgorithm.cs for more info on that.
    /// </summary>
    public class HungarianSuitabilityOptimizer : ISuitabilityOptimizer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customers">List of customers so suitability score optimization.</param>
        /// <param name="products">List of products.</param>
        /// <param name="suitabilityCalculator">The suitability calculator implementation to use for determining suitability score.</param>
        /// <returns>The maximum score possible for the combination of customers, products, and scoring algorithm.</returns>
        public double MaximizeSuitabilityScore(IEnumerable<ICustomer> customers, IEnumerable<IProduct> products, ISuitabilityCalculator suitabilityCalculator)
        {
            if (customers == null || products == null || !customers.Any() || !products.Any())
            {
                return 0;
            }
            
            double[,] negativeScoreMatrix = CreateCustomerProductAssignmentNegativeScoreMatrix(customers, products, suitabilityCalculator);
            GraphAlgorithms.HungarianAlgorithm ha = new GraphAlgorithms.HungarianAlgorithm(negativeScoreMatrix);
            int[] a = ha.Run();

            double score = 0;
            foreach (int i in a)
            {
                //Add the absolute value as scores were made negative when creating the matrix (bipartite graph) but a positive result is expected.
                //Array layout is [customer, product];
                score += Math.Abs(negativeScoreMatrix[i, a[i]]);
            }

            return score;
        }

        /// <summary>
        /// Creates the matrix required for the Hungarian Algorithm. The matrix must be square, and the values are made negative to get the max cost. Positive values would return
        /// the lowest cost.
        /// </summary>
        /// <param name="customers">All the customers to be put into the matrix.</param>
        /// <param name="products">All the products to be put in the matrix.</param>
        /// <param name="suitabilityCalulator">Suitability scoring alogorithm to use.</param>
        /// <returns>Returns a 2D array of suitability scores in the form [customer, product]</returns>
        private double[,] CreateCustomerProductAssignmentNegativeScoreMatrix(IEnumerable<ICustomer> customers, IEnumerable<IProduct> products, ISuitabilityCalculator suitabilityCalulator)
        {
            int customerCount = customers.Count();
            int productCount = products.Count();
            
            //To use the Hungarian Algorithm the matrix must be square. Counting forces an enumeration but it's required to setup a square matrix with all 0 values.
            int matrixDimension = customerCount > productCount ? customerCount : productCount;
            double[,] scoreMatrix = new double[matrixDimension, matrixDimension];

            int cCount = 0;
            //Loop through the customer list and get their score for each product.
            foreach (ICustomer c in customers)
            {
                int pCount = 0;
                foreach (IProduct p in products)
                {
                    //Hungarian algorithm searches for least cost path. Score *-1 causes it to find the max cost path.
                    scoreMatrix[cCount,pCount] = suitabilityCalulator.GetSuitabilityScore(c, p) * -1;
                    pCount++;
                }
                cCount++;
            }

            return scoreMatrix;
        }
    }
}
