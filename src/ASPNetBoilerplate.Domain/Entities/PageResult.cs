namespace ASPNetBoilerplate.Domain.Entities
{
    /// <summary>
    /// Pagination result entity
    /// </summary>
    /// <typeparam name="TEntity">Entity of type BaseEntity</typeparam>
    public class PageResult<TEntity>
    {
        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>
        /// The total count.
        /// </value>
        public long TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public List<TEntity> Data { get; set; }
    }
}
