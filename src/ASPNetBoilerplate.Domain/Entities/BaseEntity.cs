namespace ASPNetBoilerplate.Domain.Entities
{
    /// <summary>
    /// Represents base entity
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Gets or sets Id of given entity
        /// </summary>
        /// <value>
        /// Id of given entity
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets User's CreatedAt
        /// </summary>
        /// <value>
        /// User's CreatedAt
        /// </value>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets User's UpdatedAt
        /// </summary>
        /// <value>
        /// User's UpdatedAt
        /// </value>
        public DateTime UpdatedAt { get; set; }
    }
}
