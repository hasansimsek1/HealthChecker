namespace HealthChecker.Data.Definitions
{
    /// <summary>
    /// Contract for Result classes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResult<T>
    {
        /// <summary>
        /// Result must be able to tell the caller if there is error or not.
        /// </summary>
        bool HasError { get; }
    }
}
