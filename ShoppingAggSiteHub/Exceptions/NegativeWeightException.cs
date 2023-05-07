using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAggSiteHub.Exceptions
{
    public class NegativeWeightException : Exception
    {
        public NegativeWeightException(string message) : base(message) { }
    }
}
