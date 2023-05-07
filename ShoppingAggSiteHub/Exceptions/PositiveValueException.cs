using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAggSiteHub.Exceptions
{
    public class PositiveValueException : Exception
    {
        public PositiveValueException(string message) : base(message) { }
    }
}
