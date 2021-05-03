namespace HealthChecker.Data.Definitions
{
    /// <summary>
    /// Enum for passing exception locations from catch blocks to <see cref="Result"/> class to log exception in the app.
    /// </summary>
    public enum ExceptionLocations
    {
        SqlRepositoryInsert,
        SqlRepositoryUpdate,
        SqlRepositoryDelete,
        SqlRepositoryGet,
        SqlRepositoryGetById,
        SqlRepositoryGetByFilter
    }
}
