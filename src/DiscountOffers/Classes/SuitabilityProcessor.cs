using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DiscountOffers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountOffers.Classes
{
    /// <summary>
    /// Accepts a path to an input file and separator values. 
    /// Loads the target file, processes each line into lists of customers and products, then sends the lists to a ISuitabilityOptimizer to get the max suitability score.
    /// Streams the results back to the caller via IEnumerable and yield return.
    /// </summary>
    public class SuitabilityProcessor
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Class constructor. May be initialized via DI, or a service provider could be passed.
        /// Depends on ISuitabilityOptimizer, ICustomer, IProduct, ISuitabilityCalculator
        /// </summary>
        /// <param name="serviceProvider">Di Service provider object.</param>
        public SuitabilityProcessor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Responsible for parsing the input file, passing the groups of customers and products to an ISuitabilityOptimizer.
        /// </summary>
        /// <param name="filePath">The full path to the input file.</param>
        /// <param name="customerProductListSeparator">The character separating products from customers. Defined as ; in the challenge.</param>
        /// <param name="customerProductSeparator">The character separating individual customers/products. Defined as , in the challenge.</param>
        /// <returns>IEnumerable<double> of optimized results. Each result is an input file line optimized.</double></returns>
        public IEnumerable<double> ProcessFile(string filePath, char customerProductListSeparator, char customerProductSeparator)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Failed to locate file.", filePath);
            }

            ISuitabilityOptimizer optimizer = _serviceProvider.GetRequiredService<ISuitabilityOptimizer>();

            using (FileStream fs = File.OpenRead(filePath))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.ASCII))
                {
                    string pairings;
                    while ((pairings = sr.ReadLine()) != null)
                    {
                        List<ICustomer> customers = new List<ICustomer>();
                        List<IProduct> products = new List<IProduct>();
                        ParsePairings(pairings, customers, products, customerProductListSeparator, customerProductSeparator);

                        if (customers != null && products != null && customers.Any() && products.Any())
                        {
                            double optimizedScore = optimizer.MaximizeSuitabilityScore(customers, products, _serviceProvider.GetRequiredService<ISuitabilityCalculator>());
                            yield return optimizedScore;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Method that parses each line passed in into individual customer and product lists.
        /// </summary>
        /// <param name="customerProductPairing">The raw line of customers and products.</param>
        /// <param name="customers">The list of customers to be added to. List is cleared prior to use.</param>
        /// <param name="products">The list of products to be added to. List is cleared prior to use.</param>
        /// <param name="customerProductListSeparator">The character separating products from customers. Defined as ; in the challenge.</param>
        /// <param name="customerProductSeparator">The character separating individual customers/products. Defined as , in the challenge.</param>
        private void ParsePairings(string customerProductPairing, List<ICustomer> customers, List<IProduct> products, char customerProductListSeparator, char customerProductSeparator)
        {
            if (string.IsNullOrWhiteSpace(customerProductPairing))
            {
                return;
            }

            string[] pairs = customerProductPairing.Split(customerProductListSeparator);
            if (pairs.Length != 2)
            {
                return;
            }

            string[] customerCsv = pairs[0].Split(customerProductSeparator);
            string[] productCsv = pairs[1].Split(customerProductSeparator);
            if (customerCsv.Length < 1 || productCsv.Length < 1)
            {
                return;
            }

            //Prevent odd errors should refactoring happen and non-empty lists are passed in.
            products.Clear();
            customers.Clear();

            foreach (string c in customerCsv)
            {
                ICustomer customer = _serviceProvider.GetRequiredService<ICustomer>();
                customer.Configure(c, _serviceProvider.GetRequiredService<INameParser>());
                customers.Add(customer);
            }

            foreach (string p in productCsv)
            {
                IProduct product = _serviceProvider.GetRequiredService<IProduct>();
                product.Configure(p, _serviceProvider.GetRequiredService<INameParser>());
                products.Add(product);
            }
        }
    }
}
