using ShoppingAggSiteHub.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAggSiteHub.Guards
{
    public class Guard
    {
        public static void CheckUrlIsValid(string url)
        {
            bool result = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            if (result == false)
                throw new UrlNotValidException("Image is not a valid URL");
        }

        public static void EntityIsNotNull<T>(object obj, int? lookUpId)
        {
            if (obj == null)
                throw new EntityNotFoundException(lookUpId.HasValue ? $"No entity of type {typeof(T).Name} found for id {lookUpId.Value}" 
                    : $"No entity of type {typeof(T).Name} found");
        }
        public static void DecimalIsNotNegative(decimal value)
        {
            if (value < 0)
            {
                throw new PositiveValueException($"value cannot be less than zero, value inputted as {value}");
            }
        }
        public static void PriceToTwoDecimalPlaces(decimal value)
        {
            bool IsValid(decimal rate) { return rate % 0.01m == 0; };
            bool priceResult = IsValid(value);
            if (priceResult == false)
                throw new PriceFormatException($"{value} needs to be no more than 2 decimal places");
        }
    }
}
