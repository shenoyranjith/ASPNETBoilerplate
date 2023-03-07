namespace ASPNetBoilerplate.Domain.Interfaces
{
    /// <summary>
    /// Database connection string resolver
    /// </summary>
    public interface IConnectionResolver
    {
        /// <summary>
        /// Gets or sets DB connection string
        /// </summary>
        /// <value>
        /// DB connection string
        /// </value>
        string ConnectionString { get; set; }
    }
}
