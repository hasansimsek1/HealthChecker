using System;
using System.ComponentModel.DataAnnotations;

namespace HealthChecker.Business.Utilities
{
    /// <summary>
    /// A validation attribute that checks incomming string is a valid URL
    /// </summary>
    public class ValidUrlAttribute : ValidationAttribute
    {
        /// <summary>
        /// Checks if the URL is absolute and starts with http or https
        /// </summary>
        public override bool IsValid(object value)
        {
            return Uri.TryCreate(value.ToString(), UriKind.Absolute, out var uriResult)
              && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
