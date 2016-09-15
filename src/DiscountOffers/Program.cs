using System;
using DiscountOffers.Classes;
using DiscountOffers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountOffers
{
    //Andrew Lockwood, Sept 2, 2016
    //Challenge info: http://www.codeeval.com/public_sc/48/

    public class Program
    {
        //Exit code reference: https://msdn.microsoft.com/en-us/library/windows/desktop/ms681382(v=vs.85).aspx
        private const int ERROR_BAD_ARGUMENTS = 160;

        //Character separating products from customers in the input file.
        private const char CustomerProductListSeparator = ';';
        //Character separating customers from other customers/products from other products after splitting each line of the input file.
        private const char CustomerProductSeparator = ',';

        //Dependency injection service provider. Setup in ConfigureServices().
        private static IServiceProvider ServiceProvider;

        public static void Main(string[] args)
        {
            ServiceProvider = ConfigureServices();

            if (!ValidateArgs(args))
            {
                ShowUse(null);
                Environment.Exit(ERROR_BAD_ARGUMENTS); 
            }

            try
            {
                string inputFile = args[0];
                SuitabilityProcessor sp = ServiceProvider.GetRequiredService<SuitabilityProcessor>();

                foreach (double result in sp.ProcessFile(inputFile, CustomerProductListSeparator, CustomerProductSeparator))
                {
                    WriteOutput(result);
                }
            }
            catch (Exception e)
            {
                ShowUse($"An error occured while attempting to get scores. Error Message: [{e.Message}]");
            }
        }

        //Configure DI service provider for methods in the core as well as any dependencies.
        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection sc = new ServiceCollection();

            //Each call for an ICustomer or IProduct will return a new Customer or Product.
            sc.AddTransient<ICustomer, Customer>();
            sc.AddTransient<IProduct, Product>();

            //Instantiate the name parser, suitability calculator, and optimizer this app will use as singletons
            sc.AddSingleton<INameParser>(new AsciiTextNameParser());
            sc.AddSingleton<ISuitabilityCalculator>(new SuitabilityCalculator());
            sc.AddSingleton<ISuitabilityOptimizer>(new HungarianSuitabilityOptimizer());

            //Register the class that does the processing taking advantange of the better IServiceProvider injection.
            sc.AddTransient<SuitabilityProcessor>();

            return sc.BuildServiceProvider();
        }

        /// <summary>
        /// Simple console-app specific output method. Takes in the double that SuitabilityProcessor yields, formats it according to spec, prints to screen.
        /// </summary>
        /// <param name="optimizedScore">The optimized score that's been calculated for each group of customers:products</param>
        private static void WriteOutput(double optimizedScore)
        {
            Console.WriteLine(optimizedScore.ToString("N2"));
            //If running in debug mode, also write to the debug listener. Small performance overhead.
            System.Diagnostics.Debug.WriteLine(optimizedScore.ToString("N2"));
        }

        /// <summary>
        /// Simple app use reminder. Also accepts a message that can be printed for simplistic error handling.
        /// </summary>
        /// <param name="message">Optional message that will be printed on screen before the usage text.</param>
        private static void ShowUse(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine(message);
            }
            Console.WriteLine("Usage: dotnet run [PATH_TO_INPUT_FILE]");
            Console.WriteLine("[PATH_TO_INPUT_FILE] should be the full path to the file and the account executing the application must have read permission.");
        }

        /// <summary>
        /// Validate that at least one parameter was passed in. This should be the path to the configuration file. 
        /// </summary>
        /// <param name="args">Default string array of arguments passed to application.</param>
        /// <returns>True IFF args has a length = 1. False in all other cases.</returns>
        private static bool ValidateArgs(string[] args)
        {
            if (args != null && args.Length == 1)
            {
                return true;
            }

            return false;
        }
    }
}
