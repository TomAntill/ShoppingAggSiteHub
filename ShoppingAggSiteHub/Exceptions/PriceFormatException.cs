using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAggSiteHub.Exceptions
{
    public class PriceFormatException : Exception
    {
        public PriceFormatException(string message) : base(message) { }
    }
}
