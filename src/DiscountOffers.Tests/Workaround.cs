using System;

namespace DiscountOffers.Tests
{
    public class Workaround
    {
        //Found some quirks between development environments. Doing this so Community Edition VS 2013 plays nice with the solution.
        //Build and debug are working.
        [STAThread]
        static void Main()
        {
        }
    }
}
