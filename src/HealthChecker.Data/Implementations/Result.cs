using HealthChecker.Data.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthChecker.Data.Implementations
{
    /// <summary>
    /// Base return type of operations in the application.
    /// </summary>
    /// <typeparam name="T">Type of the data that will return from operation.</typeparam>
    public class Result<T> : IResult<T>
    {
        /*
         * TODO : 
         *      Specialize success and error results by taking related logic to new types like ErrorResult that inherits from this class.
         */

        public Result()
        {
            Errors = new List<string>();
        }

        public Result(T entity)
        {
            Errors = new List<string>();
            Data = entity;
        }


        public Result(Exception ex, ExceptionLocations location)
        {
            Errors = new List<string> { ex.Message };
            ErrorLocation = location.ToString();
        }

        public Result(Exception ex, ExceptionLocations location, T entity)
        {
            Errors = new List<string> { ex.Message };
            Data = entity;
            ErrorLocation = location.ToString();
        }

        public bool HasError => Errors.Any();

        public bool HasData => Data != null;

        public bool IsSuccessfulAndHasData => !HasError && HasData;


        public List<string> Errors { get; set; }

        public string ErrorLocation { get; set; }


        public T Data { get; set; }

        private void Log()
        {
            // log the error
        }

    }
}
