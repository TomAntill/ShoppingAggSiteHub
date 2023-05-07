using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAggSiteHub.Exceptions
{
    public class UrlNotValidException : Exception
    {
        public UrlNotValidException(string message) : base(message) { }
    }
}
